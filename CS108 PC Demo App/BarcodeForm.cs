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

namespace CS108_PC_Client
{
    public partial class BarcodeForm : Form
    {
        IntPtr m_hid;
        byte[] m_read_buffer = new byte[256]; // read ring buffer
        byte m_read_buffer_size = 0;
        byte m_read_buffer_head = 0;
        byte m_read_buffer_tail = 0;

        public BarcodeForm(IntPtr hid)
        {
            InitializeComponent();

            this.CenterToScreen();

            m_hid = hid;
        }

        // Fragment and transmit data by sending output reports over
        // the interrupt endpoint
        private bool TransmitData(byte[] buffer, int bufferSize)
        {
	        bool success = false;
            
            // Make sure that we are connected to a device
            if (HID.IsOpened(m_hid))
            {
                try
                {
                    success = HID.TransmitData(m_hid, buffer, bufferSize);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
            else
            {
                MessageBox.Show("Device is not connected.");
            }

	        return success;
        }

        // Receive several data input reports over the interrupt endpoint
        private bool ReceiveData(ref byte[] buffer, int bufferSize, ref int bytesRead)
        {
	        bool success = false;
            
            try
            {
                success = HID.ReceiveData(m_hid, ref buffer, bufferSize, ref bytesRead);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

	        return success;
        }

        // Start a timer to periodically check for input reports
        // over the interrupt endpoint
        private void StartReadTimer()
        {
            barcode_timer_read.Enabled = true;
            barcode_timer_read.Interval = Constants.TIMER_READ;
            barcode_timer_read.Start();
        }

        // Stop the read timer
        private void StopReadTimer()
        {
            barcode_timer_read.Stop();
            barcode_timer_read.Enabled = false;
        }

        private void Main_FormClosing(object sender, FormClosingEventArgs e)
        {
            // Disable the read timer before we disconnect
            // from the device
            StopReadTimer();
        }

        private void barcode_timer_read_Tick(object sender, EventArgs e)
        {
            byte[] buffer = new byte[128];
            int bytesRead = 0;

            // Make sure that we are connected to a device
            if (HID.IsOpened(m_hid))
            {
                if (ReceiveData(ref buffer, buffer.Length, ref bytesRead))
                {
                    if (bytesRead > 0)
                    {
                        for (int i = 0; i < bytesRead; i++)
                        {
                            m_read_buffer[m_read_buffer_head++] = buffer[i];
                        }
                        m_read_buffer_size += (byte)bytesRead;
                    }
                }

                if (m_read_buffer_size >= 8)
                {
                    if (m_read_buffer[m_read_buffer_tail] == Constants.PREFIX)
                    {
                        DecodeCommands();
                    }
                    else
                    {
                        tb_status.Text = "Wrong prefix is received";
                        ClearReadBuffer();
                    }
                }
            }
            else
            {
                StopReadTimer();
                MessageBox.Show("Device is not connected.");
                this.Close();
            }
        }

        private void ClearReadBuffer()
        {
            m_read_buffer_size = 0;
            m_read_buffer_head = 0;
            m_read_buffer_tail = 0;
        }

        private void DecodeCommands()
        {
            byte index = m_read_buffer_tail;
            uint prefix = m_read_buffer[index++];
            uint connection = m_read_buffer[index++];
            uint payload_len = m_read_buffer[index++];
            uint total_len = payload_len + 8;
            uint source = m_read_buffer[index++];
            uint direction = m_read_buffer[index++];
            uint crc = ((uint)m_read_buffer[index++] << 8) | (uint)m_read_buffer[index++];

            if (m_read_buffer_size < total_len) return; //not enough data

            index = (byte)(m_read_buffer_tail + (byte)8);
            uint event_code = ((uint)m_read_buffer[index++] << 8) | (uint)m_read_buffer[index++];
            if (source == Constants.TYPE_BARCODE)
            {
                switch (event_code)
                {
                    case 0x9000:
                        byte status = m_read_buffer[index++];
                        switch (status)
                        {
                            case 0x00:
                                tb_status.Text = "Power on successed.";
                                break;
                            case 0x01:
                                tb_status.Text = "Power on failed with RFID turned on.";
                                break;
                            case 0xFF:
                                tb_status.Text = "Power on failed with unknown reason.";
                                break;
                            default:
                                tb_status.Text = "Unknown status for barcode.";
                                break;
                        }
                        break;
                    case 0x9001:
                        status = m_read_buffer[index++];
                        switch (status)
                        {
                            case 0x00:
                                tb_status.Text = "Power off successed.";
                                break;
                            case 0xFF:
                                tb_status.Text = "Power off failed with unknown reason.";
                                break;
                            default:
                                tb_status.Text = "Unknown status for barcode.";
                                break;
                        }
                        break;
                    case 0x9002:
                        status = m_read_buffer[index++];
                        switch (status)
                        {
                            case 0x00:
                                tb_status.Text = "Trigger scan successed.";
                                break;
                            case 0x01:
                                tb_status.Text = "Trigger scan failed with not powered on.";
                                break;
                            case 0x02:
                                tb_status.Text = "Trigger scan failed with already scanning.";
                                break;
                            case 0xFF:
                                tb_status.Text = "Trigger scan failed with unknown reason.";
                                break;
                            default:
                                tb_status.Text = "Unknown status for barcode.";
                                break;
                        }
                        break;
                    case 0x9100:
                        byte[] buffer = new byte[payload_len - 2];

                        index = (byte)(m_read_buffer_tail + (byte)10);
                        for (int i = 0; i < payload_len - 2; i++)
                        {
                            buffer[i] = m_read_buffer[index++];
                        }

                        if (buffer[0] == 0x06)
                        {
                            tb_status.Text = "Command success.";
                        }
                        else if (buffer[0] == 0x02) // new prefix
                        {
                            string ascii = System.Text.Encoding.UTF8.GetString(buffer, 10, (int)(payload_len - 19));

                            tb_barcode_receive.AppendText(ascii);
                            tb_barcode_receive.AppendText("\r\n");
                        }
                        else
                        {
                            string ascii = System.Text.Encoding.UTF8.GetString(buffer);

                            tb_barcode_receive.AppendText(ascii);
                            tb_barcode_receive.AppendText("\r\n");
                        }
                        break;
                    case 0x9101:
                        tb_status.Text = "Good read.";
                        break;
                    default:
                        tb_status.Text = "Unknown event for barcode.";
                        break;
                }
            }
            else if (source == Constants.TYPE_NOTIFY)
            {
                switch (event_code)
                {
                    case 0xA000:
                        // battery percentage
                        uint level = ((uint)m_read_buffer[index++] << 8) | (uint)m_read_buffer[index++];
                        tb_status.Text = "Battery value : " + level.ToString();
                        break;
                    case 0xA100:
                        // battery fail
                        tb_status.Text = "Battery fail.";
                        break;
                    case 0xA101:
                        // error code
                        uint err_code = ((uint)m_read_buffer[index++] << 8) | (uint)m_read_buffer[index++];
                        tb_status.Text = "Error code : " + err_code.ToString();
                        break;
                    case 0xA102:
                        tb_status.Text = "Trigger is pushed.";
                        break;
                    case 0xA103:
                        tb_status.Text = "Trigger is released.";
                        break;
                    default:
                        tb_status.Text = "Unknown event for notification.";
                        break;
                }
            }
            else
            {
                tb_status.Text = "Wrong source for barcode.";
            }

            m_read_buffer_size -= (byte)total_len;
            m_read_buffer_tail += (byte)total_len;
        }

        private void btn_barcode_poweron_Click(object sender, EventArgs e)
        {
            byte[] command = BarcodeCommands.PowerOn(true);

            if (!TransmitData(command, command.Length))
            {
                MessageBox.Show("Device failed to transmit data.");
            }
            else
            {
                StartReadTimer();
            }
        }

        private void btn_barcode_poweroff_Click(object sender, EventArgs e)
        {
            byte[] command = BarcodeCommands.PowerOn(false);

            if (!TransmitData(command, command.Length))
            {
                MessageBox.Show("Device failed to transmit data.");
            }
        }

        private void btn_barcode_clear_Click(object sender, EventArgs e)
        {
            tb_barcode_receive.Clear();
        }

        private void btn_trigger_Click(object sender, EventArgs e)
        {
            byte[] command = BarcodeCommands.TriggerScan();

            if (!TransmitData(command, command.Length))
            {
                MessageBox.Show("Device failed to transmit data.");
            }
        }

        private void btn_cont_Click(object sender, EventArgs e)
        {
            byte[] data = new byte[] { 0x1b, 0x33 };
            //byte[] data = new byte[] { (byte)'n', (byte)'l', (byte)'s', (byte)'0', (byte)'0', (byte)'0', (byte)'6', (byte)'0', (byte)'1', (byte)'0', (byte)';' };
            //byte[] data = new byte[] { (byte)'n', (byte)'l', (byte)'s', (byte)'0', (byte)'3', (byte)'0', (byte)'2', (byte)'0', (byte)'2', (byte)'0', (byte)';' };
            //byte[] data = new byte[] { (byte)'n', (byte)'l', (byte)'s', (byte)'0', (byte)'0', (byte)'0', (byte)'6', (byte)'0', (byte)'0', (byte)'0', (byte)';' };
            byte[] command = BarcodeCommands.SendCommand(data);

            if (!TransmitData(command, command.Length))
            {
                MessageBox.Show("Device failed to transmit data.");
            }
        }

        private void btn_vibrator_on_Click(object sender, EventArgs e)
        {
            byte[] command = BarcodeCommands.VibratorOn(1, 1000);

            if (!TransmitData(command, command.Length))
            {
                MessageBox.Show("Device failed to transmit data.");
            }
        }

        private void btn_vibrator_off_Click(object sender, EventArgs e)
        {
            byte[] command = BarcodeCommands.VibratorOff();

            if (!TransmitData(command, command.Length))
            {
                MessageBox.Show("Device failed to transmit data.");
            }
        }
    }
}
