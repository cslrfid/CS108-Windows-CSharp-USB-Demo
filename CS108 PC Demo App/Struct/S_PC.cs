using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CS108_PC_Client
{
    class S_PC
    {
        /**
        * Protocol Control Value(one word)
        */
        public char pc;

        /**
         * Custom constructor
         * @param pc pc in string format, must be 4 hex numbers
         */
        public S_PC(String pc)
        {
            this.pc = System.Convert.ToChar(System.Convert.ToUInt32(pc));
        }
        /**
         * Custom constructor
         * @param pc pc in char format
         */
        public S_PC(char pc)
        {
            this.pc = pc;
        }

        /**
         * Set PC value in char
         * @param value
         */
        public void SetPC(char value)
        {
            pc = value;
        }
        /**
         * Get PC value in char
         * @return
         */
        public char GetPC()
        {
            return pc;
        }
        /**
         * Get 16bit EPC Length from current PC value
         * @return
         */
        public int EPCLength()
        {
            return (int)(pc >> 11 & 0x1F);
        }
        /**
         * User Memory Indicator, true if user memory contains data.
         * Notes: Not all tags support this function
         * @return
         */
        public bool UMI()
        {
            return ((pc >> 10 & 0x1) == 0x1);
        }
        /**
         * An XPC_W1 Indicator, true if XPC_W1 is non-zero value
         * Notes: Not all tags support this function
         * @return
         */
        public bool XI()
        {
            return ((pc >> 9 & 0x1) == 0x1);
        }
    }
}
