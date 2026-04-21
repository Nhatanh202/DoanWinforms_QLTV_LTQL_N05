using Doan_QLTV.Models;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace Doan_QLTV.Forms
{
    public partial class FrmNhanVien : Form
    {
        Database db = new Database(@".\SQLEXPRESS", "QuanLyThuVien");
        private bool xuLyThem = false;

        public FrmNhanVien()
        {
            InitializeComponent();
        }

        private void BatTatChucNang(bool giaTri)
        {
            btnLuu.Enabled = giaTri;
            btnHuyBo.Enabled = giaTri;
            txtHoVaTen.Enabled = giaTri;
            cboChucVu.Enabled = giaTri;
            btnChonAnh.Enabled = giaTri;

            btnThem.Enabled = !giaTri;
            btnSua.Enabled = !giaTri;
            btnXoa.Enabled = !giaTri;

            txtMaNV.Enabled = false;
        }

        private void frmNhanVien_Load(object sender, EventArgs e)
        {
            BatTatChucNang(false);
            dataGridView.AutoGenerateColumns = false;
            // Thiết lập Column ảnh (Có thể chỉnh trong Designer để bỏ bớt đoạn code này)
            if (dataGridView.Columns.Contains("ColAnh"))
            {
                var col = (DataGridViewImageColumn)dataGridView.Columns["ColAnh"];
                col.DataPropertyName = "HinhAnh"; // Khớp với tên cột trong SQL
                col.ImageLayout = DataGridViewImageCellLayout.Zoom;
            }
            RefreshBinding();
        }

        private void RefreshBinding()
        {
            DataTable dt = db.laydl("SELECT MaNV, HoTen, ChucVu, HinhAnh FROM NhanVien");
            dataGridView.DataSource = dt;

            txtMaNV.DataBindings.Clear();
            txtHoVaTen.DataBindings.Clear();
            cboChucVu.DataBindings.Clear();

            txtMaNV.DataBindings.Add("Text", dt, "MaNV");
            txtHoVaTen.DataBindings.Add("Text", dt, "HoTen");
            cboChucVu.DataBindings.Add("Text", dt, "ChucVu");
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            xuLyThem = true;
            BatTatChucNang(true);
            txtMaNV.Clear();
            txtHoVaTen.Clear();
            cboChucVu.Text = "";
            picAnh.Image = null;
        }
        private void btnSua_Click(object sender, EventArgs e)
        {
            if (dataGridView.CurrentRow == null) return;
            xuLyThem = false;
            BatTatChucNang(true);
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Xác nhận xóa nhân viên này?", "Xác nhận", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                SqlCommand cmd = new SqlCommand("DELETE FROM NhanVien WHERE MaNV = @manv", db.cn);
                cmd.Parameters.AddWithValue("@manv", txtMaNV.Text);
                db.thucthi(cmd);
                RefreshBinding();
            }
        }

        private void btnLuu_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtHoVaTen.Text))
            {
                MessageBox.Show("Vui lòng nhập họ và tên!");
                return;
            }

            using (SqlCommand cmd = new SqlCommand())
            {
                byte[] anh = ImageToBytes(picAnh.Image);
                if (xuLyThem)
                    cmd.CommandText = "INSERT INTO NhanVien (HoTen, ChucVu, HinhAnh) VALUES (@hoten, @chucvu, @anh)";
                else
                {
                    cmd.CommandText = "UPDATE NhanVien SET HoTen=@hoten, ChucVu=@chucvu, HinhAnh=@anh WHERE MaNV=@manv";
                    cmd.Parameters.AddWithValue("@manv", txtMaNV.Text);
                }

                cmd.Parameters.AddWithValue("@hoten", txtHoVaTen.Text.Trim());
                cmd.Parameters.AddWithValue("@chucvu", cboChucVu.Text);
                cmd.Parameters.Add("@anh", SqlDbType.Image).Value = (object)anh ?? DBNull.Value;

                cmd.Connection = db.cn;
                try
                {
                    db.thucthi(cmd);
                    MessageBox.Show("Thành công!");
                    RefreshBinding();
                    BatTatChucNang(false);
                }
                catch (Exception ex) { MessageBox.Show("Lỗi: " + ex.Message); }
            }
        }

        private void btnHuyBo_Click(object sender, EventArgs e)
        {
            frmNhanVien_Load(sender, e);
        }

        private void btnThoat_Click(object sender, EventArgs e)
        { 
            this.Close();
        }

        // 1. Nút chọn ảnh: Ngắn gọn, đọc file và hiển thị thẳng vào PictureBox
        private void btnChonAnh_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog ofd = new OpenFileDialog { Filter = "Images|*.jpg;*.png;*.bmp" })
            {
                if (ofd.ShowDialog() == DialogResult.OK)
                {
                    // Giải phóng ảnh cũ để tránh nặng máy, sau đó nạp ảnh mới
                    if (picAnh.Image != null) picAnh.Image.Dispose();
                    picAnh.Image = Image.FromFile(ofd.FileName);
                }
            }
        }

        // 2. Chuyển ảnh từ PictureBox thành Byte[] để lưu vào SQL
        private byte[] ImageToBytes(Image img)
        {
            if (img == null) return null;
            using (MemoryStream ms = new MemoryStream())
            {
                // Copy ảnh ra một bitmap mới để tránh lỗi "vướng" file đang mở
                new Bitmap(img).Save(ms, System.Drawing.Imaging.ImageFormat.Png);
                return ms.ToArray();
            }
        }

        // 3. Hiển thị ảnh khi chọn dòng trên DataGridView
        private void dataGridView_SelectionChanged(object sender, EventArgs e)
        {
            if (dataGridView.CurrentRow?.DataBoundItem is DataRowView row)
            {
                var data = row["HinhAnh"];
                if (data != DBNull.Value)
                {
                    using (var ms = new MemoryStream((byte[])data))
                    {
                        if (picAnh.Image != null) picAnh.Image.Dispose();
                        picAnh.Image = new Bitmap(ms); // Dùng Bitmap để clone ảnh, giải phóng stream ngay
                    }
                }
                else picAnh.Image = null;
            }
        }

        // 4. Hiển thị ảnh trực tiếp trong cột DataGridView
        private void dataGridView_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (dataGridView.Columns[e.ColumnIndex].Name == "ColAnh" && e.Value is byte[] bytes)

            {

                using (var ms = new MemoryStream(bytes))

                    e.Value = new Bitmap(ms);

            }
        }

        private void txtHoVaTen_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsLetter(e.KeyChar) && e.KeyChar != ' ' && !char.IsControl(e.KeyChar))
                e.Handled = true;
        }
    }
}
