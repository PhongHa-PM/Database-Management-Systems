using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QL_Bida.Model
{
    public class ThucDon
    {
        public int MaMon { get; set; }               // Mã món
        public string TenMon { get; set; }           // Tên món
        public string LoaiMon { get; set; }          // Loại món
        public string NhomThucDon { get; set; }      // Nhóm thực đơn
        public string DonViTinh { get; set; }        // Đơn vị tính
        public decimal Gia { get; set; }             // Giá
        public string HinhAnh { get; set; }          // Đường dẫn đến hình ảnh

        // Hàm khởi tạo không tham số
        public ThucDon() { }

        // Hàm khởi tạo có tham số
        public ThucDon(int maMon, string tenMon, string loaiMon, string nhomThucDon, string donViTinh, decimal gia, string hinhAnh)
        {
            MaMon = maMon;
            TenMon = tenMon;
            LoaiMon = loaiMon;
            NhomThucDon = nhomThucDon;
            DonViTinh = donViTinh;
            Gia = gia;
            HinhAnh = hinhAnh;
        }
    }

}
