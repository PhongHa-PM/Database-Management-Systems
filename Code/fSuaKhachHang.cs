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
    public partial class fSuaKhachHang : Form
    {
        DataProvider dataProvider = new DataProvider();
        public int MaKH { get; set; }
        public string HoTen { get; set; }
        public string SDT { get; set; }
        public DateTime NgaySinh { get; set; }
        public string Email { get; set; }
        public int DiemTichLuy { get; set; }

        public fSuaKhachHang()
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
        private void UpdateKhachHang(int maKH, string hoTen, string sdt, DateTime ngaySinh, string email, int diemTichLuy)
        {
            // Kiểm tra trùng lặp số điện thoại
            string checkQuery = "SELECT COUNT(*) FROM KhachHang WHERE SDT = @SDT AND MaKH != @MaKH";
            SqlParameter[] checkParams = {
        new SqlParameter("@SDT", sdt),
        new SqlParameter("@MaKH", maKH)
    };

            object checkResult = dataProvider.ExecScalar(checkQuery, checkParams);

            if (checkResult != null && int.TryParse(checkResult.ToString(), out int exists) && exists > 0)
            {
                MessageBox.Show("Số điện thoại này đã tồn tại! Vui lòng nhập số điện thoại khác.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return; // Nếu số điện thoại đã tồn tại, dừng lại không cập nhật
            }

            // Câu lệnh SQL để cập nhật dữ liệu khách hàng
            string query = "UPDATE KhachHang SET HoTen = @HoTen, SDT = @SDT, NgaySinh = @NgaySinh, Email = @Email, DiemTichLuy = @DiemTichLuy WHERE MaKH = @MaKH";

            // Tạo tham số cho câu lệnh SQL
            SqlParameter[] parameters = new SqlParameter[]
            {
        new SqlParameter("@MaKH", maKH),
        new SqlParameter("@HoTen", hoTen),
        new SqlParameter("@SDT", sdt),
        new SqlParameter("@NgaySinh", ngaySinh),
        new SqlParameter("@Email", email),
        new SqlParameter("@DiemTichLuy", diemTichLuy)
            };

            // Thực thi câu lệnh SQL để cập nhật dữ liệu
            int result = dataProvider.ExecNonQuery(query, parameters);
            if (result > 0)
            {
                MessageBox.Show("Cập nhật khách hàng thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("Cập nhật khách hàng thất bại!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
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

            // Kiểm tra điểm tích lũy (phải là số nguyên >= 0)
            if (!int.TryParse(diemTichLuyText, out diemTichLuy) || diemTichLuy < 0)
            {
                MessageBox.Show("Điểm tích lũy phải là số không âm!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Kiểm tra ngày sinh (không cho phép ngày sinh trong tương lai)
            if (ngaySinh > DateTime.Now)
            {
                MessageBox.Show("Ngày sinh không hợp lệ! Ngày sinh không thể nằm trong tương lai.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Kiểm tra trùng lặp số điện thoại
            string checkQuery = "SELECT COUNT(*) FROM KhachHang WHERE SDT = @SDT AND MaKH != @MaKH";
            SqlParameter[] checkParams = {
        new SqlParameter("@SDT", sdt),
        new SqlParameter("@MaKH", MaKH)
    };

            object checkResult = dataProvider.ExecScalar(checkQuery, checkParams);

            if (checkResult != null && int.TryParse(checkResult.ToString(), out int exists) && exists > 0)
            {
                MessageBox.Show("Số điện thoại này đã tồn tại! Vui lòng nhập số điện thoại khác.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return; // Dừng lại nếu trùng số điện thoại
            }

            // Cập nhật dữ liệu vào cơ sở dữ liệu
            UpdateKhachHang(MaKH, hoTen, sdt, ngaySinh, email, diemTichLuy);

            // Đóng form sửa nếu thành công
            this.Close();
        }


        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void fSuaKhachHang_Load(object sender, EventArgs e)
        {
            txtTen.Text = HoTen;
            txtSDT.Text = SDT;
            dateTimePicker1.Value = NgaySinh;
            txtEmail.Text = Email;
            txtDiem.Text = DiemTichLuy.ToString();
        }

        private void btnHuyBo_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
