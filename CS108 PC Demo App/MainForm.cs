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
using System.IO;
using System.Diagnostics;

namespace CS108_PC_Client
{
    public partial class MainForm : Form
    {
        public static IntPtr m_hid;
        bool m_Connected = false;
        static bool m_AutoReconnect = false;
        private UpdateProgressForm progressform = null;

        public static bool AutoReconnect
        {
            get { return m_AutoReconnect; }
            set
            {
                m_AutoReconnect = value;
            }
        }

        public MainForm()
        {
            InitializeComponent();

            this.CenterToScreen();

            UsbNotification.RegisterUsbDeviceNotification(this.Handle);
            EnableDeviceCtrls(false);
            UpdateDeviceList();

            progressform = new UpdateProgressForm();

            byte[] buffer = new byte[256];
            uint crc = 0;

            for (ushort i = 0; i < 8; i++)
            {
                buffer[i] = 0xFF;
            }

            //buffer = StringToByteArray("A7B346C2D39E8100041205803C0000003000E28011606000020D77249C95523000E28011606000020D76E128BC3B3000E28011606000020D77252814583000E28011606000020D7724D0B544");
            //buffer = StringToByteArray("A7B346C2D49E8100041205803C0000003000E28011606000020D7723BCA5393000E280116060A7B346C2D79E5DB33000E28011606000020D77242CE1423000E28011606000020D772528544C");
            //CA43
            //buffer = StringToByteArray("A7B346C2D89E8100041205803C0000003000E28011606000020D7722EC62393000E28011606000020D76E0EA2B383000E28011606000020D772502D13E3000E28011606000020D76E35A9C37");
            //A6F2
            /*buffer = StringToByteArray("A7B346C2D99E8100041205803C0000003000E28011606000020D77249A91533000E28011606000020D77242A64453000E28011606000020D77242AD53B3000E28011606000020D772528444C");

            for (ushort i = 0; i < 76; i++)
            {
                crc = CRC.UpdateCRC(crc, buffer[i]);
            }

            Console.WriteLine(crc);*/
        }

        public static byte[] StringToByteArray(string hex)
        {
            return Enumerable.Range(0, hex.Length)
                             .Where(x => x % 2 == 0)
                             .Select(x => Convert.ToByte(hex.Substring(x, 2), 16))
                             .ToArray();
        }

        protected override void WndProc(ref Message m)
        {
            base.WndProc(ref m);
            if (m.Msg == UsbNotification.WmDevicechange)
            {
                // Get the message event type
                int nEventType = m.WParam.ToInt32();

                // Check for devices being connected or disconnected
                if (nEventType == UsbNotification.DbtDevicearrival ||
                    nEventType == UsbNotification.DbtDeviceremovecomplete)
                {
                    UsbNotification.DevBroadcastHdr hdr = new UsbNotification.DevBroadcastHdr();

                    // Convert lparam to DevBroadcastHdr structure
                    Marshal.PtrToStructure(m.LParam, hdr);

                    if (hdr.dbch_devicetype == UsbNotification.DbtDevtypDeviceinterface)
                    {
                        UsbNotification.DevBroadcastDeviceinterface1 devIF = new UsbNotification.DevBroadcastDeviceinterface1();

                        // Convert lparam to DevBroadcastDeviceinterface1 structure
                        Marshal.PtrToStructure(m.LParam, devIF);

                        // Get the device path from the broadcast message
                        string devicePath = new string(devIF.dbcc_name);

                        // Remove null-terminated data from the string
                        int pos = devicePath.IndexOf((char)0);
                        if (pos != -1)
                        {
                            devicePath = devicePath.Substring(0, pos);
                        }

                        // An HID device was removed
                        if (nEventType == UsbNotification.DbtDeviceremovecomplete)
                        {
                            string deviceString = "";

					        // Check if our device was removed by device path
                            if (HID.GetString(m_hid, ref deviceString) == HID.HID_DEVICE_SUCCESS)
					        {
						        // Our device was removed
                                if ((devicePath.Equals(deviceString, StringComparison.OrdinalIgnoreCase)) == true)
                                {
                                    // The device handle is stale
                                    // Disconnect from it
                                    Disconnect();
                                    m_Connected = false;
                                }
					        }
                        }
                        // An HID device was connected
                        else if (nEventType == UsbNotification.DbtDevicearrival)
                        {
                        }

                        UpdateDeviceList();

                        if (m_AutoReconnect)
                            if (Connect()) m_Connected = true;
                    }
                }
            }
        }

        // Populate the device list combo box with connected device path strings
        // - Save previous device path string selection
        // - Fill the device list with connected device path strings
        // - Restore previous device selection
        private void UpdateDeviceList()
        {
            // Only update the combo list when the drop down list is closed
            if (!cb_device.DroppedDown)
            {
                int sel;
		        string path = "";
                string deviceString = ""; 

		        // Get previous combobox string selection
		        GetSelectedDevice(ref path);

                // Remove all strings from the combobox
                cb_device.Items.Clear();
                cb_device.ResetText();

                try
                {
                    // Iterate through each HID device with matching VID/PID
                    for (int i = 0; i < HID.GetNumHidDevices(); i++)
                    {
                        // Add path strings to the combobox
                        if (HID.GetHidString(i, ref deviceString) == HID.HID_DEVICE_SUCCESS)
                        {
                            cb_device.Items.Add(deviceString);
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }

                if (cb_device.Items.Count > 0)
                {
                    sel = cb_device.FindStringExact(path, 0);

                    // Restore previous combobox selection
                    if (sel < 0)
                    {
                        cb_device.SelectedIndex = 0;
                    }
                    // Select first combobox string
                    else
                    {
                        cb_device.SelectedIndex = sel;
                    }
                }
            }
        }

        // Connect to the device with the path string selected
        // in the device list
        // - Connect to the device specified in the device list
        // - Enable/disable device combobox
        private bool Connect()
        {
            bool        connected = false;
	        string		path = "";
	        int		    deviceNum = 0;

	        // Get selected device path string
            if (GetSelectedDevice(ref path))
	        {
		        // Find the selected device number
                if (FindDevice(path, ref deviceNum))
		        {
                    int status = HID.Open(ref m_hid, deviceNum);

                    // Attempt to open the device
			        if (status == HID.HID_DEVICE_SUCCESS)
			        {
				        connected = true;
                        GetSilabVersion();
                        GetBTVersion();
			        }
			        else
			        {
                        MessageBox.Show(String.Format("Failed to connect to {0}.", path));
			        }
		        }
	        }

	        // Connected
	        if (connected)
	        {
		        // Update Connect/Disconnect button caption
                btn_connect.Text = "Disconnect";

		        // Disable the device combobox
		        cb_device.Enabled = false;

		        // Enable the device controls
		        EnableDeviceCtrls(true);
	        }
	        // Disconnected
	        else
	        {
		        // Update Connect/Disconnect button caption
                btn_connect.Text = "Connect";

		        // Enable the device combobox
                cb_device.Enabled = true;

		        // Disable the device controls
		        EnableDeviceCtrls(false);
	        }

	        return connected;
        }

        // Disconnect from the currently connected device
        // - Disconnect from the current device
        // - Output any error messages
        // - Enable/disable device combobox
        private bool Disconnect()
        {
	        bool disconnected = false;

	        // Disconnect from the current device
	        int status = HID.Close(m_hid);
            m_hid = IntPtr.Zero;

	        // Output an error message if the close failed
	        if (status != HID.HID_DEVICE_SUCCESS)
	        {
                MessageBox.Show("Failed to disconnect.");
	        }
	        else
	        {
		        disconnected = true;
	        }

	        // Update Connect/Disconnect button caption
            btn_connect.Text = "Connect";

	        // Enable the device combobox
            cb_device.Enabled = true;

	        // Disable the device controls
	        EnableDeviceCtrls(false);

	        return disconnected;
        }

        // Get the combobox device selection
        // If a device is selected, return TRUE and return the path string
        // Otherwise, return FALSE
        private bool GetSelectedDevice(ref string path)
        {
	        bool selected = false;

	        int			sel;
	        string		selText;

	        // Get current selection index or CB_ERR(-1)
	        // if no device is selected
	        sel = cb_device.SelectedIndex;

	        if (sel >= 0)
	        {
		        // Get the selected device string
                selText = cb_device.SelectedItem.ToString();
		        selected = true;
		        path = selText;
	        }

	        return selected;
        }

        // Search for an HID device with a matching device path string
        // If the device was found return TRUE and return the device number
        // in deviceNumber
        // Otherwise return FALSE
        private bool FindDevice(string path, ref int deviceNum)
        {
	        bool found = false;
            string deviceString = "";
            
            try
            {
                // Iterate through each HID device with matching VID/PID
                for (int i = 0; i < HID.GetNumHidDevices(); i++)
                {
                    if (HID.GetHidString(i, ref deviceString) == HID.HID_DEVICE_SUCCESS)
                    {
                        if ((path.Equals(deviceString, StringComparison.OrdinalIgnoreCase)) == true)
                        {
                            found = true;
                            deviceNum = i;
                        }
                    }

                    if (found) break;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

	        return found;
        }

        private void GetSilabVersion()
        {
            byte[] buffer = new byte[128];
            int bytesRead = 0;

            byte[] command = RFIDCommands.PowerOn(false);

            if (!USBSocket.TransmitData(m_hid, command, command.Length))
            {
                MessageBox.Show("Device failed to transmit data.");
                return;
            }

            if (HID.IsOpened(m_hid))
            {
                if (USBSocket.ReceiveData(m_hid, ref buffer, buffer.Length, ref bytesRead, 1000))
                {
                }
            }
            else
            {
                MessageBox.Show("Device is not connected.");
            }

            command = SiliconLabCommands.GetVersion();

            if (!USBSocket.TransmitData(m_hid, command, command.Length))
            {
                MessageBox.Show("Device failed to transmit data.");
                return;
            }

            // Make sure that we are connected to a device
            if (HID.IsOpened(m_hid))
            {
                if (USBSocket.ReceiveData(m_hid, ref buffer, buffer.Length, ref bytesRead, 1000))
                {
                    if ((buffer[0] == Constants.PREFIX) && (bytesRead >= 13) &&
                        (buffer[8] == 0xB0) && (buffer[9] == 0x00))
                    {
                        tb_silab_version.Text = buffer[10].ToString() +"."+
                                                buffer[11].ToString() +"."+
                                                buffer[12].ToString();
                    }
                    else
                        MessageBox.Show("Cannot get silicon lab firmware version.");
                }
            }
            else
            {
                MessageBox.Show("Device is not connected.");
            }
        }

        private void GetBTVersion()
        {
            byte[] command = BluetoothCommands.GetVersion();

            if (!USBSocket.TransmitData(m_hid, command, command.Length))
            {
                MessageBox.Show("Device failed to transmit data.");
                return;
            }

            byte[] buffer = new byte[128];
            int bytesRead = 0;

            // Make sure that we are connected to a device
            if (HID.IsOpened(m_hid))
            {
                if (USBSocket.ReceiveData(m_hid, ref buffer, buffer.Length, ref bytesRead, 2000))
                {
                    if ((buffer[0] == Constants.PREFIX) && (bytesRead >= 13) &&
                        (buffer[8] == 0xC0) && (buffer[9] == 0x00))
                    {
                        uint crc = ((uint)buffer[6] << 8) | (uint)buffer[7];

                        if (crc != 0 && !CRC.CheckCRC(buffer, 0, 13, crc))
                        {
                            MessageBox.Show("Wrong CRC received.");
                            return;
                        }

                        tb_bt_version.Text = buffer[10].ToString() + "." +
                                                buffer[11].ToString() + "." +
                                                buffer[12].ToString();
                    }
                    else
                        MessageBox.Show("Cannot get bluetooth firmware version.");
                }
            }
            else
            {
                MessageBox.Show("Device is not connected.");
            }
        }

        private void GetBatteryLevel()
        {
            byte[] command = NotifyCommands.GetBattery();

            if (!USBSocket.TransmitData(m_hid, command, command.Length))
            {
                MessageBox.Show("Device failed to transmit data.");
                return;
            }

            byte[] buffer = new byte[128];
            int bytesRead = 0;

            // Make sure that we are connected to a device
            if (HID.IsOpened(m_hid))
            {
                if (USBSocket.ReceiveData(m_hid, ref buffer, buffer.Length, ref bytesRead, 500))
                {
                    if ((buffer[0] == Constants.PREFIX) && (bytesRead >= 12) &&
                        (buffer[8] == 0xA0) && (buffer[9] == 0x00))
                    {
                        tb_batt_lvl.Text = (buffer[10] << 8 | buffer[11]).ToString();
                    }
                    else
                        MessageBox.Show("Cannot get battery level.");
                }
            }
            else
            {
                MessageBox.Show("Device is not connected.");
            }
        }

        private void GetTriggerState()
        {
            byte[] buffer = new byte[128];
            int bytesRead = 0;

            // clear buffer
            USBSocket.ReceiveData(m_hid, ref buffer, buffer.Length, ref bytesRead, 10);
            
            byte[] command = NotifyCommands.GetTriggerState();

            if (!USBSocket.TransmitData(m_hid, command, command.Length))
            {
                MessageBox.Show("Device failed to transmit data.");
                return;
            }

            // Make sure that we are connected to a device
            if (HID.IsOpened(m_hid))
            {
                if (USBSocket.ReceiveData(m_hid, ref buffer, buffer.Length, ref bytesRead, 500))
                {
                    if ((buffer[0] == Constants.PREFIX) && (bytesRead >= 11) &&
                        (buffer[8] == 0xA0) && (buffer[9] == 0x01))
                    {
                        if (buffer[10] == 0)
                            tb_trigger_state.Text = "Released";
                        else
                            tb_trigger_state.Text = "Pushed";
                    }
                    else
                        MessageBox.Show("Cannot get trigger state.");
                }
            }
            else
            {
                MessageBox.Show("Device is not connected.");
            }
        }

        private void GetDeviceName()
        {
            byte[] command = BluetoothCommands.SendGetDeviceName();

            if (!USBSocket.TransmitData(m_hid, command, command.Length))
            {
                MessageBox.Show("Device failed to transmit data.");
                return;
            }

            byte[] buffer = new byte[128];
            int bytesRead = 0;

            // Make sure that we are connected to a device
            if (HID.IsOpened(m_hid))
            {
                if (USBSocket.ReceiveData(m_hid, ref buffer, buffer.Length, ref bytesRead, 500))
                {
                    if ((buffer[0] == Constants.PREFIX) && (bytesRead >= 31) &&
                        (buffer[8] == 0xC0) && (buffer[9] == 0x04))
                    {
                        var str = System.Text.Encoding.ASCII.GetString(buffer, 10, 21);
                        tb_devicename.Text = str;
                    }
                    else
                        MessageBox.Show("Cannot get device name.");
                }
            }
            else
            {
                MessageBox.Show("Device is not connected.");
            }
        }

        private void SetDeviceName()
        {
            byte[] name = Encoding.ASCII.GetBytes(tb_devicename.Text);
            byte[] command = BluetoothCommands.SendSetDeviceName(name);

            if (!USBSocket.TransmitData(m_hid, command, command.Length))
            {
                MessageBox.Show("Device failed to transmit data.");
                return;
            }

            byte[] buffer = new byte[128];
            int bytesRead = 0;

            // Make sure that we are connected to a device
            if (HID.IsOpened(m_hid))
            {
                if (USBSocket.ReceiveData(m_hid, ref buffer, buffer.Length, ref bytesRead, 500))
                {
                    if ((buffer[0] == Constants.PREFIX) && (bytesRead >= 11) &&
                        (buffer[8] == 0xC0) && (buffer[9] == 0x03))
                    {
                        if (buffer[10] == 0)
                            MessageBox.Show("Set device name success. Please re-boot device to take effective");
                        else
                            MessageBox.Show("Set device name fail.");
                    }
                    else
                        MessageBox.Show("Cannot set device name.");
                }
            }
            else
            {
                MessageBox.Show("Device is not connected.");
            }
        }

        // Enable/disable controls that should only be enabled
        // when connected to a device
        private void EnableDeviceCtrls(bool enable)
        {
            btn_rfid.Enabled = enable;
            btn_barcode.Enabled = enable;
            if (!enable)
            {
                tb_silab_version.Text = "";
                tb_bt_version.Text = "";
                tb_batt_lvl.Text = "";
                tb_trigger_state.Text = "";
                tb_devicename.Text = "";
            }
            btn_silab_image.Enabled = enable;
            btn_silab_bootloader.Enabled = enable;
            btn_bt_image.Enabled = enable;
            btn_bt_bootloader.Enabled = enable;
            btn_get_battery.Enabled = enable;
            btn_get_trigger.Enabled = enable;
            btn_get_devicename.Enabled = enable;
            btn_set_devicename.Enabled = enable;
            btn_rfid_upgrade.Enabled = enable;
        }

        private void Main_FormClosing(object sender, FormClosingEventArgs e)
        {
            // Close the device
            if (HID.IsOpened(m_hid))
            {
                HID.Close(m_hid);
            }

            UsbNotification.UnregisterUsbDeviceNotification();
        }

        private void btn_connect_Click(object sender, EventArgs e)
        {
            if (m_Connected)
            {
                if (Disconnect()) m_Connected = false;
            }
            else
            {
                if (Connect()) m_Connected = true;
            }
        }

        private void btn_rfid_Click(object sender, EventArgs e)
        {
            this.Hide();
            using (RFIDForm rfidForm = new RFIDForm(m_hid))
            {
                rfidForm.ShowDialog();
            }
            this.Show();
        }

        private void btn_barcode_Click(object sender, EventArgs e)
        {
            this.Hide();
            using (BarcodeForm barcodeForm = new BarcodeForm(m_hid))
            {
                barcodeForm.ShowDialog();
            }
            this.Show();
        }

        private void btn_silab_image_Click(object sender, EventArgs e)
        {
            try
            {
                using (OpenFileDialog ofile = new OpenFileDialog())
                {
                    ofile.Title = "Please choose Image update file";
                    ofile.Filter = "IMG files (*.img)|*.img|All files (*.*)|*.*";//"IMG Files\0*.img;\0All Files\0*.*\0";
                    if (ofile.ShowDialog() == DialogResult.OK)
                    {
                        Stream stream = new FileStream(ofile.FileName, FileMode.Open);

                        Thread thread = new Thread(() => UpdateSilabImage(stream));
                        thread.Start();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void UpdateSilabImage(Stream stream)
        {
            if (stream.Length != Constants.SILAB_IMAGE_SIZE)
            {
                MessageBox.Show("Incorrect image file size.");
                return;
            }

            int len, subpart = 1;
            byte[] subpartBuffer = new byte[114];
            byte[] readBuffer = new byte[128];
            byte[] command;

            while (true)
            {
                len = stream.Read(subpartBuffer, 0, 114);
                if (len > 0)
                {
                    command = SiliconLabCommands.SendImageData(subpartBuffer, subpart);
                }
                else
                {
                    MessageBox.Show("Not enough image data.");
                    return;
                }

                if (!USBSocket.TransmitData(m_hid, command, command.Length))
                {
                    MessageBox.Show("Device failed to transmit data.");
                    return;
                }

                int bytesRead = 0;

                // Make sure that we are connected to a device
                if (HID.IsOpened(m_hid))
                {
                    if (USBSocket.ReceiveData(m_hid, ref readBuffer, readBuffer.Length, ref bytesRead, 1000))
                    {
                        if ((readBuffer[0] == Constants.PREFIX) && (bytesRead >= 11) &&
                            (readBuffer[8] == 0xB0) && (readBuffer[9] == 0x01))
                        {
                            if (readBuffer[10] == 1)
                            {
                                ShowUpdateResult(false, 0);
                                break;
                            }
                            else if (readBuffer[10] == 2)
                            {
                                ShowUpdateResult(true, 5);
                                break;
                            }
                            else
                            {
                                ShowUpdatePercent((subpart * 100) / Constants.SILAB_IMAGE_TOTAL_SUBPART);
                                ++subpart;
                            }
                        }
                        else
                        {
                            MessageBox.Show("Cannot receive correct reply.");
                            break;
                        }
                    }
                    else
                    {
                        MessageBox.Show("Reply timeout.");
                        break;
                    }
                }
                else
                {
                    MessageBox.Show("Device is not connected.");
                    break;
                }
            }
            stream.Close();
        }

        private void btn_silab_bootloader_Click(object sender, EventArgs e)
        {
            try
            {
                using (OpenFileDialog ofile = new OpenFileDialog())
                {
                    ofile.Title = "Please choose Bootloader update file";
                    ofile.Filter = "BIN files (*.bin)|*.bin|All files (*.*)|*.*";//"BIN Files\0*.bin;\0All Files\0*.*\0";
                    if (ofile.ShowDialog() == DialogResult.OK)
                    {
                        Stream stream = new FileStream(ofile.FileName, FileMode.Open);

                        Thread thread = new Thread(() => UpdateSilabBootloader(stream));
                        thread.Start();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void UpdateSilabBootloader(Stream stream)
        {
            if (stream.Length != Constants.SILAB_BOOTLOADER_SIZE)
            {
                MessageBox.Show("Incorrect bootloader file size.");
                return;
            }

            int len, subpart = 1;
            byte[] subpartBuffer = new byte[114];
            byte[] readBuffer = new byte[128];
            byte[] command;

            while (true)
            {
                len = stream.Read(subpartBuffer, 0, 114);
                if (len > 0)
                {
                    command = SiliconLabCommands.SendBootloaderData(subpartBuffer, subpart);
                }
                else
                {
                    MessageBox.Show("Not enough bootloader data.");
                    return;
                }

                if (!USBSocket.TransmitData(m_hid, command, command.Length))
                {
                    MessageBox.Show("Device failed to transmit data.");
                    return;
                }

                int bytesRead = 0;

                // Make sure that we are connected to a device
                if (HID.IsOpened(m_hid))
                {
                    if (USBSocket.ReceiveData(m_hid, ref readBuffer, readBuffer.Length, ref bytesRead, 2000))
                    {
                        if ((readBuffer[0] == Constants.PREFIX) && (bytesRead >= 11) &&
                            (readBuffer[8] == 0xB0) && (readBuffer[9] == 0x02))
                        {
                            if (readBuffer[10] == 1)
                            {
                                ShowUpdateResult(false, 0);
                                break;
                            }
                            else if (readBuffer[10] == 2)
                            {
                                ShowUpdateResult(true, 2);
                                break;
                            }
                            else
                            {
                                ShowUpdatePercent((subpart * 100) / Constants.SILAB_BOOTLOADER_TOTAL_SUBPART);
                                ++subpart;
                            }
                        }
                        else
                        {
                            MessageBox.Show("Cannot receive correct reply.");
                            break;
                        }
                    }
                    else
                    {
                        MessageBox.Show("Reply timeout.");
                        break;
                    }
                }
                else
                {
                    MessageBox.Show("Device is not connected.");
                    break;
                }
            }
            stream.Close();
        }

        private void btn_bt_image_Click(object sender, EventArgs e)
        {
            try
            {
                using (OpenFileDialog ofile = new OpenFileDialog())
                {
                    ofile.Title = "Please choose Image update file";
                    ofile.Filter = "Image files (CS108_CC2541_APP_*.bin)|CS108_CC2541_APP_*.bin";
                    if (ofile.ShowDialog() == DialogResult.OK)
                    {
                        Stream stream = new FileStream(ofile.FileName, FileMode.Open);

                        Thread thread = new Thread(() => UpdateBTImage(stream));
                        thread.Start();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void UpdateBTImage(Stream stream)
        {
            if (stream.Length != Constants.BT_IMAGE_SIZE)
            {
                MessageBox.Show("Incorrect image file size.");
                return;
            }

            int len = 0, subpart = 1;
            byte[] subpartBuffer = new byte[64];
            byte[] readBuffer = new byte[128];
            byte[] command;
            int retry = 0;

            while (true)
            {
                if (retry == 0)
                {
                    len = stream.Read(subpartBuffer, 0, 64);
                }
                if (len > 0)
                {
                    command = BluetoothCommands.SendImageData(subpartBuffer, subpart);
                }
                else
                {
                    MessageBox.Show("Not enough image data.");
                    return;
                }

                if (!USBSocket.TransmitData(m_hid, command, command.Length))
                {
                    MessageBox.Show("Device failed to transmit data.");
                    return;
                }

                int bytesRead = 0;

                // Make sure that we are connected to a device
                if (HID.IsOpened(m_hid))
                {
                    if (USBSocket.ReceiveData(m_hid, ref readBuffer, readBuffer.Length, ref bytesRead, 5000))
                    {
                        if ((readBuffer[0] == Constants.PREFIX) && (bytesRead >= 11) &&
                            (readBuffer[8] == 0xC0) && (readBuffer[9] == 0x01))
                        {
                            if (readBuffer[10] == 1)
                            {
                                ShowUpdateResult(false, 0);
                                break;
                            }
                            else if (readBuffer[10] == 2)
                            {
                                ShowUpdateResult(true, 15);
                                break;
                            }
                            else
                            {
                                ShowUpdatePercent((subpart * 100) / Constants.BT_IMAGE_TOTAL_SUBPART);
                                ++subpart;
                                retry = 0;
                            }
                        }
                        else
                        {
                            //MessageBox.Show("Cannot receive correct reply.");
                            ++retry;
                            //break;
                        }
                    }
                    else
                    {
                        //MessageBox.Show("Reply timeout.");
                        ++retry;
                        //break;
                    }
                }
                else
                {
                    MessageBox.Show("Device is not connected.");
                    break;
                }
            }
            stream.Close();
        }

        private void btn_bt_bootloader_Click(object sender, EventArgs e)
        {
            try
            {
                using (OpenFileDialog ofile = new OpenFileDialog())
                {
                    ofile.Title = "Please choose Bootloader update file";
                    ofile.Filter = "Bootloader files (CS108_CC2541_BL_*.bin)|CS108_CC2541_BL_*.bin";
                    if (ofile.ShowDialog() == DialogResult.OK)
                    {
                        Stream stream = new FileStream(ofile.FileName, FileMode.Open);

                        Thread thread = new Thread(() => UpdateBTBootloader(stream));
                        thread.Start();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void UpdateBTBootloader(Stream stream)
        {
            if (stream.Length != Constants.BT_BOOTLOADER_SIZE)
            {
                MessageBox.Show("Incorrect bootloader file size.");
                return;
            }

            int len = 0, subpart = 1;
            byte[] subpartBuffer = new byte[64];
            byte[] readBuffer = new byte[128];
            byte[] command;
            int retry = 0;

            while (true)
            {
                if (retry == 0)
                {
                    len = stream.Read(subpartBuffer, 0, 64);
                }
                if (len > 0)
                {
                    command = BluetoothCommands.SendBootloaderData(subpartBuffer, subpart);
                }
                else
                {
                    MessageBox.Show("Not enough bootloader data.");
                    return;
                }

                if (!USBSocket.TransmitData(m_hid, command, command.Length))
                {
                    MessageBox.Show("Device failed to transmit data.");
                    return;
                }

                int bytesRead = 0;

                // Make sure that we are connected to a device
                if (HID.IsOpened(m_hid))
                {
                    if (USBSocket.ReceiveData(m_hid, ref readBuffer, readBuffer.Length, ref bytesRead, 2000))
                    {
                        if ((readBuffer[0] == Constants.PREFIX) && (bytesRead >= 11) &&
                            (readBuffer[8] == 0xC0) && (readBuffer[9] == 0x02))
                        {
                            if (readBuffer[10] == 1)
                            {
                                ShowUpdateResult(false, 0);
                                break;
                            }
                            else if (readBuffer[10] == 2)
                            {
                                ShowUpdateResult(true, 10);
                                break;
                            }
                            else
                            {
                                ShowUpdatePercent((subpart * 100) / Constants.BT_BOOTLOADER_TOTAL_SUBPART);
                                ++subpart;
                                retry = 0;
                            }
                        }
                        else
                        {
                            //MessageBox.Show("Cannot receive correct reply.");
                            ++retry;
                            //break;
                        }
                    }
                    else
                    {
                        //MessageBox.Show("Reply timeout.");
                        ++retry;
                        //break;
                    }
                }
                else
                {
                    MessageBox.Show("Device is not connected.");
                    break;
                }
            }
            stream.Close();
        }

        private delegate void ShowUpdateResultDeleg(bool success, int wait);
        private void ShowUpdateResult(bool success, int wait)
        {
            if (this.InvokeRequired)
            {
                Invoke(new ShowUpdateResultDeleg(ShowUpdateResult), new object[] { success, wait });
                return;
            }

            if (success)
            {
                progressform.UpdateProgressResult(String.Format("FW Upload Success. Wait {0} seconds to complete.", wait));
                progressform.Show();
                progressform.Refresh();
            }
            else
            {
                progressform.UpdateProgressResult("FW Update Fail");
                progressform.Show();
                progressform.Refresh();
            }
        }

        private delegate void ShowUpdatePercentDeleg(int percent);
        private void ShowUpdatePercent(int percent)
        {
            if (this.InvokeRequired)
            {
                Invoke(new ShowUpdatePercentDeleg(ShowUpdatePercent), new object[] { percent });
                return;
            }

            progressform.UpdateProgressPercent(percent);
            progressform.Show();
            progressform.Refresh();
        }

        private void btn_get_battery_Click(object sender, EventArgs e)
        {
            GetBatteryLevel();
        }

        private void btn_get_trigger_Click(object sender, EventArgs e)
        {
            GetTriggerState();
        }

        private void btn_get_devicename_Click(object sender, EventArgs e)
        {
            GetDeviceName();
        }

        private void btn_set_devicename_Click(object sender, EventArgs e)
        {
            SetDeviceName();
        }

        private void btn_rfid_upgrade_Click(object sender, EventArgs e)
        {
            this.Hide();
            using (RFIDUpgradeForm rfidUpgradeForm = new RFIDUpgradeForm(m_hid))
            {
                rfidUpgradeForm.ShowDialog();
            }
            this.Show();
        }
    }
}
