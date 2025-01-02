namespace QL_Bida
{
    partial class fDangNhap
    {
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.TextBox txtUsername;
        private System.Windows.Forms.TextBox txtPassword;
        private System.Windows.Forms.Button btnLogin;
        private System.Windows.Forms.RadioButton rdoCashier;
        private System.Windows.Forms.RadioButton rdoAdmin;
        private System.Windows.Forms.Label lblUsername;
        private System.Windows.Forms.Label lblPassword;
        private System.Windows.Forms.PictureBox pictureBoxLogo;

        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(fDangNhap));
            txtUsername = new TextBox();
            txtPassword = new TextBox();
            btnLogin = new Button();
            rdoCashier = new RadioButton();
            rdoAdmin = new RadioButton();
            lblUsername = new Label();
            lblPassword = new Label();
            pictureBoxLogo = new PictureBox();
            panel1 = new Panel();
            btnXemMK = new Button();
            ((System.ComponentModel.ISupportInitialize)pictureBoxLogo).BeginInit();
            panel1.SuspendLayout();
            SuspendLayout();
            // 
            // txtUsername
            // 
            txtUsername.Font = new Font("Segoe UI", 16.2F, FontStyle.Regular, GraphicsUnit.Point, 163);
            txtUsername.Location = new Point(324, 225);
            txtUsername.Name = "txtUsername";
            txtUsername.Size = new Size(446, 43);
            txtUsername.TabIndex = 2;
            // 
            // txtPassword
            // 
            txtPassword.Font = new Font("Segoe UI", 16.2F, FontStyle.Regular, GraphicsUnit.Point, 163);
            txtPassword.Location = new Point(324, 339);
            txtPassword.Name = "txtPassword";
            txtPassword.Size = new Size(446, 43);
            txtPassword.TabIndex = 4;
            txtPassword.UseSystemPasswordChar = true;
            // 
            // btnLogin
            // 
            btnLogin.BackColor = Color.Brown;
            btnLogin.FlatStyle = FlatStyle.Flat;
            btnLogin.ForeColor = Color.White;
            btnLogin.Location = new Point(324, 480);
            btnLogin.Name = "btnLogin";
            btnLogin.Size = new Size(446, 69);
            btnLogin.TabIndex = 6;
            btnLogin.Text = "ĐĂNG NHẬP";
            btnLogin.UseVisualStyleBackColor = false;
            btnLogin.Click += btnLogin_Click;
            // 
            // rdoCashier
            // 
            rdoCashier.AutoSize = true;
            rdoCashier.Location = new Point(12, 14);
            rdoCashier.Name = "rdoCashier";
            rdoCashier.Size = new Size(122, 32);
            rdoCashier.TabIndex = 0;
            rdoCashier.Text = "Thu ngân";
            rdoCashier.CheckedChanged += rdoCashier_CheckedChanged;
            // 
            // rdoAdmin
            // 
            rdoAdmin.AutoSize = true;
            rdoAdmin.Location = new Point(158, 14);
            rdoAdmin.Name = "rdoAdmin";
            rdoAdmin.Size = new Size(95, 32);
            rdoAdmin.TabIndex = 1;
            rdoAdmin.Text = "Admin";
            rdoAdmin.CheckedChanged += rdoAdmin_CheckedChanged;
            // 
            // lblUsername
            // 
            lblUsername.AutoSize = true;
            lblUsername.Location = new Point(324, 194);
            lblUsername.Name = "lblUsername";
            lblUsername.Size = new Size(152, 28);
            lblUsername.TabIndex = 1;
            lblUsername.Text = "Tên đăng nhập";
            // 
            // lblPassword
            // 
            lblPassword.AutoSize = true;
            lblPassword.Location = new Point(324, 308);
            lblPassword.Name = "lblPassword";
            lblPassword.Size = new Size(104, 28);
            lblPassword.TabIndex = 3;
            lblPassword.Text = "Mật Khẩu";
            // 
            // pictureBoxLogo
            // 
            pictureBoxLogo.Image = (Image)resources.GetObject("pictureBoxLogo.Image");
            pictureBoxLogo.Location = new Point(421, 3);
            pictureBoxLogo.Name = "pictureBoxLogo";
            pictureBoxLogo.Size = new Size(266, 179);
            pictureBoxLogo.SizeMode = PictureBoxSizeMode.StretchImage;
            pictureBoxLogo.TabIndex = 0;
            pictureBoxLogo.TabStop = false;
            // 
            // panel1
            // 
            panel1.Controls.Add(rdoCashier);
            panel1.Controls.Add(rdoAdmin);
            panel1.Location = new Point(404, 400);
            panel1.Name = "panel1";
            panel1.Size = new Size(294, 59);
            panel1.TabIndex = 7;
            // 
            // btnXemMK
            // 
            btnXemMK.BackColor = Color.DarkGray;
            btnXemMK.BackgroundImage = (Image)resources.GetObject("btnXemMK.BackgroundImage");
            btnXemMK.BackgroundImageLayout = ImageLayout.Zoom;
            btnXemMK.Location = new Point(776, 345);
            btnXemMK.Name = "btnXemMK";
            btnXemMK.Size = new Size(44, 37);
            btnXemMK.TabIndex = 8;
            btnXemMK.UseVisualStyleBackColor = true;
            btnXemMK.Click += btnXemMK_Click;
            // 
            // fDangNhap
            // 
            BackColor = Color.FromArgb(221, 190, 169);
            ClientSize = new Size(1093, 619);
            Controls.Add(btnXemMK);
            Controls.Add(panel1);
            Controls.Add(pictureBoxLogo);
            Controls.Add(lblUsername);
            Controls.Add(txtUsername);
            Controls.Add(lblPassword);
            Controls.Add(txtPassword);
            Controls.Add(btnLogin);
            Font = new Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point, 163);
            Name = "fDangNhap";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Login";
            FormClosing += fDangNhap_FormClosing;
            FormClosed += fDangNhap_FormClosed;
            Load += fDangNhap_Load;
            ((System.ComponentModel.ISupportInitialize)pictureBoxLogo).EndInit();
            panel1.ResumeLayout(false);
            panel1.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        private Panel panel1;
        public Button btnXemMK;
    }
}