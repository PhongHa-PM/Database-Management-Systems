using QL_Bida.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QL_Bida
{
    public partial class fSuaBan : Form
    {
        DataProvider dataProvider = new DataProvider();
        public event Action OnBanAdded;
        public fSuaBan()
        {
            InitializeComponent();
        }
        private int maBan;
        private string loaiBanHienTai;

        public fSuaBan(int maBan, string loaiBan)
        {
            InitializeComponent();
            this.maBan = maBan;
            this.loaiBanHienTai = loaiBan;

            // Cập nhật label tùy theo loại bàn
            if (loaiBanHienTai == "Lỗ")
            {
                labelMessage.Text = "Bạn muốn đổi sang phăng?";
            }
            else
            {
                labelMessage.Text = "Bạn muốn đổi sang lỗ?";
            }
        }
        private void btnHuy_Click(object sender, EventArgs e)
        {

            this.Close();
        }
        private List<Ban> GetBanListFromDatabase()
        {
            string query = "SELECT * FROM BanBilliards"; // Truy vấn SQL
            DataTable dataTable = dataProvider.ExecQuery(query); // Sử dụng phương thức của DataProvider

            List<Ban> list = new List<Ban>();
            foreach (DataRow row in dataTable.Rows)
            {
                Ban item = new Ban
                {
                    MaBan = Convert.ToInt32(row["MaBan"]),
                    LoaiBan = row["LoaiBan"].ToString(),
                    TrangThai = row["TrangThai"].ToString() // Giả sử TrangThai là string
                };
                list.Add(item);
            }

            return list;
        }

        
        public bool ThemBan(string loaiBan) // Chuyển đổi kiểu trả về thành bool
        {
            // Truy vấn SQL để thêm bàn mới
            string query = "INSERT INTO BanBilliards (LoaiBan, TrangThai) VALUES (@loaiBan, N'Trống')";

            // Sử dụng phương thức ExecNonQuery của DataProvider để thực hiện truy vấn
            SqlParameter parameterLoaiBan = new SqlParameter("@loaiBan", loaiBan); // Tạo tham số cho loại bàn

            // Thực hiện truy vấn
            int result = dataProvider.ExecNonQuery(query, new SqlParameter[] { parameterLoaiBan });

            // Trả về true nếu thêm thành công, false nếu thất bại
            return result > 0;
        }


       
        private void btnConfirm_Click(object sender, EventArgs e)
        {
            string loaiBanMoi = loaiBanHienTai == "Lỗ" ? "Phăng" : "Lỗ"; // Đổi loại bàn

            // Cập nhật xuống cơ sở dữ liệu
            string query = "UPDATE BanBilliards SET LoaiBan = @LoaiBan WHERE MaBan = @MaBan";
            SqlParameter[] parameters = new SqlParameter[]
            {
            new SqlParameter("@LoaiBan", loaiBanMoi),
            new SqlParameter("@MaBan", maBan) // Mã bàn hiện tại
            };

            int result = dataProvider.ExecNonQuery(query, parameters);
            if (result > 0)
            {
               
                this.Close(); // Đóng form sau khi cập nhật
            }
            else
            {
                MessageBox.Show("Cập nhật loại bàn thất bại!");
            }
            // Đóng form sau khi thực hiện
            this.Close();
        }
    }
}
