using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CS108_PC_Client
{
    class BluetoothCommands
    {
        public static byte[] GetVersion()
        {
            byte[] buffer = new byte[10];

            //header
            buffer[0] = Constants.PREFIX;
            buffer[1] = Constants.CONNECTION_USB;
            buffer[2] = 2; //payload length
            buffer[3] = Constants.TYPE_BT;
            buffer[4] = Constants.RESERVE;
            buffer[5] = Constants.LINK_DOWN;
            buffer[6] = 0;
            buffer[7] = 0;

            //payload
            buffer[8] = 0xC0;
            buffer[9] = 0x00;

            return buffer;
        }

        public static byte[] SendImageData(byte[] subpart_data, int subpart)
        {
            byte[] buffer = new byte[78];
            int total_subpart = Constants.BT_IMAGE_TOTAL_SUBPART;

            //header
            buffer[0] = Constants.PREFIX;
            buffer[1] = Constants.CONNECTION_USB;
            buffer[2] = 70; //payload length
            buffer[3] = Constants.TYPE_BT;
            buffer[4] = Constants.RESERVE;
            buffer[5] = Constants.LINK_DOWN;
            buffer[6] = 0;
            buffer[7] = 0;

            //payload
            buffer[8] = 0xC0;
            buffer[9] = 0x01;
            buffer[10] = (byte)(total_subpart >> 8);
            buffer[11] = (byte)total_subpart;
            buffer[12] = (byte)(subpart >> 8);
            buffer[13] = (byte)subpart;

            for (int i = 0; i < 64; i++)
            {
                buffer[i + 14] = subpart_data[i];
            }

            return buffer;
        }

        public static byte[] SendBootloaderData(byte[] subpart_data, int subpart)
        {
            byte[] buffer = new byte[78];
            int total_subpart = Constants.BT_BOOTLOADER_TOTAL_SUBPART;

            //header
            buffer[0] = Constants.PREFIX;
            buffer[1] = Constants.CONNECTION_USB;
            buffer[2] = 70; //payload length
            buffer[3] = Constants.TYPE_BT;
            buffer[4] = Constants.RESERVE;
            buffer[5] = Constants.LINK_DOWN;
            buffer[6] = 0;
            buffer[7] = 0;

            //payload
            buffer[8] = 0xC0;
            buffer[9] = 0x02;
            buffer[10] = (byte)(total_subpart >> 8);
            buffer[11] = (byte)total_subpart;
            buffer[12] = (byte)(subpart >> 8);
            buffer[13] = (byte)subpart;

            for (int i = 0; i < 64; i++)
            {
                buffer[i + 14] = subpart_data[i];
            }

            return buffer;
        }

        public static byte[] SendSetDeviceName(byte[] DeviceName)
        {
            byte[] buffer = new byte[31];

            //header
            buffer[0] = Constants.PREFIX;
            buffer[1] = Constants.CONNECTION_USB;
            buffer[2] = 23; //payload length
            buffer[3] = Constants.TYPE_BT;
            buffer[4] = Constants.RESERVE;
            buffer[5] = Constants.LINK_DOWN;
            buffer[6] = 0;
            buffer[7] = 0;

            //payload
            buffer[8] = 0xC0;
            buffer[9] = 0x03;
            for (int i = 0; i < DeviceName.Length; i++)
            {
                buffer[i + 10] = DeviceName[i];
            }

            return buffer;
        }

        public static byte[] SendGetDeviceName()
        {
            byte[] buffer = new byte[10];

            //header
            buffer[0] = Constants.PREFIX;
            buffer[1] = Constants.CONNECTION_USB;
            buffer[2] = 2; //payload length
            buffer[3] = Constants.TYPE_BT;
            buffer[4] = Constants.RESERVE;
            buffer[5] = Constants.LINK_DOWN;
            buffer[6] = 0;
            buffer[7] = 0;

            //payload
            buffer[8] = 0xC0;
            buffer[9] = 0x04;

            return buffer;
        }

        public static byte[] SendTestMode(byte mode)
        {
            byte[] buffer = new byte[11];

            //header
            buffer[0] = Constants.PREFIX;
            buffer[1] = Constants.CONNECTION_USB;
            buffer[2] = 3; //payload length
            buffer[3] = Constants.TYPE_BT;
            buffer[4] = Constants.RESERVE;
            buffer[5] = Constants.LINK_DOWN;
            buffer[6] = 0;
            buffer[7] = 0;

            //payload
            buffer[8] = 0xC0;
            buffer[9] = 0xFF;
            buffer[10] = mode;

            return buffer;
        }
    }
}
