using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QL_Bida.Model
{
    public class KhuyenMai
    {
        public int MaKM { get; set; }                   // Mã khuyến mãi
        public string TenKM { get; set; }                // Tên khuyến mãi
        public string MoTa { get; set; }                 // Mô tả khuyến mãi
        public DateTime ThoiGianApDungStart { get; set; } // Thời gian áp dụng bắt đầu
        public DateTime ThoiGianApDungEnd { get; set; }   // Thời gian áp dụng kết thúc
        public decimal? GiaTriKM { get; set; }
        public string TenKMWithValue
        {
            get
            {
                return $"{TenKM} ({(int)GiaTriKM}%)";
            }
        }// Giá trị khuyến mãi (có thể null)

        // Hàm khởi tạo không tham số
        public KhuyenMai() { }

        // Hàm khởi tạo có tham số
        public KhuyenMai(int maKM, string tenKM, string moTa, DateTime thoiGianApDungStart, DateTime thoiGianApDungEnd, decimal? giaTriKM)
        {
            MaKM = maKM;
            TenKM = tenKM;
            MoTa = moTa;
            ThoiGianApDungStart = thoiGianApDungStart;
            ThoiGianApDungEnd = thoiGianApDungEnd;
            GiaTriKM = giaTriKM;
        }
    }

}
