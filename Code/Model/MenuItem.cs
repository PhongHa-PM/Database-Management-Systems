using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QL_Bida.Model
{
    public class MenuItem
    {
        public int MaMon { get; set; }
        public string TenMon { get; set; }
        public string LoaiMon { get; set; }
        public string NhomThucDon {  get; set; }
        public decimal Gia { get; set; }
        public string HinhAnh { get; set; } // Đường dẫn đến hình ảnh món ăn
        public string Category { get; set; }
    }
}
