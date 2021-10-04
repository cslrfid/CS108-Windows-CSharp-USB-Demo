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
            this.lb_total = new System.Windows.Forms.Label();
            this.label13 = new System.Windows.Forms.Label();
            this.lb_rate = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
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
            this.btn_version = new System.Windows.Forms.Button();
            this.tb_version = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.tb_delay = new System.Windows.Forms.TextBox();
            this.label14 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.tb_tx_on = new System.Windows.Forms.TextBox();
            this.label12 = new System.Windows.Forms.Label();
            this.btn_clear_info = new System.Windows.Forms.Button();
            this.timer_rate = new System.Windows.Forms.Timer(this.components);
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.label25 = new System.Windows.Forms.Label();
            this.tb_cycledelay = new System.Windows.Forms.TextBox();
            this.label26 = new System.Windows.Forms.Label();
            this.btn_settest = new System.Windows.Forms.Button();
            this.cb_compact = new System.Windows.Forms.CheckBox();
            this.lb_elapsed = new System.Windows.Forms.Label();
            this.label16 = new System.Windows.Forms.Label();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.tb_retry = new System.Windows.Forms.TextBox();
            this.label23 = new System.Windows.Forms.Label();
            this.tb_threshold = new System.Windows.Forms.TextBox();
            this.tb_maxq = new System.Windows.Forms.TextBox();
            this.tb_minq = new System.Windows.Forms.TextBox();
            this.tb_startq = new System.Windows.Forms.TextBox();
            this.label22 = new System.Windows.Forms.Label();
            this.label15 = new System.Windows.Forms.Label();
            this.label17 = new System.Windows.Forms.Label();
            this.label19 = new System.Windows.Forms.Label();
            this.comboBox_algorithm = new System.Windows.Forms.ComboBox();
            this.comboBox_target = new System.Windows.Forms.ComboBox();
            this.comboBox_session = new System.Windows.Forms.ComboBox();
            this.btn_invset = new System.Windows.Forms.Button();
            this.label18 = new System.Windows.Forms.Label();
            this.label20 = new System.Windows.Forms.Label();
            this.label21 = new System.Windows.Forms.Label();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.lb_total);
            this.groupBox1.Controls.Add(this.label13);
            this.groupBox1.Controls.Add(this.lb_rate);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.label11);
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
            // lb_total
            // 
            this.lb_total.AutoSize = true;
            this.lb_total.Location = new System.Drawing.Point(594, 300);
            this.lb_total.Name = "lb_total";
            this.lb_total.Size = new System.Drawing.Size(11, 12);
            this.lb_total.TabIndex = 23;
            this.lb_total.Text = "0";
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(563, 300);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(35, 12);
            this.label13.TabIndex = 22;
            this.label13.Text = "Total: ";
            // 
            // lb_rate
            // 
            this.lb_rate.AutoSize = true;
            this.lb_rate.Location = new System.Drawing.Point(693, 300);
            this.lb_rate.Name = "lb_rate";
            this.lb_rate.Size = new System.Drawing.Size(11, 12);
            this.lb_rate.TabIndex = 21;
            this.lb_rate.Text = "0";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(717, 300);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(30, 12);
            this.label3.TabIndex = 20;
            this.label3.Text = "tags/s";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(663, 300);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(32, 12);
            this.label11.TabIndex = 19;
            this.label11.Text = "Rate: ";
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
            this.tb_info.Location = new System.Drawing.Point(12, 542);
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
            this.label1.Location = new System.Drawing.Point(12, 527);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(28, 12);
            this.label1.TabIndex = 6;
            this.label1.Text = "Info:";
            // 
            // groupBox2
            // 
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
            this.groupBox2.Size = new System.Drawing.Size(312, 123);
            this.groupBox2.TabIndex = 7;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Settings";
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
            this.btn_set.Location = new System.Drawing.Point(201, 90);
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
            // btn_version
            // 
            this.btn_version.Location = new System.Drawing.Point(588, 11);
            this.btn_version.Name = "btn_version";
            this.btn_version.Size = new System.Drawing.Size(105, 27);
            this.btn_version.TabIndex = 25;
            this.btn_version.Text = "Get Version";
            this.btn_version.UseVisualStyleBackColor = true;
            this.btn_version.Click += new System.EventHandler(this.btn_version_Click);
            // 
            // tb_version
            // 
            this.tb_version.Location = new System.Drawing.Point(699, 15);
            this.tb_version.MaxLength = 100;
            this.tb_version.Name = "tb_version";
            this.tb_version.ReadOnly = true;
            this.tb_version.Size = new System.Drawing.Size(48, 22);
            this.tb_version.TabIndex = 26;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(241, 18);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(18, 12);
            this.label2.TabIndex = 32;
            this.label2.Text = "ms";
            // 
            // tb_delay
            // 
            this.tb_delay.Location = new System.Drawing.Point(196, 15);
            this.tb_delay.MaxLength = 3;
            this.tb_delay.Name = "tb_delay";
            this.tb_delay.Size = new System.Drawing.Size(39, 22);
            this.tb_delay.TabIndex = 31;
            this.tb_delay.Text = "0";
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Location = new System.Drawing.Point(136, 18);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(54, 12);
            this.label14.TabIndex = 30;
            this.label14.Text = "Tag delay:";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(103, 18);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(18, 12);
            this.label10.TabIndex = 29;
            this.label10.Text = "ms";
            // 
            // tb_tx_on
            // 
            this.tb_tx_on.Location = new System.Drawing.Point(58, 15);
            this.tb_tx_on.MaxLength = 4;
            this.tb_tx_on.Name = "tb_tx_on";
            this.tb_tx_on.Size = new System.Drawing.Size(39, 22);
            this.tb_tx_on.TabIndex = 28;
            this.tb_tx_on.Text = "400";
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(5, 18);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(40, 12);
            this.label12.TabIndex = 27;
            this.label12.Text = "TX On:";
            // 
            // btn_clear_info
            // 
            this.btn_clear_info.Location = new System.Drawing.Point(664, 586);
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
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.label25);
            this.groupBox3.Controls.Add(this.tb_cycledelay);
            this.groupBox3.Controls.Add(this.label26);
            this.groupBox3.Controls.Add(this.label2);
            this.groupBox3.Controls.Add(this.btn_settest);
            this.groupBox3.Controls.Add(this.btn_version);
            this.groupBox3.Controls.Add(this.tb_version);
            this.groupBox3.Controls.Add(this.tb_delay);
            this.groupBox3.Controls.Add(this.label14);
            this.groupBox3.Controls.Add(this.label12);
            this.groupBox3.Controls.Add(this.label10);
            this.groupBox3.Controls.Add(this.tb_tx_on);
            this.groupBox3.Location = new System.Drawing.Point(14, 473);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(757, 51);
            this.groupBox3.TabIndex = 19;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Test Settings";
            // 
            // label25
            // 
            this.label25.AutoSize = true;
            this.label25.Location = new System.Drawing.Point(384, 18);
            this.label25.Name = "label25";
            this.label25.Size = new System.Drawing.Size(18, 12);
            this.label25.TabIndex = 35;
            this.label25.Text = "ms";
            // 
            // tb_cycledelay
            // 
            this.tb_cycledelay.Location = new System.Drawing.Point(339, 15);
            this.tb_cycledelay.MaxLength = 3;
            this.tb_cycledelay.Name = "tb_cycledelay";
            this.tb_cycledelay.Size = new System.Drawing.Size(39, 22);
            this.tb_cycledelay.TabIndex = 34;
            this.tb_cycledelay.Text = "0";
            // 
            // label26
            // 
            this.label26.AutoSize = true;
            this.label26.Location = new System.Drawing.Point(270, 18);
            this.label26.Name = "label26";
            this.label26.Size = new System.Drawing.Size(63, 12);
            this.label26.TabIndex = 33;
            this.label26.Text = "Cycle delay:";
            // 
            // btn_settest
            // 
            this.btn_settest.Location = new System.Drawing.Point(437, 11);
            this.btn_settest.Name = "btn_settest";
            this.btn_settest.Size = new System.Drawing.Size(105, 27);
            this.btn_settest.TabIndex = 17;
            this.btn_settest.Text = "Set";
            this.btn_settest.UseVisualStyleBackColor = true;
            this.btn_settest.Click += new System.EventHandler(this.btn_settest_Click);
            // 
            // cb_compact
            // 
            this.cb_compact.AutoSize = true;
            this.cb_compact.Location = new System.Drawing.Point(333, 17);
            this.cb_compact.Name = "cb_compact";
            this.cb_compact.Size = new System.Drawing.Size(96, 16);
            this.cb_compact.TabIndex = 27;
            this.cb_compact.Text = "Compact Mode";
            this.cb_compact.UseVisualStyleBackColor = true;
            // 
            // lb_elapsed
            // 
            this.lb_elapsed.AutoSize = true;
            this.lb_elapsed.Location = new System.Drawing.Point(717, 542);
            this.lb_elapsed.Name = "lb_elapsed";
            this.lb_elapsed.Size = new System.Drawing.Size(11, 12);
            this.lb_elapsed.TabIndex = 25;
            this.lb_elapsed.Text = "0";
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.Location = new System.Drawing.Point(664, 542);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(47, 12);
            this.label16.TabIndex = 24;
            this.label16.Text = "Elapsed: ";
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.tb_retry);
            this.groupBox4.Controls.Add(this.label23);
            this.groupBox4.Controls.Add(this.tb_threshold);
            this.groupBox4.Controls.Add(this.tb_maxq);
            this.groupBox4.Controls.Add(this.tb_minq);
            this.groupBox4.Controls.Add(this.tb_startq);
            this.groupBox4.Controls.Add(this.label22);
            this.groupBox4.Controls.Add(this.label15);
            this.groupBox4.Controls.Add(this.label17);
            this.groupBox4.Controls.Add(this.label19);
            this.groupBox4.Controls.Add(this.comboBox_algorithm);
            this.groupBox4.Controls.Add(this.comboBox_target);
            this.groupBox4.Controls.Add(this.comboBox_session);
            this.groupBox4.Controls.Add(this.btn_invset);
            this.groupBox4.Controls.Add(this.cb_compact);
            this.groupBox4.Controls.Add(this.label18);
            this.groupBox4.Controls.Add(this.label20);
            this.groupBox4.Controls.Add(this.label21);
            this.groupBox4.Location = new System.Drawing.Point(330, 344);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(439, 123);
            this.groupBox4.TabIndex = 28;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "Inventory Settings";
            // 
            // tb_retry
            // 
            this.tb_retry.Location = new System.Drawing.Point(63, 95);
            this.tb_retry.MaxLength = 1;
            this.tb_retry.Name = "tb_retry";
            this.tb_retry.Size = new System.Drawing.Size(39, 22);
            this.tb_retry.TabIndex = 42;
            this.tb_retry.Text = "0";
            // 
            // label23
            // 
            this.label23.AutoSize = true;
            this.label23.Location = new System.Drawing.Point(6, 99);
            this.label23.Name = "label23";
            this.label23.Size = new System.Drawing.Size(34, 12);
            this.label23.TabIndex = 41;
            this.label23.Text = "Retry:";
            // 
            // tb_threshold
            // 
            this.tb_threshold.Location = new System.Drawing.Point(219, 96);
            this.tb_threshold.MaxLength = 2;
            this.tb_threshold.Name = "tb_threshold";
            this.tb_threshold.Size = new System.Drawing.Size(39, 22);
            this.tb_threshold.TabIndex = 40;
            this.tb_threshold.Text = "4";
            // 
            // tb_maxq
            // 
            this.tb_maxq.Location = new System.Drawing.Point(219, 71);
            this.tb_maxq.MaxLength = 2;
            this.tb_maxq.Name = "tb_maxq";
            this.tb_maxq.Size = new System.Drawing.Size(39, 22);
            this.tb_maxq.TabIndex = 39;
            this.tb_maxq.Text = "15";
            // 
            // tb_minq
            // 
            this.tb_minq.Location = new System.Drawing.Point(219, 43);
            this.tb_minq.MaxLength = 2;
            this.tb_minq.Name = "tb_minq";
            this.tb_minq.Size = new System.Drawing.Size(39, 22);
            this.tb_minq.TabIndex = 38;
            this.tb_minq.Text = "0";
            // 
            // tb_startq
            // 
            this.tb_startq.Location = new System.Drawing.Point(219, 15);
            this.tb_startq.MaxLength = 2;
            this.tb_startq.Name = "tb_startq";
            this.tb_startq.Size = new System.Drawing.Size(39, 22);
            this.tb_startq.TabIndex = 25;
            this.tb_startq.Text = "4";
            // 
            // label22
            // 
            this.label22.AutoSize = true;
            this.label22.Location = new System.Drawing.Point(157, 99);
            this.label22.Name = "label22";
            this.label22.Size = new System.Drawing.Size(37, 12);
            this.label22.TabIndex = 37;
            this.label22.Text = "TMult:";
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Location = new System.Drawing.Point(157, 74);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(40, 12);
            this.label15.TabIndex = 33;
            this.label15.Text = "Max Q:";
            // 
            // label17
            // 
            this.label17.AutoSize = true;
            this.label17.Location = new System.Drawing.Point(157, 46);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(38, 12);
            this.label17.TabIndex = 32;
            this.label17.Text = "Min Q:";
            // 
            // label19
            // 
            this.label19.AutoSize = true;
            this.label19.Location = new System.Drawing.Point(157, 18);
            this.label19.Name = "label19";
            this.label19.Size = new System.Drawing.Size(40, 12);
            this.label19.TabIndex = 31;
            this.label19.Text = "Start Q:";
            // 
            // comboBox_algorithm
            // 
            this.comboBox_algorithm.FormattingEnabled = true;
            this.comboBox_algorithm.Items.AddRange(new object[] {
            "Fixed Q",
            "Dynamic Q"});
            this.comboBox_algorithm.Location = new System.Drawing.Point(63, 71);
            this.comboBox_algorithm.Name = "comboBox_algorithm";
            this.comboBox_algorithm.Size = new System.Drawing.Size(78, 20);
            this.comboBox_algorithm.TabIndex = 30;
            this.comboBox_algorithm.Text = "Fixed Q";
            // 
            // comboBox_target
            // 
            this.comboBox_target.FormattingEnabled = true;
            this.comboBox_target.Items.AddRange(new object[] {
            "Toggle",
            "A",
            "B"});
            this.comboBox_target.Location = new System.Drawing.Point(63, 43);
            this.comboBox_target.Name = "comboBox_target";
            this.comboBox_target.Size = new System.Drawing.Size(78, 20);
            this.comboBox_target.TabIndex = 29;
            this.comboBox_target.Text = "Toggle";
            // 
            // comboBox_session
            // 
            this.comboBox_session.FormattingEnabled = true;
            this.comboBox_session.Items.AddRange(new object[] {
            "S0",
            "S1",
            "S2",
            "S3"});
            this.comboBox_session.Location = new System.Drawing.Point(63, 15);
            this.comboBox_session.Name = "comboBox_session";
            this.comboBox_session.Size = new System.Drawing.Size(78, 20);
            this.comboBox_session.TabIndex = 28;
            this.comboBox_session.Text = "S0";
            // 
            // btn_invset
            // 
            this.btn_invset.Location = new System.Drawing.Point(328, 90);
            this.btn_invset.Name = "btn_invset";
            this.btn_invset.Size = new System.Drawing.Size(105, 27);
            this.btn_invset.TabIndex = 25;
            this.btn_invset.Text = "Set";
            this.btn_invset.UseVisualStyleBackColor = true;
            this.btn_invset.Click += new System.EventHandler(this.btn_invset_Click);
            // 
            // label18
            // 
            this.label18.AutoSize = true;
            this.label18.Location = new System.Drawing.Point(6, 74);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(56, 12);
            this.label18.TabIndex = 21;
            this.label18.Text = "Algorithm:";
            // 
            // label20
            // 
            this.label20.AutoSize = true;
            this.label20.Location = new System.Drawing.Point(6, 46);
            this.label20.Name = "label20";
            this.label20.Size = new System.Drawing.Size(38, 12);
            this.label20.TabIndex = 18;
            this.label20.Text = "Target:";
            // 
            // label21
            // 
            this.label21.AutoSize = true;
            this.label21.Location = new System.Drawing.Point(6, 18);
            this.label21.Name = "label21";
            this.label21.Size = new System.Drawing.Size(42, 12);
            this.label21.TabIndex = 0;
            this.label21.Text = "Session:";
            // 
            // RFIDForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(781, 625);
            this.Controls.Add(this.groupBox4);
            this.Controls.Add(this.lb_elapsed);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.label16);
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
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
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
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.TextBox tb_tx_on;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label lb_total;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox tb_delay;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.Button btn_settest;
        private System.Windows.Forms.CheckBox cb_compact;
        private System.Windows.Forms.Label lb_elapsed;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.Label label18;
        private System.Windows.Forms.Label label20;
        private System.Windows.Forms.Label label21;
        private System.Windows.Forms.Button btn_invset;
        private System.Windows.Forms.ComboBox comboBox_session;
        private System.Windows.Forms.ComboBox comboBox_target;
        private System.Windows.Forms.ComboBox comboBox_algorithm;
        private System.Windows.Forms.Label label22;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.Label label17;
        private System.Windows.Forms.Label label19;
        private System.Windows.Forms.TextBox tb_threshold;
        private System.Windows.Forms.TextBox tb_maxq;
        private System.Windows.Forms.TextBox tb_minq;
        private System.Windows.Forms.TextBox tb_startq;
        private System.Windows.Forms.TextBox tb_retry;
        private System.Windows.Forms.Label label23;
        private System.Windows.Forms.Label label25;
        private System.Windows.Forms.TextBox tb_cycledelay;
        private System.Windows.Forms.Label label26;
    }
}

