using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QL_Bida.Model
{
    public class HoaDon
    {
        public int SoHoaDon { get; set; }                  // Số hóa đơn
        public int? MaKH { get; set; }                     // Mã khách hàng (có thể null)
        public int MaBan { get; set; }                     // Mã bàn
        public int MaNV { get; set; }                      // Mã nhân viên
        public decimal SoGioChoi { get; set; }             // Số giờ chơi
        public decimal? ThanhTien { get; set; }            // Thành tiền (có thể null)
        public DateTime NgayLapHoaDon { get; set; }        // Ngày lập hóa đơn
        public string HinhThucThanhToan { get; set; }      // Hình thức thanh toán
        public int? MaKM { get; set; }                     // Mã khuyến mãi (có thể null)

        // Hàm khởi tạo không tham số
        public HoaDon() { }

        // Hàm khởi tạo có tham số
        public HoaDon(int soHoaDon, int? maKH, int maBan, int maNV, decimal soGioChoi, decimal? thanhTien, DateTime ngayLapHoaDon, string hinhThucThanhToan, int? maKM)
        {
            SoHoaDon = soHoaDon;
            MaKH = maKH;
            MaBan = maBan;
            MaNV = maNV;
            SoGioChoi = soGioChoi;
            ThanhTien = thanhTien;
            NgayLapHoaDon = ngayLapHoaDon;
            HinhThucThanhToan = hinhThucThanhToan;
            MaKM = maKM;
        }
    }


}
