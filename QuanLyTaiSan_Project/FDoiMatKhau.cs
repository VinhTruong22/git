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
    public partial class FDoiMatKhau : Form
    {
        string tendn;
        public FDoiMatKhau(string _tendn)
        {
            this.tendn = _tendn;
            InitializeComponent();
            txtTaiKhoan.Text = tendn;
        }
        void txtTrongMatKhau()
        {
            txtMatKhauCu.Text = string.Empty;
            txtMatKhauMoi.Text = string.Empty;
            txtXacNhanMatKhauMoi.Text = string.Empty;
        }
        private void btnDoiMatKhau_Click(object sender, EventArgs e)
        {
            HeThong.DoiMatKhau_XuLy doiMatKhau = new HeThong.DoiMatKhau_XuLy();
            try
            {
                string MatKhauCu = doiMatKhau.LayDuLieu(tendn, "mkdn");
                if (txtMatKhauCu.Text == string.Empty && txtMatKhauMoi.Text == string.Empty && txtXacNhanMatKhauMoi.Text == string.Empty)
                {
                    MessageBox.Show("Bạn nhập thiếu thông tin", "Thông Báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                else
                {
                    if (txtMatKhauMoi.Text.Count() <= 10 && txtXacNhanMatKhauMoi.Text.Count() <= 10)
                    {
                        if (txtXacNhanMatKhauMoi.Text.Trim() == txtMatKhauMoi.Text.Trim())
                        {
                            if (txtMatKhauCu.Text.Trim() == MatKhauCu.Trim())
                            {
                                doiMatKhau.DoiMatKhau(txtXacNhanMatKhauMoi.Text, tendn);
                                MessageBox.Show("Đổi mật khẩu thành công", "Thông Báo", MessageBoxButtons.OK);
                                txtTrongMatKhau();
                            }
                            else
                            {
                                MessageBox.Show("Mật khẩu cũ không chính xác", "Thông Báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                txtTrongMatKhau();
                            }
                        }
                        else
                        {
                            MessageBox.Show("Xác nhận mật khẩu mới không giống nhau!! mời nhập lại", "Thông Báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            txtTrongMatKhau();
                        }
                    }
                    else
                    {
                        MessageBox.Show("Mật Khẩu mới chỉ cho phép 10 kí tự", "Thông Báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    
                }
            }
            catch (Exception ex)
            {

                MessageBox.Show("Lỗi " + ex, "Thông Báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnThoat_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void FDoiMatKhau_Load(object sender, EventArgs e)
        {
        }
    }
}
