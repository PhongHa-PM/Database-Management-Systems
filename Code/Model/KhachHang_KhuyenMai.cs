using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QL_Bida.Model
{
    public class KhachHangKhuyenMai
    {
        public int MaKH { get; set; }         // Mã khách hàng
        public int MaKM { get; set; }         // Mã khuyến mãi
        public DateTime NgayNhan { get; set; } // Ngày nhận khuyến mãi

        // Hàm khởi tạo không tham số
        public KhachHangKhuyenMai() { }

        // Hàm khởi tạo có tham số
        public KhachHangKhuyenMai(int maKH, int maKM, DateTime ngayNhan)
        {
            MaKH = maKH;
            MaKM = maKM;
            NgayNhan = ngayNhan;
        }
    }

}
