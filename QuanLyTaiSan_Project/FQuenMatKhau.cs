using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QuanLyTaiSan_Project
{
    public partial class FQuenMatKhau : Form
    {
        public FQuenMatKhau()
        {
            InitializeComponent();
            LoadCbxNoiDung();
        }

        private void btnQuenMatKhau_Click(object sender, EventArgs e)
        {
            KetNoiSql sqlconn = new KetNoiSql();
            string testTaiKhoan = string.Format("select * from nguoidung where tendn = '{0}'", txtTaiKhoan.Text);
            string mail = sqlconn.LayDuLieu("select gmail from nguoidung where tendn = '"+ txtGmail.Text+"'","gmail",sqlconn.sql_data);
            string checkTrangThai = sqlconn.LayDuLieu("select * from quenmatkhau where tendn = '"+txtTaiKhoan.Text+"'", "trangthaiyeucau", sqlconn.sql_data);
            
            if (txtTaiKhoan.Text != string.Empty && txtGmail.Text != string.Empty && cbxNoiDung.Text != string.Empty)
            {
                if (sqlconn.mo_CSDL(testTaiKhoan,sqlconn.sql_data))
                {
                    if (txtGmail.Text != mail)
                    {
                        MessageBox.Show("Bạn nhập sai gmai", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                    else
                    {

                        if (checkTrangThai == "Chưa duyệt")
                        {
                            int sophut = Convert.ToInt32(sqlconn.ThucThiTableStr("select dbo.travesophutcodieukien('" + txtTaiKhoan.Text + "')", sqlconn.sql_data));
                            if (sophut <= 30 && sophut >= 0)
                            {
                                int toanThoiGian = 30 - sophut;
                                MessageBox.Show("Bạn vừa gửi thông tin lên hệ thống. Vui lòng đợi " + toanThoiGian + " phút", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            }

                        }
                        else
                        {
                            sqlconn.ThucThiSQL("insert into quenmatkhau(tendn,gmail,noidungyeucau,ngaygioyeucau,sophutdayeucau,trangthaiyeucau) values('" + txtTaiKhoan.Text + "',N'" + txtGmail.Text + "',N'" + cbxNoiDung.Text + "','" + DateTime.Now + "', 0 ,N'Chưa duyệt')", sqlconn.sql_data);
                            MessageBox.Show("Đã gửi thông tin. Vui lòng đợi 30 phút để xét duyệt", "Thông Báo", MessageBoxButtons.OK);

                        }
                    }
                }
                else
                {
                    MessageBox.Show("Tài Khoản không tồn tại!!", "Quên Mật Khẩu", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            else
            {
                MessageBox.Show("Bạn nhập thiếu thông tin", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
        void LoadCbxNoiDung()
        {
            List<string> items = new List<string> { "Quên Mật Khẩu"};
            cbxNoiDung.DataSource = items;
        }
    }
}
