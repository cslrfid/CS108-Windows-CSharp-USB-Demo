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

namespace CS108_PC_Client
{
    public partial class RFIDForm : Form
    {
        IntPtr m_hid;
        byte[] m_read_buffer = new byte[65536]; // read ring buffer
        ushort m_read_buffer_size = 0;
        ushort m_read_buffer_head = 0;
        ushort m_read_buffer_tail = 0;

        byte[] m_rfid_buffer = new byte[65536]; // rfid ring buffer
        ushort m_rfid_buffer_size = 0;
        ushort m_rfid_buffer_head = 0;
        ushort m_rfid_buffer_tail = 0;

        bool m_stopRfidDecode = false;
        bool m_stopReceive = false;
        bool m_startInventory = false;

        int m_totaltag = 0;
        int m_tagRate = 0;

        readonly object locker = new object();

        public RFIDForm(IntPtr hid)
        {
            InitializeComponent();

            this.CenterToScreen();

            EnableCtrls(false);
            btn_rfid_clear.Enabled = false;

            m_hid = hid;

            Thread thread1 = new Thread(new ThreadStart(RecvThread));
            thread1.Start();

            m_stopRfidDecode = false;
            Thread thread2 = new Thread(new ThreadStart(DecodeRfidCommands));
            thread2.Start();
        }

        private void Main_FormClosing(object sender, FormClosingEventArgs e)
        {
            m_stopRfidDecode = true;
            m_stopReceive = true;
        }

        private void RecvThread()
        {
            while (!m_stopReceive)
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

            if (crc != 0 && !CRC.CheckCRC(m_read_buffer, m_read_buffer_tail, total_len, crc))
                UpdateInfo("Wrong CRC received.");

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
                                ClearRfidBuffer();
                                Console.WriteLine("Power off successed");
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
                                UpdateInfo("Send data successed.");
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

        private void DecodeRfidCommands()
        {
            ushort index = 0;
            int pkt_ver = 0;
            int flags = 0;
            int pkt_type = 0;
            int pkt_len = 0;
            int datalen = 0;
            int reserve = 0;
            Stopwatch stopwatch1 = new Stopwatch();
            Stopwatch stopwatch2 = new Stopwatch();

            stopwatch1.Start();
            stopwatch2.Start();
            while (!m_stopRfidDecode)
            {
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
                        if (pkt_ver == 0x04)
                        {
                            datalen = pkt_len;
                        }
                        else if (pkt_ver == 0x40)
                        {
                            switch (flags)
                            {
                                case 0x02:
                                    UpdateInfo("Reset command response received.");
                                    break;
                                case 0x03:
                                    UpdateInfo("Abort command response received.");
                                    m_startInventory = false;
                                    break;
                                default:
                                    UpdateInfo("other control command response received.");
                                    break;
                            }
                            continue;
                        }
                        else if ((pkt_ver == 0x70 || pkt_ver == 0x00) && flags == 0x00)
                        {
                            uint address = (uint)(pkt_type);
                            uint data = (uint)((reserve << 16) + pkt_len);
                            switch (address)
                            {
                                case 0x0000:
                                    uint major = (data >> 24);
                                    uint minor = (data >> 12) & 0x7FF;
                                    uint build = data & 0x7FF;
                                    UpdateVersion(major.ToString() + "." + minor.ToString() + "." + build.ToString());
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
                            ClearReadBuffer();
                            ClearRfidBuffer();
                            if (m_startInventory)
                                StopInventory();
                            continue;
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
                    StopInventory();
                    continue;
                }

                lock (this.locker)
                {
                    //finish reading
                    index = m_rfid_buffer_tail;
                    m_rfid_buffer_size -= (ushort)datalen;
                    m_rfid_buffer_tail += (ushort)datalen;
                    //Console.WriteLine("+Rfid Size:" + m_rfid_buffer_size);
                    if (pkt_type == 0x8000 || pkt_type == 0x0000)
                    {
                        UpdateInfo("Command Begin Packet.");
                        continue;
                    }
                    if (pkt_type == 0x8001 || pkt_type == 0x0001)
                    {
                        byte[] data = new byte[datalen];
                        byte[] PC = new byte[2];
                        byte[] EPC = new byte[128];
                        for (int cnt = 0; cnt < datalen; cnt++)
                        {
                            data[cnt] = m_rfid_buffer[index++];
                        }
                        uint status = (uint)(data[4]) + ((uint)(data[5]) << 8);

                        UpdateInfo(String.Format("Command End Packet. Status: {0:X4}", status));
                        continue;
                    }
                    if (pkt_type == 0x8007 || pkt_type == 0x0007)
                    {
                        UpdateInfo("Antenna Cycle End Packet.");
                        continue;
                    }
                    if (pkt_type == 0x8005 || pkt_type == 0x0005)
                    {
                        byte[] data = new byte[datalen];
                        byte[] PC = new byte[2];
                        byte[] EPC = new byte[128];
                        float rssi;
                        int epclen;
                        for (int cnt = 0; cnt < datalen; cnt++)
                        {
                            data[cnt] = m_rfid_buffer[index++];
                        }
                        if (pkt_ver == 0x04)
                        {
                            int cnt = 0;
                            while (cnt < datalen)
                            {
                                PC[0] = data[cnt++];
                                PC[1] = data[cnt++];
                                epclen = ((PC[0] >> 3) & 0x1f) * 2;
                                for (int i = 0; i < epclen; i++)
                                {
                                    EPC[i] = data[cnt++];
                                }
                                rssi = ConvertNBRSSI(data[cnt++]);
                                UpdateListView(ByteArrayToHexString(PC, 2), ByteArrayToHexString(EPC, epclen), rssi);
                                m_totaltag++;
                            }
                        }
                        else
                        {
                            epclen = datalen - 16;
                            for (int cnt = 0; cnt < 2; cnt++)
                            {
                                PC[cnt] = data[12 + cnt];
                            }
                            for (int cnt = 0; cnt < epclen; cnt++)
                            {
                                EPC[cnt] = data[14 + cnt];
                            }
                            rssi = ConvertNBRSSI(data[5]);
                            UpdateListView(ByteArrayToHexString(PC, 2), ByteArrayToHexString(EPC, epclen), rssi);
                            m_totaltag++;
                        }
                        continue;
                    }
                    if (pkt_type == 0x1008)
                    {
                        byte[] data = new byte[datalen];
                        for (int cnt = 0; cnt < datalen; cnt++)
                        {
                            data[cnt] = m_rfid_buffer[index++];
                        }
                        uint Querys = (uint)(data[0]) + ((uint)(data[1]) << 8) + ((uint)(data[2]) << 16) + ((uint)(data[3]) << 24);
                        uint RN16_RX = (uint)(data[4]) + ((uint)(data[5]) << 8) + ((uint)(data[6]) << 16) + ((uint)(data[7]) << 24);
                        uint RN16_TO = (uint)(data[8]) + ((uint)(data[9]) << 8) + ((uint)(data[10]) << 16) + ((uint)(data[11]) << 24);
                        uint EPC_TO = (uint)(data[12]) + ((uint)(data[13]) << 8) + ((uint)(data[14]) << 16) + ((uint)(data[15]) << 24);
                        uint EPC_RX = (uint)(data[16]) + ((uint)(data[17]) << 8) + ((uint)(data[18]) << 16) + ((uint)(data[19]) << 24);
                        uint CRC = (uint)(data[20]) + ((uint)(data[21]) << 8) + ((uint)(data[22]) << 16) + ((uint)(data[23]) << 24);

                        UpdateInfo(String.Format("Inventory Cycle End Diag Packet. EPC_RX: {0:X4}", EPC_RX));
                        continue;
                    }
                }
                Thread.Sleep(1); // save CPU usage
            }
            stopwatch1.Stop();
            stopwatch2.Stop();
        }

        private float ConvertNBRSSI(int rssi)
        {
            float Mantissa = rssi & 0x07;
            int Exponent = (rssi >> 3) & 0x1F;

            return (float)(20 * Math.Log10((1 << Exponent) * (1 + Mantissa / 8)));
        }

        private void btn_rfid_poweron_Click(object sender, EventArgs e)
        {
            byte[] command = RFIDCommands.PowerOn(true);

            if (!USBSocket.TransmitData(m_hid, command, command.Length))
            {
                MessageBox.Show("Device failed to transmit data.");
            }
        }

        private void btn_rfid_poweroff_Click(object sender, EventArgs e)
        {
            byte[] command = RFIDCommands.PowerOn(false);

            if (!USBSocket.TransmitData(m_hid, command, command.Length))
            {
                MessageBox.Show("Device failed to transmit data.");
            }
        }

        private void btn_rfid_clear_Click(object sender, EventArgs e)
        {
            lv_tag.Items.Clear();
            UpdateTotal(0);
        }

        private void btn_clear_info_Click(object sender, EventArgs e)
        {
            tb_info.Clear();
        }

        private void btn_inventory_Click(object sender, EventArgs e)
        {
            StartInventory();
            btn_rfid_clear.Enabled = false;
            btn_rfid_poweroff.Enabled = false;
            btn_set.Enabled = false;
            btn_version.Enabled = false;
        }

        private void btn_stop_inventory_Click(object sender, EventArgs e)
        {
            StopInventory();
            btn_rfid_clear.Enabled = true;
            btn_rfid_poweroff.Enabled = true;
            btn_set.Enabled = true;
            btn_version.Enabled = true;
        }

        private void StartInventory()
        {
            //Send Abort command
            RFIDCommands.SendData(m_hid, HexStringToByteArray("4003000000000000"), 8);
            Thread.Sleep(1000);

            //QUERY_CFG Command for continuous inventory
            RFIDCommands.SendData(m_hid, HexStringToByteArray("70010007ffff0000"), 8);
            Thread.Sleep(10);

            //INV_CFG Command for dynamic Q
            int delay = Convert.ToInt32(tb_delay.Text);
            RFIDCommands.SendData(m_hid, HexStringToByteArray(String.Format("700101090100{0:X2}{1:X2}", (delay & 0xF) << 4, (delay >> 4) & 0x3)), 8);
            //RFIDCommands.SendData(m_hid, HexStringToByteArray("7001010901000004"), 8);
            //RFIDCommands.SendData(m_hid, HexStringToByteArray("7001010901000000"), 8);
            Thread.Sleep(10);

            /*RFIDCommands.SendData(m_hid, HexStringToByteArray("7001010215020000"), 8);
            Thread.Sleep(10);*/

            //Start inventory
            RFIDCommands.SendData(m_hid, HexStringToByteArray("700100f00f000000"), 8);

            m_startInventory = true;
            m_totaltag = 0;
        }

        private void StopInventory()
        {
            //Send Abort command
            RFIDCommands.SendData(m_hid, HexStringToByteArray("4003000000000000"), 8);

            m_startInventory = false;
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
            btn_inventory.Enabled = enable;
            btn_stop_inventory.Enabled = enable;
            btn_set.Enabled = enable;
            btn_version.Enabled = enable;
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

        private delegate void UpdateVersionDeleg(String version);
        private void UpdateVersion(String version)
        {
            if (this.InvokeRequired)
            {
                Invoke(new UpdateInfoDeleg(UpdateVersion), new object[] { version });
                return;
            }
            tb_version.Text = version;
        }

        private delegate void UpdateListViewDeleg(String PC, String EPC, float rssi);
        private void UpdateListView(String PC, String EPC, float rssi)
        {
            if (this.InvokeRequired)
            {
                Invoke(new UpdateListViewDeleg(UpdateListView), new object[] { PC, EPC, rssi });
                return;
            }

            int foundIndex = -1;
            int count = lv_tag.Items.Count;
            if (count > 0)
            {
                var item = lv_tag.FindItemWithText(EPC, true, 0);
                if (item != null)
                {
                    foundIndex = lv_tag.Items.IndexOf(item);
                }
            }
            if (foundIndex >= 0)
            {
                ListViewItem listViewItem = lv_tag.Items[foundIndex];
                listViewItem.SubItems[1].Text = PC;
                listViewItem.SubItems[2].Text = EPC;
                listViewItem.SubItems[3].Text = rssi.ToString();
                listViewItem.SubItems[4].Text = (Convert.ToInt32(listViewItem.SubItems[4].Text)+1).ToString();
            }
            else
            {
                string[] row = { lv_tag.Items.Count.ToString(), PC, EPC, rssi.ToString(), "1" };
                ListViewItem listViewItem = new ListViewItem(row);
                lv_tag.Items.Add(listViewItem);
                UpdateTotal(count + 1);
            }
        }

        private delegate void UpdateRateDeleg(int rate);
        private void UpdateRate(int rate)
        {
            if (this.InvokeRequired)
            {
                Invoke(new UpdateRateDeleg(UpdateRate), new object[] { rate });
                return;
            }
            lb_rate.Text = rate.ToString();
        }

        private delegate void UpdateTotalDeleg(int total);
        private void UpdateTotal(int total)
        {
            if (this.InvokeRequired)
            {
                Invoke(new UpdateTotalDeleg(UpdateTotal), new object[] { total });
                return;
            }
            lb_total.Text = total.ToString();
        }

        private void btn_set_Click(object sender, EventArgs e)
        {
            // Set power
            int power = Convert.ToInt32(tb_power.Text);
            RFIDCommands.SendData(m_hid, HexStringToByteArray(String.Format("70010607{0:X2}{1:X2}0000", power & 0xFF, (power >> 8) & 0xFF)), 8);
            Thread.Sleep(1);

            // Set channel
            int channel = Convert.ToInt32(tb_channel.Text);
            if (channel > 0)
            {
                //disable all channels
                for (int i = 0; i <= 49; i++)
                {
                    RFIDCommands.SendData(m_hid, HexStringToByteArray(String.Format("7001010C{0:X2}000000", i)), 8);
                    Thread.Sleep(1);
                    RFIDCommands.SendData(m_hid, HexStringToByteArray("7001020C00000000"), 8);
                    Thread.Sleep(1);
                }
                //enable channel
                RFIDCommands.SendData(m_hid, HexStringToByteArray(String.Format("7001010C{0:X2}000000", channel - 1)), 8);
                Thread.Sleep(1);
                RFIDCommands.SendData(m_hid, HexStringToByteArray("7001020C01000000"), 8);
                Thread.Sleep(1);
            }

            //Set Link Profile
            int profile = Convert.ToInt32(tb_profile.Text);
            RFIDCommands.SendData(m_hid, HexStringToByteArray(String.Format("7001600b{0:X2}000000", profile)), 8);
            Thread.Sleep(1);

            //HST Command
            RFIDCommands.SendData(m_hid, HexStringToByteArray("700100f019000000"), 8);
            Thread.Sleep(10);

            //Set TX on time
            int tx_on_time = Convert.ToInt32(tb_tx_on.Text);
            RFIDCommands.SendData(m_hid, HexStringToByteArray(String.Format("70010603{0:X2}{1:X2}0000", tx_on_time & 0xFF, (tx_on_time >> 8) & 0xFF)), 8);
            Thread.Sleep(1);
        }

        private void btn_version_Click(object sender, EventArgs e)
        {
            RFIDCommands.SendData(m_hid, HexStringToByteArray("7000000000000000"), 8);
            Thread.Sleep(1);
        }

        private void readrate_Tick(object sender, EventArgs e)
        {
            m_tagRate = m_totaltag;
            m_totaltag = 0;
            UpdateRate(m_tagRate);
        }
    }
}
