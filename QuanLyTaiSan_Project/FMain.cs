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
using System.Net;
using System.Net.Mail;
using System.IO;

namespace QuanLyTaiSan_Project
{

    public partial class FMain : Form
    {
        string tendn;
        public FMain(string _tendn)
        {

            this.tendn = _tendn;
            InitializeComponent();
            HienDtaGVThongTin();
            hienDtaGVLichSuDangNhap();
            HienDtaGVDonVi();
            LoadDtaGVPhanQuyen();
            HienDtaGvDanhMucThietBi();
            HienDtaGVQuenMatKhau();
            HienDtaGVKho();
            MacDinhDtaGvDanhMucThietBi();
            MacDinhDtaGVQuenMatKhau();
        }
        private void FMain_Load(object sender, EventArgs e)
        {
            
            LoadCbxMaDonVi();
            LoadCbxTaiKhoanLichSu();
            LoadCbxMaKho();
            LoadCbxTrangThaiDanhMucThietBi();
            LoadCbxTendnPhanQuyen();
            DongTabPageHeThong();
            
            MacDinhCbxKho();
        }
        
        #region Load ComboBox
        // load combobox của mã đơn vị trong người dùng của hệ thống
        void LoadCbxMaDonVi()
        {
            HeThong.NguoiDung_XuLy nguoiDung = new HeThong.NguoiDung_XuLy();
            cbxDonVi.DataSource = nguoiDung.LoadDonVi();
            cbxDonVi.DisplayMember = "madonvi";
            cbxDonVi.ValueMember = "madonvi";
        }
        // load combobox tên đăng nhạp trong phân quyền
        void LoadCbxTendnPhanQuyen()
        {
            KetNoiSql sqlconn = new KetNoiSql();
            cbxNhapTenDnPhanQuyen.DataSource = sqlconn.ThucThiTable("select tendn from nguoidung", sqlconn.sql_data);
            cbxNhapTenDnPhanQuyen.DisplayMember = "tendn";
            cbxNhapTenDnPhanQuyen.ValueMember = "tendn";
        }
        // load combobox tài khoản trong lịch sử đăng nhập
        void LoadCbxTaiKhoanLichSu()
        {
            HeThong.LichSuDangNhap_XuLy lichSuDangNhap = new HeThong.LichSuDangNhap_XuLy();
            cbxTaiKhoanLichSu.DataSource = lichSuDangNhap.LoadTaiKHoanLichSu();
            cbxTaiKhoanLichSu.DisplayMember = "tendn";
            cbxTaiKhoanLichSu.ValueMember = "tendn";
        }
        // load combobox trạng thái trong danh mục thiết bị của chức năng
        void LoadCbxTrangThaiDanhMucThietBi()
        {
            List<string> items = new List<string> { "Đang sử dụng", "Không sử dụng" };
            tsCBXTrangThaiDanhMucThietBi.ComboBox.DataSource = items;

        }
        // load combobox mã kho trong kho của chức năng
        void LoadCbxMaKho()
        {
            ChucNang.Kho kho = new ChucNang.Kho();
            tsCbxMaKho.ComboBox.DataSource = kho.LoadKhoCbx();
            tsCbxMaKho.ComboBox.DisplayMember = "makho";
            tsCbxMaKho.ComboBox.ValueMember = "makho";
        }

        #endregion Ket Thuc LoadComBoBox

        #region Hệ Thống

        #region button Main Hệ Thống
        private void btnDoiMatKhau_Click(object sender, EventArgs e)
        {
            FDoiMatKhau doiMatKhau = new FDoiMatKhau(tendn);
            doiMatKhau.ShowDialog();
        }

        private void FMain_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (MessageBox.Show("Bạn có muốn đăng xuất không??", "Thông Báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
            {
                e.Cancel = true;
            }
        }
        #region tab page
        // khi mới vào sẽ ẩn các trang bên hệ thống
        void DongTabPageHeThong()
        {
            this.tcThongTin.TabPages.Remove(tpNguoiDung);
            this.tcThongTin.TabPages.Remove(tPhanQuyen);
            this.tcThongTin.TabPages.Remove(tpDonVi);
            this.tcThongTin.TabPages.Remove(tpLichSu);
            this.tcThongTin.TabPages.Remove(tpQuenMatKhau);
        }

        // ấn vào button nào sẽ hiện ra những trang mà người dùng cần
        // người dùng
        private void btnNguoiDung_Click(object sender, EventArgs e)
        {
            tcThongTin.TabPages.Add(tpNguoiDung);
            tcThongTin.SelectedTab = tpNguoiDung;
            txtTrongNguoiDung();
        }
        // trang phân quyền
        private void btPhanQuyen_Click(object sender, EventArgs e)
        {
            tcThongTin.TabPages.Add(tPhanQuyen);
            tcThongTin.SelectedTab = tPhanQuyen;
        }
        // trang đơn vị
        private void btnDonVi_Click(object sender, EventArgs e)
        {
            tcThongTin.TabPages.Add(tpDonVi);
            txtTrongDonVi();
            tcThongTin.SelectedTab = tpDonVi;
        }
        // trang lịch sử đăng nhập
        private void btnLichSuDangNhap_Click(object sender, EventArgs e)
        {
            tcThongTin.TabPages.Add(tpLichSu);
            cbxTaiKhoanLichSu.Text = "";
            tcThongTin.SelectedTab = tpLichSu;
        }
        // trang duyệt quên mật khẩu
        private void btnQuenMatKhau_Click(object sender, EventArgs e)
        {
            tcThongTin.TabPages.Add(tpQuenMatKhau);

            tcThongTin.SelectedTab = tpQuenMatKhau;

        }
        //button để đóng các trang
        private void btnDongTCThongTin_Click(object sender, EventArgs e)
        {
            try
            {
                for (int i = 1; i <= tcThongTin.TabPages.Count; i++)
                {

                    if (MessageBox.Show("Bạn có muốn đóng TabPage này?", "Thông Báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    {
                        tcThongTin.TabPages.RemoveAt(i);
                        break;
                    }
                }

            }
            catch (Exception)
            {

                MessageBox.Show("Không còn gì để tắt", "Thông Báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        #endregion kết thúc tabpage

        #endregion

        #region Mặc định

        // cài đặt string trống người dùng để sử dụng lại
        void txtTrongNguoiDung()
        {
            txtTaiKhoan.Text = string.Empty;
            txtTenHienThi.Text = string.Empty;
            cbxDonVi.Text = string.Empty;
            dtpNgayTao.Value = DateTime.Now;
            txtNhapGmail.Text = string.Empty;

        }

        // cài đặt string trống đơn vị
        void txtTrongDonVi()
        {
            txtNhapMaDonVi.Text = string.Empty;
            txtNhapTenDonVi.Text = string.Empty;
            txtNhapSoDienThoaiDonVi.Text = string.Empty;
            txtNhapDiaDiemDonVi.Text = string.Empty;
        }


        #endregion

        #region button Người Dùng
        // Hiện thông tin lên datagridview
        void HienDtaGVThongTin()
        {
            KetNoiSql sqlconn = new KetNoiSql();
            dtaGVThongTin.DataSource = sqlconn.ThucThiTable("select tendn,hoten,donvi,gmail,ngaytao,ngaydangnhap,trangthaihoatdong,nguoicapnhat from nguoidung", sqlconn.sql_data);
            dtaGVThongTin.Columns[0].HeaderText = "Tên đăng nhập";
            dtaGVThongTin.Columns[1].HeaderText = "Họ và tên";
            dtaGVThongTin.Columns[2].HeaderText = "Đơn vị";
            dtaGVThongTin.Columns[3].HeaderText = "Gmail";
            dtaGVThongTin.Columns[4].HeaderText = "Ngày tạo";
            dtaGVThongTin.Columns[5].HeaderText = "Ngày đăng nhập";
            dtaGVThongTin.Columns[6].HeaderText = "Trạng thái hoạt động";
            dtaGVThongTin.Columns[7].HeaderText = "Người cập nhật";


        }
        //click datagridview Thông tin
        private void dtaGVThongTin_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                txtTaiKhoan.Text = dtaGVThongTin.Rows[e.RowIndex].Cells[0].Value.ToString();
                txtTenHienThi.Text = dtaGVThongTin.Rows[e.RowIndex].Cells[1].Value.ToString();
                cbxDonVi.Text = dtaGVThongTin.Rows[e.RowIndex].Cells[2].Value.ToString();
                txtNhapGmail.Text = dtaGVThongTin.Rows[e.RowIndex].Cells[3].Value.ToString();
                dtpNgayTao.Value = DateTime.Parse(dtaGVThongTin.Rows[e.RowIndex].Cells[4].Value.ToString());
            }
            catch (Exception)
            {

            }
        }
        // thêm người dùng  
        private void btnThem_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtTaiKhoan.Text != string.Empty && txtTenHienThi.Text != string.Empty && txtNhapGmail.Text != string.Empty && cbxDonVi.Text != string.Empty && dtpNgayTao.Value != null)
                {
                    if (MessageBox.Show("Bạn có muốn đăng ký tài khoản này???","Đăng Ký",MessageBoxButtons.YesNo,MessageBoxIcon.Question) == DialogResult.Yes)
                    {
                        HeThong.NguoiDung_XuLy nguoiDung = new HeThong.NguoiDung_XuLy();
                        dtpNgayTao.Value = DateTime.Now;
                        nguoiDung.ThemNguoiDung(txtTaiKhoan.Text, txtTenHienThi.Text, txtNhapGmail.Text, cbxDonVi.Text, dtpNgayTao.Value,tendn);
                    }
                }
                else
                {
                    MessageBox.Show("Bạn nhập thiếu thông tin", "Thông Báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                txtTrongNguoiDung();
                HienDtaGVThongTin();
            }
            catch (Exception ex)
            {

                MessageBox.Show("Lỗi : " + ex, "Thông Báo", MessageBoxButtons.OK);
            }
        }
        // sửa thông tin người dùng
        private void btnSua_Click(object sender, EventArgs e)
        {
            try
            {
                HeThong.NguoiDung_XuLy nguoiDung = new HeThong.NguoiDung_XuLy();
                KetNoiSql sqlconn = new KetNoiSql();
                int check = 2;
                string sqltest = string.Format("select * from nguoidung where tendn = '{0}'", txtTaiKhoan.Text);
                if (txtTaiKhoan.Text != string.Empty)
                {
                    if (MessageBox.Show("Bạn có muốn cập nhật??","Cập nhật người dùng",MessageBoxButtons.YesNo,MessageBoxIcon.Question) == DialogResult.Yes)
                    {

                        if (sqlconn.mo_CSDL(sqltest, sqlconn.sql_data))
                        {
                            if (txtTenHienThi.Text != string.Empty)
                            {
                                nguoiDung.SuaThongTinTungDuLieu(txtTaiKhoan.Text, "hoten", txtTenHienThi.Text, tendn);
                                check = 1;

                            }
                            if (txtNhapGmail.Text != string.Empty)
                            {
                                nguoiDung.SuaThongTinTungDuLieu(txtTaiKhoan.Text, "gmail", txtNhapGmail.Text, tendn);
                                check = 1;


                            }
                            if (txtNhapMaDonVi.Text != string.Empty)
                            {
                                nguoiDung.SuaThongTinTungDuLieu(txtTaiKhoan.Text, "donvi", cbxDonVi.Text, tendn);
                                check = 1;
                            }
                            if (check == 1)
                            {
                                MessageBox.Show("Cập nhật thành công tài khoản " + txtTaiKhoan.Text + "", "Người dùng", MessageBoxButtons.OK);
                            }
                            else
                            {
                                MessageBox.Show("Vui lòng nhập thông tin cần cập nhật", "Thông Báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            }
                        }
                        else
                        {
                            MessageBox.Show("Tài khoản không tồn tại!!!", "Cập nhật người dùng", MessageBoxButtons.OK, MessageBoxIcon.Warning);

                        }

                    }
                }
                else
                {
                    MessageBox.Show("Bạn hãy nhập tài khoản cần thay đổi thông tin", "Thông Báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }

                txtTrongNguoiDung();
                HienDtaGVThongTin();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi : " + ex, "Thông Báo", MessageBoxButtons.OK);
            }
        }
        // Tìm kiếm thông tin người dùng
        private void btnXem_Click(object sender, EventArgs e)
        {
            try
            {
                HeThong.NguoiDung_XuLy nguoiDung = new HeThong.NguoiDung_XuLy();

                if (txtTenHienThi.Text != string.Empty || txtTaiKhoan.Text != string.Empty || txtNhapGmail.Text != string.Empty || cbxDonVi.Text != string.Empty)
                {
                    dtaGVThongTin.DataSource = nguoiDung.TimKiemThongTin(txtTaiKhoan.Text.Trim(), txtTenHienThi.Text.Trim(), txtNhapGmail.Text, cbxDonVi.Text);
                }   
                else
                {
                    MessageBox.Show("Hãy nhập thông tin cần tìm kiếm", "Thông Báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }


            }
            catch (Exception ex)
            {

                MessageBox.Show("Lỗi : " + ex, "Thông Báo", MessageBoxButtons.OK);
            }
        }
        // xóa thông tin người dùng
        private void btnXoa_Click(object sender, EventArgs e)
        {
            try
            {
                HeThong.NguoiDung_XuLy nguoiDung = new HeThong.NguoiDung_XuLy();
                if (txtTaiKhoan.Text != string.Empty && txtTenHienThi.Text != string.Empty && cbxDonVi.Text != string.Empty)
                {
                    if (MessageBox.Show("Bạn có chắc chắn muốn xóa tài khoản "+txtTaiKhoan.Text+"","Xóa người dùng",MessageBoxButtons.YesNo,MessageBoxIcon.Question) == DialogResult.Yes)
                    {
                        nguoiDung.XoaThongTin(txtTaiKhoan.Text, txtTenHienThi.Text, cbxDonVi.Text);
                        MessageBox.Show("Đã xóa Tài Khoản '" + txtTaiKhoan.Text + "'", "Thông Báo", MessageBoxButtons.OK);
                    }
                    
                }
                else
                {
                    MessageBox.Show("Bạn hãy nhập đủ thông tin cần xóa", "Thông Báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                txtTrongNguoiDung();
                HienDtaGVThongTin();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi : " + ex, "Thông Báo", MessageBoxButtons.OK);
            }
        }
        // load lại thông tin người dùng
        private void btnLoad_Click(object sender, EventArgs e)
        {
            txtTrongNguoiDung();
            HienDtaGVThongTin();
        }
        #endregion kết thúc button

        #region Phân Quyền

        void LoadDtaGVPhanQuyen()
        {
            DataGridViewTextBoxColumn dtagvTextBox = new DataGridViewTextBoxColumn();
            dtagvTextBox.HeaderText = "Tên phân quyền";
            DataGridViewCheckBoxColumn dgvcCheckBox = new DataGridViewCheckBoxColumn();
            dgvcCheckBox.HeaderText = "Select";
            dtaGVPhanQuyen.Columns.Add(dtagvTextBox);
            dtaGVPhanQuyen.Columns.Add(dgvcCheckBox);
            dtaGVPhanQuyen.Rows.Add("Hệ thống", false);
            dtaGVPhanQuyen.Rows.Add("Người dùng", false);
            dtaGVPhanQuyen.Rows.Add("Đơn vị", false);
            dtaGVPhanQuyen.Rows.Add("Phân quyền", false);
            dtaGVPhanQuyen.Rows.Add("Lịch sử đăng nhập", false);
            dtaGVPhanQuyen.Rows.Add("Quên mật khẩu", false);
            dtaGVPhanQuyen.Rows.Add("Chức năng", false);
            dtaGVPhanQuyen.Rows.Add("Bàn làm việc", false);
            dtaGVPhanQuyen.Rows.Add("Danh mục", true);
            dtaGVPhanQuyen.Rows.Add("Danh mục tài sản", false);
            dtaGVPhanQuyen.Rows.Add("Giao nhận", false);
            dtaGVPhanQuyen.Rows.Add("Kho", false);
            dtaGVPhanQuyen.Rows.Add("Kiếm kê tài sản", false);
            dtaGVPhanQuyen.Rows.Add("Thiết lập danh mục", false);
            dtaGVPhanQuyen.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dtaGVPhanQuyen.AllowUserToAddRows = false;
        }

        #endregion kết thúc Phân Quyền

        #region Đơn Vị

        private void dtaGVDonVi_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            txtNhapMaDonVi.Text = dtaGVDonVi.Rows[e.RowIndex].Cells[0].Value.ToString();
            txtNhapTenDonVi.Text = dtaGVDonVi.Rows[e.RowIndex].Cells[1].Value.ToString();
            txtNhapSoDienThoaiDonVi.Text = dtaGVDonVi.Rows[e.RowIndex].Cells[2].Value.ToString();
            txtNhapDiaDiemDonVi.Text = dtaGVDonVi.Rows[e.RowIndex].Cells[3].Value.ToString();
        }
        private void txtNhapSoDienThoai_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!Char.IsDigit(e.KeyChar) && (!char.IsControl(e.KeyChar)))
            {
                e.Handled = true;

            }
        }
        // hiện thông tin đơn vị
        void HienDtaGVDonVi()
        {
            KetNoiSql sqlconn = new KetNoiSql();
            dtaGVDonVi.DataSource = sqlconn.ThucThiTable("select * from donvi", sqlconn.sql_data);
            dtaGVDonVi.Columns[0].HeaderText = "Mã đơn vị";
            dtaGVDonVi.Columns[1].HeaderText = "Tên đơn vị";
            dtaGVDonVi.Columns[2].HeaderText = "Số điện thoại";
            dtaGVDonVi.Columns[3].HeaderText = "Địa điểm";
        }
        // thêm đơn vị
        private void btnThemDonVi_Click(object sender, EventArgs e)
        {
            try
            {
                HeThong.DonVi_XuLy donVi = new HeThong.DonVi_XuLy();
                if (txtNhapMaDonVi.Text != string.Empty && txtNhapTenDonVi.Text != string.Empty && txtNhapSoDienThoaiDonVi.Text != string.Empty && txtNhapDiaDiemDonVi.Text != string.Empty)
                {
                    donVi.ThemDonVi(txtNhapMaDonVi.Text, txtNhapTenDonVi.Text, txtNhapSoDienThoaiDonVi.Text, txtNhapDiaDiemDonVi.Text, tendn);
                }
                else
                {
                    MessageBox.Show("Bạn nhập thiếu thông tin", "Thông Báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                txtTrongDonVi();
                HienDtaGVDonVi();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi : " + ex, "Thông Báo", MessageBoxButtons.OK);
            }
        }
        // sửa đơn vị
        private void btnSuaDonVi_Click(object sender, EventArgs e)
        {
            try
            {
                HeThong.DonVi_XuLy donVi = new HeThong.DonVi_XuLy();
                KetNoiSql sqlconn = new KetNoiSql();
                int check = 2;
                string sqltest = string.Format("select * from donvi where madonvi = '{0}'", txtNhapMaDonVi.Text);
                if (txtNhapMaDonVi.Text != string.Empty)
                {
                    if (MessageBox.Show("Bạn có muốn cập nhật??","Đơn vị",MessageBoxButtons.YesNo,MessageBoxIcon.Question) == DialogResult.Yes)
                    {
                        if (sqlconn.mo_CSDL(sqltest,sqlconn.sql_data))
                        {
                            if (txtTenDonVi.Text != string.Empty)
                            {
                                donVi.SuaDonViTungDuLieu(txtNhapMaDonVi.Text, "tendonvi", txtNhapTenDonVi.Text, tendn);
                                check = 1;
                            }
                            if (txtNhapSoDienThoaiDonVi.Text != string.Empty)
                            {
                                donVi.SuaDonViTungDuLieu(txtNhapMaDonVi.Text, "dienthoai", txtNhapSoDienThoaiDonVi.Text, tendn);
                                check = 1;
                            }
                            if (txtNhapDiaDiemDonVi.Text != string.Empty)
                            {
                                donVi.SuaDonViTungDuLieu(txtNhapMaDonVi.Text, "diadiem", txtNhapDiaDiemDonVi.Text, tendn);
                                check = 1;
                            }
                            if (check == 1)
                            {
                                MessageBox.Show("Cập nhật thành công Đơn Vị " + txtNhapMaDonVi.Text + "", "Đơn vị", MessageBoxButtons.OK);
                            }
                            else
                            {
                                MessageBox.Show("Vui lòng nhập thông tin cần cập nhật", "Thông Báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            }
                        }
                        else
                        {
                            MessageBox.Show("Mã đơn vị không tồn tại", "Đơn vị", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        }
                    }
                }
                else
                {
                    MessageBox.Show("Bạn hãy nhập \"mã đơn vị\" cần thay đổi thông tin", "Thông Báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                txtTrongDonVi();
                HienDtaGVDonVi();

            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi : " + ex, "Thông Báo", MessageBoxButtons.OK);
            }
        }
        //tìm kiếm đơn vị
        private void btnTimKiemDonVi_Click(object sender, EventArgs e)
        {
            try
            {
                HeThong.DonVi_XuLy donVi = new HeThong.DonVi_XuLy();
                if (txtNhapMaDonVi.Text != string.Empty || txtNhapTenDonVi.Text != string.Empty || txtNhapSoDienThoaiDonVi.Text != string.Empty || txtNhapDiaDiemDonVi.Text != string.Empty)
                {
                    dtaGVDonVi.DataSource = donVi.TimKiemToanBoDonVi(txtNhapMaDonVi.Text, txtNhapTenDonVi.Text, txtNhapSoDienThoaiDonVi.Text, txtNhapDiaDiemDonVi.Text);

                }
                else
                {
                    MessageBox.Show("Hãy nhập thông tin đơn vị cần tìm kiếm", "Thông Báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            catch (Exception ex)
            {

                MessageBox.Show("Lỗi : " + ex, "Thông Báo", MessageBoxButtons.OK);
            }
        }
        // xóa đơn vị
        private void btnXoaDonVi_Click(object sender, EventArgs e)
        {
            try
            {
                HeThong.DonVi_XuLy donVi = new HeThong.DonVi_XuLy();
                if (txtNhapMaDonVi.Text != string.Empty)
                {
                    if (MessageBox.Show("Bạn có chắc chắn muốn xóa đơn vị này??","Thông Báo",MessageBoxButtons.YesNo,MessageBoxIcon.Question) == DialogResult.Yes)
                    {
                        donVi.XoaDonvi(txtMaDonVi.Text);
                        MessageBox.Show("Đã xóa đơn vị \"" + txtMaDonVi.Text + "\"'", "Thông Báo", MessageBoxButtons.OK);
                    }
                }
                else
                {
                    MessageBox.Show("Bạn hãy nhập đủ thông tin cần xóa", "Thông Báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                txtTrongNguoiDung();
                HienDtaGVThongTin();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi : " + ex, "Thông Báo", MessageBoxButtons.OK);
            }
        }
        // load lại đơn vị
        private void btnLoadDonVi_Click(object sender, EventArgs e)
        {
            txtTrongDonVi();
            HienDtaGVDonVi();
        }
        #endregion

        #region lịch sử đăng nhập
        // hiện thông tin lịch sử đăng nhập
        void hienDtaGVLichSuDangNhap()
        {
            KetNoiSql sqlconn = new KetNoiSql();
            dtaGVLichSuDangNhap.DataSource = sqlconn.ThucThiTable("select tendn,ngaydangnhap from lichsudangnhap", sqlconn.sql_data);
            dtaGVLichSuDangNhap.Columns[0].HeaderText = "Tên đăng nhập";
            dtaGVLichSuDangNhap.Columns[0].HeaderText = "Ngày đăng nhập";
        }
        // button tìm kiếm lịch sử đăng nhập theo Tài khoản
        private void btnXemLichSu_Click(object sender, EventArgs e)
        {
            try
            {
                if (cbxTaiKhoanLichSu.Text.Count() <= 10)
                {
                    HeThong.LichSuDangNhap_XuLy LichSu = new HeThong.LichSuDangNhap_XuLy();
                    dtaGVLichSuDangNhap.DataSource = LichSu.HienLichSu(cbxTaiKhoanLichSu.Text);
                }
                else
                {
                    MessageBox.Show("Tài Khoản chỉ được 10 kí tự", "Thông Báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi : " + ex, "Thông Báo", MessageBoxButtons.OK);
            }
        }






        #endregion kết thúc lịch sử đăng nhập

        #region Quên mật khẩu
        void GoiHamSoPhut()
        {
            KetNoiSql sqlconn = new KetNoiSql();
            sqlconn.ThucThiSQL("exec dbo.sophutyeucau", sqlconn.sql_data);
        }
        private void timQuenMatKhau_Tick(object sender, EventArgs e)
        {
            TuDongDuyetMailQuenMatKhau();
            HeThong.QuenMatKhau_XuLy quenMatKhau = new HeThong.QuenMatKhau_XuLy();
            if (quenMatKhau.TuDongDuyetQuenMatKhau())
            {
                quenMatKhau.ThucThiQuenMatKhau();

            }
        }
        private void btnLoadBangQuenMatKhau_Click(object sender, EventArgs e)
        {
            DataTable table = new DataTable();
        }
        void TuDongDuyetMailQuenMatKhau()
        {
            HienDtaGVQuenMatKhau();
            GoiHamSoPhut();

        }
        void TuDongDuyetMail()
        {
            
        }
        
        void HienDtaGVQuenMatKhau()
        {
            KetNoiSql sqlconn = new KetNoiSql();
            dtaGVQuenMatKhau.DataSource = sqlconn.ThucThiTable("select * from quenmatkhau", sqlconn.sql_data);
        }
        void MacDinhDtaGVQuenMatKhau()
        {
            DataGridViewButtonColumn btnQuenMatKhau = new DataGridViewButtonColumn();
            btnQuenMatKhau.Text = "Duyệt";
            btnQuenMatKhau.Name = "dtaGVBtnQuenMatKhau";
            btnQuenMatKhau.HeaderText = " ";
            btnQuenMatKhau.UseColumnTextForButtonValue = true;
            dtaGVQuenMatKhau.Columns.Insert(7, btnQuenMatKhau);
            dtaGVQuenMatKhau.Columns[0].HeaderText = "Số Thứ Tự";
            dtaGVQuenMatKhau.Columns[1].HeaderText = "Tên đăng nhập";
            dtaGVQuenMatKhau.Columns[2].HeaderText = "Gmail";
            dtaGVQuenMatKhau.Columns[3].HeaderText = "Nội dung yêu cầu";
            dtaGVQuenMatKhau.Columns[4].HeaderText = "Ngày - Giờ yêu cầu";
            dtaGVQuenMatKhau.Columns[5].HeaderText = "Số phút đã yêu cầu";
            dtaGVQuenMatKhau.Columns[6].HeaderText = "Trạng thái yêu cầu";

        }
        private void dtaGVQuenMatKhau_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            KetNoiSql sqlconn = new KetNoiSql();
            HeThong.QuenMatKhau_XuLy quenMatKhau = new HeThong.QuenMatKhau_XuLy();
            try
            {
                string tenDN = dtaGVQuenMatKhau.Rows[e.RowIndex].Cells[2].Value.ToString();
                try
                {
                    string trangthai = dtaGVQuenMatKhau.Rows[e.RowIndex].Cells[7].Value.ToString();
                    string mailNhan = sqlconn.LayDuLieu("select * from nguoidung where tendn = '" + tenDN + "'", "gmail", sqlconn.sql_data);
                    string matKhau = sqlconn.LayDuLieu("select * from nguoidung where tendn = '" + tenDN + "'", "mkdn", sqlconn.sql_data);
                    if (dtaGVQuenMatKhau.Columns[e.ColumnIndex] is DataGridViewButtonColumn && e.RowIndex >= 0)
                    {
                        if (trangthai == "Chưa duyệt")
                        {
                            MessageBox.Show(trangthai);
                            MessageBox.Show(tenDN);
                            MessageBox.Show(matKhau);
                            MessageBox.Show(mailNhan);
                            quenMatKhau.DuyetMailQuenMatKhau(mailNhan, tenDN, matKhau);
                            
                            MessageBox.Show("Đã gửi tin nhắn thành công", "Thông báo", MessageBoxButtons.OK);
                        }
                        else
                        {
                            MessageBox.Show("Tài Khoản này đã duyệt", "Thông báo", MessageBoxButtons.OK);
                        }

                    }
                }

                catch (Exception ex)

                {

                    MessageBox.Show(ex.Message);
                }

            }
            catch (Exception)
            {
            }

        }

        #endregion kết thúc quên mật khẩu

        #endregion Kết thúc hệ thống

        #region Chức Năng

        #region button Main_Chức Năng

        private void btnThietLapDanhMuc_Click(object sender, EventArgs e)
        {

            FThietLapDanhMuc thietLapDanhMuc = new FThietLapDanhMuc(0);
            thietLapDanhMuc.ShowDialog();
        }
        #endregion kết thúc button Main Chức Năng

        #region DanhMucThietBi
        void HienDtaGvDanhMucThietBi()
        {
            KetNoiSql sqlconn = new KetNoiSql();
            string sql = string.Format("select matb,tentb, madonvitinh, namsx, nambdsudung, maloai, trangthai,gianhap from thietbi");
            dtaGVDanhMucThietBi.DataSource = sqlconn.ThucThiTable(sql, sqlconn.sql_data);

        }

        void MacDinhCbxKho()
        {
            KetNoiSql sqlconn = new KetNoiSql();
            string sql = string.Format("select * from kho");
            tsCbxMaKho.ComboBox.DataSource = sqlconn.ThucThiTable(sql, sqlconn.sql_data);
            tsCbxMaKho.ComboBox.DisplayMember = "makho";
            tsCbxMaKho.ComboBox.ValueMember = "makho";
        }
        void MacDinhDtaGvDanhMucThietBi()
        {
            DataGridViewButtonColumn btnThietBi = new DataGridViewButtonColumn();
            btnThietBi.Text = "Xem thiết bị";
            btnThietBi.Name = "dtaGVbtnThietBi";
            btnThietBi.HeaderText = " ";
            btnThietBi.UseColumnTextForButtonValue = true;
            dtaGVDanhMucThietBi.Columns.Insert(8, btnThietBi);
            dtaGVDanhMucThietBi.Columns[0].HeaderText = "Mã thiết bị";
            dtaGVDanhMucThietBi.Columns[1].HeaderText = "Tên thiết bị";
            dtaGVDanhMucThietBi.Columns[2].HeaderText = "Mã đơn vị tính";
            dtaGVDanhMucThietBi.Columns[3].HeaderText = "Năm sản xuất";
            dtaGVDanhMucThietBi.Columns[4].HeaderText = "Năm sử dụng";
            dtaGVDanhMucThietBi.Columns[5].HeaderText = "Mã loại";
            dtaGVDanhMucThietBi.Columns[6].HeaderText = "Trạng thái";
            dtaGVDanhMucThietBi.Columns[7].HeaderText = "Giá nhập";
        }
        private void tSBtnThemThietBi_Click(object sender, EventArgs e)
        {
            FThemDanhMucThietBi danhMucThietbi = new FThemDanhMucThietBi("", "", tendn, 2021, "", 1000000, "Đang sử dụng", "", "", "", DateTime.Now.Year, 24, 6, 0, "", "", "", "", "");
            danhMucThietbi.ShowDialog();
            HienDtaGvDanhMucThietBi();
        }
        private void btnLoad_Click_1(object sender, EventArgs e)
        {
            HienDtaGvDanhMucThietBi();
        }
        private void tSBtnTimKiemThietLapDanhMuc_Click(object sender, EventArgs e)
        {
            KetNoiSql sqlconn = new KetNoiSql();
            dtaGVDanhMucThietBi.DataSource = sqlconn.ThucThiTable("select matb,tentb, madonvitinh, namsx, nambdsudung, maloai, trangthai,gianhap from thietbi where trangthai = N'" + tsCBXTrangThaiDanhMucThietBi.Text + "'", sqlconn.sql_data);
        }

        private void dtaGVDanhMucThietBi_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            ChucNang.DanhMucThietBi danhMucThietBi = new ChucNang.DanhMucThietBi();
            string matb = dtaGVDanhMucThietBi.Rows[e.RowIndex].Cells[1].Value.ToString();
            string tentb = danhMucThietBi.LayDuLieuStr(matb, "tentb");
            string tendn = danhMucThietBi.LayDuLieuStr(matb, "tendn");
            decimal namsx = danhMucThietBi.LayDuLieuDec(matb, "namsx");
            string maLoai = danhMucThietBi.LayDuLieuStr(matb, "maloai");
            decimal giaNhap = danhMucThietBi.LayDuLieuDec(matb, "gianhap");
            string trangThai = danhMucThietBi.LayDuLieuStr(matb, "trangthai");
            string nhaSX = danhMucThietBi.LayDuLieuStr(matb, "nhaSX");
            string tepDinhKem = danhMucThietBi.LayDuLieuStr(matb, "tailieudinhkem");
            string anhDinhKem = danhMucThietBi.LayDuLieuStr(matb, "anhtb");
            decimal namSuDung = danhMucThietBi.LayDuLieuDec(matb, "nambdsudung");
            decimal hanSuDung = danhMucThietBi.LayDuLieuDec(matb, "thoihanhandung");
            decimal hanBaoHanh = danhMucThietBi.LayDuLieuDec(matb, "thoigianbaohanh");
            decimal tiLeHaoMon = danhMucThietBi.LayDuLieuDec(matb, "tylehaomon");
            string maKho = danhMucThietBi.LayDuLieuStr(matb, "makho");
            string maDonVi = danhMucThietBi.LayDuLieuStr(matb, "madonvi");
            string phongHoc = danhMucThietBi.LayDuLieuStr(matb, "phonghoc");
            string maDonViTinh = danhMucThietBi.LayDuLieuStr(matb, "madonvitinh");
            string moTa = danhMucThietBi.LayDuLieuStr(matb, "mota");


            if (dtaGVDanhMucThietBi.Columns[e.ColumnIndex] is DataGridViewButtonColumn && e.RowIndex >= 0)
            {
                FThemDanhMucThietBi themDanhMuc = new FThemDanhMucThietBi(matb, tentb, tendn, namsx, maLoai, giaNhap, trangThai, nhaSX, tepDinhKem, anhDinhKem, namSuDung, hanSuDung, hanBaoHanh, tiLeHaoMon, maKho, maDonVi, phongHoc, maDonViTinh, moTa);
                themDanhMuc.ShowDialog();
                HienDtaGvDanhMucThietBi();
            }

        }







        #endregion KetThucDanhMucThietBi

        #region Kho
        void HienDtaGVKho()
        {
            try
            {
                KetNoiSql sqlconn = new KetNoiSql();
                dtaGVKho.DataSource = sqlconn.ThucThiTable("select * from thietbi where makho ='" + tsCbxMaKho.ComboBox.Text + "'", sqlconn.sql_data);

            }
            catch (Exception)
            {
            }
        }
        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            HienDtaGVKho();
        }






        #endregion kết thúc kho

        #endregion Kết thúc Chức Năng

        #region những cái tào lao chưa gắn vô 
        private void dtaGVPhanQuyen_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            for (int i = 0; i < dtaGVPhanQuyen.Rows.Count; i++)
            {
                if (dtaGVPhanQuyen.Rows[i].Cells[1].Value.ToString() == "True")
                {
                    MessageBox.Show("dsds");
                    break;
                }
                else
                {
                    MessageBox.Show("3232");
                    break;
                }
            }
        }

        private void tsCbxMaKho_Click(object sender, EventArgs e)
        {

        }

        private void tSBtnDeXuatPhieu_Click(object sender, EventArgs e)
        {
            FXetDuyetPhieu xetDuyetPhieu = new FXetDuyetPhieu("Đề xuất",tendn);
            xetDuyetPhieu.ShowDialog();
        }

        private void btnXetDuyetDeXuat_Click(object sender, EventArgs e)
        {
            FXetDuyetPhieu xetDuyetPhieu = new FXetDuyetPhieu("Xét duyệt", tendn);
            xetDuyetPhieu.ShowDialog();
        }

        private void btnDangXuat_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        #endregion kết thúc những cái tào lao

    }
}
