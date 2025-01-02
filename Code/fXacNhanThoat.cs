using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QL_Bida
{
    public partial class fXacNhanThoat : Form
    {
        public fXacNhanThoat()
        {
            InitializeComponent();
        }




        private void btnHuy_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel; // Hủy, chỉ đóng form fXacNhanThoat
            this.Close();
        }

        private void btnConfirm_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.OK; // Xác nhận thoát
            this.Close();
        }

    }
}
