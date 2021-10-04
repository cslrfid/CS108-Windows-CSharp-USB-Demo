using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace CS108_PC_Client
{
    class RFIDFreqTable
    {
        /*
	    0  - Factory Default
	    1  - Hong Kong
	    2  - South Africa
	    3  - Thailand
	    4  - LH1
	    5  - LH2
	    6  - Venezuela
	    7  - Dominican Republic
	    8  - Indonesia
	    9  - UH2
	    10 - Uruguay
	    11 - FCC
	    12 - UH1
	    13 - Argentina
	    14 - Malaysia
	    15 - Singapore
	    16 - Australia
	    17 - Brazil 902-904
	    18 - Brazil 917-924
	    19 - Brazil 915-927
	    20 - Brazil 902-906, 915-927
	    21 - Brazil 902-906
	    22 - Philippine
	    23 - Costa Rica
	    24 - Peru
	    25 - Colombia
	    26 - Israel
	    27 - Panama
	    28 - Chile
	    29 - ETSI
	    30 - India
	    */
        //public uint NUM_COUNTRIES = 30;
        //public uint[,] freqTable = new uint[30,2];

        public static uint[] hkFreqTable = new uint[] // Hong Kong
        {
            0x00180E63, /*920.75MHz   */
            0x00180E69, /*922.25MHz   */
            0x00180E6F, /*923.75MHz   */
            0x00180E65, /*921.25MHz   */
            0x00180E6B, /*922.75MHz   */
            0x00180E71, /*924.25MHz   */
            0x00180E67, /*921.75MHz   */
            0x00180E6D, /*923.25MHz   */
        };

        public static uint[] zaFreqTable = new uint[] // South Africa
        {
            0x003C23C5, /*915.7 MHz   */ 
            0x003C23C7, /*915.9 MHz   */
            0x003C23C9, /*916.1 MHz   */
            0x003C23CB, /*916.3 MHz   */
            0x003C23CD, /*916.5 MHz   */
            0x003C23CF, /*916.7 MHz   */
            0x003C23D1, /*916.9 MHz   */
            0x003C23D3, /*917.1 MHz   */
            0x003C23D5, /*917.3 MHz   */
            0x003C23D7, /*917.5 MHz   */
            0x003C23D9, /*917.7 MHz   */
            0x003C23DB, /*917.9 MHz   */
            0x003C23DD, /*918.1 MHz   */
            0x003C23DF, /*918.3 MHz   */
            0x003C23E1, /*918.5 MHz   */
            0x003C23E3, /*918.7 MHz   */
        };

        public static uint[] thFreqTable = new uint[] // Thailand
        {
            0x00180E63, /*920.75MHz   */
            0x00180E69, /*922.25MHz   */
            0x00180E6F, /*923.75MHz   */
            0x00180E65, /*921.25MHz   */
            0x00180E6B, /*922.75MHz   */
            0x00180E71, /*924.25MHz   */
            0x00180E67, /*921.75MHz   */
            0x00180E6D, /*923.25MHz   */
        };

        public static uint[] LH1FreqTable = new uint[]
        {
            0x00180E1D, /*903.25 MHz   */
            0x00180E21, /*904.25 MHz   */
            0x00180E35, /*909.25 MHz   */
            0x00180E25, /*905.25 MHz   */
            0x00180E23, /*904.75 MHz   */
            0x00180E2B, /*906.75 MHz   */
            0x00180E1F, /*903.75 MHz   */
            0x00180E33, /*908.75 MHz   */
            0x00180E27, /*905.75 MHz   */
            0x00180E29, /*906.25 MHz   */
            0x00180E2D, /*907.25 MHz   */
            0x00180E2F, /*907.75 MHz   */
            0x00180E31, /*908.25 MHz   */
            0x00180E1B, /*902.75 MHz   */
        };

        public static uint[] LH2FreqTable = new uint[]
        {
            0x00180E4B, /*914.75 MHz   */
            0x00180E47, /*913.75 MHz   */
            0x00180E3D, /*911.25 MHz   */
            0x00180E3F, /*911.75 MHz   */
            0x00180E41, /*912.25 MHz   */
            0x00180E49, /*914.25 MHz   */
            0x00180E39, /*910.25 MHz   */
            0x00180E3B, /*910.75 MHz   */
            0x00180E37, /*909.75 MHz   */
            0x00180E45, /*913.25 MHz   */
            0x00180E43, /*912.75 MHz   */
        };

        public static uint[] veFreqTable = new uint[] // Venezuela
        {
            0x00180E7B, /*926.75 MHz   */
            0x00180E79, /*926.25 MHz   */
            0x00180E7D, /*927.25 MHz   */
            0x00180E75, /*925.25 MHz   */
            0x00180E73, /*924.75 MHz   */
            0x00180E6F, /*923.75 MHz   */
            0x00180E77, /*925.75 MHz   */
            0x00180E71, /*924.25 MHz   */
            0x00180E6B, /*922.75 MHz   */
            0x00180E6D, /*923.25 MHz   */
        };

        public static uint[] indonesiaFreqTable = new uint[] // Indonesia
        {
            0x00180E6D, /*923.25 MHz    */
            0x00180E6F, /*923.75 MHz    */
            0x00180E71, /*924.25 MHz    */
            0x00180E73, /*924.75 MHz    */
        };

        public static uint[] UH2FreqTable = new uint[]
        {
            0x00180E7B, /*926.75 MHz   */
            0x00180E79, /*926.25 MHz   */
            0x00180E7D, /*927.25 MHz   */
            0x00180E61, /*920.25 MHz   */
            0x00180E75, /*925.25 MHz   */
            0x00180E67, /*921.75 MHz   */
            0x00180E69, /*922.25 MHz   */
            0x00180E73, /*924.75 MHz   */
            0x00180E6F, /*923.75 MHz   */
            0x00180E77, /*925.75 MHz   */
            0x00180E71, /*924.25 MHz   */
            0x00180E65, /*921.25 MHz   */
            0x00180E63, /*920.75 MHz   */
            0x00180E6B, /*922.75 MHz   */
            0x00180E6D, /*923.25 MHz   */
        };

        public static uint[] UH1FreqTable = new uint[]
        {
            0x00180E4F, /*915.75 MHz   */
            0x00180E5D, /*919.25 MHz   */
            0x00180E5B, /*918.75 MHz   */
            0x00180E57, /*917.75 MHz   */
            0x00180E55, /*917.25 MHz   */
            0x00180E59, /*918.25 MHz   */
            0x00180E51, /*916.25 MHz   */
            0x00180E5F, /*919.75 MHz   */
            0x00180E53, /*916.75 MHz   */
            0x00180E4D, /*915.25 MHz   */
        };

        public static uint[] mysFreqTable = new uint[]// Malaysia
        {
            0x00180E5F, /*919.75MHz   */
            0x00180E65, /*921.25MHz   */
            0x00180E61, /*920.25MHz   */
            0x00180E67, /*921.75MHz   */
            0x00180E63, /*920.75MHz   */
            0x00180E69, /*922.25MHz   */
        };

        public static uint[] sgFreqTable = new uint[] // Singapore
        {
            0x00180E63, /*920.75MHz   */
            0x00180E69, /*922.25MHz   */
            0x00180E6F, /*923.75MHz   */
            0x00180E65, /*921.25MHz   */
            0x00180E6B, /*922.75MHz   */
            0x00180E71, /*924.25MHz   */
            0x00180E67, /*921.75MHz   */
            0x00180E6D, /*923.25MHz   */
        };

        public static uint[] AusFreqTable = new uint[] // Australia
        {
            0x00180E63, /* 920.75MHz   */
            0x00180E69, /* 922.25MHz   */
            0x00180E6F, /* 923.75MHz   */
            0x00180E73, /* 924.75MHz   */
            0x00180E65, /* 921.25MHz   */
            0x00180E6B, /* 922.75MHz   */
            0x00180E71, /* 924.25MHz   */
            0x00180E75, /* 925.25MHz   */
            0x00180E67, /* 921.75MHz   */
            0x00180E6D, /* 923.25MHz   */
        };

        public static uint[] br1FreqTable = new uint[] // Brazil 902-904
        {
		        0x00180E1B, /*902.75 MHz   */
		        0x00180E1F, /*903.75 MHz   */
		        0x00180E1D, /*903.25 MHz   */
		        0x00180E21, /*904.25 MHz   */
        };

        public static uint[] br2FreqTable = new uint[] // Brazil 917-924
        {
            0x00180E61, /*920.25 MHz   */
            0x00180E5D, /*919.25 MHz   */
            0x00180E5B, /*918.75 MHz   */
            0x00180E57, /*917.75 MHz   */
            0x00180E67, /*921.75 MHz   */
            0x00180E69, /*922.25 MHz   */
            0x00180E59, /*918.25 MHz   */
            0x00180E5F, /*919.75 MHz   */
            0x00180E6F, /*923.75 MHz   */
            0x00180E71, /*924.25 MHz   */
            0x00180E65, /*921.25 MHz   */
            0x00180E63, /*920.75 MHz   */
            0x00180E6B, /*922.75 MHz   */
            0x00180E6D, /*923.25 MHz   */
        };

        public static uint[] br3FreqTable = new uint[] // Brazil 915-927
        {
            0x00180E4F, /*915.75 MHz   */
            0x00180E7B, /*926.75 MHz   */
            0x00180E79, /*926.25 MHz   */
            0x00180E7D, /*927.25 MHz   */
            0x00180E61, /*920.25 MHz   */
            0x00180E5D, /*919.25 MHz   */
            0x00180E5B, /*918.75 MHz   */
            0x00180E57, /*917.75 MHz   */
            0x00180E75, /*925.25 MHz   */
            0x00180E67, /*921.75 MHz   */
            0x00180E69, /*922.25 MHz   */
            0x00180E55, /*917.25 MHz   */
            0x00180E59, /*918.25 MHz   */
            0x00180E51, /*916.25 MHz   */
            0x00180E73, /*924.75 MHz   */
            0x00180E5F, /*919.75 MHz   */
            0x00180E53, /*916.75 MHz   */
            0x00180E6F, /*923.75 MHz   */
            0x00180E77, /*925.75 MHz   */
            0x00180E71, /*924.25 MHz   */
            0x00180E65, /*921.25 MHz   */
            0x00180E63, /*920.75 MHz   */
            0x00180E6B, /*922.75 MHz   */
            0x00180E6D, /*923.25 MHz   */
        };

        public static uint[] br4FreqTable = new uint[] // Brazil 902-906, 915-927
        {
            0x00180E4F, /*915.75 MHz   */
            0x00180E1D, /*903.25 MHz   */
            0x00180E7B, /*926.75 MHz   */
            0x00180E79, /*926.25 MHz   */
            0x00180E21, /*904.25 MHz   */
            0x00180E7D, /*927.25 MHz   */
            0x00180E61, /*920.25 MHz   */
            0x00180E5D, /*919.25 MHz   */
            0x00180E5B, /*918.75 MHz   */
            0x00180E57, /*917.75 MHz   */
            0x00180E25, /*905.25 MHz   */
            0x00180E23, /*904.75 MHz   */
            0x00180E75, /*925.25 MHz   */
            0x00180E67, /*921.75 MHz   */
            0x00180E2B, /*906.75 MHz   */
            0x00180E69, /*922.25 MHz   */
            0x00180E1F, /*903.75 MHz   */
            0x00180E27, /*905.75 MHz   */
            0x00180E29, /*906.25 MHz   */
            0x00180E55, /*917.25 MHz   */
            0x00180E59, /*918.25 MHz   */
            0x00180E51, /*916.25 MHz   */
            0x00180E73, /*924.75 MHz   */
            0x00180E5F, /*919.75 MHz   */
            0x00180E53, /*916.75 MHz   */
            0x00180E6F, /*923.75 MHz   */
            0x00180E77, /*925.75 MHz   */
            0x00180E71, /*924.25 MHz   */
            0x00180E65, /*921.25 MHz   */
            0x00180E63, /*920.75 MHz   */
            0x00180E6B, /*922.75 MHz   */
            0x00180E1B, /*902.75 MHz   */
            0x00180E6D, /*923.25 MHz   */
        };

        public static uint[] br5FreqTable = new uint[] // Brazil 902-906
        {
            0x00180E1D, /*903.25 MHz   */
            0x00180E21, /*904.25 MHz   */
            0x00180E25, /*905.25 MHz   */
            0x00180E23, /*904.75 MHz   */
            0x00180E2B, /*906.75 MHz   */
            0x00180E1F, /*903.75 MHz   */
            0x00180E27, /*905.75 MHz   */
            0x00180E29, /*906.25 MHz   */
            0x00180E1B, /*902.75 MHz   */
        };

        public static uint[] phiFreqTable = new uint[] // Philippine
        {
		    0x00301CB1, /*918.125MHz   */
		    0x00301CBD, /*919.625MHz   */
		    0x00301CB3, /*918.375MHz   */
		    0x00301CB7, /*918.875MHz   */
		    0x00301CB5, /*918.625MHz   */
		    0x00301CB9, /*919.125MHz   */
		    0x00301CBB, /*919.375MHz   */
		    0x00301CBF, /*919.875MHz   */
        };

        public static uint[] isFreqTable = new uint[] // Israel
        {
		    0x00180E4D, /*915.25 MHz   */
		    0x00180E51, /*916.25 MHz   */
		    0x003C23C8, /*916.00 MHz   */
		    0x003C23C3, /*915.50 MHz   */
		    0x003C23CD, /*916.50 MHz   */
		    0x00180E4F, /*915.75 MHz   */
		    0x00180E53, /*916.75 MHz   */
        };

        public static uint[] fccFreqTable = new uint[]
        {
            0x00180E4F, /*915.75 MHz   */
            0x00180E4D, /*915.25 MHz   */
            0x00180E1D, /*903.25 MHz   */
            0x00180E7B, /*926.75 MHz   */
            0x00180E79, /*926.25 MHz   */
            0x00180E21, /*904.25 MHz   */
            0x00180E7D, /*927.25 MHz   */
            0x00180E61, /*920.25 MHz   */
            0x00180E5D, /*919.25 MHz   */
            0x00180E35, /*909.25 MHz   */
            0x00180E5B, /*918.75 MHz   */
            0x00180E57, /*917.75 MHz   */
            0x00180E25, /*905.25 MHz   */
            0x00180E23, /*904.75 MHz   */
            0x00180E75, /*925.25 MHz   */
            0x00180E67, /*921.75 MHz   */
            0x00180E4B, /*914.75 MHz   */
            0x00180E2B, /*906.75 MHz   */
            0x00180E47, /*913.75 MHz   */
            0x00180E69, /*922.25 MHz   */
            0x00180E3D, /*911.25 MHz   */
            0x00180E3F, /*911.75 MHz   */
            0x00180E1F, /*903.75 MHz   */
            0x00180E33, /*908.75 MHz   */
            0x00180E27, /*905.75 MHz   */
            0x00180E41, /*912.25 MHz   */
            0x00180E29, /*906.25 MHz   */
            0x00180E55, /*917.25 MHz   */
            0x00180E49, /*914.25 MHz   */
            0x00180E2D, /*907.25 MHz   */
            0x00180E59, /*918.25 MHz   */
            0x00180E51, /*916.25 MHz   */
            0x00180E39, /*910.25 MHz   */
            0x00180E3B, /*910.75 MHz   */
            0x00180E2F, /*907.75 MHz   */
            0x00180E73, /*924.75 MHz   */
            0x00180E37, /*909.75 MHz   */
            0x00180E5F, /*919.75 MHz   */
            0x00180E53, /*916.75 MHz   */
            0x00180E45, /*913.25 MHz   */
            0x00180E6F, /*923.75 MHz   */
            0x00180E31, /*908.25 MHz   */
            0x00180E77, /*925.75 MHz   */
            0x00180E43, /*912.75 MHz   */
            0x00180E71, /*924.25 MHz   */
            0x00180E65, /*921.25 MHz   */
            0x00180E63, /*920.75 MHz   */
            0x00180E6B, /*922.75 MHz   */
            0x00180E1B, /*902.75 MHz   */
            0x00180E6D, /*923.25 MHz   */
        };

        // Fixed

        public static uint[] etsiFreqTable = new uint[]
        {
            0x003C21D1, /*865.700MHz   */
            0x003C21D7, /*866.300MHz   */
            0x003C21DD, /*866.900MHz   */
            0x003C21E3, /*867.500MHz   */
        };

        public static uint[] indiaFreqTable = new uint[]
        {
            0x003C21D1, /*865.700MHz   */
            0x003C21D7, /*866.300MHz   */
            0x003C21DD, /*866.900MHz   */
        };
    }
}
