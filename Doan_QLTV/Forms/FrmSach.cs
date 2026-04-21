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
using ClosedXML.Excel;

namespace Doan_QLTV.Froms
{
    public partial class FrmSach : Form
    {

        bool isThem = false; // Kiểm tra xem đang ở chế độ Thêm hay Sửa
        Database db = new Database("ADMIN-PC\\SQLEXPRESS", "QuanLyThuVien");

        void SetControls(bool edit)
        {
            // TextBox
            txtTenSach.Enabled = edit;
            txtTacGia.Enabled = edit;
            txtMaLoai.Enabled = edit;
            txtSoLuongTon.Enabled = edit;
            dtpNamSanXuat.Enabled = edit;

            // Button
            btnThem.Enabled = !edit;
            btnSua.Enabled = !edit;
            btnXoa.Enabled = !edit;
            btnLuu.Enabled = edit;
            btnHuy.Enabled = edit;
            btnThoat.Enabled = !edit;
        }

        void LoadData()
        {
            string sql = "SELECT * FROM Sach";
            dgvThongTinSach.DataSource = db.laydl(sql);
        }
        public FrmSach()
        {
            InitializeComponent();
        }

        private void FrmSach_Load(object sender, EventArgs e)
        {
            try 
            {
                LoadData();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi kết nối: " + ex.Message);
            }
        }

        private void dgvThongTinSach_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = dgvThongTinSach.Rows[e.RowIndex];
                txtMaSach.Text = row.Cells["MaSach"].Value.ToString();
                txtTenSach.Text = row.Cells["TenSach"].Value.ToString();
                txtTacGia.Text = row.Cells["TacGia"].Value.ToString();
                txtMaLoai.Text = row.Cells["MaLoai"].Value.ToString();
                txtSoLuongTon.Text = row.Cells["SoLuongTon"].Value.ToString();


                // Kiểm tra ô dữ liệu không trống
                if (row.Cells["NamXuatBan"].Value != null && !string.IsNullOrEmpty(row.Cells["NamXuatBan"].Value.ToString()))
                {
                    int namXB;
                    if (int.TryParse(row.Cells["NamXuatBan"].Value.ToString(), out namXB))
                    {
                        // Kiểm tra năm hợp lệ rồi mới gán
                        if (namXB >= 1753 && namXB <= 9999)
                        {
                            dtpNamSanXuat.Value = new DateTime(namXB, 1, 1);
                        }
                    }
                }
                else
                {
                    dtpNamSanXuat.Value = DateTime.Now;
                }
                // ĐỪNG để dòng gán dtpNamSanXuat.Value ở ngoài này vì namXB sẽ không tồn tại ở đây
            }
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            isThem = true;
            SetControls(true);
            // Xóa trắng các ô nhập
            txtMaSach.Text = "";
            txtTenSach.Text = "";
            txtTacGia.Text = "";
            txtSoLuongTon.Text = "0";
            txtTenSach.Focus();
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtMaSach.Text)) return;
            isThem = false;
            SetControls(true);
            txtTenSach.Focus();
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtMaSach.Text)) return;

            if (MessageBox.Show("Bạn có chắc chắn muốn xóa sách này?", "Xác nhận", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                string sql = "DELETE FROM Sach WHERE MaSach = @id";
                SqlCommand cmd = new SqlCommand(sql, db.cn);
                cmd.Parameters.AddWithValue("@id", txtMaSach.Text);
                db.thucthi(cmd);
                LoadData();
            }
        }

        private void btnLuu_Click(object sender, EventArgs e)
        {
            string sql = "";
            if (isThem)
            {
                sql = "INSERT INTO Sach (TenSach, TacGia, MaLoai, NamXuatBan, SoLuongTon) " +
                      "VALUES (@ten, @tg, @loai, @nam, @sl)";
            }
            else
            {
                sql = "UPDATE Sach SET TenSach=@ten, TacGia=@tg, MaLoai=@loai, " +
                      "NamXuatBan=@nam, SoLuongTon=@sl WHERE MaSach=@id";
            }

            SqlCommand cmd = new SqlCommand(sql, db.cn);
            cmd.Parameters.AddWithValue("@ten", txtTenSach.Text);
            cmd.Parameters.AddWithValue("@tg", txtTacGia.Text);
            cmd.Parameters.AddWithValue("@loai", txtMaLoai.Text);
            cmd.Parameters.AddWithValue("@nam", dtpNamSanXuat.Value.Year); // Lấy năm từ DateTimePicker
            cmd.Parameters.AddWithValue("@sl", txtSoLuongTon.Text);
            if (!isThem) cmd.Parameters.AddWithValue("@id", txtMaSach.Text);

            try
            {
                db.thucthi(cmd);
                MessageBox.Show("Cập nhật thành công!");
                LoadData();
                SetControls(false);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi: " + ex.Message);
            }
        }

        private void btnHuy_Click(object sender, EventArgs e)
        {
            SetControls(false);
            LoadData(); // Quay lại dữ liệu cũ
        }

        private void btnThoat_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnNhap_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Title = "Nhập dữ liệu từ file Excel";
            ofd.Filter = "Tập tin Excel|*.xls;*.xlsx";

            if (ofd.ShowDialog() == DialogResult.OK)
            {
                DataTable dt = new DataTable();

                XLWorkbook workbook = new XLWorkbook(ofd.FileName);
                IXLWorksheet worksheet = workbook.Worksheet(1);

                bool firstRow = true;

                foreach (IXLRow row in worksheet.RowsUsed())
                {
                    if (firstRow)
                    {
                        foreach (IXLCell cell in row.Cells())
                            dt.Columns.Add(cell.Value.ToString());
                        firstRow = false;
                    }
                    else
                    {
                        dt.Rows.Add();
                        int i = 0;

                        foreach (IXLCell cell in row.Cells(1, dt.Columns.Count))
                        {
                            dt.Rows[dt.Rows.Count - 1][i] = cell.Value.ToString();
                            i++;
                        }
                    }
                }

                if (dt.Rows.Count == 0)
                {
                    MessageBox.Show("Tập tin Excel rỗng");
                    return;
                }

                foreach (DataRow r in dt.Rows)
                {
                    string tenSach = dt.Columns.Contains("TenSach") ? r["TenSach"].ToString() : "";
                    string tacGia = dt.Columns.Contains("TacGia") ? r["TacGia"].ToString() : "";
                    string maLoai = dt.Columns.Contains("MaLoai") ? r["MaLoai"].ToString() : "";
                    string namXuatBan = dt.Columns.Contains("NamXuatBan") ? r["NamXuatBan"].ToString() : "";
                    string soLuongTon = dt.Columns.Contains("SoLuongTon") ? r["SoLuongTon"].ToString() : "";
                    
                    if (!string.IsNullOrWhiteSpace(tenSach))
                    {
                        string sql = $"INSERT INTO Sach (TenSach, TacGia, MaLoai, NamXuatBan, SoLuongTon) VALUES (N'{tenSach}', N'{tacGia}', '{maLoai}', '{namXuatBan}', '{soLuongTon}')";
                        SqlCommand cmd = new SqlCommand(sql, db.cn);
                        db.thucthi(cmd);
                    }
                }

                MessageBox.Show("Đã import thành công!");
                FrmSach_Load(sender, e);
            }
        }

        private void btnXuat_Click(object sender, EventArgs e)
        {
            SaveFileDialog sfd = new SaveFileDialog();
            sfd.Filter = "Excel Files|*.xlsx";
            sfd.Title = "Xuất dữ liệu ra file Excel";
            sfd.FileName = "DanhSachSach.xlsx";

            if (sfd.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    using (XLWorkbook workbook = new XLWorkbook())
                    {
                        DataTable dt = db.laydl("SELECT * FROM Sach");
                        workbook.Worksheets.Add(dt, "Sach");
                        workbook.SaveAs(sfd.FileName);
                    }
                    MessageBox.Show("Xuất file Excel thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi xuất file: " + ex.Message);
                }
            }
        }
    }
}

        