using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;
using System.Threading;

namespace CS108_PC_Client
{
    class USBSocket
    {
        // Fragment and transmit data by sending output reports over
        // the interrupt endpoint
        public static bool TransmitData(IntPtr hid, byte[] buffer, int bufferSize)
        {
            bool success = false;

            // Make sure that we are connected to a device
            if (HID.IsOpened(hid))
            {
                try
                {
                    success = HID.TransmitData(hid, buffer, bufferSize);
                }
                catch (Exception ex)
                {
                    throw new Exception(ex.Message);
                }
            }
            else
            {
                throw new Exception("Device is not connected.");
            }

            return success;
        }

        // Receive several data input reports over the interrupt endpoint
        public static bool ReceiveData(IntPtr hid, ref byte[] buffer, int bufferSize, ref int bytesRead, int timeout)
        {
            bool success = false;
            Stopwatch stopwatch = new Stopwatch();

            try
            {
                bytesRead = 0;
                stopwatch.Start();
                while (bytesRead == 0)
                {
                    success = HID.ReceiveData(hid, ref buffer, bufferSize, ref bytesRead);
                    if (!success || stopwatch.ElapsedMilliseconds > timeout)
                    {
                        success = false;
                        break;
                    }
                    Thread.Sleep(Constants.TIMER_READ);
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            finally
            {
                stopwatch.Stop();
            }

            return success;
        }
    }
}
