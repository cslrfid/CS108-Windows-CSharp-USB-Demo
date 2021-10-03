using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using System.Threading;
using System.Diagnostics;
using System.IO;

namespace CS108_PC_Client
{
    public partial class RFIDUpgradeForm : Form
    {
        enum State
        {
            Normal,
            StartingNVUpdate,
            StartedNVUpdate
        }

        enum NVStatus
        {
            NVMEMUPD_STAT_UPD_SUCCESS,      /* 0x00000000 */
            NVMEMUPD_STAT_RESERVED_01,      /* 0x00000001 */
            NVMEMUPD_STAT_WR_FAIL,          /* 0x00000002 */
            NVMEMUPD_STAT_UNK_CMD,          /* 0x00000003 */
            NVMEMUPD_STAT_CMD_IGN,          /* 0x00000004 */
            NVMEMUPD_STAT_BNDS,             /* 0x00000005 */
            NVMEMUPD_STAT_MAGIC,            /* 0x00000006 */
            NVMEMUPD_STAT_PKTLEN,           /* 0x00000007 */
            NVMEMUPD_STAT_EXIT_ERR,         /* 0x00000008 */
            NVMEMUPD_STAT_EXIT_SUCCESS,     /* 0x00000009 */
            NVMEMUPD_STAT_EXIT_NOWRITES,    /* 0x0000000A */
            NVMEMUPD_STAT_GEN_RXPKT_ERR,    /* 0x0000000B */
            NVMEMUPD_STAT_RESERVED_02,      /* 0x0000000C */
            NVMEMUPD_STAT_INT_MEM_BNDS,     /* 0x0000000D */
            NVMEMUPD_STAT_ENTRY_OK,         /* 0x0000000E */
            NVMEMUPD_STAT_RXPKT_MAX,        /* 0x0000000F */
            NVMEMUPD_STAT_RX_TO,            /* 0x00000010 */
            NVMEMUPD_STAT_CRC_ERR,          /* 0x00000011 */
            /* end of list marker - used for bounds checking in code!*/
            NVMEMUPD_STAT_LAST = NVMEMUPD_STAT_CRC_ERR,
        }

        IntPtr m_hid;
        byte[] m_read_buffer = new byte[65536]; // read ring buffer
        ushort m_read_buffer_size = 0;
        ushort m_read_buffer_head = 0;
        ushort m_read_buffer_tail = 0;

        byte[] m_rfid_buffer = new byte[65536]; // rfid ring buffer
        ushort m_rfid_buffer_size = 0;
        ushort m_rfid_buffer_head = 0;
        ushort m_rfid_buffer_tail = 0;

        bool stopRfidReceive = false;
        bool stopReceive = false;
        bool startNVMemUpdateMode = false;

        readonly object locker = new object();

        State currentState = State.Normal;
        int currentNVStatus = -1;

        public RFIDUpgradeForm(IntPtr hid)
        {
            InitializeComponent();

            this.CenterToScreen();

            EnableCtrls(false);

            m_hid = hid;

            Thread thread1 = new Thread(new ThreadStart(RecvThread));
            thread1.Start();

            stopRfidReceive = false;
            Thread thread2 = new Thread(new ThreadStart(DecodeRfidReceive));
            thread2.Start();
        }

        private void Main_FormClosing(object sender, FormClosingEventArgs e)
        {
            stopRfidReceive = true;
            stopReceive = true;
        }

        private void RecvThread()
        {
            while (!stopReceive)
            {
                // Make sure that we are connected to a device
                if (HID.IsOpened(m_hid))
                {
                    int bufferSize = HID.GetMaxReportRequest(m_hid) * HID.SIZE_MAX_READ;
                    byte[] buffer = new byte[bufferSize];
                    int bytesRead = 0;

                    if (USBSocket.ReceiveData(m_hid, ref buffer, buffer.Length, ref bytesRead, 1000))
                    {
                        if (bytesRead > 0)
                        {
                            for (int i = 0; i < bytesRead; i++)
                            {
                                m_read_buffer[m_read_buffer_head++] = buffer[i];
                            }
                            m_read_buffer_size += (ushort)bytesRead;
                        }
                        while (m_read_buffer_size >= 8)
                        {
                            if (m_read_buffer[m_read_buffer_tail] == Constants.PREFIX)
                            {
                                if (!DecodeCommands())
                                {
                                    break;
                                }
                            }
                            else
                            {
                                //tb_status.Text = "Wrong prefix is received";
                                MessageBox.Show("Wrong prefix is received");
                                ClearReadBuffer();
                            }
                        }
                    }
                }
                else
                {
                    MessageBox.Show("Device is not connected.");
                    this.Close();
                    return;
                }
            }
        }

        private void ClearReadBuffer()
        {
            m_read_buffer_size = 0;
            m_read_buffer_head = 0;
            m_read_buffer_tail = 0;
        }

        private void ClearRfidBuffer()
        {
            m_rfid_buffer_size = 0;
            m_rfid_buffer_head = 0;
            m_rfid_buffer_tail = 0;
        }

        private ushort GetRfidBufferSize()
        {
            lock (this.locker)
            {
                return this.m_rfid_buffer_size;
            }
        }

        private bool DecodeCommands()
        {
            ushort index = m_read_buffer_tail;
            uint prefix = m_read_buffer[index++];
            uint connection = m_read_buffer[index++];
            uint payload_len = m_read_buffer[index++];
            uint total_len = payload_len + 8;
            uint source = m_read_buffer[index++];
            uint reserve = m_read_buffer[index++];
            uint direction = m_read_buffer[index++];

            uint crc = ((uint)m_read_buffer[index++] << 8) | (uint)m_read_buffer[index++];

            if (m_read_buffer_size < total_len) return false; //not enough data

            uint event_code = ((uint)m_read_buffer[index++] << 8) | (uint)m_read_buffer[index++];
            if (source == Constants.TYPE_RFID)
            {
                switch (event_code)
                {
                    case 0x8000:
                        byte status = m_read_buffer[index++];
                        switch (status)
                        {
                            case 0x00:
                                UpdateInfo("Power on successed.");
                                EnableCtrls(true);
                                break;
                            case 0xFF:
                                UpdateInfo("Power on failed with unknown reason.");
                                break;
                            default:
                                UpdateInfo("Unknown status for rfid.");
                                break;
                        }
                        break;
                    case 0x8001:
                        status = m_read_buffer[index++];
                        switch (status)
                        {
                            case 0x00:
                                UpdateInfo("Power off successed.");
                                EnableCtrls(false);
                                break;
                            case 0xFF:
                                UpdateInfo("Power off failed with unknown reason.");
                                break;
                            default:
                                UpdateInfo("Unknown status for rfid.");
                                break;
                        }
                        break;
                    case 0x8002:
                        status = m_read_buffer[index++];
                        switch (status)
                        {
                            case 0x00:
                                //UpdateInfo("Send data successed.");
                                break;
                            case 0xFF:
                                UpdateInfo("Send data failed with unknown reason.");
                                break;
                            default:
                                UpdateInfo("Unknown status for rfid.");
                                break;
                        }
                        break;
                    case 0x8100:
                        ushort len = (ushort)(payload_len - 2);
                        index = m_read_buffer_tail;
                        index += 10;
                        lock (this.locker)
                        {
                            for (int i = 0; i < len; i++)
                            {
                                m_rfid_buffer[m_rfid_buffer_head++] = m_read_buffer[index++];
                            }
                            m_rfid_buffer_size += len;
                        }
                        break;
                    default:
                        UpdateInfo("Unknown event for rfid.");
                        break;
                }
            }
            else if (source == Constants.TYPE_NOTIFY)
            {
                switch (event_code)
                {
                    case 0xA000:
                        // battery percentage
                        break;
                    case 0xA100:
                        // battery fail
                        UpdateInfo("Battery fail.");
                        break; 
                    case 0xA101:
                        // error code
                        uint err_code = ((uint)m_read_buffer[index++] << 8) | (uint)m_read_buffer[index++];
                        UpdateInfo("Error code : " + err_code.ToString());
                        break;
                    default:
                        UpdateInfo("Unknown event for notification.");
                        break;
                }
            }
            else
            {
                UpdateInfo("Wrong source for rfid.");
            }

            m_read_buffer_size -= (byte)total_len;
            m_read_buffer_tail += (byte)total_len;
            //Console.WriteLine("Read Size:" + m_read_buffer_size);

            return true;
        }

        private void DecodeRfidReceive()
        {
            ushort index = 0;
            int pkt_ver = 0;
            int flags = 0;
            int pkt_type = 0;
            int pkt_len = 0;
            int reserve = 0;
            int datalen = 0;
            uint address = 0;
            uint data = 0;
            Stopwatch stopwatch1 = new Stopwatch();
            Stopwatch stopwatch2 = new Stopwatch();

            stopwatch1.Start();
            stopwatch2.Start();
            while (!stopRfidReceive)
            {
                System.Threading.SpinWait.SpinUntil(() => (m_rfid_buffer_size >= 8), 500);
                lock (this.locker)
                {
                    if (m_rfid_buffer_size >= 8)
                    {
                        stopwatch2.Reset();
                        stopwatch2.Start();
                        //get packet header
                        index = m_rfid_buffer_tail;
                        pkt_ver = m_rfid_buffer[index++];
                        flags = m_rfid_buffer[index++];
                        pkt_type = (int)(m_rfid_buffer[index++]) + ((int)(m_rfid_buffer[index++]) << 8);
                        pkt_len = (int)(m_rfid_buffer[index++]) + ((int)(m_rfid_buffer[index++]) << 8);
                        reserve = (int)(m_rfid_buffer[index++]) + ((int)(m_rfid_buffer[index++]) << 8);
                        datalen = pkt_len * 4;
                        m_rfid_buffer_size -= 8;
                        m_rfid_buffer_tail += 8;
                        if (!startNVMemUpdateMode)
                        {
                            if (pkt_ver == 0x40)
                            {
                                switch (flags)
                                {
                                    case 0x02:
                                        UpdateInfo("Reset command response received.");
                                        break;
                                    case 0x03:
                                        UpdateInfo("Abort command response received.");
                                        break;
                                    default:
                                        UpdateInfo("other control command response received.");
                                        break;
                                }
                                continue;
                            }
                            else if ((pkt_ver == 0x70 || pkt_ver == 0x00) && flags == 0x00)
                            {
                                address = (uint)(pkt_type);
                                data = (uint)((reserve << 16) + pkt_len);
                                switch (address)
                                {
                                    case 0x0000:
                                        uint major = (data >> 24);
                                        uint minor = (data >> 12) & 0x7FF;
                                        uint build = data & 0x7FF;
                                        UpdateInfo("Version: " + major.ToString() + "." + minor.ToString() + "." + build.ToString());
                                        break;
                                    default:
                                        UpdateInfo("MAC Address: " + address.ToString("X4") + "; Data: " + data.ToString("X8"));
                                        break;
                                }
                                continue;
                            }
                            else if (pkt_ver != 0x01 && pkt_ver != 0x02 && pkt_ver != 0x03)
                            {
                                UpdateInfo("Unrecognized packet header: " + pkt_ver.ToString("X2"));
                                continue;
                            }
                        }
                        else
                        {
                            if (pkt_ver == 0x0D && flags == 0xF0)
                            {
                            }
                            else
                            {
                                UpdateInfo("Unrecognized NV mem command packet header: " + pkt_ver.ToString("X2"));
                                continue;
                            }
                        }
                    }
                    else {
                        /*if (stopwatch2.ElapsedMilliseconds >= 4000) {
                            UpdateInfo("No data received within 4 seconds.");
                            stopwatch2.Reset();
                            stopwatch2.Start();
                        }*/
                        Thread.Sleep(1); // save CPU usage
                        continue;
                    }
                }

                //wait until the full packet data has come in
                bool inCompletePacket = false;
                stopwatch1.Reset();
                stopwatch1.Start();
                while (GetRfidBufferSize() < datalen)
                {
                    if (stopwatch1.ElapsedMilliseconds >= 3000)
                    {
                        UpdateInfo("Incomplete packet returned.");
                        inCompletePacket = true;
                        break;
                    }
                    Thread.Sleep(1);
                }
                if (inCompletePacket)
                {
                    ClearRfidBuffer();
                    continue;
                }

                lock (this.locker)
                {
                    //finish reading
                    index = m_rfid_buffer_tail;
                    m_rfid_buffer_size -= (ushort)datalen;
                    m_rfid_buffer_tail += (ushort)datalen;
                    if (!startNVMemUpdateMode)
                    {
                        if (pkt_type == 0x8000 || pkt_type == 0x0000)
                        {
                            //UpdateInfo("Command Begin Packet.");
                            continue;
                        }
                        else if (pkt_type == 0x8001 || pkt_type == 0x0001)
                        {
                            //UpdateInfo("Command End Packet.");
                            if (currentState == State.StartingNVUpdate)
                            {
                                currentState = State.StartedNVUpdate;
                                startNVMemUpdateMode = true;
                            }
                            continue;
                        }
                        else if (pkt_type == 0x8007 || pkt_type == 0x0007)
                        {
                            UpdateInfo("Antenna Cycle End Packet.");
                            continue;
                        }
                        else if (pkt_type == 0x8005 || pkt_type == 0x0005)
                        {
                            UpdateInfo("Inventory Packet.");
                            continue;
                        }
                        else if (pkt_type == 0x3007)
                        {
                            address = (uint)(m_rfid_buffer[index++]) + ((uint)(m_rfid_buffer[index++]) << 8) +
                                      ((uint)(m_rfid_buffer[index++]) << 16) + ((uint)(m_rfid_buffer[index++]) << 24);
                            data = (uint)(m_rfid_buffer[index++]) + ((uint)(m_rfid_buffer[index++]) << 8) +
                                   ((uint)(m_rfid_buffer[index++]) << 16) + ((uint)(m_rfid_buffer[index++]) << 24);

                            UpdateInfo("OEM Address: " + address.ToString("X8") + "; Data: " + data.ToString("X8"));
                            continue;
                        }
                        else if (pkt_type == 0x300B)
                        {
                            UpdateInfo("Start NV memroy update. DO NOT turn off power or un-plug USB cable.");
                            currentState = State.StartingNVUpdate;
                            continue;
                        }
                        else
                        {
                            byte[] buf = new byte[datalen];
                            for (int cnt = 0; cnt < datalen; cnt++)
                            {
                                buf[cnt] = m_rfid_buffer[index++];
                            }

                            UpdateInfo("Unknown Packet." + ByteArrayToHexString(buf, datalen));
                            continue;
                        }
                    }
                    else
                    {
                        if (pkt_type == 0x0000)
                        {
                            uint re_cmd = (uint)(m_rfid_buffer[index++]) + ((uint)(m_rfid_buffer[index++]) << 8) +
                                      ((uint)(m_rfid_buffer[index++]) << 16) + ((uint)(m_rfid_buffer[index++]) << 24);
                            uint status = (uint)(m_rfid_buffer[index++]) + ((uint)(m_rfid_buffer[index++]) << 8) +
                                   ((uint)(m_rfid_buffer[index++]) << 16) + ((uint)(m_rfid_buffer[index++]) << 24);

                            currentNVStatus = (int)(status & 0x0FFFFFFFF);
                            if (re_cmd == 0x02)
                            {
                                UpdateInfo(Enum.GetName(typeof(NVStatus), currentNVStatus));
                                currentState = State.Normal;
                                startNVMemUpdateMode = false;
                            }
                            continue;
                        }
                    }
                }
            }
            stopwatch1.Stop();
            stopwatch2.Stop();
        }

        private void btn_rfid_poweron_Click(object sender, EventArgs e)
        {
            byte[] command = RFIDCommands.PowerOn(true);

            if (!USBSocket.TransmitData(m_hid, command, command.Length))
            {
                MessageBox.Show("Device failed to transmit data.");
            }

            Thread.Sleep(500);
            RFIDCommands.GetVersion(m_hid);
        }

        private void btn_rfid_poweroff_Click(object sender, EventArgs e)
        {
            byte[] command = RFIDCommands.PowerOn(false);

            if (!USBSocket.TransmitData(m_hid, command, command.Length))
            {
                MessageBox.Show("Device failed to transmit data.");
            }

            ClearRfidBuffer();
        }

        private void btn_clear_info_Click(object sender, EventArgs e)
        {
            tb_info.Clear();
        }
        
        private byte[] HexStringToByteArray(String HexString)
        {
            if (HexString.Length % 2 == 1)
            {
                throw new Exception("Lenght of hex string cannot be odd.");
            }
            int NumberChars = HexString.Length;
            byte[] bytes = new byte[NumberChars / 2];
            for (int i = 0; i < NumberChars; i += 2)
            {
                bytes[i / 2] = Convert.ToByte(HexString.Substring(i, 2), 16);
            }

            return bytes;
        }

        private string ByteArrayToHexString(byte[] bytes, int length)
        {
            return BitConverter.ToString(bytes, 0, length).Replace("-", string.Empty);
        }

        private delegate void EnableCtrlsDeleg(bool enable);
        private void EnableCtrls(bool enable)
        {
            if (this.InvokeRequired)
            {
                Invoke(new EnableCtrlsDeleg(EnableCtrls), new object[] { enable });
                return;
            }
            btn_rfid_image.Enabled = enable;
            btn_rfid_bootloader.Enabled = enable;
        }

        private delegate void UpdateInfoDeleg(String info);
        private void UpdateInfo(String info)
        {
            if (this.InvokeRequired)
            {
                Invoke(new UpdateInfoDeleg(UpdateInfo), new object[] { info });
                return;
            }
            tb_info.AppendText(info);
            tb_info.AppendText("\r\n");
        }

        private delegate void UpdateProgressDeleg(int value, int block, int total);
        private void UpdateProgress(int value, int block, int total)
        {
            if (this.InvokeRequired)
            {
                Invoke(new UpdateProgressDeleg(UpdateProgress), new object[] { value, block, total });
                return;
            }
            progressBar.Value = value;
            lb_block.Text = block.ToString();
            lb_total.Text = total.ToString();
        }

        private void btn_rfid_image_Click(object sender, EventArgs e)
        {
            try
            {
                using (OpenFileDialog ofile = new OpenFileDialog())
                {
                    ofile.Title = "Please choose Image update file";
                    ofile.Filter = "Image files (image_*.a79)|image_*.a79";
                    if (ofile.ShowDialog() == DialogResult.OK)
                    {
                        btn_rfid_poweroff.Enabled = false;

                        Stream stream = new FileStream(ofile.FileName, FileMode.Open);

                        Thread thread = new Thread(() => UpdateRFIDImage(stream));
                        thread.Start();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btn_rfid_bootloader_Click(object sender, EventArgs e)
        {
            try
            {
                using (OpenFileDialog ofile = new OpenFileDialog())
                {
                    ofile.Title = "Please choose Bootloader update file";
                    ofile.Filter = "Image files (bootloader_*.a79)|bootloader_*.a79";
                    if (ofile.ShowDialog() == DialogResult.OK)
                    {
                        btn_rfid_poweroff.Enabled = false;

                        Stream stream = new FileStream(ofile.FileName, FileMode.Open);

                        Thread thread = new Thread(() => UpdateRFIDBootloader(stream));
                        thread.Start();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void UpdateRFIDImage(Stream stream)
        {
            int len;
            byte[] subpartBuffer = new byte[32];
            byte[] readBuffer = new byte[128];

            if (stream.Length != Constants.RFID_IMAGE_SIZE)
            {
                MessageBox.Show("Incorrect image file size.");
                stream.Close();
                return;
            }

            EnableCtrls(false);

            RFIDCommands.GetVersion(m_hid);

            // Reset to bootloader
            UpdateInfo("Reset to bootloader.");
            RFIDCommands.WriteOEM(m_hid, 0x3c4, 0x00000020);
            RFIDCommands.ReadOEM(m_hid, 0x3c4);
            RFIDCommands.Reset(m_hid);
            RFIDCommands.GetVersion(m_hid);

            // Upload data
            RFIDCommands.StartNVMemUpdate(m_hid);

            for (int i = 0; i < Constants.RFID_IMAGE_TOTAL_SUBPART; i++)
            {
                currentNVStatus = -1;
                len = stream.Read(subpartBuffer, 0, 32);
                if (len > 0)
                {
                    RFIDCommands.SendImageData(m_hid, subpartBuffer, i);
                }
                else
                {
                    MessageBox.Show("Not enough image data.");
                    return;
                }

                System.Threading.SpinWait.SpinUntil(() => (currentNVStatus != -1), 500);
                if (currentNVStatus == -1)
                {
                    UpdateInfo("Update receive timeout.");
                    RFIDCommands.UpdateComplete(m_hid);
                    break;
                }

                if (currentNVStatus != (int)NVStatus.NVMEMUPD_STAT_UPD_SUCCESS)
                {
                    UpdateInfo("Update fail: " + Enum.GetName(typeof(NVStatus), currentNVStatus));
                    break;
                }
                else
                {
                    UpdateProgress((i * 100) / Constants.RFID_IMAGE_TOTAL_SUBPART, i + 1, Constants.RFID_IMAGE_TOTAL_SUBPART);
                    if ((i + 1) == Constants.RFID_IMAGE_TOTAL_SUBPART)
                    {
                        UpdateInfo("Upload image success.");

                        RFIDCommands.UpdateComplete(m_hid);

                        // Reset to image
                        UpdateInfo("Reset to image. Please wait 6 seconds...");
                        Thread.Sleep(6000);
                        RFIDCommands.GetVersion(m_hid);
                        RFIDCommands.WriteOEM(m_hid, 0x3c4, 0x80000020);
                        RFIDCommands.ReadOEM(m_hid, 0x3c4);
                        RFIDCommands.Reset(m_hid);
                        RFIDCommands.GetVersion(m_hid);

                        UpdateInfo("Upgrade success.");
                    }
                }
            }
            stream.Close();
            EnableCtrls(true);
        }

        private void UpdateRFIDBootloader(Stream stream)
        {
            int len;
            byte[] subpartBuffer = new byte[32];
            byte[] readBuffer = new byte[128];

            if (stream.Length != Constants.RFID_BOOTLOADER_SIZE)
            {
                MessageBox.Show("Incorrect image file size.");
                stream.Close();
                return;
            }

            EnableCtrls(false);

            RFIDCommands.GetVersion(m_hid);

            // Reset to image
            UpdateInfo("Reset to image.");
            RFIDCommands.WriteOEM(m_hid, 0x3c4, 0x80000020);
            RFIDCommands.ReadOEM(m_hid, 0x3c4);
            RFIDCommands.Reset(m_hid);
            RFIDCommands.GetVersion(m_hid);

            // Upload data
            RFIDCommands.StartNVMemUpdate(m_hid);

            for (int i = 0; i < Constants.RFID_BOOTLOADER_TOTAL_SUBPART; i++)
            {
                currentNVStatus = -1;
                len = stream.Read(subpartBuffer, 0, 32);
                if (len > 0)
                {
                    RFIDCommands.SendBootloaderData(m_hid, subpartBuffer, i);
                }
                else
                {
                    MessageBox.Show("Not enough bootloader data.");
                    return;
                }

                System.Threading.SpinWait.SpinUntil(() => (currentNVStatus != -1), 500);
                if (currentNVStatus == -1)
                {
                    UpdateInfo("Update receive timeout.");
                    RFIDCommands.UpdateComplete(m_hid);
                    break;
                }

                if (currentNVStatus != (int)NVStatus.NVMEMUPD_STAT_UPD_SUCCESS)
                {
                    UpdateInfo("Update fail: " + Enum.GetName(typeof(NVStatus), currentNVStatus));
                    break;
                }
                else
                {
                    UpdateProgress((i * 100) / Constants.RFID_BOOTLOADER_TOTAL_SUBPART, i + 1, Constants.RFID_BOOTLOADER_TOTAL_SUBPART);
                    if ((i + 1) == Constants.RFID_BOOTLOADER_TOTAL_SUBPART)
                    {
                        UpdateInfo("Upload bootloader success.");

                        RFIDCommands.UpdateComplete(m_hid);

                        // Reset to bootloader
                        UpdateInfo("Reset to bootloader.Please wait 6 seconds...");
                        Thread.Sleep(6000);
                        RFIDCommands.GetVersion(m_hid);
                        RFIDCommands.WriteOEM(m_hid, 0x3c4, 0x00000020);
                        RFIDCommands.ReadOEM(m_hid, 0x3c4);
                        RFIDCommands.Reset(m_hid);
                        RFIDCommands.GetVersion(m_hid);

                        // Reset to image
                        UpdateInfo("Reset to image.");
                        RFIDCommands.WriteOEM(m_hid, 0x3c4, 0x80000020);
                        RFIDCommands.ReadOEM(m_hid, 0x3c4);
                        RFIDCommands.Reset(m_hid);
                        RFIDCommands.GetVersion(m_hid);

                        UpdateInfo("Upgrade success.");
                    }
                }
            }
            stream.Close();
            EnableCtrls(true);
        }
    }
}
