using QL_Bida.Model;

using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Drawing;

using System.Windows.Forms;
using System.Data.SqlClient;
using ClosedXML.Excel;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Rebar;

namespace QL_Bida
{
    public partial class fHome : Form
    {
        DataProvider dataProvider = new DataProvider();
        private string tenDangNhap;
        public bool IsExiting { get; set; } = false;
        public fHome(string tenDangNhap)
        {
            InitializeComponent();
            this.tenDangNhap = tenDangNhap;
            tabControl1.Appearance = TabAppearance.FlatButtons;
            tabControl1.ItemSize = new Size(0, 1); 
            tabControl1.SizeMode = TabSizeMode.Fixed;
            this.FormBorderStyle = FormBorderStyle.None;
            this.Scale(new SizeF(1.5f, 1.5f));
            //this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F); // Dành cho 100%
            this.AutoScaleDimensions = new System.Drawing.SizeF(7.5F, 16.25F); // Dành cho 125%
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
        }

        public fHome()
        {
        }
       
        private List<ThucDon> GetThucDonListFromDatabase()
        {
            string query = "EXEC sp_GetAllThucDon"; 
            DataTable dataTable = dataProvider.ExecQuery(query); 

            List<ThucDon> list = new List<ThucDon>();
            foreach (DataRow row in dataTable.Rows)
            {
                ThucDon item = new ThucDon
                {
                    MaMon = int.Parse(row["MaMon"].ToString()),
                    TenMon = row["TenMon"].ToString(),
                    LoaiMon = row["LoaiMon"].ToString(),
                    NhomThucDon = row["NhomThucDon"].ToString(),
                    DonViTinh = row["DonViTinh"].ToString(),
                    Gia = Convert.ToDecimal(row["Gia"]),
                    HinhAnh = row["HinhAnh"].ToString()
                };
                list.Add(item);
            }

            return list;
        }
        private void DeleteThucDon(int maMon)
        {
            string query = "EXEC sp_DeleteThucDon @MaMon";
            using (SqlConnection connection = new SqlConnection(dataProvider.constr))
            {
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@MaMon", maMon);
                    connection.Open();
                    command.ExecuteNonQuery();
                }
            }
        }

        private List<Ban> GetBanListFromDatabase()
        {
            string query = "EXEC sp_GetAllBanBilliards"; // Truy vấn SQL
            DataTable dataTable = dataProvider.ExecQuery(query); // Sử dụng phương thức của DataProvider

            List<Ban> list = new List<Ban>();
            foreach (DataRow row in dataTable.Rows)
            {
                Ban item = new Ban
                {
                    MaBan = Convert.ToInt32(row["MaBan"]),
                    LoaiBan = row["LoaiBan"].ToString(),
                    TrangThai = row["TrangThai"].ToString() // Giả sử TrangThai là string
                };
                list.Add(item);
            }

            return list;
        }
        public void ThemBan(string loaiBan)
        {
            // Truy vấn SQL để thêm bàn mới
            string query = "EXEC sp_ThemBan @LoaiBan";

            // Sử dụng phương thức ExecNonQuery của DataProvider để thực hiện truy vấn
            SqlParameter parameter = new SqlParameter("@loaiBan", loaiBan); // Tạo tham số

            // Thực hiện truy vấn
            int result = dataProvider.ExecNonQuery(query, new SqlParameter[] { parameter });

            // Kiểm tra kết quả
            if (result > 0)
            {
                MessageBox.Show("Thêm bàn thành công!", "Thông báo");
            }
            else
            {
                MessageBox.Show("Thêm bàn thất bại!", "Thông báo");
            }
        }
        public List<NhanVien> GetNhanVienListFromDatabase()
        {
            string query = "EXEC sp_GetNhanVienList"; // Gọi Stored Procedure
            DataTable dataTable = dataProvider.ExecQuery(query); // Sử dụng phương thức của DataProvider

            List<NhanVien> list = new List<NhanVien>();
            foreach (DataRow row in dataTable.Rows)
            {
                NhanVien item = new NhanVien
                {
                    MaNV = Convert.ToInt32(row["MaNV"]),
                    TenNV = row["TenNV"].ToString(),
                    VaiTro = row["VaiTro"]?.ToString(),
                    GioiTinh = row["GioiTinh"].ToString(),
                    NgaySinh = row["NgaySinh"] != DBNull.Value ? Convert.ToDateTime(row["NgaySinh"]) : (DateTime?)null,
                    MaCa = Convert.ToInt32(row["MaCa"]),
                    HinhAnh = row["HinhAnh"]?.ToString(),
                    TenDangNhap = row["TenDangNhap"]?.ToString(),
                    MatKhau = row["MatKhau"]?.ToString()
                };
                list.Add(item);
            }

            // Sắp xếp VaiTro theo thứ tự ưu tiên: Admin -> Thu Ngân -> Các vai trò khác
            list = list.OrderBy(nv =>
                nv.VaiTro == "Admin" ? 1 :
                nv.VaiTro == "Thu Ngân" ? 2 :
                nv.VaiTro == "Lao Công" ? 3 :
                nv.VaiTro == "Bảo Vệ" ? 4 :
                nv.VaiTro == "Phục Vụ" ? 5 : 6)
                .ThenBy(nv => nv.TenNV) // Thứ tự theo tên nhân viên
                .ToList();

            return list;
        }
        private List<Kho> GetKhoListFromDatabase()
        {
            string query = "EXEC sp_GetKhoList"; // Gọi Stored Procedure
            DataTable dataTable = dataProvider.ExecQuery(query); // Sử dụng phương thức của DataProvider

            List<Kho> list = new List<Kho>();
            foreach (DataRow row in dataTable.Rows)
            {
                Kho item = new Kho
                {
                    MaSP = Convert.ToInt32(row["MaSP"]),
                    MaMon = Convert.ToInt32(row["MaMon"]),
                    TenMon = row["TenMon"].ToString(),
                    SoLuong = Convert.ToInt32(row["SoLuong"]),
                    DonVi = row["DonViTinh"].ToString(),
                    GiaBan = Convert.ToDecimal(row["Gia"]),
                    NgayNhapGanNhat = Convert.ToDateTime(row["NgayNhapGanNhat"])
                };
                list.Add(item);
            }

            return list;
        }
        private List<KhuyenMai> GetKhuyenMaiListFromDatabase()
        {
            string query = "EXEC sp_GetKhuyenMaiList"; // Gọi Stored Procedure
            DataTable dataTable = dataProvider.ExecQuery(query); // Sử dụng phương thức của DataProvider

            List<KhuyenMai> list = new List<KhuyenMai>();
            foreach (DataRow row in dataTable.Rows)
            {
                KhuyenMai item = new KhuyenMai
                {
                    MaKM = Convert.ToInt32(row["MaKM"]),
                    TenKM = row["TenKM"].ToString(),
                    MoTa = row["MoTa"].ToString(),
                    ThoiGianApDungStart = Convert.ToDateTime(row["ThoiGianApDungStart"]),
                    ThoiGianApDungEnd = Convert.ToDateTime(row["ThoiGianApDungEnd"]),
                    GiaTriKM = Convert.ToDecimal(row["GiaTriKM"])
                };
                list.Add(item);
            }

            return list;
        }
        private List<KhachHang> GetKhachHangListFromDatabase()
        {
            string query = "sp_GetKhachHangList"; // Truy vấn SQL
            DataTable dataTable = dataProvider.ExecQuery(query); // Sử dụng biến dataProvider

            List<KhachHang> list = new List<KhachHang>();
            foreach (DataRow row in dataTable.Rows)
            {
                KhachHang item = new KhachHang
                {
                    MaKH = Convert.ToInt32(row["MaKH"]),
                    HoTen = row["HoTen"].ToString(),
                    SDT = row["SDT"] != DBNull.Value ? row["SDT"].ToString() : null,
                    NgaySinh = row["NgaySinh"] != DBNull.Value ? Convert.ToDateTime(row["NgaySinh"]) : (DateTime?)null,
                    Email = row["Email"] != DBNull.Value ? row["Email"].ToString() : null,
                    DiemTichLuy = Convert.ToInt32(row["DiemTichLuy"])
                };
                list.Add(item);
            }

            return list;
        }
        private List<HoaDon> GetHoaDonListFromDatabase()
        {
            string query = "sp_GetHoaDonList"; // Truy vấn SQL
            DataTable dataTable = dataProvider.ExecQuery(query); // Sử dụng biến dataProvider

            List<HoaDon> list = new List<HoaDon>();
            foreach (DataRow row in dataTable.Rows)
            {
                HoaDon item = new HoaDon
                {
                    SoHoaDon = Convert.ToInt32(row["SoHoaDon"]),

                    // Kiểm tra null cho MaKH
                    MaKH = row["MaKH"] != DBNull.Value ? Convert.ToInt32(row["MaKH"]) : (int?)null,

                    MaBan = Convert.ToInt32(row["MaBan"]),
                    MaNV = Convert.ToInt32(row["MaNV"]),
                    SoGioChoi = Convert.ToDecimal(row["SoGioChoi"]),

                    // Kiểm tra null cho ThanhTien
                    ThanhTien = row["ThanhTien"] != DBNull.Value ? Convert.ToDecimal(row["ThanhTien"]) : (decimal?)null,

                    NgayLapHoaDon = Convert.ToDateTime(row["NgayLapHoaDon"]),

                    HinhThucThanhToan = row["HinhThucThanhToan"].ToString(),

                    // Kiểm tra null cho MaKM
                    MaKM = row["MaKM"] != DBNull.Value ? Convert.ToInt32(row["MaKM"]) : (int?)null
                };
                list.Add(item);
            }

            return list;
        }


        // Sự kiện khi tab được chọn
        private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Ẩn tất cả các Panel
            pnlThucDon.Visible = false;
            pnlQuanLyBan.Visible = false;
            pnlNhanVien.Visible = false;
            pnlKhuyenMai.Visible = false;
            pnlKho.Visible = false;
            pnlKhachHang.Visible = false;
            pnlLichSu.Visible = false;

            // Kiểm tra tab được chọn và hiển thị Panel tương ứng
            switch (tabControl1.SelectedIndex)
            {
                case 0: // Tab Thực Đơn
                    pnlThucDon.Visible = true;
                    LoadThucDonData(); // Tải dữ liệu Thực Đơn
                    LoadNhomThucDonData();
                    lblUserName_TD.Text = tenDangNhap;
                    cbo_ThucDon.SelectedIndex = 0;
                   
                    break;
                case 1: // Tab Quản Lý Bàn
                    pnlQuanLyBan.Visible = true;
                    LoadBanData("Tất cả"); // Tải dữ liệu Bàn
                    if (cboQuanLyBan.Items.Count == 0)
                    {
                        cboQuanLyBan.Items.Add("Tất cả");
                        cboQuanLyBan.Items.Add("Lỗ");
                        cboQuanLyBan.Items.Add("Phăng");
                    }
                    lblUserName_Ban.Text = tenDangNhap;
                    cboQuanLyBan.SelectedIndex = 0;
                    LoadBanData();
                    break;
                case 2: // Tab Nhân Viên
                    pnlNhanVien.Visible = true;
                    LoadNhanVienData();
                    lblUserName_NhanVien.Text = tenDangNhap;
                    break;
                case 3:
                    pnlKhuyenMai.Visible = true;
                    LoadKhuyenMaiData();
                    lblUserName_KM.Text = tenDangNhap;
                    break;
                case 4:
                    pnlKho.Visible = true;
                    LoadKhoData();
                    lblUserName_Kho.Text = tenDangNhap;
                    break;
                case 5:
                    pnlKhachHang.Visible = true;
                    LoadKhachHangData();
                    lblUserName_KH.Text = tenDangNhap;
                    break;
                case 6:
                    pnlLichSu.Visible = true;
                    LoadHoaDonData();
                    lblUserName_LS.Text = tenDangNhap;
                    break;
            }
        }


        private void LoadThucDonData()
        {
            // Gán danh sách thực đơn vào DataGridView
            dgvThucDon.DataSource = GetThucDonListFromDatabase();

            // Tùy chỉnh Header của DataGridView
            dgvThucDon.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(174, 113, 102);
            dgvThucDon.ColumnHeadersDefaultCellStyle.ForeColor = Color.Black;
            dgvThucDon.ColumnHeadersDefaultCellStyle.Font = new Font(dgvThucDon.Font.FontFamily, dgvThucDon.Font.Size + 2, FontStyle.Bold);
            dgvThucDon.DefaultCellStyle.Font = new Font(dgvThucDon.Font.FontFamily, dgvThucDon.Font.Size + 2);
            //dgvThucDon.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            // Gán tiêu đề cột có dấu
            dgvThucDon.Columns["MaMon"].HeaderText = "Mã Món";
            dgvThucDon.Columns["TenMon"].HeaderText = "Tên Món";
            dgvThucDon.Columns["LoaiMon"].HeaderText = "Loại Món";
            dgvThucDon.Columns["NhomThucDon"].HeaderText = "Nhóm Thực Đơn";
            dgvThucDon.Columns["DonViTinh"].HeaderText = "Đơn Vị Tính";
            dgvThucDon.Columns["Gia"].HeaderText = "Giá";
            dgvThucDon.Columns["HinhAnh"].HeaderText = "Hình Ảnh";
        }

        private void LoadNhomThucDonData()
        {
            // Kiểm tra và chỉ thêm mục "Tất cả", "Giá tăng dần" và "Giá giảm dần" nếu chưa có
            if (!cbo_ThucDon.Items.Contains("Tất cả"))
            {
                cbo_ThucDon.Items.Add("Tất cả");
            }
            if (!cbo_ThucDon.Items.Contains("Giá tăng dần"))
            {
                cbo_ThucDon.Items.Add("Giá tăng dần");
            }
            if (!cbo_ThucDon.Items.Contains("Giá giảm dần"))
            {
                cbo_ThucDon.Items.Add("Giá giảm dần");
            }

            // Gọi Stored Procedure để lấy các nhóm thực đơn
            string query = "EXEC sp_GetNhomThucDon";  // Gọi procedure đã tạo
            DataTable dataTable = dataProvider.ExecQuery(query);  // Sử dụng phương thức của DataProvider

            // Thêm các nhóm thực đơn vào ComboBox nếu chưa có
            foreach (DataRow row in dataTable.Rows)
            {
                string nhomThucDon = row["NhomThucDon"].ToString();
                if (!cbo_ThucDon.Items.Contains(nhomThucDon))
                {
                    cbo_ThucDon.Items.Add(nhomThucDon);
                }
            }

            // Đặt SelectedIndex về -1 để không chọn mục nào
            cbo_ThucDon.SelectedIndex = -1;
        }



        private void LoadBanData()
        {
            // Xóa các nút hiện có trong FlowLayoutPanel
            flpQuanLyBan.Controls.Clear();

            // Lấy danh sách bàn từ database
            List<Ban> banList = GetBanListFromDatabase();

            // Lọc danh sách theo loại được chọn trong ComboBox
            string selectedLoaiBan = cboQuanLyBan.SelectedItem.ToString();
            if (selectedLoaiBan != "Tất cả")
            {
                // Lọc danh sách bàn chỉ hiển thị loại "Lỗ" hoặc "Phăng" tùy theo lựa chọn
                banList = banList.Where(ban => ban.LoaiBan == selectedLoaiBan).ToList();
            }

            // Duyệt qua từng bàn để tạo nút
            foreach (Ban ban in banList)
            {
                Button btnBan = new Button
                {
                    Width = 208,      // Kích thước hình vuông
                    Height = 208,
                    Text = $"Bàn {ban.MaBan}",
                    Tag = ban          // Lưu đối tượng Bàn vào Tag để dùng sau nếu cần
                };

                // Đặt màu nền theo loại bàn
                if (ban.LoaiBan == "Phăng")
                {
                    btnBan.BackColor = Color.FromArgb(0, 206, 209); // Màu xanh
                }
                else if (ban.LoaiBan == "Lỗ")
                {
                    btnBan.BackColor = Color.FromArgb(254, 142, 142); // Màu đỏ
                }
                btnBan.Click += BtnBan_Click;
                // Thêm nút vào FlowLayoutPanel
                flpQuanLyBan.Controls.Add(btnBan);
            }
        }

        private Ban selectedBan; // Biến lưu thông tin bàn được chọn

        private void LoadBanData(string trangThai = "Tất cả")
        {
            // Xóa các nút hiện có trong FlowLayoutPanel
            flpQuanLyBan.Controls.Clear();

            // Lấy danh sách bàn từ database
            List<Ban> banList = GetBanListFromDatabase();

            // Lọc danh sách bàn dựa trên trạng thái
            if (trangThai != "Tất cả")
            {
                // Lọc theo trạng thái
                banList = banList.Where(b => b.TrangThai.Equals(trangThai, StringComparison.OrdinalIgnoreCase)).ToList();
            }

            // Duyệt qua từng bàn để tạo nút
            foreach (Ban ban in banList)
            {
                Button btnBan = new Button
                {
                    Width = 208,      // Kích thước hình vuông
                    Height = 208,
                    Text = $"Bàn {ban.MaBan}",
                    Tag = ban          // Lưu đối tượng Bàn vào Tag để dùng sau nếu cần
                };

                // Đặt màu nền theo loại bàn
                if (ban.LoaiBan.Equals("Phăng", StringComparison.OrdinalIgnoreCase))
                {
                    btnBan.BackColor = Color.FromArgb(0, 206, 209); // Màu xanh
                }
                else if (ban.LoaiBan.Equals("Lỗ", StringComparison.OrdinalIgnoreCase))
                {
                    btnBan.BackColor = Color.FromArgb(254, 142, 142); // Màu đỏ
                }

                // Đảm bảo sự kiện Click được đăng ký cho mỗi nút bàn


                // Thêm nút vào FlowLayoutPanel
                flpQuanLyBan.Controls.Add(btnBan);
            }
        }


        private void BtnBan_Click(object sender, EventArgs e)
        {


            Button btnBan = sender as Button;
            if (btnBan != null)
            {
                selectedBan = (Ban)btnBan.Tag;

            }
        }








        private void LoadNhanVienData()
        {
            // Gán danh sách nhân viên vào DataGridView
            var list = GetNhanVienListFromDatabase();
            dgvNhanVien.DataSource = list;

            // Đặt vị trí cột
            dgvNhanVien.Columns["MaNV"].DisplayIndex = 0;
            dgvNhanVien.Columns["TenNV"].DisplayIndex = 1;
            dgvNhanVien.Columns["VaiTro"].DisplayIndex = 2;
            dgvNhanVien.Columns["TenDangNhap"].DisplayIndex = 3;
            dgvNhanVien.Columns["MatKhau"].DisplayIndex = 4;
            dgvNhanVien.Columns["NgaySinh"].DisplayIndex = 5;
            dgvNhanVien.Columns["GioiTinh"].DisplayIndex = 6;
            dgvNhanVien.Columns["MaCa"].DisplayIndex = 7;
            dgvNhanVien.Columns["HinhAnh"].DisplayIndex = 8;

            // Đặt tiêu đề tiếng Việt cho các cột
            dgvNhanVien.Columns["MaNV"].HeaderText = "Mã NV";
            dgvNhanVien.Columns["TenNV"].HeaderText = "Tên NV";
            dgvNhanVien.Columns["VaiTro"].HeaderText = "Vai Trò";
            dgvNhanVien.Columns["TenDangNhap"].HeaderText = "Tên Đăng Nhập";
            dgvNhanVien.Columns["MatKhau"].HeaderText = "Mật Khẩu";
            dgvNhanVien.Columns["NgaySinh"].HeaderText = "Ngày Sinh";
            dgvNhanVien.Columns["GioiTinh"].HeaderText = "Giới Tính";
            dgvNhanVien.Columns["MaCa"].HeaderText = "Mã Ca";
            dgvNhanVien.Columns["HinhAnh"].HeaderText = "Hình Ảnh";

            // Tuỳ chỉnh màu và kích thước
            dgvNhanVien.ColumnHeadersDefaultCellStyle.ForeColor = Color.Black;
            dgvNhanVien.ColumnHeadersDefaultCellStyle.Font = new Font(dgvNhanVien.Font.FontFamily, dgvNhanVien.Font.Size + 2, FontStyle.Bold);
            dgvNhanVien.DefaultCellStyle.Font = new Font(dgvNhanVien.Font.FontFamily, dgvNhanVien.Font.Size + 2);
           
        }

        private void LoadKhuyenMaiData()
        {
            // Gán danh sách khuyến mãi vào DataGridView
            dgvKhuyenMai.DataSource = GetKhuyenMaiListFromDatabase();
            dgvKhuyenMai.ColumnHeadersDefaultCellStyle.ForeColor = Color.Black;
            dgvKhuyenMai.ColumnHeadersDefaultCellStyle.Font = new Font(dgvKhuyenMai.Font.FontFamily, dgvKhuyenMai.Font.Size + 2, FontStyle.Bold);
            dgvKhuyenMai.DefaultCellStyle.Font = new Font(dgvKhuyenMai.Font.FontFamily, dgvKhuyenMai.Font.Size + 2);
            dgvKhuyenMai.Columns["MaKM"].HeaderText = " Mã Khuyến Mãi";
            dgvKhuyenMai.Columns["TenKM"].HeaderText = " Tên Khuyến Mãi";
            dgvKhuyenMai.Columns["MoTa"].HeaderText = " Mô tả";
            dgvKhuyenMai.Columns["ThoiGianApDungStart"].HeaderText = "Ngày bắt đầu";
            dgvKhuyenMai.Columns["ThoiGianApDungEnd"].HeaderText = "Ngày kết thúc";
            dgvKhuyenMai.Columns["GiaTriKM"].HeaderText = "Giá trị";
            dgvKhuyenMai.Columns["TenKMWithvalue"].HeaderText = "Hiển thị";

            

        }
        private void LoadKhoData()
        {
            // Gán danh sách kho vào DataGridView
            dgvKho.DataSource = GetKhoListFromDatabase();
            dgvKho.ColumnHeadersDefaultCellStyle.ForeColor = Color.Black;
            dgvKho.ColumnHeadersDefaultCellStyle.Font = new Font(dgvKho.Font.FontFamily, dgvKho.Font.Size + 2, FontStyle.Bold);
            dgvKho.DefaultCellStyle.Font = new Font(dgvKho.Font.FontFamily, dgvKho.Font.Size + 2);
            dgvKho.Columns["MaSP"].HeaderText = "Mã Sản Phẩm";
            dgvKho.Columns["MaMon"].HeaderText = "Mã Món";
            dgvKho.Columns["TenMon"].HeaderText = "Tên Món";
            dgvKho.Columns["SoLuong"].HeaderText = "Số Lượng";
            dgvKho.Columns["DonVi"].HeaderText = "Đơn Vị";
            dgvKho.Columns["GiaBan"].HeaderText = "Giá Bán";
            dgvKho.Columns["NgayNhapGanNhat"].HeaderText = "Ngày Nhập Gần Nhất";
          

        }
        private void LoadKhachHangData()
        {
            // Gán danh sách khách hàng vào DataGridView
            dgvKhachHang.DataSource = GetKhachHangListFromDatabase();
            dgvKhachHang.ColumnHeadersDefaultCellStyle.ForeColor = Color.Black;
            dgvKhachHang.ColumnHeadersDefaultCellStyle.Font = new Font(dgvKhachHang.Font.FontFamily, dgvKhachHang.Font.Size + 2, FontStyle.Bold);
            dgvKhachHang.DefaultCellStyle.Font = new Font(dgvKhachHang.Font.FontFamily, dgvKhachHang.Font.Size + 2);
            dgvKhachHang.Columns["MaKH"].HeaderText = "Mã Khách Hàng";
            dgvKhachHang.Columns["HoTen"].HeaderText = "Họ Tên";
            dgvKhachHang.Columns["SDT"].HeaderText = "Số Điện Thoại";
            dgvKhachHang.Columns["NgaySinh"].HeaderText = "Ngày Sinh";
            dgvKhachHang.Columns["Email"].HeaderText = "Email";
            dgvKhachHang.Columns["DiemTichLuy"].HeaderText = "Điểm Tích Luỹ";
            dgvKhachHang.Columns["TenHienThi"].HeaderText = "Tên Hiển Thị";


        }
        private void LoadHoaDonData()
        {
            // Gán danh sách hóa đơn vào DataGridView
            dgvLichSu.DataSource = GetHoaDonListFromDatabase();
            dgvLichSu.ColumnHeadersDefaultCellStyle.ForeColor = Color.Black;
            dgvLichSu.ColumnHeadersDefaultCellStyle.Font = new Font(dgvLichSu.Font.FontFamily, dgvLichSu.Font.Size + 2, FontStyle.Bold);
            dgvLichSu.DefaultCellStyle.Font = new Font(dgvLichSu.Font.FontFamily, dgvLichSu.Font.Size + 2);
            dgvLichSu.Columns["SoHoaDon"].HeaderText = "Số Hoá Đơn";
            dgvLichSu.Columns["MaKH"].HeaderText = "Mã Khách Hàng";
            dgvLichSu.Columns["MaBan"].HeaderText = "Mã Bàn";
            dgvLichSu.Columns["MaNV"].HeaderText = "Mã Nhân Viên";
            dgvLichSu.Columns["SoGioChoi"].HeaderText = "Số Giờ Chơi";
            dgvLichSu.Columns["ThanhTien"].HeaderText = "Thành Tiền";
            dgvLichSu.Columns["NgayLapHoaDon"].HeaderText = "Ngày Lập Hoá Đơn";
            dgvLichSu.Columns["HinhThucThanhToan"].HeaderText = "Hình Thức Thanh Toán";
            dgvLichSu.Columns["MaKM"].HeaderText = "Mã Khuyến Mãi";



        }

        private void fHome_Load(object sender, EventArgs e)
        {
            
            tabControl1.SelectedIndex = 1;
            LoadBanData();
            

        }

        private void fHome_FormClosing(object sender, FormClosingEventArgs e)
        {


            e.Cancel = true; // Ngăn chặn đóng ngay lập tức

            fXacNhanThoat formXacNhanThoat = new fXacNhanThoat();
            if (formXacNhanThoat.ShowDialog() == DialogResult.OK) // Nếu xác nhận thoát
            {
                e.Cancel = false; // Cho phép đóng
                fDangNhap formDangNhap = new fDangNhap();
                formDangNhap.Show(); // Hiển thị lại form đăng nhập

                // Đặt lại cờ IsExiting về false khi quay lại fDangNhap
                fDangNhap.IsExiting = false;
            }

        }

        private void btnThoat_ThucDon_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void btnThoat_Ban_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void btnThoat_NhanVien_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void btnThoat_KM_Click(object sender, EventArgs e)
        {
          
            Close();
        }

        #region CLICK
        private void btnThoat_Kho_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void btnThoat_KH_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void btnThoat_LS_Click(object sender, EventArgs e)
        {
            Close();
        }

        // Định nghĩa màu sắc cho nút khi chọn và không chọn
        Color defaultColor = Color.FromArgb(102, 44, 33); // Màu mặc định (có thể thay đổi)
        Color selectedColor = Color.FromArgb(122, 52, 39);  // Màu khi được chọn

        // Hàm để đặt lại màu nền của tất cả các nút
        private void ResetButtonColors()
        {
            btnThucDon.BackColor = defaultColor;
            btnQuanLyBan.BackColor = defaultColor;
            btnNhanVien.BackColor = defaultColor;
            btnKhuyenMai.BackColor = defaultColor;
            btnKho.BackColor = defaultColor;
            btnKhachHang.BackColor = defaultColor;
            btnLichSu.BackColor = defaultColor;

        }

        // Hàm xử lý khi click vào từng nút
        private void btnThucDon_Click(object sender, EventArgs e)
        {
            ResetButtonColors(); // Đặt lại màu cho tất cả các nút
            btnThucDon.BackColor = selectedColor; // Đổi màu cho nút hiện tại
            tabControl1.SelectedIndex = 0;
        }

        private void btnQuanLyBan_Click(object sender, EventArgs e)
        {
            ResetButtonColors();
            btnQuanLyBan.BackColor = selectedColor;
            tabControl1.SelectedIndex = 1;
        }

        private void btnNhanVien_Click(object sender, EventArgs e)
        {
            ResetButtonColors();
            btnNhanVien.BackColor = selectedColor;
            tabControl1.SelectedIndex = 2;
        }

        private void btnKhuyenMai_Click(object sender, EventArgs e)
        {
            ResetButtonColors();
            btnKhuyenMai.BackColor = selectedColor;
            tabControl1.SelectedIndex = 3;
        }

        private void btnKho_Click(object sender, EventArgs e)
        {
            ResetButtonColors();
            btnKho.BackColor = selectedColor;
            tabControl1.SelectedIndex = 4;
        }

        private void btnKhachHang_Click(object sender, EventArgs e)
        {
            ResetButtonColors();
            btnKhachHang.BackColor = selectedColor;
            tabControl1.SelectedIndex = 5;
        }


        private void btnLichSu_Click(object sender, EventArgs e)
        {
            ResetButtonColors(); 
            btnLichSu.BackColor = selectedColor;
            tabControl1.SelectedIndex = 6;
        }
        #endregion




        #region THUCDON
        private void UpdateThucDon(int maMon, string tenMon, string loaiMon, string nhomThucDon, string donViTinh, decimal gia, string hinhAnh)
        {
            // Câu lệnh gọi Stored Procedure
            string query = "EXEC sp_UpdateThucDon @MaMon, @TenMon, @LoaiMon, @NhomThucDon, @DonViTinh, @Gia, @HinhAnh";

            // Thêm tham số vào câu lệnh SQL
            SqlParameter[] parameters = new SqlParameter[]
            {
        new SqlParameter("@MaMon", maMon),
        new SqlParameter("@TenMon", tenMon),
        new SqlParameter("@LoaiMon", loaiMon),
        new SqlParameter("@NhomThucDon", nhomThucDon),
        new SqlParameter("@DonViTinh", donViTinh),
        new SqlParameter("@Gia", gia),
        new SqlParameter("@HinhAnh", hinhAnh)
            };

            // Thực thi câu lệnh cập nhật trong bảng ThucDon
            int result = dataProvider.ExecNonQuery(query, parameters);
            if (result > 0)
            {
                MessageBox.Show("Sửa món thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("Sửa món thất bại!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void UpdateKhoHang(int maMon, decimal gia)
        {
            // Câu lệnh gọi Stored Procedure
            string query = "EXEC sp_UpdateKhoHang @MaMon, @SoLuong, @NgayNhapGanNhat";

            // Thêm tham số vào câu lệnh SQL
            SqlParameter[] parameters = new SqlParameter[]
            {
        new SqlParameter("@MaMon", maMon),
        new SqlParameter("@SoLuong", 0), // Số lượng đặt mặc định là 0, có thể thay đổi theo yêu cầu
        new SqlParameter("@NgayNhapGanNhat", DateTime.Now) // Ngày nhập là ngày hiện tại
            };

            // Thực thi câu lệnh cập nhật trong bảng KhoHang thông qua stored procedure
            int result = dataProvider.ExecNonQuery(query, parameters);
            if (result > 0)
            {
                MessageBox.Show("Dữ liệu trong kho đã được cập nhật!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("Cập nhật dữ liệu trong kho thất bại!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        private void btnSua_ThucDon_Click(object sender, EventArgs e)
        {
            if (dgvThucDon.CurrentCell != null)
            {
                DataGridViewRow row = dgvThucDon.Rows[dgvThucDon.CurrentCell.RowIndex];

                int maMon = (int)row.Cells["MaMon"].Value;
                string tenMon = row.Cells["TenMon"].Value.ToString();
                string loaiMon = row.Cells["LoaiMon"].Value.ToString();
                string nhomThucDon = row.Cells["NhomThucDon"].Value.ToString();
                string donViTinh = row.Cells["DonViTinh"].Value.ToString();
                decimal gia = (decimal)row.Cells["Gia"].Value;
                string hinhAnh = row.Cells["HinhAnh"].Value.ToString();

                fSuaThucDon formSua = new fSuaThucDon();
                formSua.MaMon = maMon;
                formSua.TenMon = tenMon;
                formSua.LoaiMon = loaiMon;
                formSua.NhomThucDon = nhomThucDon;
                formSua.DonViTinh = donViTinh;
                formSua.Gia = gia;
                formSua.HinhAnh = hinhAnh;

                // Gọi SetImage để hiển thị ảnh
                formSua.SetImage(hinhAnh);

                formSua.ShowDialog();

                if (formSua.DialogResult == DialogResult.OK)
                {
                    // Sau khi sửa món ăn, cập nhật dữ liệu vào cơ sở dữ liệu
                    UpdateThucDon(maMon, formSua.TenMon, formSua.LoaiMon, formSua.NhomThucDon, formSua.DonViTinh, formSua.Gia, formSua.HinhAnh);

                    // Cập nhật thông tin trong bảng KhoHang nếu cần thiết
                    UpdateKhoHang(maMon, formSua.Gia);

                    // Tải lại dữ liệu cho DataGridView

                }
                LoadThucDonData();
            }
            else
            {
                MessageBox.Show("Vui lòng chọn món để sửa!", "Thông báo");
            }
        }

        private void txtTimKiem_ThucDon_TextChanged(object sender, EventArgs e)
        {
            string searchTerm = txtTimKiem_ThucDon.Text.ToLower();
            List<ThucDon> allThucDon = GetThucDonListFromDatabase();

            var filteredList = allThucDon.Where(item => item.TenMon.ToLower().Contains(searchTerm)).ToList();
            dgvThucDon.DataSource = filteredList;
        }

        private void txtTimKiem_ThucDon_Enter(object sender, EventArgs e)
        {
            if (txtTimKiem_ThucDon.Text == "Tìm kiếm ...")
            {
                txtTimKiem_ThucDon.Text = ""; // Làm trống TextBox khi nhấp vào
                txtTimKiem_ThucDon.ForeColor = Color.Black; // Đặt màu chữ về màu đen
            }
        }

        private void txtTimKiem_ThucDon_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtTimKiem_ThucDon.Text))
            {
                txtTimKiem_ThucDon.Text = "Tìm kiếm ..."; // Đưa lại giá trị mặc định khi rời khỏi
                txtTimKiem_ThucDon.ForeColor = Color.Gray; // Đặt màu chữ về màu xám
            }
        }
        private void cbo_ThucDon_SelectedIndexChanged(object sender, EventArgs e)
        {

            string selectedGroup = cbo_ThucDon.SelectedItem?.ToString(); // Lấy nhóm đã chọn
            List<ThucDon> allThucDon = GetThucDonListFromDatabase(); // Lấy tất cả món ăn

            if (!string.IsNullOrEmpty(selectedGroup))
            {
                if (selectedGroup == "Tất cả")
                {
                    dgvThucDon.DataSource = allThucDon; // Hiển thị tất cả món ăn
                }
                else if (selectedGroup == "Giá tăng dần")
                {
                    var sortedList = allThucDon.OrderBy(item => item.Gia).ToList(); // Sắp xếp theo giá tăng dần
                    dgvThucDon.DataSource = sortedList; // Gán danh sách đã sắp xếp vào DataGridView
                }
                else if (selectedGroup == "Giá giảm dần")
                {
                    var sortedList = allThucDon.OrderByDescending(item => item.Gia).ToList(); // Sắp xếp theo giá giảm dần
                    dgvThucDon.DataSource = sortedList; // Gán danh sách đã sắp xếp vào DataGridView
                }
                else
                {
                    // Lọc theo nhóm
                    var filteredList = allThucDon.Where(item => item.NhomThucDon.Equals(selectedGroup)).ToList();
                    dgvThucDon.DataSource = filteredList; // Gán danh sách đã lọc vào DataGridView
                }
            }
            else
            {
                LoadThucDonData(); // Hiển thị tất cả món ăn nếu không có nhóm nào được chọn
            }

        }
        private void btnThem_ThucDon_Click(object sender, EventArgs e)
        {
            fThemThucDon fThemtd = new fThemThucDon();
            fThemtd.ShowDialog();
            LoadThucDonData();
        }
        private void btnXoa_ThucDon_Click(object sender, EventArgs e)
        {
            // Kiểm tra xem có hàng nào được chọn trong DataGridView không
            if (dgvThucDon.CurrentRow != null)
            {
                // Hiển thị form xác nhận xóa
                fXacNhanXoa confirmationForm = new fXacNhanXoa();
                confirmationForm.ShowDialog();

                // Nếu người dùng xác nhận xóa, thực hiện xóa
                if (confirmationForm.DialogResult == DialogResult.OK)
                {
                    // Lấy mã món ăn từ dòng hiện tại
                    int maMon = (int)dgvThucDon.CurrentRow.Cells["MaMon"].Value; // Lấy giá trị MaMon từ hàng hiện tại

                    // Xóa dữ liệu liên quan trong bảng KhoHang
                    DeleteKhoHangByMaMon(maMon);

                    // Gọi phương thức để xóa món ăn từ cơ sở dữ liệu
                    DeleteThucDon(maMon);

                    // Tải lại dữ liệu cho DataGridView
                    LoadThucDonData();
                }
            }
            else
            {
                MessageBox.Show("Vui lòng chọn món ăn để xóa.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }


        private void DeleteKhoHangByMaMon(int maMon)
        {
            // Câu lệnh gọi Stored Procedure
            string query = "EXEC sp_DeleteKhoHangByMaMon @MaMon";

            // Thêm tham số vào câu lệnh SQL
            SqlParameter[] parametersKhoHang = new SqlParameter[]
            {
        new SqlParameter("@MaMon", maMon)
            };

            // Thực thi câu lệnh xóa trong bảng KhoHang thông qua stored procedure
            int resultKhoHang = dataProvider.ExecNonQuery(query, parametersKhoHang);

            if (resultKhoHang > 0)
            {
                MessageBox.Show("Dữ liệu liên quan trong kho đã được xóa!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
           
        }


        int GetMaNVByTenDangNhap(string tenDangNhap)
        {
            // Câu lệnh gọi Stored Procedure
            string query = "EXEC sp_GetMaNVByTenDangNhap @TenDangNhap";

            // Thêm tham số vào câu lệnh SQL
            SqlParameter[] parameters = new SqlParameter[]
            {
        new SqlParameter("@TenDangNhap", tenDangNhap)
            };

            // Thực thi câu lệnh và lấy kết quả
            object result = dataProvider.ExecScalar(query, parameters);

            if (result != null && int.TryParse(result.ToString(), out int maNV))
            {
                return maNV;
            }
            return 0; // Trả về 0 nếu không tìm thấy
        }


        public DataTable GetNhanVienByTenDangNhap(string tenDangNhap)
        {
            // Câu lệnh gọi Stored Procedure
            string query = "EXEC sp_GetNhanVienByTenDangNhap @TenDangNhap";

            // Thêm tham số vào câu lệnh SQL
            SqlParameter[] parameters = new SqlParameter[]
            {
        new SqlParameter("@TenDangNhap", tenDangNhap)
            };

            // Thực thi câu lệnh và trả về DataTable
            return dataProvider.ExecQuery(query, parameters);
        }



        private void toolStripMenuItem1_Click(object sender, EventArgs e)
        {
            // Giả sử bạn đã có biến `tenDangNhap` là Tên Đăng Nhập hiện tại
            string tenDangNhap1 = tenDangNhap; // Thay bằng giá trị Tên Đăng Nhập thực tế

            DataProvider dataProvider = new DataProvider();

            // Lấy MaNV từ TenDangNhap
            int maNV = dataProvider.GetMaNVByTenDangNhap(tenDangNhap1);
            if (maNV == 0)
            {
                MessageBox.Show("Không tìm thấy nhân viên!", "Thông báo");
                return;
            }

            // Lấy thông tin nhân viên dựa trên MaNV
            DataTable nhanVienData = dataProvider.GetNhanVienInfoByMaNV(maNV);
            if (nhanVienData.Rows.Count > 0)
            {
                DataRow row = nhanVienData.Rows[0];

                // Khởi tạo form sửa nhân viên
                fSuaNhanVien formSua = new fSuaNhanVien();
                formSua.MaNV = (int)row["MaNV"];
                formSua.TenNV = row["TenNV"].ToString();
                formSua.VaiTro = row["VaiTro"].ToString();
                formSua.NgaySinh = (DateTime)row["NgaySinh"];
                formSua.GioiTinh = row["GioiTinh"].ToString();
                formSua.MaCa = (int)row["MaCa"];
                formSua.HinhAnh = row["HinhAnh"]?.ToString();
                formSua.label1.Text = "Thông tin nhân viên";
                formSua.lblXacNhanMK.Visible = false;
                formSua.txtXacNhanMK.Visible = false;
                formSua.btnLuu.Visible = false;
                formSua.panel4.Visible = false;
                formSua.txtTen.ReadOnly = true;
                formSua.cboGioiTinh.Enabled = false;
                formSua.cboCa.Enabled = false;
                formSua.cboVaiTro.Enabled = false;
                formSua.dateTimePicker1.Enabled = false;
                formSua.txtTenDangNhap.ReadOnly = true;
                formSua.txtMatKhau.ReadOnly = true;
                formSua.label10.Visible = false;

                // Lấy thông tin tài khoản từ bảng NguoiDung
                string queryNguoiDung = "SELECT TenDangNhap, MatKhau FROM NguoiDung WHERE MaNV = @MaNV";
                SqlParameter[] parameters = new SqlParameter[]
                {
            new SqlParameter("@MaNV", maNV)
                };
                DataTable nguoiDungData = dataProvider.ExecQuery(queryNguoiDung, parameters);

                if (nguoiDungData.Rows.Count > 0)
                {
                    formSua.TenDangNhap = nguoiDungData.Rows[0]["TenDangNhap"].ToString();
                    formSua.MatKhau = nguoiDungData.Rows[0]["MatKhau"].ToString();
                }

                formSua.SetImage(formSua.HinhAnh); // Hiển thị ảnh nếu có
                formSua.ShowDialog();

            }
            else
            {
                MessageBox.Show("Không tìm thấy thông tin nhân viên!", "Thông báo");
            }
        }



        private void toolStripMenuItem2_Click(object sender, EventArgs e)
        {
            // Giả sử bạn đã có biến `tenDangNhap` là Tên Đăng Nhập hiện tại
            string tenDangNhap1 = tenDangNhap; // Thay bằng giá trị Tên Đăng Nhập thực tế

            DataProvider dataProvider = new DataProvider();

            // Lấy MaNV từ TenDangNhap
            int maNV = dataProvider.GetMaNVByTenDangNhap(tenDangNhap1);
            if (maNV == 0)
            {
                MessageBox.Show("Không tìm thấy nhân viên!", "Thông báo");
                return;
            }

            // Lấy thông tin nhân viên dựa trên MaNV
            DataTable nhanVienData = dataProvider.GetNhanVienInfoByMaNV(maNV);
            if (nhanVienData.Rows.Count > 0)
            {
                DataRow row = nhanVienData.Rows[0];

                // Khởi tạo form sửa nhân viên
                fSuaNhanVien formSua = new fSuaNhanVien();
                formSua.MaNV = (int)row["MaNV"];
                formSua.TenNV = row["TenNV"].ToString();
                formSua.VaiTro = row["VaiTro"].ToString();
                formSua.NgaySinh = (DateTime)row["NgaySinh"];
                formSua.GioiTinh = row["GioiTinh"].ToString();
                formSua.MaCa = (int)row["MaCa"];
                formSua.HinhAnh = row["HinhAnh"]?.ToString();
                formSua.label1.Text = "Đổi mật khẩu";
                formSua.txtTen.ReadOnly = true;
                formSua.cboGioiTinh.Enabled = false;
                formSua.cboCa.Enabled = false;
                formSua.cboVaiTro.Enabled = false;
                formSua.dateTimePicker1.Enabled = false;
                formSua.txtTenDangNhap.ReadOnly = true;


                formSua.lblMatKhau.Text = "Mật khẩu mới";
                // Thêm Label thông báo mật khẩu
                Label lblThongBaoMatKhau = new Label
                {
                    Text = "(Đây là mật khẩu hiện tại của bạn. Nếu muốn thay đổi xin nhập lại!)",
                    Location = new Point(527, 181),
                    ForeColor = Color.Red,
                    Font = new Font("Arial", 9F, FontStyle.Italic),
                    AutoSize = true,
                    Visible = true
                };
                formSua.pnlTaiKhoan.Controls.Add(lblThongBaoMatKhau);

                // Lấy thông tin tài khoản từ bảng NguoiDung
                string queryNguoiDung = "SELECT TenDangNhap, MatKhau FROM NguoiDung WHERE MaNV = @MaNV";
                SqlParameter[] parameters = new SqlParameter[]
                {
            new SqlParameter("@MaNV", maNV)
                };
                DataTable nguoiDungData = dataProvider.ExecQuery(queryNguoiDung, parameters);

                if (nguoiDungData.Rows.Count > 0)
                {
                    formSua.TenDangNhap = nguoiDungData.Rows[0]["TenDangNhap"].ToString();
                    formSua.MatKhau = nguoiDungData.Rows[0]["MatKhau"].ToString();
                }

                formSua.SetImage(formSua.HinhAnh); // Hiển thị ảnh nếu có
                formSua.ShowDialog();
            }
            else
            {
                MessageBox.Show("Không tìm thấy thông tin nhân viên!", "Thông báo");
            }
        }

    
        #endregion

        #region DATBAN
        private void btnSua_Ban_Click(object sender, EventArgs e)
        {
            if (selectedBan != null)
            {
                // Khi có bàn được chọn, mở form fSuaBan và truyền thông tin bàn vào
                fSuaBan formSuaBan = new fSuaBan(selectedBan.MaBan, selectedBan.LoaiBan);
                formSuaBan.ShowDialog();

                // Sau khi form fSuaBan đóng, tải lại danh sách bàn
                LoadBanData();
            }
            else
            {
                MessageBox.Show("Vui lòng chọn bàn để sửa!", "Thông báo");
            }
        }

        private void btnXoa_Ban_Click(object sender, EventArgs e)
        {

            if (selectedBan == null)
            {
                MessageBox.Show("Vui lòng chọn bàn để xóa.");
                return;
            }

            // Kiểm tra xem MaBan có hợp lệ không
            if (selectedBan.MaBan <= 0)
            {
                MessageBox.Show("Mã bàn không hợp lệ.");
                return;
            }

            // Tạo form xác nhận xóa
            fXacNhanXoa confirmForm = new fXacNhanXoa();
            DialogResult result = confirmForm.ShowDialog();

            if (result == DialogResult.OK)
            {
                // Gọi hàm Xóa bàn
                XoaBan(selectedBan.MaBan);

                // Cập nhật lại danh sách bàn sau khi xóa
                LoadBanData();
            }

        }

        public void XoaBan(int maBan)
        {
            // Câu lệnh gọi Stored Procedure
            string query = "EXEC sp_XoaBan @MaBan";

            // Tạo tham số cho câu lệnh
            SqlParameter[] parameters = new SqlParameter[]
            {
        new SqlParameter("@MaBan", SqlDbType.Int) { Value = maBan }
            };

            // Sử dụng DataProvider để thực thi câu lệnh
            DataProvider dataProvider = new DataProvider();
            int rowsAffected = dataProvider.ExecNonQuery(query, parameters); // Thực thi câu lệnh xóa

            if (rowsAffected > 0)
            {
                MessageBox.Show($"Bàn {maBan} đã được xóa thành công.");
            }
            else
            {
                MessageBox.Show("Không tìm thấy bàn để xóa.");
            }
        }
        private void LuuBanTrucTiepVaoDatabase(string loaiBan, string trangThai)
        {
            // Chuỗi kết nối tới SQL Server
            string constr = dataProvider.constr;

            // Câu truy vấn thêm một bàn mới vào bảng "Ban"
            string query = "INSERT INTO Ban (LoaiBan, TrangThai) VALUES (@LoaiBan, @TrangThai)";

            // Sử dụng SqlConnection để kết nối tới database
            using (SqlConnection con = new SqlConnection(constr))
            {
                try
                {
                    // Mở kết nối
                    con.Open();

                    // Sử dụng SqlCommand để thực thi câu lệnh SQL
                    using (SqlCommand cmd = new SqlCommand(query, con))
                    {
                        // Thêm các tham số vào câu lệnh SQL
                        cmd.Parameters.AddWithValue("@LoaiBan", loaiBan);
                        cmd.Parameters.AddWithValue("@TrangThai", trangThai);

                        // Thực thi câu lệnh và kiểm tra kết quả
                        int result = cmd.ExecuteNonQuery();

                        if (result > 0)
                        {
                            MessageBox.Show("Thêm bàn thành công!", "Thông báo");
                        }
                        else
                        {
                            MessageBox.Show("Thêm bàn thất bại!", "Thông báo");
                        }
                    }
                }
                catch (Exception ex)
                {
                    // Xử lý ngoại lệ (lỗi) nếu có
                    MessageBox.Show("Lỗi kết nối: " + ex.Message, "Thông báo");
                }
            }
        }

        private void btnThem_Ban_Click(object sender, EventArgs e)
        {
            fXacNhanDatBan fXacNhan = new fXacNhanDatBan();
            fXacNhan.OnBanAdded += LoadBanData; // Đăng ký sự kiện
            fXacNhan.ShowDialog();
        }

        private void cboQuanLyBan_SelectedIndexChanged(object sender, EventArgs e)
        {

            LoadBanData();
        }

        #endregion

        #region NHANVIEN
        private void btnThem_NhanVien_Click(object sender, EventArgs e)
        {
            fThemNhanVien formThemNhanVien = new fThemNhanVien();

            // Hiển thị form fThemNhanVien
            formThemNhanVien.ShowDialog();
            LoadNhanVienData();
        }



        private void btnXoa_NhanVien_Click(object sender, EventArgs e)
        {
            // Kiểm tra xem có hàng nào được chọn trong DataGridView không
            if (dgvNhanVien.CurrentRow != null)
            {
                // Hiển thị form xác nhận xóa
                fXacNhanXoa confirmationForm = new fXacNhanXoa();
                confirmationForm.ShowDialog();

                // Nếu người dùng xác nhận xóa, thực hiện xóa
                if (confirmationForm.DialogResult == DialogResult.OK)
                {
                    // Lấy mã nhân viên từ dòng hiện tại
                    int maNV = (int)dgvNhanVien.CurrentRow.Cells["MaNV"].Value; // Lấy giá trị MaNV từ hàng hiện tại

                    // Gọi phương thức để xóa nhân viên từ cơ sở dữ liệu
                    DeleteNhanVien(maNV);

                    // Tải lại dữ liệu cho DataGridView
                    LoadNhanVienData();
                }
            }
            else
            {
                MessageBox.Show("Vui lòng chọn nhân viên để xóa.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
        private void DeleteNhanVien(int maNV)
        {
            using (SqlConnection connection = new SqlConnection(dataProvider.constr))
            {
                connection.Open();

                // Xóa thông tin người dùng nếu nhân viên này có tài khoản trong bảng NguoiDung
                string deleteNguoiDungQuery = "DELETE FROM NguoiDung WHERE MaNV = @MaNV";
                using (SqlCommand cmdNguoiDung = new SqlCommand(deleteNguoiDungQuery, connection))
                {
                    cmdNguoiDung.Parameters.AddWithValue("@MaNV", maNV);
                    cmdNguoiDung.ExecuteNonQuery();
                }

                // Xóa thông tin nhân viên từ bảng NhanVien
                string deleteNhanVienQuery = "DELETE FROM NhanVien WHERE MaNV = @MaNV";
                using (SqlCommand cmdNhanVien = new SqlCommand(deleteNhanVienQuery, connection))
                {
                    cmdNhanVien.Parameters.AddWithValue("@MaNV", maNV);
                    cmdNhanVien.ExecuteNonQuery();
                }
            }
        }

        private void btnSua_NhanVien_Click(object sender, EventArgs e)
        {
            if (dgvNhanVien.CurrentCell != null)
            {
                // Lấy dữ liệu từ hàng được chọn trong DataGridView
                DataGridViewRow row = dgvNhanVien.Rows[dgvNhanVien.CurrentCell.RowIndex];

                int maNV = (int)row.Cells["MaNV"].Value;
                string tenNV = row.Cells["TenNV"].Value.ToString();
                string vaiTro = row.Cells["VaiTro"].Value.ToString();
                DateTime ngaySinh = (DateTime)row.Cells["NgaySinh"].Value;
                string gioiTinh = row.Cells["GioiTinh"].Value.ToString();
                int maCa = (int)row.Cells["MaCa"].Value;
                string hinhAnh = row.Cells["HinhAnh"].Value?.ToString();
                string tenDangNhap = row.Cells["TenDangNhap"].Value?.ToString();
                string matKhau = row.Cells["MatKhau"].Value?.ToString();

                // Khởi tạo và gán dữ liệu vào form fSuaNhanVien
                fSuaNhanVien formSua = new fSuaNhanVien();
                formSua.MaNV = maNV;
                formSua.TenNV = tenNV;
                formSua.VaiTro = vaiTro;
                formSua.NgaySinh = ngaySinh;
                formSua.GioiTinh = gioiTinh;
                formSua.MaCa = maCa;
                formSua.HinhAnh = hinhAnh;
                formSua.TenDangNhap = tenDangNhap;
                formSua.MatKhau = matKhau;

                // Hiển thị ảnh nếu có
                formSua.SetImage(hinhAnh);

                formSua.ShowDialog();
                LoadNhanVienData(); // Tải lại dữ liệu sau khi sửa
            }
            else
            {
                MessageBox.Show("Vui lòng chọn nhân viên để sửa!", "Thông báo");
            }
        }

        private void txtTimKiem_NhanVien_TextChanged(object sender, EventArgs e)
        {
            string searchTerm = txtTimKiem_NhanVien.Text.ToLower();  // Lấy từ khóa tìm kiếm và chuyển thành chữ thường
            List<NhanVien> allNhanVien = GetNhanVienListFromDatabase();  // Lấy toàn bộ danh sách nhân viên từ cơ sở dữ liệu

            // Lọc danh sách nhân viên dựa trên tên (hoặc các thuộc tính khác như VaiTro, TenDangNhap, v.v.)
            var filteredList = allNhanVien.Where(item => item.TenNV.ToLower().Contains(searchTerm) || item.VaiTro.ToLower().Contains(searchTerm)).ToList();

            // Cập nhật lại DataGridView với danh sách nhân viên đã lọc
            dgvNhanVien.DataSource = filteredList;
        }

        private void txtTimKiem_NhanVien_Enter(object sender, EventArgs e)
        {
            if (txtTimKiem_NhanVien.Text == "Tìm kiếm ...")
            {
                txtTimKiem_NhanVien.Text = "";
                txtTimKiem_NhanVien.ForeColor = Color.Black; // Đổi màu chữ thành đen để dễ nhìn
            }
        }

        private void txtTimKiem_NhanVien_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtTimKiem_NhanVien.Text))
            {
                txtTimKiem_NhanVien.Text = "Tìm kiếm ...";
                txtTimKiem_NhanVien.ForeColor = Color.Gray; // Đổi màu chữ thành xám cho dễ nhận diện là placeholder
            }
        }
        #endregion

        #region KHUYENMAI

        private void btnThem_KM_Click(object sender, EventArgs e)
        {
            // Khởi tạo form fThemKhuyenMai
            fThemKhuyenMai formThemKM = new fThemKhuyenMai();
            formThemKM.OnDataSaved += LoadKhuyenMaiData;
            // Hiển thị form fThemKhuyenMai
            formThemKM.ShowDialog(); // Dùng ShowDialog để mở form ở chế độ Modal (chỉ có thể làm việc với form này cho đến khi đóng)
        }
        private void DeleteKhuyenMai(int maKM)
        {
            // Tạo chuỗi kết nối SQL
            string connectionString = dataProvider.constr; // Chuỗi kết nối từ DataProvider

            // Tạo câu lệnh SQL DELETE
            string query = "DELETE FROM KhuyenMai WHERE MaKM = @MaKM";

            // Thực thi câu truy vấn SQL
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                try
                {
                    con.Open(); // Mở kết nối

                    using (SqlCommand cmd = new SqlCommand(query, con))
                    {
                        // Thêm tham số vào câu lệnh SQL
                        cmd.Parameters.AddWithValue("@MaKM", maKM);

                        // Thực thi câu lệnh DELETE
                        cmd.ExecuteNonQuery();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi khi xóa khuyến mãi: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
        private void btnXoa_KM_Click(object sender, EventArgs e)
        {
            // Kiểm tra xem có ô nào được chọn trong DataGridView không
            if (dgvKhuyenMai.CurrentCell != null)
            {
                // Hiển thị form xác nhận xóa
                fXacNhanXoa confirmationForm = new fXacNhanXoa();
                confirmationForm.ShowDialog();

                // Nếu người dùng xác nhận xóa, thực hiện xóa
                if (confirmationForm.DialogResult == DialogResult.OK)
                {
                    // Lấy mã khuyến mãi từ hàng chứa ô hiện tại
                    int maKM = (int)dgvKhuyenMai.Rows[dgvKhuyenMai.CurrentCell.RowIndex].Cells["MaKM"].Value;

                    // Gọi phương thức để xóa khuyến mãi từ cơ sở dữ liệu
                    DeleteKhuyenMai(maKM);

                    // Tải lại dữ liệu cho DataGridView khuyến mãi
                    LoadKhuyenMaiData();

                    // Thông báo xóa thành công
                    MessageBox.Show("Đã xóa khuyến mãi thành công.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            else
            {
                // Thông báo nếu không có ô nào được chọn
                MessageBox.Show("Vui lòng chọn khuyến mãi để xóa.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void btnSua_KM_Click(object sender, EventArgs e)
        {
            // Kiểm tra xem có ô nào được chọn trong DataGridView không
            if (dgvKhuyenMai.CurrentCell != null)
            {
                // Lấy mã khuyến mãi từ hàng chứa ô hiện tại
                int maKM = (int)dgvKhuyenMai.Rows[dgvKhuyenMai.CurrentCell.RowIndex].Cells["MaKM"].Value;
                string tenKM = dgvKhuyenMai.Rows[dgvKhuyenMai.CurrentCell.RowIndex].Cells["TenKM"].Value.ToString();
                string moTa = dgvKhuyenMai.Rows[dgvKhuyenMai.CurrentCell.RowIndex].Cells["MoTa"].Value.ToString();
                decimal giaTri = (decimal)dgvKhuyenMai.Rows[dgvKhuyenMai.CurrentCell.RowIndex].Cells["GiaTriKM"].Value;
                DateTime thoiGianStart = (DateTime)dgvKhuyenMai.Rows[dgvKhuyenMai.CurrentCell.RowIndex].Cells["ThoiGianApDungStart"].Value;
                DateTime thoiGianEnd = (DateTime)dgvKhuyenMai.Rows[dgvKhuyenMai.CurrentCell.RowIndex].Cells["ThoiGianApDungEnd"].Value;

                // Mở form fSuaKhuyenMai và truyền dữ liệu
                fSuaKhuyenMai suaKMForm = new fSuaKhuyenMai();
                suaKMForm.SetKhuyenMaiData(maKM, tenKM, moTa, giaTri, thoiGianStart, thoiGianEnd);

                // Hiển thị form sửa khuyến mãi
                suaKMForm.ShowDialog();

                // Tải lại dữ liệu nếu người dùng đã lưu thay đổi
                LoadKhuyenMaiData();
            }
            else
            {
                MessageBox.Show("Vui lòng chọn khuyến mãi để sửa.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void txtTimKiem_KM_TextChanged(object sender, EventArgs e)
        {
            string searchTerm = txtTimKiem_KM.Text.ToLower(); // Lấy từ khóa tìm kiếm và chuyển thành chữ thường
            List<KhuyenMai> allKhuyenMai = GetKhuyenMaiListFromDatabase(); // Lấy toàn bộ danh sách khuyến mãi từ cơ sở dữ liệu

            // Lọc danh sách khuyến mãi dựa trên tên khuyến mãi hoặc mô tả
            var filteredList = allKhuyenMai
                .Where(item => item.TenKM.ToLower().Contains(searchTerm) || item.MoTa.ToLower().Contains(searchTerm))
                .ToList();

            // Cập nhật lại DataGridView với danh sách khuyến mãi đã lọc
            dgvKhuyenMai.DataSource = filteredList;
        }

        private void txtTimKiem_KM_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtTimKiem_KM.Text))
            {
                txtTimKiem_KM.Text = "Tìm kiếm ...";
                txtTimKiem_KM.ForeColor = Color.Gray; // Đổi màu chữ thành xám cho dễ nhận diện là placeholder
            }
        }

        private void txtTimKiem_KM_Enter(object sender, EventArgs e)
        {
            if (txtTimKiem_KM.Text == "Tìm kiếm ...")
            {
                txtTimKiem_KM.Text = "";
                txtTimKiem_KM.ForeColor = Color.Black; // Đổi màu chữ thành đen để dễ nhìn
            }
        }
        #endregion



        #region KHO
        private int selectedMaSP = 0;
        private int selectedSoLuong = 0;
        private void btnCapNhat_Kho_Click(object sender, EventArgs e)
        {
            // Kiểm tra nếu thông tin đã được lưu sau khi click vào dòng trong DataGridView
            if (selectedMaSP != 0)
            {
                // Mở form fCapNhatKho và truyền thông tin vào form
                fCapNhatKho formCapNhat = new fCapNhatKho();
                formCapNhat.MaSP = selectedMaSP;
                formCapNhat.SoLuong = selectedSoLuong;

                // Hiển thị form
                formCapNhat.ShowDialog();
                LoadKhoData();
            }
            else
            {
                MessageBox.Show("Vui lòng chọn một món ăn trong danh sách trước khi cập nhật.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void dgvKho_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            // Kiểm tra nếu người dùng click vào một ô trong DataGridView
            if (e.RowIndex >= 0) // Kiểm tra nếu có dòng được chọn
            {
                // Lấy thông tin từ dòng đã chọn
                int maSP = (int)dgvKho.Rows[e.RowIndex].Cells["MaSP"].Value;
                int soLuong = (int)dgvKho.Rows[e.RowIndex].Cells["SoLuong"].Value;

                // Lưu thông tin vào các biến hoặc fields để truyền cho form cập nhật
                selectedMaSP = maSP;
                selectedSoLuong = soLuong;
            }
        }

        private void txtTimKiem_Kho_TextChanged(object sender, EventArgs e)
        {

            string searchTerm = txtTimKiem_Kho.Text.ToLower();  // Lấy từ khóa tìm kiếm và chuyển thành chữ thường
            List<Kho> allKho = GetKhoListFromDatabase();  // Lấy toàn bộ danh sách hàng trong kho từ cơ sở dữ liệu

            // Lọc danh sách kho dựa trên tên sản phẩm (hoặc các thuộc tính khác như Mã sản phẩm, Số lượng, v.v.)
            var filteredList = allKho.Where(item => item.TenMon.ToLower().Contains(searchTerm) || item.MaSP.ToString().Contains(searchTerm)).ToList();

            // Cập nhật lại DataGridView với danh sách kho đã lọc
            dgvKho.DataSource = filteredList;
        }

        private void txtTimKiem_Kho_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtTimKiem_Kho.Text))
            {
                txtTimKiem_Kho.Text = "Tìm kiếm ...";
                txtTimKiem_Kho.ForeColor = Color.Gray; // Đổi màu chữ thành xám cho dễ nhận diện là placeholder
            }
        }

        private void txtTimKiem_Kho_Enter(object sender, EventArgs e)
        {
            if (txtTimKiem_Kho.Text == "Tìm kiếm ...")
            {
                txtTimKiem_Kho.Text = "";
                txtTimKiem_Kho.ForeColor = Color.Black; // Đổi màu chữ thành đen để dễ nhìn
            }
        }
        #endregion




        #region KHACHHANG
        private void btnThem_KH_Click(object sender, EventArgs e)
        {
            fThemKhachHang formThemKH = new fThemKhachHang();
            formThemKH.ShowDialog();
            LoadKhachHangData();
        }
        private void DeleteKhachHang(int maKH)
        {
            // Câu lệnh SQL xóa khách hàng theo mã khách hàng
            string query = "DELETE FROM KhachHang WHERE MaKH = @MaKH";

            // Tạo tham số cho câu lệnh
            SqlParameter[] parameters = new SqlParameter[]
            {
        new SqlParameter("@MaKH", maKH)
            };

            // Thực thi câu lệnh
            int result = dataProvider.ExecNonQuery(query, parameters);
            if (result > 0)
            {
                MessageBox.Show("Xóa khách hàng thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("Xóa khách hàng thất bại!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void btnXoa_KH_Click(object sender, EventArgs e)
        {

            // Kiểm tra xem có hàng nào được chọn trong DataGridView không
            if (dgvKhachHang.CurrentRow != null)
            {
                // Hiển thị form xác nhận xóa
                fXacNhanXoa confirmationForm = new fXacNhanXoa();
                confirmationForm.ShowDialog();

                // Nếu người dùng xác nhận xóa, thực hiện xóa
                if (confirmationForm.DialogResult == DialogResult.OK)
                {
                    // Lấy mã khách hàng từ dòng hiện tại
                    int maKH = (int)dgvKhachHang.CurrentRow.Cells["MaKH"].Value; // Lấy giá trị MaKH từ hàng hiện tại

                    // Gọi phương thức để xóa khách hàng từ cơ sở dữ liệu
                    DeleteKhachHang(maKH);

                    // Tải lại dữ liệu cho DataGridView
                    LoadKhachHangData();
                }
            }
            else
            {
                MessageBox.Show("Vui lòng chọn khách hàng để xóa.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void btnSua_KH_Click(object sender, EventArgs e)
        {
            // Lấy thông tin từ dòng hiện tại (dòng được chọn trong DataGridView)
            if (dgvKhachHang.CurrentRow != null)
            {
                int maKH = (int)dgvKhachHang.CurrentRow.Cells["MaKH"].Value;
                string hoTen = dgvKhachHang.CurrentRow.Cells["HoTen"].Value.ToString();
                string sdt = dgvKhachHang.CurrentRow.Cells["SDT"].Value.ToString();
                DateTime ngaySinh = (DateTime)dgvKhachHang.CurrentRow.Cells["NgaySinh"].Value;
                string email = dgvKhachHang.CurrentRow.Cells["Email"].Value.ToString();
                int diemTichLuy = (int)dgvKhachHang.CurrentRow.Cells["DiemTichLuy"].Value;

                // Tạo instance của form fSuaKhachHang
                fSuaKhachHang formSua = new fSuaKhachHang();

                // Truyền dữ liệu vào form fSuaKhachHang
                formSua.MaKH = maKH;
                formSua.HoTen = hoTen;
                formSua.SDT = sdt;
                formSua.NgaySinh = ngaySinh;
                formSua.Email = email;
                formSua.DiemTichLuy = diemTichLuy;

                // Hiển thị form sửa
                formSua.ShowDialog();
                LoadKhachHangData();
            }
            else
            {
                MessageBox.Show("Vui lòng chọn khách hàng để sửa!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void txtTimKiem_KH_TextChanged(object sender, EventArgs e)
        {
            string searchTerm = txtTimKiem_KH.Text.ToLower(); // Lấy từ khóa tìm kiếm và chuyển thành chữ thường
            List<KhachHang> allKhachHang = GetKhachHangListFromDatabase(); // Lấy toàn bộ danh sách khách hàng từ cơ sở dữ liệu

            // Lọc danh sách khách hàng dựa trên các thuộc tính như: tên khách hàng, email, số điện thoại,...
            var filteredList = allKhachHang.Where(item =>
                item.HoTen.ToLower().Contains(searchTerm) ||
                item.SDT.ToLower().Contains(searchTerm) ||
                item.Email.ToLower().Contains(searchTerm)).ToList();

            // Cập nhật lại DataGridView với danh sách khách hàng đã lọc
            dgvKhachHang.DataSource = filteredList;
        }

        private void txtTimKiem_KH_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtTimKiem_KH.Text))
            {
                txtTimKiem_KH.Text = "Tìm kiếm ...";
                txtTimKiem_KH.ForeColor = Color.Gray; // Đổi màu chữ thành xám cho dễ nhận diện là placeholder
            }

        }

        private void txtTimKiem_KH_Enter(object sender, EventArgs e)
        {

            if (txtTimKiem_KH.Text == "Tìm kiếm ...")
            {
                txtTimKiem_KH.Text = "";
                txtTimKiem_KH.ForeColor = Color.Black; // Đổi màu chữ thành đen để dễ nhìn
            }
        }
        #endregion



        #region HOADON
        private void FilterBySearchTerm()
        {
            string searchTerm = txtTimKiem_LS.Text.ToLower();
            List<HoaDon> allHoaDon = GetHoaDonListFromDatabase();

            var filteredList = allHoaDon.Where(item =>
                item.SoHoaDon.ToString().Contains(searchTerm) ||
                item.MaKH.ToString().Contains(searchTerm) ||
                item.NgayLapHoaDon.ToString("dd/MM/yyyy").Contains(searchTerm) ||
                item.HinhThucThanhToan.ToLower().Contains(searchTerm)
            ).ToList();

            dgvLichSu.DataSource = filteredList;
        }

        private void FilterByDate()
        {
            DateTime startDate = dtpStart_LS.Value.Date;
            DateTime endDate = dtpEnd_LS.Value.Date;

            List<HoaDon> allHoaDon = GetHoaDonListFromDatabase();

            var filteredList = allHoaDon.Where(item =>
                item.NgayLapHoaDon >= startDate && item.NgayLapHoaDon <= endDate
            ).ToList();

            dgvLichSu.DataSource = filteredList;
        }

        private void txtTimKiem_LS_TextChanged(object sender, EventArgs e)
        {
            FilterBySearchTerm();
        }




        private void dtpStart_LS_ValueChanged(object sender, EventArgs e)
        {
            FilterByDate();
        }

        private void dtpEnd_LS_ValueChanged(object sender, EventArgs e)
        {
            FilterByDate();
        }

        private void txtTimKiem_LS_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtTimKiem_LS.Text))  // Nếu ô tìm kiếm rỗng
            {
                txtTimKiem_LS.Text = "Tìm kiếm...";  // Đặt lại text mặc định
                txtTimKiem_LS.ForeColor = Color.Gray;  // Đổi màu chữ thành màu xám
            }
        }

        private void txtTimKiem_LS_Enter(object sender, EventArgs e)
        {
            if (txtTimKiem_LS.Text == "Tìm kiếm..." || string.IsNullOrEmpty(txtTimKiem_LS.Text))  // Nếu là text mặc định hoặc rỗng
            {
                txtTimKiem_LS.Text = "";  // Xóa text đi
                txtTimKiem_LS.ForeColor = Color.Black;  // Đổi màu chữ thành đen
            }
        }

        private void btnXuatExcel_LS_Click(object sender, EventArgs e)
        {
            using (var workbook = new XLWorkbook())
            {
                var worksheet = workbook.Worksheets.Add("LichSu");

                // Thêm tiêu đề cho các cột (giả sử bạn có DataGridView là dgvLichSu)
                for (int i = 0; i < dgvLichSu.Columns.Count; i++)
                {
                    worksheet.Cell(1, i + 1).Value = dgvLichSu.Columns[i].HeaderText;
                }

                // Thêm dữ liệu vào các ô
                for (int i = 0; i < dgvLichSu.Rows.Count; i++)
                {
                    for (int j = 0; j < dgvLichSu.Columns.Count; j++)
                    {
                        // Sử dụng ToString() để tránh lỗi chuyển đổi kiểu
                        worksheet.Cell(i + 2, j + 1).Value = dgvLichSu.Rows[i].Cells[j].Value?.ToString();
                    }
                }

                // Lưu file
                using (var saveFileDialog = new SaveFileDialog() { Filter = "Excel Workbook|*.xlsx" })
                {
                    if (saveFileDialog.ShowDialog() == DialogResult.OK)
                    {
                        workbook.SaveAs(saveFileDialog.FileName);
                        MessageBox.Show("Xuất dữ liệu thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }

            }
        }
        #endregion


        #region USER
        #endregion
        private void btnUser_ThucDon_Click(object sender, EventArgs e)
        {

            contextMenuStrip1.Show(btnUser_ThucDon, new Point(btnUser_ThucDon.Width - contextMenuStrip1.Width, btnUser_ThucDon.Height));

        }

        private void btnUser_Ban_Click(object sender, EventArgs e)
        {
            contextMenuStrip1.Show(btnUser_ThucDon, new Point(btnUser_ThucDon.Width - contextMenuStrip1.Width, btnUser_ThucDon.Height));
        }

        private void btnUser_NhanVien_Click(object sender, EventArgs e)
        {
            contextMenuStrip1.Show(btnUser_NhanVien, new Point(btnUser_NhanVien.Width - contextMenuStrip1.Width, btnUser_NhanVien.Height));
        }

        private void btnUser_KM_Click(object sender, EventArgs e)
        {
            contextMenuStrip1.Show(btnUser_KM, new Point(btnUser_KM.Width - contextMenuStrip1.Width, btnUser_KM.Height));
        }

        private void btnUser_Kho_Click(object sender, EventArgs e)
        {
            contextMenuStrip1.Show(btnUser_Kho, new Point(btnUser_Kho.Width - contextMenuStrip1.Width, btnUser_Kho.Height));
        }

        private void btnUser_KH_Click(object sender, EventArgs e)
        {
            contextMenuStrip1.Show(btnUser_KH, new Point(btnUser_KH.Width - contextMenuStrip1.Width, btnUser_KH.Height));
        }

        private void btnUser_LS_Click(object sender, EventArgs e)
        {
            contextMenuStrip1.Show(btnUser_LS, new Point(btnUser_LS.Width - contextMenuStrip1.Width, btnUser_LS.Height));
        }
    }
}
