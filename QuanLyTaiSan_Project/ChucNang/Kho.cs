using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;

namespace QuanLyTaiSan_Project.ChucNang
{
    class Kho
    {
        KetNoiSql sqlconn = new KetNoiSql();
        public DataTable LoadKhoCbx()
        {
            string sql = string.Format("select makho from kho");
            return sqlconn.ThucThiTable(sql, sqlconn.sql_data);
        }
    }
}
