namespace CS108_PC_Client
{
    partial class RFIDForm
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
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.lb_rate = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.lv_tag = new CS108_PC_Client.TListView();
            this.col_index = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.col_pc = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.col_epc = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.col_rssi = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.col_count = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.btn_stop_inventory = new System.Windows.Forms.Button();
            this.btn_inventory = new System.Windows.Forms.Button();
            this.btn_rfid_clear = new System.Windows.Forms.Button();
            this.btn_rfid_poweroff = new System.Windows.Forms.Button();
            this.btn_rfid_poweron = new System.Windows.Forms.Button();
            this.tb_info = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.tb_version = new System.Windows.Forms.TextBox();
            this.btn_version = new System.Windows.Forms.Button();
            this.label9 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.tb_profile = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.tb_channel = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.btn_set = new System.Windows.Forms.Button();
            this.tb_power = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.btn_clear_info = new System.Windows.Forms.Button();
            this.timer_rate = new System.Windows.Forms.Timer(this.components);
            this.label10 = new System.Windows.Forms.Label();
            this.tb_tx_on = new System.Windows.Forms.TextBox();
            this.label12 = new System.Windows.Forms.Label();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.lb_rate);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.label11);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.lv_tag);
            this.groupBox1.Controls.Add(this.btn_stop_inventory);
            this.groupBox1.Controls.Add(this.btn_inventory);
            this.groupBox1.Controls.Add(this.btn_rfid_clear);
            this.groupBox1.Controls.Add(this.btn_rfid_poweroff);
            this.groupBox1.Controls.Add(this.btn_rfid_poweron);
            this.groupBox1.Location = new System.Drawing.Point(12, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(757, 326);
            this.groupBox1.TabIndex = 4;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "RFID";
            // 
            // lb_rate
            // 
            this.lb_rate.AutoSize = true;
            this.lb_rate.Location = new System.Drawing.Point(644, 300);
            this.lb_rate.Name = "lb_rate";
            this.lb_rate.Size = new System.Drawing.Size(11, 12);
            this.lb_rate.TabIndex = 21;
            this.lb_rate.Text = "0";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(668, 300);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(30, 12);
            this.label3.TabIndex = 20;
            this.label3.Text = "tags/s";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(614, 300);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(32, 12);
            this.label11.TabIndex = 19;
            this.label11.Text = "Rate: ";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(3, 18);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(33, 12);
            this.label2.TabIndex = 18;
            this.label2.Text = "label2";
            // 
            // lv_tag
            // 
            this.lv_tag.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.col_index,
            this.col_pc,
            this.col_epc,
            this.col_rssi,
            this.col_count});
            this.lv_tag.GridLines = true;
            this.lv_tag.Location = new System.Drawing.Point(8, 21);
            this.lv_tag.Name = "lv_tag";
            this.lv_tag.Size = new System.Drawing.Size(743, 266);
            this.lv_tag.TabIndex = 17;
            this.lv_tag.UseCompatibleStateImageBehavior = false;
            this.lv_tag.View = System.Windows.Forms.View.Details;
            // 
            // col_index
            // 
            this.col_index.Tag = "";
            this.col_index.Text = "Index";
            // 
            // col_pc
            // 
            this.col_pc.Text = "PC";
            this.col_pc.Width = 50;
            // 
            // col_epc
            // 
            this.col_epc.Text = "EPC";
            this.col_epc.Width = 496;
            // 
            // col_rssi
            // 
            this.col_rssi.Text = "RSSI";
            this.col_rssi.Width = 59;
            // 
            // col_count
            // 
            this.col_count.Text = "Count";
            this.col_count.Width = 73;
            // 
            // btn_stop_inventory
            // 
            this.btn_stop_inventory.Location = new System.Drawing.Point(341, 293);
            this.btn_stop_inventory.Name = "btn_stop_inventory";
            this.btn_stop_inventory.Size = new System.Drawing.Size(105, 27);
            this.btn_stop_inventory.TabIndex = 16;
            this.btn_stop_inventory.Text = "Stop Inventory";
            this.btn_stop_inventory.UseVisualStyleBackColor = true;
            this.btn_stop_inventory.Click += new System.EventHandler(this.btn_stop_inventory_Click);
            // 
            // btn_inventory
            // 
            this.btn_inventory.Location = new System.Drawing.Point(230, 293);
            this.btn_inventory.Name = "btn_inventory";
            this.btn_inventory.Size = new System.Drawing.Size(105, 27);
            this.btn_inventory.TabIndex = 15;
            this.btn_inventory.Text = "Start Inventory";
            this.btn_inventory.UseVisualStyleBackColor = true;
            this.btn_inventory.Click += new System.EventHandler(this.btn_inventory_Click);
            // 
            // btn_rfid_clear
            // 
            this.btn_rfid_clear.Location = new System.Drawing.Point(452, 293);
            this.btn_rfid_clear.Name = "btn_rfid_clear";
            this.btn_rfid_clear.Size = new System.Drawing.Size(105, 27);
            this.btn_rfid_clear.TabIndex = 11;
            this.btn_rfid_clear.Text = "Clear";
            this.btn_rfid_clear.UseVisualStyleBackColor = true;
            this.btn_rfid_clear.Click += new System.EventHandler(this.btn_rfid_clear_Click);
            // 
            // btn_rfid_poweroff
            // 
            this.btn_rfid_poweroff.Location = new System.Drawing.Point(119, 293);
            this.btn_rfid_poweroff.Name = "btn_rfid_poweroff";
            this.btn_rfid_poweroff.Size = new System.Drawing.Size(105, 27);
            this.btn_rfid_poweroff.TabIndex = 10;
            this.btn_rfid_poweroff.Text = "Power Off";
            this.btn_rfid_poweroff.UseVisualStyleBackColor = true;
            this.btn_rfid_poweroff.Click += new System.EventHandler(this.btn_rfid_poweroff_Click);
            // 
            // btn_rfid_poweron
            // 
            this.btn_rfid_poweron.Location = new System.Drawing.Point(8, 293);
            this.btn_rfid_poweron.Name = "btn_rfid_poweron";
            this.btn_rfid_poweron.Size = new System.Drawing.Size(105, 27);
            this.btn_rfid_poweron.TabIndex = 7;
            this.btn_rfid_poweron.Text = "Power On";
            this.btn_rfid_poweron.UseVisualStyleBackColor = true;
            this.btn_rfid_poweron.Click += new System.EventHandler(this.btn_rfid_poweron_Click);
            // 
            // tb_info
            // 
            this.tb_info.Location = new System.Drawing.Point(12, 470);
            this.tb_info.Multiline = true;
            this.tb_info.Name = "tb_info";
            this.tb_info.ReadOnly = true;
            this.tb_info.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.tb_info.Size = new System.Drawing.Size(646, 71);
            this.tb_info.TabIndex = 5;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 455);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(28, 12);
            this.label1.TabIndex = 6;
            this.label1.Text = "Info:";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.label10);
            this.groupBox2.Controls.Add(this.tb_tx_on);
            this.groupBox2.Controls.Add(this.label12);
            this.groupBox2.Controls.Add(this.tb_version);
            this.groupBox2.Controls.Add(this.btn_version);
            this.groupBox2.Controls.Add(this.label9);
            this.groupBox2.Controls.Add(this.label8);
            this.groupBox2.Controls.Add(this.tb_profile);
            this.groupBox2.Controls.Add(this.label7);
            this.groupBox2.Controls.Add(this.label6);
            this.groupBox2.Controls.Add(this.tb_channel);
            this.groupBox2.Controls.Add(this.label5);
            this.groupBox2.Controls.Add(this.btn_set);
            this.groupBox2.Controls.Add(this.tb_power);
            this.groupBox2.Controls.Add(this.label4);
            this.groupBox2.Location = new System.Drawing.Point(12, 344);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(757, 106);
            this.groupBox2.TabIndex = 7;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Settings";
            // 
            // tb_version
            // 
            this.tb_version.Location = new System.Drawing.Point(611, 71);
            this.tb_version.MaxLength = 100;
            this.tb_version.Name = "tb_version";
            this.tb_version.ReadOnly = true;
            this.tb_version.Size = new System.Drawing.Size(48, 22);
            this.tb_version.TabIndex = 26;
            // 
            // btn_version
            // 
            this.btn_version.Location = new System.Drawing.Point(500, 67);
            this.btn_version.Name = "btn_version";
            this.btn_version.Size = new System.Drawing.Size(105, 27);
            this.btn_version.TabIndex = 25;
            this.btn_version.Text = "Get Version";
            this.btn_version.UseVisualStyleBackColor = true;
            this.btn_version.Click += new System.EventHandler(this.btn_version_Click);
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(104, 18);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(39, 12);
            this.label9.TabIndex = 24;
            this.label9.Text = "0 - 320";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(104, 74);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(27, 12);
            this.label8.TabIndex = 23;
            this.label8.Text = "0 - 3";
            // 
            // tb_profile
            // 
            this.tb_profile.Location = new System.Drawing.Point(59, 71);
            this.tb_profile.MaxLength = 1;
            this.tb_profile.Name = "tb_profile";
            this.tb_profile.Size = new System.Drawing.Size(39, 22);
            this.tb_profile.TabIndex = 22;
            this.tb_profile.Text = "1";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(6, 74);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(39, 12);
            this.label7.TabIndex = 21;
            this.label7.Text = "Profile:";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(104, 46);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(165, 12);
            this.label6.TabIndex = 20;
            this.label6.Text = "0 = hopping; 1-50 = fixed channel";
            // 
            // tb_channel
            // 
            this.tb_channel.Location = new System.Drawing.Point(59, 43);
            this.tb_channel.MaxLength = 2;
            this.tb_channel.Name = "tb_channel";
            this.tb_channel.Size = new System.Drawing.Size(39, 22);
            this.tb_channel.TabIndex = 19;
            this.tb_channel.Text = "0";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(6, 46);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(47, 12);
            this.label5.TabIndex = 18;
            this.label5.Text = "Channel:";
            // 
            // btn_set
            // 
            this.btn_set.Location = new System.Drawing.Point(277, 67);
            this.btn_set.Name = "btn_set";
            this.btn_set.Size = new System.Drawing.Size(105, 27);
            this.btn_set.TabIndex = 17;
            this.btn_set.Text = "Set";
            this.btn_set.UseVisualStyleBackColor = true;
            this.btn_set.Click += new System.EventHandler(this.btn_set_Click);
            // 
            // tb_power
            // 
            this.tb_power.Location = new System.Drawing.Point(59, 15);
            this.tb_power.MaxLength = 3;
            this.tb_power.Name = "tb_power";
            this.tb_power.Size = new System.Drawing.Size(39, 22);
            this.tb_power.TabIndex = 1;
            this.tb_power.Text = "300";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(6, 18);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(37, 12);
            this.label4.TabIndex = 0;
            this.label4.Text = "Power:";
            // 
            // btn_clear_info
            // 
            this.btn_clear_info.Location = new System.Drawing.Point(664, 514);
            this.btn_clear_info.Name = "btn_clear_info";
            this.btn_clear_info.Size = new System.Drawing.Size(105, 27);
            this.btn_clear_info.TabIndex = 18;
            this.btn_clear_info.Text = "Clear";
            this.btn_clear_info.UseVisualStyleBackColor = true;
            this.btn_clear_info.Click += new System.EventHandler(this.btn_clear_info_Click);
            // 
            // timer_rate
            // 
            this.timer_rate.Enabled = true;
            this.timer_rate.Interval = 1000;
            this.timer_rate.Tick += new System.EventHandler(this.readrate_Tick);
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(373, 18);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(18, 12);
            this.label10.TabIndex = 29;
            this.label10.Text = "ms";
            // 
            // tb_tx_on
            // 
            this.tb_tx_on.Location = new System.Drawing.Point(328, 15);
            this.tb_tx_on.MaxLength = 3;
            this.tb_tx_on.Name = "tb_tx_on";
            this.tb_tx_on.Size = new System.Drawing.Size(39, 22);
            this.tb_tx_on.TabIndex = 28;
            this.tb_tx_on.Text = "400";
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(275, 18);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(40, 12);
            this.label12.TabIndex = 27;
            this.label12.Text = "TX On:";
            // 
            // RFIDForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(781, 553);
            this.Controls.Add(this.btn_clear_info);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.tb_info);
            this.Controls.Add(this.groupBox1);
            this.Name = "RFIDForm";
            this.Text = "RFID";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Main_FormClosing);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button btn_rfid_clear;
        private System.Windows.Forms.Button btn_rfid_poweroff;
        private System.Windows.Forms.Button btn_rfid_poweron;
        private System.Windows.Forms.TextBox tb_info;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btn_inventory;
        private System.Windows.Forms.Button btn_stop_inventory;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.TextBox tb_power;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox tb_channel;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Button btn_set;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox tb_profile;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label9;
        //private System.Windows.Forms.ListView lv_tag;
        private TListView lv_tag;
        private System.Windows.Forms.ColumnHeader col_index;
        private System.Windows.Forms.ColumnHeader col_pc;
        private System.Windows.Forms.ColumnHeader col_epc;
        private System.Windows.Forms.ColumnHeader col_rssi;
        private System.Windows.Forms.ColumnHeader col_count;
        private System.Windows.Forms.Button btn_clear_info;
        private System.Windows.Forms.TextBox tb_version;
        private System.Windows.Forms.Button btn_version;
        private System.Windows.Forms.Timer timer_rate;
        private System.Windows.Forms.Label lb_rate;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.TextBox tb_tx_on;
        private System.Windows.Forms.Label label12;
    }
}

