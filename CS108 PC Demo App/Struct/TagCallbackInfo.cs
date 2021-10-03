using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CS108_PC_Client
{
    class TagCallbackInfo
    {
        /**
     * Index number, First come with small number.
     */
        public int index = -1;
        /**
         * The Receive Signal Strength Indicator (RSSI).
         */
        public float rssi;//4
        /**
         * Total count
         */
        public int count = 1;//4
        /**
         * PC Data
         */
        public S_PC pc;
        /**
         * EPC Data
         */
        public S_EPC epc;
        /**
         * TID Data
         */
        public S_EPC tid;
        /**
         * Constructor
         */
        public TagCallbackInfo() { }
        /**
         * Constructor
         * @param index
         * @param rssi
         * @param count
         * @param pc
         * @param epc
         * @param port
         */
        public TagCallbackInfo(int index, float rssi, int count, S_PC pc, S_EPC epc)
        {
            this.index = index;
            this.rssi = rssi;
            this.count = count;
            this.pc = pc;
            this.epc = epc;
        }

        /**
         * Constructor
         * @param rssi
         * @param pc
         * @param epc
         * @param port
         */
        public TagCallbackInfo(float rssi, S_PC pc, S_EPC epc)
        {
            this.rssi = rssi;
            this.pc = pc;
            this.epc = epc;
        }

        /**
         * Constructor
         * @param index
         * @param pc
         * @param epc
         * @param port
         */
        public TagCallbackInfo(int index, S_PC pc, S_EPC epc)
        {
            this.index = index;
            this.pc = pc;
            this.epc = epc;
        }

        /**
         * Constructor
         * @param pc
         * @param epc
         * @param port
         */
        public TagCallbackInfo(S_PC pc, S_EPC epc)
        {
            this.pc = pc;
            this.epc = epc;
        }

        public TagCallbackInfo(S_PC pc, S_EPC epc, S_EPC tid)
        {
            this.pc = pc;
            this.epc = epc;
            this.tid = tid;
        }

        /**
         * Search and sorting compare
         * @param o
         * @return
         */
        public int compareTo(Object o)
        {
            TagCallbackInfo that;
            that = (TagCallbackInfo)o;
            return this.epc.ToString().CompareTo(that.epc.ToString());
        }
    }
}
