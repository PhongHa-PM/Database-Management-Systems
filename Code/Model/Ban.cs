using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QL_Bida.Model
{
    public class Ban
    {
        public int MaBan { get; set; }         
        public string LoaiBan { get; set; }    
        public string TrangThai { get; set; }
        public string TenHienThi
        {
            get { return $"Bàn {MaBan}"; }
        }
    }
}
