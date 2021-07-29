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
    class NguoiDung_XuLy
    {
        KetNoiSql sqlconn = new KetNoiSql();
        // thêm người dùng
        public void ThemNguoiDung(string tenDN, string hoTen, string gmail ,string donVi, DateTime ngayTao,string nguoiCapNhat)
        {
            try
            {
                string sqltest = string.Format("select * from nguoidung where tendn = '{0}'", tenDN);
                if (sqlconn.mo_CSDL(sqltest, sqlconn.sql_data))
                {
                    MessageBox.Show("Tài khoản đã được đăng ký", "Đăng Ký", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                else
                {
                    string sql = string.Format("insert into nguoidung(tendn,mkdn,hoten,gmail,donvi,ngaytao,trangthaihoatdong,nguoicapnhat) values ('{0}','123',N'{1}',N'{2}','{3}','{4}',N'Không hoạt động',N'{5}')", tenDN, hoTen, gmail, donVi, ngayTao, nguoiCapNhat);
                    sqlconn.ThucThiSQL(sql, sqlconn.sql_data);
                    MessageBox.Show("Đăng ký thành công tài khoản " + tenDN + "", "Đăng ký", MessageBoxButtons.OK);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        // sửa thông tin từng dữ liệu Người dùng cho
        public void SuaThongTinTungDuLieu(string dieuKien ,string truongDuLieu, string duLieu,string nguoiCapNhat)
        {
            try
            {
                string sql = string.Format("update nguoidung set {0} = N'{1}', nguoicapnhat = N'{3}' where tendn = '{2}'", truongDuLieu, duLieu, dieuKien, nguoiCapNhat);
                sqlconn.ThucThiSQL(sql, sqlconn.sql_data);

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }
        
        
        // tìm kiếm thông tin theo tendn và họ tên
        public DataTable TimKiemThongTin(string tendn, string hoTen,string gmail, string donVi)
        {
            string sql = string.Format("select tendn, hoten, donvi, gmail,ngaytao, ngaydangnhap, trangthaihoatdong from nguoidung where tendn = '{0}' or hoten like N'%{1}%' or gmail = N'{2}' or donvi = '{3}'",tendn, hoTen,gmail,donVi);
            return sqlconn.ThucThiTable(sql, sqlconn.sql_data);
        }
        // xóa thông tin
        public void XoaThongTin(string tendn, string hoTen, string donVi)
        {
            string sqltest = string.Format("select * from nguoidung where tendn = '{0}'", tendn);
            if (sqlconn.mo_CSDL(sqltest, sqlconn.sql_data))
            {
                string sql = string.Format("delete nguoidung where tendn = '{0}'", tendn);
                sqlconn.ThucThiSQL(sql, sqlconn.sql_data);
            }
            else
            {
                MessageBox.Show("Tài khoản không tồn tại", "Người dùng", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            
        }
        public DataTable LoadDonVi()
        {
            KetNoiSql sqlconn = new KetNoiSql();
            return sqlconn.ThucThiTable("select madonvi from donvi",sqlconn.sql_data);
        }
    }
}
