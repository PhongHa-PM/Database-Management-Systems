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
    public partial class fChuyenBan : Form
    {
        DataProvider dataProvider = new DataProvider();
        public event Action OnBanAdded;
        public fChuyenBan()
        {
            InitializeComponent();
        }
        private void btnHuy_Click(object sender, EventArgs e)
        {

            this.Close();
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
        private List<Ban> GetBanListFromDatabase()
        {
            string query = "SELECT * FROM BanBilliards"; // Truy vấn SQL lấy tất cả bàn
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

            // Lọc các bàn có trạng thái "Trống"
            return list.Where(b => b.TrangThai == "Trống").ToList();
        }

        private void LoadBansToComboBox()
        {
            // Lấy danh sách bàn có trạng thái "Trống"
            List<Ban> banList = GetBanListFromDatabase();

            // Thiết lập DataSource cho ComboBox
            cboChuyenBan.DataSource = banList;
            cboChuyenBan.DisplayMember = "MaBan"; // Hiển thị mã bàn (hoặc bạn có thể hiển thị thông tin khác nếu muốn)
            cboChuyenBan.ValueMember = "MaBan";  // Lấy giá trị là MaBan
        }

        private void btnConfirm_Click(object sender, EventArgs e)
        {

            // Đóng form sau khi thực hiện
            this.Close();
        }

        private void fChuyenBan_Load(object sender, EventArgs e)
        {
            LoadBansToComboBox();
        }

        private void cboChuyenBan_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cboChuyenBan.SelectedIndex != -1)
            {
                int maBanChon = (int)cboChuyenBan.SelectedValue;
                // Xử lý khi người dùng chọn bàn, ví dụ như hiển thị thông tin bàn đã chọn, hoặc chuyển thông tin
            }
        }
    }
}
