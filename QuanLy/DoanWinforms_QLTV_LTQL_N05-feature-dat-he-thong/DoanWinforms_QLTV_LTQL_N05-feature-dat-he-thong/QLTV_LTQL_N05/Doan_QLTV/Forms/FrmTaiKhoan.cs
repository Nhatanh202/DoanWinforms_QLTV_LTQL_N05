using Doan_QLTV.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace Doan_QLTV.Forms
{
    public partial class FrmTaiKhoan : Form
    {
        private Database db;
        private bool isAdding = false;

        public FrmTaiKhoan()
        {
            InitializeComponent();
            db = new Database(".\\SQLEXPRESS", "QuanLyThuVien");

            this.Load += FrmTaiKhoan_Load;
            btnThem.Click += btnThem_Click;
            btnSua.Click += btnSua_Click;
            btnLuu.Click += btnLuu_Click;
            btnXoa.Click += btnXoa_Click;
            btnHuy.Click += btnHuy_Click;
            btnThoat.Click += btnThoat_Click;
        }

        private void BatTatChucNang(bool giatri)
        {
            btnLuu.Enabled = giatri;
            btnHuy.Enabled = giatri;
            txtUserName.Enabled = giatri;
            txtPassWord.Enabled = giatri;

            btnThem.Enabled = !giatri;
            btnSua.Enabled = !giatri;
            btnXoa.Enabled = !giatri;

            txtMaTaiKhoan.Enabled = false;
        }

        private void FrmTaiKhoan_Load(object sender, EventArgs e)
        {
            LoadVaiTro();;
            LoadData();
            BatTatChucNang(false);
        }

        private void LoadVaiTro()
        {
            cboVaiTro.DataSource = db.laydl(@"
        SELECT 
            MaVaiTro,
            CAST(MaVaiTro AS VARCHAR) + '-' + TenVaiTro AS TenHienThi
        FROM VaiTro
        WHERE MaVaiTro IN (2,3)
    ");

            cboVaiTro.DisplayMember = "TenHienThi";
            cboVaiTro.ValueMember = "MaVaiTro";
        }
        private void LoadData()
        {
            DataTable dt = db.laydl("SELECT * FROM TaiKhoan");
            dtgTaiKhoan.DataSource = dt;
            dtgTaiKhoan.Columns["MaNV"].Visible = false;
            dtgTaiKhoan.Columns["MaDocGia"].Visible = false;

            txtMaTaiKhoan.DataBindings.Clear();
            txtUserName.DataBindings.Clear();
            txtPassWord.DataBindings.Clear();

            txtMaTaiKhoan.DataBindings.Add("Text", dt, "MaTK");
            txtUserName.DataBindings.Add("Text", dt, "Username");
            txtPassWord.DataBindings.Add("Text", dt, "Password");
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            isAdding = true;

            txtMaTaiKhoan.Clear();
            txtUserName.Clear();
            txtPassWord.Clear();

            BatTatChucNang(true);
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            if (txtMaTaiKhoan.Text == "") return;
            isAdding = false;
            BatTatChucNang(true);
        }

        private void btnLuu_Click(object sender, EventArgs e)
        {
            if (txtUserName.Text == "" || txtPassWord.Text == "")
            {
                MessageBox.Show("Nhập đầy đủ thông tin!");
                return;
            }

            int maVaiTro = Convert.ToInt32(cboVaiTro.SelectedValue);

            object maNV = DBNull.Value;
            object maDocGia = DBNull.Value;

            if (maVaiTro == 2) 
            {
                maNV = 2; 
            }
            else if (maVaiTro == 3) 
            {
                maDocGia = 1; 
            }

            SqlCommand cmd = new SqlCommand();
            cmd.Connection = db.cn;

            if (isAdding)
            {
                cmd.CommandText = @"INSERT INTO TaiKhoan
                (Username, Password, MaVaiTro, MaNV, MaDocGia)
                VALUES (@u, @p, @v, @nv, @dg)";
            }
            else
            {
                cmd.CommandText = @"UPDATE TaiKhoan SET
                Username=@u, Password=@p,
                MaVaiTro=@v, MaNV=@nv, MaDocGia=@dg
                WHERE MaTK=@id";

                cmd.Parameters.AddWithValue("@id", txtMaTaiKhoan.Text);
            }

            cmd.Parameters.AddWithValue("@u", txtUserName.Text);
            cmd.Parameters.AddWithValue("@p", txtPassWord.Text);
            cmd.Parameters.AddWithValue("@v", maVaiTro);
            cmd.Parameters.AddWithValue("@nv", maNV);
            cmd.Parameters.AddWithValue("@dg", maDocGia);

            db.thucthi(cmd);

            MessageBox.Show("Lưu thành công!");
            LoadData();
            BatTatChucNang(false);
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            if (txtMaTaiKhoan.Text == "") return;

            SqlCommand cmd = new SqlCommand(
                "DELETE FROM TaiKhoan WHERE MaTK=@id", db.cn);

            cmd.Parameters.AddWithValue("@id", txtMaTaiKhoan.Text);

            db.thucthi(cmd);
            LoadData();
        }

        private void btnHuy_Click(object sender, EventArgs e)
        {
            FrmTaiKhoan_Load(sender, e);
        }

        private void btnThoat_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void txtUserName_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsLetter(e.KeyChar) && e.KeyChar != ' ' && !char.IsControl(e.KeyChar))
                e.Handled = true;
        }

        private void dtgTaiKhoan_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (dtgTaiKhoan.Columns[e.ColumnIndex].Name == "Password")
            {
                if (e.Value != null)
                {
                    e.Value = "******";
                }
            }
        }

        private void chkHienMatKhau_CheckedChanged(object sender, EventArgs e)
        {
            if (chkHienMatKhau.Checked)
            {
                txtPassWord.UseSystemPasswordChar = false; 
            }
            else
            {
                txtPassWord.UseSystemPasswordChar = true; 
            }
        }
    }
}
