using ClosedXML.Excel;
using Doan_QLTV.Forms;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Doan_QLTV.Froms
{
    public partial class FrmMuonTraSach : Form
    {
        Database db;
        bool xulyThem = false;
        int id;
        public FrmMuonTraSach()
        {
            InitializeComponent();
        }
        private void setButton(bool giatri)
        {
            btnThem.Enabled = giatri;
            btnSua.Enabled = giatri;
            btnXoa.Enabled = giatri;

            btnLuu.Enabled = !giatri;
            btnHuy.Enabled = !giatri;
            btnHuy.Enabled = !giatri;

        }
        private void loadComboBox()
        {
            try
            {
                string sqlDG = "SELECT MaDocGia, HoTen FROM DocGia";
                DataTable dtDG = db.laydl(sqlDG);
                cboDocGia.DataSource = dtDG;
                cboDocGia.DisplayMember = "HoTen";      
                cboDocGia.ValueMember = "MaDocGia";     
                string sqlNV = "SELECT MaNV, HoTen FROM NhanVien";
                DataTable dtNV = db.laydl(sqlNV);
                cboNhanvien.DataSource = dtNV;
                cboNhanvien.DisplayMember = "HoTen";
                cboNhanvien.ValueMember = "MaNV";
                cboNhanvien.SelectedIndex = -1;
                cboDocGia.SelectedIndex = -1;
  
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi nạp danh sách: " + ex.Message);
            }
        }
        private void FrmMuonTraSach_Load(object sender, EventArgs e)
        {
            db = new Database(@"LAPTOP-08CQM6N0", "QuanLyThuVien");
            string sql = @"SELECT 
                pm.MaPhieu AS N'Mã Phiếu', 
                dg.HoTen AS N'Tên Độc Giả', 
                nv.HoTen AS N'Nhân Viên Lập', 
                pm.NgayMuon AS N'Ngày Mượn', 
                pm.NgayHenTra AS N'Ngày Trả', 
                pm.TrangThai AS N'Trạng Thái'
              FROM PhieuMuon pm
              JOIN DocGia dg ON pm.MaDocGia = dg.MaDocGia
              LEFT JOIN NhanVien nv ON pm.MaNV = nv.MaNV";
            DataTable dt = db.laydl(sql);
            dgvMuonTra.DataSource = dt;
            loadComboBox();
             

        }

        private void dgvMuonTra_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = dgvMuonTra.Rows[e.RowIndex];
                setButton(true);
            }
        }

       
        private void btnThem_Click(object sender, EventArgs e)
        {
            setButton(false);
            txtMaPhieu.Clear();
            txtTrangThai.Clear();
            xulyThem = true;
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            if (dgvMuonTra.CurrentRow != null)
            {
                setButton(false);
                xulyThem = false;
                txtMaPhieu.Text = dgvMuonTra.CurrentRow.Cells[0].Value.ToString();
                txtTrangThai.Text = dgvMuonTra.CurrentRow.Cells[5].Value.ToString();
                txtMaPhieu.ReadOnly = true;
                txtTrangThai.Focus(); 
            }
            else
            {
                MessageBox.Show("Vui lòng chọn một phiếu mượn để sửa.");
            }
        }

        private void btnLuu_Click(object sender, EventArgs e)
        {
            if (cboDocGia.SelectedValue == null || cboNhanvien.SelectedValue == null)
            {
                MessageBox.Show("Vui lòng chọn đầy đủ Độc giả và Nhân viên!");
                return;
            }

            try
            {
                SqlCommand cmd;
                if (xulyThem)
                {
                    // 1. Thêm NgayHenTra vào câu SQL
                    string sql = "INSERT INTO PhieuMuon (MaDocGia, MaNV, NgayMuon, NgayHenTra, TrangThai) " +
                                 "VALUES (@MaDG, @MaNV, @NgayMuon, @NgayHT, @TrangThai)";

                    cmd = new SqlCommand(sql, db.cn);

                    // 2. Truyền giá trị cho NgayMuon và NgayHenTra
                    DateTime ngayMuon = DateTime.Now;
                    DateTime ngayHenTra = ngayMuon.AddDays(7); // Mặc định cho mượn 7 ngày

                    cmd.Parameters.AddWithValue("@NgayMuon", ngayMuon);
                    cmd.Parameters.AddWithValue("@NgayHT", ngayHenTra); // Fix lỗi NULL tại đây
                }
                else
                {
                    string sql = "UPDATE PhieuMuon SET MaDocGia=@MaDG, MaNV=@MaNV, NgayHenTra=@NgayHT, TrangThai=@TrangThai WHERE MaPhieu=@MaPM";
                    cmd = new SqlCommand(sql, db.cn);
                    cmd.Parameters.AddWithValue("@MaPM", id);

                    // Giả sử lấy ngày hẹn trả mới từ DateTimePicker trên giao diện
                    cmd.Parameters.AddWithValue("@NgayHT", dtpNgayTra.Value);
                }

                // GÁN ĐÚNG GIÁ TRỊ TỪ COMBOBOX
                cmd.Parameters.AddWithValue("@MaDG", cboDocGia.SelectedValue);
                cmd.Parameters.AddWithValue("@MaNV", cboNhanvien.SelectedValue);
                cmd.Parameters.AddWithValue("@TrangThai", txtTrangThai.Text);

                db.thucthi(cmd);
                MessageBox.Show("Thành công!");
                setButton(true);
                FrmMuonTraSach_Load(sender, e);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi: " + ex.Message);
            }
        }

        private void btnHuy_Click(object sender, EventArgs e)
        {
            FrmMuonTraSach_Load(sender, e); 
        }

        private void btnThoat_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnTim_Click(object sender, EventArgs e)
        {
            string tuKhoa = txtMaPhieu.Text.Trim();
            if (string.IsNullOrEmpty(tuKhoa))
            {
                MessageBox.Show("Vui lòng nhập từ khóa cần tìm (Mã phiếu hoặc Tên độc giả)!", "Thông báo");
                FrmMuonTraSach_Load(sender, e);
                return;
            }

            try
            {
                string sql = $@"SELECT 
                        pm.MaPhieu AS N'Mã Phiếu', 
                        dg.HoTen AS N'Tên Độc Giả', 
                        nv.HoTen AS N'Nhân Viên Lập', 
                        pm.NgayMuon AS N'Ngày Mượn', 
                        pm.NgayHenTra AS N'Ngày Trả', 
                        pm.TrangThai AS N'Trạng Thái'
                      FROM PhieuMuon pm
                      JOIN DocGia dg ON pm.MaDocGia = dg.MaDocGia
                      LEFT JOIN NhanVien nv ON pm.MaNV = nv.MaNV
                      WHERE CAST(pm.MaPhieu AS VARCHAR) LIKE '%{tuKhoa}%' 
                         OR dg.HoTen LIKE N'%{tuKhoa}%'
                         OR pm.TrangThai LIKE N'%{tuKhoa}%'";

                DataTable dt = db.laydl(sql);
                if (dt != null && dt.Rows.Count > 0)
                {
                    dgvMuonTra.DataSource = dt;
                }
                else
                {
                    MessageBox.Show("Không tìm thấy kết quả nào khớp với từ khóa!", "Thông báo");
                    dgvMuonTra.DataSource = null;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi tìm kiếm: " + ex.Message);
            }
        }
        private void btnXoa_Click(object sender, EventArgs e)
        {
            if (dgvMuonTra.CurrentRow == null)
            {
                MessageBox.Show("Vui lòng chọn một phiếu mượn để xóa!", "Thông báo");
                return;
            }

            // 2. Xác nhận trước khi xóa (tránh bấm nhầm)
            DialogResult dr = MessageBox.Show("Bạn có chắc chắn muốn xóa phiếu mượn này và toàn bộ chi tiết liên quan không?",
                                              "Xác nhận xóa", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (dr == DialogResult.Yes)
            {
                try
                {
                    if (db.cn.State == ConnectionState.Closed) db.cn.Open();

                    // Lấy mã phiếu từ dòng đang chọn (Cột 0)
                    int maPhieuXoa = Convert.ToInt32(dgvMuonTra.CurrentRow.Cells[0].Value);

                    // Xóa chi tiết phiếu mượn trước (Ràng buộc khóa ngoại)
                    string sqlXoaChiTiet = "DELETE FROM ChiTietPhieuMuon WHERE MaPhieu = @MaP";
                    SqlCommand cmdChiTiet = new SqlCommand(sqlXoaChiTiet, db.cn);
                    cmdChiTiet.Parameters.AddWithValue("@MaP", maPhieuXoa);
                    cmdChiTiet.ExecuteNonQuery();

                    // Sau đó mới xóa phiếu mượn
                    string sql = "DELETE FROM PhieuMuon WHERE MaPhieu = @MaP";
                    SqlCommand cmd = new SqlCommand(sql, db.cn);
                    cmd.Parameters.AddWithValue("@MaP", maPhieuXoa);
                    cmd.ExecuteNonQuery();

                    db.cn.Close();

                    MessageBox.Show("Đã xóa phiếu mượn thành công!", "Thành công");

                    // Load lại dữ liệu để cập nhật Grid
                    FrmMuonTraSach_Load(sender, e);
                }
                catch (Exception ex)
                {
                    if (db.cn.State == ConnectionState.Open) db.cn.Close();
                    // Nếu Trigger trg_KhongXoaDocGia hoặc các ràng buộc khác chặn lại, lỗi sẽ hiện ở đây
                    MessageBox.Show("Không thể xóa phiếu mượn này! \nLỗi chi tiết: " + ex.Message,
                                    "Lỗi hệ thống", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void dgvMuonTra_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                try
                {
                    // 2. Lấy giá trị từ cột đầu tiên (Index 0 - Mã Phiếu)
                    // Ép kiểu trực tiếp sang int để đảm bảo dữ liệu chuẩn
                    var cellValue = dgvMuonTra.Rows[e.RowIndex].Cells[0].Value;

                    if (cellValue != null && cellValue != DBNull.Value)
                    {
                        int ma = Convert.ToInt32(cellValue);

                        // 3. Mở Form và truyền mã 'ma' vào Constructor
                        // Đảm bảo dùng biến 'ma' chứ không phải ID hay biến khác
                        FrmChiTietPhieuMuon frm = new FrmChiTietPhieuMuon(ma);
                        frm.ShowDialog();

                        // 4. Sau khi đóng form chi tiết, load lại Grid chính
                        FrmMuonTraSach_Load(sender, e);
                    }
                    else
                    {
                        MessageBox.Show("Dòng được chọn không có dữ liệu Mã Phiếu!");
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi khi truyền mã phiếu: " + ex.Message);
                }
            }
        }

        private void btnXuat_Click(object sender, EventArgs e)
        {
            try
            {
                string sql = "SELECT DG.HoTen as [Họ Tên Độc Giả], NV.HoTen as [Nhân Viên Lập], " +
                             "PM.NgayMuon as [Ngày Mượn], PM.NgayHenTra as [Ngày Hẹn Trả] " +
                             "FROM PHIEUMUON PM, DOCGIA DG, NHANVIEN NV " + // Kiểm tra tên bảng DOCGIA hay KHACHHANG
                             "WHERE PM.MaDocGia = DG.MaDocGia AND PM.MaNV = NV.MaNV";

                DataTable dt = db.laydl(sql);

                // 2. Thiết lập hộp thoại lưu file
                SaveFileDialog sfd = new SaveFileDialog();
                sfd.Filter = "Excel Workbook (*.xlsx)|*.xlsx";
                sfd.FileName = "Báo cáo danh sách Mượn - Trả";

                if (sfd.ShowDialog() == DialogResult.OK)
                {
                    // 3. Sử dụng ClosedXML để tạo file
                    using (XLWorkbook workbook = new XLWorkbook())
                    {
                        var worksheet = workbook.Worksheets.Add(dt, "Doanh Thu");

                        // Định dạng tiêu đề in đậm và tự căn rộng cột
                        worksheet.Row(1).Style.Font.Bold = true;
                        worksheet.Columns().AdjustToContents();

                        workbook.SaveAs(sfd.FileName);
                    }
                    MessageBox.Show("Xuất file Excel thành công!", "Thông báo");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi: " + ex.Message);
            }
        }

        private void btnNhap_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = "Excel Workbook (*.xlsx)|*.xlsx";

            if (ofd.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    using (var workbook = new XLWorkbook(ofd.FileName))
                    {
                        // 1. Lấy Worksheet đầu tiên
                        var worksheet = workbook.Worksheet(1);
                        var rows = worksheet.RangeUsed().RowsUsed().Skip(1); // Bỏ qua dòng tiêu đề

                        int count = 0;
                        foreach (var row in rows)
                        {
                            // 2. Lấy dữ liệu từng cột (Phải khớp với file Excel của ní)
                            string maDG = row.Cell(1).Value.ToString();
                            string maNV = row.Cell(2).Value.ToString();
                            DateTime ngayMuon = row.Cell(3).GetDateTime();
                            DateTime ngayHenTra = row.Cell(4).GetDateTime();
                            string trangThai = row.Cell(5).Value.ToString();

                            // 3. Tạo câu lệnh SQL INSERT (Dùng Parameter để tránh lỗi NULL và Format ngày)
                            string sql = "INSERT INTO PhieuMuon (MaDocGia, MaNV, NgayMuon, NgayHenTra, TrangThai) " +
                                         "VALUES (@MaDG, @MaNV, @NgayMuon, @NgayHT, @TrangThai)";

                            SqlCommand cmd = new SqlCommand(sql, db.cn);
                            cmd.Parameters.AddWithValue("@MaDG", maDG);
                            cmd.Parameters.AddWithValue("@MaNV", maNV);
                            cmd.Parameters.AddWithValue("@NgayMuon", ngayMuon);
                            cmd.Parameters.AddWithValue("@NgayHT", ngayHenTra); // Fix triệt để lỗi NULL NgayHenTra nè!
                            cmd.Parameters.AddWithValue("@TrangThai", trangThai);

                            if (db.cn.State == ConnectionState.Closed) db.cn.Open();
                            cmd.ExecuteNonQuery();
                            count++;
                        }

                        MessageBox.Show($"Đã nhập thành công {count} phiếu mượn từ Excel!", "Thông báo");
                        // Load lại dữ liệu lên Grid để kiểm tra
                        // loadData(); 
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi khi nhập file: " + ex.Message);
                }
            }
        }
    }
}
