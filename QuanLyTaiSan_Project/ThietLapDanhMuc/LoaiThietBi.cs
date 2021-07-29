using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Windows.Forms;

namespace QuanLyTaiSan_Project.ThietLapDanhMuc
{
    class LoaiThietBi
    {
        KetNoiSql sqlconn = new KetNoiSql();
        // thêm loại thiết bị
        public void ThemLoaiThietBi(string maLoaitb,string tenLoaitb)
        {
            string sqltest = string.Format("select * from loaithietbi where maloaitb = N'{0}'", maLoaitb);
            if (sqlconn.mo_CSDL(sqltest, sqlconn.sql_data))
            {
                MessageBox.Show("Mã loại thiết bị đã tồn tại!! Xin thử lại", "Loại thiết bị", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
            {
                string sql = string.Format("insert into loaithietbi(maloaitb,tenloaitb) values ('{0}',N'{1}')", maLoaitb, tenLoaitb);
                sqlconn.ThucThiSQL(sql, sqlconn.sql_data);
                MessageBox.Show("Thêm loại thiết bị thành công", "Loại thiết bị", MessageBoxButtons.OK);
            }
        }
        // sửa loại thiết bị
        public void SuaLoaiThietBi(string truongDuLieu, string duLieu,string dieuKien)
        {
            try
            {
                string sql = string.Format("update loaithietbi set {0} = N'{1}' where maloaitb = '{2}'", truongDuLieu, duLieu, dieuKien);
                sqlconn.ThucThiSQL(sql, sqlconn.sql_data);

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }
        //tìm kiếm tất cả loại thiết bị
        public DataTable TimKiemLoaiThietBi(string maLoaiTb, string tenLoaiTb)
        {
            string sql = string.Format("select * from loaithietbi where maloaitb = '{0}' or tenloaitb = N'{1}'", maLoaiTb, tenLoaiTb);
            return sqlconn.ThucThiTable(sql, sqlconn.sql_data);
        }
        // xóa loại thiết bị
        public void XoaLoaiThietBi(string dieuKien)
        {
            string sqltest = string.Format("select * from loaithietbi where maloaitb = N'{0}'", dieuKien);
            if (sqlconn.mo_CSDL(sqltest, sqlconn.sql_data))
            {
                string sql = string.Format("delete loaithietbi where maloaitb = '{0}'", dieuKien);
                sqlconn.ThucThiSQL(sql, sqlconn.sql_data);
                MessageBox.Show("Xóa thành công loại phiếu " + dieuKien + "", "Loại thiết bị", MessageBoxButtons.OK);
            }
            else
            {
                MessageBox.Show("Mã loại thiết bị không tồn tại!! Xin thử lại", "Loại thiết bị", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
           
        }
    }
}
