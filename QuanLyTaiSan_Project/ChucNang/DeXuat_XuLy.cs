using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Windows.Forms;

namespace QuanLyTaiSan_Project.ChucNang
{
    class DeXuat_XuLy
    {
        KetNoiSql sqlconn = new KetNoiSql();
        // hiện datagridview
        public string LayDuLieu(string maDeXuat,string truongLayDuLieu)
        {
            KetNoiSql sqlconn = new KetNoiSql();
            string sql = string.Format("select * from xetduyet where madexuat = N'{0}'",maDeXuat);
            return sqlconn.LayDuLieu(sql, truongLayDuLieu, sqlconn.sql_data);
        }
        public DataTable HienDtaGV()
        {
            string sql = string.Format("select * from xetduyet");
            return sqlconn.ThucThiTable(sql, sqlconn.sql_data);
        }
        // thêm đề xuất
        public void ThemDeXuat(string tenDeXuat, string maDonVi, string loaiThietBi, string tenThietBi,DateTime ngayDeXuat,string lyDoDeXuat,string nguoiDeXuat)
        {

            try
            {
                // thêm những thông tin liên quan đến vấn đề đề xuất
                string sqltest = string.Format("select * from donvi where madonvi = N'{0}'",maDonVi);
                if (sqlconn.mo_CSDL(sqltest,sqlconn.sql_data))
                {
                    string sql = string.Format("insert into xetduyet(tendexuat,madonvi,maloaitb,tentb,ngaydexuat,lydodexuat,trangthaiduyet,nguoidexuat) values (N'{0}','{1}','{2}',N'{3}','{4}',N'{5}',N'Chưa duyệt','{6}')", tenDeXuat, maDonVi, loaiThietBi, tenThietBi, ngayDeXuat, lyDoDeXuat, nguoiDeXuat);
                    sqlconn.ThucThiSQL(sql, sqlconn.sql_data);
                    MessageBox.Show("Đã thêm thành công đề xuất " + tenDeXuat + "", "Thông Báo", MessageBoxButtons.OK);
                }
                else
                {
                    MessageBox.Show("Mã đơn vị không tồn tại", "Thông Báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        // cập nhật đề xuất 
        public void CapNhatDeXuat(string truongDuLieu, string duLieu, string maDeXuat, string nguoiDeXuat)
        {
            
            string sqltest = string.Format("select * from xetduyet where madexuat = N'{0}'", maDeXuat);
            //try
            //{
                // dòng if này để thể hiện bạn phải nhập đúng mã đề xuất để được cập nhật nếu sai sẽ xuất ra thông báo
                if (sqlconn.mo_CSDL(sqltest, sqlconn.sql_data))
                {
                    // kiểu update này sẽ được hiểu là bạn muốn update vấn đề nào thì sẽ ghi ra trường dữ liệu đó và dữ liệu đó cho mã đề xuất, theo đó cũng cập nhật luôn người đề xuất
                    string sql = string.Format("update xetduyet set {0} = N'{1}', nguoidexuat = '{3}' where madexuat = N'{2}'", truongDuLieu, duLieu, maDeXuat, nguoiDeXuat);
                    sqlconn.ThucThiSQL(sql, sqlconn.sql_data);
                }
                else
                {
                    MessageBox.Show("Mã đề xuất không tồn tại", "Thông Báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            //}
            //catch (Exception ex)
            //{
            //    MessageBox.Show(ex.Message);
            //}
        }
        // xóa đề xuất
        public void XoaDeXuat(string maDeXuat)
        {
            string sqltest = string.Format("select * from xetduyet where madexuat = N'{0}'", maDeXuat);
            try
            {
                // xác nhận bạn có nhập đúng mã đề xuất không??
                if (sqlconn.mo_CSDL(sqltest, sqlconn.sql_data))
                {
                    string sql = string.Format("delete xetduyet where madexuat = N'{0}'", maDeXuat);
                    sqlconn.ThucThiSQL(sql, sqlconn.sql_data);
                    MessageBox.Show("Xóa thành công đê xuất " + maDeXuat + "");
                }
                else
                {
                    MessageBox.Show("Mã đề xuất không tồn tại", "Thông Báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        //// xét duyệt
        
        public void XetDuyet(string trangthai,string lyDoDuyet, DateTime ngayXetDuyet, string nguoiDuyet, string maDeXuat)
        {
            string sqltest = string.Format("select * from xetduyet where madexuat = N'{0}'", maDeXuat);
            try
            {
                // xác nhận bạn có nhập đúng mã đề xuất không??
                if (sqlconn.mo_CSDL(sqltest,sqlconn.sql_data))
                {
                    if (trangthai == "Duyệt")
                    {
                        if (MessageBox.Show("Bạn chấp nhận "+trangthai+" ??","Thông Báo",MessageBoxButtons.YesNo,MessageBoxIcon.Question) == DialogResult.Yes)
                        {
                            // if bạn chấp nhận duyệt thì sẽ cập nhật trạng thái, lý do, ngay xét duyệt và người duyệt
                            string sql = string.Format("update xetduyet set trangthaiduyet = N'Duyệt', lydoduyet = N'{0}', ngayxetduyet = '{1}', nguoiduyet = '{2}' where madexuat = N'{3}'", lyDoDuyet, ngayXetDuyet, nguoiDuyet, maDeXuat);
                            sqlconn.ThucThiSQL(sql, sqlconn.sql_data);
                            MessageBox.Show("Bạn duyệt thành công đề xuất này", "Xét duyệt", MessageBoxButtons.OK);
                        } 
                    }
                    else
                    {
                        if (MessageBox.Show("Bạn chấp nhận " + trangthai + " ??", "Thông Báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                        {
                            // if bạn không chấp nhận duyệt thì sẽ cập nhật trạng thái, lý do, ngay xét duyệt và người duyệt
                            string sql = string.Format("update xetduyet set trangthaiduyet = N'Không duyệt', lydoduyet = N'{0}', ngayxetduyet = '{1}', nguoiduyet = '{2}' where madexuat = N'{3}'", lyDoDuyet, ngayXetDuyet, nguoiDuyet, maDeXuat);
                            sqlconn.ThucThiSQL(sql, sqlconn.sql_data);
                            MessageBox.Show("Bạn không duyệt đề xuất này", "Xét duyệt", MessageBoxButtons.OK);
                        }
                    }
                }
                else
                {
                    MessageBox.Show("Mã đề xuất không tồn tại", "Thông Báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
