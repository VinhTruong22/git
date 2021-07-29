using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Windows.Forms;

namespace QuanLyTaiSan_Project.ThietLapDanhMuc
{
    class PhongHoc
    {
        KetNoiSql sqlconn = new KetNoiSql();
        // thêm phòng học
        public void ThemPhongHoc(string maPhongHoc, string tenPhongHoc, string diaDiemPhongHoc)
        {
            try
            {

                string sqltest = string.Format("select * from phonghoc where maphonghoc = N'{0}'", maPhongHoc);
                if (sqlconn.mo_CSDL(sqltest, sqlconn.sql_data))
                {
                    MessageBox.Show("Mã phòng học đã tồn tại", "Phòng học", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                else
                {
                    string sql = string.Format("insert into phonghoc(maphonghoc,tenphonghoc,diadiem) values ('{0}',N'{1}',N'{2}')", maPhongHoc, tenPhongHoc, diaDiemPhongHoc);
                    sqlconn.ThucThiSQL(sql, sqlconn.sql_data);
                    MessageBox.Show("Thêm thành công phòng học " + maPhongHoc + "", "Phòng học", MessageBoxButtons.OK);
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        //sửa phòng học theo dữ liệu
        public void SuaPhongHocTheoDuLieu(string truongDuLieu, string duLieu,string dieuKien)
        {
            try
            {
                string sql = string.Format("update phonghoc set {0} = N'{1}' where maphonghoc = N'{2}'", truongDuLieu, duLieu, dieuKien);
                sqlconn.ThucThiSQL(sql, sqlconn.sql_data);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            
        }
        
        // tìm kiếm phòng học
        public DataTable TimKiemPhongHoc(string maPhongHoc, string tenPhongHoc, string diaChiPhongHoc)
        {
            string sql = string.Format("select * from phonghoc where maphonghoc = N'{0}' or tenphonghoc = N'{1}' or diadiem = N'{2}'", maPhongHoc, tenPhongHoc, diaChiPhongHoc);
            return sqlconn.ThucThiTable(sql, sqlconn.sql_data);
        }
        // xóa phòng học
        public void XoaPhongHoc(string maPhongHoc)
        {
            try
            {
                string sqltest = string.Format("select * from phonghoc where maphonghoc = N'{0}'", maPhongHoc);
                if (sqlconn.mo_CSDL(sqltest, sqlconn.sql_data))
                {
                    string sql = string.Format("delete phonghoc where maphonghoc = N'{0}'", maPhongHoc);
                    sqlconn.ThucThiSQL(sql, sqlconn.sql_data);
                    MessageBox.Show("Xóa thành công phòng học " + maPhongHoc + "", "Phòng học", MessageBoxButtons.OK);
                }
                else
                {
                    MessageBox.Show("Mã phòng học không tồn tại", "Phòng học", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
