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
using System.Drawing;
using System.Drawing.Printing;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.TrackBar;
using QRCoder;

namespace QL_Bida
{
    public partial class fThanhToan : Form
    {
        DataProvider dataProvider = new DataProvider();
        public int MaBan { get; set; }
        private int maNV;

        private PrintDocument printDocument = new PrintDocument();
        private List<string> danhSachMon;
        private List<int> soLuongMon;
        private List<decimal> giaMon;
        private int maBan;
        private string tenKhachHang, phuongThucThanhToan, khuyenMai;
        private decimal tongTien;

        public fThanhToan(int maNV)
        {
            InitializeComponent();
            printDocument.PrintPage += new PrintPageEventHandler(PrintDocument_PrintPage);
            this.maNV = maNV;
            this.StartPosition = FormStartPosition.CenterScreen;
            dgvBangGia.ColumnHeadersDefaultCellStyle.BackColor = Color.White;
            dgvBangGia.ColumnHeadersDefaultCellStyle.ForeColor = Color.DarkRed;
            dgvBangGia.RowHeadersVisible = false;
            dgvBangGia.ColumnHeadersHeight = 40;
            dgvBangGia.EnableHeadersVisualStyles = false;
            dgvBangGia.AdvancedColumnHeadersBorderStyle.All = DataGridViewAdvancedCellBorderStyle.None;
            dgvBangGia.ColumnHeadersDefaultCellStyle.Font = new Font(dgvBangGia.Font.FontFamily, dgvBangGia.Font.Size + 2, FontStyle.Bold);
            dgvBangGia.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

            dgvBangGia.Columns[0].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dgvBangGia.Columns[1].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgvBangGia.Columns[2].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight;

            dgvBangGia.Columns[0].Width = 100;
            dgvBangGia.Columns[1].Width = 150;
            dgvBangGia.Columns[2].Width = 150;

            dgvBangGia.Columns[0].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;  // Căn trái cột 1
            dgvBangGia.Columns[1].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter; // Căn giữa cột 2
            dgvBangGia.Columns[2].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;  // Căn phải cột 3

            dgvBangGia.Columns[1].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgvBangGia.Columns[1].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgvBangGia.DefaultCellStyle.Font = new Font(dgvBangGia.Font.FontFamily, dgvBangGia.Font.Size + 2);
        }
        private Dictionary<int, fThanhToan> danhSachThanhToan = new Dictionary<int, fThanhToan>();
        //private void btnXuatHoaDon_Click(object sender, EventArgs e)
        //{
        //    try
        //    {
        //        // Lấy thông tin từ form
        //        maBan = this.MaBan;
        //        tenKhachHang = cbThongTinKH.Text;
        //        tongTien = decimal.Parse(lbThanhTien.Text);
        //        phuongThucThanhToan = cbPhuongThucTT.Text;
        //        khuyenMai = cbKhuyenMai.SelectedItem != null ? cbKhuyenMai.Text : "Không có";

        //        // Lấy danh sách món từ DataGridView
        //        danhSachMon = new List<string>();
        //        soLuongMon = new List<int>();
        //        giaMon = new List<decimal>();

        //        foreach (DataGridViewRow row in dgvBangGia.Rows)
        //        {
        //            if (row.Cells[0].Value != null && row.Cells[1].Value != null)
        //            {
        //                danhSachMon.Add(row.Cells[0].Value.ToString());
        //                soLuongMon.Add(Convert.ToInt32(row.Cells[1].Value));
        //                giaMon.Add(Convert.ToDecimal(row.Cells[2].Value));
        //            }
        //        }

        //        // Thiết lập PrintDialog
        //        PrintDialog printDialog = new PrintDialog();
        //        printDialog.Document = printDocument;

        //        // Chọn "Microsoft Print to PDF" để xuất ra file PDF
        //        printDocument.PrinterSettings.PrinterName = "Microsoft Print to PDF";

        //        if (printDialog.ShowDialog() == DialogResult.OK)
        //        {
        //            printDocument.Print();
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        MessageBox.Show($"Lỗi khi xuất hóa đơn: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
        //    }
        //}
        //private void PrintDocument_PrintPage(object sender, PrintPageEventArgs e)
        //{
        //    Graphics g = e.Graphics;
        //    float yPos = 10;
        //    int leftMargin = e.MarginBounds.Left;
        //    int rightMargin = e.MarginBounds.Right;
        //    Font titleFont = new Font("Arial", 18, FontStyle.Bold);
        //    Font headerFont = new Font("Arial", 12, FontStyle.Bold);
        //    Font normalFont = new Font("Arial", 12);
        //    Brush brush = Brushes.Black;

        //    // Tiêu đề hóa đơn
        //    g.DrawString("HÓA ĐƠN THANH TOÁN", titleFont, brush, (rightMargin - leftMargin) / 2 + leftMargin, yPos, new StringFormat { Alignment = StringAlignment.Center });
        //    yPos += 30;

        //    // Thông tin chung
        //    g.DrawString($"Bàn: {maBan}", normalFont, brush, leftMargin, yPos);
        //    yPos += 25;
        //    g.DrawString($"Khách hàng: {tenKhachHang}", normalFont, brush, leftMargin, yPos);
        //    yPos += 25;
        //    g.DrawString($"Thời gian sử dụng: {lbTongGio.Text}", normalFont, brush, leftMargin, yPos);  // Thêm thời gian sử dụng
        //    yPos += 25;
        //    g.DrawString($"Tiền bàn: {decimal.Parse(lbTienBan.Text):N0} VND", normalFont, brush, leftMargin, yPos);  // Thêm tiền bàn
        //    yPos += 25;

        //    // In khuyến mãi từ lbTongCong
        //    g.DrawString($"Khuyến mãi: {khuyenMai}", normalFont, brush, leftMargin, yPos);
        //    yPos += 25;

        //    // In khuyến mãi thành viên từ lbThanhVien
        //    g.DrawString($"Khuyến mãi thành viên: {lbThanhVien.Text}", normalFont, brush, leftMargin, yPos); // Thêm dòng khuyến mãi thành viên
        //    yPos += 25;

        //    g.DrawString($"Phương thức thanh toán: {phuongThucThanhToan}", normalFont, brush, leftMargin, yPos);
        //    yPos += 25;
        //    g.DrawString($"Ngày: {DateTime.Now:dd/MM/yyyy HH:mm}", normalFont, brush, leftMargin, yPos);
        //    yPos += 30;

        //    // Dòng phân cách
        //    g.DrawLine(new Pen(brush), leftMargin, yPos, rightMargin, yPos);
        //    yPos += 10;

        //    // Tiêu đề các cột
        //    int x = 100;
        //    g.DrawString("STT", headerFont, brush, leftMargin, yPos);
        //    g.DrawString("MÓN ĂN", headerFont, brush, leftMargin + 40+x, yPos);
        //    g.DrawString("SL", headerFont, brush, leftMargin + 220 + x +40, yPos, new StringFormat { Alignment = StringAlignment.Far });
        //    g.DrawString("GIÁ", headerFont, brush, leftMargin + 300 + x +75, yPos, new StringFormat { Alignment = StringAlignment.Far });
        //    g.DrawString("TỔNG", headerFont, brush, leftMargin + 400 + x+125, yPos, new StringFormat { Alignment = StringAlignment.Far });
        //    yPos += 40;

        //    // Dòng phân cách
        //    g.DrawLine(new Pen(brush), leftMargin, yPos, rightMargin, yPos);
        //    yPos += 10;

        //    // Chi tiết món ăn
        //    for (int i = 0; i < danhSachMon.Count; i++)
        //    {
        //        // STT và Tên món ăn
        //        g.DrawString((i + 1).ToString(), normalFont, brush, leftMargin, yPos);
        //        g.DrawString(danhSachMon[i], normalFont, brush, leftMargin + 40 + x, yPos);

        //        // Số lượng (SL), giá (GIÁ), và tổng (TỔNG) với căn phải
        //        g.DrawString(soLuongMon[i].ToString(), normalFont, brush, leftMargin + 220 + x+40, yPos, new StringFormat { Alignment = StringAlignment.Far });
        //        g.DrawString(giaMon[i].ToString("N0"), normalFont, brush, leftMargin + 300 + x+75, yPos, new StringFormat { Alignment = StringAlignment.Far });
        //        g.DrawString((soLuongMon[i] * giaMon[i]).ToString("N0"), normalFont, brush, leftMargin + 400 + x+125, yPos, new StringFormat { Alignment = StringAlignment.Far });

        //        yPos += 20;
        //    }

        //    // Dòng phân cách
        //    yPos += 10;
        //    g.DrawLine(new Pen(brush), leftMargin, yPos, rightMargin, yPos);
        //    yPos += 10;

        //    // Tổng tiền trước khuyến mãi
            

        //    decimal tongTruocKhuyenMai = decimal.Parse(lbTongCong.Text);
        //    StringFormat rightAlignFormat = new StringFormat { Alignment = StringAlignment.Far };
        //    g.DrawString($"Tổng trước khuyến mãi: {tongTruocKhuyenMai:N0} VND", headerFont, brush, rightMargin, yPos, rightAlignFormat);
        //    yPos += 25;

        //    // Số tiền giảm (bỏ đậm và cách dòng thêm 1 khoảng)
        //    decimal soTienGiam = tongTruocKhuyenMai - tongTien;
        //    g.DrawString($"Số tiền giảm: -{soTienGiam:N0} VND", normalFont, brush, rightMargin, yPos, rightAlignFormat);  // Không in đậm
        //    yPos += 25;  // Khoảng cách giữa Số tiền giảm và Tổng tiền

        //    // Tổng tiền sau khuyến mãi
        //    g.DrawString($"Tổng tiền: {tongTien:N0} VND", headerFont, brush, rightMargin, yPos, rightAlignFormat);
        //    yPos += 35;

        //    // Căn giữa cho dòng "Cảm ơn"
        //    StringFormat centerAlignFormat = new StringFormat { Alignment = StringAlignment.Center };
        //    g.DrawString("Cảm ơn quý khách đã sử dụng dịch vụ!", normalFont, brush, (rightMargin + leftMargin) / 2, yPos, centerAlignFormat);
        //    yPos += 40;

        //}



        private void btnThanhToan_Click(object sender, EventArgs e)
        {
            // Lấy mã bàn từ lbTenBan (nếu có)
            int maBanTemp;
            if (lbTenBan.Text.StartsWith("Bàn ") && int.TryParse(lbTenBan.Text.Replace("Bàn ", ""), out maBanTemp))
            {
                MaBan = maBanTemp; // Gán lại giá trị cho thuộc tính MaBan
            }
            else
            {
                MessageBox.Show("Vui lòng chọn bàn hợp lệ.");
                return;
            }

            // Xác nhận thanh toán
            DialogResult result = MessageBox.Show("Bạn có chắc chắn muốn thanh toán?", "Xác nhận", MessageBoxButtons.YesNo);
            if (result == DialogResult.Yes)
            {
                try
                {
                    // Kiểm tra nếu người dùng đã chọn phương thức thanh toán
                    if (cbPhuongThucTT.SelectedIndex == -1)
                    {
                        MessageBox.Show("Vui lòng chọn phương thức thanh toán.");
                        return;
                    }

                    // Lấy mã khách hàng (có thể null)
                    int? maKH = cbThongTinKH.SelectedValue != null && Convert.ToInt32(cbThongTinKH.SelectedValue) > 0
                                 ? (int?)Convert.ToInt32(cbThongTinKH.SelectedValue)
                                 : null;

                    int maNV = this.maNV;  // Lấy maNV từ form đăng nhập

                    // Kiểm tra và chuyển đổi SoGioChoi từ lbTongGio.Text
                    decimal soGioChoi = 0;
                    string gioText = lbTongGio.Text.Replace(" giờ", "").Replace(" phút", "").Trim();
                    string[] timeParts = gioText.Split(' ');

                    if (timeParts.Length == 2)
                    {
                        int gio = 0;
                        int phut = 0;

                        if (!int.TryParse(timeParts[0], out gio) || !int.TryParse(timeParts[1], out phut))
                        {
                            MessageBox.Show("Thời gian không hợp lệ.");
                            return;
                        }

                        soGioChoi = gio + phut / 60.0m;
                    }
                    else
                    {
                        MessageBox.Show("Tổng giờ chơi không hợp lệ.");
                        return;
                    }

                    decimal thanhTien;
                    if (!decimal.TryParse(lbThanhTien.Text, out thanhTien))
                    {
                        MessageBox.Show("Tổng tiền không hợp lệ.");
                        return;
                    }

                    DateTime ngayLapHoaDon = DateTime.Now;
                    string hinhThucThanhToan = cbPhuongThucTT.Text;

                    // Lấy mã khuyến mãi (có thể null)
                    int? maKM = cbKhuyenMai.SelectedItem is KhuyenMai selectedKhuyenMai && selectedKhuyenMai.MaKM > 0
                                 ? (int?)selectedKhuyenMai.MaKM
                                 : null;

                    using (SqlConnection connection = new SqlConnection(dataProvider.constr))
                    {
                        connection.Open();

                        // Thêm hóa đơn mới
                        string insertQuery = @"INSERT INTO HoaDon (MaKH, MaBan, MaNV, SoGioChoi, ThanhTien, NgayLapHoaDon, HinhThucThanhToan, MaKM) 
        VALUES (@MaKH, @MaBan, @MaNV, @SoGioChoi, @ThanhTien, @NgayLapHoaDon, @HinhThucThanhToan, @MaKM)";

                        using (SqlCommand insertCommand = new SqlCommand(insertQuery, connection))
                        {
                            if (maKH.HasValue)
                                insertCommand.Parameters.AddWithValue("@MaKH", maKH.Value);
                            else
                                insertCommand.Parameters.AddWithValue("@MaKH", DBNull.Value);

                            insertCommand.Parameters.AddWithValue("@MaBan", MaBan);
                            insertCommand.Parameters.AddWithValue("@MaNV", maNV);
                            insertCommand.Parameters.AddWithValue("@SoGioChoi", soGioChoi);
                            insertCommand.Parameters.AddWithValue("@ThanhTien", thanhTien);
                            insertCommand.Parameters.AddWithValue("@NgayLapHoaDon", ngayLapHoaDon);
                            insertCommand.Parameters.AddWithValue("@HinhThucThanhToan", hinhThucThanhToan);

                            if (maKM.HasValue)
                                insertCommand.Parameters.AddWithValue("@MaKM", maKM.Value);
                            else
                                insertCommand.Parameters.AddWithValue("@MaKM", DBNull.Value);

                            insertCommand.ExecuteNonQuery();
                            MessageBox.Show("Lưu hóa đơn thành công!");
                        }
                        if (maKH.HasValue)
                        {
                            int diemTichLuy = (int)(thanhTien / 1000);  // Tính điểm tích luỹ: 1000 đồng = 1 điểm

                            string updateDiemQuery = "UPDATE KhachHang SET DiemTichLuy = DiemTichLuy + @DiemTichLuy WHERE MaKH = @MaKH";
                            using (SqlCommand updateDiemCommand = new SqlCommand(updateDiemQuery, connection))
                            {
                                updateDiemCommand.Parameters.AddWithValue("@DiemTichLuy", diemTichLuy);
                                updateDiemCommand.Parameters.AddWithValue("@MaKH", maKH.Value);
                                updateDiemCommand.ExecuteNonQuery();
                            }
                        }

                        // Cập nhật trạng thái bàn
                        string updateQuery = "UPDATE BanBilliards SET TrangThai = N'Trống' WHERE MaBan = @MaBan";
                        using (SqlCommand updateCommand = new SqlCommand(updateQuery, connection))
                        {
                            updateCommand.Parameters.AddWithValue("@MaBan", MaBan);
                            updateCommand.ExecuteNonQuery();
                        }

                        // Lấy SoHoaDon mới nhất của bàn này
                        string selectHoaDonQuery = "SELECT MAX(SoHoaDon) FROM HoaDon WHERE MaBan = @MaBan";
                        int soHoaDon;
                        using (SqlCommand selectHoaDonCommand = new SqlCommand(selectHoaDonQuery, connection))
                        {
                            selectHoaDonCommand.Parameters.AddWithValue("@MaBan", MaBan);
                            object resultObj = selectHoaDonCommand.ExecuteScalar();

                            // Kiểm tra nếu kết quả trả về là null
                            if (resultObj == DBNull.Value || resultObj == null)
                            {
                                MessageBox.Show("Không tìm thấy hóa đơn cho bàn này.");
                                return;
                            }

                            soHoaDon = Convert.ToInt32(resultObj);
                           
                        }

                        foreach (DataGridViewRow row in dgvBangGia.Rows)
                        {
                            // Lấy tên món và loại bỏ khoảng trắng
                            string tenMon = row.Cells[0].Value?.ToString().Trim();

                            // Kiểm tra tên món nếu rỗng, bỏ qua dòng này (không cần hiển thị thông báo lỗi)
                            if (string.IsNullOrEmpty(tenMon))
                            {
                                continue;
                            }

                            int soLuong;
                            if (row.Cells[1].Value == null || !int.TryParse(row.Cells[1].Value.ToString(), out soLuong))
                            {
                                MessageBox.Show($"Số lượng không hợp lệ cho món: {tenMon}");
                                continue;
                            }

                            // Truy vấn MaMon và Gia từ TênMon
                            string selectMonQuery = "SELECT MaMon, Gia FROM ThucDon WHERE TenMon = @TenMon";
                            int maMon = 0;
                            decimal giaMon = 0;

                            using (SqlCommand selectMonCommand = new SqlCommand(selectMonQuery, connection))
                            {
                                selectMonCommand.Parameters.AddWithValue("@TenMon", tenMon);
                                using (SqlDataReader reader = selectMonCommand.ExecuteReader())
                                {
                                    if (reader.Read())
                                    {
                                        maMon = reader.GetInt32(0);  // Lấy MaMon
                                        giaMon = reader.GetDecimal(1);  // Lấy Gia
                                    }
                                    else
                                    {
                                        MessageBox.Show($"Không tìm thấy món: {tenMon}");
                                        continue;
                                    }
                                }
                            }
                            string updateKhoQuery = "UPDATE KhoHang SET SoLuong = SoLuong - @SoLuong WHERE MaSP = @MaMon";
                            using (SqlCommand updateKhoCommand = new SqlCommand(updateKhoQuery, connection))
                            {
                                updateKhoCommand.Parameters.AddWithValue("@SoLuong", soLuong);
                                updateKhoCommand.Parameters.AddWithValue("@MaMon", maMon);
                                updateKhoCommand.ExecuteNonQuery();
                            }
                            // Thêm vào ChiTietHoaDon với giá trị Gia
                            string insertDetailQuery = @"INSERT INTO ChiTietHoaDon (SoHoaDon, MaMon, SoLuong, Gia) 
                                VALUES (@SoHoaDon, @MaMon, @SoLuong, @Gia)";
                            using (SqlCommand insertDetailCommand = new SqlCommand(insertDetailQuery, connection))
                            {
                                insertDetailCommand.Parameters.AddWithValue("@SoHoaDon", soHoaDon);
                                insertDetailCommand.Parameters.AddWithValue("@MaMon", maMon);
                                insertDetailCommand.Parameters.AddWithValue("@SoLuong", soLuong);
                                insertDetailCommand.Parameters.AddWithValue("@Gia", giaMon);  // Thêm giá vào đây
                                insertDetailCommand.ExecuteNonQuery();
                            }


                            // Xóa thông báo lỗi "Tên món không hợp lệ" khi tên món hợp lệ
                        }


                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Lỗi: {ex.Message}");
                }

                // Reset lại thông tin sau khi thanh toán
                dgvBangGia.Rows.Clear();
                lbTienBan.Text = "0";
                lbTongGio.Text = "0 giờ 0 phút";
                lbTongCong.Text = "0";
                lbTenBan.Text = "Chưa chọn bàn";
                this.Hide();
            }
        }
        private int currentMaHoaDon;
        private void btnXuatHoaDon_Click(object sender, EventArgs e)
        {
            try
            {
                // Lấy mã hóa đơn mới từ SQL
                string query = "SELECT ISNULL(MAX(SoHoaDon), 0) + 1 FROM HoaDon";
                object result = dataProvider.ExecScalar(query); // dataProvider là đối tượng thực hiện truy vấn SQL
                if (result != null)
                {
                    currentMaHoaDon = Convert.ToInt32(result);
                }
                else
                {
                    currentMaHoaDon = 1; // Nếu không có dữ liệu, bắt đầu từ 1
                }

                // Lấy thông tin từ form
                maBan = this.MaBan;
                tenKhachHang = cbThongTinKH.Text;
                tongTien = decimal.Parse(lbThanhTien.Text);
                phuongThucThanhToan = cbPhuongThucTT.Text;
                khuyenMai = cbKhuyenMai.SelectedItem != null ? cbKhuyenMai.Text : "Không có";

                // Lấy danh sách món từ DataGridView
                danhSachMon = new List<string>();
                soLuongMon = new List<int>();
                giaMon = new List<decimal>();

                foreach (DataGridViewRow row in dgvBangGia.Rows)
                {
                    if (row.Cells[0].Value != null && row.Cells[1].Value != null)
                    {
                        danhSachMon.Add(row.Cells[0].Value.ToString());
                        soLuongMon.Add(Convert.ToInt32(row.Cells[1].Value));
                        giaMon.Add(Convert.ToDecimal(row.Cells[2].Value));
                    }
                }

                // Thiết lập PrintDialog
                PrintDialog printDialog = new PrintDialog();
                printDialog.Document = printDocument;

                // Chọn "Microsoft Print to PDF" để xuất ra file PDF
                printDocument.PrinterSettings.PrinterName = "Microsoft Print to PDF";

                if (printDialog.ShowDialog() == DialogResult.OK)
                {
                    printDocument.Print();
                    MessageBox.Show("Xuất hoá đơn thành công");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi xuất hóa đơn: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void PrintDocument_PrintPage(object sender, PrintPageEventArgs e)
        {
            Graphics g = e.Graphics;
            float yPos = 10;
            int leftMargin = e.MarginBounds.Left;
            int rightMargin = e.MarginBounds.Right;
            Font titleFont = new Font("Arial", 18, FontStyle.Bold);
            Font headerFont = new Font("Arial", 12, FontStyle.Bold);
            Font boldFont = new Font("Arial", 12, FontStyle.Bold); // Phông chữ đậm
            Font normalFont = new Font("Arial", 12);
            Brush brush = Brushes.Black;

            // In mã hóa đơn

            //Phần còn lại giữ nguyên...
            // Tiêu đề hóa đơn
            g.DrawString("HÓA ĐƠN THANH TOÁN", titleFont, brush, (rightMargin - leftMargin) / 2 + leftMargin, yPos, new StringFormat { Alignment = StringAlignment.Center });
            yPos += 30;

            // Thông tin chung
            g.DrawString($"Mã hóa đơn: {currentMaHoaDon}", normalFont, brush, leftMargin, yPos);
            yPos += 25;
            g.DrawString($"Bàn: {maBan}", normalFont, brush, leftMargin, yPos);
            yPos += 25;
            g.DrawString($"Khách hàng: {tenKhachHang}", normalFont, brush, leftMargin, yPos);
            yPos += 25;

            // Thời gian sử dụng
            string labelTime = "Thời gian sử dụng: ";
            string valueTime = lbTongGio.Text;
            g.DrawString(labelTime, normalFont, brush, leftMargin, yPos);
            g.DrawString(valueTime, boldFont, brush, leftMargin + g.MeasureString(labelTime, normalFont).Width, yPos);
            yPos += 25;

            // Tiền bàn
            string labelTienBan = "Tiền bàn: ";
            string valueTienBan = $"{decimal.Parse(lbTienBan.Text):N0} VND";
            g.DrawString(labelTienBan, normalFont, brush, leftMargin, yPos);
            g.DrawString(valueTienBan, boldFont, brush, leftMargin + g.MeasureString(labelTienBan, normalFont).Width, yPos);
            yPos += 25;

            g.DrawString($"Khuyến mãi: {khuyenMai}", normalFont, brush, leftMargin, yPos);
            yPos += 25;

            // Khuyến mãi thành viên
            string labelKhuyenMaiTV = "Khuyến mãi thành viên: ";
            string valueKhuyenMaiTV = lbThanhVien.Text;
            g.DrawString(labelKhuyenMaiTV, normalFont, brush, leftMargin, yPos);
            g.DrawString(valueKhuyenMaiTV, boldFont, brush, leftMargin + g.MeasureString(labelKhuyenMaiTV, normalFont).Width, yPos);
            yPos += 25;

            g.DrawString($"Phương thức thanh toán: {phuongThucThanhToan}", normalFont, brush, leftMargin, yPos);
            yPos += 25;
            g.DrawString($"Ngày: {DateTime.Now:dd/MM/yyyy HH:mm}", normalFont, brush, leftMargin, yPos);
            yPos += 30;

            // Dòng phân cách
            g.DrawLine(new Pen(brush), leftMargin, yPos, rightMargin, yPos);
            yPos += 10;

            // Tiêu đề các cột
            int x = 100;
            g.DrawString("STT", headerFont, brush, leftMargin, yPos);
            g.DrawString("MÓN ĂN", headerFont, brush, leftMargin + 40 + x, yPos);
            g.DrawString("SL", headerFont, brush, leftMargin + 220 + x + 40, yPos, new StringFormat { Alignment = StringAlignment.Far });
            g.DrawString("GIÁ", headerFont, brush, leftMargin + 300 + x + 75, yPos, new StringFormat { Alignment = StringAlignment.Far });
            g.DrawString("TỔNG", headerFont, brush, leftMargin + 400 + x + 125, yPos, new StringFormat { Alignment = StringAlignment.Far });
            yPos += 40;

            // Dòng phân cách
            g.DrawLine(new Pen(brush), leftMargin, yPos, rightMargin, yPos);
            yPos += 10;

            // Chi tiết món ăn
            for (int i = 0; i < danhSachMon.Count; i++)
            {
                // STT và Tên món ăn
                g.DrawString((i + 1).ToString(), normalFont, brush, leftMargin, yPos);
                g.DrawString(danhSachMon[i], normalFont, brush, leftMargin + 40 + x, yPos);

                // Số lượng (SL), giá (GIÁ), và tổng (TỔNG) với căn phải
                g.DrawString(soLuongMon[i].ToString(), normalFont, brush, leftMargin + 220 + x + 40, yPos, new StringFormat { Alignment = StringAlignment.Far });
                g.DrawString(giaMon[i].ToString("N0"), normalFont, brush, leftMargin + 300 + x + 75, yPos, new StringFormat { Alignment = StringAlignment.Far });
                g.DrawString((soLuongMon[i] * giaMon[i]).ToString("N0"), normalFont, brush, leftMargin + 400 + x + 125, yPos, new StringFormat { Alignment = StringAlignment.Far });

                yPos += 20;
            }

            // Dòng phân cách
            yPos += 10;
            g.DrawLine(new Pen(brush), leftMargin, yPos, rightMargin, yPos);
            yPos += 10;

            // Tổng tiền trước khuyến mãi
            decimal tongTruocKhuyenMai = decimal.Parse(lbTongCong.Text);
            StringFormat rightAlignFormat = new StringFormat { Alignment = StringAlignment.Far };
            g.DrawString($"Tổng trước khuyến mãi: {tongTruocKhuyenMai:N0} VND", headerFont, brush, rightMargin, yPos, rightAlignFormat);
            yPos += 25;

            // Số tiền giảm (bỏ đậm và cách dòng thêm 1 khoảng)
            decimal soTienGiam = tongTruocKhuyenMai - tongTien;
            g.DrawString($"Số tiền giảm: -{soTienGiam:N0} VND", normalFont, brush, rightMargin, yPos, rightAlignFormat);  // Không in đậm
            yPos += 25;  // Khoảng cách giữa Số tiền giảm và Tổng tiền

            // Tổng tiền sau khuyến mãi
            g.DrawString($"Tổng tiền: {tongTien:N0} VND", headerFont, brush, rightMargin, yPos, rightAlignFormat);
            yPos += 35;

            // In mã QR tại vị trí thích hợp
            string qrImagePath = Path.Combine(Application.StartupPath, "HinhAnh", "qr.png");
            // Đường dẫn tới ảnh QR
            Image qrImage = Image.FromFile(qrImagePath);
            int qrWidth = 150;  // Chiều rộng mã QR
            int qrHeight = 150;  // Chiều cao mã QR
            int qrXPos = (rightMargin + leftMargin) / 2 - qrWidth / 2;  // Căn giữa mã QR
            g.DrawImage(qrImage, qrXPos, yPos, qrWidth, qrHeight);
            yPos += qrHeight + 20;

            // Căn giữa cho dòng "Cảm ơn"
            StringFormat centerAlignFormat = new StringFormat { Alignment = StringAlignment.Center };
            g.DrawString("Cảm ơn quý khách đã sử dụng dịch vụ!", normalFont, brush, (rightMargin + leftMargin) / 2, yPos, centerAlignFormat);
            yPos += 40;
        }

















        private void button1_Click(object sender, EventArgs e)
        {
            this.Hide();
        }
 
        public void TinhTongGio(int maBan)
        {
            if (fNhanVien.banMoThoiGian.ContainsKey(maBan))
            {
                DateTime thoiGianMo = fNhanVien.banMoThoiGian[maBan];
                TimeSpan tongThoiGian = DateTime.Now - thoiGianMo;

                // Hiển thị tổng thời gian lên label lbTongGio
                lbTongGio.Text = $"{tongThoiGian.Hours} giờ {tongThoiGian.Minutes} phút";
            }
            else
            {
                lbTongGio.Text = "Bàn chưa mở";
            }
        }
        public void TinhTienBan(int maBan)
        {
            if (fNhanVien.banMoThoiGian.ContainsKey(maBan))
            {
                DateTime thoiGianMo = fNhanVien.banMoThoiGian[maBan];
                TimeSpan tongThoiGian = DateTime.Now - thoiGianMo;

                // Tính tiền bàn dựa trên giờ (50.000 VND mỗi giờ)
                decimal tienBan = (decimal)tongThoiGian.TotalHours * 50000m;

                // Cập nhật label lbTienBan
                lbTienBan.Text = $"{tienBan:0,0}";
            }
            else
            {
                lbTienBan.Text = "Bàn chưa mở";
            }
        }


        public void TinhTongCong()
        {
            decimal tongTienMon = 0m;

            // Tính tổng tiền các món trong DataGridView
            foreach (DataGridViewRow row in dgvBangGia.Rows)
            {
                if (row.Cells["ThanhTien"].Value != null)
                {
                    tongTienMon += Convert.ToDecimal(row.Cells["ThanhTien"].Value);
                }
            }

            // Lấy tiền bàn từ lbTienBan
            decimal tienBan = 0m;
            if (decimal.TryParse(lbTienBan.Text.Replace("Tiền bàn: ", "").Replace(" VND", "").Replace(",", ""), out decimal tienBanParsed))
            {
                tienBan = tienBanParsed;
            }

            // Cộng tiền bàn vào tổng tiền
            decimal tongCong = tongTienMon + tienBan;

            // Hiển thị tổng cộng trên lbTongCong
            lbTongCong.Text = $"{tongCong:0,0}";
        }





        private void timerTienBan_Tick(object sender, EventArgs e)
        {
            TinhTienBan(MaBan);
        }
        private List<KhachHang> GetKhachHangListFromDatabase()
        {
            string query = "SELECT * FROM KhachHang"; // Truy vấn SQL
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
        private void LoadKhachHangVaoComboBox()
        {
            List<KhachHang> allKhachHangList = GetKhachHangListFromDatabase();

            // Tạo đối tượng KhachHang giả lập cho Khách Vãng Lai
            KhachHang khachVangLai = new KhachHang
            {
                MaKH = 0, // Mã khách hàng là 0 hoặc một giá trị đặc biệt
                HoTen = "Khách Vãng Lai" // Tên hiển thị
            };

            // Thêm Khách Vãng Lai vào đầu danh sách
            allKhachHangList.Insert(0, khachVangLai);

            // Thiết lập DataSource cho ComboBox
            cbThongTinKH.DataSource = allKhachHangList;
            cbThongTinKH.DisplayMember = "TenHienThi"; // Hiển thị tên khách hàng
            cbThongTinKH.ValueMember = "MaKH";    // Giá trị là Mã khách hàng
        }
        private void LoadKhuyenMaiVaoComboBox()
        {
            List<KhuyenMai> allKhuyenMaiList = GetKhuyenMaiListFromDatabase();

            // Thêm một mục "Không áp dụng" vào đầu danh sách
            KhuyenMai khuyenMaiKhongApDung = new KhuyenMai
            {
                TenKM = "Không áp dụng",
                GiaTriKM = 0, // Giá trị khuyến mãi là 0
                MaKM = -1 // Thêm một mã khuyến mãi đặc biệt cho trường hợp "Không áp dụng"
            };

            // Chèn vào đầu danh sách
            allKhuyenMaiList.Insert(0, khuyenMaiKhongApDung);

            // Thiết lập DataSource cho ComboBox
            cbKhuyenMai.DataSource = allKhuyenMaiList;
            cbKhuyenMai.DisplayMember = "TenKMWithValue"; // Hiển thị dạng "TenKM (GiaTriKM%)"
            cbKhuyenMai.ValueMember = "GiaTriKM"; // Giá trị là GiaTriKM

            // Gán MaKM vào Tag của ComboBox (chỉ dùng cho thao tác sau khi chọn item)
            cbKhuyenMai.SelectedIndexChanged += (sender, e) =>
            {
                if (cbKhuyenMai.SelectedItem is KhuyenMai selectedKhuyenMai)
                {
                    cbKhuyenMai.Tag = selectedKhuyenMai.MaKM;  // Lưu MaKM vào Tag khi chọn mục
                }
            };
        }


        private List<KhuyenMai> GetKhuyenMaiListFromDatabase()
        {
            string query = "SELECT * FROM KhuyenMai"; // Truy vấn SQL
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
        public void TinhTongTienCuoi()
        {
            // Lấy giá trị tổng cộng từ lbTongCong (giả sử lbTongCong.Text là số tiền)
            decimal tongCong = 0;
            decimal thanhVienGiam = 0;
            decimal khuyenMaiGiam = 0;

            // Lấy tổng cộng
            if (decimal.TryParse(lbTongCong.Text, out tongCong))
            {
                // Lấy giá trị giảm giá thành viên (nếu có)
                if (decimal.TryParse(lbThanhVien.Text.Replace("-", "").Trim(), out thanhVienGiam))
                {
                    thanhVienGiam = Math.Abs(thanhVienGiam); // Đảm bảo giá trị dương
                }

                // Lấy giá trị giảm giá khuyến mãi (nếu có)
                if (decimal.TryParse(lbKhuyenMai.Text.Replace("-", "").Trim(), out khuyenMaiGiam))
                {
                    khuyenMaiGiam = Math.Abs(khuyenMaiGiam); // Đảm bảo giá trị dương
                }

                // Tính tổng tiền cuối cùng
                decimal tongTienCuoi = tongCong - thanhVienGiam - khuyenMaiGiam;

                // Cập nhật lbThanhTien
                lbThanhTien.Text = $"{tongTienCuoi:#,##0}";
            }
        }

        private void ResetComboBox(ComboBox comboBox)
        {
            comboBox.SelectedIndex = -1; // Đặt lại SelectedIndex về -1 (không chọn mục nào)
            comboBox.Text = string.Empty; // Xóa text hiển thị
            comboBox.Items.Clear(); // Xóa tất cả các mục trong ComboBox
        }

        private void fThanhToan_Load(object sender, EventArgs e)
        {
            TinhTongGio(MaBan);
            TinhTienBan(MaBan); // Cập nhật tiền bàn
            TinhTongCong();
            TinhTongTienCuoi();

            LoadKhachHangVaoComboBox();
            LoadKhuyenMaiVaoComboBox();
            LoadHinhThucThanhToanVaoComboBox();

        }


        // Phương thức mới cho Khuyến Mãi
        public void CapNhatKhuyenMai()
        {
            if (cbKhuyenMai.SelectedIndex != -1)
            {
                // Lấy giá trị tổng cộng từ lbTongCong (giả sử lbTongCong.Text là số tiền)
                decimal tongCong = 0;
                if (decimal.TryParse(lbTongCong.Text, out tongCong))
                {
                    // Lấy đối tượng khuyến mãi được chọn
                    KhuyenMai selectedKM = cbKhuyenMai.SelectedItem as KhuyenMai;

                    if (selectedKM != null && selectedKM.GiaTriKM.HasValue)
                    {
                        // Tính giảm giá dựa trên phần trăm khuyến mãi
                        decimal giamGia = tongCong * (selectedKM.GiaTriKM ?? 0) / 100;
                        lbKhuyenMai.Text = $"- {giamGia:#,##0}"; // Định dạng tiền tệ
                    }
                    else
                    {
                        lbKhuyenMai.Text = "0"; // Không có khuyến mãi
                    }
                }
                else
                {
                    lbKhuyenMai.Text = "0"; // Đảm bảo giá trị mặc định nếu lbTongCong không hợp lệ
                }
            }

            // Tính lại tổng tiền cuối cùng
            TinhTongTienCuoi();
        }

        private void cbKhuyenMai_SelectedIndexChanged(object sender, EventArgs e)
        {
            CapNhatKhuyenMai();
        }

        // Phương thức mới cho Thông Tin Khách Hàng
        public void CapNhatThongTinKH()
        {
            if (cbThongTinKH.SelectedIndex != -1)
            {
                decimal tongCong = 0;
                if (decimal.TryParse(lbTongCong.Text, out tongCong))
                {
                    if (cbThongTinKH.SelectedIndex == 0)
                    {
                        lbThanhVien.Text = "0"; // Không có giảm giá cho khách vãng lai
                    }
                    else
                    {
                        decimal giamGiaThanhVien = tongCong * 0.1m;
                        lbThanhVien.Text = $"- {giamGiaThanhVien:#,##0}"; // Định dạng tiền tệ
                    }
                }
                else
                {
                    lbThanhVien.Text = "0"; // Đảm bảo giá trị mặc định nếu lbTongCong không hợp lệ
                }
            }

            TinhTongTienCuoi();
        }

        private void cbThongTinKH_SelectedIndexChanged(object sender, EventArgs e)
        {
            CapNhatThongTinKH();
        }



        private List<HoaDon> GetHoaDonListFromDatabase()
        {
            string query = "SELECT * FROM HoaDon"; // Truy vấn SQL
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

        private void LoadHinhThucThanhToanVaoComboBox()
        {
            // Lấy danh sách hóa đơn từ database
            List<HoaDon> allHoaDonList = GetHoaDonListFromDatabase();

            // Tạo một danh sách các hình thức thanh toán từ danh sách hóa đơn
            List<string> hinhThucList = allHoaDonList
                .Select(hd => hd.HinhThucThanhToan)
                .Distinct() // Loại bỏ các giá trị trùng lặp
                .Where(ht => !string.IsNullOrEmpty(ht)) // Loại bỏ các giá trị null hoặc rỗng
                .ToList();

            // Nếu bạn muốn thêm mục "Không áp dụng" vào đầu danh sách
            // hinhThucList.Insert(0, "Không áp dụng");

            // Thiết lập DataSource cho ComboBox
            cbPhuongThucTT.DataSource = hinhThucList;
        }



       

    }
}
