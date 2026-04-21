using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Doan_QLTV.Froms
{
    public partial class FrmDocGia : Form
    {

        Database db = new Database("DESKTOP-LESSMLI\\SQLEXPRESS", "QuanLyThuVien");
        bool xulyThem = false;
        string id; // Lưu MaDocGia đang chọn

        public FrmDocGia()
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

            txtHoTen.Enabled = !giatri;
            txtEmail.Enabled = !giatri;
            txtSDT.Enabled = !giatri;
            dtpNgayLapThe.Enabled = !giatri;
            btnDoiAnh.Enabled = !giatri;
        }

        private string kiemTraDuongDan(string fileName)
        {
            if (string.IsNullOrEmpty(fileName)) return "";
            string fullPath = Path.Combine(Application.StartupPath, fileName);
            return File.Exists(fullPath) ? fullPath : "";
        }

        void LoadData()
        {
            DataTable dt = db.laydl("SELECT MaDocGia, HoTen, Email, SoDienThoai, NgayLapThe, HinhAnh FROM DocGia");
            dgvDocGia.DataSource = dt;

            // Ẩn cột đường dẫn ảnh (dạng text)
            if (dgvDocGia.Columns.Contains("HinhAnh"))
                dgvDocGia.Columns["HinhAnh"].Visible = false;

            // Tạo cột hình ảnh hiển thị (nếu chưa có)
            if (!dgvDocGia.Columns.Contains("HinhAnhHienThi"))
            {
                DataGridViewImageColumn imgCol = new DataGridViewImageColumn();
                imgCol.Name = "HinhAnhHienThi";
                imgCol.HeaderText = "Ảnh";
                imgCol.ImageLayout = DataGridViewImageCellLayout.Zoom;
                imgCol.Width = 60;
                dgvDocGia.Columns.Add(imgCol);
            }

            // Đổ ảnh vào từng dòng của GridView
            foreach (DataGridViewRow row in dgvDocGia.Rows)
            {
                if (row.Cells["HinhAnh"].Value != null && row.Cells["HinhAnh"].Value != DBNull.Value)
                {
                    string fileName = row.Cells["HinhAnh"].Value.ToString();
                    string path = kiemTraDuongDan(fileName);

                    if (!string.IsNullOrEmpty(path))
                    {
                        using (MemoryStream ms = new MemoryStream(File.ReadAllBytes(path)))
                        {
                            row.Cells["HinhAnhHienThi"].Value = Image.FromStream(ms);
                        }
                    }
                }
            }
        }

       

        private void FrmDocGia_Load(object sender, EventArgs e)
        {
            pbAnh.SizeMode = PictureBoxSizeMode.Zoom;
            dtpNgayLapThe.MaxDate = DateTime.Today;
            dtpNgayLapThe.Value = DateTime.Today;
            setButton(true);
            LoadData(); 
        }

       

        private void btnThem_Click(object sender, EventArgs e)
        {
            xulyThem = true;
            setButton(false);
            txtHoTen.Text = "";
            txtEmail.Text = "";
            txtSDT.Text = "";
            dtpNgayLapThe.Value = DateTime.Today;
            if (pbAnh.Image != null) pbAnh.Image.Dispose();
            pbAnh.Image = null;
            pbAnh.Tag = null;
            txtHoTen.Focus();
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(id)) return;
            xulyThem = false;
            setButton(false);
            txtHoTen.Focus();
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(id)) return;

            if (MessageBox.Show("Xóa độc giả này?", "Xác nhận", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                try
                {
                    SqlCommand cmd = new SqlCommand("DELETE FROM DocGia WHERE MaDocGia=@id", db.cn);
                    cmd.Parameters.AddWithValue("@id", id);
                    db.thucthi(cmd);
                    LoadData();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Không thể xóa độc giả này (có thể do đang mượn sách)!");
                }
            }
        }

        private void btnLuu_Click(object sender, EventArgs e)
        {
            // Kiểm tra rỗng
            if (txtHoTen.Text.Trim() == "" ||
                txtEmail.Text.Trim() == "" ||
                txtSDT.Text.Trim() == "")
            {
                MessageBox.Show("Cần nhập đầy đủ thông tin!");
                return;
            }

            // Kiểm tra chọn ảnh
            if (pbAnh.Image == null)
            {
                MessageBox.Show("Vui lòng chọn ảnh!");
                return;
            }

            // Họ tên không được có số
            if (txtHoTen.Text.Any(char.IsDigit))
            {
                MessageBox.Show("Họ tên không được chứa số!");
                return;
            }

            // Email phải có @
            if (!txtEmail.Text.Contains("@"))
            {
                MessageBox.Show("Email không hợp lệ!");
                return;
            }

            // SĐT chỉ được nhập số
            if (!txtSDT.Text.All(char.IsDigit))
            {
                MessageBox.Show("Số điện thoại không được chứa chữ hoặc ký tự đặc biệt!");
                txtSDT.Focus();
                return;
            }

            if (txtSDT.Text.Length != 10)
            {
                MessageBox.Show("Số điện thoại phải có đúng 10 số!");
                txtSDT.Focus();
                return;
            }

            if (dtpNgayLapThe.Value > DateTime.Now)
            {
                MessageBox.Show("Ngày lập thẻ không được vượt quá thời điểm hiện tại!");
                return;
            }

            try
            {
                string sql = "";
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = db.cn;

                // Xử lý tên file ảnh để lưu vào DB
                string tenFileAnh = (pbAnh.Tag != null) ? Path.GetFileName(pbAnh.Tag.ToString()) : "";

                if (xulyThem)
                {
                    sql = "INSERT INTO DocGia (HoTen, Email, SoDienThoai, NgayLapThe, HinhAnh) " +
                          "VALUES (@ten, @email, @sdt, @ngay, @anh)";
                }
                else
                {
                    sql = "UPDATE DocGia SET HoTen=@ten, Email=@email, SoDienThoai=@sdt, " +
                          "NgayLapThe=@ngay, HinhAnh=@anh WHERE MaDocGia=@id";
                    cmd.Parameters.AddWithValue("@id", id);
                }

                cmd.CommandText = sql;
                cmd.Parameters.AddWithValue("@ten", txtHoTen.Text);
                cmd.Parameters.AddWithValue("@email", txtEmail.Text);
                cmd.Parameters.AddWithValue("@sdt", txtSDT.Text);
                cmd.Parameters.AddWithValue("@ngay", dtpNgayLapThe.Value);
                cmd.Parameters.AddWithValue("@anh", tenFileAnh);

                db.thucthi(cmd);

                // Copy ảnh vào thư mục ứng dụng nếu người dùng có đổi ảnh
                if (pbAnh.Tag != null)
                {
                    string nguon = pbAnh.Tag.ToString();
                    string dich = Path.Combine(Application.StartupPath, tenFileAnh);
                    if (nguon != dich) File.Copy(nguon, dich, true);
                }

                MessageBox.Show("Lưu thành công!");
                setButton(true);
                LoadData();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi: " + ex.Message);
            }
        }

        private void btnHuy_Click(object sender, EventArgs e)
        {
            setButton(true);
            LoadData();
        }

        private void btnThoat_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        
        private void btnDoiAnh_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog ofd = new OpenFileDialog())
            {
                ofd.Filter = "Image Files (*.jpg;*.png)|*.jpg;*.png";
                if (ofd.ShowDialog() == DialogResult.OK)
                {
                    if (pbAnh.Image != null) pbAnh.Image.Dispose();
                    using (var stream = new FileStream(ofd.FileName, FileMode.Open, FileAccess.Read))
                    {
                        pbAnh.Image = Image.FromStream(stream);
                    }
                    pbAnh.Tag = ofd.FileName; // Lưu đường dẫn file gốc vào Tag để tí nữa copy
                }
            }
        }

        private void dgvDocGia_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;

            DataGridViewRow row = dgvDocGia.Rows[e.RowIndex];

            // QUAN TRỌNG: Gán giá trị cho biến id ở đây
            id = row.Cells["MaDocGia"].Value.ToString();

            txtMaDocGia.Text = id;
            txtHoTen.Text = row.Cells["HoTen"].Value.ToString();
            txtEmail.Text = row.Cells["Email"].Value.ToString();
            txtSDT.Text = row.Cells["SoDienThoai"].Value.ToString();

            if (row.Cells["NgayLapThe"].Value != DBNull.Value)
            {
                dtpNgayLapThe.Value =
                    Convert.ToDateTime(row.Cells["NgayLapThe"].Value).Date;
            }
            else
            {
                dtpNgayLapThe.Value = DateTime.Today;
            }

            // Thêm phần hiển thị ảnh lên PictureBox khi click vào dòng
            if (row.Cells["HinhAnh"].Value != DBNull.Value)
            {
                string fileName = row.Cells["HinhAnh"].Value.ToString();
                string path = kiemTraDuongDan(fileName);
                if (!string.IsNullOrEmpty(path))
                {
                    if (pbAnh.Image != null) pbAnh.Image.Dispose();
                    pbAnh.Image = Image.FromFile(path);
                }
            }

        }

        
    }
    
}
