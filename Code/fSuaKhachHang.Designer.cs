namespace QL_Bida
{
    partial class fSuaKhachHang
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
            label1 = new Label();
            label2 = new Label();
            label3 = new Label();
            txtTen = new TextBox();
            panel1 = new Panel();
            dateTimePicker1 = new DateTimePicker();
            btnLuu = new Button();
            btnHuyBo = new Button();
            btnGiup = new Button();
            label25 = new Label();
            label9 = new Label();
            label5 = new Label();
            label7 = new Label();
            label24 = new Label();
            label8 = new Label();
            label4 = new Label();
            label6 = new Label();
            panel5 = new Panel();
            txtDiem = new TextBox();
            panel4 = new Panel();
            txtEmail = new TextBox();
            panel3 = new Panel();
            txtSDT = new TextBox();
            panel2 = new Panel();
            pictureBox2 = new PictureBox();
            panel1.SuspendLayout();
            panel5.SuspendLayout();
            panel4.SuspendLayout();
            panel3.SuspendLayout();
            panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBox2).BeginInit();
            SuspendLayout();
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
            label1.ForeColor = SystemColors.Control;
            label1.Location = new Point(8, 21);
            label1.Name = "label1";
            label1.Size = new Size(280, 28);
            label1.TabIndex = 1;
            label1.Text = "Thêm khách hàng thân thiết";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Font = new Font("Segoe UI Semibold", 13.5F, FontStyle.Bold);
            label2.Location = new Point(28, 81);
            label2.Name = "label2";
            label2.Size = new Size(117, 31);
            label2.TabIndex = 0;
            label2.Text = "Họ và Tên";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Font = new Font("Segoe UI", 12F);
            label3.ForeColor = Color.Red;
            label3.Location = new Point(177, 81);
            label3.Name = "label3";
            label3.Size = new Size(32, 28);
            label3.TabIndex = 1;
            label3.Text = "(*)";
            // 
            // txtTen
            // 
            txtTen.BackColor = Color.Silver;
            txtTen.BorderStyle = BorderStyle.None;
            txtTen.Font = new Font("Segoe UI", 25F);
            txtTen.Location = new Point(16, 4);
            txtTen.Name = "txtTen";
            txtTen.Size = new Size(1082, 56);
            txtTen.TabIndex = 2;
            // 
            // panel1
            // 
            panel1.BackColor = Color.White;
            panel1.Controls.Add(dateTimePicker1);
            panel1.Controls.Add(btnLuu);
            panel1.Controls.Add(btnHuyBo);
            panel1.Controls.Add(btnGiup);
            panel1.Controls.Add(label25);
            panel1.Controls.Add(label9);
            panel1.Controls.Add(label5);
            panel1.Controls.Add(label7);
            panel1.Controls.Add(label3);
            panel1.Controls.Add(label24);
            panel1.Controls.Add(label8);
            panel1.Controls.Add(label4);
            panel1.Controls.Add(label6);
            panel1.Controls.Add(label2);
            panel1.Controls.Add(panel5);
            panel1.Controls.Add(panel4);
            panel1.Controls.Add(panel3);
            panel1.Controls.Add(panel2);
            panel1.Location = new Point(8, 69);
            panel1.Name = "panel1";
            panel1.Size = new Size(1457, 449);
            panel1.TabIndex = 0;
            panel1.Paint += panel1_Paint;
            // 
            // dateTimePicker1
            // 
            dateTimePicker1.CalendarFont = new Font("Segoe UI", 24F, FontStyle.Bold, GraphicsUnit.Point, 163);
            dateTimePicker1.Font = new Font("Segoe UI", 24F, FontStyle.Bold, GraphicsUnit.Point, 163);
            dateTimePicker1.Format = DateTimePickerFormat.Short;
            dateTimePicker1.Location = new Point(1116, 169);
            dateTimePicker1.Name = "dateTimePicker1";
            dateTimePicker1.Size = new Size(246, 61);
            dateTimePicker1.TabIndex = 16;
            // 
            // btnLuu
            // 
            btnLuu.Font = new Font("Segoe UI Semibold", 12F, FontStyle.Bold);
            btnLuu.Image = Properties.Resources.save_24;
            btnLuu.ImageAlign = ContentAlignment.MiddleLeft;
            btnLuu.Location = new Point(1083, 363);
            btnLuu.Margin = new Padding(3, 3, 3, 7);
            btnLuu.Name = "btnLuu";
            btnLuu.Padding = new Padding(18, 0, 0, 0);
            btnLuu.Size = new Size(116, 59);
            btnLuu.TabIndex = 13;
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
            btnHuyBo.Location = new Point(1205, 362);
            btnHuyBo.Margin = new Padding(3, 3, 3, 7);
            btnHuyBo.Name = "btnHuyBo";
            btnHuyBo.Padding = new Padding(18, 0, 0, 0);
            btnHuyBo.Size = new Size(133, 60);
            btnHuyBo.TabIndex = 14;
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
            btnGiup.Location = new Point(1343, 363);
            btnGiup.Margin = new Padding(9, 3, 7, 7);
            btnGiup.Name = "btnGiup";
            btnGiup.Padding = new Padding(6, 0, 0, 0);
            btnGiup.Size = new Size(116, 59);
            btnGiup.TabIndex = 15;
            btnGiup.Text = "Giúp";
            btnGiup.TextImageRelation = TextImageRelation.ImageBeforeText;
            btnGiup.UseCompatibleTextRendering = true;
            btnGiup.UseVisualStyleBackColor = true;
            // 
            // label25
            // 
            label25.AutoSize = true;
            label25.Font = new Font("Segoe UI", 12F);
            label25.ForeColor = Color.Red;
            label25.Location = new Point(1062, 176);
            label25.Name = "label25";
            label25.Size = new Size(32, 28);
            label25.TabIndex = 1;
            label25.Text = "(*)";
            // 
            // label9
            // 
            label9.AutoSize = true;
            label9.Font = new Font("Segoe UI", 12F);
            label9.ForeColor = Color.Red;
            label9.Location = new Point(607, 173);
            label9.Name = "label9";
            label9.Size = new Size(32, 28);
            label9.TabIndex = 1;
            label9.Text = "(*)";
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Font = new Font("Segoe UI", 12F);
            label5.ForeColor = Color.Red;
            label5.Location = new Point(181, 173);
            label5.Name = "label5";
            label5.Size = new Size(32, 28);
            label5.TabIndex = 1;
            label5.Text = "(*)";
            // 
            // label7
            // 
            label7.AutoSize = true;
            label7.Font = new Font("Segoe UI", 12F);
            label7.ForeColor = Color.Red;
            label7.Location = new Point(177, 281);
            label7.Name = "label7";
            label7.Size = new Size(32, 28);
            label7.TabIndex = 1;
            label7.Text = "(*)";
            // 
            // label24
            // 
            label24.AutoSize = true;
            label24.Font = new Font("Segoe UI Semibold", 13.5F, FontStyle.Bold);
            label24.Location = new Point(938, 173);
            label24.Name = "label24";
            label24.Size = new Size(118, 31);
            label24.TabIndex = 0;
            label24.Text = "Ngày sinh";
            // 
            // label8
            // 
            label8.AutoSize = true;
            label8.Font = new Font("Segoe UI Semibold", 13.5F, FontStyle.Bold);
            label8.Location = new Point(520, 173);
            label8.Name = "label8";
            label8.Size = new Size(69, 31);
            label8.TabIndex = 0;
            label8.Text = "Điểm";
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Font = new Font("Segoe UI Semibold", 13.5F, FontStyle.Bold);
            label4.Location = new Point(28, 173);
            label4.Name = "label4";
            label4.Size = new Size(151, 31);
            label4.TabIndex = 0;
            label4.Text = "Số điện thoại";
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Font = new Font("Segoe UI Semibold", 13.5F, FontStyle.Bold);
            label6.Location = new Point(28, 281);
            label6.Name = "label6";
            label6.Size = new Size(70, 31);
            label6.TabIndex = 0;
            label6.Text = "Email";
            // 
            // panel5
            // 
            panel5.BackColor = Color.Silver;
            panel5.Controls.Add(txtDiem);
            panel5.Location = new Point(675, 164);
            panel5.Margin = new Padding(3, 37, 19, 3);
            panel5.Name = "panel5";
            panel5.Size = new Size(215, 66);
            panel5.TabIndex = 4;
            // 
            // txtDiem
            // 
            txtDiem.BackColor = Color.Silver;
            txtDiem.BorderStyle = BorderStyle.None;
            txtDiem.Font = new Font("Segoe UI", 25F);
            txtDiem.Location = new Point(3, 3);
            txtDiem.Name = "txtDiem";
            txtDiem.Size = new Size(186, 56);
            txtDiem.TabIndex = 2;
            // 
            // panel4
            // 
            panel4.BackColor = Color.Silver;
            panel4.Controls.Add(txtEmail);
            panel4.Location = new Point(250, 264);
            panel4.Margin = new Padding(3, 37, 19, 3);
            panel4.Name = "panel4";
            panel4.Size = new Size(1127, 66);
            panel4.TabIndex = 4;
            // 
            // txtEmail
            // 
            txtEmail.BackColor = Color.Silver;
            txtEmail.BorderStyle = BorderStyle.None;
            txtEmail.Font = new Font("Segoe UI", 25F);
            txtEmail.Location = new Point(16, 4);
            txtEmail.Name = "txtEmail";
            txtEmail.Size = new Size(1082, 56);
            txtEmail.TabIndex = 2;
            // 
            // panel3
            // 
            panel3.BackColor = Color.Silver;
            panel3.Controls.Add(txtSDT);
            panel3.Location = new Point(250, 164);
            panel3.Margin = new Padding(3, 37, 19, 3);
            panel3.Name = "panel3";
            panel3.Size = new Size(270, 66);
            panel3.TabIndex = 4;
            // 
            // txtSDT
            // 
            txtSDT.BackColor = Color.Silver;
            txtSDT.BorderStyle = BorderStyle.None;
            txtSDT.Font = new Font("Segoe UI", 25F);
            txtSDT.Location = new Point(3, 3);
            txtSDT.Name = "txtSDT";
            txtSDT.Size = new Size(261, 56);
            txtSDT.TabIndex = 2;
            // 
            // panel2
            // 
            panel2.BackColor = Color.Silver;
            panel2.Controls.Add(txtTen);
            panel2.Location = new Point(250, 64);
            panel2.Margin = new Padding(3, 37, 19, 3);
            panel2.Name = "panel2";
            panel2.Size = new Size(1127, 66);
            panel2.TabIndex = 4;
            // 
            // pictureBox2
            // 
            pictureBox2.Location = new Point(1405, 12);
            pictureBox2.Name = "pictureBox2";
            pictureBox2.Size = new Size(45, 47);
            pictureBox2.TabIndex = 3;
            pictureBox2.TabStop = false;
            // 
            // fSuaKhachHang
            // 
            AutoScaleMode = AutoScaleMode.None;
            BackColor = Color.Firebrick;
            ClientSize = new Size(1462, 542);
            ControlBox = false;
            Controls.Add(pictureBox2);
            Controls.Add(label1);
            Controls.Add(panel1);
            Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            ForeColor = SystemColors.ControlText;
            Name = "fSuaKhachHang";
            StartPosition = FormStartPosition.CenterParent;
            Load += fSuaKhachHang_Load;
            panel1.ResumeLayout(false);
            panel1.PerformLayout();
            panel5.ResumeLayout(false);
            panel5.PerformLayout();
            panel4.ResumeLayout(false);
            panel4.PerformLayout();
            panel3.ResumeLayout(false);
            panel3.PerformLayout();
            panel2.ResumeLayout(false);
            panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBox2).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        private void InitializeCustomComponents()
        {
            this.BackColor = Color.White;
            this.Size = new Size(1489, 795);

            // Create a Panel as a container
            Panel mainPanel = new Panel()
            {
                Size = new Size(1489, 795),
                Location = new Point(0, 0),
                BackColor = Color.White
            };
            this.Controls.Add(mainPanel);

            // Background Rectangle (as a panel here)
            Panel backgroundRectangle = new Panel()
            {
                Size = new Size(1489, 795),
                Location = new Point(0, 0),
                BackColor = Color.LightGray
            };
            mainPanel.Controls.Add(backgroundRectangle);

            // Inner Rectangle
            Panel innerRectangle = new Panel()
            {
                Size = new Size(1457, 696),
                Location = new Point(12, 85),
                BackColor = Color.WhiteSmoke
            };
            mainPanel.Controls.Add(innerRectangle);

            // Text "Thêm nhân viên"
            Label titleLabel = new Label()
            {
                Text = "Thêm nhân viên",
                Location = new Point(21, 20),
                Size = new Size(334, 38),
                Font = new Font("Arial", 16, FontStyle.Bold)
            };
            mainPanel.Controls.Add(titleLabel);

            // Save Button
            Button saveButton = new Button()
            {
                Text = "Lưu",
                Location = new Point(1071, 714),
                Size = new Size(123, 59),
                BackColor = Color.LightBlue
            };
            mainPanel.Controls.Add(saveButton);
        }

        #endregion
        private Label label1;
        private Label label2;
        private Label label3;
        private TextBox txtTen;
        private Panel panel1;
        private Panel panel2;
        private Label label5;
        private Label label4;
        private Panel panel6;
        private TextBox txtSoCMND;
        private Label label23;
        private Label label22;
        private DateTimePicker dtNgaySinh;
        private DateTimePicker dtNgayCap;
        private Button btnLuu;
        private Button btnHuyBo;
        private Button btnGiup;
        private PictureBox pictureBox2;
        private Label label25;
        private Label label24;
        private ComboBox comboBox2;
        private Label label27;
        private Label label26;
        private DateTimePicker dateTimePicker1;
        private Label label7;
        private Label label6;
        private Panel panel4;
        private TextBox txtEmail;
        private Panel panel3;
        private TextBox txtSDT;
        private Label label9;
        private Label label8;
        private Panel panel5;
        private TextBox txtDiem;
    }
}