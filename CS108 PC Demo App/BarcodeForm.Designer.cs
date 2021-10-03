namespace CS108_PC_Client
{
    partial class BarcodeForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.barcode_timer_read = new System.Windows.Forms.Timer(this.components);
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.btn_trigger = new System.Windows.Forms.Button();
            this.btn_barcode_clear = new System.Windows.Forms.Button();
            this.btn_barcode_poweroff = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.tb_barcode_receive = new System.Windows.Forms.TextBox();
            this.btn_barcode_poweron = new System.Windows.Forms.Button();
            this.tb_status = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.btn_cont = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // barcode_timer_read
            // 
            this.barcode_timer_read.Interval = 15;
            this.barcode_timer_read.Tick += new System.EventHandler(this.barcode_timer_read_Tick);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.btn_cont);
            this.groupBox1.Controls.Add(this.btn_trigger);
            this.groupBox1.Controls.Add(this.btn_barcode_clear);
            this.groupBox1.Controls.Add(this.btn_barcode_poweroff);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.tb_barcode_receive);
            this.groupBox1.Controls.Add(this.btn_barcode_poweron);
            this.groupBox1.Location = new System.Drawing.Point(12, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(523, 220);
            this.groupBox1.TabIndex = 4;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Barcode";
            // 
            // btn_trigger
            // 
            this.btn_trigger.Location = new System.Drawing.Point(402, 99);
            this.btn_trigger.Name = "btn_trigger";
            this.btn_trigger.Size = new System.Drawing.Size(105, 27);
            this.btn_trigger.TabIndex = 12;
            this.btn_trigger.Text = "Trigger Scan";
            this.btn_trigger.UseVisualStyleBackColor = true;
            this.btn_trigger.Click += new System.EventHandler(this.btn_trigger_Click);
            // 
            // btn_barcode_clear
            // 
            this.btn_barcode_clear.Location = new System.Drawing.Point(402, 142);
            this.btn_barcode_clear.Name = "btn_barcode_clear";
            this.btn_barcode_clear.Size = new System.Drawing.Size(105, 27);
            this.btn_barcode_clear.TabIndex = 11;
            this.btn_barcode_clear.Text = "Clear";
            this.btn_barcode_clear.UseVisualStyleBackColor = true;
            this.btn_barcode_clear.Click += new System.EventHandler(this.btn_barcode_clear_Click);
            // 
            // btn_barcode_poweroff
            // 
            this.btn_barcode_poweroff.Location = new System.Drawing.Point(402, 66);
            this.btn_barcode_poweroff.Name = "btn_barcode_poweroff";
            this.btn_barcode_poweroff.Size = new System.Drawing.Size(105, 27);
            this.btn_barcode_poweroff.TabIndex = 10;
            this.btn_barcode_poweroff.Text = "Power Off";
            this.btn_barcode_poweroff.UseVisualStyleBackColor = true;
            this.btn_barcode_poweroff.Click += new System.EventHandler(this.btn_barcode_poweroff_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(6, 18);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(89, 12);
            this.label3.TabIndex = 9;
            this.label3.Text = "Receive (ASCII) :";
            // 
            // tb_barcode_receive
            // 
            this.tb_barcode_receive.Location = new System.Drawing.Point(6, 33);
            this.tb_barcode_receive.Multiline = true;
            this.tb_barcode_receive.Name = "tb_barcode_receive";
            this.tb_barcode_receive.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.tb_barcode_receive.Size = new System.Drawing.Size(392, 136);
            this.tb_barcode_receive.TabIndex = 8;
            // 
            // btn_barcode_poweron
            // 
            this.btn_barcode_poweron.Location = new System.Drawing.Point(402, 33);
            this.btn_barcode_poweron.Name = "btn_barcode_poweron";
            this.btn_barcode_poweron.Size = new System.Drawing.Size(105, 27);
            this.btn_barcode_poweron.TabIndex = 7;
            this.btn_barcode_poweron.Text = "Power On";
            this.btn_barcode_poweron.UseVisualStyleBackColor = true;
            this.btn_barcode_poweron.Click += new System.EventHandler(this.btn_barcode_poweron_Click);
            // 
            // tb_status
            // 
            this.tb_status.Location = new System.Drawing.Point(12, 250);
            this.tb_status.Name = "tb_status";
            this.tb_status.ReadOnly = true;
            this.tb_status.Size = new System.Drawing.Size(522, 22);
            this.tb_status.TabIndex = 5;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 235);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(35, 12);
            this.label1.TabIndex = 6;
            this.label1.Text = "Status:";
            // 
            // btn_cont
            // 
            this.btn_cont.Location = new System.Drawing.Point(8, 175);
            this.btn_cont.Name = "btn_cont";
            this.btn_cont.Size = new System.Drawing.Size(105, 27);
            this.btn_cont.TabIndex = 13;
            this.btn_cont.Text = "Continuous Mode";
            this.btn_cont.UseVisualStyleBackColor = true;
            this.btn_cont.Click += new System.EventHandler(this.btn_cont_Click);
            // 
            // BarcodeForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(548, 284);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.tb_status);
            this.Controls.Add(this.groupBox1);
            this.Name = "BarcodeForm";
            this.Text = "Barcode";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Main_FormClosing);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Timer barcode_timer_read;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button btn_barcode_clear;
        private System.Windows.Forms.Button btn_barcode_poweroff;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox tb_barcode_receive;
        private System.Windows.Forms.Button btn_barcode_poweron;
        private System.Windows.Forms.TextBox tb_status;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btn_trigger;
        private System.Windows.Forms.Button btn_cont;
    }
}

