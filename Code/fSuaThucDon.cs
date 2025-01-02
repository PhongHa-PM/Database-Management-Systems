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
    public partial class fSuaThucDon : Form
    {
        public int MaMon { get; set; }
        public string TenMon { get; set; }
        public string LoaiMon { get; set; }
        public string NhomThucDon { get; set; }
        public string DonViTinh { get; set; }
        public decimal Gia { get; set; }
        public string HinhAnh { get; set; }
        private string tenHinhAnh;
        DataProvider dataProvider = new DataProvider();
        public fSuaThucDon()
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



        private bool CapNhatDuLieuThucDon()
        {
            // Lấy dữ liệu từ các điều khiển
            string tenMon = txtTenMon.Text.Trim();
            string loaiMon = cboLoaiMon.SelectedItem?.ToString() ?? "";
            string nhomThucDon = cboNhomThucDon.SelectedItem?.ToString() ?? "";
            string donViTinh = cboDonViTinh.SelectedItem?.ToString() ?? "";
            string giaText = txtGia.Text.Trim();

            // Kiểm tra dữ liệu không được để trống
            if (string.IsNullOrEmpty(tenMon))
            {
                MessageBox.Show("Tên món không được để trống.", "Lỗi nhập liệu", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtTenMon.Focus();
                return false;
            }

            if (string.IsNullOrEmpty(loaiMon))
            {
                MessageBox.Show("Vui lòng chọn loại món.", "Lỗi nhập liệu", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                cboLoaiMon.Focus();
                return false;
            }

            if (string.IsNullOrEmpty(nhomThucDon))
            {
                MessageBox.Show("Vui lòng chọn nhóm thực đơn.", "Lỗi nhập liệu", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                cboNhomThucDon.Focus();
                return false;
            }

            if (string.IsNullOrEmpty(donViTinh))
            {
                MessageBox.Show("Vui lòng chọn đơn vị tính.", "Lỗi nhập liệu", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                cboDonViTinh.Focus();
                return false;
            }

            if (string.IsNullOrEmpty(giaText) || !decimal.TryParse(giaText, out decimal gia) || gia <= 0)
            {
                MessageBox.Show("Giá không hợp lệ. Vui lòng nhập giá lớn hơn 0.", "Lỗi nhập liệu", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtGia.Focus();
                return false;
            }

            // Kiểm tra tên món đã tồn tại nhưng không trùng với chính nó
            string queryKiemTra = "SELECT COUNT(*) FROM ThucDon WHERE TenMon = @TenMon AND MaMon != @MaMon";
            SqlParameter[] parametersKiemTra = new SqlParameter[]
            {
        new SqlParameter("@TenMon", tenMon),
        new SqlParameter("@MaMon", MaMon) // Sử dụng MaMon để loại trừ chính dòng hiện tại
            };

            object checkResult = dataProvider.ExecScalar(queryKiemTra, parametersKiemTra);

            if (checkResult != null && Convert.ToInt32(checkResult) > 0)
            {
                // Nếu tên món đã tồn tại
                MessageBox.Show("Tên món đã tồn tại. Vui lòng chọn tên khác.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            // Kiểm tra nếu `tenAnh` rỗng thì sử dụng hình ảnh cũ
            if (string.IsNullOrEmpty(tenAnh))
            {
                tenAnh = HinhAnh; // Sử dụng hình ảnh hiện tại từ thuộc tính HinhAnh
            }

            // Thực hiện cập nhật dữ liệu
            string query = "UPDATE ThucDon SET TenMon = @TenMon, LoaiMon = @LoaiMon, NhomThucDon = @NhomThucDon, DonViTinh = @DonViTinh, Gia = @Gia, HinhAnh = @HinhAnh WHERE MaMon = @MaMon";

            SqlParameter[] parameters = new SqlParameter[]
            {
        new SqlParameter("@TenMon", tenMon),
        new SqlParameter("@LoaiMon", loaiMon),
        new SqlParameter("@NhomThucDon", nhomThucDon),
        new SqlParameter("@DonViTinh", donViTinh),
        new SqlParameter("@Gia", gia),
        new SqlParameter("@HinhAnh", tenAnh), // Dùng hình ảnh sau kiểm tra
        new SqlParameter("@MaMon", MaMon) // Sử dụng MaMon làm khoá chính
            };

            int result = dataProvider.ExecNonQuery(query, parameters);
            if (result > 0)
            {
                MessageBox.Show("Cập nhật món thành công!");
                return true;
            }
            else
            {
                MessageBox.Show("Cập nhật món thất bại!");
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

        private void btnLuu_Click(object sender, EventArgs e)
        {
            if (CapNhatDuLieuThucDon())
            {
                this.Close();
            }
        }

        public void SetImage(string fileName)
        {
            string imagePath = Path.Combine(Application.StartupPath, "HinhAnh", "ThucDon", fileName);

            if (File.Exists(imagePath))
            {
                pictureBox1.Image = Image.FromFile(imagePath);
            }
            else
            {
                MessageBox.Show("Không tìm thấy ảnh!", "Lỗi");
            }
        }


        private void fSuaThucDon_Load(object sender, EventArgs e)
        {
            
                txtTenMon.Text = TenMon;
                cboLoaiMon.SelectedItem = LoaiMon;
                cboNhomThucDon.SelectedItem = NhomThucDon;
                cboDonViTinh.SelectedItem = DonViTinh;
                txtGia.Text = Gia.ToString();

                // Hiển thị ảnh từ thư mục Hình ảnh\Thực Đơn nếu có
                string imagePath = Path.Combine(Application.StartupPath, "HinhAnh", "ThucDon", HinhAnh);
                if (File.Exists(imagePath))
                {
                    pictureBox1.Image = Image.FromFile(imagePath);
                }
            
        }
    }
}
