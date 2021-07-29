using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Windows.Forms;

namespace QuanLyTaiSan_Project.ThietLapDanhMuc
{
    class Kho
    {
        KetNoiSql sqlconn = new KetNoiSql();
        // thêm kho
        public void ThemKho(string maKho, string tenKho, string diaChiKho)
        {
            try
            {
                string sqltest = string.Format("select * from kho where makho = N'{0}'", maKho);
                if (sqlconn.mo_CSDL(sqltest, sqlconn.sql_data))
                {
                    MessageBox.Show("Mã kho đã tồn tại", "Kho", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                else
                {
                    string sql = string.Format("insert into kho(makho,tenkho,diachikho) values ('{0}' , N'{1}', N'{2}')", maKho, tenKho, diaChiKho);
                    sqlconn.ThucThiSQL(sql, sqlconn.sql_data);
                    MessageBox.Show("Thêm thành công kho " + maKho + "", "Kho", MessageBoxButtons.OK);
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }
        // sửa kho theo từng dữ liệu
        public void SuaKhoTungDuLieu(string truongDuLieu,string duLieu, string dieuKien)
        {
            try
            {
                string sql = string.Format("update kho set {0} = N'{1}' where makho = '{2}'", truongDuLieu, duLieu, dieuKien);
                sqlconn.ThucThiSQL(sql, sqlconn.sql_data);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
       
        // tìm kiếm thông tin theo tendn và họ tên
        public DataTable TimKiemKho(string maKho, string tenKho, string diaChiKho)
        {
            string sql = string.Format("select * from kho where makho = N'{0}' or tenkho = N'{1}' or diachikho = N'{2}'", maKho, tenKho, diaChiKho);
            return sqlconn.ThucThiTable(sql, sqlconn.sql_data);
        }
        public void XoaKho(string maKho)
        {
            string sqltest = string.Format("select * from kho where makho = N'{0}'", maKho);
            if (sqlconn.mo_CSDL(sqltest,sqlconn.sql_data))
            {
                string sql = string.Format("delete kho where makho = '{0}'", maKho);
                sqlconn.ThucThiSQL(sql, sqlconn.sql_data);
                MessageBox.Show("Xóa thành công kho " + maKho + "", "Kho", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
            {
                MessageBox.Show("Mã kho không tồn tại!!", "Kho", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            
        }

    }
}
