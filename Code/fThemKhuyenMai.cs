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
    public partial class fThemKhuyenMai : Form
    {
        DataProvider DataProvider = new DataProvider();
        public event Action OnDataSaved;
        public fThemKhuyenMai()
        {
            InitializeComponent();
            this.FormBorderStyle = FormBorderStyle.None;
            this.Padding = new Padding(0);
            this.Region = System.Drawing.Region.FromHrgn(CreateRoundRectRgn(0, 0, Width, Height, 0, 0));
            this.StartPosition = FormStartPosition.CenterScreen;


        }
        [System.Runtime.InteropServices.DllImport("Gdi32.dll", EntryPoint = "CreateRoundRectRgn")]
        private static extern IntPtr CreateRoundRectRgn(int nLeftRect, int nTopRect, int nRightRect, int nBottomRect, int nWidthEllipse, int nHeightEllipse);

        private void btnHuyBo_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnLuu_Click(object sender, EventArgs e)
        {
            // Kiểm tra thông tin nhập vào
            if (string.IsNullOrEmpty(txtTenKM.Text))
            {
                MessageBox.Show("Vui lòng nhập tên khuyến mãi.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (string.IsNullOrEmpty(txtMoTa.Text))
            {
                MessageBox.Show("Vui lòng nhập mô tả khuyến mãi.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (string.IsNullOrEmpty(txtGiaTri.Text) || !decimal.TryParse(txtGiaTri.Text, out decimal giaTri) || giaTri <= 0)
            {
                MessageBox.Show("Vui lòng nhập giá trị khuyến mãi hợp lệ.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (giaTri > 100)
            {
                MessageBox.Show("Giá trị khuyến mãi không được vượt quá 100%.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (dtThoiGianStart.Value >= dtThoiGianEnd.Value)
            {
                MessageBox.Show("Thời gian bắt đầu phải trước thời gian kết thúc.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Tạo câu truy vấn SQL
            string query = "INSERT INTO KhuyenMai (TenKM, MoTa, ThoiGianApDungStart, ThoiGianApDungEnd, GiaTriKM) " +
                           "VALUES (@TenKM, @MoTa, @ThoiGianApDungStart, @ThoiGianApDungEnd, @GiaTriKM)";

            // Tạo các tham số cho câu lệnh SQL
            SqlParameter[] parameters = new SqlParameter[]
            {
        new SqlParameter("@TenKM", SqlDbType.NVarChar) { Value = txtTenKM.Text },
        new SqlParameter("@MoTa", SqlDbType.NVarChar) { Value = txtMoTa.Text },
        new SqlParameter("@ThoiGianApDungStart", SqlDbType.DateTime) { Value = dtThoiGianStart.Value },
        new SqlParameter("@ThoiGianApDungEnd", SqlDbType.DateTime) { Value = dtThoiGianEnd.Value },
        new SqlParameter("@GiaTriKM", SqlDbType.Decimal) { Value = giaTri }
            };

            // Sử dụng chuỗi kết nối từ DataProvider
            string connectionString = DataProvider.constr;

            // Thực thi câu truy vấn SQL
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                try
                {
                    con.Open();

                    using (SqlCommand cmd = new SqlCommand(query, con))
                    {
                        cmd.Parameters.AddRange(parameters);

                        int result = cmd.ExecuteNonQuery();

                        if (result > 0)
                        {
                            MessageBox.Show("Khuyến mãi đã được lưu thành công.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);

                            // Reset dữ liệu
                            txtTenKM.Clear();
                            txtMoTa.Clear();
                            txtGiaTri.Clear();
                            dtThoiGianStart.Value = DateTime.Now;
                            dtThoiGianEnd.Value = DateTime.Now;

                            OnDataSaved?.Invoke();
                            this.Close();
                        }
                        else
                        {
                            MessageBox.Show("Lỗi khi lưu khuyến mãi. Vui lòng thử lại.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi khi kết nối cơ sở dữ liệu: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }



    }
}
