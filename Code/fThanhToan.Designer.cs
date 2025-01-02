namespace QL_Bida
{
    partial class fThanhToan
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
        public void InitializeComponent()
        {
            components = new System.ComponentModel.Container();
            label1 = new Label();
            panel1 = new Panel();
            cbKhuyenMai = new ComboBox();
            dgvBangGia = new DataGridView();
            TenMon = new DataGridViewTextBoxColumn();
            SOLuong = new DataGridViewTextBoxColumn();
            ThanhTien = new DataGridViewTextBoxColumn();
            btnThanhToan = new Button();
            btnXuatHoaDon = new Button();
            cbThongTinKH = new ComboBox();
            cbPhuongThucTT = new ComboBox();
            panel4 = new Panel();
            panel3 = new Panel();
            panel6 = new Panel();
            panel5 = new Panel();
            panel2 = new Panel();
            lbThanhVien = new Label();
            lbKhuyenMai = new Label();
            lbThanhTien = new Label();
            label9 = new Label();
            label8 = new Label();
            label7 = new Label();
            label5 = new Label();
            lbTienBan = new Label();
            lbTongGio = new Label();
            label6 = new Label();
            label2 = new Label();
            label4 = new Label();
            lbTenBan = new Label();
            lbTongCong = new Label();
            button1 = new Button();
            timerTienBan = new System.Windows.Forms.Timer(components);
            label3 = new Label();
            panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgvBangGia).BeginInit();
            SuspendLayout();
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
            label1.ForeColor = SystemColors.Control;
            label1.Location = new Point(12, 23);
            label1.Name = "label1";
            label1.Size = new Size(122, 28);
            label1.TabIndex = 2;
            label1.Text = "Chi tiết bàn";
            // 
            // panel1
            // 
            panel1.BackColor = Color.White;
            panel1.Controls.Add(label3);
            panel1.Controls.Add(cbKhuyenMai);
            panel1.Controls.Add(dgvBangGia);
            panel1.Controls.Add(btnThanhToan);
            panel1.Controls.Add(btnXuatHoaDon);
            panel1.Controls.Add(cbThongTinKH);
            panel1.Controls.Add(cbPhuongThucTT);
            panel1.Controls.Add(panel4);
            panel1.Controls.Add(panel3);
            panel1.Controls.Add(panel6);
            panel1.Controls.Add(panel5);
            panel1.Controls.Add(panel2);
            panel1.Controls.Add(lbThanhVien);
            panel1.Controls.Add(lbKhuyenMai);
            panel1.Controls.Add(lbThanhTien);
            panel1.Controls.Add(label9);
            panel1.Controls.Add(label8);
            panel1.Controls.Add(label7);
            panel1.Controls.Add(label5);
            panel1.Controls.Add(lbTienBan);
            panel1.Controls.Add(lbTongGio);
            panel1.Controls.Add(label6);
            panel1.Controls.Add(label2);
            panel1.Controls.Add(label4);
            panel1.Controls.Add(lbTenBan);
            panel1.Controls.Add(lbTongCong);
            panel1.Location = new Point(8, 69);
            panel1.Name = "panel1";
            panel1.Size = new Size(1246, 626);
            panel1.TabIndex = 3;
            // 
            // cbKhuyenMai
            // 
            cbKhuyenMai.Font = new Font("Segoe UI Semibold", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            cbKhuyenMai.FormattingEnabled = true;
            cbKhuyenMai.Location = new Point(947, 208);
            cbKhuyenMai.Name = "cbKhuyenMai";
            cbKhuyenMai.Size = new Size(269, 36);
            cbKhuyenMai.TabIndex = 9;
            cbKhuyenMai.Text = "Khuyến mãi";
            cbKhuyenMai.SelectedIndexChanged += cbKhuyenMai_SelectedIndexChanged;
            // 
            // dgvBangGia
            // 
            dgvBangGia.BackgroundColor = SystemColors.ButtonHighlight;
            dgvBangGia.BorderStyle = BorderStyle.None;
            dgvBangGia.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
            dgvBangGia.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.None;
            dgvBangGia.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvBangGia.Columns.AddRange(new DataGridViewColumn[] { TenMon, SOLuong, ThanhTien });
            dgvBangGia.Location = new Point(43, 189);
            dgvBangGia.Name = "dgvBangGia";
            dgvBangGia.RowHeadersWidth = 51;
            dgvBangGia.Size = new Size(604, 249);
            dgvBangGia.TabIndex = 8;
            // 
            // TenMon
            // 
            TenMon.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            TenMon.HeaderText = "Tên món";
            TenMon.MinimumWidth = 6;
            TenMon.Name = "TenMon";
            // 
            // SOLuong
            // 
            SOLuong.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            SOLuong.HeaderText = "Số lượng";
            SOLuong.MinimumWidth = 6;
            SOLuong.Name = "SOLuong";
            // 
            // ThanhTien
            // 
            ThanhTien.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            ThanhTien.HeaderText = "Thành Tiền";
            ThanhTien.MinimumWidth = 6;
            ThanhTien.Name = "ThanhTien";
            // 
            // btnThanhToan
            // 
            btnThanhToan.BackColor = Color.Firebrick;
            btnThanhToan.Font = new Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            btnThanhToan.ForeColor = Color.White;
            btnThanhToan.Location = new Point(937, 553);
            btnThanhToan.Name = "btnThanhToan";
            btnThanhToan.Size = new Size(288, 52);
            btnThanhToan.TabIndex = 7;
            btnThanhToan.Text = "THANH TOÁN";
            btnThanhToan.UseVisualStyleBackColor = false;
            btnThanhToan.Click += btnThanhToan_Click;
            // 
            // btnXuatHoaDon
            // 
            btnXuatHoaDon.BackColor = Color.Firebrick;
            btnXuatHoaDon.Font = new Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            btnXuatHoaDon.ForeColor = Color.White;
            btnXuatHoaDon.Location = new Point(560, 553);
            btnXuatHoaDon.Margin = new Padding(3, 3, 86, 3);
            btnXuatHoaDon.Name = "btnXuatHoaDon";
            btnXuatHoaDon.Size = new Size(288, 52);
            btnXuatHoaDon.TabIndex = 7;
            btnXuatHoaDon.Text = "XUẤT HOÁ ĐƠN";
            btnXuatHoaDon.UseVisualStyleBackColor = false;
            btnXuatHoaDon.Click += btnXuatHoaDon_Click;
            // 
            // cbThongTinKH
            // 
            cbThongTinKH.Font = new Font("Segoe UI Semibold", 16.2F, FontStyle.Bold, GraphicsUnit.Point, 0);
            cbThongTinKH.FormattingEnabled = true;
            cbThongTinKH.Location = new Point(782, 38);
            cbThongTinKH.Name = "cbThongTinKH";
            cbThongTinKH.Size = new Size(443, 45);
            cbThongTinKH.TabIndex = 6;
            cbThongTinKH.Text = "Thông tin khách hàng";
            cbThongTinKH.SelectedIndexChanged += cbThongTinKH_SelectedIndexChanged;
            // 
            // cbPhuongThucTT
            // 
            cbPhuongThucTT.Font = new Font("Segoe UI Semibold", 16.2F, FontStyle.Bold, GraphicsUnit.Point, 0);
            cbPhuongThucTT.FormattingEnabled = true;
            cbPhuongThucTT.Location = new Point(782, 404);
            cbPhuongThucTT.Name = "cbPhuongThucTT";
            cbPhuongThucTT.Size = new Size(443, 45);
            cbPhuongThucTT.TabIndex = 6;
            cbPhuongThucTT.Text = "Phương thức thanh toán";
            // 
            // panel4
            // 
            panel4.BackColor = Color.Black;
            panel4.Location = new Point(747, 103);
            panel4.Margin = new Padding(97, 280, 3, 3);
            panel4.Name = "panel4";
            panel4.Size = new Size(4, 320);
            panel4.TabIndex = 5;
            // 
            // panel3
            // 
            panel3.BackColor = Color.Black;
            panel3.Location = new Point(40, 445);
            panel3.Margin = new Padding(3, 255, 3, 3);
            panel3.Name = "panel3";
            panel3.Size = new Size(607, 4);
            panel3.TabIndex = 5;
            // 
            // panel6
            // 
            panel6.BackColor = Color.Black;
            panel6.Location = new Point(782, 448);
            panel6.Margin = new Padding(3, 334, 21, 3);
            panel6.Name = "panel6";
            panel6.Size = new Size(443, 4);
            panel6.TabIndex = 5;
            // 
            // panel5
            // 
            panel5.BackColor = Color.Black;
            panel5.Location = new Point(782, 82);
            panel5.Margin = new Padding(3, 27, 21, 3);
            panel5.Name = "panel5";
            panel5.Size = new Size(443, 4);
            panel5.TabIndex = 5;
            // 
            // panel2
            // 
            panel2.BackColor = Color.Black;
            panel2.Location = new Point(40, 179);
            panel2.Margin = new Padding(3, 27, 3, 3);
            panel2.Name = "panel2";
            panel2.Size = new Size(607, 4);
            panel2.TabIndex = 5;
            // 
            // lbThanhVien
            // 
            lbThanhVien.AutoSize = true;
            lbThanhVien.Font = new Font("Segoe UI Semibold", 12F, FontStyle.Bold);
            lbThanhVien.ForeColor = SystemColors.ControlText;
            lbThanhVien.Location = new Point(1120, 130);
            lbThanhVien.Margin = new Padding(45, 50, 45, 0);
            lbThanhVien.Name = "lbThanhVien";
            lbThanhVien.Size = new Size(80, 28);
            lbThanhVien.TabIndex = 1;
            lbThanhVien.Text = "-20,000";
            // 
            // lbKhuyenMai
            // 
            lbKhuyenMai.AutoSize = true;
            lbKhuyenMai.Font = new Font("Segoe UI Semibold", 12F, FontStyle.Bold);
            lbKhuyenMai.ForeColor = SystemColors.ControlText;
            lbKhuyenMai.Location = new Point(1120, 251);
            lbKhuyenMai.Margin = new Padding(45, 50, 45, 0);
            lbKhuyenMai.Name = "lbKhuyenMai";
            lbKhuyenMai.Size = new Size(80, 28);
            lbKhuyenMai.TabIndex = 1;
            lbKhuyenMai.Text = "-20,000";
            // 
            // lbThanhTien
            // 
            lbThanhTien.AutoSize = true;
            lbThanhTien.Font = new Font("Segoe UI Semibold", 14F, FontStyle.Bold);
            lbThanhTien.ForeColor = Color.Red;
            lbThanhTien.Location = new Point(1120, 324);
            lbThanhTien.Margin = new Padding(45, 50, 45, 0);
            lbThanhTien.Name = "lbThanhTien";
            lbThanhTien.Size = new Size(95, 32);
            lbThanhTien.TabIndex = 1;
            lbThanhTien.Text = "215,000";
            // 
            // label9
            // 
            label9.AutoSize = true;
            label9.Font = new Font("Segoe UI Semibold", 14F, FontStyle.Bold);
            label9.ForeColor = Color.Red;
            label9.Location = new Point(782, 324);
            label9.Margin = new Padding(45, 50, 3, 0);
            label9.Name = "label9";
            label9.Size = new Size(137, 32);
            label9.TabIndex = 1;
            label9.Text = "Thành tiền:";
            // 
            // label8
            // 
            label8.AutoSize = true;
            label8.Font = new Font("Segoe UI Semibold", 12F, FontStyle.Bold);
            label8.Location = new Point(782, 211);
            label8.Margin = new Padding(45, 50, 3, 0);
            label8.Name = "label8";
            label8.Size = new Size(125, 28);
            label8.TabIndex = 1;
            label8.Text = "Khuyến mãi:";
            // 
            // label7
            // 
            label7.AutoSize = true;
            label7.Font = new Font("Segoe UI Semibold", 12F, FontStyle.Bold);
            label7.Location = new Point(782, 130);
            label7.Margin = new Padding(45, 0, 3, 0);
            label7.Name = "label7";
            label7.Size = new Size(118, 28);
            label7.TabIndex = 1;
            label7.Text = "Thành viên:";
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Font = new Font("Segoe UI Semibold", 13.8F, FontStyle.Bold);
            label5.Location = new Point(252, 105);
            label5.Margin = new Padding(45, 20, 3, 0);
            label5.Name = "label5";
            label5.Size = new Size(109, 31);
            label5.TabIndex = 1;
            label5.Text = "Tiền bàn:";
            // 
            // lbTienBan
            // 
            lbTienBan.AutoSize = true;
            lbTienBan.Font = new Font("Segoe UI Semibold", 13.8F, FontStyle.Bold);
            lbTienBan.Location = new Point(533, 108);
            lbTienBan.Margin = new Padding(170, 55, 3, 35);
            lbTienBan.Name = "lbTienBan";
            lbTienBan.Size = new Size(85, 31);
            lbTienBan.TabIndex = 1;
            lbTienBan.Text = "00,000";
            // 
            // lbTongGio
            // 
            lbTongGio.AutoSize = true;
            lbTongGio.Font = new Font("Segoe UI Semibold", 13.8F, FontStyle.Bold);
            lbTongGio.Location = new Point(533, 42);
            lbTongGio.Margin = new Padding(170, 55, 3, 35);
            lbTongGio.Name = "lbTongGio";
            lbTongGio.Size = new Size(104, 31);
            lbTongGio.TabIndex = 1;
            lbTongGio.Text = "00:00:00";
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Font = new Font("Segoe UI Semibold", 13.8F, FontStyle.Bold);
            label6.Location = new Point(598, 483);
            label6.Margin = new Padding(3, 42, 3, 35);
            label6.Name = "label6";
            label6.Size = new Size(64, 31);
            label6.TabIndex = 1;
            label6.Text = "VND";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Font = new Font("Segoe UI Semibold", 13.8F, FontStyle.Bold);
            label2.Location = new Point(40, 483);
            label2.Margin = new Padding(3, 42, 3, 35);
            label2.Name = "label2";
            label2.Size = new Size(132, 31);
            label2.TabIndex = 1;
            label2.Text = "Tổng cộng:";
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Font = new Font("Segoe UI Semibold", 13.8F, FontStyle.Bold);
            label4.Location = new Point(252, 42);
            label4.Margin = new Padding(3, 55, 3, 35);
            label4.Name = "label4";
            label4.Size = new Size(108, 31);
            label4.TabIndex = 1;
            label4.Text = "Tổng giờ";
            // 
            // lbTenBan
            // 
            lbTenBan.AutoSize = true;
            lbTenBan.Font = new Font("Segoe UI Semibold", 31.8000011F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lbTenBan.Location = new Point(40, 50);
            lbTenBan.Margin = new Padding(40, 60, 3, 0);
            lbTenBan.Name = "lbTenBan";
            lbTenBan.Size = new Size(168, 72);
            lbTenBan.TabIndex = 0;
            lbTenBan.Text = "Bàn 6";
            // 
            // lbTongCong
            // 
            lbTongCong.AutoSize = true;
            lbTongCong.Font = new Font("Segoe UI Semibold", 13.8F, FontStyle.Bold);
            lbTongCong.Location = new Point(507, 483);
            lbTongCong.Margin = new Padding(3, 42, 3, 35);
            lbTongCong.Name = "lbTongCong";
            lbTongCong.Size = new Size(98, 31);
            lbTongCong.TabIndex = 1;
            lbTongCong.Text = "245,000";
            // 
            // button1
            // 
            button1.Font = new Font("Segoe UI", 13.8F, FontStyle.Bold, GraphicsUnit.Point, 163);
            button1.Location = new Point(1203, 12);
            button1.Name = "button1";
            button1.Size = new Size(49, 39);
            button1.TabIndex = 4;
            button1.Text = "X";
            button1.UseVisualStyleBackColor = true;
            button1.Click += button1_Click;
            // 
            // timerTienBan
            // 
            timerTienBan.Tick += timerTienBan_Tick;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Font = new Font("Segoe UI Semibold", 13.8F, FontStyle.Bold);
            label3.Location = new Point(624, 108);
            label3.Margin = new Padding(3, 42, 3, 35);
            label3.Name = "label3";
            label3.Size = new Size(64, 31);
            label3.TabIndex = 10;
            label3.Text = "VND";
            // 
            // fThanhToan
            // 
            AutoScaleMode = AutoScaleMode.None;
            BackColor = Color.Firebrick;
            ClientSize = new Size(1264, 704);
            ControlBox = false;
            Controls.Add(button1);
            Controls.Add(panel1);
            Controls.Add(label1);
            Name = "fThanhToan";
            StartPosition = FormStartPosition.CenterParent;
            Load += fThanhToan_Load;
            panel1.ResumeLayout(false);
            panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)dgvBangGia).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label label1;
        private Panel panel1;
        private Label label5;
        private Label lbTienBan;
        private Label lbTongGio;
        private Label label4;
        private Panel panel2;
        private Panel panel4;
        private Panel panel3;
        private Label label6;
        private Label label2;
        private Label lbTongCong;
        private Panel panel5;
        private ComboBox cbPhuongThucTT;
        private Panel panel6;
        public ComboBox cbThongTinKH;
        private Label label9;
        private Label label8;
        private Label label7;
        private Label lbThanhVien;
        private Label lbKhuyenMai;
        private Label lbThanhTien;
        private Button btnXuatHoaDon;
        private Button btnThanhToan;
        private DataGridViewTextBoxColumn TenMon;
        private DataGridViewTextBoxColumn SOLuong;
        private DataGridViewTextBoxColumn ThanhTien;
        public Label lbTenBan;
        public DataGridView dgvBangGia;
        private Button button1;
        private System.Windows.Forms.Timer timerTienBan;
        public ComboBox cbKhuyenMai;
        private Label label3;
    }
}