using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Data.SqlClient;

namespace QL_Bida
{


    public partial class fDangNhap : Form
    {
        DataProvider dataProvider = new DataProvider();
        public static bool IsExiting = false;
        public fDangNhap()
        {
            InitializeComponent();
        }


        private string KiemTraDangNhap(string tenDangNhap, string matKhau)
        {
            // Truy vấn SQL để kiểm tra tài khoản
            string query = $"SELECT LoaiNguoiDung FROM NguoiDung WHERE TenDangNhap = '{tenDangNhap}' AND MatKhau = '{matKhau}'";

            // Tạo đối tượng DataProvider để lấy dữ liệu
            DataProvider dataProvider = new DataProvider();

            // Thực thi truy vấn và lấy kết quả vào DataTable
            DataTable data = dataProvider.ExecQuery(query);

            // Kiểm tra nếu có dữ liệu trả về thì lấy giá trị của LoaiNguoiDung
            if (data.Rows.Count > 0)
            {
                return data.Rows[0]["LoaiNguoiDung"].ToString();
            }
            else
            {
                return null; // Trả về null nếu không tìm thấy người dùng
            }
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            string tenDangNhap = txtUsername.Text;
            string matKhau = txtPassword.Text;
            string selectedRole = "";

            // Kiểm tra nếu người dùng đã chọn RadioButton Admin hay Thu Ngân
            if (rdoAdmin.Checked)
            {
                selectedRole = "Admin";
            }
            else if (rdoCashier.Checked)
            {
                selectedRole = "Thu Ngân";
            }

            // Kiểm tra nếu người dùng chưa chọn vai trò
            if (string.IsNullOrEmpty(selectedRole))
            {
                // Hiển thị form lỗi đăng nhập với thông báo "Vui lòng chọn vai trò!"
                fLoiDangNhap formLoiDangNhap = new fLoiDangNhap();
                formLoiDangNhap.lblMessage.Text = "Vui lòng chọn vai trò!";
                formLoiDangNhap.ShowDialog();
                return; // Dừng hàm để không kiểm tra tiếp
            }

            // Gọi phương thức KiemTraDangNhap để lấy loại người dùng từ cơ sở dữ liệu
            string loaiNguoiDung = KiemTraDangNhap(tenDangNhap, matKhau);

            // Kiểm tra xem thông tin đăng nhập có khớp với quyền người dùng đã chọn hay không
            if (loaiNguoiDung == "Admin" && selectedRole == "Admin")
            {
                // Mở form dành cho Admin
                fHome formHome = new fHome(tenDangNhap);
                formHome.Show();
                this.Hide();
            }
            else if (loaiNguoiDung == "Thu Ngân" && selectedRole == "Thu Ngân")
            {
                // Mở form dành cho Thu Ngân
                fNhanVien formNhanVien = new fNhanVien(tenDangNhap);
                formNhanVien.Show();
                this.Hide();
            }
            else
            {
                // Hiển thị form lỗi đăng nhập với thông báo tùy chỉnh
                fLoiDangNhap formLoiDangNhap = new fLoiDangNhap();

                // Kiểm tra quyền đăng nhập và RadioButton đã chọn để đưa ra thông báo phù hợp
                if (loaiNguoiDung == "Admin" && selectedRole == "Thu Ngân")
                {
                    formLoiDangNhap.lblMessage.Text = "Bạn không phải là Thu Ngân!";
                }
                else if (loaiNguoiDung == "Thu Ngân" && selectedRole == "Admin")
                {
                    formLoiDangNhap.lblMessage.Text = "Bạn không phải là Admin!";
                }
                else
                {
                    formLoiDangNhap.lblMessage.Text = "Tên đăng nhập hoặc mật khẩu không đúng!";
                }

                formLoiDangNhap.ShowDialog();
            }
        }






        private void fDangNhap_Load(object sender, EventArgs e)
        {

        }




        private void fDangNhap_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (!IsExiting) // Kiểm tra cờ trạng thái
            {
                var result = MessageBox.Show("Bạn có chắc chắn muốn thoát không?", "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (result == DialogResult.No)
                {
                    e.Cancel = true; // Hủy nếu chọn "No"
                }
                else
                {
                    IsExiting = true; // Đặt cờ khi người dùng xác nhận thoát
                    Application.Exit(); // Thoát ứng dụng
                }
            }
        }

        private void fDangNhap_FormClosed(object sender, FormClosedEventArgs e)
        {

        }

        private void btnXemMK_Click(object sender, EventArgs e)
        {
            txtPassword.UseSystemPasswordChar = !txtPassword.UseSystemPasswordChar;
        }

        // Biến lưu loại người dùng được chọn
        private string selectedRole = "Admin"; // Mặc định là Admin

        private void rdoAdmin_CheckedChanged(object sender, EventArgs e)
        {
            if (rdoAdmin.Checked)
            {
                selectedRole = "Admin";
            }
        }

        private void rdoCashier_CheckedChanged(object sender, EventArgs e)
        {
            if (rdoCashier.Checked)
            {
                selectedRole = "Thu Ngân";
            }
        }

    }
}
