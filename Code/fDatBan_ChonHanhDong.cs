using QL_Bida.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QL_Bida
{
    public partial class fDatBan_ChonHanhDong : Form
    {
        public int MaBan { get; set; } // Thuộc tính để nhận mã bàn từ form cha
        DataProvider dataProvider = new DataProvider();
        private Dictionary<int, fThanhToan> danhSachThanhToan = new Dictionary<int, fThanhToan>();
        public fDatBan_ChonHanhDong()
        {
            InitializeComponent();
        }


        private void btnDatMon_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void btnChuyenBan_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.No;
            this.Close();
        }

        private void btnXoaAnh_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }

}
