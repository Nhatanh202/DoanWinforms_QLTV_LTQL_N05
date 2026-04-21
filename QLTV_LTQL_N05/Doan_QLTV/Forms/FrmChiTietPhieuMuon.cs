using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace Doan_QLTV.Forms
{
    public partial class FrmChiTietPhieuMuon : Form
    {
        private int _maPhieu;
        bool xulyThem = false;
        private readonly Database db;

        // Constructor không tham số
        public FrmChiTietPhieuMuon()
        {
            InitializeComponent();
            db = new Database(@"LAPTOP-08CQM6N0", "QuanLyThuVien");
            dtgChiTietPM.SelectionChanged += dtgChiTietPM_SelectionChanged;
            btnThoat.Click += btnThoat_Click;

            btnSua.Click += btnSua_Click;
            btnXoa.Click += btnXoa_Click;
            btnHuy.Click += btnHuy_Click;
            btnIn.Click += btnIn_Click;

            cboTrangThai.SelectedIndexChanged += CboTrangThai_SelectedIndexChanged;
            cboMaPhieu.SelectedIndexChanged += CboMaPhieu_SelectedIndexChanged;
        }

        private void CboMaPhieu_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cboMaPhieu.SelectedValue != null && cboMaPhieu.SelectedValue is int ma)
            {
                _maPhieu = ma;
                LoadData();
            }
        }

        private void CboTrangThai_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cboTrangThai.Text == "Đã trả")
            {
                dtpNgayTra.Checked = true;
                dtpNgayTra.Value = DateTime.Today;
            }
            else
            {
                dtpNgayTra.Checked = false;
            }
        }

        // Constructor chính - Nhận mã phiếu từ form FrmMuonTraSach
        public FrmChiTietPhieuMuon(int maPhieu) : this()
        {
            this._maPhieu = maPhieu;
        }

        private void setButton(bool enable)
        {
            btnThem.Enabled = enable;
            btnSua.Enabled = enable;
            btnXoa.Enabled = enable;
            btnLuu.Enabled = !enable;
            btnHuy.Enabled = !enable;

            cboTinhTrang.Enabled = !enable;
            txtGhichu.Enabled = !enable;

            cboMaPhieu.Enabled = !enable || _maPhieu == 0; // Để cho phép chọn phiếu ở chế độ view nếu ko có phiếu truyền vào

            cboMaSach.Enabled = !enable;
            cboTrangThai.Enabled = !enable;
            dtpNgayTra.Enabled = !enable;
        }

        private void LoadMaSachCombobox()
        {
            try
            {
                DataTable dtSach = db.laydl("SELECT MaSach, TenSach FROM Sach");
                cboMaSach.DataSource = dtSach;
                cboMaSach.DisplayMember = "TenSach";
                cboMaSach.ValueMember = "MaSach";
                cboMaSach.SelectedIndex = -1;
            }
            catch { }
        }

        private void LoadMaPhieuCombobox()
        {
            try
            {
                DataTable dtPM = db.laydl("SELECT MaPhieu FROM PhieuMuon ORDER BY MaPhieu");

                // Gỡ tạm event để tránh load liên tục khi nạp data
                cboMaPhieu.SelectedIndexChanged -= CboMaPhieu_SelectedIndexChanged;

                cboMaPhieu.DataSource = dtPM;
                cboMaPhieu.DisplayMember = "MaPhieu";
                cboMaPhieu.ValueMember = "MaPhieu";

                if (_maPhieu > 0)
                {
                    cboMaPhieu.SelectedValue = _maPhieu;
                }
                else
                {
                    cboMaPhieu.SelectedIndex = -1;
                }

                // Gắn lại event
                cboMaPhieu.SelectedIndexChanged += CboMaPhieu_SelectedIndexChanged;
            }
            catch { }
        }

        // ====================== LOAD DỮ LIỆU ======================
        private void LoadData()
        {
            try
            {
                LoadMaSachCombobox();
                LoadMaPhieuCombobox();

                bool hasPhieuFilter = _maPhieu > 0;
                string whereClause = hasPhieuFilter ? $"WHERE ct.MaPhieu = {_maPhieu}" : string.Empty;

                string sqlGrid = $@"SELECT 
                    ct.MaPhieu,
                    ct.MaSach,
                    ISNULL(s.TenSach, N'') AS TenSach,
                    ct.NgayTraThucTe,
                    CASE 
                        WHEN ISNULL(ct.TrangThaiTra, 0) = 1 THEN N'Đã trả'
                        ELSE N'Chưa trả'
                    END AS TrangThaiTraText,
                    ISNULL(ct.TinhTrangSach, N'') AS TinhTrangSach,
                    ISNULL(ct.GhiChu, N'') AS GhiChu
                FROM ChiTietPhieuMuon ct
                LEFT JOIN Sach s ON ct.MaSach = s.MaSach
                {whereClause}
                ORDER BY ct.MaPhieu, ct.MaSach";

                DataTable dt = db.laydl(sqlGrid);
                dtgChiTietPM.DataSource = dt;
                ConfigureGrid(hasPhieuFilter);
                groupBox2.Text = hasPhieuFilter ? "Danh sách phiếu mượn" : "Danh sách chi tiết phiếu mượn";

                if (dt.Rows.Count > 0)
                {
                    dtgChiTietPM.ClearSelection();
                    dtgChiTietPM.Rows[0].Selected = true;
                    ShowRowDetail(dtgChiTietPM.Rows[0]);
                }
                else
                {
                    ClearDetailControls();
                    cboMaPhieu.Text = hasPhieuFilter ? _maPhieu.ToString() : string.Empty;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi tải dữ liệu: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ConfigureGrid(bool hasPhieuFilter)
        {
            if (dtgChiTietPM.Columns.Count == 0)
            {
                return;
            }

            dtgChiTietPM.AutoGenerateColumns = true;
            dtgChiTietPM.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dtgChiTietPM.ReadOnly = true;
            dtgChiTietPM.AllowUserToResizeRows = false;

            dtgChiTietPM.Columns["MaPhieu"].Visible = !hasPhieuFilter;
            dtgChiTietPM.Columns["MaPhieu"].HeaderText = "Mã Phiếu";
            dtgChiTietPM.Columns["MaSach"].HeaderText = "Mã Sách";
            dtgChiTietPM.Columns["TenSach"].HeaderText = "Tên Sách";
            dtgChiTietPM.Columns["NgayTraThucTe"].HeaderText = "Ngày Trả";
            dtgChiTietPM.Columns["TrangThaiTraText"].HeaderText = "Trạng Thái";
            dtgChiTietPM.Columns["TinhTrangSach"].HeaderText = "Tình Trạng";
            dtgChiTietPM.Columns["GhiChu"].HeaderText = "Ghi Chú";

            DataGridViewColumn ngayTraColumn = dtgChiTietPM.Columns["NgayTraThucTe"];
            if (ngayTraColumn != null)
            {
                ngayTraColumn.DefaultCellStyle.Format = "dd/MM/yyyy";
            }
        }

        private void ShowRowDetail(DataGridViewRow row)
        {
            if (row == null || row.IsNewRow)
            {
                return;
            }

            // Xử lý cắm giá trị cho cboMaPhieu mà ko kích hoạt SelectedChanged
            cboMaPhieu.SelectedIndexChanged -= CboMaPhieu_SelectedIndexChanged;
            cboMaPhieu.SelectedValue = row.Cells["MaPhieu"].Value;
            cboMaPhieu.SelectedIndexChanged += CboMaPhieu_SelectedIndexChanged;

            if (row.Cells["MaSach"].Value != DBNull.Value)
                cboMaSach.SelectedValue = row.Cells["MaSach"].Value;

            cboTrangThai.Text = Convert.ToString(row.Cells["TrangThaiTraText"].Value);
            cboTinhTrang.Text = Convert.ToString(row.Cells["TinhTrangSach"].Value);
            txtGhichu.Text = Convert.ToString(row.Cells["GhiChu"].Value);

            object ngayTraValue = row.Cells["NgayTraThucTe"].Value;
            dtpNgayTra.ShowCheckBox = true;
            if (ngayTraValue == null || ngayTraValue == DBNull.Value)
            {
                dtpNgayTra.Checked = false;
                dtpNgayTra.Value = DateTime.Today;
            }
            else
            {
                dtpNgayTra.Checked = true;
                dtpNgayTra.Value = Convert.ToDateTime(ngayTraValue);
            }
        }

        private void ClearDetailControls()
        {
            cboMaPhieu.SelectedIndexChanged -= CboMaPhieu_SelectedIndexChanged;
            if (_maPhieu == 0) cboMaPhieu.SelectedIndex = -1;
            cboMaPhieu.SelectedIndexChanged += CboMaPhieu_SelectedIndexChanged;

            cboMaSach.SelectedIndex = -1;
            cboTrangThai.SelectedIndex = -1;
            cboTinhTrang.SelectedIndex = -1;
            txtGhichu.Clear();
            dtpNgayTra.ShowCheckBox = true;
            dtpNgayTra.Checked = false;
            dtpNgayTra.Value = DateTime.Today;
        }

        private void dtgChiTietPM_SelectionChanged(object sender, EventArgs e)
        {
            if (dtgChiTietPM.CurrentRow != null)
            {
                ShowRowDetail(dtgChiTietPM.CurrentRow);
            }
        }

        private void btnThoat_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void FrmChiTietPhieuMuon_Load(object sender, EventArgs e)
        {
            LoadData();
            setButton(true);
        }

        private void contextMenuStrip1_Opening(object sender, System.ComponentModel.CancelEventArgs e)
        {

        }

        private void btnLuu_Click(object sender, EventArgs e)
        {
            if (cboMaPhieu.SelectedValue == null || cboMaSach.SelectedValue == null)
            {
                MessageBox.Show("Vui lòng nhập đầy đủ Chọn Phiếu và chọn Mã Sách!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                if (db.cn.State == ConnectionState.Closed)
                {
                    db.cn.Open();
                }

                int maPhieuToSave = (int)cboMaPhieu.SelectedValue;

                string sql = "";
                if (xulyThem)
                {
                    sql = "INSERT INTO ChiTietPhieuMuon (MaPhieu, MaSach, NgayTraThucTe, TrangThaiTra, TinhTrangSach, GhiChu) " +
                          "VALUES (@MaPM, @MaS, @NgayT, @TrangT, @TinhT, @GhiC)";
                }
                else
                {
                    sql = "UPDATE ChiTietPhieuMuon SET NgayTraThucTe = @NgayT, TrangThaiTra = @TrangT, TinhTrangSach = @TinhT, GhiChu = @GhiC " +
                          "WHERE MaPhieu = @MaPM AND MaSach = @MaS";
                }

                SqlCommand cmd = new SqlCommand(sql, db.cn);

                cmd.Parameters.AddWithValue("@MaPM", maPhieuToSave);
                cmd.Parameters.AddWithValue("@MaS", cboMaSach.SelectedValue);

                bool isDaTra = cboTrangThai.Text.Equals("Đã trả", StringComparison.OrdinalIgnoreCase);
                if (isDaTra && dtpNgayTra.Checked)
                    cmd.Parameters.AddWithValue("@NgayT", dtpNgayTra.Value.Date);
                else
                    cmd.Parameters.AddWithValue("@NgayT", DBNull.Value);

                // Chuyển chữ "Đã trả" thành số 1, "Chưa trả" thành số 0
                cmd.Parameters.AddWithValue("@TrangT", isDaTra ? 1 : 0);
                cmd.Parameters.AddWithValue("@TinhT", string.IsNullOrWhiteSpace(cboTinhTrang.Text) ? (object)DBNull.Value : cboTinhTrang.Text);
                cmd.Parameters.AddWithValue("@GhiC", string.IsNullOrWhiteSpace(txtGhichu.Text) ? (object)DBNull.Value : txtGhichu.Text);

                int rowsAffected = cmd.ExecuteNonQuery();
                db.cn.Close();

                if (rowsAffected > 0)
                {
                    MessageBox.Show("Lưu thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    setButton(true);
                    LoadData();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                if (db.cn.State == ConnectionState.Open) db.cn.Close();
            }
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            if (dtgChiTietPM.CurrentRow == null) return;

            xulyThem = false;
            setButton(false);

            // Edit mode specific configurations
            cboTinhTrang.Enabled = true;
            txtGhichu.Enabled = true;
            cboTrangThai.Enabled = true;
            dtpNgayTra.Enabled = true;
            cboMaSach.Enabled = false; // Usually shouldn't change book code for existing detail record
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            if (dtgChiTietPM.CurrentRow == null)
            {
                MessageBox.Show("Vui lòng chọn dòng để xóa!");
                return;
            }

            DialogResult dr = MessageBox.Show("Bạn có chắc muốn xóa chi tiết sách mượn này?", "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (dr == DialogResult.Yes)
            {
                try
                {
                    string sql = "DELETE FROM ChiTietPhieuMuon WHERE MaPhieu = @MaPM AND MaSach = @MaS";
                    SqlCommand cmd = new SqlCommand(sql, db.cn);
                    cmd.Parameters.AddWithValue("@MaPM", cboMaPhieu.SelectedValue);

                    // Lấy mã sách thực tế đang chọn trên Grid để xóa, thay vì lấy từ Combobox (nếu Combobox đang chọn nhầm/sai)
                    cmd.Parameters.AddWithValue("@MaS", dtgChiTietPM.CurrentRow.Cells["MaSach"].Value);

                    if (db.cn.State == ConnectionState.Closed) db.cn.Open();
                    cmd.ExecuteNonQuery();
                    db.cn.Close();

                    MessageBox.Show("Xóa thành công!");
                    LoadData();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi xóa: " + ex.Message);
                    if (db.cn.State == ConnectionState.Open) db.cn.Close();
                }
            }
        }

        private void btnHuy_Click(object sender, EventArgs e)
        {
            setButton(true);
            if (dtgChiTietPM.CurrentRow != null)
            {
                ShowRowDetail(dtgChiTietPM.CurrentRow);
            }
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            xulyThem = true;

            // 1. Xóa trắng các ô nhập liệu để người dùng gõ mới (Trừ Mã phiếu nếu đã có sẵn)
            if (_maPhieu == 0) cboMaPhieu.SelectedIndex = -1;
            cboTinhTrang.SelectedIndex = -1;
            cboMaSach.SelectedIndex = -1;
            txtGhichu.Clear();
            cboTrangThai.SelectedIndex = -1;
            dtpNgayTra.Value = DateTime.Now;

            // 2. Mở khóa (Enable) các ô TextBox/ComboBox và nút Lưu
            cboTinhTrang.Enabled = true;
            txtGhichu.Enabled = true;
            cboMaSach.Enabled = true;
            cboTrangThai.Enabled = true;
            if (_maPhieu == 0) cboMaPhieu.Enabled = true;

            // 3. Khóa các nút chức năng khác để tránh xung đột
            btnThem.Enabled = false;
            btnHuy.Enabled = true;
            btnSua.Enabled = false;
            btnXoa.Enabled = false;
            btnLuu.Enabled = true;
            if (_maPhieu == 0) cboMaPhieu.Focus(); else cboMaSach.Focus();
        }

        private void btnIn_Click(object sender, EventArgs e)
        {
            if (cboMaPhieu.SelectedValue == null)
            {
                MessageBox.Show("Vui lòng chọn một phiếu mượn để in!", "Lưu ý", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            int maPhieuIn = (int)cboMaPhieu.SelectedValue;

            // Xóa nội dung: "WHERE PM.MaPhieu = {maPhieuIn}" ở cuối câu SQL
            string sqlIn = $@"
                SELECT 
                    PM.MaPhieu, 
                    DG.HoTen AS HoTenDocGia, 
                    PM.NgayMuon, 
                    PM.NgayHenTra, 
                    S.TenSach, 
                    CT.NgayTraThucTe, 
                    CT.TinhTrangSach
                FROM PhieuMuon PM
                JOIN DocGia DG ON PM.MaDocGia = DG.MaDocGia
                JOIN ChiTietPhieuMuon CT ON PM.MaPhieu = CT.MaPhieu
                JOIN Sach S ON CT.MaSach = S.MaSach";
            DataTable dtIn = db.laydl(sqlIn);

            if (dtIn == null || dtIn.Rows.Count == 0)
            {
                MessageBox.Show("Không có dữ liệu hoặc Chi tiết phiếu mượn trống. Lỗi, không thể in phiếu!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            // Gán TableName tuỳ chọn (Đảm bảo nó map đúng với Data Table bạn thiết kế trong Dataset QLTV)
            // dtIn.TableName = "Tên_DataTable_Trong_Dataset_Của_Bạn"; 

            try
            {
                // 1. Khởi tạo đối tượng Report mà bạn vừa tạo
                rptChiTietPhieuMuon rpt = new rptChiTietPhieuMuon();

                // Đổ dữ liệu từ DataTable vào Report
                rpt.SetDataSource(dtIn);

                // 2. Chuyển Report sang Form HienThiReport bạn đã tạo
                FrmHienThiReport frmReport = new FrmHienThiReport();
                frmReport.SetReport(rpt);

                frmReport.Text = "In Chi Tiết Phiếu Mượn - Hệ thống Quản Lý Thư Viện";
                frmReport.WindowState = FormWindowState.Maximized;
                frmReport.Icon = this.Icon;

                // 3. Hiển thị form
                frmReport.ShowDialog();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi mở Report: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnIn_Click_1(object sender, EventArgs e)
        {
            btnIn_Click(sender, e);
        }
    }
}
