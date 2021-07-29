using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Windows.Forms;

namespace QuanLyTaiSan_Project.ThietLapDanhMuc
{
    class DonViTinh
    {
        KetNoiSql sqlconn = new KetNoiSql();
        // thêm Đơn Vị Tính
        public void ThemDonViTinh(string maDonViTinh, string tenDonViTinh)
        {
            try
            {

                string sqltest = string.Format("select * from donvitinh where madonvitinh = N'{0}'", maDonViTinh);
                if (sqlconn.mo_CSDL(sqltest, sqlconn.sql_data))
                {
                    MessageBox.Show("Mã đơn vị tính đã tồn tại", "Đơn vị tính", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                else
                {
                    string sql = string.Format("insert into donvitinh(madonvitinh,tendonvitinh) values ('{0}', N'{1}')", maDonViTinh, tenDonViTinh);
                    sqlconn.ThucThiSQL(sql, sqlconn.sql_data);
                    MessageBox.Show("Thêm đơn vị tính thành công", "Đơn vị Tính", MessageBoxButtons.OK);
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        // sửa đơn vị tính
        public void SuaDonViTinh(string truongDuLieu, string duLieu, string dieuKien)
        {
            try
            {
                string sql = string.Format("update donvitinh set {0 = N'{1}' where madonvitinh = N'{2}'", truongDuLieu, duLieu, dieuKien);
                sqlconn.ThucThiSQL(sql, sqlconn.sql_data);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        //tìm kiếm đơn vị tính
        public DataTable TimKiemDonViTinh(string maDonViTinh, string tenDonViTinh)
        {
            string sql = string.Format("select * from donvitinh where madonvitinh = '{0}' or tendonvitinh = N'{1}'", maDonViTinh, tenDonViTinh);
            return sqlconn.ThucThiTable(sql, sqlconn.sql_data);
        }
        // xóa loại phiếu
        public void XoaDonViTinh(string maDonViTinh)
        {
            string sqltest = string.Format("select * from donvitinh where madonvitinh = N'{0}'", maDonViTinh);
            if (sqlconn.mo_CSDL(sqltest,sqlconn.sql_data))
            {
                string sql = string.Format("delete donvitinh where madonvitinh = N'{0}'", maDonViTinh);
                sqlconn.ThucThiSQL(sql, sqlconn.sql_data);
            }
            else
            {
                MessageBox.Show("Mã đơn vị tính không tồn tại", "Đơn vị tính", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            
        }
    }
}
