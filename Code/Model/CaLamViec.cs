using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QL_Bida.Model
{
    public class CaLamViec
    {
        public int MaCa { get; set; }                     // Mã ca làm việc
        public string TenCa { get; set; }                 // Tên ca làm việc
        public TimeSpan ThoiGianBatDau { get; set; }      // Thời gian bắt đầu
        public TimeSpan ThoiGianKetThuc { get; set; }     // Thời gian kết thúc

        // Hàm khởi tạo không tham số
        public CaLamViec() { }

        // Hàm khởi tạo có tham số
        public CaLamViec(int maCa, string tenCa, TimeSpan thoiGianBatDau, TimeSpan thoiGianKetThuc)
        {
            MaCa = maCa;
            TenCa = tenCa;
            ThoiGianBatDau = thoiGianBatDau;
            ThoiGianKetThuc = thoiGianKetThuc;
        }
    }

}
