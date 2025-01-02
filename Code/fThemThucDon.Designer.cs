namespace QL_Bida
{
    partial class fThemThucDon
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
            txtTenMon = new TextBox();
            panel1 = new Panel();
            cboNhomThucDon = new ComboBox();
            cboDonViTinh = new ComboBox();
            cboLoaiMon = new ComboBox();
            btnLuu = new Button();
            btnHuyBo = new Button();
            btnGiup = new Button();
            groupBox1 = new GroupBox();
            pictureBox1 = new PictureBox();
            btnXoaAnh = new Button();
            btnChonAnh = new Button();
            pbAnhDaiDien = new PictureBox();
            label20 = new Label();
            label21 = new Label();
            label11 = new Label();
            label7 = new Label();
            label25 = new Label();
            label5 = new Label();
            label10 = new Label();
            label6 = new Label();
            label24 = new Label();
            label4 = new Label();
            panel8 = new Panel();
            txtGia = new TextBox();
            panel2 = new Panel();
            pictureBox2 = new PictureBox();
            panel1.SuspendLayout();
            groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pbAnhDaiDien).BeginInit();
            panel8.SuspendLayout();
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
            label1.Size = new Size(113, 28);
            label1.TabIndex = 1;
            label1.Text = "Thêm món";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Font = new Font("Segoe UI Semibold", 13.5F, FontStyle.Bold);
            label2.Location = new Point(28, 81);
            label2.Name = "label2";
            label2.Size = new Size(103, 31);
            label2.TabIndex = 0;
            label2.Text = "Tên món";
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
            // txtTenMon
            // 
            txtTenMon.BackColor = Color.Silver;
            txtTenMon.BorderStyle = BorderStyle.None;
            txtTenMon.Font = new Font("Segoe UI", 25F);
            txtTenMon.Location = new Point(16, 4);
            txtTenMon.Name = "txtTenMon";
            txtTenMon.Size = new Size(706, 56);
            txtTenMon.TabIndex = 2;
            // 
            // panel1
            // 
            panel1.BackColor = Color.White;
            panel1.Controls.Add(cboNhomThucDon);
            panel1.Controls.Add(cboDonViTinh);
            panel1.Controls.Add(cboLoaiMon);
            panel1.Controls.Add(btnLuu);
            panel1.Controls.Add(btnHuyBo);
            panel1.Controls.Add(btnGiup);
            panel1.Controls.Add(groupBox1);
            panel1.Controls.Add(label11);
            panel1.Controls.Add(label7);
            panel1.Controls.Add(label25);
            panel1.Controls.Add(label5);
            panel1.Controls.Add(label3);
            panel1.Controls.Add(label10);
            panel1.Controls.Add(label6);
            panel1.Controls.Add(label24);
            panel1.Controls.Add(label4);
            panel1.Controls.Add(label2);
            panel1.Controls.Add(panel8);
            panel1.Controls.Add(panel2);
            panel1.Location = new Point(8, 69);
            panel1.Name = "panel1";
            panel1.Size = new Size(1457, 696);
            panel1.TabIndex = 0;
            // 
            // cboNhomThucDon
            // 
            cboNhomThucDon.BackColor = Color.Silver;
            cboNhomThucDon.FlatStyle = FlatStyle.Flat;
            cboNhomThucDon.Font = new Font("Segoe UI", 13.8F, FontStyle.Regular, GraphicsUnit.Point, 163);
            cboNhomThucDon.FormattingEnabled = true;
            cboNhomThucDon.Items.AddRange(new object[] { "Bia", "Thuốc Lá", "Rượu", "Sinh Tố", "Trà", "Cà Phê", "Bánh", "Mì", "Cơm" });
            cboNhomThucDon.Location = new Point(741, 161);
            cboNhomThucDon.Name = "cboNhomThucDon";
            cboNhomThucDon.Size = new Size(246, 39);
            cboNhomThucDon.TabIndex = 5;
            // 
            // cboDonViTinh
            // 
            cboDonViTinh.BackColor = Color.Silver;
            cboDonViTinh.FlatStyle = FlatStyle.Flat;
            cboDonViTinh.Font = new Font("Segoe UI", 13.8F, FontStyle.Regular, GraphicsUnit.Point, 163);
            cboDonViTinh.FormattingEnabled = true;
            cboDonViTinh.Items.AddRange(new object[] { "Phần", "Tô", "Ly", "Bao", "Lon", "Chai" });
            cboDonViTinh.Location = new Point(250, 263);
            cboDonViTinh.Name = "cboDonViTinh";
            cboDonViTinh.Size = new Size(246, 39);
            cboDonViTinh.TabIndex = 5;
            // 
            // cboLoaiMon
            // 
            cboLoaiMon.BackColor = Color.Silver;
            cboLoaiMon.FlatStyle = FlatStyle.Flat;
            cboLoaiMon.Font = new Font("Segoe UI", 13.8F, FontStyle.Regular, GraphicsUnit.Point, 163);
            cboLoaiMon.FormattingEnabled = true;
            cboLoaiMon.Location = new Point(250, 161);
            cboLoaiMon.Name = "cboLoaiMon";
            cboLoaiMon.Size = new Size(246, 39);
            cboLoaiMon.TabIndex = 5;
            cboLoaiMon.SelectedIndexChanged += cboLoaiMon_SelectedIndexChanged;
            // 
            // btnLuu
            // 
            btnLuu.Font = new Font("Segoe UI Semibold", 12F, FontStyle.Bold);
            btnLuu.Image = Properties.Resources.save_24;
            btnLuu.ImageAlign = ContentAlignment.MiddleLeft;
            btnLuu.Location = new Point(1074, 630);
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
            btnHuyBo.Location = new Point(1196, 629);
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
            btnGiup.Location = new Point(1334, 630);
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
            // groupBox1
            // 
            groupBox1.BackColor = Color.Silver;
            groupBox1.Controls.Add(pictureBox1);
            groupBox1.Controls.Add(btnXoaAnh);
            groupBox1.Controls.Add(btnChonAnh);
            groupBox1.Controls.Add(pbAnhDaiDien);
            groupBox1.Controls.Add(label20);
            groupBox1.Controls.Add(label21);
            groupBox1.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
            groupBox1.Location = new Point(1007, 37);
            groupBox1.Name = "groupBox1";
            groupBox1.Size = new Size(444, 415);
            groupBox1.TabIndex = 3;
            groupBox1.TabStop = false;
            groupBox1.Text = "Ảnh đại diện";
            // 
            // pictureBox1
            // 
            pictureBox1.Location = new Point(29, 41);
            pictureBox1.Name = "pictureBox1";
            pictureBox1.Size = new Size(309, 256);
            pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;
            pictureBox1.TabIndex = 2;
            pictureBox1.TabStop = false;
            // 
            // btnXoaAnh
            // 
            btnXoaAnh.BackColor = Color.White;
            btnXoaAnh.Font = new Font("Segoe UI Semibold", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            btnXoaAnh.ForeColor = Color.Red;
            btnXoaAnh.Location = new Point(376, 103);
            btnXoaAnh.Margin = new Padding(35, 17, 3, 3);
            btnXoaAnh.Name = "btnXoaAnh";
            btnXoaAnh.Size = new Size(42, 42);
            btnXoaAnh.TabIndex = 1;
            btnXoaAnh.Text = "X";
            btnXoaAnh.UseVisualStyleBackColor = false;
            btnXoaAnh.Click += btnXoaAnh_Click;
            // 
            // btnChonAnh
            // 
            btnChonAnh.Font = new Font("Segoe UI Semibold", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            btnChonAnh.Location = new Point(376, 41);
            btnChonAnh.Margin = new Padding(35, 3, 3, 3);
            btnChonAnh.Name = "btnChonAnh";
            btnChonAnh.Size = new Size(42, 42);
            btnChonAnh.TabIndex = 1;
            btnChonAnh.Text = "...";
            btnChonAnh.UseVisualStyleBackColor = false;
            btnChonAnh.Click += btnChonAnh_Click;
            // 
            // pbAnhDaiDien
            // 
            pbAnhDaiDien.Location = new Point(29, 41);
            pbAnhDaiDien.Margin = new Padding(26, 18, 3, 3);
            pbAnhDaiDien.Name = "pbAnhDaiDien";
            pbAnhDaiDien.Size = new Size(309, 256);
            pbAnhDaiDien.TabIndex = 0;
            pbAnhDaiDien.TabStop = false;
            // 
            // label20
            // 
            label20.Font = new Font("Segoe UI", 13.2000008F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label20.Location = new Point(29, 313);
            label20.Margin = new Padding(3, 13, 3, 0);
            label20.Name = "label20";
            label20.Size = new Size(309, 34);
            label20.TabIndex = 0;
            label20.Text = "Chọn các ảnh có định dạng\r\n\r\n";
            // 
            // label21
            // 
            label21.Font = new Font("Segoe UI Semibold", 13.5F, FontStyle.Bold);
            label21.Location = new Point(23, 340);
            label21.Margin = new Padding(3, 13, 3, 0);
            label21.Name = "label21";
            label21.Size = new Size(309, 34);
            label21.TabIndex = 0;
            label21.Text = "(.jpg, .jeg, .png, .gif)";
            label21.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // label11
            // 
            label11.AutoSize = true;
            label11.Font = new Font("Segoe UI", 12F);
            label11.ForeColor = Color.Red;
            label11.Location = new Point(106, 353);
            label11.Name = "label11";
            label11.Size = new Size(32, 28);
            label11.TabIndex = 1;
            label11.Text = "(*)";
            // 
            // label7
            // 
            label7.AutoSize = true;
            label7.Font = new Font("Segoe UI", 12F);
            label7.ForeColor = Color.Red;
            label7.Location = new Point(181, 266);
            label7.Name = "label7";
            label7.Size = new Size(32, 28);
            label7.TabIndex = 1;
            label7.Text = "(*)";
            // 
            // label25
            // 
            label25.AutoSize = true;
            label25.Font = new Font("Segoe UI", 12F);
            label25.ForeColor = Color.Red;
            label25.Location = new Point(699, 166);
            label25.Name = "label25";
            label25.Size = new Size(32, 28);
            label25.TabIndex = 1;
            label25.Text = "(*)";
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Font = new Font("Segoe UI", 12F);
            label5.ForeColor = Color.Red;
            label5.Location = new Point(181, 164);
            label5.Name = "label5";
            label5.Size = new Size(32, 28);
            label5.TabIndex = 1;
            label5.Text = "(*)";
            // 
            // label10
            // 
            label10.AutoSize = true;
            label10.Font = new Font("Segoe UI Semibold", 13.5F, FontStyle.Bold);
            label10.Location = new Point(28, 351);
            label10.Name = "label10";
            label10.Size = new Size(48, 31);
            label10.TabIndex = 0;
            label10.Text = "Giá";
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Font = new Font("Segoe UI Semibold", 13.5F, FontStyle.Bold);
            label6.Location = new Point(28, 266);
            label6.Name = "label6";
            label6.Size = new Size(128, 31);
            label6.TabIndex = 0;
            label6.Text = "Đơn vị tính";
            // 
            // label24
            // 
            label24.AutoSize = true;
            label24.Font = new Font("Segoe UI Semibold", 13.5F, FontStyle.Bold);
            label24.Location = new Point(527, 166);
            label24.Name = "label24";
            label24.Size = new Size(178, 31);
            label24.TabIndex = 0;
            label24.Text = "Nhóm thực đơn";
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Font = new Font("Segoe UI Semibold", 13.5F, FontStyle.Bold);
            label4.Location = new Point(28, 164);
            label4.Name = "label4";
            label4.Size = new Size(110, 31);
            label4.TabIndex = 0;
            label4.Text = "Loại món";
            // 
            // panel8
            // 
            panel8.BackColor = Color.Silver;
            panel8.Controls.Add(txtGia);
            panel8.Location = new Point(250, 334);
            panel8.Margin = new Padding(3, 14, 3, 3);
            panel8.Name = "panel8";
            panel8.Size = new Size(737, 66);
            panel8.TabIndex = 4;
            // 
            // txtGia
            // 
            txtGia.BackColor = Color.Silver;
            txtGia.BorderStyle = BorderStyle.None;
            txtGia.Font = new Font("Segoe UI", 26F);
            txtGia.Location = new Point(16, 5);
            txtGia.Name = "txtGia";
            txtGia.Size = new Size(706, 58);
            txtGia.TabIndex = 2;
            // 
            // panel2
            // 
            panel2.BackColor = Color.Silver;
            panel2.Controls.Add(txtTenMon);
            panel2.Location = new Point(250, 64);
            panel2.Margin = new Padding(3, 37, 19, 3);
            panel2.Name = "panel2";
            panel2.Size = new Size(737, 66);
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
            // fThemThucDon
            // 
            AutoScaleMode = AutoScaleMode.None;
            BackColor = Color.Firebrick;
            ClientSize = new Size(1462, 772);
            ControlBox = false;
            Controls.Add(pictureBox2);
            Controls.Add(label1);
            Controls.Add(panel1);
            Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            ForeColor = SystemColors.ControlText;
            Name = "fThemThucDon";
            StartPosition = FormStartPosition.CenterParent;
            panel1.ResumeLayout(false);
            panel1.PerformLayout();
            groupBox1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
            ((System.ComponentModel.ISupportInitialize)pbAnhDaiDien).EndInit();
            panel8.ResumeLayout(false);
            panel8.PerformLayout();
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
        private TextBox txtTenMon;
        private Panel panel1;
        private Panel panel2;
        private Label label5;
        private Label label4;
        private Label label9;
        private Label label8;
        private Panel panel6;
        private TextBox txtSoCMND;
        private Label label11;
        private Label label10;
        private Panel panel8;
        private TextBox txtGia;
        private Label label23;
        private Label label22;
        private DateTimePicker dtNgaySinh;
        private DateTimePicker dtNgayCap;
        private Button btnLuu;
        private Button btnHuyBo;
        private Button btnGiup;
        private PictureBox pictureBox2;
        private Panel panel4;
        private ComboBox cboLoaiMon;
        private ComboBox cboNhomThucDon;
        private ComboBox cboDonViTinh;
        private Label label7;
        private Label label25;
        private Label label6;
        private Label label24;
        private GroupBox groupBox1;
        private PictureBox pictureBox1;
        private Button btnXoaAnh;
        private Button btnChonAnh;
        private PictureBox pbAnhDaiDien;
        private Label label20;
        private Label label21;
        private ComboBox comboBox2;
        private ComboBox comboBox1;
        private Label label27;
        private Label label26;
    }
}