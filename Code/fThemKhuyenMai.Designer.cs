namespace QL_Bida
{
    partial class fThemKhuyenMai
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            label1 = new Label();
            panel1 = new Panel();
            btnLuu = new Button();
            btnHuyBo = new Button();
            btnGiup = new Button();
            groupBox1 = new GroupBox();
            dtThoiGianEnd = new DateTimePicker();
            dtThoiGianStart = new DateTimePicker();
            label9 = new Label();
            label7 = new Label();
            panel4 = new Panel();
            txtMoTa = new TextBox();
            panel3 = new Panel();
            txtTenKM = new TextBox();
            panel5 = new Panel();
            txtGiaTri = new TextBox();
            label12 = new Label();
            label5 = new Label();
            label6 = new Label();
            label11 = new Label();
            label4 = new Label();
            pictureBox2 = new PictureBox();
            panel1.SuspendLayout();
            groupBox1.SuspendLayout();
            panel4.SuspendLayout();
            panel3.SuspendLayout();
            panel5.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBox2).BeginInit();
            SuspendLayout();
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
            label1.ForeColor = SystemColors.Control;
            label1.Location = new Point(7, 14);
            label1.Margin = new Padding(10, 20, 3, 0);
            label1.Name = "label1";
            label1.Size = new Size(181, 28);
            label1.TabIndex = 2;
            label1.Text = "Thêm khuyến mãi";
            // 
            // panel1
            // 
            panel1.BackColor = Color.White;
            panel1.Controls.Add(btnLuu);
            panel1.Controls.Add(btnHuyBo);
            panel1.Controls.Add(btnGiup);
            panel1.Controls.Add(groupBox1);
            panel1.Controls.Add(panel4);
            panel1.Controls.Add(panel3);
            panel1.Controls.Add(panel5);
            panel1.Controls.Add(label12);
            panel1.Controls.Add(label5);
            panel1.Controls.Add(label6);
            panel1.Controls.Add(label11);
            panel1.Controls.Add(label4);
            panel1.Location = new Point(7, 67);
            panel1.Margin = new Padding(3, 25, 3, 3);
            panel1.Name = "panel1";
            panel1.Size = new Size(1443, 359);
            panel1.TabIndex = 3;
            // 
            // btnLuu
            // 
            btnLuu.Font = new Font("Segoe UI Semibold", 12F, FontStyle.Bold);
            btnLuu.Image = Properties.Resources.save_24;
            btnLuu.ImageAlign = ContentAlignment.MiddleLeft;
            btnLuu.Location = new Point(1051, 284);
            btnLuu.Margin = new Padding(3, 3, 3, 7);
            btnLuu.Name = "btnLuu";
            btnLuu.Padding = new Padding(18, 0, 0, 0);
            btnLuu.Size = new Size(116, 59);
            btnLuu.TabIndex = 10;
            btnLuu.Text = "Lưu";
            btnLuu.UseCompatibleTextRendering = true;
            btnLuu.UseVisualStyleBackColor = true;
            btnLuu.Click += btnLuu_Click;
            // 
            // btnHuyBo
            // 
            btnHuyBo.Font = new Font("Segoe UI Semibold", 12F, FontStyle.Bold);
            btnHuyBo.Image = Properties.Resources.cancel_24;
            btnHuyBo.ImageAlign = ContentAlignment.MiddleLeft;
            btnHuyBo.Location = new Point(1173, 283);
            btnHuyBo.Margin = new Padding(3, 3, 3, 7);
            btnHuyBo.Name = "btnHuyBo";
            btnHuyBo.Padding = new Padding(18, 0, 0, 0);
            btnHuyBo.Size = new Size(133, 60);
            btnHuyBo.TabIndex = 11;
            btnHuyBo.Text = "Huỷ bỏ";
            btnHuyBo.TextAlign = ContentAlignment.MiddleRight;
            btnHuyBo.UseCompatibleTextRendering = true;
            btnHuyBo.UseVisualStyleBackColor = true;
            btnHuyBo.Click += btnHuyBo_Click;
            // 
            // btnGiup
            // 
            btnGiup.Font = new Font("Segoe UI Semibold", 12F, FontStyle.Bold);
            btnGiup.Image = Properties.Resources.help_24;
            btnGiup.ImageAlign = ContentAlignment.MiddleLeft;
            btnGiup.Location = new Point(1311, 284);
            btnGiup.Margin = new Padding(9, 3, 7, 7);
            btnGiup.Name = "btnGiup";
            btnGiup.Padding = new Padding(6, 0, 0, 0);
            btnGiup.Size = new Size(116, 59);
            btnGiup.TabIndex = 12;
            btnGiup.Text = "Giúp";
            btnGiup.TextImageRelation = TextImageRelation.ImageBeforeText;
            btnGiup.UseCompatibleTextRendering = true;
            btnGiup.UseVisualStyleBackColor = true;
            // 
            // groupBox1
            // 
            groupBox1.Controls.Add(dtThoiGianEnd);
            groupBox1.Controls.Add(dtThoiGianStart);
            groupBox1.Controls.Add(label9);
            groupBox1.Controls.Add(label7);
            groupBox1.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            groupBox1.Location = new Point(813, 13);
            groupBox1.Name = "groupBox1";
            groupBox1.Size = new Size(627, 112);
            groupBox1.TabIndex = 6;
            groupBox1.TabStop = false;
            groupBox1.Text = "Thời gian áp dụng";
            // 
            // dtThoiGianEnd
            // 
            dtThoiGianEnd.CustomFormat = "  dd/MM/yyyy";
            dtThoiGianEnd.Font = new Font("Segoe UI", 19F, FontStyle.Regular, GraphicsUnit.Point, 0);
            dtThoiGianEnd.Format = DateTimePickerFormat.Custom;
            dtThoiGianEnd.Location = new Point(373, 46);
            dtThoiGianEnd.Margin = new Padding(3, 3, 12, 3);
            dtThoiGianEnd.Name = "dtThoiGianEnd";
            dtThoiGianEnd.Size = new Size(242, 50);
            dtThoiGianEnd.TabIndex = 3;
            // 
            // dtThoiGianStart
            // 
            dtThoiGianStart.CustomFormat = "  dd/MM/yyyy";
            dtThoiGianStart.Font = new Font("Segoe UI", 19F, FontStyle.Regular, GraphicsUnit.Point, 0);
            dtThoiGianStart.Format = DateTimePickerFormat.Custom;
            dtThoiGianStart.Location = new Point(55, 46);
            dtThoiGianStart.Name = "dtThoiGianStart";
            dtThoiGianStart.Size = new Size(242, 50);
            dtThoiGianStart.TabIndex = 3;
            // 
            // label9
            // 
            label9.AutoSize = true;
            label9.Font = new Font("Segoe UI Semibold", 13.5F, FontStyle.Bold);
            label9.Location = new Point(311, 59);
            label9.Name = "label9";
            label9.Size = new Size(56, 31);
            label9.TabIndex = 2;
            label9.Text = "Đến";
            // 
            // label7
            // 
            label7.AutoSize = true;
            label7.Font = new Font("Segoe UI Semibold", 13.5F, FontStyle.Bold);
            label7.Location = new Point(10, 59);
            label7.Name = "label7";
            label7.Size = new Size(41, 31);
            label7.TabIndex = 2;
            label7.Text = "Từ";
            // 
            // panel4
            // 
            panel4.BackColor = Color.Silver;
            panel4.Controls.Add(txtMoTa);
            panel4.Location = new Point(152, 137);
            panel4.Margin = new Padding(146, 24, 19, 3);
            panel4.Name = "panel4";
            panel4.Size = new Size(655, 66);
            panel4.TabIndex = 5;
            // 
            // txtMoTa
            // 
            txtMoTa.BackColor = Color.Silver;
            txtMoTa.BorderStyle = BorderStyle.None;
            txtMoTa.Font = new Font("Segoe UI", 26F);
            txtMoTa.Location = new Point(16, 5);
            txtMoTa.Name = "txtMoTa";
            txtMoTa.Size = new Size(619, 58);
            txtMoTa.TabIndex = 2;
            // 
            // panel3
            // 
            panel3.BackColor = Color.Silver;
            panel3.Controls.Add(txtTenKM);
            panel3.Location = new Point(152, 13);
            panel3.Margin = new Padding(146, 15, 19, 3);
            panel3.Name = "panel3";
            panel3.Size = new Size(655, 66);
            panel3.TabIndex = 5;
            // 
            // txtTenKM
            // 
            txtTenKM.BackColor = Color.Silver;
            txtTenKM.BorderStyle = BorderStyle.None;
            txtTenKM.Font = new Font("Segoe UI", 26F);
            txtTenKM.Location = new Point(16, 4);
            txtTenKM.Name = "txtTenKM";
            txtTenKM.Size = new Size(619, 58);
            txtTenKM.TabIndex = 2;
            // 
            // panel5
            // 
            panel5.BackColor = Color.Silver;
            panel5.Controls.Add(txtGiaTri);
            panel5.Location = new Point(1109, 142);
            panel5.Margin = new Padding(146, 3, 19, 3);
            panel5.Name = "panel5";
            panel5.Size = new Size(315, 52);
            panel5.TabIndex = 5;
            // 
            // txtGiaTri
            // 
            txtGiaTri.BackColor = Color.Silver;
            txtGiaTri.BorderStyle = BorderStyle.None;
            txtGiaTri.Font = new Font("Segoe UI", 20F);
            txtGiaTri.Location = new Point(16, 4);
            txtGiaTri.Name = "txtGiaTri";
            txtGiaTri.Size = new Size(282, 45);
            txtGiaTri.TabIndex = 2;
            // 
            // label12
            // 
            label12.AutoSize = true;
            label12.Font = new Font("Segoe UI", 12F);
            label12.ForeColor = Color.Red;
            label12.Location = new Point(1074, 158);
            label12.Name = "label12";
            label12.Size = new Size(32, 28);
            label12.TabIndex = 3;
            label12.Text = "(*)";
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Font = new Font("Segoe UI", 12F);
            label5.ForeColor = Color.Red;
            label5.Location = new Point(106, 30);
            label5.Name = "label5";
            label5.Size = new Size(32, 28);
            label5.TabIndex = 3;
            label5.Text = "(*)";
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Font = new Font("Segoe UI Semibold", 13.5F, FontStyle.Bold);
            label6.Location = new Point(21, 155);
            label6.Name = "label6";
            label6.Size = new Size(75, 31);
            label6.TabIndex = 2;
            label6.Text = "Mô tả";
            // 
            // label11
            // 
            label11.AutoSize = true;
            label11.Font = new Font("Segoe UI Semibold", 13.5F, FontStyle.Bold);
            label11.Location = new Point(825, 155);
            label11.Name = "label11";
            label11.Size = new Size(243, 31);
            label11.TabIndex = 2;
            label11.Text = "Giá trị khuyến mãi (%)";
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Font = new Font("Segoe UI Semibold", 13.5F, FontStyle.Bold);
            label4.Location = new Point(21, 30);
            label4.Name = "label4";
            label4.Size = new Size(91, 31);
            label4.TabIndex = 2;
            label4.Text = "Tên KM";
            // 
            // pictureBox2
            // 
            pictureBox2.Location = new Point(1405, 12);
            pictureBox2.Name = "pictureBox2";
            pictureBox2.Size = new Size(45, 47);
            pictureBox2.TabIndex = 4;
            pictureBox2.TabStop = false;
            // 
            // fThemKhuyenMai
            // 
            AutoScaleMode = AutoScaleMode.None;
            BackColor = Color.Firebrick;
            ClientSize = new Size(1462, 440);
            ControlBox = false;
            Controls.Add(pictureBox2);
            Controls.Add(panel1);
            Controls.Add(label1);
            Name = "fThemKhuyenMai";
            StartPosition = FormStartPosition.CenterParent;
            panel1.ResumeLayout(false);
            panel1.PerformLayout();
            groupBox1.ResumeLayout(false);
            groupBox1.PerformLayout();
            panel4.ResumeLayout(false);
            panel4.PerformLayout();
            panel3.ResumeLayout(false);
            panel3.PerformLayout();
            panel5.ResumeLayout(false);
            panel5.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBox2).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label label1;
        private Panel panel1;
        private Panel panel4;
        private TextBox txtMoTa;
        private Panel panel3;
        private TextBox txtTenKM;
        private Label label5;
        private Label label6;
        private Label label4;
        private GroupBox groupBox1;
        private DateTimePicker dtThoiGianStart;
        private Label label7;
        private DateTimePicker dtThoiGianEnd;
        private Label label9;
        private Panel panel5;
        private TextBox txtGiaTri;
        private Label label12;
        private Label label11;
        private Button btnLuu;
        private Button btnHuyBo;
        private Button btnGiup;
        private PictureBox pictureBox2;
    }
}