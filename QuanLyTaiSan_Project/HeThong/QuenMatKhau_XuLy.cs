using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Mail;
using System.IO;
using System.Data;
using System.Windows.Forms;
namespace QuanLyTaiSan_Project.HeThong
{
    class QuenMatKhau_XuLy
    {
        KetNoiSql sqlconn = new KetNoiSql();
        public void DuyetMailQuenMatKhau(string _MailNhan, string _TenDN, string _MatKhau)
        {
            try
            {

                KetNoiSql sqlconn = new KetNoiSql();
                MailMessage message = new MailMessage();
                message = new MailMessage();
                message.To.Add(_MailNhan);
                message.Subject = "Quên mật khẩu QLDA";
                message.From = new MailAddress("qldahethong2000@gmail.com");
                message.Body = string.Format(" Bạn vừa xác nhận lại mật khẩu của tài khoản {0}:  \nMật khẩu của bạn là : {1}", _TenDN, _MatKhau);

                SmtpClient smtp = new SmtpClient();
                smtp = new SmtpClient();
                smtp.UseDefaultCredentials = false;
                smtp.Credentials = new NetworkCredential("qldahethong2000@gmail.com", "quAng2320");
                smtp.Port = 587;
                smtp.Host = "smtp.gmail.com";
                smtp.DeliveryMethod = System.Net.Mail.SmtpDeliveryMethod.Network;
                smtp.EnableSsl = true;
                sqlconn.ThucThiSQL("update quenmatkhau set trangthaiyeucau = N'Đã duyệt' where tendn = '" + _TenDN + "'", sqlconn.sql_data);

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        public bool TuDongDuyetQuenMatKhau()
        {
            KetNoiSql sqlconn = new KetNoiSql();
            DataSet ds = sqlconn.ThucthiDataSet("select tendn,trangthaiyeucau,sophutdayeucau from quenmatkhau", sqlconn.sql_data);
            foreach (DataRow item in ds.Tables[0].Rows)
            {
                if (Convert.ToInt32(item["sophutdayeucau"].ToString()) == 2)
                {
                    return true;
                }
            }
            return false;
        }
        public void ThucThiQuenMatKhau()
        {
            KetNoiSql sqlconn = new KetNoiSql();
            DataSet ds = sqlconn.ThucthiDataSet("select tendn,trangthaiyeucau,sophutdayeucau from quenmatkhau", sqlconn.sql_data);
            foreach (DataRow item in ds.Tables[0].Rows)
            {
                if (item["trangthaiyeucau"].ToString() == "Chưa duyệt")
                {
                    string matKhau = sqlconn.LayDuLieu("select * from nguoidung where tendn = '" + item["tendn"].ToString().Trim() + "'", "mkdn", sqlconn.sql_data);
                    string mailNhan = sqlconn.LayDuLieu("select * from nguoidung where tendn = '" + item["tendn"].ToString().Trim() + "'", "gmail", sqlconn.sql_data);
                    DuyetMailQuenMatKhau(mailNhan, item["tendn"].ToString(), matKhau);
                }
            }
        }
    }
}
