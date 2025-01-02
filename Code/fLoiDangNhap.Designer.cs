namespace QL_Bida
{
    partial class fLoiDangNhap
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
            lblMessage = new Label();
            picCrocodile = new PictureBox();
            btnThoat = new Button();
            ((System.ComponentModel.ISupportInitialize)picCrocodile).BeginInit();
            SuspendLayout();
            // 
            // lblMessage
            // 
            lblMessage.Font = new Font("Microsoft Sans Serif", 14F);
            lblMessage.ForeColor = Color.White;
            lblMessage.Location = new Point(28, 35);
            lblMessage.Name = "lblMessage";
            lblMessage.Size = new Size(418, 263);
            lblMessage.TabIndex = 0;
            lblMessage.Text = "Tên đăng nhập hoặc mật khẩu không chính xác.\nVui lòng nhập lại!";
            lblMessage.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // picCrocodile
            // 
            picCrocodile.ErrorImage = Properties.Resources.crocodile;
            picCrocodile.Image = Properties.Resources.crocodile;
            picCrocodile.Location = new Point(452, 35);
            picCrocodile.Name = "picCrocodile";
            picCrocodile.Size = new Size(241, 207);
            picCrocodile.SizeMode = PictureBoxSizeMode.StretchImage;
            picCrocodile.TabIndex = 1;
            picCrocodile.TabStop = false;
            // 
            // btnThoat
            // 
            btnThoat.AutoSize = true;
            btnThoat.BackColor = Color.White;
            btnThoat.Cursor = Cursors.Hand;
            btnThoat.FlatAppearance.BorderSize = 0;
            btnThoat.FlatStyle = FlatStyle.Popup;
            btnThoat.Font = new Font("Arial", 12F, FontStyle.Bold, GraphicsUnit.Point, 163);
            btnThoat.ForeColor = Color.FromArgb(102, 44, 33);
            btnThoat.Location = new Point(291, 283);
            btnThoat.Name = "btnThoat";
            btnThoat.Size = new Size(108, 42);
            btnThoat.TabIndex = 6;
            btnThoat.Text = "Xác nhận";
            btnThoat.UseVisualStyleBackColor = false;
            btnThoat.Click += btnThoat_Click;
            // 
            // fLoiDangNhap
            // 
            BackColor = Color.FromArgb(131, 93, 78);
            ClientSize = new Size(708, 373);
            ControlBox = false;
            Controls.Add(btnThoat);
            Controls.Add(picCrocodile);
            Controls.Add(lblMessage);
            FormBorderStyle = FormBorderStyle.None;
            Name = "fLoiDangNhap";
            ShowIcon = false;
            StartPosition = FormStartPosition.CenterParent;
            Text = "Thông Báo";
            ((System.ComponentModel.ISupportInitialize)picCrocodile).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        private PictureBox picCrocodile;
        private Button btnThoat;
        public Label lblMessage;
    }
}