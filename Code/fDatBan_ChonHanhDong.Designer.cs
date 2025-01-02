namespace QL_Bida
{
    partial class fDatBan_ChonHanhDong
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            panelBackground = new Panel();
            btnXoaAnh = new Button();
            panelWhiteBox = new Panel();
            labelMessage = new Label();
            btnChuyenBan = new Button();
            btnDatMon = new Button();
            pictureBoxCharacter = new PictureBox();
            panelBackground.SuspendLayout();
            panelWhiteBox.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBoxCharacter).BeginInit();
            SuspendLayout();
            // 
            // panelBackground
            // 
            panelBackground.BackColor = Color.FromArgb(212, 162, 118);
            panelBackground.BorderStyle = BorderStyle.FixedSingle;
            panelBackground.Controls.Add(btnXoaAnh);
            panelBackground.Controls.Add(panelWhiteBox);
            panelBackground.Controls.Add(pictureBoxCharacter);
            panelBackground.Location = new Point(0, 1);
            panelBackground.Name = "panelBackground";
            panelBackground.Size = new Size(731, 419);
            panelBackground.TabIndex = 0;
            // 
            // btnXoaAnh
            // 
            btnXoaAnh.BackColor = Color.White;
            btnXoaAnh.Font = new Font("Segoe UI Semibold", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            btnXoaAnh.ForeColor = Color.Red;
            btnXoaAnh.Location = new Point(677, 24);
            btnXoaAnh.Margin = new Padding(35, 17, 3, 3);
            btnXoaAnh.Name = "btnXoaAnh";
            btnXoaAnh.Size = new Size(42, 42);
            btnXoaAnh.TabIndex = 2;
            btnXoaAnh.Text = "X";
            btnXoaAnh.UseVisualStyleBackColor = false;
            btnXoaAnh.Click += btnXoaAnh_Click;
            // 
            // panelWhiteBox
            // 
            panelWhiteBox.BackColor = Color.White;
            panelWhiteBox.Controls.Add(labelMessage);
            panelWhiteBox.Controls.Add(btnChuyenBan);
            panelWhiteBox.Controls.Add(btnDatMon);
            panelWhiteBox.Location = new Point(98, 185);
            panelWhiteBox.Name = "panelWhiteBox";
            panelWhiteBox.Size = new Size(528, 186);
            panelWhiteBox.TabIndex = 0;
            // 
            // labelMessage
            // 
            labelMessage.AutoSize = true;
            labelMessage.Font = new Font("Microsoft Sans Serif", 18F, FontStyle.Bold);
            labelMessage.Location = new Point(130, 19);
            labelMessage.Name = "labelMessage";
            labelMessage.Size = new Size(269, 36);
            labelMessage.TabIndex = 0;
            labelMessage.Text = "Bạn muốn làm gì?";
            labelMessage.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // btnChuyenBan
            // 
            btnChuyenBan.BackColor = Color.Silver;
            btnChuyenBan.FlatStyle = FlatStyle.Flat;
            btnChuyenBan.Font = new Font("Microsoft Sans Serif", 12F, FontStyle.Bold);
            btnChuyenBan.Location = new Point(143, 129);
            btnChuyenBan.Name = "btnChuyenBan";
            btnChuyenBan.Size = new Size(242, 40);
            btnChuyenBan.TabIndex = 2;
            btnChuyenBan.Text = "Huỷ Bàn";
            btnChuyenBan.UseVisualStyleBackColor = false;
            btnChuyenBan.Click += btnChuyenBan_Click;
            // 
            // btnDatMon
            // 
            btnDatMon.BackColor = Color.Silver;
            btnDatMon.FlatStyle = FlatStyle.Flat;
            btnDatMon.Font = new Font("Microsoft Sans Serif", 12F, FontStyle.Bold);
            btnDatMon.Location = new Point(143, 73);
            btnDatMon.Name = "btnDatMon";
            btnDatMon.Size = new Size(242, 40);
            btnDatMon.TabIndex = 1;
            btnDatMon.Text = "Đặt Món/Thanh Toán";
            btnDatMon.UseVisualStyleBackColor = false;
            btnDatMon.Click += btnDatMon_Click;
            // 
            // pictureBoxCharacter
            // 
            pictureBoxCharacter.Image = Properties.Resources.bagia;
            pictureBoxCharacter.Location = new Point(33, 20);
            pictureBoxCharacter.Name = "pictureBoxCharacter";
            pictureBoxCharacter.Size = new Size(689, 384);
            pictureBoxCharacter.SizeMode = PictureBoxSizeMode.StretchImage;
            pictureBoxCharacter.TabIndex = 2;
            pictureBoxCharacter.TabStop = false;
            // 
            // fDatBan_ChonHanhDong
            // 
            ClientSize = new Size(732, 420);
            ControlBox = false;
            Controls.Add(panelBackground);
            FormBorderStyle = FormBorderStyle.None;
            Name = "fDatBan_ChonHanhDong";
            StartPosition = FormStartPosition.CenterScreen;
            panelBackground.ResumeLayout(false);
            panelWhiteBox.ResumeLayout(false);
            panelWhiteBox.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBoxCharacter).EndInit();
            ResumeLayout(false);
        }

        private System.Windows.Forms.Panel panelBackground;
        private System.Windows.Forms.Panel panelWhiteBox;
        private System.Windows.Forms.Label labelMessage;
        private System.Windows.Forms.Button btnDatMon;
        private System.Windows.Forms.PictureBox pictureBoxCharacter;
        private Button btnChuyenBan;
        private Button btnXoaAnh;
    }
}