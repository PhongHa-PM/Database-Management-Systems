using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QL_Bida.Model
{
    public class KhachHang
    {
        public int MaKH { get; set; }               // Mã khách hàng
        public string HoTen { get; set; }           // Họ tên khách hàng
        public string SDT { get; set; }             // Số điện thoại (có thể null)
        public DateTime? NgaySinh { get; set; }     // Ngày sinh (có thể null)
        public string Email { get; set; }           // Địa chỉ email (có thể null)
        public int DiemTichLuy { get; set; }
        public string TenHienThi => $"{HoTen} ({SDT})";// Điểm tích lũy

        // Hàm khởi tạo không tham số
        public KhachHang() { }

        // Hàm khởi tạo có tham số
        public KhachHang(int maKH, string hoTen, string sdt, DateTime? ngaySinh, string email, int diemTichLuy)
        {
            MaKH = maKH;
            HoTen = hoTen;
            SDT = sdt;
            NgaySinh = ngaySinh;
            Email = email;
            DiemTichLuy = diemTichLuy;
        }
    }

}
