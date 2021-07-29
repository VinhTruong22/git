using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyTaiSan_Project.HeThong
{
    class DoiMatKhau_XuLy
    {
        KetNoiSql sqlconn = new KetNoiSql();
        // đổi mật khẩu
        public void DoiMatKhau(string matKhau,string dieuKien)
        {
            string sql = "update nguoidung set mkdn = '" + matKhau + "' where tendn = '" + dieuKien + "'";
            sqlconn.ThucThiSQL(sql, sqlconn.sql_data);
        }
        public string LayDuLieu(string dieuKien,string duLieuLay)
        {
            return sqlconn.LayDuLieu("select * from nguoidung where tendn = N'"+dieuKien+"'",duLieuLay,sqlconn.sql_data);
        }
    }
}
