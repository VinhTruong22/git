using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
namespace QuanLyTaiSan_Project
{
    public partial class FDangNhap : Form
    {
        public FDangNhap()
        {
            InitializeComponent();
        }

        private void btnThoat_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnDangNhap_Click(object sender, EventArgs e)
        {
            
            HeThong.DangNhap_XuLy nguoiDung = new HeThong.DangNhap_XuLy();
            DateTime dtOnline = DateTime.Now;
           DateTime dtOnlineLater30Minuter = DateTime.Now.AddSeconds(3);
            // kiểm tra kết nối
           // try
            //{
                // thử người dùng đã có nhập dữ liệu??
                if (!nguoiDung.testDauVao(txtTaiKhoan.Text,txtMatKhau.Text))
                {
                    MessageBox.Show("Bạn nhập thiếu thông tin", "Thông Báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtTaiKhoan.Focus();
                }
                else
                {// thử đăng nhập đã đúng với dữ liệu??
                if (nguoiDung.testlogin(txtTaiKhoan.Text,txtMatKhau.Text))
                {
                    string kiemTraOnline = nguoiDung.KiemTraHoatDong();
                    //if (kiemTraOnline == "Không hoạt động")
                    //{
                    this.Hide();
                    string timeNow = DateTime.Now.ToString();
                    nguoiDung.UpdateTableNguoiDungDangNhap("ngaydangnhap", timeNow);
                    nguoiDung.UpdateTableNguoiDungDangNhap("trangthaihoatdong", "Đang hoạt động");
                    nguoiDung.InsertTableLichSuDangNhap(txtTaiKhoan.Text.Trim(), timeNow);
                    string strTenDN = nguoiDung.LayDuLieu(txtTaiKhoan.Text, "tendn");
                    FMain main = new FMain(strTenDN);
                    main.ShowDialog();
                    nguoiDung.UpdateTableNguoiDungDangNhap("trangthaihoatdong", "Không hoạt động");
                    this.Show();
                    txtTaiKhoan.Text = "";
                    txtMatKhau.Text = "";
                    txtTaiKhoan.Focus();
                    //    }
                    //    else
                    //    {
                    //        if (MessageBox.Show("Tài khoản '" + txtTaiKhoan.Text + "' đang hoạt động!!\n Bạn có chấp nhận đợi 30 phút??", "Thông Báo", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
                    //        {
                    //            string time = DateTime.Now.AddMinutes(30).ToString();
                    //            nguoiDung.UpdateTableNguoiDungDangNhap("checkhoatdong", time);
                    //        }
                    //        else
                    //        {
                    //            MessageBox.Show("Hãy đăng nhập tài khoản khác", "Thông Báo", MessageBoxButtons.OK, MessageBoxIcon.None);
                    //        }
                    //        txtTaiKhoan.Text = "";
                    //        txtMatKhau.Text = "";

                    //    }
                }
                else
                {
                    MessageBox.Show("Bạn đã nhập sai tài khoản hoặc mật khẩu!!!", "Thông Báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtMatKhau.Text = "";
                    txtMatKhau.Focus();
                }
                }
            //}
            //catch (Exception ex)
            //{
            //    MessageBox.Show("Lỗi : " + ex, "Thông Báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
            //}
        }

        private void FDangNhap_Load(object sender, EventArgs e)
        {
            HeThong.DangNhap_XuLy nguoiDung = new HeThong.DangNhap_XuLy();
            KetNoiSql sqlconn = new KetNoiSql();
            string time =sqlconn.LayDuLieu("select checkhoatdong from nguoidung where tendn = '"+txtTaiKhoan.Text+"'", "checkhoatdong", sqlconn.sql_data).ToString();
            string timeNow = DateTime.Now.ToString();
            if (timeNow == time)
            {
                nguoiDung.UpdateTableNguoiDungDangNhap("trangthaihoatdong", "Không hoạt động");
            }
            GoiHamSoPhut();
        }
        void GoiHamSoPhut()
        {
            KetNoiSql sqlconn = new KetNoiSql();
            sqlconn.ThucThiSQL("exec dbo.sophutyeucau", sqlconn.sql_data);
        }
        private void btnQuenMatKhau_Click(object sender, EventArgs e)
        {
            FQuenMatKhau quenMatKhau = new FQuenMatKhau();
            quenMatKhau.ShowDialog();
        }
    }
}
            