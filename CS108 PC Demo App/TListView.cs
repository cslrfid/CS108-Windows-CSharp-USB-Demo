using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace CS108_PC_Client
{
    class TListView : ListView
    {
        public TListView()
        {
            this.DoubleBuffered = true;
        }
    }
}
