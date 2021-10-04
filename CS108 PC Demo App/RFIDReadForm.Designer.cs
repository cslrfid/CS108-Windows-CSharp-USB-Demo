namespace CS108_PC_Client
{
    partial class RFIDReadForm
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
            this.label13 = new System.Windows.Forms.Label();
            this.btn_blockwrite = new System.Windows.Forms.Button();
            this.lb_bits = new System.Windows.Forms.Label();
            this.bits = new System.Windows.Forms.Label();
            this.tb_data = new System.Windows.Forms.TextBox();
            this.label12 = new System.Windows.Forms.Label();
            this.tb_acc_pwd = new System.Windows.Forms.TextBox();
            this.label11 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.tb_result = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.tb_count = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.tb_ptr = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.tb_bank = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.tb_epc = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.btn_read = new System.Windows.Forms.Button();
            this.tb_info = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.btn_clear_info = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.label13);
            this.groupBox1.Controls.Add(this.btn_blockwrite);
            this.groupBox1.Controls.Add(this.lb_bits);
            this.groupBox1.Controls.Add(this.bits);
            this.groupBox1.Controls.Add(this.tb_data);
            this.groupBox1.Controls.Add(this.label12);
            this.groupBox1.Controls.Add(this.tb_acc_pwd);
            this.groupBox1.Controls.Add(this.label11);
            this.groupBox1.Controls.Add(this.label10);
            this.groupBox1.Controls.Add(this.tb_result);
            this.groupBox1.Controls.Add(this.label8);
            this.groupBox1.Controls.Add(this.tb_count);
            this.groupBox1.Controls.Add(this.label9);
            this.groupBox1.Controls.Add(this.label7);
            this.groupBox1.Controls.Add(this.tb_ptr);
            this.groupBox1.Controls.Add(this.label6);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.tb_bank);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.tb_epc);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.btn_read);
            this.groupBox1.Location = new System.Drawing.Point(12, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(529, 417);
            this.groupBox1.TabIndex = 4;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Read / Write";
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(188, 105);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(188, 12);
            this.label13.TabIndex = 44;
            this.label13.Text = "(Max 512 for read; 512 for blockwrite)";
            // 
            // btn_blockwrite
            // 
            this.btn_blockwrite.Location = new System.Drawing.Point(119, 290);
            this.btn_blockwrite.Name = "btn_blockwrite";
            this.btn_blockwrite.Size = new System.Drawing.Size(105, 27);
            this.btn_blockwrite.TabIndex = 43;
            this.btn_blockwrite.Text = "BlockWrite";
            this.btn_blockwrite.UseVisualStyleBackColor = true;
            this.btn_blockwrite.Click += new System.EventHandler(this.btn_blockwrite_Click);
            // 
            // lb_bits
            // 
            this.lb_bits.AutoSize = true;
            this.lb_bits.Location = new System.Drawing.Point(476, 290);
            this.lb_bits.Name = "lb_bits";
            this.lb_bits.Size = new System.Drawing.Size(11, 12);
            this.lb_bits.TabIndex = 42;
            this.lb_bits.Text = "0";
            // 
            // bits
            // 
            this.bits.AutoSize = true;
            this.bits.Location = new System.Drawing.Point(502, 290);
            this.bits.Name = "bits";
            this.bits.Size = new System.Drawing.Size(21, 12);
            this.bits.TabIndex = 41;
            this.bits.Text = "bits";
            // 
            // tb_data
            // 
            this.tb_data.Location = new System.Drawing.Point(82, 160);
            this.tb_data.MaxLength = 2048;
            this.tb_data.Multiline = true;
            this.tb_data.Name = "tb_data";
            this.tb_data.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.tb_data.Size = new System.Drawing.Size(441, 124);
            this.tb_data.TabIndex = 40;
            this.tb_data.TextChanged += new System.EventHandler(this.tb_data_TextChanged);
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(6, 163);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(56, 12);
            this.label12.TabIndex = 39;
            this.label12.Text = "Write data:";
            // 
            // tb_acc_pwd
            // 
            this.tb_acc_pwd.Location = new System.Drawing.Point(82, 130);
            this.tb_acc_pwd.MaxLength = 8;
            this.tb_acc_pwd.Name = "tb_acc_pwd";
            this.tb_acc_pwd.Size = new System.Drawing.Size(60, 22);
            this.tb_acc_pwd.TabIndex = 38;
            this.tb_acc_pwd.Text = "00000000";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(6, 133);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(62, 12);
            this.label11.TabIndex = 37;
            this.label11.Text = "Access pwd:";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(6, 325);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(37, 12);
            this.label10.TabIndex = 19;
            this.label10.Text = "Result:";
            // 
            // tb_result
            // 
            this.tb_result.Location = new System.Drawing.Point(6, 340);
            this.tb_result.Multiline = true;
            this.tb_result.Name = "tb_result";
            this.tb_result.ReadOnly = true;
            this.tb_result.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.tb_result.Size = new System.Drawing.Size(517, 71);
            this.tb_result.TabIndex = 19;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(119, 105);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(63, 12);
            this.label8.TabIndex = 36;
            this.label8.Text = "16 bit words";
            // 
            // tb_count
            // 
            this.tb_count.Location = new System.Drawing.Point(82, 102);
            this.tb_count.MaxLength = 3;
            this.tb_count.Name = "tb_count";
            this.tb_count.Size = new System.Drawing.Size(31, 22);
            this.tb_count.TabIndex = 35;
            this.tb_count.Text = "2";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(6, 105);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(41, 12);
            this.label9.TabIndex = 34;
            this.label9.Text = "Length:";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(119, 77);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(63, 12);
            this.label7.TabIndex = 33;
            this.label7.Text = "16 bit words";
            // 
            // tb_ptr
            // 
            this.tb_ptr.Location = new System.Drawing.Point(82, 74);
            this.tb_ptr.MaxLength = 3;
            this.tb_ptr.Name = "tb_ptr";
            this.tb_ptr.Size = new System.Drawing.Size(31, 22);
            this.tb_ptr.TabIndex = 32;
            this.tb_ptr.Text = "0";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(6, 77);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(36, 12);
            this.label6.TabIndex = 31;
            this.label6.Text = "Offset:";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(119, 49);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(199, 12);
            this.label5.TabIndex = 30;
            this.label5.Text = "0 = Reserved; 1 = EPC; 2 = TID; 3 = User";
            // 
            // tb_bank
            // 
            this.tb_bank.Location = new System.Drawing.Point(82, 46);
            this.tb_bank.MaxLength = 1;
            this.tb_bank.Name = "tb_bank";
            this.tb_bank.Size = new System.Drawing.Size(31, 22);
            this.tb_bank.TabIndex = 29;
            this.tb_bank.Text = "2";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(6, 49);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(33, 12);
            this.label4.TabIndex = 28;
            this.label4.Text = "Bank:";
            // 
            // tb_epc
            // 
            this.tb_epc.Location = new System.Drawing.Point(82, 15);
            this.tb_epc.Name = "tb_epc";
            this.tb_epc.Size = new System.Drawing.Size(441, 22);
            this.tb_epc.TabIndex = 27;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(6, 18);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(70, 12);
            this.label3.TabIndex = 26;
            this.label3.Text = "Selected EPC:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(6, 18);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(70, 12);
            this.label2.TabIndex = 26;
            this.label2.Text = "Selected EPC:";
            // 
            // btn_read
            // 
            this.btn_read.Location = new System.Drawing.Point(8, 290);
            this.btn_read.Name = "btn_read";
            this.btn_read.Size = new System.Drawing.Size(105, 27);
            this.btn_read.TabIndex = 24;
            this.btn_read.Text = "Read";
            this.btn_read.UseVisualStyleBackColor = true;
            this.btn_read.Click += new System.EventHandler(this.btn_read_Click);
            // 
            // tb_info
            // 
            this.tb_info.Location = new System.Drawing.Point(12, 447);
            this.tb_info.Multiline = true;
            this.tb_info.Name = "tb_info";
            this.tb_info.ReadOnly = true;
            this.tb_info.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.tb_info.Size = new System.Drawing.Size(417, 71);
            this.tb_info.TabIndex = 5;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 432);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(28, 12);
            this.label1.TabIndex = 6;
            this.label1.Text = "Info:";
            // 
            // btn_clear_info
            // 
            this.btn_clear_info.Location = new System.Drawing.Point(435, 494);
            this.btn_clear_info.Name = "btn_clear_info";
            this.btn_clear_info.Size = new System.Drawing.Size(105, 27);
            this.btn_clear_info.TabIndex = 18;
            this.btn_clear_info.Text = "Clear";
            this.btn_clear_info.UseVisualStyleBackColor = true;
            this.btn_clear_info.Click += new System.EventHandler(this.btn_clear_info_Click);
            // 
            // RFIDReadForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(553, 530);
            this.Controls.Add(this.btn_clear_info);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.tb_info);
            this.Controls.Add(this.groupBox1);
            this.Name = "RFIDReadForm";
            this.Text = "RFID Read/Write";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Main_FormClosing);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.TextBox tb_info;
        private System.Windows.Forms.Label label1;
        //private System.Windows.Forms.ListView lv_tag;
        private System.Windows.Forms.Button btn_clear_info;
        private System.Windows.Forms.Button btn_read;
        private System.Windows.Forms.TextBox tb_epc;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox tb_count;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox tb_ptr;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox tb_bank;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox tb_result;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.TextBox tb_acc_pwd;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.TextBox tb_data;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label lb_bits;
        private System.Windows.Forms.Label bits;
        private System.Windows.Forms.Button btn_blockwrite;
        private System.Windows.Forms.Label label13;
    }
}

