using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;

namespace CS108_PC_Client
{
    class HID
    {
        // USB Parameters
        public const int VID = 0x10C4;
        public const int PID = 0x8468;
        public const int HID_READ_TIMEOUT = 0;
        public const int HID_WRITE_TIMEOUT = 1000;
        // HID Report IDs
        public const int ID_IN_CONTROL = 0xFE;
        public const int ID_OUT_CONTROL = 0xFD;
        public const int ID_IN_DATA = 0x01;
        public const int ID_OUT_DATA = 0x02;
        // HID Report Sizes
        public const int SIZE_IN_CONTROL = 5;
        public const int SIZE_OUT_CONTROL = 5;
        public const int SIZE_IN_DATA = 61;
        public const int SIZE_OUT_DATA = 61;
        public const int SIZE_MAX_WRITE = 59;
        public const int SIZE_MAX_READ = 59;
        // Return Codes
        public const int HID_DEVICE_SUCCESS = 0x00;
        public const int HID_DEVICE_NOT_FOUND = 0x01;
        public const int HID_DEVICE_NOT_OPENED = 0x02;
        public const int HID_DEVICE_ALREADY_OPENED = 0x03;
        public const int HID_DEVICE_TRANSFER_TIMEOUT = 0x04;
        public const int HID_DEVICE_TRANSFER_FAILED = 0x05;
        public const int HID_DEVICE_CANNOT_GET_HID_INFO = 0x06;
        public const int HID_DEVICE_HANDLE_ERROR = 0x07;
        public const int HID_DEVICE_INVALID_BUFFER_SIZE = 0x08;
        public const int HID_DEVICE_SYSTEM_CODE = 0x09;
        public const int HID_DEVICE_UNSUPPORTED_FUNCTION = 0x0A;
        public const int HID_DEVICE_UNKNOWN_ERROR = 0xFF;
        // String Types
        public const int HID_VID_STRING = 0x01;
        public const int HID_PID_STRING = 0x02;
        public const int HID_PATH_STRING = 0x03;
        public const int HID_SERIAL_STRING = 0x04;
        public const int HID_MANUFACTURER_STRING = 0x05;
        public const int HID_PRODUCT_STRING = 0x06;
        public const int MAX_PATH_LENGTH = 260;

        private const int MAX_REPORT_REQUEST_XP = 512;

        [DllImport("SLABHIDDevice.dll")]
        private static extern int HidDevice_GetNumHidDevices(int vid, int pid);

        [DllImport("SLABHIDDevice.dll")]
        private static extern byte HidDevice_GetHidString(int deviceIndex, int vid, int pid, int hidStringType, IntPtr deviceString, int deviceStringLength);

        [DllImport("SLABHIDDevice.dll")]
        private static extern byte HidDevice_GetString(IntPtr device, int hidStringType, IntPtr deviceString, int deviceStringLength);

        [DllImport("SLABHIDDevice.dll")]
        private static extern byte HidDevice_Open(ref IntPtr device, int deviceIndex, int vid, int pid, int numInputBuffers);

        [DllImport("SLABHIDDevice.dll")]
        private static extern byte HidDevice_Close(IntPtr device);

        [DllImport("SLABHIDDevice.dll")]
        private static extern void HidDevice_SetTimeouts(IntPtr device, int getReportTimeout, int setReportTimeout);

        [DllImport("SLABHIDDevice.dll")]
        private static extern short HidDevice_GetOutputReportBufferLength(IntPtr device);

        [DllImport("SLABHIDDevice.dll")]
        private static extern bool HidDevice_IsOpened(IntPtr device);

        [DllImport("SLABHIDDevice.dll")]
        private static extern byte HidDevice_SetOutputReport_Interrupt(IntPtr device, IntPtr buffer, int bufferSize);

        [DllImport("SLABHIDDevice.dll")]
        private static extern byte HidDevice_GetInputReport_Interrupt(IntPtr device, IntPtr buffer, int bufferSize, int numReports, ref int bytesReturned);

        [DllImport("SLABHIDDevice.dll")]
        private static extern int HidDevice_GetMaxReportRequest(IntPtr device);

        public static int GetNumHidDevices()
        {
            return HidDevice_GetNumHidDevices(VID, PID);
        }

        public static int GetMaxReportRequest(IntPtr device)
        {
            return HidDevice_GetMaxReportRequest(device);
        }

        public static int GetHidString(int deviceIndex, ref string deviceString)
        {
            int status = 0;
            byte[] String = new byte[MAX_PATH_LENGTH];
            
            IntPtr pnt = Marshal.AllocHGlobal(MAX_PATH_LENGTH);
            status = HID.HidDevice_GetHidString(deviceIndex, VID, PID, HID_PATH_STRING, pnt, MAX_PATH_LENGTH);
            if (status == HID_DEVICE_SUCCESS)
            {
                Marshal.Copy(pnt, String, 0, MAX_PATH_LENGTH);
                deviceString = System.Text.Encoding.ASCII.GetString(String);
                deviceString = deviceString.Split('\0')[0];
            }
            Marshal.FreeHGlobal(pnt);

            return status;
        }

        public static int GetString(IntPtr device, ref string deviceString)
        {
            int status = 0;
            byte[] String = new byte[MAX_PATH_LENGTH];

            IntPtr pnt = Marshal.AllocHGlobal(MAX_PATH_LENGTH);
            status = HID.HidDevice_GetString(device, HID_PATH_STRING, pnt, MAX_PATH_LENGTH);
            if (status == HID_DEVICE_SUCCESS)
            {
                Marshal.Copy(pnt, String, 0, MAX_PATH_LENGTH);
                deviceString = System.Text.Encoding.ASCII.GetString(String);
                deviceString = deviceString.Split('\0')[0];
            }
            Marshal.FreeHGlobal(pnt);

            return status;
        }

        public static void SetTimeouts(IntPtr device, int getReportTimeout, int setReportTimeout)
        {
            HidDevice_SetTimeouts(device, getReportTimeout, setReportTimeout);
        }

        public static int Open(ref IntPtr device, int deviceIndex)
        {
            int status = HidDevice_Open(ref device, deviceIndex, VID, PID, MAX_REPORT_REQUEST_XP);

            if (status == HID_DEVICE_SUCCESS)
            {
                // Set read/write timeouts
                // Read timeouts should be set very low since we are periodically
                // reading for input reports over the interrupt endpoint in the
                // selector timer
                HidDevice_SetTimeouts(device, HID_READ_TIMEOUT, HID_WRITE_TIMEOUT);
            }

            return status;
        }

        public static int Close(IntPtr device)
        {
            return HidDevice_Close(device);
        }

        public static int GetOutputReportBufferLength(IntPtr device)
        {
            return HidDevice_GetOutputReportBufferLength(device);
        }

        public static bool IsOpened(IntPtr device)
        {
            return HidDevice_IsOpened(device);
        }

        public static bool TransmitData(IntPtr device, byte[] buffer, int bufferSize)
        {
	        bool	success		= false;
	        int 	reportSize	= HidDevice_GetOutputReportBufferLength(device);
	        byte[]	report		= new byte[reportSize];

	        // Make sure that the device report size is adequate
	        if (reportSize >= SIZE_OUT_DATA)
	        {
		        int bytesWritten = 0;
		        int bytesToWrite = bufferSize;
                IntPtr pnt = Marshal.AllocHGlobal(reportSize);

		        // Fragment the buffer into several writes of up to SIZE_MAX_WRITE(61)
		        // bytes
		        while (bytesWritten < bytesToWrite)
		        {
			        int transferSize = Math.Min(bytesToWrite - bytesWritten, SIZE_MAX_WRITE);

			        report[0] = ID_OUT_DATA;
			        report[1] = (byte)transferSize;
                    Buffer.BlockCopy(buffer, bytesWritten, report, 2, transferSize);
                    Marshal.Copy(report, 0, pnt, reportSize);

			        // Send an output report over the interrupt endpoint
                    if (HidDevice_SetOutputReport_Interrupt(device, pnt, reportSize) != HID_DEVICE_SUCCESS)
			        {
				        // Stop transmitting if there was an error
				        break;
			        }

			        bytesWritten += transferSize;
		        }

                Marshal.FreeHGlobal(pnt);

		        // Write completed successfully
		        if (bytesWritten == bytesToWrite)
		        {
			        success = true;
		        }
	        }

	        return success;
        }

        public static bool ReceiveData(IntPtr device, ref byte[] buffer, int bufferSize, ref int bytesRead)
        {
            bool success = false;
            int reportSize = HidDevice_GetOutputReportBufferLength(device);

            // Make sure that the device report size is adequate
            if (reportSize >= SIZE_IN_DATA)
            {
                // Make sure that the buffer is at least big enough to hold the maximum
                // number of input data bytes from a single report
                if (bufferSize >= SIZE_MAX_READ)
                {
                    // Determine the worst-case number of reports that will fit in the
                    // user buffer
                    int numReports = bufferSize / SIZE_MAX_READ;
                    int reportBufferSize = numReports * reportSize;
                    byte[] reportBuffer = new byte[reportBufferSize];
                    int reportBufferRead = 0;
                    byte status;

                    IntPtr pnt = Marshal.AllocHGlobal(reportBufferSize);

                    // Receive as many input reports as possible
                    // (resulting data bytes must be able to fit in the user buffer)
                    status = HidDevice_GetInputReport_Interrupt(device, pnt, reportBufferSize, numReports, ref reportBufferRead);

                    // Success indicates that numReports were read
                    // Transfer timeout may have returned less data
                    if (status == HID_DEVICE_SUCCESS ||
                        status == HID_DEVICE_TRANSFER_TIMEOUT)
                    {
                        bytesRead = 0;

                        Marshal.Copy(pnt, reportBuffer, 0, reportBufferSize);

                        // Iterate through each report in the report buffer
                        for (int i = 0; i < reportBufferRead; i += reportSize)
                        {
                            // Determine the number of valid data bytes in the current report
                            byte bytesInReport = reportBuffer[i + 1];

                            // Copy the data bytes into the user buffer
                            Array.Copy(reportBuffer, i + 2, buffer, bytesRead, bytesInReport);

                            // Keep track of how many valid bytes are being returned in the user buffer
                            bytesRead += bytesInReport;
                        }

                        success = true;
                    }

                    Marshal.FreeHGlobal(pnt);
                }
            }

            return success;
        }
    }
}
