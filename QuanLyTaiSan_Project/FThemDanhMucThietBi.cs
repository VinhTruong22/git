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
    public partial class FThemDanhMucThietBi : Form
    {
        string matb, tentb, tendn, maloai, trangthai, nhasanxuat, tepdinhkem, anhdinhkem, maKho, maDonVi, phongHoc, maDonViTinh, moTa;
        decimal namSX, giaNhap, namSuDung, hanSuDung, hanBaoHanh, tiLeHaoMon;

       
        public FThemDanhMucThietBi(string _matb, string _tentb, string _tendn, decimal _namsx, string _maLoai, decimal _gianhap, string _trangThai, string _nhaSanXuat, string _tepDinhKem, string _anhDinhKem, decimal _namSuDung, decimal _hanSuDung, decimal _hanBaoHanh, decimal _tiLeHaoMon, string _maKho, string _maDonVi, string _phongHoc, string _maDonViTinh, string _moTa)
        {
            this.matb = _matb;
            this.tentb = _tentb;
            this.tendn = _tendn;
            this.namSX = _namsx;
            this.maloai = _maLoai;
            this.giaNhap = _gianhap;
            this.trangthai = _trangThai;
            this.nhasanxuat = _nhaSanXuat;
            this.tepdinhkem = _tepDinhKem;
            this.anhdinhkem = _anhDinhKem;
            this.namSuDung = _namSuDung;
            this.hanSuDung = _hanSuDung;
            this.hanBaoHanh = _hanBaoHanh;
            this.tiLeHaoMon = _tiLeHaoMon;
            this.maKho = _maKho;
            this.maDonVi = _maDonVi;
            this.phongHoc = _phongHoc;
            this.maDonViTinh = _maDonViTinh;
            this.moTa = _moTa;
            InitializeComponent();
            this.maDonVi = "QL";
        }
        
        private void btnluuthemmoi_Click(object sender, EventArgs e)
        {
            for (int i = 1; i <= numUDSoLuong.Value; i++)
            {
                ThemThietBi();
            }
            MessageBox.Show("đã thêm thiết bị " + txtNhapTenThietBi.Text + "", "Thông Báo", MessageBoxButtons.OK);
        }
        private void btnluuthoat_Click(object sender, EventArgs e)
        {
            SuaThietBi();

        }
        private void FThemDanhMucThietBi_Load(object sender, EventArgs e)
        {

            SetMacDinhcbx();
            MacDinhTruyenVao();

        }
        void MacDinhTruyenVao()
        {
            txtNhapMaThietbi.Text = matb;
            txtNhapTenThietBi.Text = tentb;
            txtNhapTenDangNhap.Text = tendn;
            numUDNamSanXuat.Value = namSX;
            cbxNhapMaLoai.SelectedIndex = cbxNhapMaLoai.FindStringExact(maloai);
            numUDGiaNhap.Value = giaNhap;
            cbxNhapTrangThai.SelectedIndex = cbxNhapTrangThai.FindStringExact(trangthai);
            txtNhapNhaSanXuat.Text = nhasanxuat;
            txtDinhKemTep.Text = tepdinhkem;
            btnimage.Text = anhdinhkem;
            numUDNamSuDung.Value = namSuDung;
            numUDThoiHanSuDung.Value = hanSuDung;
            numUDThoiGianBaoHanh.Value = hanBaoHanh;
            numUDTiLeHaoMon.Value = tiLeHaoMon;
            cbxNhapMaKho.SelectedIndex = cbxNhapMaKho.FindStringExact(maKho);
            txtNhapMaDonVi.Text = maDonVi;
            cbxNhapMaPhongHoc.SelectedIndex = cbxNhapMaPhongHoc.FindStringExact(phongHoc);
            cbxNhapMaDonViTinh.SelectedIndex = cbxNhapMaDonViTinh.FindStringExact(maDonViTinh);
            rtbMoTa.Text = moTa;
        }
        private void btnthoat_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        void SuaThietBi()
        {
            KetNoiSql sqlconn = new KetNoiSql();
            string sqltest = string.Format("select * from thietbi where matb = '{0}'", txtNhapMaThietbi.Text);
            string sql = string.Format("update thietbi set tentb = N'{0}' , maloai = '{1}', madonvitinh = '{2}', nhasx = N'{3}', anhtb = '{4}', gianhap = {5}, trangthai = N'{6}', namsx = {7}, thoigianbaohanh = {8}, thoihanhandung = {9}, nambdsudung = {10} , tylehaomon = {11}, tailieudinhkem = N'{12}', mota = N'{13}', makho = '{14}',tendn = '{15}', madonvi = '{16}', phonghoc = '{17}' where matb = '{18}'", txtNhapTenThietBi.Text, cbxNhapMaLoai.Text.Trim(), cbxNhapMaDonViTinh.Text.Trim(), txtNhapNhaSanXuat.Text, "dsds", numUDGiaNhap.Value, cbxNhapTrangThai.Text.Trim(), numUDNamSanXuat.Value, numUDThoiGianBaoHanh.Value, numUDThoiHanSuDung.Value, numUDNamSuDung.Value, numUDTiLeHaoMon.Value, txtDinhKemTep.Text, rtbMoTa.Text, cbxNhapMaKho.Text.Trim(), txtNhapTenDangNhap.Text.Trim(), txtNhapMaDonVi.Text.Trim(), cbxNhapMaPhongHoc.Text.Trim(), txtNhapMaThietbi.Text.Trim());
                if (txtNhapMaThietbi.Text != string.Empty && txtNhapTenThietBi.Text != string.Empty && cbxNhapMaLoai.Text != string.Empty && cbxNhapMaDonViTinh.Text != string.Empty && cbxNhapTrangThai.Text != string.Empty && cbxNhapMaKho.Text != string.Empty && txtNhapTenDangNhap.Text != string.Empty && txtNhapMaDonVi.Text != string.Empty && cbxNhapMaPhongHoc.Text != string.Empty)
                {
                    if ((MessageBox.Show("bạn chắc chắn có muốn cập nhật TB: " + txtNhapMaThietbi.Text + "", "Thông Báo", MessageBoxButtons.YesNo) == DialogResult.Yes))
                    {
                        if (sqlconn.mo_CSDL(sqltest,sqlconn.sql_data))
                        {
                            sqlconn.ThucThiSQL(sql, sqlconn.sql_data);
                            MessageBox.Show("đã Sửa thiết bị " + txtNhapMaThietbi.Text + "", "Thông Báo", MessageBoxButtons.OK);
                            this.Close();
                        }
                        else
                        {
                            MessageBox.Show("Mã thiết bị không tồn tại", "Thiết bị", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        }
                    }
                }
                else
                {
                    MessageBox.Show("Mời Bạn Nhập Dữ Liệu", "Thông Báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            
        }
        void ThemThietBi()
        {
            {

                KetNoiSql sqlconn = new KetNoiSql(); // 0- matb, 1 tentb,2 maloai,3 donvitinh, 4 nhasx, 5 anhtb, 6 giapnhap, 7 trangthai,8 namsx, 9 tgbaohanh,10 hạn sử dụng, 11 năm bd sử dụng, 12 tỷ lệ hao mòn, 13 tài liệu đính kèm, 14 mô tả, 15 mã kho, 16 tenvn, 17,madonvi, 18 phonghoc
                string sql = string.Format("insert into thietbi(matb,tentb,maloai,madonvitinh,nhaSX,anhtb,gianhap,trangthai,namsx,thoigianbaohanh,thoihanhandung,nambdsudung,tylehaomon,tailieudinhkem,mota,makho,tendn,madonvi,phonghoc) values (dbo.Auto_MaThietBi(),N'{0}','{1}','{2}',N'{3}','{4}',{5},N'{6}','{7}',{8},{9},{10},{11},N'{12}',N'{13}','{14}','{15}','{16}','{17}')", txtNhapTenThietBi.Text, cbxNhapMaLoai.Text.Trim(), cbxNhapMaDonViTinh.Text.Trim(), txtNhapNhaSanXuat.Text, "dsds", numUDGiaNhap.Value, cbxNhapTrangThai.Text.Trim(), numUDNamSanXuat.Value, numUDThoiGianBaoHanh.Value, numUDThoiHanSuDung.Value, numUDNamSuDung.Value, numUDTiLeHaoMon.Value, txtDinhKemTep.Text, rtbMoTa.Text, cbxNhapMaKho.Text.Trim(), txtNhapTenDangNhap.Text.Trim(), txtNhapMaDonVi.Text.Trim(), cbxNhapMaPhongHoc.Text.Trim());
                if (txtNhapTenThietBi.Text != string.Empty && cbxNhapMaLoai.Text != string.Empty && cbxNhapMaDonViTinh.Text != string.Empty && cbxNhapTrangThai.Text != string.Empty && cbxNhapMaKho.Text != string.Empty && txtNhapTenDangNhap.Text != string.Empty && txtNhapMaDonVi.Text != string.Empty && cbxNhapMaPhongHoc.Text != string.Empty)
                {
                    sqlconn.ThucThiSQL(sql, sqlconn.sql_data);
                }
                else
                {
                    MessageBox.Show("Mời Bạn Nhập Dữ Liệu", "Thông Báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            
        }
        private void BtnXoa_Click(object sender, EventArgs e)
        {
            KetNoiSql sqlconn = new KetNoiSql();
            string sqltest = string.Format("select * from thietbi where matb = N'{0}'", txtNhapMaThietbi.Text);
            if (txtNhapMaThietbi.Text != string.Empty)
            {
                if (MessageBox.Show("bạn chắc chắn có muốn xóa TB: " + txtNhapMaThietbi.Text + "", "Thông Báo", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    if (sqlconn.mo_CSDL(sqltest,sqlconn.sql_data))
                    {
                        sqlconn.ThucThiSQL("delete thietbi where matb = '" + matb + "'", sqlconn.sql_data);
                        MessageBox.Show("đã xóa thiết bị " + txtNhapMaThietbi.Text + "", "Thông Báo", MessageBoxButtons.OK);
                        this.Close();
                    }
                }
            }
            else
            {
                MessageBox.Show("Mời bạn nhập mã thiết bị để xóa", "Thông Báo", MessageBoxButtons.OK);
            }

        }



        private void btnimage_Click(object sender, EventArgs e)
        {

        }
        #region thêm mã các danh mục
        private void btnThemMaLoai_Click(object sender, EventArgs e)
        {
            FThietLapDanhMuc thietlapDanhMuc = new FThietLapDanhMuc(0);
            thietlapDanhMuc.ShowDialog();
        }

        private void btnThemMaKho_Click(object sender, EventArgs e)
        {
            FThietLapDanhMuc thietlapDanhMuc = new FThietLapDanhMuc(1);
            thietlapDanhMuc.ShowDialog();
        }

        private void btnThemMaPhongHoc_Click(object sender, EventArgs e)
        {
            FThietLapDanhMuc thietlapDanhMuc = new FThietLapDanhMuc(2);
            thietlapDanhMuc.ShowDialog();
        }

        private void BtnMaDonViTinh_Click(object sender, EventArgs e)
        {
            FThietLapDanhMuc thietlapDanhMuc = new FThietLapDanhMuc(4);
            thietlapDanhMuc.ShowDialog();
        }
        #endregion kết thúc mã các danh mục
        void SetMacDinhcbx()
        {
            KetNoiSql sqlconn = new KetNoiSql();
            // mã loại
            cbxNhapMaLoai.DataSource = sqlconn.ThucThiTable("select maloaitb from loaithietbi", sqlconn.sql_data);
            cbxNhapMaLoai.DisplayMember = "maloaitb";
            cbxNhapMaLoai.ValueMember = "maloaitb";
            // trạng thái
            cbxNhapTrangThai.Items.Add("Đang sử dụng");
            cbxNhapTrangThai.Items.Add("Không sử dụng");
            // mã kho
            cbxNhapMaKho.DataSource = sqlconn.ThucThiTable("select makho from kho", sqlconn.sql_data);
            cbxNhapMaKho.DisplayMember = "makho";
            cbxNhapMaKho.ValueMember = "makho";
            // mã phòng học
            cbxNhapMaPhongHoc.DataSource = sqlconn.ThucThiTable("select maphonghoc from phonghoc", sqlconn.sql_data);
            cbxNhapMaPhongHoc.DisplayMember = "maphonghoc";
            cbxNhapMaPhongHoc.ValueMember = "maphonghoc";
            // mã đơn vị tính
            cbxNhapMaDonViTinh.DataSource = sqlconn.ThucThiTable("select madonvitinh from donvitinh", sqlconn.sql_data);
            cbxNhapMaDonViTinh.DisplayMember = "madonvitinh";
            cbxNhapMaDonViTinh.ValueMember = "madonvitinh";

        }
    }
}