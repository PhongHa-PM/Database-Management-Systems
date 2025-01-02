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
    public partial class fSuaKhuyenMai : Form
    {
        DataProvider DataProvider = new DataProvider();
        public event Action OnDataSaved;
        private int maKM;
        public fSuaKhuyenMai()
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
        public void SetKhuyenMaiData(int maKM, string tenKM, string moTa, decimal giaTri, DateTime thoiGianStart, DateTime thoiGianEnd)
        {
            this.maKM = maKM;
            txtTenKM.Text = tenKM;
            txtMoTa.Text = moTa;
            txtGiaTri.Text = giaTri.ToString();
            dtThoiGianStart.Value = thoiGianStart;
            dtThoiGianEnd.Value = thoiGianEnd;
        }
        private void btnLuu_Click(object sender, EventArgs e)
        {
            // Kiểm tra dữ liệu và thông tin người dùng nhập vào
            string tenKM = txtTenKM.Text.Trim();
            string moTa = txtMoTa.Text.Trim();
            decimal giaTri;

            if (string.IsNullOrEmpty(tenKM))
            {
                MessageBox.Show("Vui lòng nhập tên khuyến mãi.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtTenKM.Focus();
                return;
            }

            if (string.IsNullOrEmpty(moTa))
            {
                MessageBox.Show("Vui lòng nhập mô tả khuyến mãi.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtMoTa.Focus();
                return;
            }

            if (string.IsNullOrEmpty(txtGiaTri.Text) || !decimal.TryParse(txtGiaTri.Text, out giaTri) || giaTri <= 0)
            {
                MessageBox.Show("Vui lòng nhập giá trị khuyến mãi hợp lệ.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtGiaTri.Focus();
                return;
            }

            if (giaTri > 100)
            {
                MessageBox.Show("Giá trị khuyến mãi không được vượt quá 100%.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtGiaTri.Focus();
                return;
            }

            DateTime thoiGianStart = dtThoiGianStart.Value;
            DateTime thoiGianEnd = dtThoiGianEnd.Value;

            if (thoiGianStart >= thoiGianEnd)
            {
                MessageBox.Show("Thời gian bắt đầu phải trước thời gian kết thúc.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                dtThoiGianStart.Focus();
                return;
            }

            // Chuỗi truy vấn cập nhật dữ liệu
            string query = "UPDATE KhuyenMai SET TenKM = @TenKM, MoTa = @MoTa, GiaTriKM = @GiaTriKM, " +
                           "ThoiGianApDungStart = @ThoiGianStart, ThoiGianApDungEnd = @ThoiGianEnd WHERE MaKM = @MaKM";

            using (SqlConnection con = new SqlConnection(DataProvider.constr))
            {
                try
                {
                    con.Open();
                    using (SqlCommand cmd = new SqlCommand(query, con))
                    {
                        cmd.Parameters.AddWithValue("@TenKM", tenKM);
                        cmd.Parameters.AddWithValue("@MoTa", moTa);
                        cmd.Parameters.AddWithValue("@GiaTriKM", giaTri);
                        cmd.Parameters.AddWithValue("@ThoiGianStart", thoiGianStart);
                        cmd.Parameters.AddWithValue("@ThoiGianEnd", thoiGianEnd);
                        cmd.Parameters.AddWithValue("@MaKM", maKM); // maKM là mã khuyến mãi được truyền vào form

                        int result = cmd.ExecuteNonQuery();

                        if (result > 0)
                        {
                            MessageBox.Show("Khuyến mãi đã được cập nhật thành công.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            this.DialogResult = DialogResult.OK;
                            this.Close();
                        }
                       
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi khi cập nhật khuyến mãi: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }



    }
}
