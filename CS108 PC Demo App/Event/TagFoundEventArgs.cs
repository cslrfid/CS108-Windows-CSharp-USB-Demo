using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CS108_PC_Client
{
    class TagFoundEventArgs : EventArgs
    {
        public TagCallbackInfo info;

        public TagFoundEventArgs(TagCallbackInfo info)
        {
            this.info = info;
        }
    }
}
