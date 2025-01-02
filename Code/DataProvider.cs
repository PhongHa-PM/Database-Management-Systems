using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;


namespace QL_Bida
{
    public class DataProvider
    {
        public string constr = @"Data Source=PHONGHA;Initial Catalog=QL_BIDA1;Integrated Security=True;";

        //Phương thức thực thi truy vấn và trả về DataTable
        public DataTable ExecQuery(string query)
        {
            DataTable data = new DataTable();
            using (SqlConnection con = new SqlConnection(constr))
            {
                try
                {
                    con.Open(); // Mở kết nối
                    using (SqlCommand cmd = new SqlCommand(query, con))
                    {
                        using (SqlDataAdapter adapter = new SqlDataAdapter(cmd))
                        {
                            adapter.Fill(data); // Điền dữ liệu vào DataTable
                        }
                    }
                }
                catch (Exception ex)
                {
                    // Xử lý lỗi nếu cần
                    MessageBox.Show("Thất bại", "Thông báo");
                }
            }
            return data; // Trả về DataTable
        }
      
        public int ExecNonQuery(string query, SqlParameter[] parameters = null)
        {
            using (SqlConnection con = new SqlConnection(constr))
            {
                try
                {
                    con.Open(); // Mở kết nối
                    using (SqlCommand cmd = new SqlCommand(query, con))
                    {
                        // Nếu có tham số, thêm tham số vào lệnh SQL
                        if (parameters != null)
                        {
                            cmd.Parameters.AddRange(parameters);
                        }

                        return cmd.ExecuteNonQuery(); // Trả về số dòng bị ảnh hưởng
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Thất bại: " + ex.Message, "Thông báo");
                    return 0; // Trả về 0 nếu có lỗi
                }
            }
        }



        // Phương thức thực thi truy vấn trả về một giá trị đơn (SELECT COUNT, SELECT TOP 1, ...)
        //public object ExecScalar(string query)
        //{
        //    using (SqlConnection con = new SqlConnection(constr))
        //    {
        //        try
        //        {
        //            con.Open(); // Mở kết nối
        //            using (SqlCommand cmd = new SqlCommand(query, con))
        //            {
        //                return cmd.ExecuteScalar(); // Trả về giá trị đầu tiên trong kết quả
        //            }
        //        }
        //        catch (Exception ex)
        //        {
        //            // Xử lý lỗi nếu cần
        //            MessageBox.Show("Thất bại", "Thông báo");
        //            return null; // Trả về null nếu có lỗi
        //        }
        //    }
        //}
        public string GetEmployeeName(int employeeId)
        {
            string query = "SELECT TenNV FROM NhanVien WHERE MaNV = @MaNV"; // Giả sử bảng là NhanVien và trường TenNV là tên nhân viên
            using (SqlConnection con = new SqlConnection(constr))
            {
                try
                {
                    con.Open(); // Mở kết nối
                    using (SqlCommand cmd = new SqlCommand(query, con))
                    {
                        cmd.Parameters.AddWithValue("@MaNV", employeeId); // Thêm tham số vào câu truy vấn
                        object result = cmd.ExecuteScalar(); // Thực thi truy vấn

                        if (result != null)
                        {
                            return result.ToString(); // Trả về tên nhân viên
                        }
                        else
                        {
                            return null; // Nếu không tìm thấy nhân viên, trả về null
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lấy tên nhân viên thất bại", "Thông báo");
                    return null; // Trả về null nếu có lỗi
                }
            }

        }
        public object ExecScalar(string query, SqlParameter[] parameters = null)
        {
            using (SqlConnection connection = new SqlConnection(constr))
            {
                connection.Open();
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    // Nếu có tham số, thêm các tham số vào câu lệnh SQL
                    if (parameters != null)
                    {
                        command.Parameters.AddRange(parameters);
                    }
                    // Thực thi câu lệnh và trả về kết quả đầu tiên
                    return command.ExecuteScalar();
                }
            }
        }
        // Phương thức thực thi truy vấn và trả về DataTable có hỗ trợ tham số
        public DataTable ExecQuery(string query, SqlParameter[] parameters = null)
        {
            DataTable data = new DataTable();
            using (SqlConnection con = new SqlConnection(constr))
            {
                try
                {
                    con.Open(); // Mở kết nối
                    using (SqlCommand cmd = new SqlCommand(query, con))
                    {
                        // Nếu có tham số, thêm các tham số vào câu lệnh SQL
                        if (parameters != null)
                        {
                            cmd.Parameters.AddRange(parameters);
                        }

                        using (SqlDataAdapter adapter = new SqlDataAdapter(cmd))
                        {
                            adapter.Fill(data); // Điền dữ liệu vào DataTable
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Thất bại: " + ex.Message, "Thông báo");
                }
            }
            return data; // Trả về DataTable
        }
        public int GetMaNVByTenDangNhap(string tenDangNhap)
        {
            string query = "SELECT MaNV FROM NguoiDung WHERE TenDangNhap = @TenDangNhap";
            SqlParameter[] parameters = new SqlParameter[]
            {
            new SqlParameter("@TenDangNhap", tenDangNhap)
            };
            object result = ExecScalar(query, parameters);
            return result != null ? Convert.ToInt32(result) : 0;
        }

        // Lấy thông tin nhân viên dựa trên MaNV
        public DataTable GetNhanVienInfoByMaNV(int maNV)
        {
            string query = "SELECT * FROM NhanVien WHERE MaNV = @MaNV";
            SqlParameter[] parameters = new SqlParameter[]
            {
            new SqlParameter("@MaNV", maNV)
            };
            return ExecQuery(query, parameters);
        }

    }
}
