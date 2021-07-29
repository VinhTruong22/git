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
    public partial class FThietLapDanhMuc : Form
    {
        int chiMuc;
        public FThietLapDanhMuc(int _chiMuc)
        {
            this.chiMuc = _chiMuc;
            InitializeComponent();
            LoadDtaGVLoaiThietBi();
            LoadDtaGVKho();
            LoadDtaGVPhongHoc();
            LoadDtaGVLoaiPhieu();
            LoadDtaGVDonViTinh();
        }

        #region MacDinh
        void txtTrongLoaiThietBi()
        {
            txtNhapTenLoaiThietBi.Text = string.Empty;
            txtNhapMaLoaiThietBi.Text = string.Empty;
        }
        void txtTrongKho()
        {
            txtNhapMaKho.Text = string.Empty;
            txtNhapTenKho.Text = string.Empty;
            txtNhapDiaChiKho.Text = string.Empty;
        }
        void txtTrongPhongHoc()
        {
            txtNhapMaPhongHoc.Text = string.Empty;
            txtNhapTenPhongHoc.Text = string.Empty;
            txtNhapDiaDiemPhongHoc.Text = string.Empty;
        }
        void txtTrongLoaiPhieu()
        {
            txtNhapMaLoaiPhieu.Text = string.Empty;
            txtNhapTenLoaiPhieu.Text = string.Empty;
        }
        void txtTrongDonViTinh()
        {
            txtNhapMaDonViTinh.Text = string.Empty;
            txtNhapTenDonViTinh.Text = string.Empty;
        }
        #endregion KetThucMacDinh

        #region LoaiThietBi

        // load DtaGVLoaiThietBi
        void LoadDtaGVLoaiThietBi()
        {
            KetNoiSql sqlconn = new KetNoiSql();
            dtaGVLoaiThietBi.DataSource = sqlconn.ThucThiTable("select * from loaithietbi", sqlconn.sql_data);
        }

        // thêm loại thiết bị
        private void btnThemLoaiThietBi_Click(object sender, EventArgs e)
        {
            ThietLapDanhMuc.LoaiThietBi loaiThietBi = new ThietLapDanhMuc.LoaiThietBi();
            try
            {
                if (txtNhapMaLoaiThietBi.Text != string.Empty && txtNhapTenLoaiThietBi.Text != string.Empty)
                {
                    if (MessageBox.Show("Bạn có chắc muốn thêm loại thiêt bị này??", "Loại thiết bị", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    {
                        loaiThietBi.ThemLoaiThietBi(txtNhapMaLoaiThietBi.Text, txtNhapTenLoaiThietBi.Text);
                    }
                }
                else
                {
                    MessageBox.Show("Vui lòng nhập thông tin", "Thông Báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            txtTrongLoaiThietBi();
            LoadDtaGVLoaiThietBi();
        }


        // sửa loại thiết bị
        private void btnSuaLoaiThietBi_Click(object sender, EventArgs e)
        {
            ThietLapDanhMuc.LoaiThietBi loaiThietBi = new ThietLapDanhMuc.LoaiThietBi();
            KetNoiSql sqlconn = new KetNoiSql();
            string sqltest = string.Format("select * from loaithietbi where maloaitb = N'{0}'", txtNhapMaLoaiThietBi.Text);
            int check = 2;
            if (MessageBox.Show("Bạn có chắc muốn cập nhật loại thiết bị này??","Loại thiết bị",MessageBoxButtons.YesNo,MessageBoxIcon.Question) == DialogResult.Yes)
            {
                if (txtNhapMaLoaiThietBi.Text != string.Empty)
                {
                    if (sqlconn.mo_CSDL(sqltest, sqlconn.sql_data))
                    {
                        if (txtNhapTenLoaiPhieu.Text != string.Empty)
                        {
                            loaiThietBi.SuaLoaiThietBi("tenloaitb", txtNhapTenLoaiThietBi.Text, txtNhapMaLoaiThietBi.Text);
                            check = 1;
                        }
                        if (check == 1)
                        {
                            MessageBox.Show("Bạn cập nhật thành công loại thiết bị " + txtNhapMaLoaiThietBi.Text + "", "Loại thiết bị", MessageBoxButtons.OK);
                        }
                        else
                        {
                            MessageBox.Show("Vui lòng nhập thông tin để cập nhật", "Thông Báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        }
                    }
                    else
                    {
                        MessageBox.Show("Mã loại thiết bị không tồn tại!!", "Loại thiết bị", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                }
                else
                {
                    MessageBox.Show("Vui lòng nhập mã loại thiết bị để cập nhật", "Thông Báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            
            txtTrongLoaiThietBi();
            LoadDtaGVLoaiThietBi();
        }

        // Tìm Kiếm Loại thiết bị
        private void btnTimKiemLoaiThietBi_Click(object sender, EventArgs e)
        {
            ThietLapDanhMuc.LoaiThietBi loaiThietBi = new ThietLapDanhMuc.LoaiThietBi();
            if (txtNhapMaLoaiThietBi.Text != string.Empty || txtNhapTenLoaiThietBi.Text != string.Empty)
            {        
                    dtaGVLoaiThietBi.DataSource = loaiThietBi.TimKiemLoaiThietBi(txtNhapMaLoaiThietBi.Text, txtNhapTenLoaiThietBi.Text);
            }
            else
            {
                MessageBox.Show("Vui lòng nhập thông tin", "Thông Báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }

        }

        // xóa loại thiết bị
        private void btnXoaLoaiThietBi_Click(object sender, EventArgs e)
        {
            ThietLapDanhMuc.LoaiThietBi loaiThietBi = new ThietLapDanhMuc.LoaiThietBi();
            if (txtNhapMaLoaiThietBi.Text != string.Empty)
            {
                if (MessageBox.Show("Bạn có chắc chắn muốn xóa loại thiết bị này??","Loại thiết bị",MessageBoxButtons.YesNo,MessageBoxIcon.Warning) == DialogResult.Yes)
                {
                    loaiThietBi.XoaLoaiThietBi(txtNhapMaLoaiThietBi.Text);
                }    
            }
            else
            {
                MessageBox.Show("Vui lòng nhập Mã Loại Thiết bị cần xóa", "Thông Báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            txtTrongLoaiThietBi();
            LoadDtaGVLoaiThietBi();
        }

        // load lại thông tin loại thiết bị
        private void btnLoadLoaiThietBi_Click(object sender, EventArgs e)
        {
            txtTrongLoaiThietBi();
            LoadDtaGVLoaiThietBi();
        }

        #endregion KetThucLoaiThietbi

        #region Kho
        void LoadDtaGVKho()
        {
            KetNoiSql sqlconn = new KetNoiSql();
            dtaGVKho.DataSource = sqlconn.ThucThiTable("select * from kho", sqlconn.sql_data);
        }

        //thêm kho
        private void btnThemKho_Click(object sender, EventArgs e)
        {
            ThietLapDanhMuc.Kho kho = new ThietLapDanhMuc.Kho();
            if (txtNhapMaKho.Text != string.Empty && txtNhapTenKho.Text != string.Empty && txtNhapDiaChiKho.Text != string.Empty)
            {
                if (MessageBox.Show("Bạn có chắc chắn muốn thêm kho??","Kho",MessageBoxButtons.YesNo,MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    kho.ThemKho(txtNhapMaKho.Text, txtNhapTenKho.Text, txtNhapDiaChiKho.Text);
                }   
            }
            else
            {
                MessageBox.Show("Vui lòng nhập thông tin", "Thông Báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            txtTrongKho();
            LoadDtaGVKho();
        }

        // sửa kho
        private void btnSuaKho_Click(object sender, EventArgs e)
        {
            ThietLapDanhMuc.Kho kho = new ThietLapDanhMuc.Kho();
            KetNoiSql sqlconn = new KetNoiSql();
            string sqltest = string.Format("select * from kho where makho = N'{0}'",txtNhapMaKho.Text);
            int check = 2;
            if (MessageBox.Show("bạn có chắc chắn muốn cập nhật kho này??","Thông báo",MessageBoxButtons.YesNo,MessageBoxIcon.Warning) == DialogResult.Yes)
            {
                if (txtNhapMaKho.Text != string.Empty)
                {
                    if (sqlconn.mo_CSDL(sqltest,sqlconn.sql_data))
                    {
                        if (txtNhapTenKho.Text != string.Empty)
                        {
                            kho.SuaKhoTungDuLieu("tenkho", txtNhapTenKho.Text, txtNhapMaKho.Text);
                            check = 1;
                        }
                        if (txtNhapDiaChiKho.Text != string.Empty)
                        {
                            kho.SuaKhoTungDuLieu("diachikho", txtNhapDiaChiKho.Text, txtNhapMaKho.Text);
                            check = 1;
                        }
                        if (check == 1)
                        {
                            MessageBox.Show("Cập nhật thành công Kho " + txtNhapMaKho.Text + "", "Kho", MessageBoxButtons.OK);
                        }
                        else
                        {
                            MessageBox.Show("Vui lòng nhập thông tin cần cập nhật", "Thông Báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        }
                    }
                    else
                    {
                        MessageBox.Show("Mã kho không tồn tại", "Kho", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                }
                else
                {
                    MessageBox.Show("Vui lòng nhập mã kho để cập nhật", "Kho", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            txtTrongKho();
            LoadDtaGVKho();
        }
        
        // tìm kiếm kho
        private void btnTimKiemKho_Click(object sender, EventArgs e)
        {
            ThietLapDanhMuc.Kho kho = new ThietLapDanhMuc.Kho();
            if (txtNhapMaKho.Text != string.Empty || txtNhapTenKho.Text != string.Empty || txtNhapDiaChiKho.Text != string.Empty)
            {
                    dtaGVKho.DataSource = kho.TimKiemKho(txtNhapMaKho.Text, txtNhapTenKho.Text, txtNhapDiaChiKho.Text);
            }
            else
            {
                MessageBox.Show("Vui lòng nhập thông tin", "Thông Báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
        // xóa kho
        private void btnXoaKho_Click(object sender, EventArgs e)
        {
            try
            {

                ThietLapDanhMuc.Kho kho = new ThietLapDanhMuc.Kho();

                if (txtNhapMaKho.Text != string.Empty)
                {
                    if (MessageBox.Show("Bạn có chắc chắc muốn xóa kho này??", "Kho", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    {
                        kho.XoaKho(txtNhapMaKho.Text);
                    }

                }
                else
                {
                    MessageBox.Show("Vui lòng nhập Mã Kho cần xóa", "Thông Báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                LoadDtaGVKho();
                txtTrongKho();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        // load kho
        private void btnLoadKho_Click(object sender, EventArgs e)
        {
            LoadDtaGVKho();
            txtTrongKho();
        }

        #endregion kết thúc kho

        #region Phòng Học
        //LoadDtaGVPhongHoc
        void LoadDtaGVPhongHoc()
        {
            KetNoiSql sqlconn = new KetNoiSql();
            dtaGVPhongHoc.DataSource = sqlconn.ThucThiTable("select * from phonghoc", sqlconn.sql_data);
        }
        // thêm phòng học
        private void btnThemPhongHoc_Click(object sender, EventArgs e)
        {
            ThietLapDanhMuc.PhongHoc phongHoc = new ThietLapDanhMuc.PhongHoc();
            if (txtNhapMaPhongHoc.Text != string.Empty && txtNhapTenPhongHoc.Text != string.Empty && txtNhapDiaDiemPhongHoc.Text != string.Empty)
            {
                    phongHoc.ThemPhongHoc(txtNhapMaPhongHoc.Text, txtNhapTenPhongHoc.Text, txtNhapDiaDiemPhongHoc.Text);
            }
            else
            {
                MessageBox.Show("Vui lòng nhập thông tin", "Thông Báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            txtTrongPhongHoc();
            LoadDtaGVPhongHoc();
        }

        // sửa phòng học
        private void btnSuaPhongHoc_Click(object sender, EventArgs e)
        {
            ThietLapDanhMuc.PhongHoc phongHoc = new ThietLapDanhMuc.PhongHoc();
            KetNoiSql sqlconn = new KetNoiSql();
            string sqltest = string.Format("select * from phonghoc where maphonghoc = N'{0}'", txtNhapMaPhongHoc.Text);
            int check = 2;
            if (MessageBox.Show("Bạn có chắc chắn muốn thêm phòng học??","Phòng học",MessageBoxButtons.YesNo,MessageBoxIcon.Question) == DialogResult.Yes)
            {
                if (txtNhapMaPhongHoc.Text != string.Empty)
                {
                    if (sqlconn.mo_CSDL(sqltest,sqlconn.sql_data))
                    {
                        if (txtNhapTenPhongHoc.Text != string.Empty)
                        {
                            phongHoc.SuaPhongHocTheoDuLieu("tenphonghoc", txtNhapTenPhongHoc.Text, txtNhapMaPhongHoc.Text);
                            check = 1;
                        }
                        if (txtNhapDiaDiemPhongHoc.Text != string.Empty)
                        {
                            phongHoc.SuaPhongHocTheoDuLieu("diadiem", txtNhapDiaDiemPhongHoc.Text, txtNhapMaPhongHoc.Text);
                            check = 1;
                        }
                        if (check == 1)
                        {
                            MessageBox.Show("Cập nhật thành công " + txtNhapMaPhongHoc.Text + "", "Phòng học", MessageBoxButtons.OK);
                        }
                        else
                        {
                            MessageBox.Show("Mời bạn nhập thông tin cần cập nhật", "Thông Báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        }
                    }
                    else
                    {
                        MessageBox.Show("Mã phòng học không tồn tại!!", "Phòng học", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                }
                else
                {
                    MessageBox.Show("Vui lòng nhập mã phòng học cần cập nhật", "Phòng học", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            txtTrongPhongHoc();
            LoadDtaGVPhongHoc();
        }

        // tìm kiếm phòng học
        private void btnTimKiemPhongHoc_Click(object sender, EventArgs e)
        {
            ThietLapDanhMuc.PhongHoc phongHoc = new ThietLapDanhMuc.PhongHoc();
            if (txtNhapMaPhongHoc.Text != string.Empty || txtNhapTenPhongHoc.Text != string.Empty || txtNhapDiaDiemPhongHoc.Text != string.Empty)
            {
                    dtaGVPhongHoc.DataSource = phongHoc.TimKiemPhongHoc(txtNhapMaPhongHoc.Text,txtNhapTenPhongHoc.Text,txtNhapDiaDiemPhongHoc.Text);
            }
            else
            {
                MessageBox.Show("Vui lòng nhập thông tin", "Thông Báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        // xóa phòng học
        private void btnXoaPhongHoc_Click(object sender, EventArgs e)
        {
            ThietLapDanhMuc.PhongHoc phongHoc = new ThietLapDanhMuc.PhongHoc();
            if (txtNhapMaPhongHoc.Text != string.Empty)
            {
                if (MessageBox.Show("Bạn có chắc chắn muốn xóa phòng học này??","Phòng học",MessageBoxButtons.YesNo,MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    phongHoc.XoaPhongHoc(txtNhapMaPhongHoc.Text);
                }
                   
            }
            else
            {
                MessageBox.Show("Vui lòng nhập thông tin", "Thông Báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            txtTrongPhongHoc();
            LoadDtaGVPhongHoc();
        }

        // load phòng học
        private void btnLoadPhongHoc_Click(object sender, EventArgs e)
        {
            txtTrongPhongHoc();
            LoadDtaGVPhongHoc();
        }

        #endregion Kết thúc phòng học

        #region Loại Phiếu

        void LoadDtaGVLoaiPhieu()
        {
            KetNoiSql sqlconn = new KetNoiSql();
            dtaGVLoaiPhieu.DataSource = sqlconn.ThucThiTable("select * from loaiphieu",sqlconn.sql_data);
        }

        // thêm loại phiếu
        private void btnThemLoaiPhieu_Click(object sender, EventArgs e)
        {
            ThietLapDanhMuc.LoaiPhieu loaiPhieu = new ThietLapDanhMuc.LoaiPhieu();
            if (txtNhapMaLoaiPhieu.Text != string.Empty && txtNhapTenLoaiPhieu.Text != string.Empty)
            {
                    loaiPhieu.ThemLoaiPhieu(txtNhapMaLoaiPhieu.Text, txtNhapTenLoaiPhieu.Text);
            }
            else
            {
                MessageBox.Show("Vui lòng nhập thông tin", "Thông Báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            txtTrongLoaiPhieu();
            LoadDtaGVLoaiPhieu();
        }

        // sửa loại phiếu
        private void btnSuaLoaiPhieu_Click(object sender, EventArgs e)
        {
            ThietLapDanhMuc.LoaiPhieu loaiPhieu = new ThietLapDanhMuc.LoaiPhieu();
            KetNoiSql sqlconn = new KetNoiSql();
            string sqltest = string.Format("select * from loaiphieu where maloaiphieu = N'{0}'", txtNhapMaLoaiPhieu.Text);
            int check = 2;
            if (MessageBox.Show("Bạn có chắc chắn muốn thêm loại phiếu này??","Loại phiếu",MessageBoxButtons.YesNo,MessageBoxIcon.Question) == DialogResult.Yes)
            {
                if (txtNhapMaLoaiPhieu.Text != string.Empty)
                {
                    if (sqlconn.mo_CSDL(sqltest,sqlconn.sql_data))
                    {
                        if (txtNhapTenLoaiPhieu.Text != string.Empty)
                        {
                            loaiPhieu.SuaLoaiPhieu("tenloaiphieu", txtNhapTenLoaiPhieu.Text, txtNhapMaLoaiPhieu.Text);
                            check = 1; 
                        }
                        if (check == 1)
                        {
                            MessageBox.Show("Bạn cập nhật thành công mã loại phiếu", "Loại phiếu", MessageBoxButtons.OK);
                        }
                        else
                        {
                            MessageBox.Show("Vui lòng nhập thông tin cần cập nhật", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        }
                    }
                    else
                    {
                        MessageBox.Show("Mã loại phiếu không tồn tại", "Loại phiếu", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                }
                else
                {
                    MessageBox.Show("Vui lòng nhập mẫ loại phiếu!!", "Loại phiếu", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            txtTrongLoaiPhieu();
            LoadDtaGVLoaiPhieu();
        }
        
        // tìm kiếm loại phiếu
        private void btnTimKiemLoaiPhieu_Click(object sender, EventArgs e)
        {
            ThietLapDanhMuc.LoaiPhieu loaiPhieu = new ThietLapDanhMuc.LoaiPhieu();
            if (txtNhapMaLoaiPhieu.Text != string.Empty|| txtNhapTenLoaiPhieu.Text != string.Empty)
            {
                    dtaGVLoaiPhieu.DataSource = loaiPhieu.TimKiemLoaiPhieu(txtNhapMaLoaiPhieu.Text, txtNhapTenLoaiPhieu.Text);
            }
            else
            {
                MessageBox.Show("Vui lòng nhập thông tin", "Thông Báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        // xóa loại phiếu
        private void btnXoaLoaiPhieu_Click(object sender, EventArgs e)
        {
            ThietLapDanhMuc.LoaiPhieu loaiPhieu = new ThietLapDanhMuc.LoaiPhieu();
            if (txtNhapMaLoaiPhieu.Text != string.Empty)
            {
                if (MessageBox.Show("Bạn có chắc chắn muốn xóa loại phiếu","Loại phiếu",MessageBoxButtons.YesNo,MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    loaiPhieu.XoaLoaiPhieu(txtNhapMaLoaiPhieu.Text);
                }
                    
            }
            else
            {
                MessageBox.Show("Vui lòng nhập thông tin", "Thông Báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            txtTrongLoaiPhieu();
            LoadDtaGVLoaiPhieu();
        }

        // load lại loại phiếu
        private void btnLoadLoaiPhieu_Click(object sender, EventArgs e)
        {
            txtTrongLoaiPhieu();
            LoadDtaGVLoaiPhieu();
        }

        #endregion Kết thúc loại phiếu

        #region Đơn Vị Tính
        void LoadDtaGVDonViTinh()
        {
            KetNoiSql sqlconn = new KetNoiSql();
            dtaGVDonViTinh.DataSource = sqlconn.ThucThiTable("select * from donvitinh",sqlconn.sql_data);
        }
        // thêm đơn vị tính
        private void btnThemDonViTinh_Click(object sender, EventArgs e)
        {
            ThietLapDanhMuc.DonViTinh donViTinh = new ThietLapDanhMuc.DonViTinh();
            if (txtNhapMaDonViTinh.Text != string.Empty && txtNhapTenDonViTinh.Text != string.Empty)
            {
                if (MessageBox.Show("Bạn có chắc chắn muốn thêm đơn vị tính??","Đơn vị tính",MessageBoxButtons.YesNo,MessageBoxIcon.Warning) == DialogResult.Yes)
                {
                    donViTinh.ThemDonViTinh(txtNhapMaDonViTinh.Text, txtNhapTenDonViTinh.Text);
                }
            }
            else
            {
                MessageBox.Show("Vui lòng nhập thông tin", "Thông Báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            LoadDtaGVDonViTinh();
            txtTrongDonViTinh();
        }
        // sửa đơn vị tính
        private void btnSuaDonViTinh_Click(object sender, EventArgs e)
        {
            ThietLapDanhMuc.DonViTinh donViTinh = new ThietLapDanhMuc.DonViTinh();
            KetNoiSql sqlconn = new KetNoiSql();
            string sqltest = string.Format("select * from donvitinh where madonvitinh = N'{0}'", txtNhapMaDonViTinh.Text);
            int check = 2;
            if (MessageBox.Show("Bạn có chắc chắn muốn thêm đơn vị tính??", "Đơn vị tính", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
            {
                if (txtNhapMaDonViTinh.Text != string.Empty)
                {
                    if (sqlconn.mo_CSDL(sqltest,sqlconn.sql_data))
                    {
                        if (txtNhapTenDonViTinh.Text != string.Empty)
                        {
                            donViTinh.SuaDonViTinh("tendonvitinh", txtNhapTenDonViTinh.Text, txtNhapMaDonViTinh.Text);
                            check = 1;
                        }
                        if (check == 1)
                        {
                            MessageBox.Show("Cập nhật thành công đơn vị tính " + txtNhapMaDonViTinh.Text + "", "Đơn vị tính", MessageBoxButtons.OK);
                        }
                        else
                        {
                            MessageBox.Show("Vui lòng nhập thông tin để cập nhật", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        }
                    }
                    else
                    {
                        MessageBox.Show("Mã đơn vị tính không tồn tại", "Mã đơn vị tính", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                }
                else
                {
                    MessageBox.Show("Vui lòng nhập mã đơn vị tính để cập nhật", "Đơn vị tính", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                
            }
            LoadDtaGVDonViTinh();
            txtTrongDonViTinh();
        }
        // tìm kiếm đơn vị tính
        private void btnTimKiemDonViTinh_Click(object sender, EventArgs e)
        {
            ThietLapDanhMuc.DonViTinh donViTinh = new ThietLapDanhMuc.DonViTinh();
            if (txtNhapMaDonViTinh.Text != string.Empty || txtNhapTenDonViTinh.Text != string.Empty)
            {
                    dtaGVDonViTinh.DataSource = donViTinh.TimKiemDonViTinh(txtNhapMaDonViTinh.Text, txtNhapTenDonViTinh.Text);
            }
            else
            {
                MessageBox.Show("Vui lòng nhập thông tin", "Thông Báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }

        }
        // xóa đơn vị tính
        private void btnXoaDonViTinh_Click(object sender, EventArgs e)
        {
            ThietLapDanhMuc.DonViTinh donViTinh = new ThietLapDanhMuc.DonViTinh();
            if (txtNhapMaDonViTinh.Text != string.Empty)
            {
                if (MessageBox.Show("Bạn có chắc chắn muốn xóa đơn vị tính??", "Đơn vị tính", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
                {
                    donViTinh.XoaDonViTinh(txtNhapMaDonViTinh.Text);
                }
                
            }
            else
            {
                MessageBox.Show("Vui lòng nhập thông tin", "Thông Báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            LoadDtaGVDonViTinh();
            txtTrongDonViTinh();
        }
        // load đơn vị tính
        private void btnLoadDonViTinh_Click(object sender, EventArgs e)
        {
            LoadDtaGVDonViTinh();
            txtTrongDonViTinh();
        }
        #endregion kết thúc đơn vị tính

        private void FThietLapDanhMuc_Load(object sender, EventArgs e)
        {
            tcThietLapDanhMuc.SelectedIndex = chiMuc;
            btnThoat.TabStop = false;
            btnThoat.FlatStyle = FlatStyle.Flat;
            btnThoat.FlatAppearance.BorderSize = 0;
        }

        private void btnThoat_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
