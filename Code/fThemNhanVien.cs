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

    public partial class fThemNhanVien : Form
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
        private string tenHinhAnh;
        DataProvider dataProvider = new DataProvider();
        public fThemNhanVien()
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
            string queryVaiTro = "SELECT DISTINCT VaiTro FROM NhanVien"; // Tùy vào bảng của bạn
            DataTable dtVaiTro = dataProvider.ExecQuery(queryVaiTro);
            cboVaiTro.Items.Clear();
            foreach (DataRow row in dtVaiTro.Rows)
            {
                cboVaiTro.Items.Add(row["VaiTro"].ToString());
            }

            // Cài đặt dữ liệu cho ComboBox Ca làm việc - Load từ Database
            string queryCaLamViec = "SELECT MaCa, TenCa FROM CaLamViec"; // Giả sử bảng CaLamViec chứa MaCa và TenCa
            DataTable dtCaLamViec = dataProvider.ExecQuery(queryCaLamViec);
            cboCa.Items.Clear();
            foreach (DataRow row in dtCaLamViec.Rows)
            {
                cboCa.Items.Add(new ComboBoxItem
                {
                    Value = row["MaCa"],
                    Text = row["TenCa"].ToString()
                });
            }
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

        private bool SaveEmployee()
        {
            try
            {
                // Lấy dữ liệu từ form
                string tenNV = txtTen.Text.Trim();
                string vaiTro = cboVaiTro.SelectedItem?.ToString() ?? string.Empty;
                DateTime ngaySinh = dateTimePicker1.Value;
                string gioiTinh = cboGioiTinh.SelectedItem?.ToString() ?? string.Empty;
                int maCa = (cboCa.SelectedItem as ComboBoxItem)?.Value as int? ?? 0; // Lấy MaCa từ ComboBox
                string hinhAnh = tenAnh;

                // Kiểm tra thông tin bắt buộc
                if (string.IsNullOrEmpty(tenNV))
                {
                    MessageBox.Show("Vui lòng nhập tên nhân viên.", "Thông báo");
                    txtTen.Focus();
                    return false;
                }

                if (string.IsNullOrEmpty(vaiTro))
                {
                    MessageBox.Show("Vui lòng chọn vai trò cho nhân viên.", "Thông báo");
                    cboVaiTro.Focus();
                    return false;
                }

                if (string.IsNullOrEmpty(gioiTinh))
                {
                    MessageBox.Show("Vui lòng chọn giới tính.", "Thông báo");
                    cboGioiTinh.Focus();
                    return false;
                }

                if (maCa == 0)
                {
                    MessageBox.Show("Vui lòng chọn ca làm việc.", "Thông báo");
                    cboCa.Focus();
                    return false;
                }

                // Kiểm tra ngày sinh
                if (ngaySinh > DateTime.Now)
                {
                    MessageBox.Show("Ngày sinh không được lớn hơn ngày hiện tại.", "Thông báo");
                    dateTimePicker1.Focus();
                    return false;
                }

                if (string.IsNullOrEmpty(hinhAnh))
                {
                    MessageBox.Show("Vui lòng chọn hình ảnh cho nhân viên.", "Thông báo");
                    return false;
                }

                // Nếu là Admin hoặc Thu Ngân, kiểm tra mật khẩu và xác nhận mật khẩu
                if (vaiTro == "Admin" || vaiTro == "Thu Ngân")
                {
                    string tenDangNhap = txtTenDangNhap.Text.Trim();
                    string matKhau = txtMatKhau.Text;
                    string xacNhanMK = txtXacNhanMK.Text;

                    if (string.IsNullOrEmpty(tenDangNhap))
                    {
                        MessageBox.Show("Vui lòng nhập tên đăng nhập.", "Thông báo");
                        txtTenDangNhap.Focus();
                        return false;
                    }

                    if (string.IsNullOrEmpty(matKhau))
                    {
                        MessageBox.Show("Vui lòng nhập mật khẩu.", "Thông báo");
                        txtMatKhau.Focus();
                        return false;
                    }

                    if (matKhau != xacNhanMK)
                    {
                        MessageBox.Show("Mật khẩu và xác nhận mật khẩu không khớp! Vui lòng nhập lại.", "Thông báo");
                        txtXacNhanMK.Clear();
                        txtXacNhanMK.Focus();
                        return false;
                    }

                    // Kiểm tra tên đăng nhập đã tồn tại trong bảng NguoiDung
                    using (SqlConnection connection = new SqlConnection(dataProvider.constr))
                    {
                        connection.Open();
                        string checkUsernameQuery = "SELECT COUNT(*) FROM NguoiDung WHERE TenDangNhap = @TenDangNhap";
                        using (SqlCommand command = new SqlCommand(checkUsernameQuery, connection))
                        {
                            command.Parameters.AddWithValue("@TenDangNhap", tenDangNhap);
                            int usernameExists = (int)command.ExecuteScalar();

                            if (usernameExists > 0)
                            {
                                MessageBox.Show("Tên đăng nhập đã tồn tại! Vui lòng chọn tên đăng nhập khác.", "Thông báo");
                                return false;
                            }
                        }
                    }
                }

                int maNV = 0; // Biến lưu mã nhân viên vừa được tạo

                // Kết nối và lưu dữ liệu vào bảng NhanVien
                using (SqlConnection connection = new SqlConnection(dataProvider.constr))
                {
                    connection.Open();

                    // Câu truy vấn chèn dữ liệu vào bảng NhanVien và lấy mã NV vừa được tạo
                    string insertNhanVienQuery = @"
    INSERT INTO NhanVien (TenNV, VaiTro, NgaySinh, GioiTinh, MaCa, HinhAnh)
    VALUES (@TenNV, @VaiTro, @NgaySinh, @GioiTinh, @MaCa, @HinhAnh);
    SELECT SCOPE_IDENTITY();";

                    using (SqlCommand command = new SqlCommand(insertNhanVienQuery, connection))
                    {
                        // Thêm các tham số cho NhanVien
                        command.Parameters.AddWithValue("@TenNV", tenNV);
                        command.Parameters.AddWithValue("@VaiTro", vaiTro);
                        command.Parameters.AddWithValue("@NgaySinh", ngaySinh);
                        command.Parameters.AddWithValue("@GioiTinh", gioiTinh);
                        command.Parameters.AddWithValue("@MaCa", maCa);
                        command.Parameters.AddWithValue("@HinhAnh", hinhAnh);

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

                            if (rowsAffectedNguoiDung <= 0)
                            {
                                MessageBox.Show("Lưu thông tin tài khoản thất bại!", "Thông báo");
                                return false;
                            }
                        }
                    }
                }

                MessageBox.Show("Lưu thông tin nhân viên thành công!", "Thông báo");
                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lưu thông tin thất bại: " + ex.Message, "Thông báo");
                return false;
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

        //    private void btnLuu_Click(object sender, EventArgs e)
        //    {
        //        // Lấy dữ liệu từ các control trên form
        //        string tenNV = txtTen.Text.Trim();
        //        string gioiTinh = cboGioiTinh.SelectedItem.ToString();
        //        string vaiTro = cboVaiTro.SelectedItem.ToString();
        //        DateTime ngaySinh = dateTimePicker1.Value;
        //        int maCa = Convert.ToInt32(txtMaCa.Text.Trim());
        //        string hinhAnh = txtHinhAnh.Text.Trim();

        //        // Tạo câu lệnh SQL INSERT cho bảng NhanVien
        //        string queryNhanVien = @"
        //INSERT INTO NhanVien (TenNV, GioiTinh, VaiTro, NgaySinh, MaCa, HinhAnh)
        //OUTPUT INSERTED.MaNV
        //VALUES (@TenNV, @GioiTinh, @VaiTro, @NgaySinh, @MaCa, @HinhAnh)";

        //        // Khởi tạo các tham số để thay thế vào câu lệnh SQL cho bảng NhanVien
        //        SqlParameter[] parametersNhanVien = new SqlParameter[]
        //        {
        //    new SqlParameter("@TenNV", SqlDbType.NVarChar) { Value = tenNV },
        //    new SqlParameter("@GioiTinh", SqlDbType.NVarChar) { Value = gioiTinh },
        //    new SqlParameter("@VaiTro", SqlDbType.NVarChar) { Value = vaiTro },
        //    new SqlParameter("@NgaySinh", SqlDbType.DateTime) { Value = ngaySinh },
        //    new SqlParameter("@MaCa", SqlDbType.Int) { Value = maCa },
        //    new SqlParameter("@HinhAnh", SqlDbType.NVarChar) { Value = hinhAnh }
        //        };

        //        // Thực thi câu lệnh INSERT và lấy MaNV vừa chèn vào
        //        DataProvider dataProvider = new DataProvider();
        //        object result = dataProvider.ExecScalar(queryNhanVien, parametersNhanVien);
        //        int maNV = Convert.ToInt32(result);

        //        // Kiểm tra nếu lưu nhân viên thành công
        //        if (maNV > 0)
        //        {
        //            // Nếu vai trò là Admin hoặc Thu Ngân, lưu tài khoản và mật khẩu vào bảng NguoiDung
        //            if (vaiTro == "Admin" || vaiTro == "Thu Ngân")
        //            {
        //                string tenDangNhap = txtTaiKhoan.Text.Trim();
        //                string matKhau = txtMatKhau.Text.Trim();

        //                // Tạo câu lệnh SQL INSERT cho bảng NguoiDung
        //                string queryNguoiDung = @"
        //        INSERT INTO NguoiDung (MaNV, TenDangNhap, MatKhau, LoaiNguoiDung)
        //        VALUES (@MaNV, @TenDangNhap, @MatKhau, @LoaiNguoiDung)";

        //                // Khởi tạo các tham số cho bảng NguoiDung
        //                SqlParameter[] parametersNguoiDung = new SqlParameter[]
        //                {
        //            new SqlParameter("@MaNV", SqlDbType.Int) { Value = maNV },
        //            new SqlParameter("@TenDangNhap", SqlDbType.NVarChar) { Value = tenDangNhap },
        //            new SqlParameter("@MatKhau", SqlDbType.NVarChar) { Value = matKhau },
        //            new SqlParameter("@LoaiNguoiDung", SqlDbType.NVarChar) { Value = vaiTro }
        //                };

        //                // Thực thi câu lệnh INSERT vào bảng NguoiDung
        //                int resultNguoiDung = dataProvider.ExecNonQuery(queryNguoiDung, parametersNguoiDung);

        //                // Kiểm tra kết quả thêm tài khoản vào bảng NguoiDung
        //                if (resultNguoiDung > 0)
        //                {
        //                    MessageBox.Show("Thêm nhân viên và tài khoản thành công!", "Thông báo");
        //                    this.Close(); // Đóng form nếu lưu thành công
        //                }
        //                else
        //                {
        //                    MessageBox.Show("Lỗi khi thêm tài khoản vào bảng NguoiDung!", "Thông báo");
        //                }
        //            }
        //            else
        //            {
        //                // Nếu không phải Admin hoặc Thu Ngân, chỉ lưu thông tin nhân viên
        //                MessageBox.Show("Thêm nhân viên thành công!", "Thông báo");
        //                this.Close(); // Đóng form nếu lưu thành công
        //            }
        //        }
        //        else
        //        {
        //            MessageBox.Show("Lỗi khi thêm nhân viên!", "Thông báo");
        //        }
        //    }

        private void btnLuu_Click(object sender, EventArgs e)
        {
            bool isSaved = SaveEmployee(); // Kiểm tra kết quả lưu
            if (isSaved)
            {
                this.Close(); // Chỉ đóng form khi lưu thành công
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



        private void fThemNhanVien_Load(object sender, EventArgs e)
        {
            LoadComboBoxData();
        }

        private void btnXemMK_Click(object sender, EventArgs e)
        {
            txtMatKhau.UseSystemPasswordChar = !txtMatKhau.UseSystemPasswordChar;
        }
    }
}
