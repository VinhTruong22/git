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
    class DangNhap_XuLy
    {
        KetNoiSql sqlconn = new KetNoiSql();
        
        // thử xem người dùng có nhập dữ liệu vào chưa
        public bool testDauVao(string taiKhoan, string matKhau)
        {
            if (taiKhoan == string.Empty || matKhau == string.Empty)
            {
                return false;
            }
            return true;
            
        }
        // thuwr xem người dùng nhập đúng thông tin không??
        public bool testlogin(string taiKhoan, string matKhau)
        {
            string sql = string.Format("exec Proc_DangNhap {0} , {1}", taiKhoan, matKhau);
            if (sqlconn.mo_CSDL(sql, sqlconn.sql_data))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public void UpdateTableNguoiDungDangNhap(string truongDuLieu, string duLieu)
        {
            sqlconn.ThucThiSQL("update nguoidung set "+truongDuLieu+" = N'"+duLieu+"' where tendn = ''",sqlconn.sql_data);
        }
        
        public void InsertTableLichSuDangNhap(string duLieu, string duLieuThuHai)
        {
            sqlconn.ThucThiSQL("insert into lichsudangnhap(tendn,ngaydangnhap) values('"+duLieu+"','"+duLieuThuHai+"')",sqlconn.sql_data);
        }
        public string KiemTraHoatDong()
        {
            return sqlconn.LayDuLieu("select trangthaihoatdong from nguoidung where tendn = ''","trangthaihoatdong",sqlconn.sql_data);
        }
       public string LayDuLieu(string tendn,string duLieuLay)
        {
            return sqlconn.LayDuLieu("select * from nguoidung where tendn = '" + tendn + "'", duLieuLay, sqlconn.sql_data);
        }
    }
}
