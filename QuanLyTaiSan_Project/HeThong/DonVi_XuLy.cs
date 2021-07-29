using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data;
using System.Data.SqlClient;

namespace QuanLyTaiSan_Project.HeThong
{
    class DonVi_XuLy
    {
        KetNoiSql sqlconn = new KetNoiSql();

        // thêm đơn vị
        public void ThemDonVi(string maDV, string tenDV, string SDT, string diaDiem,string nguoicapnhat)
        {
            string sqltestMa = string.Format("select * from donvi where madonvi = '{0}'", maDV);
            if (sqlconn.mo_CSDL(sqltestMa, sqlconn.sql_data))
            {
                MessageBox.Show("Mã đơn vị đã tồn tại", "Đơn vị", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
            {
                string sql = string.Format("insert into donvi(madonvi,tendonvi,dienthoai,diadiem,nguoicapnhat) values('{0}', N'{1}','{2}',N'{3}',N'{4}')", maDV, tenDV, SDT, diaDiem, nguoicapnhat);
                sqlconn.ThucThiSQL(sql, sqlconn.sql_data);
                MessageBox.Show("Thêm đơn vị thành công", "Đơn vị", MessageBoxButtons.OK);
            }
        }

        // sửa đơn vị theo từng trường dữ liệu
        public void SuaDonViTungDuLieu(string dieuKien, string truongDuLieu, string duLieu,string nguoicapnhat)
        {
                string sql = string.Format("update donvi set {0} = N'{1}', nguoicapnhat = N'{3}' where madonvi = N'{2}'", truongDuLieu, duLieu, dieuKien, nguoicapnhat);
                sqlconn.ThucThiSQL(sql, sqlconn.sql_data);
        }
       
        //tìm kiếm toàn bộ thông tin đơn vị 
        public DataTable TimKiemToanBoDonVi(string maDonvi, string tenDonvi, string dienThoai, string diaDiem)
        {
            string sql = string.Format("select * from donvi where madonvi = '{0}' or tendonvi like N'%{1}%' or dienthoai = N'{2}' or  diadiem = N'{3}'", maDonvi, tenDonvi, dienThoai, diaDiem);
            return sqlconn.ThucThiTable(sql, sqlconn.sql_data);
        }
        // xóa đơn vị
        public void XoaDonvi(string maDonvi)
        {
            string sqltest = string.Format("select * from donvi where madonvi = '{0}'", maDonvi);
            if (sqlconn.mo_CSDL(sqltest,sqlconn.sql_data))
            {
                string sql = string.Format("delete donvi where madonvi = '{0}' ", maDonvi);
                sqlconn.ThucThiSQL(sql, sqlconn.sql_data);
            }
            else
            {
                MessageBox.Show("Mã đơn vị không tồn tại", "Đơn vị", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            
        }
        public string LayThongTinDonVi(string truongDuLieu)
        {
            return sqlconn.LayDuLieu("select * from donvi",truongDuLieu, sqlconn.sql_data);
        }
    }
}
