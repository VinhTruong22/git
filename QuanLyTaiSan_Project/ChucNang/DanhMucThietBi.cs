using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyTaiSan_Project.ChucNang
{
    class DanhMucThietBi
    {
        KetNoiSql sqlconn = new KetNoiSql();
        public string LayDuLieuStr(string matb , string truongDuLieu)
        {
            return sqlconn.LayDuLieu("select * from thietbi where matb = N'" + matb + "'", truongDuLieu, sqlconn.sql_data);
        }
        public decimal LayDuLieuDec(string matb, string truongDuLieu)
        {
            return decimal.Parse(sqlconn.LayDuLieu("select * from thietbi where matb = N'" + matb + "'", truongDuLieu, sqlconn.sql_data));
        }
        

    }
}
