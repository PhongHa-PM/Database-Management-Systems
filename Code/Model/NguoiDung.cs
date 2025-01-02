using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QL_Bida.Model
{
    public class NguoiDung
    {
        public int MaNguoiDung { get; set; }         // Mã người dùng
        public int MaNV { get; set; }                 // Mã nhân viên
        public string TenDangNhap { get; set; }       // Tên đăng nhập
        public string MatKhau { get; set; }           // Mật khẩu
        public string LoaiNguoiDung { get; set; }     // Loại người dùng

        // Hàm khởi tạo không tham số
        public NguoiDung() { }

        // Hàm khởi tạo có tham số
        public NguoiDung(int maNguoiDung, int maNV, string tenDangNhap, string matKhau, string loaiNguoiDung)
        {
            MaNguoiDung = maNguoiDung;
            MaNV = maNV;
            TenDangNhap = tenDangNhap;
            MatKhau = matKhau;
            LoaiNguoiDung = loaiNguoiDung;
        }
    }

}
