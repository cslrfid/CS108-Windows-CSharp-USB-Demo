using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CS108_PC_Client
{
    class NotifyCommands
    {
        public static byte[] GetBattery()
        {
            byte[] buffer = new byte[10];

            //header
            buffer[0] = Constants.PREFIX;
            buffer[1] = Constants.CONNECTION_USB;
            buffer[2] = 2; //payload length
            buffer[3] = Constants.TYPE_NOTIFY;
            buffer[4] = Constants.RESERVE;
            buffer[5] = Constants.LINK_DOWN;
            buffer[6] = 0;
            buffer[7] = 0;

            //payload
            buffer[8] = 0xA0;
            buffer[9] = 0x00;

            return buffer;
        }

        public static byte[] GetTriggerState()
        {
            byte[] buffer = new byte[10];

            //header
            buffer[0] = Constants.PREFIX;
            buffer[1] = Constants.CONNECTION_USB;
            buffer[2] = 2; //payload length
            buffer[3] = Constants.TYPE_NOTIFY;
            buffer[4] = Constants.RESERVE;
            buffer[5] = Constants.LINK_DOWN;
            buffer[6] = 0;
            buffer[7] = 0;

            //payload
            buffer[8] = 0xA0;
            buffer[9] = 0x01;

            return buffer;
        }

        public static byte[] SetBatteryReport(bool on)
        {
            byte[] buffer = new byte[10];

            //header
            buffer[0] = Constants.PREFIX;
            buffer[1] = Constants.CONNECTION_USB;
            buffer[2] = 2; //payload length
            buffer[3] = Constants.TYPE_NOTIFY;
            buffer[4] = Constants.RESERVE;
            buffer[5] = Constants.LINK_DOWN;
            buffer[6] = 0;
            buffer[7] = 0;

            //payload
            if (on)
            {
                buffer[8] = 0xA0;
                buffer[9] = 0x02;
            }
            else
            {
                buffer[8] = 0xA0;
                buffer[9] = 0x03;
            }

            return buffer;
        }
    }
}
