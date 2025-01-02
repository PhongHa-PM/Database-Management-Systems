using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QL_Bida.Model
{
    public class PhieuDatBan
    {
        public int MaPhieuDat { get; set; }          // Mã phiếu đặt
        public int MaKH { get; set; }                 // Mã khách hàng
        public int MaBan { get; set; }                // Mã bàn
        public DateTime? NgayDatBan { get; set; }    // Ngày đặt bàn (Nullable nếu có thể không có)
        public int MaNV { get; set; }                 // Mã nhân viên

        // Hàm khởi tạo không tham số
        public PhieuDatBan() { }

        // Hàm khởi tạo có tham số
        public PhieuDatBan(int maPhieuDat, int maKH, int maBan, DateTime? ngayDatBan, int maNV)
        {
            MaPhieuDat = maPhieuDat;
            MaKH = maKH;
            MaBan = maBan;
            NgayDatBan = ngayDatBan;
            MaNV = maNV;
        }
    }

}
