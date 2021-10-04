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
    public partial class RFIDReadForm : Form
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

        byte[] m_write_data;
        uint m_current_offset, m_total_count, m_current_count, m_word_written;
        uint m_word_read;

        readonly object locker = new object();

        public RFIDReadForm(IntPtr hid, string epc)
        {
            InitializeComponent();

            this.CenterToScreen();

            m_hid = hid;
            tb_epc.Text = epc;

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
                    if (pkt_type == 0x0006)
                    {
                        UpdateInfo("Tag-Access Packet.");
                        byte[] data = new byte[datalen];
                        for (int cnt = 0; cnt < datalen; cnt++)
                        {
                            data[cnt] = m_rfid_buffer[index++];
                        }
                        if ((flags & 0x01) == 0x01)
                        {
                            UpdateResult(String.Format("An error occurred: {0:X2}", data[12]));
                            EnableCtrls(true);
                        }
                        else if ((flags & 0x02) == 0x02)
                        {
                            UpdateResult(String.Format("Tag backscatter error code: {0:X2}", data[5]));
                            EnableCtrls(true);
                        }
                        else
                        {
                            if (data[4] == 0xC2) // read
                            {
                                for (int cnt = 0; cnt < m_current_count * 2; cnt++)
                                    UpdateResult(String.Format("{0:X2}", data[12 + cnt]));
                                m_word_read += m_current_count;
                                m_current_offset += m_current_count;
                                if (m_word_read < m_total_count)
                                {
                                    calculateCurrentReadCount(m_total_count - m_word_read);
                                    readBlock(m_current_offset, m_current_count);
                                }
                                else
                                    EnableCtrls(true);
                            }
                            if (data[4] == 0xC7) // blockwrite
                            {
                                UpdateResult(String.Format("Block write success. Offset: {0}; Count: {1}\n", m_current_offset, m_current_count));
                                m_word_written += m_current_count;
                                m_current_offset += m_current_count;
                                if (m_word_written < m_total_count)
                                {
                                    calculateCurrentWriteCount(m_total_count - m_word_written);
                                    writeBlock(m_current_offset, m_current_count, m_write_data, m_word_written*2);
                                }
                                else
                                    EnableCtrls(true);
                            }
                        }
                        continue;
                    }
                    if (pkt_type == 0x8005 || pkt_type == 0x0005)
                    {
                        UpdateInfo("Inventory Response Packet.");
                        byte[] data = new byte[datalen];
                        byte[] PC = new byte[2];
                        byte[] EPC = new byte[128];
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
                        }
                        continue;
                    }
                }
                Thread.Sleep(1); // save CPU usage
            }
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
            btn_read.Enabled = enable;
            btn_blockwrite.Enabled = enable;
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

        private delegate void UpdateResultDeleg(String result);
        private void UpdateResult(String result)
        {
            if (this.InvokeRequired)
            {
                Invoke(new UpdateResultDeleg(UpdateResult), new object[] { result });
                return;
            }
            tb_result.AppendText(result);
        }

        private void btn_read_Click(object sender, EventArgs e)
        {
            uint buf;
            byte[] strtmp, cutbuf = new byte[5];

            EnableCtrls(false);
            tb_result.Clear();

            // Step 1: Set Inventory Parameters
            // QUERY_CFG
            RFIDCommands.SendData(m_hid, HexStringToByteArray("7001000980010000"), 8);
            Thread.Sleep(10);

            // Step 2: Set Inventory Algorithm. (Assume only 1 tag in front of reader and Fixed Q = 0)
            // INV_CFG
            RFIDCommands.SendData(m_hid, HexStringToByteArray("7001010940400000"), 8);
            Thread.Sleep(10);
            
            // INV_SEL
            RFIDCommands.SendData(m_hid, HexStringToByteArray("7001020900000000"), 8);
            Thread.Sleep(10);

            // INV_ALG_PARM_0
            RFIDCommands.SendData(m_hid, HexStringToByteArray("7001030900000000"), 8);
            Thread.Sleep(10);

            // INV_ALG_PARM_2
            RFIDCommands.SendData(m_hid, HexStringToByteArray("7001050903000000"), 8);
            Thread.Sleep(10);

            // Step 3: Select the desired tag.
	        // TAGMSG_DESC_CFG
	        RFIDCommands.SendData(m_hid, HexStringToByteArray("7001010809000000"), 8);
            Thread.Sleep(10);

	        // TAGMSG_BANK
	        RFIDCommands.SendData(m_hid, HexStringToByteArray("7001020801000000"), 8);
            Thread.Sleep(10);

	        // TAGMSG_PTR
	        RFIDCommands.SendData(m_hid, HexStringToByteArray("7001030820000000"), 8);
            Thread.Sleep(10);

            // TAGMSG_LEN
	        buf = (uint)tb_epc.Text.Length * 4;
            RFIDCommands.SendData(m_hid, HexStringToByteArray(String.Format("70010408{0:X2}{1:X2}{2:X2}{3:X2}", (uint)(buf & 0xff), (uint)(buf >> 8 & 0xff), (uint)(buf >> 16 & 0xff), (uint)(buf >> 24 & 0xff))), 8);
            Thread.Sleep(10);

            // TAGMSG_0_3
            strtmp = HexStringToByteArray(tb_epc.Text.PadRight(64, '0'));
	        cutbuf[4] = 0;
	        for (int i = 0; i < 8; i++)
	        {
                Array.Copy(strtmp, i * 4, cutbuf, 0, 4);
                RFIDCommands.SendData(m_hid, HexStringToByteArray(String.Format("7001{0:X2}08{1:X2}{2:X2}{3:X2}{4:X2}", i + 5, cutbuf[0], cutbuf[1], cutbuf[2], cutbuf[3])), 8);
                Thread.Sleep(10);
	        }

            // Step 4: Start reading the tag data.
            buf = Convert.ToUInt32(tb_bank.Text);
            RFIDCommands.SendData(m_hid, HexStringToByteArray(String.Format("7001020A{0:X2}{1:X2}{2:X2}{3:X2}", (uint)(buf & 0xff), (uint)(buf >> 8 & 0xff), (uint)(buf >> 16 & 0xff), (uint)(buf >> 24 & 0xff))), 8);
            Thread.Sleep(10);

            strtmp = HexStringToByteArray(tb_acc_pwd.Text);
            RFIDCommands.SendData(m_hid, HexStringToByteArray(String.Format("7001060A{0:X2}{1:X2}{2:X2}{3:X2}", strtmp[0], strtmp[1], strtmp[2], strtmp[3])), 8);
            Thread.Sleep(10);

            m_current_offset = Convert.ToUInt32(tb_ptr.Text);
            m_total_count = Convert.ToUInt32(tb_count.Text);
            m_word_read = 0;

            calculateCurrentReadCount(m_total_count);
            readBlock(m_current_offset, m_current_count);
        }

        private void calculateCurrentReadCount(uint remaining_count)
        {
            if (remaining_count > 253)
                m_current_count = 253;
            else
                m_current_count = remaining_count;
        }

        private void readBlock(uint offset, uint count)
        {
            uint buf;

            buf = offset;
            RFIDCommands.SendData(m_hid, HexStringToByteArray(String.Format("7001030A{0:X2}{1:X2}{2:X2}{3:X2}", (uint)(buf & 0xff), (uint)(buf >> 8 & 0xff), (uint)(buf >> 16 & 0xff), (uint)(buf >> 24 & 0xff))), 8);
            Thread.Sleep(10);

            buf = count;
            RFIDCommands.SendData(m_hid, HexStringToByteArray(String.Format("7001040A{0:X2}{1:X2}{2:X2}{3:X2}", (uint)(buf & 0xff), (uint)(buf >> 8 & 0xff), (uint)(buf >> 16 & 0xff), (uint)(buf >> 24 & 0xff))), 8);
            Thread.Sleep(10);

            // HST Command
            RFIDCommands.SendData(m_hid, HexStringToByteArray("700100f010000000"), 8);
            Thread.Sleep(10);
        }

        private void tb_data_TextChanged(object sender, EventArgs e)
        {
            lb_bits.Text = (tb_data.Text.Length*4).ToString();
        }

        private void btn_blockwrite_Click(object sender, EventArgs e)
        {
            uint buf;
            byte[] strtmp, cutbuf = new byte[5];

            EnableCtrls(false);
            tb_result.Clear();

            // Step 1: Set Inventory Parameters
            // QUERY_CFG
            RFIDCommands.SendData(m_hid, HexStringToByteArray("7001000980010000"), 8);
            Thread.Sleep(10);

            // Step 2: Set Inventory Algorithm. (Assume only 1 tag in front of reader and Fixed Q = 0)
            // INV_CFG
            RFIDCommands.SendData(m_hid, HexStringToByteArray("7001010940400000"), 8);
            Thread.Sleep(10);

            // INV_SEL
            RFIDCommands.SendData(m_hid, HexStringToByteArray("7001020900000000"), 8);
            Thread.Sleep(10);

            // INV_ALG_PARM_0
            RFIDCommands.SendData(m_hid, HexStringToByteArray("7001030900000000"), 8);
            Thread.Sleep(10);

            // INV_ALG_PARM_2
            RFIDCommands.SendData(m_hid, HexStringToByteArray("7001050903000000"), 8);
            Thread.Sleep(10);

            // Step 3: Select the desired tag.
            // TAGMSG_DESC_CFG
            RFIDCommands.SendData(m_hid, HexStringToByteArray("7001010809000000"), 8);
            Thread.Sleep(10);

            // TAGMSG_BANK
            RFIDCommands.SendData(m_hid, HexStringToByteArray("7001020801000000"), 8);
            Thread.Sleep(10);

            // TAGMSG_PTR
            RFIDCommands.SendData(m_hid, HexStringToByteArray("7001030820000000"), 8);
            Thread.Sleep(10);

            // TAGMSG_LEN
            buf = (uint)tb_epc.Text.Length * 4;
            RFIDCommands.SendData(m_hid, HexStringToByteArray(String.Format("70010408{0:X2}{1:X2}{2:X2}{3:X2}", (uint)(buf & 0xff), (uint)(buf >> 8 & 0xff), (uint)(buf >> 16 & 0xff), (uint)(buf >> 24 & 0xff))), 8);
            Thread.Sleep(10);

            // TAGMSG_0_3
            strtmp = HexStringToByteArray(tb_epc.Text.PadRight(64, '0'));
            cutbuf[4] = 0;
            for (int i = 0; i < 8; i++)
            {
                Array.Copy(strtmp, i * 4, cutbuf, 0, 4);
                RFIDCommands.SendData(m_hid, HexStringToByteArray(String.Format("7001{0:X2}08{1:X2}{2:X2}{3:X2}{4:X2}", i + 5, cutbuf[0], cutbuf[1], cutbuf[2], cutbuf[3])), 8);
                Thread.Sleep(10);
            }

            strtmp = HexStringToByteArray(tb_acc_pwd.Text);
            RFIDCommands.SendData(m_hid, HexStringToByteArray(String.Format("7001060A{0:X2}{1:X2}{2:X2}{3:X2}", strtmp[0], strtmp[1], strtmp[2], strtmp[3])), 8);
            Thread.Sleep(10);

            // Step 4: Start writing the tag data.
            buf = Convert.ToUInt32(tb_bank.Text);
            RFIDCommands.SendData(m_hid, HexStringToByteArray(String.Format("7001020A{0:X2}{1:X2}{2:X2}{3:X2}", (uint)(buf & 0xff), (uint)(buf >> 8 & 0xff), (uint)(buf >> 16 & 0xff), (uint)(buf >> 24 & 0xff))), 8);
            Thread.Sleep(10);

            m_current_offset = Convert.ToUInt32(tb_ptr.Text);
            m_total_count = Convert.ToUInt32(tb_count.Text);
            m_write_data = HexStringToByteArray(tb_data.Text.PadRight(2052, '0'));
            m_word_written = 0;

            calculateCurrentWriteCount(m_total_count);
            writeBlock(m_current_offset, m_current_count, m_write_data, m_word_written*2);
        }

        private void calculateCurrentWriteCount(uint remaining_count)
        {
            if (m_current_offset < 256) // first 4096 bits
            {
                if (m_current_offset == 0 && remaining_count > 255)
                    m_current_count = 255;
                else if (m_current_offset + remaining_count > 256)
                    m_current_count = 256 - m_current_offset;
                else
                    m_current_count = remaining_count;
            }
            else // second 4096 bits
            {
                if (m_current_offset == 256 && remaining_count == 256)
                    m_current_count = 255;
                else
                    m_current_count = m_total_count - m_word_written;
            }
        }

        private void writeBlock(uint offset, uint count, byte[] data, uint data_offset)
        {
            uint buf;
            byte[] cutbuf = new byte[5];

            buf = offset;
            RFIDCommands.SendData(m_hid, HexStringToByteArray(String.Format("7001030A{0:X2}{1:X2}{2:X2}{3:X2}", (uint)(buf & 0xff), (uint)(buf >> 8 & 0xff), (uint)(buf >> 16 & 0xff), (uint)(buf >> 24 & 0xff))), 8);
            Thread.Sleep(10);

            buf = count;
            RFIDCommands.SendData(m_hid, HexStringToByteArray(String.Format("7001040A{0:X2}{1:X2}{2:X2}{3:X2}", (uint)(buf & 0xff), (uint)(buf >> 8 & 0xff), (uint)(buf >> 16 & 0xff), (uint)(buf >> 24 & 0xff))), 8);
            Thread.Sleep(10);

            // TAGWRDAT_X
            int sel = 0;
            cutbuf[4] = 0;
            for (int i = 0; i < count; i += 2)
            {
                if ((i % 32) == 0)
                {
                    // TAGWRDAT_SEL
                    RFIDCommands.SendData(m_hid, HexStringToByteArray(String.Format("7001080A{0:X2}000000", sel)), 8);
                    Thread.Sleep(10);
                    ++sel;
                }
                Array.Copy(data, data_offset + (i * 2), cutbuf, 0, 4);
                RFIDCommands.SendData(m_hid, HexStringToByteArray(String.Format("7001{0:X2}0A{1:X2}{2:X2}{3:X2}{4:X2}", 9 + ((i >> 1) % 16), cutbuf[3], cutbuf[2], cutbuf[1], cutbuf[0])), 8);
                Thread.Sleep(10);
            }

            // HST Command
            RFIDCommands.SendData(m_hid, HexStringToByteArray("700100f01F000000"), 8);
            Thread.Sleep(10);
        }
    }
}
