using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CS108_PC_Client
{
    class BarcodeCommands
    {
        public static byte[] PowerOn(bool on)
        {
            byte[] buffer = new byte[10];

            //header
            buffer[0] = Constants.PREFIX;
            buffer[1] = Constants.CONNECTION_USB;
            buffer[2] = 2; //payload length
            buffer[3] = Constants.TYPE_BARCODE;
            buffer[4] = Constants.RESERVE;
            buffer[5] = Constants.LINK_DOWN;
            buffer[6] = 0;
            buffer[7] = 0;

            //payload
            buffer[8] = 0x90;
            if (on)
                buffer[9] = 0x00;
            else
                buffer[9] = 0x01;

            return buffer;
        }

        public static byte[] TriggerScan()
        {
            byte[] buffer = new byte[10];

            //header
            buffer[0] = Constants.PREFIX;
            buffer[1] = Constants.CONNECTION_USB;
            buffer[2] = 2; //payload length
            buffer[3] = Constants.TYPE_BARCODE;
            buffer[4] = Constants.RESERVE;
            buffer[5] = Constants.LINK_DOWN;
            buffer[6] = 0;
            buffer[7] = 0;

            //payload
            buffer[8] = 0x90;
            buffer[9] = 0x02;

            return buffer;
        }

        public static byte[] SendCommand(byte[] command)
        {
            byte[] buffer = new byte[command.Length + 10];

            //header
            buffer[0] = Constants.PREFIX;
            buffer[1] = Constants.CONNECTION_USB;
            buffer[2] = (byte)(command.Length + 2); //payload length
            buffer[3] = Constants.TYPE_BARCODE;
            buffer[4] = Constants.RESERVE;
            buffer[5] = Constants.LINK_DOWN;
            buffer[6] = 0;
            buffer[7] = 0;

            //payload
            buffer[8] = 0x90;
            buffer[9] = 0x03;
            for (int i = 0; i < command.Length; i++)
            {
                buffer[10 + i] = command[i];
            }

            return buffer;
        }
    }
}
