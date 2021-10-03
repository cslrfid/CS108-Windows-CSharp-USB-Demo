namespace CS108_PC_Client
{
    partial class MainForm
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
            this.GB_Connection = new System.Windows.Forms.GroupBox();
            this.btn_connect = new System.Windows.Forms.Button();
            this.cb_device = new System.Windows.Forms.ComboBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.btn_barcode = new System.Windows.Forms.Button();
            this.btn_rfid = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.tb_silab_version = new System.Windows.Forms.TextBox();
            this.tb_bt_version = new System.Windows.Forms.TextBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.btn_set_devicename = new System.Windows.Forms.Button();
            this.tb_devicename = new System.Windows.Forms.TextBox();
            this.btn_get_devicename = new System.Windows.Forms.Button();
            this.tb_trigger_state = new System.Windows.Forms.TextBox();
            this.btn_get_trigger = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.tb_batt_lvl = new System.Windows.Forms.TextBox();
            this.btn_get_battery = new System.Windows.Forms.Button();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.btn_silab_bootloader = new System.Windows.Forms.Button();
            this.btn_silab_image = new System.Windows.Forms.Button();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.btn_bt_bootloader = new System.Windows.Forms.Button();
            this.btn_bt_image = new System.Windows.Forms.Button();
            this.groupBox6 = new System.Windows.Forms.GroupBox();
            this.btn_rfid_upgrade = new System.Windows.Forms.Button();
            this.GB_Connection.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.groupBox6.SuspendLayout();
            this.SuspendLayout();
            // 
            // GB_Connection
            // 
            this.GB_Connection.Controls.Add(this.btn_connect);
            this.GB_Connection.Controls.Add(this.cb_device);
            this.GB_Connection.Location = new System.Drawing.Point(8, 11);
            this.GB_Connection.Name = "GB_Connection";
            this.GB_Connection.Size = new System.Drawing.Size(528, 79);
            this.GB_Connection.TabIndex = 0;
            this.GB_Connection.TabStop = false;
            this.GB_Connection.Text = "Connection";
            // 
            // btn_connect
            // 
            this.btn_connect.Location = new System.Drawing.Point(406, 46);
            this.btn_connect.Name = "btn_connect";
            this.btn_connect.Size = new System.Drawing.Size(107, 27);
            this.btn_connect.TabIndex = 1;
            this.btn_connect.Text = "Connect";
            this.btn_connect.UseVisualStyleBackColor = true;
            this.btn_connect.Click += new System.EventHandler(this.btn_connect_Click);
            // 
            // cb_device
            // 
            this.cb_device.FormattingEnabled = true;
            this.cb_device.Location = new System.Drawing.Point(10, 20);
            this.cb_device.Name = "cb_device";
            this.cb_device.Size = new System.Drawing.Size(503, 20);
            this.cb_device.TabIndex = 0;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.btn_barcode);
            this.groupBox2.Controls.Add(this.btn_rfid);
            this.groupBox2.Location = new System.Drawing.Point(8, 216);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(528, 60);
            this.groupBox2.TabIndex = 4;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Reader";
            // 
            // btn_barcode
            // 
            this.btn_barcode.Location = new System.Drawing.Point(134, 19);
            this.btn_barcode.Name = "btn_barcode";
            this.btn_barcode.Size = new System.Drawing.Size(121, 27);
            this.btn_barcode.TabIndex = 4;
            this.btn_barcode.Text = "Barcode";
            this.btn_barcode.UseVisualStyleBackColor = true;
            this.btn_barcode.Click += new System.EventHandler(this.btn_barcode_Click);
            // 
            // btn_rfid
            // 
            this.btn_rfid.Location = new System.Drawing.Point(10, 19);
            this.btn_rfid.Name = "btn_rfid";
            this.btn_rfid.Size = new System.Drawing.Size(118, 27);
            this.btn_rfid.TabIndex = 3;
            this.btn_rfid.Text = "RFID";
            this.btn_rfid.UseVisualStyleBackColor = true;
            this.btn_rfid.Click += new System.EventHandler(this.btn_rfid_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 18);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(147, 12);
            this.label1.TabIndex = 5;
            this.label1.Text = "Silicon Lab Firmware Version:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(234, 18);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(140, 12);
            this.label2.TabIndex = 6;
            this.label2.Text = "Bluetooth Firmware Version:";
            // 
            // tb_silab_version
            // 
            this.tb_silab_version.Location = new System.Drawing.Point(159, 15);
            this.tb_silab_version.Name = "tb_silab_version";
            this.tb_silab_version.ReadOnly = true;
            this.tb_silab_version.Size = new System.Drawing.Size(49, 22);
            this.tb_silab_version.TabIndex = 7;
            // 
            // tb_bt_version
            // 
            this.tb_bt_version.Location = new System.Drawing.Point(380, 15);
            this.tb_bt_version.Name = "tb_bt_version";
            this.tb_bt_version.ReadOnly = true;
            this.tb_bt_version.Size = new System.Drawing.Size(49, 22);
            this.tb_bt_version.TabIndex = 8;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.btn_set_devicename);
            this.groupBox1.Controls.Add(this.tb_devicename);
            this.groupBox1.Controls.Add(this.btn_get_devicename);
            this.groupBox1.Controls.Add(this.tb_trigger_state);
            this.groupBox1.Controls.Add(this.btn_get_trigger);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.tb_batt_lvl);
            this.groupBox1.Controls.Add(this.btn_get_battery);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.tb_bt_version);
            this.groupBox1.Controls.Add(this.tb_silab_version);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Location = new System.Drawing.Point(8, 96);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(528, 114);
            this.groupBox1.TabIndex = 9;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Info";
            // 
            // btn_set_devicename
            // 
            this.btn_set_devicename.Location = new System.Drawing.Point(380, 78);
            this.btn_set_devicename.Name = "btn_set_devicename";
            this.btn_set_devicename.Size = new System.Drawing.Size(107, 27);
            this.btn_set_devicename.TabIndex = 15;
            this.btn_set_devicename.Text = "Set Device Name";
            this.btn_set_devicename.UseVisualStyleBackColor = true;
            this.btn_set_devicename.Click += new System.EventHandler(this.btn_set_devicename_Click);
            // 
            // tb_devicename
            // 
            this.tb_devicename.Location = new System.Drawing.Point(159, 82);
            this.tb_devicename.MaxLength = 20;
            this.tb_devicename.Name = "tb_devicename";
            this.tb_devicename.Size = new System.Drawing.Size(215, 22);
            this.tb_devicename.TabIndex = 14;
            // 
            // btn_get_devicename
            // 
            this.btn_get_devicename.Location = new System.Drawing.Point(46, 78);
            this.btn_get_devicename.Name = "btn_get_devicename";
            this.btn_get_devicename.Size = new System.Drawing.Size(107, 27);
            this.btn_get_devicename.TabIndex = 13;
            this.btn_get_devicename.Text = "Get Device Name";
            this.btn_get_devicename.UseVisualStyleBackColor = true;
            this.btn_get_devicename.Click += new System.EventHandler(this.btn_get_devicename_Click);
            // 
            // tb_trigger_state
            // 
            this.tb_trigger_state.Location = new System.Drawing.Point(380, 49);
            this.tb_trigger_state.Name = "tb_trigger_state";
            this.tb_trigger_state.ReadOnly = true;
            this.tb_trigger_state.Size = new System.Drawing.Size(49, 22);
            this.tb_trigger_state.TabIndex = 12;
            // 
            // btn_get_trigger
            // 
            this.btn_get_trigger.Location = new System.Drawing.Point(267, 49);
            this.btn_get_trigger.Name = "btn_get_trigger";
            this.btn_get_trigger.Size = new System.Drawing.Size(107, 27);
            this.btn_get_trigger.TabIndex = 11;
            this.btn_get_trigger.Text = "Get Trigger State";
            this.btn_get_trigger.UseVisualStyleBackColor = true;
            this.btn_get_trigger.Click += new System.EventHandler(this.btn_get_trigger_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(214, 59);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(22, 12);
            this.label3.TabIndex = 10;
            this.label3.Text = "mV";
            // 
            // tb_batt_lvl
            // 
            this.tb_batt_lvl.Location = new System.Drawing.Point(159, 49);
            this.tb_batt_lvl.Name = "tb_batt_lvl";
            this.tb_batt_lvl.ReadOnly = true;
            this.tb_batt_lvl.Size = new System.Drawing.Size(49, 22);
            this.tb_batt_lvl.TabIndex = 9;
            // 
            // btn_get_battery
            // 
            this.btn_get_battery.Location = new System.Drawing.Point(46, 45);
            this.btn_get_battery.Name = "btn_get_battery";
            this.btn_get_battery.Size = new System.Drawing.Size(107, 27);
            this.btn_get_battery.TabIndex = 2;
            this.btn_get_battery.Text = "Get Battery Level";
            this.btn_get_battery.UseVisualStyleBackColor = true;
            this.btn_get_battery.Click += new System.EventHandler(this.btn_get_battery_Click);
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.btn_silab_bootloader);
            this.groupBox3.Controls.Add(this.btn_silab_image);
            this.groupBox3.Location = new System.Drawing.Point(8, 282);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(260, 60);
            this.groupBox3.TabIndex = 10;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Silicon Lab Firmware Upgrade";
            // 
            // btn_silab_bootloader
            // 
            this.btn_silab_bootloader.Location = new System.Drawing.Point(134, 21);
            this.btn_silab_bootloader.Name = "btn_silab_bootloader";
            this.btn_silab_bootloader.Size = new System.Drawing.Size(121, 27);
            this.btn_silab_bootloader.TabIndex = 12;
            this.btn_silab_bootloader.Text = "Upgrade Bootloader";
            this.btn_silab_bootloader.UseVisualStyleBackColor = true;
            this.btn_silab_bootloader.Click += new System.EventHandler(this.btn_silab_bootloader_Click);
            // 
            // btn_silab_image
            // 
            this.btn_silab_image.Location = new System.Drawing.Point(7, 21);
            this.btn_silab_image.Name = "btn_silab_image";
            this.btn_silab_image.Size = new System.Drawing.Size(121, 27);
            this.btn_silab_image.TabIndex = 11;
            this.btn_silab_image.Text = "Upgrade Image";
            this.btn_silab_image.UseVisualStyleBackColor = true;
            this.btn_silab_image.Click += new System.EventHandler(this.btn_silab_image_Click);
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.btn_bt_bootloader);
            this.groupBox4.Controls.Add(this.btn_bt_image);
            this.groupBox4.Location = new System.Drawing.Point(274, 282);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(260, 60);
            this.groupBox4.TabIndex = 11;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "Bluetooth Firmware Upgrade";
            // 
            // btn_bt_bootloader
            // 
            this.btn_bt_bootloader.Location = new System.Drawing.Point(134, 21);
            this.btn_bt_bootloader.Name = "btn_bt_bootloader";
            this.btn_bt_bootloader.Size = new System.Drawing.Size(121, 27);
            this.btn_bt_bootloader.TabIndex = 13;
            this.btn_bt_bootloader.Text = "Upgrade Bootloader";
            this.btn_bt_bootloader.UseVisualStyleBackColor = true;
            this.btn_bt_bootloader.Click += new System.EventHandler(this.btn_bt_bootloader_Click);
            // 
            // btn_bt_image
            // 
            this.btn_bt_image.Location = new System.Drawing.Point(7, 21);
            this.btn_bt_image.Name = "btn_bt_image";
            this.btn_bt_image.Size = new System.Drawing.Size(121, 27);
            this.btn_bt_image.TabIndex = 13;
            this.btn_bt_image.Text = "Upgrade Image";
            this.btn_bt_image.UseVisualStyleBackColor = true;
            this.btn_bt_image.Click += new System.EventHandler(this.btn_bt_image_Click);
            // 
            // groupBox6
            // 
            this.groupBox6.Controls.Add(this.btn_rfid_upgrade);
            this.groupBox6.Location = new System.Drawing.Point(8, 348);
            this.groupBox6.Name = "groupBox6";
            this.groupBox6.Size = new System.Drawing.Size(260, 60);
            this.groupBox6.TabIndex = 14;
            this.groupBox6.TabStop = false;
            this.groupBox6.Text = "RFID Firmware Upgrade";
            // 
            // btn_rfid_upgrade
            // 
            this.btn_rfid_upgrade.Location = new System.Drawing.Point(7, 21);
            this.btn_rfid_upgrade.Name = "btn_rfid_upgrade";
            this.btn_rfid_upgrade.Size = new System.Drawing.Size(121, 27);
            this.btn_rfid_upgrade.TabIndex = 13;
            this.btn_rfid_upgrade.Text = "Upgrade";
            this.btn_rfid_upgrade.UseVisualStyleBackColor = true;
            this.btn_rfid_upgrade.Click += new System.EventHandler(this.btn_rfid_upgrade_Click);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(542, 418);
            this.Controls.Add(this.groupBox6);
            this.Controls.Add(this.groupBox4);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.GB_Connection);
            this.Name = "MainForm";
            this.Text = "CS108 PC Demo App 1.3";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Main_FormClosing);
            this.GB_Connection.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox4.ResumeLayout(false);
            this.groupBox6.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox GB_Connection;
        private System.Windows.Forms.ComboBox cb_device;
        private System.Windows.Forms.Button btn_connect;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Button btn_barcode;
        private System.Windows.Forms.Button btn_rfid;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox tb_silab_version;
        private System.Windows.Forms.TextBox tb_bt_version;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.Button btn_silab_bootloader;
        private System.Windows.Forms.Button btn_silab_image;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.Button btn_bt_image;
        private System.Windows.Forms.Button btn_bt_bootloader;
        private System.Windows.Forms.TextBox tb_batt_lvl;
        private System.Windows.Forms.Button btn_get_battery;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox tb_trigger_state;
        private System.Windows.Forms.Button btn_get_trigger;
        private System.Windows.Forms.TextBox tb_devicename;
        private System.Windows.Forms.Button btn_get_devicename;
        private System.Windows.Forms.Button btn_set_devicename;
        private System.Windows.Forms.GroupBox groupBox6;
        private System.Windows.Forms.Button btn_rfid_upgrade;
    }
}

