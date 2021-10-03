using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CS108_PC_Client
{
    class S_EPC
    {
        /**
        * EPC
        */
        public byte[] epc;

        /**
         * Custom constructor
         * @param epc epc in string format, must be smaller than or equal to 62 hex numbers
         */
        public S_EPC(String epc)
        {
            this.epc = Encoding.ASCII.GetBytes(epc);
        }
        /**
         * Custom constructor
         * @param epc epc in char array format, must be smaller than or equal to 31
         */
        public S_EPC(byte[] epc)
        {
            Array.Copy(epc, this.epc, epc.Length);
        }
        /**
         * Custom constructor
         * @param epc epc in char array format, must be smaller than or equal to 31
         * @param count number of char copy to local
         */
        public S_EPC(byte[] epc, int count)
        {
            Array.Copy(epc, this.epc, count);
        }
        /**
         * Set epc
         * @param value epc in char array format
         */
        public void SetEPC(char[] value)
        {
            Array.Copy(value, this.epc, value.Length);
        }
        /**
         * Get epc
         * @return value epc in char array format
         */
        public byte[] GetEPC()
        {
            return this.epc;
        }

        /**
         * get length of EPC
         * @return total char length of EPC
         */
        public int Length()
        {
            return (this.epc == null || this.epc.Length == 0) ? 0 : this.epc.Length;
        }

        /**
         * Convert epc to string format
         * @return EPC in string format
         */
        public String GetString()
        {
            return BitConverter.ToString(this.epc).Replace("-", "");
        }
    }
}
