namespace QL_Bida
{
    partial class fCapNhatKho
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
            panelWhiteBox = new Panel();
            txtSoLuong = new TextBox();
            label7 = new Label();
            labelMessage = new Label();
            btnHuy = new Button();
            btnConfirm = new Button();
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
            panelBackground.Controls.Add(panelWhiteBox);
            panelBackground.Controls.Add(pictureBoxCharacter);
            panelBackground.Location = new Point(0, 1);
            panelBackground.Name = "panelBackground";
            panelBackground.Size = new Size(731, 419);
            panelBackground.TabIndex = 0;
            // 
            // panelWhiteBox
            // 
            panelWhiteBox.BackColor = Color.White;
            panelWhiteBox.Controls.Add(txtSoLuong);
            panelWhiteBox.Controls.Add(label7);
            panelWhiteBox.Controls.Add(labelMessage);
            panelWhiteBox.Controls.Add(btnHuy);
            panelWhiteBox.Controls.Add(btnConfirm);
            panelWhiteBox.Location = new Point(98, 185);
            panelWhiteBox.Name = "panelWhiteBox";
            panelWhiteBox.Size = new Size(528, 186);
            panelWhiteBox.TabIndex = 0;
            // 
            // txtSoLuong
            // 
            txtSoLuong.BackColor = SystemColors.ScrollBar;
            txtSoLuong.BorderStyle = BorderStyle.None;
            txtSoLuong.Font = new Font("Arial", 16.2F, FontStyle.Bold);
            txtSoLuong.Location = new Point(258, 71);
            txtSoLuong.Name = "txtSoLuong";
            txtSoLuong.Size = new Size(125, 32);
            txtSoLuong.TabIndex = 4;
            // 
            // label7
            // 
            label7.AutoSize = true;
            label7.Font = new Font("Arial", 16.2F, FontStyle.Bold, GraphicsUnit.Point, 163);
            label7.Location = new Point(101, 69);
            label7.Name = "label7";
            label7.Size = new Size(151, 33);
            label7.TabIndex = 3;
            label7.Text = "Số lượng:";
            // 
            // labelMessage
            // 
            labelMessage.AutoSize = true;
            labelMessage.Font = new Font("Microsoft Sans Serif", 18F, FontStyle.Bold);
            labelMessage.Location = new Point(59, 17);
            labelMessage.Name = "labelMessage";
            labelMessage.Size = new Size(414, 36);
            labelMessage.TabIndex = 0;
            labelMessage.Text = "Nhập số lượng cần thay đổi!";
            labelMessage.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // btnHuy
            // 
            btnHuy.BackColor = Color.White;
            btnHuy.FlatStyle = FlatStyle.Flat;
            btnHuy.Font = new Font("Microsoft Sans Serif", 12F, FontStyle.Bold);
            btnHuy.Image = Properties.Resources.x;
            btnHuy.Location = new Point(271, 126);
            btnHuy.Name = "btnHuy";
            btnHuy.Size = new Size(58, 45);
            btnHuy.TabIndex = 2;
            btnHuy.UseVisualStyleBackColor = false;
            btnHuy.Click += btnHuy_Click;
            // 
            // btnConfirm
            // 
            btnConfirm.BackColor = Color.White;
            btnConfirm.FlatStyle = FlatStyle.Flat;
            btnConfirm.Font = new Font("Microsoft Sans Serif", 12F, FontStyle.Bold);
            btnConfirm.Location = new Point(184, 126);
            btnConfirm.Name = "btnConfirm";
            btnConfirm.Size = new Size(58, 45);
            btnConfirm.TabIndex = 1;
            btnConfirm.Text = "✔";
            btnConfirm.UseVisualStyleBackColor = false;
            btnConfirm.Click += btnConfirm_Click;
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
            // fCapNhatKho
            // 
            ClientSize = new Size(732, 420);
            ControlBox = false;
            Controls.Add(panelBackground);
            FormBorderStyle = FormBorderStyle.None;
            Name = "fCapNhatKho";
            StartPosition = FormStartPosition.CenterScreen;
            Load += fCapNhatKho_Load;
            panelBackground.ResumeLayout(false);
            panelWhiteBox.ResumeLayout(false);
            panelWhiteBox.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBoxCharacter).EndInit();
            ResumeLayout(false);
        }

        private System.Windows.Forms.Panel panelBackground;
        private System.Windows.Forms.Panel panelWhiteBox;
        private System.Windows.Forms.Label labelMessage;
        private System.Windows.Forms.Button btnConfirm;
        private System.Windows.Forms.PictureBox pictureBoxCharacter;
        private Button btnHuy;
        private Label label7;
        private TextBox txtSoLuong;
    }
}