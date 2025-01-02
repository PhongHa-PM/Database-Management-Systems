using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QL_Bida.Model
{
    public class NhanVien
    {
        public int MaNV { get; set; }               // Mã nhân viên
        public string TenDangNhap { get; set; }     // Tên đăng nhập (null nếu vai trò khác Admin, Thu Ngân)
        public string MatKhau { get; set; }         // Mật khẩu (null nếu vai trò khác Admin, Thu Ngân)
        public string TenNV { get; set; }           // Tên nhân viên
        public DateTime? NgaySinh { get; set; }    // Ngày sinh
        public string GioiTinh { get; set; }        // Giới tính
        public int MaCa { get; set; }               // Mã ca làm việc
        public string HinhAnh { get; set; }         // Đường dẫn hoặc tên tệp hình ảnh (có thể null)
        public string VaiTro { get; set; }          // Vai trò của nhân viên (Admin, Thu Ngân, Phục Vụ, Lao Công, Bảo Vệ)

        // Hàm khởi tạo không tham số
        public NhanVien() { }

        // Hàm khởi tạo có tham số
        public NhanVien(int maNV, string tenDangNhap, string matKhau, string vaiTro, string tenNV, DateTime ngaySinh, string gioiTinh, int maCa, string hinhAnh)
        {
            MaNV = maNV;
            TenDangNhap = tenDangNhap;
            MatKhau = matKhau;
            VaiTro = vaiTro;
            TenNV = tenNV;
            NgaySinh = ngaySinh;
            GioiTinh = gioiTinh;
            MaCa = maCa;
            HinhAnh = hinhAnh;
        }
    }




}
