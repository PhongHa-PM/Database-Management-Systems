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
    public partial class fXacNhanDatBan : Form
    {
        DataProvider dataProvider = new DataProvider();
        public event Action OnBanAdded;
        public fXacNhanDatBan()
        {
            InitializeComponent();
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
            if (cbo_ThucDon.SelectedItem != null)
            {
                string loaiBan = cbo_ThucDon.SelectedItem.ToString(); // Lấy loại bàn từ ComboBox
                bool isAdded = ThemBan(loaiBan); // Gọi hàm thêm bàn

                if (isAdded) // Nếu thêm bàn thành công
                {
                    OnBanAdded?.Invoke(); // Gọi sự kiện
                }
                else
                {
                    MessageBox.Show("Thêm bàn thất bại!", "Thông báo");
                }
            }
            else
            {
                MessageBox.Show("Vui lòng chọn loại bàn!", "Thông báo");
            }

            // Đóng form sau khi thực hiện
            this.Close();
        }

    }
}
