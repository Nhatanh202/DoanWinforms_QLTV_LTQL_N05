using System;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace Doan_QLTV.Froms
{
    public partial class FrmSach : Form
    {
        bool isThem = false;
        Database db = new Database("DESKTOP-LESSMLI\\SQLEXPRESS", "QuanLyThuVien");

        public FrmSach()
        {
            InitializeComponent();
        }

        void SetControls(bool edit)
        {
            txtTenSach.Enabled = edit;
            txtTacGia.Enabled = edit;
            cboTenLoaiSach.Enabled = edit;
            txtSoLuongTon.Enabled = edit;
            dtpNamSanXuat.Enabled = edit;
            btnDoiAnh.Enabled = edit;

            btnThem.Enabled = !edit;
            btnSua.Enabled = !edit;
            btnXoa.Enabled = !edit;
            btnLuu.Enabled = edit;
            btnHuy.Enabled = edit;
            btnThoat.Enabled = !edit;
        }

        private string KiemTraDuongDan(string fileName)
        {
            if (string.IsNullOrEmpty(fileName)) return "";

            string fullPath = Path.Combine(Application.StartupPath, fileName);
            return File.Exists(fullPath) ? fullPath : "";
        }

        void LoadLoaiSach()
        {
            string sql = @"
            SELECT MaLoai,
            CAST(MaLoai AS NVARCHAR(10)) + ' - ' + TenLoai AS HienThi
            FROM TheLoai";

            cboTenLoaiSach.DataSource = db.laydl(sql);
            cboTenLoaiSach.DisplayMember = "HienThi";
            cboTenLoaiSach.ValueMember = "MaLoai";
        }

        void LoadData()
        {
            string sql = @"
            SELECT 
                s.MaSach,
                s.TenSach,
                s.TacGia,
                s.MaLoai,
                tl.TenLoai,
                s.NamXuatBan,
                s.SoLuongTon,
                s.HinhAnh
            FROM Sach s
            INNER JOIN TheLoai tl ON s.MaLoai = tl.MaLoai";

            dgvThongTinSach.DataSource = db.laydl(sql);

            if (dgvThongTinSach.Columns.Contains("HinhAnh"))
                dgvThongTinSach.Columns["HinhAnh"].Visible = false;

            if (!dgvThongTinSach.Columns.Contains("AnhHienThi"))
            {
                DataGridViewImageColumn img = new DataGridViewImageColumn();
                img.Name = "AnhHienThi";
                img.HeaderText = "Ảnh";
                img.Width = 60;
                img.ImageLayout = DataGridViewImageCellLayout.Zoom;
                dgvThongTinSach.Columns.Add(img);
            }

            foreach (DataGridViewRow row in dgvThongTinSach.Rows)
            {
                if (row.Cells["HinhAnh"].Value != null &&
                    row.Cells["HinhAnh"].Value != DBNull.Value)
                {
                    string file = row.Cells["HinhAnh"].Value.ToString();
                    string path = KiemTraDuongDan(file);

                    if (!string.IsNullOrEmpty(path))
                    {
                        using (MemoryStream ms =
                            new MemoryStream(File.ReadAllBytes(path)))
                        {
                            row.Cells["AnhHienThi"].Value =
                                Image.FromStream(ms);
                        }
                    }
                }
            }
        }

        private void FrmSach_Load(object sender, EventArgs e)
        {
            pbAnh.SizeMode = PictureBoxSizeMode.Zoom;
            SetControls(false);
            LoadLoaiSach();
            LoadData();
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            isThem = true;
            SetControls(true);

            txtMaSach.Text = "";
            txtTenSach.Text = "";
            txtTacGia.Text = "";
            txtSoLuongTon.Text = "0";
            cboTenLoaiSach.SelectedIndex = 0;

            pbAnh.Image = null;
            pbAnh.Tag = null;

            txtTenSach.Focus();
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            if (txtMaSach.Text == "") return;

            isThem = false;
            SetControls(true);
            txtTenSach.Focus();
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            if (txtMaSach.Text == "") return;

            if (MessageBox.Show("Xóa sách này?", "Xác nhận",
                MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                SqlCommand cmd = new SqlCommand(
                    "DELETE FROM Sach WHERE MaSach=@id", db.cn);

                cmd.Parameters.AddWithValue("@id", txtMaSach.Text);
                db.thucthi(cmd);

                LoadData();
            }
        }

        private void btnLuu_Click(object sender, EventArgs e)
        {
            // Kiểm tra rỗng
            if (txtTenSach.Text.Trim() == "" ||
                txtTacGia.Text.Trim() == "" ||
                txtSoLuongTon.Text.Trim() == "")
            {
                MessageBox.Show("Nhập đầy đủ thông tin!");
                return;
            }

            // Phải chọn ảnh
            if (pbAnh.Image == null)
            {
                MessageBox.Show("Vui lòng chọn ảnh!");
                return;
            }

            // ===== RÀNG BUỘC TÊN TÁC GIẢ =====
            if (txtTacGia.Text.Any(char.IsDigit))
            {
                MessageBox.Show("Tên tác giả không được chứa số!");
                txtTacGia.Focus();
                return;
            }

            // ===== RÀNG BUỘC SỐ LƯỢNG =====
            int sl;
            if (!int.TryParse(txtSoLuongTon.Text, out sl))
            {
                MessageBox.Show("Số lượng tồn phải là số!");
                txtSoLuongTon.Focus();
                return;
            }

            if (sl < 0)
            {
                MessageBox.Show("Số lượng tồn không được âm!");
                txtSoLuongTon.Focus();
                return;
            }

            // ===== RÀNG BUỘC NĂM XUẤT BẢN =====
            if (dtpNamSanXuat.Value.Year > DateTime.Now.Year)
            {
                MessageBox.Show("Năm xuất bản không hợp lệ!");
                return;
            }

            string tenFile = "";

            if (pbAnh.Tag != null)
                tenFile = @"Images\Sach\" + Path.GetFileName(pbAnh.Tag.ToString());

            try
            {
                SqlCommand cmd;

                if (isThem)
                {
                    cmd = new SqlCommand(@"
            INSERT INTO Sach
            (TenSach,TacGia,MaLoai,NamXuatBan,SoLuongTon,HinhAnh)
            VALUES
            (@ten,@tg,@loai,@nam,@sl,@anh)", db.cn);
                }
                else
                {
                    cmd = new SqlCommand(@"
            UPDATE Sach SET
            TenSach=@ten,
            TacGia=@tg,
            MaLoai=@loai,
            NamXuatBan=@nam,
            SoLuongTon=@sl,
            HinhAnh=@anh
            WHERE MaSach=@id", db.cn);

                    cmd.Parameters.AddWithValue("@id", txtMaSach.Text);
                }

                cmd.Parameters.AddWithValue("@ten", txtTenSach.Text);
                cmd.Parameters.AddWithValue("@tg", txtTacGia.Text);
                cmd.Parameters.AddWithValue("@loai", cboTenLoaiSach.SelectedValue);
                cmd.Parameters.AddWithValue("@nam", dtpNamSanXuat.Value.Year);
                cmd.Parameters.AddWithValue("@sl", sl);
                cmd.Parameters.AddWithValue("@anh", tenFile);

                db.thucthi(cmd);

                if (pbAnh.Tag != null)
                {
                    string thuMuc = Path.Combine(Application.StartupPath, "Images\\Sach");

                    if (!Directory.Exists(thuMuc))
                        Directory.CreateDirectory(thuMuc);

                    string dich = Path.Combine(
                        thuMuc,
                        Path.GetFileName(pbAnh.Tag.ToString())
                    );

                    File.Copy(pbAnh.Tag.ToString(), dich, true);
                }

                MessageBox.Show("Lưu thành công!");
                SetControls(false);
                LoadData();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi: " + ex.Message);
            }
        }

        private void btnHuy_Click(object sender, EventArgs e)
        {
            SetControls(false);
            LoadData();
        }

        private void btnThoat_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void btnDoiAnh_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = "Image Files|*.jpg;*.jpeg;*.png;*.bmp";

            if (ofd.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    if (pbAnh.Image != null)
                    {
                        pbAnh.Image.Dispose();
                        pbAnh.Image = null;
                    }

                    byte[] bytes = File.ReadAllBytes(ofd.FileName);

                    using (MemoryStream ms = new MemoryStream(bytes))
                    {
                        using (Image img = Image.FromStream(ms))
                        {
                            pbAnh.Image = new Bitmap(img);
                        }
                    }

                    pbAnh.Tag = ofd.FileName;
                }
                catch
                {
                    MessageBox.Show("Ảnh này không hỗ trợ hoặc bị lỗi.");
                }
            }
        }

        private void dgvThongTinSach_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;

            DataGridViewRow row = dgvThongTinSach.Rows[e.RowIndex];

            txtMaSach.Text = row.Cells["MaSach"].Value.ToString();
            txtTenSach.Text = row.Cells["TenSach"].Value.ToString();
            txtTacGia.Text = row.Cells["TacGia"].Value.ToString();
            txtSoLuongTon.Text = row.Cells["SoLuongTon"].Value.ToString();

            cboTenLoaiSach.SelectedValue = row.Cells["MaLoai"].Value;

            if (row.Cells["NamXuatBan"].Value != null &&
                row.Cells["NamXuatBan"].Value != DBNull.Value)
            {
                int nam = Convert.ToInt32(row.Cells["NamXuatBan"].Value);
                dtpNamSanXuat.Value = new DateTime(nam, 1, 1);
            }
            else
            {
                dtpNamSanXuat.Value = DateTime.Today;
            }

            if (row.Cells["HinhAnh"].Value != null &&
                row.Cells["HinhAnh"].Value != DBNull.Value)
            {
                string file = row.Cells["HinhAnh"].Value.ToString();
                string path = KiemTraDuongDan(file);

                if (!string.IsNullOrEmpty(path))
                    pbAnh.Image = Image.FromFile(path);
            }
        }
    }
}