using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QL_Bida.Model
{
    public class Kho
    {
        public int MaSP { get; set; }                   // Mã sản phẩm
        public int MaMon { get; set; }                   // Mã món
        public string TenMon { get; set; }               // Tên món
        public int SoLuong { get; set; }                // Số lượng sản phẩm
        public string DonVi { get; set; }                // Đơn vị tính
        public decimal GiaBan { get; set; }              // Giá bán
        public DateTime NgayNhapGanNhat { get; set; }   // Ngày nhập gần nhất

        // Hàm khởi tạo không tham số
        public Kho() { }

        // Hàm khởi tạo có tham số
        public Kho(int maSP, int maMon, string tenMon, int soLuong, string donVi, decimal giaBan, DateTime ngayNhapGanNhat)
        {
            MaSP = maSP;
            MaMon = maMon;
            TenMon = tenMon;
            SoLuong = soLuong;
            DonVi = donVi;
            GiaBan = giaBan;
            NgayNhapGanNhat = ngayNhapGanNhat;
        }
    }

}
