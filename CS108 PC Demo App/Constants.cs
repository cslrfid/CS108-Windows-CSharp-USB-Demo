using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CS108_PC_Client
{
    static class Constants
    {
        public const int TIMER_READ = 1; //ms
        
        public const int SILAB_IMAGE_SIZE = 16384;
        public const int SILAB_IMAGE_TOTAL_SUBPART = 144; //size/114+1
        public const int SILAB_BOOTLOADER_SIZE = 9216;
        public const int SILAB_BOOTLOADER_TOTAL_SUBPART = 81; //size/114+1

        public const int BT_IMAGE_SIZE = 124928;
        public const int BT_IMAGE_TOTAL_SUBPART = 1952; //size/64
        public const int BT_BOOTLOADER_SIZE = 4096;
        public const int BT_BOOTLOADER_TOTAL_SUBPART = 64; //size/64

        public const int RFID_IMAGE_SIZE = 203776;
        public const int RFID_IMAGE_TOTAL_SUBPART = 6368; //size/32
        public const int RFID_BOOTLOADER_SIZE = 24576;
        public const int RFID_BOOTLOADER_TOTAL_SUBPART = 768; //size/32

        public const byte PREFIX = 0xA7;
        public const byte CONNECTION_BT = 0xB3;
        public const byte CONNECTION_USB = 0xE6;
        public const byte TYPE_RFID = 0xC2;
        public const byte TYPE_BARCODE = 0x6A;
        public const byte TYPE_NOTIFY = 0xD9;
        public const byte TYPE_SILAB = 0xE8;
        public const byte TYPE_BT = 0x5F;
        public const byte LINK_DOWN = 0x37;
        public const byte LINK_UP = 0x9E;
        public const byte RESERVE = 0x82;
    }
}
