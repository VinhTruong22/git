using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;

namespace QuanLyTaiSan_Project.HeThong
{
    class LichSuDangNhap_XuLy
    {
        KetNoiSql sqlconn = new KetNoiSql();
        public DataTable HienLichSu(string taiKhoan)
        {
            return sqlconn.ThucThiTable("select tendn,ngaydangnhap from lichsudangnhap where tendn = '"+taiKhoan+"'",sqlconn.sql_data);
        }
        public DataTable LoadTaiKHoanLichSu()
        {
            return sqlconn.ThucThiTable("select tendn from nguoidung", sqlconn.sql_data);
        }
    }
}
