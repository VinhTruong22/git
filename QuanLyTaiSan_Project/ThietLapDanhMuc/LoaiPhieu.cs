using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Windows.Forms;

namespace QuanLyTaiSan_Project.ThietLapDanhMuc
{
    class LoaiPhieu
    {
        KetNoiSql sqlconn = new KetNoiSql();
        
        // thêm loại phiếu
        public void ThemLoaiPhieu(string maLoaiPhieu , string tenLoaiPhieu)
        {
            try
            {
                string sqltest = string.Format("select * from loaiphieu where maloaiphieu = N'{0}'", maLoaiPhieu);
                if (sqlconn.mo_CSDL(sqltest, sqlconn.sql_data))
                {
                    MessageBox.Show("Mã loại phiếu đã tồn tại", "Loại phiếu", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                else
                {
                    string sql = string.Format("insert into loaiphieu(maloaiphieu,tenloaiphieu) values ('{0}', N'{1}')", maLoaiPhieu, tenLoaiPhieu);
                    sqlconn.ThucThiSQL(sql, sqlconn.sql_data);
                    MessageBox.Show("Đã thêm Mã Phiếu " + maLoaiPhieu + "", "Thông Báo", MessageBoxButtons.OK);
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        // sửa loại phiếu
        public void SuaLoaiPhieu(string truongDuLieu,string duLieu, string dieuKien)
        {
            try
            {
                string sql = string.Format("update loaiphieu set {0} = N'{1}' where maloaiphieu = N'{2}'", truongDuLieu, duLieu, dieuKien);
                sqlconn.ThucThiSQL(sql, sqlconn.sql_data);

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        //tìm kiếm loại phiếu
        public DataTable TimKiemLoaiPhieu(string maLoaiPhieu, string tenLoaiPhieu)
        {
            string sql = string.Format("select * from loaiphieu where maloaiphieu = '{0}' or tenloaiphieu = N'{1}'", maLoaiPhieu, tenLoaiPhieu);
            return sqlconn.ThucThiTable(sql, sqlconn.sql_data);
        }
        // xóa loại phiếu
        public void XoaLoaiPhieu(string maLoaiPhieu)
        {
            try
            {

                string sqltest = string.Format("select * from loaiphieu where maloaiphieu = N'{0}'", maLoaiPhieu);
                if (sqlconn.mo_CSDL(sqltest, sqlconn.sql_data))
                {
                    string sql = string.Format("delete loaiphieu where maloaiphieu = '{0}'", maLoaiPhieu);
                    sqlconn.ThucThiSQL(sql, sqlconn.sql_data);
                }
                else
                {
                    MessageBox.Show("Mã loại phiếu không tồn tại", "Loại phiếu", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }
    }
}
