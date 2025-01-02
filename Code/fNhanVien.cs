using QL_Bida.Model;

using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Drawing;


using System.Windows.Forms;
using System.Data.SqlClient;
using ClosedXML.Excel;

namespace QL_Bida
{
    public partial class fNhanVien : Form
    {
        DataProvider dataProvider = new DataProvider();
        private List<SelectedItem> selectedItems = new List<SelectedItem>();
        private string currentMenuType = string.Empty;
        private string tenDangNhap;
        public int maNV;
        public static bool ExitApplication = false;

        public fNhanVien(string tenDangNhap)
        {
            InitializeComponent();
            this.tenDangNhap = tenDangNhap;
            LayMaNV();
            tabControl1.Appearance = TabAppearance.FlatButtons;
            tabControl1.ItemSize = new Size(0, 1);
            tabControl1.SizeMode = TabSizeMode.Fixed;
            this.FormBorderStyle = FormBorderStyle.None;
            this.Scale(new SizeF(1.5f, 1.5f));


        }
        private List<MenuItem> GetMenuItemsFromDatabase()
        {
            // Truy vấn sử dụng UDF để lấy danh sách món ăn
            string query = "SELECT * FROM dbo.fn_GetMenuItems()";
            DataTable dataTable = dataProvider.ExecQuery(query); // Sử dụng phương thức ExecQuery của DataProvider

            List<MenuItem> menuItems = new List<MenuItem>();
            foreach (DataRow row in dataTable.Rows)
            {
                MenuItem item = new MenuItem
                {
                    MaMon = Convert.ToInt32(row["MaMon"]),
                    TenMon = row["TenMon"].ToString(),
                    LoaiMon = row["LoaiMon"]?.ToString(),
                    NhomThucDon = row["NhomThucDon"]?.ToString(),
                    Gia = Convert.ToDecimal(row["Gia"]),
                    HinhAnh = row["HinhAnh"]?.ToString() // Đường dẫn hình ảnh (có thể null)
                };
                menuItems.Add(item);
            }

            return menuItems;
        }


        private List<Ban> GetBanListFromDatabase()
        {
            string query = "SELECT * FROM BanBilliards"; // Truy vấn SQL
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

        private List<KhachHang> GetKhachHangListFromDatabase()
        {
            // Truy vấn sử dụng UDF để lấy danh sách khách hàng
            string query = "SELECT * FROM dbo.fn_GetKhachHangList()";
            DataTable dataTable = dataProvider.ExecQuery(query); // Sử dụng phương thức ExecQuery của DataProvider

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
            // Truy vấn sử dụng UDF để lấy danh sách hóa đơn
            string query = "sp_GetHoaDonList";
            DataTable dataTable = dataProvider.ExecQuery(query); // Sử dụng phương thức ExecQuery của DataProvider

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
        private void LayMaNV()
        {
            // Kết nối tới cơ sở dữ liệu
            using (SqlConnection connection = new SqlConnection(dataProvider.constr))
            {
                try
                {
                    connection.Open();

                    // Sử dụng hàm fn_LayMaNV để lấy MaNV
                    string query = "SELECT dbo.fn_LayMaNV(@TenDangNhap)";
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@TenDangNhap", tenDangNhap);

                        // Lấy kết quả từ hàm
                        object result = command.ExecuteScalar();

                        // Kiểm tra kết quả
                        if (result != null && Convert.ToInt32(result) > 0)
                        {
                            maNV = Convert.ToInt32(result); // Gán giá trị cho maNV
                        }
                        else
                        {
                            MessageBox.Show("Không tìm thấy nhân viên!");
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Lỗi: {ex.Message}");
                }
            }
        }


        private void ChonMon(string tenMon, decimal gia)
        {
            var item = selectedItems.FirstOrDefault(i => i.TenMon == tenMon);
            if (item == null)
            {
                // Thêm món mới vào danh sách nếu chưa có
                item = new SelectedItem { TenMon = tenMon, SoLuong = 1, Gia = gia, ThanhTien = gia };
                selectedItems.Add(item);

                // Tạo Panel mới cho món ăn và thêm vào FlowLayoutPanel
                Panel panel = TaoPanelMon(item);
                fplChiTietHoaDon.Controls.Add(panel);
            }
            else
            {
                // Tăng số lượng nếu món đã tồn tại
                item.SoLuong++;
                item.ThanhTien = item.SoLuong * item.Gia; // Cập nhật lại thành tiền

                // Tìm panel của món này và cập nhật lại giá trị của NumericUpDown và thành tiền
                foreach (Panel panel in fplChiTietHoaDon.Controls)
                {
                    if (panel.Tag == item)
                    {
                        NumericUpDown nudSoLuong = panel.Controls.OfType<NumericUpDown>().FirstOrDefault();
                        if (nudSoLuong != null)
                        {
                            nudSoLuong.Value = item.SoLuong;
                        }

                        CapNhatPanelMon(item);
                        break;
                    }
                }
            }

            CapNhatTongTien();
        }
        private Panel TaoPanelMon(SelectedItem item)
        {
            Panel panel = new Panel
            {
                Width = 700,
                Height = 50,
                Tag = item,
                Margin = new Padding(0),
                BorderStyle = BorderStyle.None,
                BackColor = Color.FromArgb(235, 228, 226),
            };

            Label lblTenMon = new Label
            {
                Text = item.TenMon,
                Width = 150,
                TextAlign = ContentAlignment.MiddleLeft
            };

            NumericUpDown nudSoLuong = new NumericUpDown
            {
                Value = item.SoLuong,
                Width = 60,
                Minimum = 1,
                Maximum = 100
            };

            Label lblThanhTien = new Label
            {
                Text = $"{item.ThanhTien:N0} VND",
                Width = 100,
                TextAlign = ContentAlignment.MiddleRight
            };

            Button btnXoa = new Button
            {
                Text = "X",
                Width = 30,
                Height = 30,
                BackColor = Color.Red,
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                Font = new Font("Arial", 10, FontStyle.Bold),
                TextAlign = ContentAlignment.MiddleCenter
            };

            btnXoa.FlatAppearance.BorderSize = 0;
            btnXoa.Click += (s, e) =>
            {
                selectedItems.Remove(item);
                fplChiTietHoaDon.Controls.Remove(panel);
                CapNhatTongTien();
            };

            // Sự kiện ValueChanged của NumericUpDown
            nudSoLuong.ValueChanged += (s, e) =>
            {
                CapNhatSoLuongVaThanhTien(item, nudSoLuong);
            };

            // Thêm sự kiện Leave để xử lý khi người dùng nhập số lượng bằng tay
            nudSoLuong.Leave += (s, e) =>
            {
                CapNhatSoLuongVaThanhTien(item, nudSoLuong);
            };

            panel.Controls.Add(lblTenMon);
            panel.Controls.Add(nudSoLuong);
            panel.Controls.Add(lblThanhTien);
            panel.Controls.Add(btnXoa);

            int centerY = (panel.Height - lblTenMon.Height) / 2;
            lblTenMon.Location = new Point(10, centerY);
            nudSoLuong.Location = new Point(280, centerY);
            lblThanhTien.Location = new Point(490, centerY);
            btnXoa.Location = new Point(620, centerY);

            return panel;
        }

        private void CapNhatSoLuongVaThanhTien(SelectedItem item, NumericUpDown nudSoLuong)
        {
            item.SoLuong = (int)nudSoLuong.Value;
            item.ThanhTien = item.SoLuong * item.Gia; // Cập nhật lại thành tiền
            CapNhatPanelMon(item); // Cập nhật hiển thị của ThanhTien
            CapNhatTongTien(); // Cập nhật tổng tiền hóa đơn
        }

        private void CapNhatPanelMon(SelectedItem item)
        {
            // Tìm panel của món này và cập nhật lại thông tin
            foreach (Panel panel in fplChiTietHoaDon.Controls)
            {
                if (panel.Tag == item)
                {
                    // Cập nhật lại thành tiền
                    Label lblThanhTien = panel.Controls.OfType<Label>().Last();
                    lblThanhTien.Text = $"{item.ThanhTien:N0} VND";
                    break;
                }
            }
        }

        private void CapNhatTongTien()
        {
            decimal tongTien = selectedItems.Sum(i => i.ThanhTien);
            lblTongTien.Text = $"{tongTien:N0} VND";
        }



        






        // Sự kiện khi tab được chọn
        private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Ẩn tất cả các Panel
            pnlThucDon.Visible = false;
            pnlQuanLyBan.Visible = false;

            pnlKhachHang.Visible = false;
            pnlLichSu.Visible = false;

            // Kiểm tra tab được chọn và hiển thị Panel tương ứng
            switch (tabControl1.SelectedIndex)
            {
                case 0: // Tab Thực Đơn
                    pnlThucDon.Visible = true;
                    LoadMenuItems(); // Tải dữ liệu Thực Đơn
                    lblUsername_TD.Text = tenDangNhap;
                    LoadBanToComboBox();

                    break;
                case 1: // Tab Quản Lý Bàn
                    pnlQuanLyBan.Visible = true;
                    LoadBanData(); // Tải dữ liệu Bàn
                    if (!cboLoai.Items.Contains("Tất cả"))
                    {
                        cboLoai.Items.Insert(0, "Tất cả");
                    }
                    LoadBanToComboBox();
                    // Kiểm tra và thêm "Tất cả" vào ComboBox Trạng Thái nếu chưa có
                    if (!cboTrangThai.Items.Contains("Tất cả"))
                    {
                        cboTrangThai.Items.Insert(0, "Tất cả");
                    }

                    // Thiết lập giá trị mặc định là "Tất cả"
                    cboLoai.SelectedIndex = 0;
                    cboTrangThai.SelectedIndex = 0;
                    lblUserName_Ban.Text = tenDangNhap;
                    break;
                case 2: // Tab Nhân Viên
                    pnlKhachHang.Visible = true;
                    LoadKhachHangData();
                    lblUserName_KH.Text = tenDangNhap;
                    break;
                case 3:
                    pnlLichSu.Visible = true;
                    lblUserName_LSC.Text = tenDangNhap;
                    LoadHoaDonData();
                    break;




            }
        }
        // Định nghĩa màu sắc cho nút khi chọn và không chọn
        Color defaultColor = Color.FromArgb(102, 44, 33); // Màu mặc định (có thể thay đổi)
        Color selectedColor = Color.FromArgb(122, 52, 39);  // Màu khi được chọn
        private void ResetButtonColors()
        {
            btnThucDon.BackColor = defaultColor;
            btnQuanLyBan.BackColor = defaultColor;
           
            btnLichSu.BackColor = defaultColor;
            btnKhachHang.BackColor = defaultColor;
        }

        private void btnThucDon_Click(object sender, EventArgs e)
        {
            ResetButtonColors(); // Đặt lại màu cho tất cả các nút
            btnThucDon.BackColor = selectedColor; // Đổi màu cho nút hiện tại
            tabControl1.SelectedIndex = 0;
        }

        private void btnQuanLyBan_Click(object sender, EventArgs e)
        {
            ResetButtonColors(); // Đặt lại màu cho tất cả các nút
            btnQuanLyBan.BackColor = selectedColor; // Đổi màu cho nút hiện tại
            tabControl1.SelectedIndex = 1;

        }

        private void btnKhachHang_Click(object sender, EventArgs e)
        {
            ResetButtonColors(); // Đặt lại màu cho tất cả các nút
            btnKhachHang.BackColor = selectedColor; // Đổi màu cho nút hiện tại
            tabControl1.SelectedIndex = 2;

        }

        private void btnLichSu_Click(object sender, EventArgs e)
        {
            ResetButtonColors(); // Đặt lại màu cho tất cả các nút
            btnLichSu.BackColor = selectedColor; // Đổi màu cho nút hiện tại
            tabControl1.SelectedIndex = 3;

        }
       




        private void LoadBanData(string loaiBanFilter = "", string trangThaiFilter = "")
        {
            // Xóa các nút hiện có trong FlowLayoutPanel
            flpQuanLyBan.Controls.Clear();

            // Lấy danh sách bàn mới nhất từ database
            List<Ban> banList = GetBanListFromDatabase();

            // Áp dụng bộ lọc theo loại bàn (nếu có)
            if (!string.IsNullOrEmpty(loaiBanFilter))
            {
                banList = banList.Where(b => b.LoaiBan == loaiBanFilter).ToList();
            }

            // Áp dụng bộ lọc theo trạng thái (nếu có)
            if (!string.IsNullOrEmpty(trangThaiFilter))
            {
                banList = banList.Where(b => b.TrangThai == trangThaiFilter).ToList();
            }

            // Duyệt qua từng bàn để tạo nút
            foreach (Ban ban in banList)
            {
                Button btnBan = new Button
                {
                    Width = 208,
                    Height = 208,
                    Text = $"Bàn {ban.MaBan}",
                    Tag = ban
                };

                // Đặt màu nền theo trạng thái
                btnBan.BackColor = (ban.TrangThai == "Trống")
                    ? Color.FromArgb(0, 206, 209)  // Màu xanh cho bàn trống
                    : Color.FromArgb(254, 142, 142); // Màu đỏ cho bàn đang sử dụng

                // Gán sự kiện click cho từng nút bàn
                btnBan.Click += (s, e) =>
                {
                    // Lấy dữ liệu mới nhất từ database
                    Ban currentBan = GetBanFromDatabaseById(ban.MaBan);

                    // Kiểm tra trạng thái của bàn sau khi lấy từ database
                    if (currentBan.TrangThai == "Trống")
                    {
                        // Mở form fMoBan cho bàn trống
                        fMoBan frmMoBan = new fMoBan();
                        frmMoBan.MaBan = currentBan.MaBan;
                        DialogResult result = frmMoBan.ShowDialog();

                        // Sau khi mở bàn, lưu lại thời gian mở vào Dictionary
                        if (result == DialogResult.OK)
                        {
                            if (fNhanVien.banMoThoiGian.ContainsKey(currentBan.MaBan))
                            {
                                fNhanVien.banMoThoiGian.Remove(currentBan.MaBan);
                            }
                            fNhanVien.banMoThoiGian[currentBan.MaBan] = DateTime.Now;
                        }
                    }
                    else if (currentBan.TrangThai == "Đang Sử Dụng")
                    {
                        // Mở form fDatBan_ChonHanhDong cho bàn đang sử dụng
                        fDatBan_ChonHanhDong frmChonHanhDong = new fDatBan_ChonHanhDong();
                        frmChonHanhDong.MaBan = currentBan.MaBan;

                        // Kiểm tra kết quả sau khi form đóng
                        DialogResult result = frmChonHanhDong.ShowDialog();

                        if (result == DialogResult.OK)
                        {
                            // Chuyển sang tab thực đơn và tải lại dữ liệu
                            tabControl1.SelectedIndex = 0;
                            LoadMenuItems();
                            lblUsername_TD.Text = tenDangNhap;
                            cboBan.SelectedValue = frmChonHanhDong.MaBan;
                        }
                        else if (result == DialogResult.No)
                        {
                            // Cập nhật trạng thái bàn thành "Trống"
                            UpdateBanTrangThai(currentBan.MaBan, "Trống");

                            // Xóa bàn khỏi Dictionary nếu nó tồn tại
                            if (fNhanVien.banMoThoiGian.ContainsKey(currentBan.MaBan))
                            {
                                fNhanVien.banMoThoiGian.Remove(currentBan.MaBan);
                            }

                            // Xóa form thanh toán tương ứng trong danhSachThanhToan
                            if (danhSachThanhToan.ContainsKey(currentBan.MaBan))
                            {
                                // Đóng form thanh toán
                                danhSachThanhToan[currentBan.MaBan].Close();
                                // Xóa form thanh toán khỏi dictionary
                                danhSachThanhToan.Remove(currentBan.MaBan);
                            }
                        }
                    }

                    // Sau khi đóng form, tải lại dữ liệu bàn để cập nhật trạng thái
                    LoadBanData(loaiBanFilter, trangThaiFilter);
                };

                // Thêm nút vào FlowLayoutPanel
                flpQuanLyBan.Controls.Add(btnBan);
            }
        }


        private Ban GetBanFromDatabaseById(int maBan)
        {
            // Câu truy vấn SQL để lấy thông tin bàn theo MaBan
            string query = $"SELECT * FROM BanBilliards WHERE MaBan = {maBan}";

            // Sử dụng phương thức ExecQuery của DataProvider để thực hiện truy vấn
            DataTable dataTable = dataProvider.ExecQuery(query);

            // Kiểm tra nếu có dữ liệu trả về
            if (dataTable.Rows.Count > 0)
            {
                DataRow row = dataTable.Rows[0]; // Lấy hàng đầu tiên
                Ban item = new Ban
                {
                    MaBan = Convert.ToInt32(row["MaBan"]),
                    LoaiBan = row["LoaiBan"].ToString(),
                    TrangThai = row["TrangThai"].ToString() // Giả sử TrangThai là string
                };
                return item;
            }

            // Trả về null nếu không tìm thấy bàn
            return null;
        }

        private void UpdateBanTrangThai(int maBan, string trangThai)
        {
            using (SqlConnection connection = new SqlConnection(dataProvider.constr))
            {
                try
                {
                    connection.Open();
                    string updateQuery = "UPDATE BanBilliards SET TrangThai = @TrangThai WHERE MaBan = @MaBan";
                    using (SqlCommand command = new SqlCommand(updateQuery, connection))
                    {
                        command.Parameters.AddWithValue("@TrangThai", trangThai);
                        command.Parameters.AddWithValue("@MaBan", maBan);
                        command.ExecuteNonQuery();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Lỗi khi cập nhật trạng thái bàn: {ex.Message}");
                }
            }
        }

        private void ApplyFilters()
        {
            // Lấy giá trị đã chọn từ ComboBox
            string loaiBan = cboLoai.SelectedItem?.ToString() ?? "";
            string trangThai = cboTrangThai.SelectedItem?.ToString() ?? "";

            // Nếu chọn "Tất cả", thì bỏ qua bộ lọc
            if (loaiBan == "Tất cả") loaiBan = "";
            if (trangThai == "Tất cả") trangThai = "";

            // Tải lại dữ liệu với bộ lọc
            LoadBanData(loaiBan, trangThai);
        }

        // Hàm xử lý khi bấm vào nút Bàn
        private void BtnBan_Click(object sender, EventArgs e)
        {
            Button btnBan = sender as Button;
            Ban ban = btnBan.Tag as Ban;

            if (ban != null)
            {
                MessageBox.Show($"Bạn vừa chọn {ban.LoaiBan} - Bàn {ban.MaBan}\nTrạng thái: {ban.TrangThai}", "Thông báo");
            }
        }


   

        private void LoadKhachHangData()
        {
            // Gán danh sách khách hàng vào DataGridView
            dgvKhachHang.DataSource = GetKhachHangListFromDatabase();
            dgvKhachHang.ColumnHeadersDefaultCellStyle.BackColor = Color.White;
            dgvKhachHang.ColumnHeadersDefaultCellStyle.ForeColor = Color.DarkRed;
            dgvKhachHang.RowHeadersVisible = false;
            dgvKhachHang.ColumnHeadersHeight = 40;
            //dgvKhachHang.EnableHeadersVisualStyles = false;
            //dgvKhachHang.AdvancedColumnHeadersBorderStyle.All = DataGridViewAdvancedCellBorderStyle.None;
            dgvKhachHang.ColumnHeadersDefaultCellStyle.Font = new Font(dgvKhachHang.Font.FontFamily, dgvKhachHang.Font.Size + 2, FontStyle.Bold);
            dgvKhachHang.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
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
            dgvLichSuCa.DataSource = GetHoaDonListFromDatabase();
            dgvLichSuCa.ColumnHeadersDefaultCellStyle.BackColor = Color.White;
            dgvLichSuCa.ColumnHeadersDefaultCellStyle.ForeColor = Color.DarkRed;
            dgvLichSuCa.RowHeadersVisible = false;
            dgvLichSuCa.ColumnHeadersHeight = 40;
            dgvLichSuCa.ColumnHeadersDefaultCellStyle.Font = new Font(dgvLichSuCa.Font.FontFamily, dgvLichSuCa.Font.Size + 2, FontStyle.Bold);
            dgvLichSuCa.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgvLichSuCa.DefaultCellStyle.Font = new Font(dgvKhachHang.Font.FontFamily, dgvKhachHang.Font.Size + 2);
            dgvLichSuCa.Columns["SoHoaDon"].HeaderText = "Số Hoá Đơn";
            dgvLichSuCa.Columns["MaKH"].HeaderText = "Mã Khách Hàng";
            dgvLichSuCa.Columns["MaBan"].HeaderText = "Mã Bàn";
            dgvLichSuCa.Columns["MaNV"].HeaderText = "Mã Nhân Viên";
            dgvLichSuCa.Columns["SoGioChoi"].HeaderText = "Số Giờ Chơi";
            dgvLichSuCa.Columns["ThanhTien"].HeaderText = "Thành Tiền";
            dgvLichSuCa.Columns["NgayLapHoaDon"].HeaderText = "Ngày Lập Hoá Đơn";
            dgvLichSuCa.Columns["HinhThucThanhToan"].HeaderText = "Hình Thức Thanh Toán";
            dgvLichSuCa.Columns["MaKM"].HeaderText = "Mã Khuyến Mãi";
        }

        private void fNhanVien_Load(object sender, EventArgs e)
        {
            tabControl1.SelectedIndex = 1;

        }

        // Biến để theo dõi trạng thái của menu hiện tại
        private void LoadMenuItems(string loaiMon = null)
        {
            // Thư mục lưu trữ hình ảnh
            string imageFolder = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "HinhAnh", "ThucDon");

            // Xóa tất cả control cũ nếu có
            fplThucDon.Controls.Clear();

            // Lấy danh sách món ăn từ cơ sở dữ liệu
            List<MenuItem> menuItems = GetMenuItemsFromDatabase();

            // Nếu có loại món ăn để lọc, thực hiện lọc
            if (!string.IsNullOrEmpty(loaiMon))
            {
                menuItems = menuItems.Where(item => item.LoaiMon == loaiMon).ToList();
            }

            foreach (var item in menuItems)
            {
                // Tạo Button hoặc Panel cho mỗi món ăn
                Button btnItem = new Button
                {
                    Width = 180,
                    Height = 220,
                    Text = $"{item.TenMon}\n{item.Gia:N0} VND",
                    Tag = item,
                    TextAlign = ContentAlignment.BottomCenter,
                    ImageAlign = ContentAlignment.TopCenter,
                    Font = new Font("Arial", 10, FontStyle.Bold),
                    ForeColor = Color.Black,
                    BackColor = Color.FromArgb(217, 186, 166)
                };

                // Tạo đường dẫn đầy đủ cho hình ảnh
                string imagePath = Path.Combine(imageFolder, item.HinhAnh);

                // Thêm hình ảnh nếu có
                if (!string.IsNullOrEmpty(item.HinhAnh) && File.Exists(imagePath))
                {
                    using (Image img = Image.FromFile(imagePath))
                    {
                        btnItem.Image = new Bitmap(img, new Size(180, 170)); // Điều chỉnh kích thước hình ảnh
                    }
                }

                // Thêm sự kiện Click cho Button
                btnItem.Click += (sender, e) =>
                {
                    var selectedItem = (MenuItem)((Button)sender).Tag;
                    ChonMon(selectedItem.TenMon, selectedItem.Gia); // Gọi phương thức chọn món khi người dùng nhấp vào
                };

                // Thêm Button vào FlowLayoutPanel
                fplThucDon.Controls.Add(btnItem);
            }
        }


        private void LoadMenuItemsNotFoodDrinkCigarettesOrAlcohol()
        {
            // Thư mục lưu trữ hình ảnh
            string imageFolder = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "HinhAnh", "ThucDon");

            // Xóa tất cả control cũ nếu có
            fplThucDon.Controls.Clear();

            // Lấy danh sách món ăn từ cơ sở dữ liệu
            List<MenuItem> menuItems = GetMenuItemsFromDatabase();

            // Lọc ra các món ăn không thuộc các loại "Đồ Ăn", "Đồ Uống", "Thuốc Lá", "Rượu Bia"
            menuItems = menuItems.Where(item =>
                item.LoaiMon != "Đồ Ăn" &&
                item.LoaiMon != "Đồ Uống" &&
                item.LoaiMon != "Thuốc Lá" &&
                item.LoaiMon != "Rượu Bia").ToList();

            foreach (var item in menuItems)
            {
                // Tạo Button hoặc Panel cho mỗi món ăn
                Button btnItem = new Button
                {
                    Width = 160,
                    Height = 200,
                    Text = $"{item.TenMon}\n{item.Gia:N0} VND",
                    Tag = item,
                    TextAlign = ContentAlignment.BottomCenter,
                    ImageAlign = ContentAlignment.TopCenter,
                    Font = new Font("Arial", 10, FontStyle.Bold),
                    ForeColor = Color.Black,
                    BackColor = Color.FromArgb(217, 186, 166)
                };

                // Tạo đường dẫn đầy đủ cho hình ảnh
                string imagePath = Path.Combine(imageFolder, item.HinhAnh);

                // Thêm hình ảnh nếu có
                if (!string.IsNullOrEmpty(item.HinhAnh) && File.Exists(imagePath))
                {
                    using (Image img = Image.FromFile(imagePath))
                    {
                        btnItem.Image = new Bitmap(img, new Size(160, 150)); // Điều chỉnh kích thước hình ảnh
                    }
                }

                // Thêm sự kiện Click cho Button
                btnItem.Click += (sender, e) =>
                {
                    var selectedItem = (MenuItem)((Button)sender).Tag;
                    ChonMon(selectedItem.TenMon, selectedItem.Gia); // Gọi phương thức chọn món khi người dùng nhấp vào
                };

                // Thêm Button vào FlowLayoutPanel
                fplThucDon.Controls.Add(btnItem);
            }
        }

        private void btnCoffe_Click(object sender, EventArgs e)
        {
            if (currentMenuType == "Đồ Ăn") // Nếu đã chọn đồ ăn rồi
            {
                ShowAllMenuItems();
                currentMenuType = string.Empty; // Đặt lại trạng thái
            }
            else
            {
                // Hiển thị món đồ ăn
                LoadMenuItems("Đồ Ăn");
                pnlBanh.Visible = false;
                pnlCoffe.Visible = true;
                pnlKhac.Visible = false;
                pnlTra.Visible = false;
                currentMenuType = "Đồ Ăn"; // Cập nhật loại menu hiện tại
            }
        }

        private void btnTra_Click(object sender, EventArgs e)
        {
            if (currentMenuType == "Đồ Uống") // Nếu đã chọn đồ uống rồi
            {
                ShowAllMenuItems();
                currentMenuType = string.Empty; // Đặt lại trạng thái
            }
            else
            {
                // Hiển thị món đồ uống
                LoadMenuItems("Đồ Uống");
                pnlBanh.Visible = false;
                pnlCoffe.Visible = false;
                pnlKhac.Visible = false;
                pnlTra.Visible = true;
                currentMenuType = "Đồ Uống"; // Cập nhật loại menu hiện tại
            }
        }

        private void btnBanh_Click(object sender, EventArgs e)
        {
            if (currentMenuType == "Thuốc Lá") // Nếu đã chọn thuốc lá rồi
            {
                ShowAllMenuItems();
                currentMenuType = string.Empty; // Đặt lại trạng thái
            }
            else
            {
                // Hiển thị món thuốc lá
                LoadMenuItems("Thuốc Lá");
                pnlBanh.Visible = true;
                pnlCoffe.Visible = false;
                pnlKhac.Visible = false;
                pnlTra.Visible = false;
                currentMenuType = "Thuốc Lá"; // Cập nhật loại menu hiện tại
            }
        }

        private void btnKhac_Click(object sender, EventArgs e)
        {
            if (currentMenuType == "Rượu Bia") // Nếu đã chọn rượu bia rồi
            {
                ShowAllMenuItems();
                currentMenuType = string.Empty; // Đặt lại trạng thái
            }
            else
            {
                // Hiển thị món rượu bia
                LoadMenuItems("Rượu Bia");
                pnlBanh.Visible = false;
                pnlCoffe.Visible = false;
                pnlKhac.Visible = true;
                pnlTra.Visible = false;
                currentMenuType = "Rượu Bia"; // Cập nhật loại menu hiện tại
            }
        }

        // Phương thức để hiển thị tất cả các món ăn
        private void ShowAllMenuItems()
        {
            // Gọi phương thức để tải tất cả món ăn từ cơ sở dữ liệu
            LoadMenuItems();

            // Đặt tất cả các panel thành visible
            pnlBanh.Visible = true;
            pnlCoffe.Visible = true;
            pnlKhac.Visible = true;
            pnlTra.Visible = true;
        }


        private void fNhanVien_FormClosing(object sender, FormClosingEventArgs e)
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

        private void cboLoai_SelectedIndexChanged(object sender, EventArgs e)
        {
            ApplyFilters();
        }

        private void cboTrangThai_SelectedIndexChanged(object sender, EventArgs e)
        {
            ApplyFilters();
        }





        private void LoadBanToComboBox()
        {
            // Lấy danh sách bàn từ cơ sở dữ liệu
            List<Ban> allBanList = GetBanListFromDatabase();
            List<Ban> openBanList = allBanList.Where(b => b.TrangThai == "Đang Sử Dụng").ToList();

            cboBan.DataSource = openBanList;
            cboBan.DisplayMember = "TenHienThi";
            cboBan.ValueMember = "MaBan";

        }


        private void cboBan_SelectedIndexChanged(object sender, EventArgs e)
        {

            // Reset tổng tiền về 0
            lblTongTien.Text = "0 VND";

            // Xóa tất cả các món trong FlowLayoutPanel
            fplChiTietHoaDon.Controls.Clear();

            // Xóa danh sách các món đã chọn
            selectedItems.Clear();


        }
        public static Dictionary<int, DateTime> banMoThoiGian = new Dictionary<int, DateTime>();
        private Dictionary<int, fThanhToan> danhSachThanhToan = new Dictionary<int, fThanhToan>();
        public int maBanDuocChon;
        private void btnGui_Click(object sender, EventArgs e)
        {
            if (cboBan.SelectedItem is Ban selectedBan)
            {
                int soBan = selectedBan.MaBan;
                bool isBanTrong = selectedBan.TrangThai == "Trống";

                if (isBanTrong)
                {
                    if (danhSachThanhToan.ContainsKey(soBan))
                    {
                        danhSachThanhToan[soBan].Close();
                        danhSachThanhToan.Remove(soBan);
                    }
                }

                if (!danhSachThanhToan.ContainsKey(soBan) || danhSachThanhToan[soBan].IsDisposed)
                {
                    danhSachThanhToan[soBan] = new fThanhToan(maNV);
                }

                fThanhToan frmThanhToan = danhSachThanhToan[soBan];

                if (isBanTrong)
                {
                    frmThanhToan.dgvBangGia.Rows.Clear();
                }

                if (frmThanhToan.dgvBangGia.Columns.Count == 0)
                {
                    frmThanhToan.dgvBangGia.Columns.Add("TenMon", "Tên Món");
                    frmThanhToan.dgvBangGia.Columns.Add("SoLuong", "Số Lượng");
                    frmThanhToan.dgvBangGia.Columns.Add("ThanhTien", "Thành Tiền");
                }

                foreach (Panel panel in fplChiTietHoaDon.Controls)
                {
                    if (panel.Tag is SelectedItem item)
                    {
                        var (soLuongDu, soLuongConLai) = KiemTraSoLuongKho(item.TenMon, item.SoLuong);

                        if (!soLuongDu)
                        {
                            MessageBox.Show($"Số lượng món {item.TenMon} không đủ trong kho. (Còn {soLuongConLai})");
                            return;
                        }

                        bool daTonTai = false;

                        foreach (DataGridViewRow row in frmThanhToan.dgvBangGia.Rows)
                        {
                            if (row.Cells["TenMon"].Value != null && row.Cells["TenMon"].Value.ToString() == item.TenMon)
                            {
                                int soLuongCu = Convert.ToInt32(row.Cells["SoLuong"].Value);
                                row.Cells["SoLuong"].Value = soLuongCu + item.SoLuong;

                                decimal giaMoi = (soLuongCu + item.SoLuong) * (item.ThanhTien / item.SoLuong);
                                row.Cells["ThanhTien"].Value = giaMoi;

                                daTonTai = true;
                                break;
                            }
                        }

                        if (!daTonTai)
                        {
                            frmThanhToan.dgvBangGia.Rows.Add(item.TenMon, item.SoLuong, item.ThanhTien);
                        }
                    }
                }

                frmThanhToan.lbTenBan.Text = $"Bàn {soBan}";
                frmThanhToan.MaBan = soBan;
                frmThanhToan.cbKhuyenMai.SelectedIndex = -1;
                frmThanhToan.cbThongTinKH.SelectedIndex = -1;
                frmThanhToan.cbThongTinKH.Text = "Khách Vãng Lai ()";
                frmThanhToan.cbKhuyenMai.Text = "Không áp dụng (0%)";

                frmThanhToan.TinhTongGio(soBan);
                frmThanhToan.TinhTienBan(soBan);
                frmThanhToan.TinhTongCong();
                frmThanhToan.TinhTongTienCuoi();

                frmThanhToan.Show();
            }
            else
            {
                MessageBox.Show("Vui lòng chọn bàn hợp lệ.");
            }
        }



        // Phương thức kiểm tra số lượng trong kho
        private (bool isEnough, int soLuongConLai) KiemTraSoLuongKho(string tenMon, int soLuongCanThem)
        {
            using (SqlConnection connection = new SqlConnection(dataProvider.constr))
            {
                connection.Open();

                // Truy vấn số lượng tồn kho từ bảng KhoHang dựa trên TenMon
                string query = @"
            SELECT kh.SoLuong 
            FROM KhoHang kh
            INNER JOIN ThucDon td ON kh.MaSP = td.MaMon
            WHERE td.TenMon = @TenMon";

                using (SqlCommand cmd = new SqlCommand(query, connection))
                {
                    cmd.Parameters.AddWithValue("@TenMon", tenMon); // Truyền tên món

                    object result = cmd.ExecuteScalar();

                    if (result != null && int.TryParse(result.ToString(), out int soLuongKho))
                    {
                        // Kiểm tra xem số lượng yêu cầu có lớn hơn số lượng tồn kho không
                        return (soLuongCanThem <= soLuongKho, soLuongKho);
                    }
                    else
                    {
                        MessageBox.Show($"Không tìm thấy món với tên '{tenMon}' trong kho.");
                        return (false, 0);  // Không tìm thấy món
                    }
                }
            }
        }







        private void xemThôngTinToolStripMenuItem_Click(object sender, EventArgs e)
        {
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

        private void đổiMậtKhẩuToolStripMenuItem_Click(object sender, EventArgs e)
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

        private void btnUser_ThucDon_Click(object sender, EventArgs e)
        {
            contextMenuStrip1.Show(btnUser_ThucDon, new Point(btnUser_ThucDon.Width - contextMenuStrip1.Width, btnUser_ThucDon.Height));
        }

        private void btnUser_Ban_Click(object sender, EventArgs e)
        {
            contextMenuStrip1.Show(btnUser_Ban, new Point(btnUser_Ban.Width - contextMenuStrip1.Width, btnUser_Ban.Height));
        }

        private void btnUser_KH_Click(object sender, EventArgs e)
        {
            contextMenuStrip1.Show(btnUser_KH, new Point(btnUser_KH.Width - contextMenuStrip1.Width, btnUser_KH.Height));
        }

        private void btnUser_LSC_Click(object sender, EventArgs e)
        {
            contextMenuStrip1.Show(btnUser_LSC, new Point(btnUser_LSC.Width - contextMenuStrip1.Width, btnUser_LSC.Height));
        }

        private void btnThoat_ThucDon_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void btnThoat_Ban_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void btnThoat_KH_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void btnThoat_LSC_Click(object sender, EventArgs e)
        {
            Close();
        }
        private void FilterBySearchTerm()
        {
            string searchTerm = txtTimKiem_LSC.Text.ToLower();
            List<HoaDon> allHoaDon = GetHoaDonListFromDatabase();

            var filteredList = allHoaDon.Where(item =>
                item.SoHoaDon.ToString().Contains(searchTerm) ||
                item.MaKH.ToString().Contains(searchTerm) ||
                item.NgayLapHoaDon.ToString("dd/MM/yyyy").Contains(searchTerm) ||
                item.HinhThucThanhToan.ToLower().Contains(searchTerm)
            ).ToList();

            dgvLichSuCa.DataSource = filteredList;
        }

        private void FilterByDate()
        {
            DateTime startDate = dtpStart_LSC.Value.Date;
            DateTime endDate = dtpEndC_LSC.Value.Date;

            List<HoaDon> allHoaDon = GetHoaDonListFromDatabase();

            var filteredList = allHoaDon.Where(item =>
                item.NgayLapHoaDon >= startDate && item.NgayLapHoaDon <= endDate
            ).ToList();

            dgvLichSuCa.DataSource = filteredList;
        }

        private void dateTimePicker2_ValueChanged(object sender, EventArgs e)
        {
            FilterByDate();
        }

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {
            FilterByDate();
        }

        private void txtTimKiem_LSC_TextChanged(object sender, EventArgs e)
        {
            FilterBySearchTerm();
        }

        private void txtTimKiem_LSC_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtTimKiem_LSC.Text))  // Nếu ô tìm kiếm rỗng
            {
                txtTimKiem_LSC.Text = "Tìm kiếm...";  // Đặt lại text mặc định
                txtTimKiem_LSC.ForeColor = Color.Gray;  // Đổi màu chữ thành màu xám
            }
        }

        private void txtTimKiem_LSC_Enter(object sender, EventArgs e)
        {
            if (txtTimKiem_LSC.Text == "Tìm kiếm..." || string.IsNullOrEmpty(txtTimKiem_LSC.Text))  // Nếu là text mặc định hoặc rỗng
            {
                txtTimKiem_LSC.Text = "";  // Xóa text đi
                txtTimKiem_LSC.ForeColor = Color.Black;  // Đổi màu chữ thành đen
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            using (var workbook = new XLWorkbook())
            {
                var worksheet = workbook.Worksheets.Add("LichSu");

                // Thêm tiêu đề cho các cột (giả sử bạn có DataGridView là dgvLichSu)
                for (int i = 0; i < dgvLichSuCa.Columns.Count; i++)
                {
                    worksheet.Cell(1, i + 1).Value = dgvLichSuCa.Columns[i].HeaderText;
                }

                // Thêm dữ liệu vào các ô
                for (int i = 0; i < dgvLichSuCa.Rows.Count; i++)
                {
                    for (int j = 0; j < dgvLichSuCa.Columns.Count; j++)
                    {
                        // Sử dụng ToString() để tránh lỗi chuyển đổi kiểu
                        worksheet.Cell(i + 2, j + 1).Value = dgvLichSuCa.Rows[i].Cells[j].Value?.ToString();
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

        private void txtTimKiem_KH_TextChanged(object sender, EventArgs e)
        {
            // Lấy từ khóa tìm kiếm và chuyển về chữ thường
            string searchTerm = txtTimKiem_KH.Text.ToLower();

            // Lấy danh sách tất cả khách hàng từ database
            List<KhachHang> allKhachHang = GetKhachHangListFromDatabase();

            // Lọc danh sách khách hàng dựa trên từ khóa tìm kiếm
            var filteredList = allKhachHang.Where(item =>
                item.MaKH.ToString().Contains(searchTerm) || // Tìm theo mã khách hàng
                item.HoTen.ToLower().Contains(searchTerm) || // Tìm theo họ tên
                (item.SDT != null && item.SDT.Contains(searchTerm)) || // Tìm theo số điện thoại
                (item.Email != null && item.Email.ToLower().Contains(searchTerm)) || // Tìm theo email
                (item.NgaySinh.HasValue && item.NgaySinh.Value.ToString("dd/MM/yyyy").Contains(searchTerm)) // Tìm theo ngày sinh
            ).ToList();

            // Hiển thị danh sách kết quả vào DataGridView
            dgvKhachHang.DataSource = filteredList;
        }

        private void txtTimKiem_KH_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtTimKiem_KH.Text))  // Nếu ô tìm kiếm rỗng
            {
                txtTimKiem_KH.Text = "Tìm kiếm...";  // Đặt lại text mặc định
                txtTimKiem_KH.ForeColor = Color.Gray;  // Đổi màu chữ thành màu xám
            }
        }

        private void txtTimKiem_KH_Enter(object sender, EventArgs e)
        {
            if (txtTimKiem_KH.Text == "Tìm kiếm..." || string.IsNullOrEmpty(txtTimKiem_KH.Text))  // Nếu là text mặc định hoặc rỗng
            {
                txtTimKiem_KH.Text = "";  // Xóa text đi
                txtTimKiem_KH.ForeColor = Color.Black;  // Đổi màu chữ thành đen
            }
        }

        private void txtTimKiem_ThucDon_TextChanged(object sender, EventArgs e)
        {
            // Lấy từ khóa tìm kiếm từ TextBox và chuyển về chữ thường
            string searchTerm = txtTimKiem_ThucDon.Text.ToLower();

            // Gọi lại hàm LoadMenuItems() và truyền từ khóa tìm kiếm
            LoadMenuItemsWithSearch(searchTerm);
        }
        private void LoadMenuItemsWithSearch(string searchTerm = "", string Nhom = null)
        {
            // Thư mục lưu trữ hình ảnh
            string imageFolder = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "HinhAnh", "ThucDon");

            // Xóa tất cả control cũ nếu có
            fplThucDon.Controls.Clear();

            // Lấy danh sách món ăn từ cơ sở dữ liệu
            List<MenuItem> menuItems = GetMenuItemsFromDatabase();

            // Nếu có loại món ăn để lọc, thực hiện lọc theo NhomThucDon
            if (!string.IsNullOrEmpty(Nhom))
            {
                menuItems = menuItems.Where(item => item.NhomThucDon == Nhom).ToList();
            }

            // Thực hiện lọc món ăn dựa trên từ khóa tìm kiếm (searchTerm)
            if (!string.IsNullOrEmpty(searchTerm))
            {
                menuItems = menuItems.Where(item =>
                    item.TenMon.ToLower().Contains(searchTerm) // Lọc theo tên món ăn
                ).ToList();
            }

            // Duyệt qua danh sách món ăn để hiển thị
            foreach (var item in menuItems)
            {
                // Tạo Button cho mỗi món ăn
                Button btnItem = new Button
                {
                    Width = 180,
                    Height = 220,
                    Text = $"{item.TenMon}\n{item.Gia:N0} VND",
                    Tag = item,
                    TextAlign = ContentAlignment.BottomCenter,
                    ImageAlign = ContentAlignment.TopCenter,
                    Font = new Font("Arial", 10, FontStyle.Bold),
                    ForeColor = Color.Black,
                    BackColor = Color.FromArgb(217, 186, 166)
                };

                // Tạo đường dẫn đầy đủ cho hình ảnh
                string imagePath = Path.Combine(imageFolder, item.HinhAnh);

                // Thêm hình ảnh nếu có
                if (!string.IsNullOrEmpty(item.HinhAnh) && File.Exists(imagePath))
                {
                    using (Image img = Image.FromFile(imagePath))
                    {
                        btnItem.Image = new Bitmap(img, new Size(180, 170)); // Điều chỉnh kích thước hình ảnh
                    }
                }

                // Thêm sự kiện Click cho Button
                btnItem.Click += (sender, e) =>
                {
                    var selectedItem = (MenuItem)((Button)sender).Tag;
                    ChonMon(selectedItem.TenMon, selectedItem.Gia); // Gọi phương thức chọn món khi người dùng nhấp vào
                };

                // Thêm Button vào FlowLayoutPanel
                fplThucDon.Controls.Add(btnItem);
            }
        }

        private void txtTimKiem_ThucDon_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtTimKiem_ThucDon.Text))  // Nếu ô tìm kiếm rỗng
            {
                txtTimKiem_ThucDon.Text = "Tìm kiếm...";  // Đặt lại text mặc định
                txtTimKiem_ThucDon.ForeColor = Color.Gray;  // Đổi màu chữ thành màu xám
            }
        }

        private void txtTimKiem_ThucDon_Enter(object sender, EventArgs e)
        {
            if (txtTimKiem_ThucDon.Text == "Tìm kiếm..." || string.IsNullOrEmpty(txtTimKiem_ThucDon.Text))  // Nếu là text mặc định hoặc rỗng
            {
                txtTimKiem_ThucDon.Text = "";  // Xóa text đi
                txtTimKiem_ThucDon.ForeColor = Color.Black;  // Đổi màu chữ thành đen
            }
        }

        private void btnHuy_Click(object sender, EventArgs e)
        {
            // Duyệt ngược qua tất cả các Panel trong FlowLayoutPanel
            for (int i = fplChiTietHoaDon.Controls.Count - 1; i >= 0; i--)
            {
                if (fplChiTietHoaDon.Controls[i] is Panel panel)
                {
                    // Tìm Button Xóa trong Panel
                    foreach (Control innerControl in panel.Controls)
                    {
                        if (innerControl is Button btnXoa)
                        {
                            // Kích hoạt sự kiện Click của Button Xóa
                            btnXoa.PerformClick();
                            break; // Thoát khỏi vòng lặp sau khi xóa
                        }
                    }
                }
            }

            // Cập nhật tổng tiền sau khi xóa hết các món
            CapNhatTongTien();
        }

        private void btnThem_KH_Click(object sender, EventArgs e)
        {
            fThemKhachHang formThemKH = new fThemKhachHang();
            formThemKH.ShowDialog();
            LoadKhachHangData();
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
    }
}
