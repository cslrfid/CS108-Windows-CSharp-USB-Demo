using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace CS108_PC_Client
{
    public partial class UpdateProgressForm : Form
    {
        public UpdateProgressForm()
        {
            InitializeComponent();
        }

        public void UpdateProgressPercent(int percent)
        {
            pb_prog.Value = percent;
            lb_msg.Text = String.Format("Total percent : {0}/100", percent);
        }

        public void UpdateProgressResult(String status)
        {
            lb_msg.Text = status;
        }

        private void btn_ok_Click(object sender, EventArgs e)
        {
            this.Hide();
        }
    }
}