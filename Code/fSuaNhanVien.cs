using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QL_Bida
{
    public partial class fSuaNhanVien : Form
    {
        public class ComboBoxItem
        {
            public object Value { get; set; }
            public string Text { get; set; }

            public override string ToString()
            {
                return Text;
            }
        }
        public int MaNV { get; set; }
        public string TenNV { get; set; }
        public string VaiTro { get; set; }
        public DateTime NgaySinh { get; set; }
        public string GioiTinh { get; set; }
        public int MaCa { get; set; }
        public string HinhAnh { get; set; }
        public string TenDangNhap { get; set; }
        public string MatKhau { get; set; }
        private string tenHinhAnh;
        DataProvider dataProvider = new DataProvider();
        public fSuaNhanVien()
        {
            InitializeComponent();
            this.FormBorderStyle = FormBorderStyle.None;
            this.Padding = new Padding(0);
            this.Region = System.Drawing.Region.FromHrgn(CreateRoundRectRgn(0, 0, Width, Height, 0, 0));
            this.StartPosition = FormStartPosition.CenterScreen;
          

            InitializeComboBox();
        }
        private void InitializeComboBox()
        {
            cboGioiTinh.DrawMode = DrawMode.OwnerDrawFixed;
            cboGioiTinh.FlatStyle = FlatStyle.Flat;
            cboGioiTinh.BackColor = Color.Silver;
            cboGioiTinh.ForeColor = Color.Black;
            cboGioiTinh.DrawItem += CbGioiTinh_DrawItem;
        }

        private void CbGioiTinh_DrawItem(object sender, DrawItemEventArgs e)
        {
            if (e.Index < 0) return;
            e.DrawBackground();
            using (Brush textBrush = new SolidBrush(e.ForeColor))
            {
                e.Graphics.DrawString(cboGioiTinh.Items[e.Index].ToString(), e.Font, textBrush, e.Bounds);
            }

            e.DrawFocusRectangle();
        }
        [DllImport("Gdi32.dll", EntryPoint = "CreateRoundRectRgn")]
        private static extern IntPtr CreateRoundRectRgn(int nLeftRect, int nTopRect, int nRightRect, int nBottomRect, int nWidthEllipse, int nHeightEllipse);


        private void LoadComboBoxData()
        {
            // Cài đặt dữ liệu cho ComboBox Giới tính (Nam/Nữ)
            cboGioiTinh.Items.Clear();
            cboGioiTinh.Items.Add("Nam");
            cboGioiTinh.Items.Add("Nữ");

            // Cài đặt dữ liệu cho ComboBox Vai trò - Load từ Database
            string queryVaiTro = "SELECT DISTINCT VaiTro FROM NhanVien";
            DataTable dtVaiTro = dataProvider.ExecQuery(queryVaiTro);
            cboVaiTro.Items.Clear();
            foreach (DataRow row in dtVaiTro.Rows)
            {
                cboVaiTro.Items.Add(row["VaiTro"].ToString());
            }

            // Cài đặt dữ liệu cho ComboBox Ca làm việc - Load từ Database
            string queryCaLamViec = "SELECT MaCa, TenCa FROM CaLamViec";
            DataTable dtCaLamViec = dataProvider.ExecQuery(queryCaLamViec);
            cboCa.DataSource = dtCaLamViec;
            cboCa.DisplayMember = "TenCa";
            cboCa.ValueMember = "MaCa";
        }


        private void HuyButton_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnHuyBo_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        // Khai báo biến trường để lưu tên ảnh
        private string tenAnh;

        // Phương thức để chọn ảnh
        private void btnChonAnh_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                openFileDialog.Filter = "Image Files|*.jpg;*.jpeg;*.png;*.bmp";
                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    // Lấy tên ảnh và đường dẫn đến thư mục lưu ảnh
                    tenAnh = Path.GetFileName(openFileDialog.FileName); // Lưu tên file vào biến
                    string destinationPath = Path.Combine(Application.StartupPath, "HinhAnh", "NhanVien", tenAnh);

                    // Tạo thư mục nếu chưa có
                    Directory.CreateDirectory(Path.GetDirectoryName(destinationPath));

                    // Copy ảnh vào thư mục
                    File.Copy(openFileDialog.FileName, destinationPath, true);

                    // Hiển thị ảnh trong PictureBox
                    pictureBox1.Image = Image.FromFile(destinationPath);
                }
            }
        }

        private void SaveEmployee()
        {
            try
            {
                // Lấy dữ liệu từ form
                string tenNV = txtTen.Text;
                string vaiTro = cboVaiTro.SelectedItem.ToString();
                DateTime ngaySinh = dateTimePicker1.Value;
                string gioiTinh = cboGioiTinh.SelectedItem.ToString();
                int maCa = (cboCa.SelectedItem as ComboBoxItem)?.Value as int? ?? 0; // Lấy MaCa từ ComboBox
                string hinhAnh = string.IsNullOrEmpty(tenAnh) ? null : tenAnh; // Kiểm tra nếu không có ảnh

                int maNV = 0; // Biến lưu mã nhân viên vừa được tạo

                // Kết nối và lưu dữ liệu vào bảng NhanVien
                using (SqlConnection connection = new SqlConnection(dataProvider.constr))
                {
                    connection.Open();

                    // Câu truy vấn chèn dữ liệu vào bảng NhanVien và lấy mã NV vừa được tạo
                    string insertNhanVienQuery = @"
                INSERT INTO NhanVien (TenNV, VaiTro, NgaySinh, GioiTinh, MaCa, HinhAnh)
                VALUES (@TenNV, @VaiTro, @NgaySinh, @GioiTinh, @MaCa, @HinhAnh);
                SELECT SCOPE_IDENTITY();"; // Lấy MaNV vừa được tạo

                    using (SqlCommand command = new SqlCommand(insertNhanVienQuery, connection))
                    {
                        // Thêm các tham số cho NhanVien
                        command.Parameters.AddWithValue("@TenNV", tenNV);
                        command.Parameters.AddWithValue("@VaiTro", vaiTro);
                        command.Parameters.AddWithValue("@NgaySinh", ngaySinh);
                        command.Parameters.AddWithValue("@GioiTinh", gioiTinh);
                        command.Parameters.AddWithValue("@MaCa", maCa);
                        command.Parameters.AddWithValue("@HinhAnh", (object)hinhAnh ?? DBNull.Value); // Thêm điều kiện null cho HinhAnh

                        // Lấy MaNV vừa được thêm
                        maNV = Convert.ToInt32(command.ExecuteScalar());
                    }

                    // Nếu là Admin hoặc Thu Ngân, lưu vào bảng NguoiDung
                    if (vaiTro == "Admin" || vaiTro == "Thu Ngân")
                    {
                        string tenDangNhap = txtTenDangNhap.Text;
                        string matKhau = txtMatKhau.Text;
                        string loaiNguoiDung = vaiTro;

                        string insertNguoiDungQuery = @"
                    INSERT INTO NguoiDung (MaNV, TenDangNhap, MatKhau, LoaiNguoiDung)
                    VALUES (@MaNV, @TenDangNhap, @MatKhau, @LoaiNguoiDung);";

                        using (SqlCommand commandNguoiDung = new SqlCommand(insertNguoiDungQuery, connection))
                        {
                            // Thêm các tham số cho NguoiDung
                            commandNguoiDung.Parameters.AddWithValue("@MaNV", maNV);
                            commandNguoiDung.Parameters.AddWithValue("@TenDangNhap", tenDangNhap);
                            commandNguoiDung.Parameters.AddWithValue("@MatKhau", matKhau);
                            commandNguoiDung.Parameters.AddWithValue("@LoaiNguoiDung", loaiNguoiDung);

                            // Thực thi câu lệnh
                            int rowsAffectedNguoiDung = commandNguoiDung.ExecuteNonQuery();

                            if (rowsAffectedNguoiDung > 0)
                            {
                                MessageBox.Show("Lưu thông tin tài khoản thành công!", "Thông báo");
                            }
                            else
                            {
                                MessageBox.Show("Lưu thông tin tài khoản thất bại!", "Thông báo");
                            }
                        }
                    }

                    MessageBox.Show("Lưu thông tin nhân viên thành công!", "Thông báo");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lưu thông tin thất bại: " + ex.Message, "Thông báo");
            }
        }






        // Hàm chuyển ảnh từ PictureBox thành mảng byte
        private byte[] ImageToByteArray(Image image)
        {
            if (image == null) return null;

            using (var ms = new System.IO.MemoryStream())
            {
                image.Save(ms, image.RawFormat);
                return ms.ToArray();
            }
        }

        private void btnXoaAnh_Click(object sender, EventArgs e)
        {
            pictureBox1.Image = null;
        }




        private void btnLuu_Click(object sender, EventArgs e)
        {
            try
            {
                // Kiểm tra nếu tên nhân viên bị bỏ trống
                if (string.IsNullOrWhiteSpace(txtTen.Text))
                {
                    MessageBox.Show("Vui lòng nhập tên nhân viên!", "Thông báo");
                    txtTen.Focus(); // Đưa con trỏ về ô nhập tên
                    return; // Dừng lại
                }

                // Lấy giá trị ngày sinh
                DateTime ngaySinh = dateTimePicker1.Value;

                // Kiểm tra ngày sinh không được lớn hơn ngày hiện tại
                if (ngaySinh > DateTime.Now)
                {
                    MessageBox.Show("Ngày sinh không được lớn hơn ngày hiện tại!", "Thông báo");
                    dateTimePicker1.Focus(); // Đưa con trỏ về ô chọn ngày
                    return; // Dừng lại
                }

                // Kiểm tra mật khẩu và xác nhận mật khẩu
                if (txtMatKhau.Text != txtXacNhanMK.Text)
                {
                    MessageBox.Show("Mật khẩu và xác nhận mật khẩu không khớp! Vui lòng nhập lại.", "Thông báo");
                    txtXacNhanMK.Clear();
                    txtXacNhanMK.Focus();
                    return; // Dừng lại nếu không khớp
                }

                // Lấy dữ liệu từ form
                string tenNV = txtTen.Text.Trim();
                string vaiTro = cboVaiTro.SelectedItem?.ToString() ?? string.Empty;
                string gioiTinh = cboGioiTinh.SelectedItem?.ToString() ?? string.Empty;
                int maCa = Convert.ToInt32(cboCa.SelectedValue);

                // Kiểm tra nếu biến `tenAnh` chưa có giá trị thì gán giá trị từ thuộc tính `HinhAnh`
                if (string.IsNullOrEmpty(tenAnh))
                {
                    if (string.IsNullOrEmpty(HinhAnh))
                    {
                        MessageBox.Show("Vui lòng chọn hình ảnh!", "Thông báo");
                        return; // Dừng lại nếu không có hình ảnh
                    }
                    tenAnh = HinhAnh; // Sử dụng tên ảnh cũ từ thuộc tính HinhAnh
                }

                // Kiểm tra tên đăng nhập có tồn tại hay không
                string tenDangNhap = txtTenDangNhap.Text.Trim();
                if (IsTenDangNhapExists(tenDangNhap, MaNV)) // Hàm kiểm tra tên đăng nhập
                {
                    MessageBox.Show("Tên đăng nhập đã tồn tại! Vui lòng chọn tên khác.", "Thông báo");
                    return; // Dừng lại nếu tên đăng nhập đã tồn tại
                }

                // Cập nhật bảng NhanVien
                string updateNhanVienQuery = @"
        UPDATE NhanVien 
        SET TenNV = @TenNV, VaiTro = @VaiTro, NgaySinh = @NgaySinh, GioiTinh = @GioiTinh, MaCa = @MaCa, HinhAnh = @HinhAnh 
        WHERE MaNV = @MaNV";

                using (SqlConnection connection = new SqlConnection(dataProvider.constr))
                {
                    connection.Open();

                    using (SqlCommand command = new SqlCommand(updateNhanVienQuery, connection))
                    {
                        command.Parameters.AddWithValue("@TenNV", tenNV);
                        command.Parameters.AddWithValue("@VaiTro", vaiTro);
                        command.Parameters.AddWithValue("@NgaySinh", ngaySinh);
                        command.Parameters.AddWithValue("@GioiTinh", gioiTinh);
                        command.Parameters.AddWithValue("@MaCa", maCa);
                        command.Parameters.AddWithValue("@HinhAnh", tenAnh);
                        command.Parameters.AddWithValue("@MaNV", MaNV);

                        command.ExecuteNonQuery();
                    }

                    // Cập nhật thông tin đăng nhập nếu là Admin hoặc Thu Ngân
                    if (vaiTro == "Admin" || vaiTro == "Thu Ngân")
                    {
                        string matKhau = txtMatKhau.Text;

                        string updateNguoiDungQuery = @"
                UPDATE NguoiDung 
                SET TenDangNhap = @TenDangNhap, MatKhau = @MatKhau, LoaiNguoiDung = @LoaiNguoiDung 
                WHERE MaNV = @MaNV";

                        using (SqlCommand commandNguoiDung = new SqlCommand(updateNguoiDungQuery, connection))
                        {
                            commandNguoiDung.Parameters.AddWithValue("@TenDangNhap", tenDangNhap);
                            commandNguoiDung.Parameters.AddWithValue("@MatKhau", matKhau);
                            commandNguoiDung.Parameters.AddWithValue("@LoaiNguoiDung", vaiTro);
                            commandNguoiDung.Parameters.AddWithValue("@MaNV", MaNV);

                            commandNguoiDung.ExecuteNonQuery();
                        }
                    }
                }

                MessageBox.Show("Cập nhật thông tin nhân viên thành công!", "Thông báo");
                this.DialogResult = DialogResult.OK; // Đóng form với kết quả OK
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lưu thông tin thất bại: " + ex.Message, "Thông báo");
            }
        }

        private bool IsTenDangNhapExists(string tenDangNhap, int maNV)
        {
            // Kiểm tra tên đăng nhập có tồn tại hay không
            string checkQuery = @"
    SELECT COUNT(*) 
    FROM NguoiDung 
    WHERE TenDangNhap = @TenDangNhap AND MaNV != @MaNV";

            using (SqlConnection connection = new SqlConnection(dataProvider.constr))
            {
                connection.Open();
                using (SqlCommand command = new SqlCommand(checkQuery, connection))
                {
                    command.Parameters.AddWithValue("@TenDangNhap", tenDangNhap);
                    command.Parameters.AddWithValue("@MaNV", maNV); // Đảm bảo không kiểm tra chính nhân viên đang sửa

                    int count = Convert.ToInt32(command.ExecuteScalar());
                    return count > 0; // Nếu có kết quả > 0, nghĩa là tên đăng nhập đã tồn tại
                }
            }
        }





        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }
        private void cboVaiTro_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Kiểm tra vai trò được chọn
            string selectedRole = cboVaiTro.SelectedItem.ToString();

            // Kiểm tra nếu là Admin hoặc Thu Ngân
            if (selectedRole == "Admin" || selectedRole == "Thu Ngân")
            {
                // Nếu chọn Admin hoặc Thu Ngân thì enable các textbox Tài khoản và Mật khẩu
                txtTenDangNhap.Enabled = true;
                txtMatKhau.Enabled = true;
                txtXacNhanMK.Enabled = true;

                // Hiển thị Panel chứa các textbox
                pnlTaiKhoan.Visible = true;
            }
            else
            {
                // Nếu không phải Admin hoặc Thu Ngân, disable các textbox Tài khoản và Mật khẩu
                txtTenDangNhap.Enabled = false;
                txtMatKhau.Enabled = false;
                txtXacNhanMK.Enabled = false;

                // Ẩn Panel chứa các textbox
                pnlTaiKhoan.Visible = false;

                // Xóa dữ liệu trong các textbox khi không cần thiết
                txtXacNhanMK.Clear();
                txtTenDangNhap.Clear();
                txtMatKhau.Clear();
            }
        }
        public void SetImage(string fileName)
        {
            // Đường dẫn đầy đủ đến thư mục chứa ảnh của nhân viên
            string imagePath = Path.Combine(Application.StartupPath, "HinhAnh", "NhanVien", fileName);

            if (!string.IsNullOrEmpty(fileName) && File.Exists(imagePath))
            {
                pictureBox1.ImageLocation = imagePath; // Load ảnh trực tiếp từ file
            }
            else
            {
                pictureBox1.Image = null; // Nếu không có ảnh hoặc file không tồn tại, để trống
                MessageBox.Show("Không tìm thấy ảnh!", "Lỗi"); // Thông báo nếu không tìm thấy ảnh
            }
        }

        public void LoadData()
        {
            txtTen.Text = TenNV;
            cboVaiTro.SelectedItem = VaiTro;
            dateTimePicker1.Value = NgaySinh;
            cboGioiTinh.SelectedItem = GioiTinh;

            // Gán MaCa vào cboCa dựa trên MaCa đã chọn của nhân viên
            cboCa.SelectedValue = MaCa;

            txtTenDangNhap.Text = TenDangNhap;
            txtMatKhau.Text = MatKhau;

            // Hiển thị ảnh nếu có
            SetImage(HinhAnh); // Sử dụng tên file ảnh (không có đường dẫn đầy đủ)
        }


        private void fSuaNhanVien_Load(object sender, EventArgs e)
        {
            LoadComboBoxData();
            LoadData();

        }

        private void btnXemMK_Click(object sender, EventArgs e)
        {
            txtMatKhau.UseSystemPasswordChar = !txtMatKhau.UseSystemPasswordChar;
        }
    }
}
