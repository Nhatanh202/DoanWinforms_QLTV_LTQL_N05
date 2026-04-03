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
    public partial class FrmTheLoai : Form
    {

        bool isThem = false;
        Database db = new Database("DESKTOP-LESSMLI\\SQLEXPRESS", "QuanLyThuVien");
        public FrmTheLoai()
        {
            InitializeComponent();
        }

        // Hàm ẩn/hiện các nút và ô nhập liệu
        void SetControls(bool edit)
        {
            txtTenTheLoai.Enabled = edit;

            btnThem.Enabled = !edit;
            btnSua.Enabled = !edit;
            btnXoa.Enabled = !edit;
            btnLuu.Enabled = edit;
            btnHuy.Enabled = edit;
            btnThoat.Enabled = !edit;
        }

        // Tải dữ liệu từ bảng TheLoai
        void LoadData()
        {
            string sql = "SELECT MaLoai as [Mã Loại], TenLoai as [Tên Thể Loại] FROM TheLoai";
            dgvTheLoai.DataSource = db.laydl(sql);
        }

        private void FrmTheLoai_Load(object sender, EventArgs e)
        {
            try 
            {
                LoadData();
                SetControls(false);
                txtMaTheLoai.Enabled = false; // Mã loại tự tăng nên không cho sửa
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi kết nối: " + ex.Message);
            }
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            isThem = true;
            SetControls(true);
            txtMaTheLoai.Text = "";
            txtTenTheLoai.Text = "";
            txtTenTheLoai.Focus();
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtMaTheLoai.Text)) return;
            isThem = false;
            SetControls(true);
            txtTenTheLoai.Focus();
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtMaTheLoai.Text)) return;

            // Lưu ý: Nếu loại này đang có Sách tham chiếu tới, SQL sẽ báo lỗi khóa ngoại
            if (MessageBox.Show("Bạn có chắc muốn xóa thể loại này?", "Xác nhận", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                try
                {
                    string sql = "DELETE FROM TheLoai WHERE MaLoai = @id";
                    SqlCommand cmd = new SqlCommand(sql, db.cn);
                    cmd.Parameters.AddWithValue("@id", txtMaTheLoai.Text);
                    db.thucthi(cmd);
                    LoadData();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Không thể xóa thể loại này vì đang có sách thuộc thể loại này!");
                }
            }
        }

        private void btnLuu_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtTenTheLoai.Text))
            {
                MessageBox.Show("Vui lòng nhập tên thể loại!");
                return;
            }

            string sql = "";
            if (isThem)
            {
                sql = "INSERT INTO TheLoai (TenLoai) VALUES (@ten)";
            }
            else
            {
                sql = "UPDATE TheLoai SET TenLoai = @ten WHERE MaLoai = @id";
            }

            SqlCommand cmd = new SqlCommand(sql, db.cn);
            cmd.Parameters.AddWithValue("@ten", txtTenTheLoai.Text);
            if (!isThem) cmd.Parameters.AddWithValue("@id", txtMaTheLoai.Text);

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
            LoadData();
        }

        private void btnThoat_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void dgvTheLoai_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = dgvTheLoai.Rows[e.RowIndex];
                txtMaTheLoai.Text = row.Cells[0].Value.ToString();
                txtTenTheLoai.Text = row.Cells[1].Value.ToString();
            }
        }
    }
}
