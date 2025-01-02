using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QL_Bida.Model
{
    public class ChiTietHoaDon
    {
        public int SoHoaDon { get; set; }       // Số hóa đơn
        public int MaMon { get; set; }          // Mã món ăn
        public int SoLuong { get; set; }        // Số lượng món ăn
        public decimal Gia { get; set; }        // Giá món ăn

        // Hàm khởi tạo không tham số
        public ChiTietHoaDon() { }

        // Hàm khởi tạo có tham số
        public ChiTietHoaDon(int soHoaDon, int maMon, int soLuong, decimal gia)
        {
            SoHoaDon = soHoaDon;
            MaMon = maMon;
            SoLuong = soLuong;
            Gia = gia;
        }
    }

}
