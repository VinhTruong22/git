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
    public partial class FXetDuyetPhieu : Form
    {
        // người đề xuất để biết ai cập nhật những vấn đề liên quan đến form này
        string nguoiDeXuat;
        string tendonvi;
        // loại phiếu đề biết bạn muốn vào chế độ nào của form : có 2 chế độ xét duyệt và đề xuất
        string loaiPhieu;
        public FXetDuyetPhieu(string _loaiPhieu,string _maDeXuat, string _tenDeXuat, string _maDonViDeXuat, string _loaiThietBi, string _tenThietBi, DateTime _ngayDeXuat, string _deXuatVanDe, string _lyDoDuyet)
        {
            InitializeComponent();
            this.loaiPhieu = _loaiPhieu;
            CapNhatPhieu();
            HienDtaGV();
            LoadCbxDeXuat();
            txtMaDeXuat.Text = _maDeXuat;
            txtTenDeXuat.Text = _tenDeXuat;
            cbxMaDonViDeXuat.Text = _maDonViDeXuat;
            cbxLoaiThietBi.Text = _loaiThietBi;
            cbxTenThietBi.Text = _tenThietBi;
            dtpNgayDeXuat.Value = _ngayDeXuat;
            txtDeXuatVanDe.Text = _deXuatVanDe;
            txtLyDoDuyet.Text = _lyDoDuyet;
            MacDinhHeaderDtaGV();
            
           

        }
        public FXetDuyetPhieu(string _loaiPhieu, string _nguoiDeXuat)
        {
            this.loaiPhieu = _loaiPhieu;
            this.nguoiDeXuat = _nguoiDeXuat;
            InitializeComponent();
            CapNhatPhieu();
            HienDtaGV();
            MacDinhHeaderDtaGV();
            LoadCbxDeXuat();
            LoadCbxLoaiThietBi();
            LoadCBXTenThietBi();
            
        }

        void SD()
        {
            if (cbxMaDonViDeXuat.Text == "QL")
            {
                tendonvi = "Quản Lý";
            }
        }
        // hàm chỉnh lại control khi người dùng muốn vào theo chế độ nào
        void CapNhatPhieu()
        {
            // chế độ đề xuất thì sẽ ẩn những vấn đề liên quan đến xét duyệt
            if (loaiPhieu == "Đề xuất")
            {
                lbLyDoDuyet.Visible = false;
                txtLyDoDuyet.Visible = false;
                pnlBtnDeXuat.Visible = true;
                pnlDuyet.Visible = false;
                pnlBtnDeXuat.Location = new System.Drawing.Point(813, 632);

            }
            if (loaiPhieu == "Xem xét duyệt")
            {
                pnlBtnDeXuat.Visible = false;
                pnlDuyet.Visible = false;
                txtMaDeXuat.Enabled = false;
                txtTenDeXuat.Enabled = false;
                cbxMaDonViDeXuat.Enabled = false;
                txtTenDonViDeXuat.Enabled = false;
                cbxLoaiThietBi.Enabled = false;
                txtLyDoDuyet.Enabled = false;
                txtDeXuatVanDe.Enabled = false;
                cbxTenThietBi.Enabled = false;
            }
            // chế độ xét duyệt thì sẽ ẩn những button đề xuất
            if (loaiPhieu == "Xét duyệt")
            {
                pnlBtnDeXuat.Visible = false;
                txtMaDeXuat.Enabled = false;
                txtTenDeXuat.Enabled = false;
                cbxMaDonViDeXuat.Enabled = false;
                txtTenDonViDeXuat.Enabled = false;
                cbxLoaiThietBi.Enabled = false;
                cbxTenThietBi.Enabled = false;
                dtpNgayDeXuat.Enabled = false;
                txtDeXuatVanDe.Enabled = false;
                dtaGVPhieuXetDuyet.ReadOnly = true;

            }
        }

        #region Mặc Định
        // hàm load lại combobox mà đơn vị đề xuất
        void LoadCbxDeXuat()
        {
            
            KetNoiSql sqlconn = new KetNoiSql();
            string sql = string.Format("select * from dbo.donvi");
            if (sqlconn.mo_CSDL(sql,sqlconn.sql_data))
            {
                cbxMaDonViDeXuat.DataSource = sqlconn.ThucThiTable(sql, sqlconn.sql_data);
                cbxMaDonViDeXuat.DisplayMember = "madonvi";
                cbxMaDonViDeXuat.ValueMember = "madonvi";
            }
            else
            {
                MessageBox.Show("Mã đơn vị không tồn tại", "Thông Báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            

        }
        // sự kiện thay đổi tên đơn vị để người dùng nhận ra đó là tên đơn vị nào theo mã đơn vị
        private void cbxMaDonViDeXuat_TextChanged(object sender, EventArgs e)
        {
            KetNoiSql sqlconn = new KetNoiSql();
            string sql = string.Format("select * from dbo.donvi where madonvi = '{0}'", cbxMaDonViDeXuat.Text);
            txtTenDonViDeXuat.Text = sqlconn.LayDuLieu(sql, "tendonvi", sqlconn.sql_data);
        }
        // hàm load lại combobox loại thiết bị
        void LoadCbxLoaiThietBi()
        {
            KetNoiSql sqlconn = new KetNoiSql();
            string sql = string.Format("select * from dbo.loaithietbi");
            cbxLoaiThietBi.DataSource = sqlconn.ThucThiTable(sql, sqlconn.sql_data);
            cbxLoaiThietBi.DisplayMember = "maloaitb";
            cbxLoaiThietBi.ValueMember = "maloaitb";
        }
        // sự kiện thay đôi khi người dùng nhập loại thiết bị sẽ chọn tên thiết bị tương ứng nằm trong thiết bị đó
        private void cbxLoaiThietBi_TextChanged(object sender, EventArgs e)
        {
            LoadCBXTenThietBi();
        }
        // load combobox tên thiết bị theo loại thiết bị tương ứng
        void LoadCBXTenThietBi()
        {
            KetNoiSql sqlconn = new KetNoiSql();
            string sql = string.Format("select tentb from dbo.thietbi where maloai = N'{0}'", cbxLoaiThietBi.Text);
            cbxTenThietBi.DataSource = sqlconn.ThucThiTable(sql, sqlconn.sql_data);
            cbxTenThietBi.DisplayMember = "tentb";
            cbxTenThietBi.ValueMember = "tentb";
        }

       
        // hàm hiện datagridview
        void HienDtaGV()
        {
            ChucNang.DeXuat_XuLy deXuat = new ChucNang.DeXuat_XuLy();
            dtaGVPhieuXetDuyet.DataSource = deXuat.HienDtaGV();
        }
        void MacDinhHeaderDtaGV()
        {
            dtaGVPhieuXetDuyet.Columns[0].HeaderText = "Mã đề xuất";
            dtaGVPhieuXetDuyet.Columns[1].HeaderText = "Tên đề xuất";
            dtaGVPhieuXetDuyet.Columns[2].HeaderText = "Mã đơn vị";
            dtaGVPhieuXetDuyet.Columns[3].HeaderText = "Loại thiết bị";
            dtaGVPhieuXetDuyet.Columns[4].HeaderText = "Tên thiết bị";
            dtaGVPhieuXetDuyet.Columns[5].HeaderText = "Ngày đề xuất";
            dtaGVPhieuXetDuyet.Columns[6].HeaderText = "Lý do đề xuất";
            dtaGVPhieuXetDuyet.Columns[7].HeaderText = "Trạng thái duyệt";
            dtaGVPhieuXetDuyet.Columns[8].HeaderText = "Lý do không duyệt";
            dtaGVPhieuXetDuyet.Columns[9].HeaderText = "Ngày duyệt";
            dtaGVPhieuXetDuyet.Columns[10].HeaderText = "Người đề xuất";
            dtaGVPhieuXetDuyet.Columns[11].HeaderText = "Người duyệt";
            if (loaiPhieu == "Đề xuất")
            {
                ThemButtonKiemTraXetDuyet();
            }
        }
        void ThemButtonKiemTraXetDuyet()
        {
            DataGridViewButtonColumn btnXemXetDuyet = new DataGridViewButtonColumn();
            btnXemXetDuyet.Text = "Xem xét duyệt";
            btnXemXetDuyet.Name = "dtaGVbtnThietBi";
            btnXemXetDuyet.HeaderText = " ";
            btnXemXetDuyet.UseColumnTextForButtonValue = true;
            dtaGVPhieuXetDuyet.Columns.Insert(12, btnXemXetDuyet);
        }
        // cài đặt mặc định textbox trống
        void txtTrongDeXuat()
        {
            txtMaDeXuat.Text = string.Empty;
            txtTenDeXuat.Text = string.Empty;
            cbxMaDonViDeXuat.Text = string.Empty;
            txtTenDonViDeXuat.Text = string.Empty;
            cbxLoaiThietBi.Text = string.Empty;
            cbxTenThietBi.Text = string.Empty;
            dtpNgayDeXuat.Value = DateTime.Now;
            txtDeXuatVanDe.Text = string.Empty;
            txtLyDoDuyet.Text = string.Empty;
        }
        #endregion kết thúc mặc định

        // button những vấn đề liên quan đến đề xuất
        #region Button đề xuất
        // Trước tiên có những vấn đề gì thì người dùng sẽ thêm những đề xuất lên cấp trên
        private void btnThemDeXuat_Click(object sender, EventArgs e)
        {
            // viết sẵn những xử lý trong này chỉ cần lấy ra sử dụng lại
            ChucNang.DeXuat_XuLy deXuat = new ChucNang.DeXuat_XuLy();
            try
            {
                // xác nhận ban có chắc chắn thêm, tránh trường hợp người dùng bấm nhầm
                if (MessageBox.Show("Bạn có chắc chắn muốn đề xuất??","Thông Báo",MessageBoxButtons.YesNo,MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    if (txtTenDeXuat.Text != string.Empty && cbxMaDonViDeXuat.Text != string.Empty && cbxTenThietBi.Text != string.Empty)
                    {
                        // thêm đề xuất những vấn đề liên quan để gửi lên hệ thống và hiện những đề xuất lên bảng datagridview
                        deXuat.ThemDeXuat(txtTenDeXuat.Text, cbxMaDonViDeXuat.Text, cbxLoaiThietBi.Text, cbxTenThietBi.Text, dtpNgayDeXuat.Value, txtDeXuatVanDe.Text, nguoiDeXuat);
                        HienDtaGV();
                        txtTrongDeXuat();
                    }
                    else
                    {
                        MessageBox.Show("Vui lòng nhập đủ thông tin", "Đề Xuất", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                    
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        // cập nhật lại những đề xuất nếu người dùng muốn thay đổi lại đề xuất của mình
        private void btnCapNhatDeXuat_Click(object sender, EventArgs e)
        {
            // dùng để check các trường hợp xuất ra thông báo
            int check = 0;
            // viết sẵn những xử lý trong này chỉ cần lấy ra sử dụng lại
            ChucNang.DeXuat_XuLy deXuat = new ChucNang.DeXuat_XuLy();
            KetNoiSql sqlconn = new KetNoiSql();
            try
            {
                string sqltest = string.Format("select * from donvi where madonvi = '{0}'", cbxMaDonViDeXuat.Text);
                // xác nhận ban có chắc chắn cập nhật, tránh trường hợp người dùng bấm nhầm
                if (MessageBox.Show("Bạn có muốn cập nhật đề xuất??", "Thông Báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    // muốn sửa những thông tin gì thì người dùng chỉ cần nhập những thông tin đó và sửa không cần phải viết hết thông tin sửa một lần
                    if (txtMaDeXuat.Text != string.Empty)
                    {
                        if (txtTenDeXuat.Text != string.Empty)
                        {
                            deXuat.CapNhatDeXuat("tendexuat", txtTenDeXuat.Text, txtMaDeXuat.Text, nguoiDeXuat);
                            check = 1;
                        }
                        if (cbxMaDonViDeXuat.Text != string.Empty)
                        {
                            if (sqlconn.mo_CSDL(sqltest, sqlconn.sql_data))
                            {
                                deXuat.CapNhatDeXuat("madonvi", cbxMaDonViDeXuat.Text, txtMaDeXuat.Text, nguoiDeXuat);
                                check = 1;
                            }
                            else
                            {
                                MessageBox.Show("Mã đơn vị không tồn tại", "Thông Báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            }

                        }
                        // ở trường này sử dụng 2 điều kiện vì loại thiết bị và tên thiết bị thường đi chung vì thế ta nên cập nhật cả 2
                        if (cbxLoaiThietBi.Text != string.Empty && cbxTenThietBi.Text != string.Empty)
                        {
                            deXuat.CapNhatDeXuat("maloaitb", cbxLoaiThietBi.Text, txtMaDeXuat.Text, nguoiDeXuat);
                            deXuat.CapNhatDeXuat("tentb", cbxTenThietBi.Text, txtMaDeXuat.Text, nguoiDeXuat);
                            check = 1;
                        }

                        if (txtDeXuatVanDe.Text != string.Empty)
                        {
                            deXuat.CapNhatDeXuat("lydodexuat", txtDeXuatVanDe.Text, txtMaDeXuat.Text, nguoiDeXuat);
                            check = 1;
                        }
                        // if check == 1 thì xuất ra thông báo thành công
                        if (check == 1)
                        {
                            MessageBox.Show("Cập nhật thành công " + txtMaDeXuat.Text + "");
                        }
                        // if check == 0 là chưa vào các trường hợp nào trên đây thì có nghĩa là chưa nhập thông tin cần cập nhật nên sẽ xuất ra thông báo
                        else
                        {
                            MessageBox.Show("Vui lòng nhập thông tin cần cập nhật", "Thông Báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        }
                        // hiện datagridview sau khi người dùng đã cập nhật những thông tin trên
                        HienDtaGV();
                        txtTrongDeXuat();
                    }
                    else
                    {
                        MessageBox.Show("Vui lòng nhập mã đề xuất cần cập nhật", "Thông Báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        // xóa đề xuất nếu người dùng muốn
        private void btnXoaDeXuat_Click(object sender, EventArgs e)
        {
            // viết sẵn những xử lý trong này chỉ cần lấy ra sử dụng lại
            ChucNang.DeXuat_XuLy deXuat = new ChucNang.DeXuat_XuLy();
            try
            {
                // xác nhận ban có chắc chắn xóa, tránh trường hợp người dùng bấm nhầm
                if (MessageBox.Show("Bạn có muốn xóa đề xuất??", "Thông Báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    // nhập mã đề xuất cần thay đổi
                    if (txtMaDeXuat.Text != string.Empty)
                    {
                        deXuat.XoaDeXuat(txtMaDeXuat.Text);
                        HienDtaGV();
                        txtTrongDeXuat();
                    }
                    else
                    {
                        MessageBox.Show("Vui lòng nhập mã đề xuất cần xóa", "Thông Báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        #endregion kết thúc button đề xuất

        // phần duyệt cho người dùng
        #region button duyệt
        // if người dùng bấm duyệt đề xuất thì sẽ cập nhật lại thông tin đề xuất đó
        private void btnDuyet_Click(object sender, EventArgs e)
        {
            ChucNang.DeXuat_XuLy deXuat = new ChucNang.DeXuat_XuLy();
            try
            { 
                // cập nhật lại thông tin duyệt , lý do , thời gian , người duyệt, cho đề xuất đó
                deXuat.XetDuyet("Duyệt", txtLyDoDuyet.Text, DateTime.Now, nguoiDeXuat, txtMaDeXuat.Text);
                HienDtaGV();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }  
        }

        private void btnKhongDuyet_Click(object sender, EventArgs e)
        {
            ChucNang.DeXuat_XuLy deXuat = new ChucNang.DeXuat_XuLy();
            try
            {
                // cập nhật lại thông tin duyệt , lý do , thời gian , người duyệt, cho đề xuất đó
                deXuat.XetDuyet("Không duyệt", txtLyDoDuyet.Text, DateTime.Now, nguoiDeXuat, txtMaDeXuat.Text);
                HienDtaGV();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        #endregion kết thúc button duyệt

        // button quay lại form trước
        private void btnQuayLai_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        // hiện các thông tin trên datagridview lên textbox
        private void dtaGVPhieuXetDuyet_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                ChucNang.DeXuat_XuLy deXuat = new ChucNang.DeXuat_XuLy();
                
                //string tentb = sqlconn.LayDuLieu("select * from xetduyet where madexuat = N'" + txtMaDeXuat.Text + "'", "tentb", sqlconn.sql_data);
                // if người dùng vào xét duyệt thì sẽ hiện thêm phần lý do duyệt bởi vì phần đề xuất sẽ ẩn textbox này
                if (loaiPhieu == "Đề xuất")
                {
                    txtMaDeXuat.Text = dtaGVPhieuXetDuyet.Rows[e.RowIndex].Cells[1].Value.ToString();
                    txtTenDeXuat.Text = dtaGVPhieuXetDuyet.Rows[e.RowIndex].Cells[2].Value.ToString();
                    cbxMaDonViDeXuat.Text = dtaGVPhieuXetDuyet.Rows[e.RowIndex].Cells[3].Value.ToString();
                    cbxLoaiThietBi.Text = dtaGVPhieuXetDuyet.Rows[e.RowIndex].Cells[4].Value.ToString();
                    cbxTenThietBi.Text = dtaGVPhieuXetDuyet.Rows[e.RowIndex].Cells[5].Value.ToString();
                    dtpNgayDeXuat.Value = DateTime.Parse(dtaGVPhieuXetDuyet.Rows[e.RowIndex].Cells[6].Value.ToString());
                    txtDeXuatVanDe.Text = dtaGVPhieuXetDuyet.Rows[e.RowIndex].Cells[7].Value.ToString();
                    string lydo = deXuat.LayDuLieu(txtMaDeXuat.Text, "lydoduyet");
                    string thietbi = deXuat.LayDuLieu(txtMaDeXuat.Text, "tentb");
                    if (dtaGVPhieuXetDuyet.Columns[e.ColumnIndex] is DataGridViewButtonColumn && e.RowIndex >= 0)
                    {
                        
                        FXetDuyetPhieu xemXetDuyet = new FXetDuyetPhieu("Xem xét duyệt", txtMaDeXuat.Text, txtTenDeXuat.Text, cbxMaDonViDeXuat.Text ,cbxLoaiThietBi.Text,thietbi, dtpNgayDeXuat.Value, txtDeXuatVanDe.Text, lydo);
                        xemXetDuyet.ShowDialog();
                    }
                }
                else
                {
                    txtMaDeXuat.Text = dtaGVPhieuXetDuyet.Rows[e.RowIndex].Cells[0].Value.ToString();
                    txtTenDeXuat.Text = dtaGVPhieuXetDuyet.Rows[e.RowIndex].Cells[1].Value.ToString();
                    cbxMaDonViDeXuat.Text = dtaGVPhieuXetDuyet.Rows[e.RowIndex].Cells[2].Value.ToString();
                    cbxLoaiThietBi.Text = dtaGVPhieuXetDuyet.Rows[e.RowIndex].Cells[3].Value.ToString();
                    cbxTenThietBi.Text = dtaGVPhieuXetDuyet.Rows[e.RowIndex].Cells[4].Value.ToString();
                    dtpNgayDeXuat.Value = DateTime.Parse(dtaGVPhieuXetDuyet.Rows[e.RowIndex].Cells[5].Value.ToString());
                    txtDeXuatVanDe.Text = dtaGVPhieuXetDuyet.Rows[e.RowIndex].Cells[6].Value.ToString();
                    txtLyDoDuyet.Text = dtaGVPhieuXetDuyet.Rows[e.RowIndex].Cells[8].Value.ToString();
                }
                
            }
            catch (Exception)
            {
            }
            

        }

        private void FXetDuyetPhieu_Load(object sender, EventArgs e)
        { //load lại combobox để trống
            if (loaiPhieu == "Xem xét duyệt")
            {

            }
            else
            {
                cbxMaDonViDeXuat.Text = string.Empty;
                cbxLoaiThietBi.Text = string.Empty;
                cbxTenThietBi.Text = string.Empty;
            }
        }

    }
}
