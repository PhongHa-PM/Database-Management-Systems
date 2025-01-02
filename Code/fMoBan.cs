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
    public partial class fMoBan : Form
    {
        public static Dictionary<int, DateTime> banMoThoiGian = new Dictionary<int, DateTime>();
        public int MaBan { get; set; } // Thuộc tính để nhận mã bàn từ form cha
        DataProvider dataProvider = new DataProvider();
        
        public fMoBan()
        {
            InitializeComponent();
        }

        private void btnHuy_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        //private void btnConfirm_Click(object sender, EventArgs e)
        //{
        //    // Mở bàn: Cập nhật trạng thái bàn trong cơ sở dữ liệu thành "Đang Sử Dụng"
        //    using (SqlConnection connection = new SqlConnection(dataProvider.constr))
        //    {
        //        connection.Open();
        //        string query = "UPDATE BanBilliards SET TrangThai = N'Đang Sử Dụng' WHERE MaBan = @MaBan";
        //        using (SqlCommand command = new SqlCommand(query, connection))
        //        {
        //            command.Parameters.AddWithValue("@MaBan", MaBan);
        //            command.ExecuteNonQuery();
        //        }
        //    }

        //    // Lưu thời gian mở bàn vào Dictionary
        //    if (!fNhanVien.banMoThoiGian.ContainsKey(MaBan))
        //    {
        //        fNhanVien.banMoThoiGian[MaBan] = DateTime.Now;
        //    }

        //    // Đóng form và trả về OK
        //    this.DialogResult = DialogResult.OK;
        //    this.Close();
        //}
        private void btnConfirm_Click(object sender, EventArgs e)
        {
            if (MaBan <= 0)
            {
                MessageBox.Show("Vui lòng chọn bàn hợp lệ!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Mở bàn: Cập nhật trạng thái bàn trong cơ sở dữ liệu thành "Đang Sử Dụng"
            using (SqlConnection connection = new SqlConnection(dataProvider.constr))
            {
                connection.Open();
                string query = "UPDATE BanBilliards SET TrangThai = N'Đang Sử Dụng' WHERE MaBan = @MaBan";
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@MaBan", MaBan);
                    command.ExecuteNonQuery();
                }
            }

            // Xóa bàn khỏi Dictionary nếu đã tồn tại trước đó
            if (fNhanVien.banMoThoiGian.ContainsKey(MaBan))
            {
                fNhanVien.banMoThoiGian.Remove(MaBan);
            }

            // Lưu thời gian mở bàn vào Dictionary
            fNhanVien.banMoThoiGian[MaBan] = DateTime.Now;
            MessageBox.Show($"Bàn {MaBan} đã được mở lúc: {DateTime.Now}", "Thông tin mở bàn");

            // Đóng form và trả về OK
            this.DialogResult = DialogResult.OK;
            this.Close();
        }



    }

}
