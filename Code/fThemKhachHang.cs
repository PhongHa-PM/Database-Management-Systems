using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QL_Bida
{
    public partial class fThemKhachHang : Form
    {


        DataProvider dataProvider = new DataProvider();
        public fThemKhachHang()
        {
            InitializeComponent();
            this.FormBorderStyle = FormBorderStyle.None;
            this.Padding = new Padding(0);
            this.Region = System.Drawing.Region.FromHrgn(CreateRoundRectRgn(0, 0, Width, Height, 0, 0));
            this.StartPosition = FormStartPosition.CenterScreen;
        }

        [DllImport("Gdi32.dll", EntryPoint = "CreateRoundRectRgn")]
        private static extern IntPtr CreateRoundRectRgn(int nLeftRect, int nTopRect, int nRightRect, int nBottomRect, int nWidthEllipse, int nHeightEllipse);


        private void HuyButton_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        private void btnLuu_Click(object sender, EventArgs e)
        {
            // Lấy thông tin từ các TextBox và DateTimePicker
            string hoTen = txtTen.Text.Trim();
            string sdt = txtSDT.Text.Trim();
            DateTime ngaySinh = dateTimePicker1.Value;
            string email = txtEmail.Text.Trim();
            string diemTichLuyText = txtDiem.Text.Trim();
            int diemTichLuy;

            // Kiểm tra tính hợp lệ của dữ liệu
            if (string.IsNullOrWhiteSpace(hoTen) || string.IsNullOrWhiteSpace(sdt) || string.IsNullOrWhiteSpace(email) || string.IsNullOrWhiteSpace(diemTichLuyText))
            {
                MessageBox.Show("Vui lòng nhập đầy đủ thông tin!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Kiểm tra định dạng số điện thoại (10 chữ số, bắt đầu bằng 0)
            if (!Regex.IsMatch(sdt, @"^0[0-9]{9}$"))
            {
                MessageBox.Show("Số điện thoại không đúng định dạng! Vui lòng nhập số điện thoại gồm 10 chữ số và bắt đầu bằng 0.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Kiểm tra định dạng email
            if (!Regex.IsMatch(email, @"^[^@\s]+@[^@\s]+\.[^@\s]+$"))
            {
                MessageBox.Show("Email không đúng định dạng! Vui lòng nhập đúng định dạng email (ví dụ: example@gmail.com).", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Kiểm tra điểm tích lũy (phải là số nguyên không âm)
            if (!int.TryParse(diemTichLuyText, out diemTichLuy) || diemTichLuy < 0)
            {
                MessageBox.Show("Điểm tích lũy phải là số không âm!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Kiểm tra trùng lặp số điện thoại
            string checkQuery = "SELECT COUNT(*) FROM KhachHang WHERE SDT = @SDT";
            SqlParameter[] checkParams = { new SqlParameter("@SDT", sdt) };

            object checkResult = dataProvider.ExecScalar(checkQuery, checkParams);

            if (checkResult != null && int.TryParse(checkResult.ToString(), out int exists))
            {
                if (exists > 0)
                {
                    MessageBox.Show("Số điện thoại này đã tồn tại! Vui lòng nhập số điện thoại khác.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
            }

            // Câu lệnh SQL để thêm khách hàng vào cơ sở dữ liệu
            string query = "INSERT INTO KhachHang (HoTen, SDT, NgaySinh, Email, DiemTichLuy) VALUES (@HoTen, @SDT, @NgaySinh, @Email, @DiemTichLuy)";

            // Thêm tham số vào câu lệnh
            SqlParameter[] parameters = new SqlParameter[]
            {
        new SqlParameter("@HoTen", hoTen),
        new SqlParameter("@SDT", sdt),
        new SqlParameter("@NgaySinh", ngaySinh),
        new SqlParameter("@Email", email),
        new SqlParameter("@DiemTichLuy", diemTichLuy)
            };

            // Gọi phương thức ExecNonQuery để thực hiện câu lệnh SQL
            int insertResult = dataProvider.ExecNonQuery(query, parameters);
            if (insertResult > 0)
            {
                MessageBox.Show("Thêm khách hàng thành công!");
                this.Close(); // Đóng form sau khi lưu thành công
                              // Cập nhật lại dữ liệu cho DataGridView trong form chính nếu cần
                              // LoadKhachHangData(); (Nếu bạn có hàm này để cập nhật lại DataGridView)
            }
            else
            {
                MessageBox.Show("Thêm khách hàng thất bại!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }



        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void btnHuyBo_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
