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
    public partial class fCapNhatKho : Form
    {
        DataProvider dataProvider = new DataProvider();
        public int MaSP { get; set; }
        public int SoLuong { get; set; }

        public fCapNhatKho()
        {
            InitializeComponent();
        }
        private void btnHuy_Click(object sender, EventArgs e)
        {

            this.Close();
        }
        private void UpdateKhoHang(int maSP, int soLuongMoi)
        {
            // Câu lệnh SQL để cập nhật số lượng và ngày nhập gần nhất trong bảng KhoHang
            string query = "UPDATE KhoHang SET SoLuong = @SoLuong, NgayNhapGanNhat = @NgayNhapGanNhat WHERE MaSP = @MaSP";

            // Thêm tham số vào câu lệnh SQL
            SqlParameter[] parameters = new SqlParameter[]
            {
        new SqlParameter("@MaSP", maSP),
        new SqlParameter("@SoLuong", soLuongMoi),
        new SqlParameter("@NgayNhapGanNhat", DateTime.Now)  // Ngày nhập là ngày hiện tại
            };

            // Thực thi câu lệnh cập nhật trong cơ sở dữ liệu
            int result = dataProvider.ExecNonQuery(query, parameters);
            if (result > 0)
            {
                MessageBox.Show("Cập nhật kho thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("Cập nhật kho thất bại!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnConfirm_Click(object sender, EventArgs e)
        {
            // Kiểm tra nếu số lượng là một giá trị hợp lệ
            if (int.TryParse(txtSoLuong.Text, out int newSoLuong))
            {
                // Cập nhật số lượng mới vào cơ sở dữ liệu
                UpdateKhoHang(MaSP, newSoLuong);

                // Đóng form sau khi cập nhật
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            else
            {
                MessageBox.Show("Vui lòng nhập số lượng hợp lệ.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void fCapNhatKho_Load(object sender, EventArgs e)
        {
            txtSoLuong.Text = SoLuong.ToString();
        }
    }
}
