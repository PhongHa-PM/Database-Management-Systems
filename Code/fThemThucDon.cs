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
    public partial class fThemThucDon : Form
    {
        private string tenHinhAnh;
        DataProvider dataProvider = new DataProvider();
        private Dictionary<string, List<string>> nhomThucDonData = new Dictionary<string, List<string>>()
        {
            { "Đồ Ăn", new List<string> { "Cơm", "Mì", "Bánh" } },
            { "Đồ Uống", new List<string> { "Trà", "Cà Phê", "Sinh Tố", "Nước Ngọt" } },
            { "Thuốc Lá", new List<string> { "Thuốc Lá" } },
            { "Rượu Bia", new List<string> { "Rượu", "Bia" } }
        };

        private Dictionary<string, List<string>> donViTinhData = new Dictionary<string, List<string>>()
        {
            { "Đồ Ăn", new List<string> { "Hộp", "Ly", "Gói" } },
            { "Đồ Uống", new List<string> { "Ly", "Chai", "Cốc", "Lon" } },
            { "Thuốc Lá", new List<string> { "Bao" } },
            { "Rượu Bia", new List<string> { "Chai", "Ly", "Lon" } }
        };
        public fThemThucDon()
        {
            InitializeComponent();
            this.FormBorderStyle = FormBorderStyle.None;
            this.Padding = new Padding(0);
            this.Region = System.Drawing.Region.FromHrgn(CreateRoundRectRgn(0, 0, Width, Height, 0, 0));
            this.StartPosition = FormStartPosition.CenterScreen;

            LoadLoaiMon();
            InitializeComboBox();
        }
        private void LoadLoaiMon()
        {
            cboLoaiMon.Items.AddRange(new object[] { "Đồ Ăn", "Đồ Uống", "Thuốc Lá", "Rượu Bia" });
        }
        private void InitializeComboBox()
        {
            cboLoaiMon.DrawMode = DrawMode.OwnerDrawFixed;
            cboLoaiMon.FlatStyle = FlatStyle.Flat;
            cboLoaiMon.BackColor = Color.Silver;
            cboLoaiMon.ForeColor = Color.Black;
            cboLoaiMon.DrawItem += CbGioiTinh_DrawItem;
        }

        private void CbGioiTinh_DrawItem(object sender, DrawItemEventArgs e)
        {
            if (e.Index < 0) return;
            e.DrawBackground();
            using (Brush textBrush = new SolidBrush(e.ForeColor))
            {
                e.Graphics.DrawString(cboLoaiMon.Items[e.Index].ToString(), e.Font, textBrush, e.Bounds);
            }

            e.DrawFocusRectangle();
        }
        [DllImport("Gdi32.dll", EntryPoint = "CreateRoundRectRgn")]
        private static extern IntPtr CreateRoundRectRgn(int nLeftRect, int nTopRect, int nRightRect, int nBottomRect, int nWidthEllipse, int nHeightEllipse);





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
                    string destinationPath = Path.Combine(Application.StartupPath, "HinhAnh", "ThucDon", tenAnh);

                    // Tạo thư mục nếu chưa có
                    Directory.CreateDirectory(Path.GetDirectoryName(destinationPath));

                    // Copy ảnh vào thư mục
                    File.Copy(openFileDialog.FileName, destinationPath, true);

                    // Hiển thị ảnh trong PictureBox
                    pictureBox1.Image = Image.FromFile(destinationPath);
                }
            }
        }



        private void LuuDuLieuThucDon()
        {
            // Lấy dữ liệu từ các điều khiển
            string tenMon = txtTenMon.Text.Trim();
            string loaiMon = cboLoaiMon.SelectedItem?.ToString() ?? ""; // Xử lý trường hợp chưa chọn
            string nhomThucDon = cboNhomThucDon.SelectedItem?.ToString() ?? "";
            string donViTinh = cboDonViTinh.SelectedItem?.ToString() ?? "";
            string giaText = txtGia.Text.Trim();
            string hinhAnh = tenAnh;

            // Kiểm tra dữ liệu không được để trống
            if (string.IsNullOrEmpty(tenMon))
            {
                MessageBox.Show("Tên món không được để trống.", "Lỗi nhập liệu", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtTenMon.Focus();
                return;
            }

            if (string.IsNullOrEmpty(loaiMon))
            {
                MessageBox.Show("Vui lòng chọn loại món.", "Lỗi nhập liệu", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                cboLoaiMon.Focus();
                return;
            }

            if (string.IsNullOrEmpty(nhomThucDon))
            {
                MessageBox.Show("Vui lòng chọn nhóm thực đơn.", "Lỗi nhập liệu", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                cboNhomThucDon.Focus();
                return;
            }

            if (string.IsNullOrEmpty(donViTinh))
            {
                MessageBox.Show("Vui lòng chọn đơn vị tính.", "Lỗi nhập liệu", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                cboDonViTinh.Focus();
                return;
            }

            if (string.IsNullOrEmpty(giaText) || !decimal.TryParse(giaText, out decimal gia) || gia <= 0)
            {
                MessageBox.Show("Giá không hợp lệ. Vui lòng nhập giá lớn hơn 0.", "Lỗi nhập liệu", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtGia.Focus();
                return;
            }

            if (string.IsNullOrEmpty(hinhAnh))
            {
                MessageBox.Show("Vui lòng chọn hình ảnh.", "Lỗi nhập liệu", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Kiểm tra tên món đã tồn tại hay chưa
            string queryKiemTra = "SELECT COUNT(*) FROM ThucDon WHERE TenMon = @TenMon";
            SqlParameter[] parametersKiemTra = new SqlParameter[]
            {
        new SqlParameter("@TenMon", tenMon)
            };

            object checkResult = dataProvider.ExecScalar(queryKiemTra, parametersKiemTra);

            if (checkResult != null && Convert.ToInt32(checkResult) > 0)
            {
                // Nếu tên món đã tồn tại
                MessageBox.Show("Tên món đã tồn tại. Vui lòng chọn tên khác.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Thực hiện lưu dữ liệu nếu tên món chưa tồn tại
            string queryThucDon = "INSERT INTO ThucDon (TenMon, LoaiMon, NhomThucDon, DonViTinh, Gia, HinhAnh) VALUES (@TenMon, @LoaiMon, @NhomThucDon, @DonViTinh, @Gia, @HinhAnh); SELECT SCOPE_IDENTITY();";

            SqlParameter[] parametersThucDon = new SqlParameter[]
            {
        new SqlParameter("@TenMon", tenMon),
        new SqlParameter("@LoaiMon", loaiMon),
        new SqlParameter("@NhomThucDon", nhomThucDon),
        new SqlParameter("@DonViTinh", donViTinh),
        new SqlParameter("@Gia", gia),
        new SqlParameter("@HinhAnh", hinhAnh)
            };

            object result = dataProvider.ExecScalar(queryThucDon, parametersThucDon);
            if (result != null)
            {
                int maMon = Convert.ToInt32(result);
                MessageBox.Show("Thêm món thành công!");

                string queryKho = "INSERT INTO KhoHang (MaSP, SoLuong, NgayNhapGanNhat) VALUES (@MaSP, @SoLuong, @NgayNhapGanNhat)";
                SqlParameter[] parametersKho = new SqlParameter[]
                {
            new SqlParameter("@MaSP", maMon),
            new SqlParameter("@SoLuong", 1),
            new SqlParameter("@NgayNhapGanNhat", DateTime.Now)
                };

                int resultKho = dataProvider.ExecNonQuery(queryKho, parametersKho);
                if (resultKho > 0)
                {
                    MessageBox.Show("Thêm món vào kho thành công!");
                    this.Close();
                }
                else
                {
                    MessageBox.Show("Thêm món vào kho thất bại!");
                }
            }
            else
            {
                MessageBox.Show("Thêm món thất bại!");
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
            LuuDuLieuThucDon();
        }

        private void cboLoaiMon_SelectedIndexChanged(object sender, EventArgs e)
        {
            string selectedLoaiMon = cboLoaiMon.SelectedItem.ToString();

            // Cập nhật dữ liệu cho cboNhomThucDon dựa trên LoaiMon
            cboNhomThucDon.Items.Clear();
            if (nhomThucDonData.ContainsKey(selectedLoaiMon))
            {
                cboNhomThucDon.Items.AddRange(nhomThucDonData[selectedLoaiMon].ToArray());
            }

            // Cập nhật dữ liệu cho cboDonViTinh dựa trên LoaiMon
            cboDonViTinh.Items.Clear();
            if (donViTinhData.ContainsKey(selectedLoaiMon))
            {
                cboDonViTinh.Items.AddRange(donViTinhData[selectedLoaiMon].ToArray());
            }

            // Reset giá trị của các ComboBox khác
            cboNhomThucDon.SelectedIndex = -1;
            cboDonViTinh.SelectedIndex = -1;
        }
    }
}
