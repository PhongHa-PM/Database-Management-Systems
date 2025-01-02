namespace QL_Bida
{
    partial class fXacNhanXoa
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
            panelWhiteBox.Controls.Add(labelMessage);
            panelWhiteBox.Controls.Add(btnHuy);
            panelWhiteBox.Controls.Add(btnConfirm);
            panelWhiteBox.Location = new Point(98, 185);
            panelWhiteBox.Name = "panelWhiteBox";
            panelWhiteBox.Size = new Size(528, 186);
            panelWhiteBox.TabIndex = 0;
            // 
            // labelMessage
            // 
            labelMessage.AutoSize = true;
            labelMessage.Font = new Font("Microsoft Sans Serif", 18F, FontStyle.Bold);
            labelMessage.Location = new Point(140, 65);
            labelMessage.Name = "labelMessage";
            labelMessage.Size = new Size(236, 36);
            labelMessage.TabIndex = 0;
            labelMessage.Text = "Bạn muốn xoá?";
            labelMessage.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // btnHuy
            // 
            btnHuy.BackColor = Color.White;
            btnHuy.FlatStyle = FlatStyle.Flat;
            btnHuy.Font = new Font("Microsoft Sans Serif", 12F, FontStyle.Bold);
            btnHuy.Image = Properties.Resources.x;
            btnHuy.Location = new Point(270, 116);
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
            btnConfirm.Location = new Point(184, 116);
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
            // fXacNhanXoa
            // 
            ClientSize = new Size(732, 420);
            ControlBox = false;
            Controls.Add(panelBackground);
            FormBorderStyle = FormBorderStyle.None;
            Name = "fXacNhanXoa";
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
        private System.Windows.Forms.Button btnConfirm;
        private System.Windows.Forms.PictureBox pictureBoxCharacter;
        private Button btnHuy;
    }
}