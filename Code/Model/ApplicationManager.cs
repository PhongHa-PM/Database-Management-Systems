using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QL_Bida.Model
{
    public static class ApplicationManager
    {
        // Dùng Dictionary để lưu các form thanh toán theo mã bàn
        public static Dictionary<int, fThanhToan> danhSachThanhToan = new Dictionary<int, fThanhToan>();
    }
}
