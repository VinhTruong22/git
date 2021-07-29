using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;

namespace QuanLyTaiSan_Project
{
    class KetNoiSql
    {
        // mở kết nối sql
        SqlConnection sqlconn = new SqlConnection();
        // string data để liên kết kết nối
        public string sql_data = @"Data Source=DESKTOP-Q25FA0S;Initial Catalog=QTTB;Integrated Security=True";
        // nạp dữ liệu vào Table
        public DataTable ThucThiTable(string sql, string dataconn)
        {
            sqlconn.ConnectionString = dataconn;
            sqlconn.Open();
            SqlCommand cmd = new SqlCommand(sql, sqlconn);
            DataTable data = new DataTable();
            SqlDataAdapter adapter = new SqlDataAdapter(cmd);
            adapter.Fill(data);
            sqlconn.Close();
            return data;
        }
        // thực thi hàm trả về
        public string ThucThiTableStr(string sql, string dataconn)
        {
            sqlconn.ConnectionString = dataconn;
            sqlconn.Open();
            SqlCommand cmd = new SqlCommand(sql, sqlconn);
            DataTable data = new DataTable();
            SqlDataAdapter adapter = new SqlDataAdapter(cmd);
            adapter.Fill(data);
            sqlconn.Close();
            return data.Rows[0][0].ToString();
        }
        // đọc dữ liệu và xét duyệt nó
        public bool mo_CSDL(string sql, string dataconn)
        {
            sqlconn.ConnectionString = dataconn;
            sqlconn.Open();
            SqlCommand cmd = new SqlCommand(sql, sqlconn);
            SqlDataReader dta = cmd.ExecuteReader();

            if (dta.Read() == true)
            {
                sqlconn.Close();
                return true;
            }
            else
            {
                sqlconn.Close();
                return false;
            }
        }
        // thực thi những câu lệnh 
        public void ThucThiSQL(string sql, string dataconn)
        {
            sqlconn.ConnectionString = dataconn;
            sqlconn.Open();
            SqlCommand cmd = new SqlCommand(sql, sqlconn);
            cmd.ExecuteNonQuery();
            sqlconn.Close();
        }
        
        // lấy ra những trường dữ liệu trong database
        public string LayDuLieu(string sql, string GiaTri, string dataconn)
        {
            sqlconn.ConnectionString = dataconn;
            sqlconn.Open();
            SqlCommand cmd = new SqlCommand(sql, sqlconn);
            SqlDataReader reader = cmd.ExecuteReader();
            string data = "";
            if (reader.Read())
            {
                data = reader[GiaTri].ToString();
            }
            sqlconn.Close();
            return data;
        }
        public DataSet ThucthiDataSet(string sql, string dataconn)
        {
            sqlconn.ConnectionString = dataconn;
            sqlconn.Open();
            SqlCommand cmd = new SqlCommand(sql,sqlconn);
            SqlDataAdapter adapter = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            adapter.Fill(ds);
            sqlconn.Close();
            return ds;
        }
        
    }
}
