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
    public partial class FrmSach : Form
    {

        bool isThem = false; // Kiểm tra xem đang ở chế độ Thêm hay Sửa
        Database db = new Database("DESKTOP-LESSMLI\\SQLEXPRESS", "QuanLyThuVien");

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
            // Kiểm tra rỗng
            if (txtTenSach.Text.Trim() == "" ||
                txtTacGia.Text.Trim() == "" ||
                txtMaLoai.Text.Trim() == "" ||
                txtSoLuongTon.Text.Trim() == "")
            {
                MessageBox.Show("Vui lòng nhập đầy đủ thông tin!");
                return;
            }

            // Tác giả không được có số
            if (txtTacGia.Text.Any(char.IsDigit))
            {
                MessageBox.Show("Tên tác giả không được chứa số!");
                txtTacGia.Focus();
                return;
            }

            // Năm xuất bản không vượt hiện tại
            if (dtpNamSanXuat.Value.Year > DateTime.Now.Year)
            {
                MessageBox.Show("Năm xuất bản không được vượt quá năm hiện tại!");
                return;
            }

            // Kiểm tra số lượng tồn
            int soLuong;

            if (!int.TryParse(txtSoLuongTon.Text, out soLuong))
            {
                MessageBox.Show("Số lượng tồn phải là số!");
                txtSoLuongTon.Focus();
                return;
            }

            if (soLuong < 0)
            {
                MessageBox.Show("Số lượng tồn không được âm!");
                txtSoLuongTon.Focus();
                return;
            }

            // ===== PHẦN LƯU DỮ LIỆU =====

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
            cmd.Parameters.AddWithValue("@nam", dtpNamSanXuat.Value.Year);
            cmd.Parameters.AddWithValue("@sl", soLuong);

            if (!isThem)
                cmd.Parameters.AddWithValue("@id", txtMaSach.Text);

            try
            {
                db.thucthi(cmd);
                MessageBox.Show("Lưu thành công!");
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
    }
}

        