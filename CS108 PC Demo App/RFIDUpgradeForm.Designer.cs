namespace CS108_PC_Client
{
    partial class RFIDUpgradeForm
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
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label2 = new System.Windows.Forms.Label();
            this.lb_total = new System.Windows.Forms.Label();
            this.lb_block = new System.Windows.Forms.Label();
            this.progressBar = new System.Windows.Forms.ProgressBar();
            this.btn_clear_info = new System.Windows.Forms.Button();
            this.btn_rfid_bootloader = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.btn_rfid_image = new System.Windows.Forms.Button();
            this.tb_info = new System.Windows.Forms.TextBox();
            this.btn_rfid_poweroff = new System.Windows.Forms.Button();
            this.btn_rfid_poweron = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.lb_total);
            this.groupBox1.Controls.Add(this.lb_block);
            this.groupBox1.Controls.Add(this.progressBar);
            this.groupBox1.Controls.Add(this.btn_clear_info);
            this.groupBox1.Controls.Add(this.btn_rfid_bootloader);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.btn_rfid_image);
            this.groupBox1.Controls.Add(this.tb_info);
            this.groupBox1.Controls.Add(this.btn_rfid_poweroff);
            this.groupBox1.Controls.Add(this.btn_rfid_poweron);
            this.groupBox1.Location = new System.Drawing.Point(12, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(593, 326);
            this.groupBox1.TabIndex = 4;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Upgrade";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(539, 274);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(8, 12);
            this.label2.TabIndex = 22;
            this.label2.Text = "/";
            // 
            // lb_total
            // 
            this.lb_total.AutoSize = true;
            this.lb_total.Location = new System.Drawing.Point(553, 274);
            this.lb_total.Name = "lb_total";
            this.lb_total.Size = new System.Drawing.Size(11, 12);
            this.lb_total.TabIndex = 21;
            this.lb_total.Text = "0";
            // 
            // lb_block
            // 
            this.lb_block.AutoSize = true;
            this.lb_block.Location = new System.Drawing.Point(501, 274);
            this.lb_block.Name = "lb_block";
            this.lb_block.Size = new System.Drawing.Size(11, 12);
            this.lb_block.TabIndex = 20;
            this.lb_block.Text = "0";
            // 
            // progressBar
            // 
            this.progressBar.Location = new System.Drawing.Point(6, 264);
            this.progressBar.Name = "progressBar";
            this.progressBar.Size = new System.Drawing.Size(489, 23);
            this.progressBar.Step = 1;
            this.progressBar.TabIndex = 19;
            // 
            // btn_clear_info
            // 
            this.btn_clear_info.Location = new System.Drawing.Point(478, 293);
            this.btn_clear_info.Name = "btn_clear_info";
            this.btn_clear_info.Size = new System.Drawing.Size(105, 27);
            this.btn_clear_info.TabIndex = 18;
            this.btn_clear_info.Text = "Clear";
            this.btn_clear_info.UseVisualStyleBackColor = true;
            this.btn_clear_info.Click += new System.EventHandler(this.btn_clear_info_Click);
            // 
            // btn_rfid_bootloader
            // 
            this.btn_rfid_bootloader.Location = new System.Drawing.Point(355, 293);
            this.btn_rfid_bootloader.Name = "btn_rfid_bootloader";
            this.btn_rfid_bootloader.Size = new System.Drawing.Size(117, 27);
            this.btn_rfid_bootloader.TabIndex = 16;
            this.btn_rfid_bootloader.Text = "Upgrade Bootloader";
            this.btn_rfid_bootloader.UseVisualStyleBackColor = true;
            this.btn_rfid_bootloader.Click += new System.EventHandler(this.btn_rfid_bootloader_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 18);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(28, 12);
            this.label1.TabIndex = 6;
            this.label1.Text = "Info:";
            // 
            // btn_rfid_image
            // 
            this.btn_rfid_image.Location = new System.Drawing.Point(230, 293);
            this.btn_rfid_image.Name = "btn_rfid_image";
            this.btn_rfid_image.Size = new System.Drawing.Size(119, 27);
            this.btn_rfid_image.TabIndex = 15;
            this.btn_rfid_image.Text = "Upgrade Image";
            this.btn_rfid_image.UseVisualStyleBackColor = true;
            this.btn_rfid_image.Click += new System.EventHandler(this.btn_rfid_image_Click);
            // 
            // tb_info
            // 
            this.tb_info.Location = new System.Drawing.Point(6, 33);
            this.tb_info.Multiline = true;
            this.tb_info.Name = "tb_info";
            this.tb_info.ReadOnly = true;
            this.tb_info.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.tb_info.Size = new System.Drawing.Size(577, 225);
            this.tb_info.TabIndex = 5;
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
            // RFIDUpgradeForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(616, 346);
            this.Controls.Add(this.groupBox1);
            this.Name = "RFIDUpgradeForm";
            this.Text = "RFID Upgrade";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Main_FormClosing);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button btn_rfid_poweroff;
        private System.Windows.Forms.Button btn_rfid_poweron;
        private System.Windows.Forms.TextBox tb_info;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btn_rfid_image;
        private System.Windows.Forms.Button btn_rfid_bootloader;
        //private System.Windows.Forms.ListView lv_tag;
        private System.Windows.Forms.Button btn_clear_info;
        private System.Windows.Forms.ProgressBar progressBar;
        private System.Windows.Forms.Label lb_total;
        private System.Windows.Forms.Label lb_block;
        private System.Windows.Forms.Label label2;
    }
}

