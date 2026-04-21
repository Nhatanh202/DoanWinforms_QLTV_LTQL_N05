using System;
using System.Data.SqlClient;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.Serialization.Formatters;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Doan_QLTV.Models;

namespace Doan_QLTV.Forms
{
    public partial class FrmDangNhap : Form
    {
        Database db;
        SqlConnection ketnoi;
        public FrmDangNhap()
        {
            InitializeComponent();
            db = new Database(".\\SQLEXPRESS", "QuanLyThuVien");
            ketnoi = db.cn;
        }
        private void btnThoat_Click(object sender, EventArgs e)
        {
            DialogResult traloi = MessageBox.Show("Bạn có muốn đóng chương trình không?", "Thông báo",
                                  MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
            if (traloi == DialogResult.OK)
                Application.Exit();
        }

        private void btnDangNhap_Click(object sender, EventArgs e)
        {
            //1. Kiểm tra dữ liệu đầu vào
            if (string.IsNullOrWhiteSpace(txtTenTaiKhoan.Text) || string.IsNullOrWhiteSpace(txtMatKhau.Text))
            {
                MessageBox.Show("Vui lòng nhập thông tin đăng nhập", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            try
            {
                //2. Mở kết nối
                if (ketnoi.State != ConnectionState.Open)
                    ketnoi.Open();

                //3. Truy vấn kiểm tra tài khoản và lấy vai trò
                string query = @"SELECT v.TenVaiTro 
                FROM TaiKhoan tk
                JOIN VaiTro v ON tk.MaVaiTro = v.MaVaiTro
                WHERE tk.Username = @username 
                AND tk.Password = @password";

                using (SqlCommand command = new SqlCommand(query, ketnoi))
                {
                    command.Parameters.AddWithValue("@username", txtTenTaiKhoan.Text.Trim());
                    command.Parameters.AddWithValue("@password", txtMatKhau.Text);
                    object result = command.ExecuteScalar();

                    // Kiểm tra kết quả
                    if (result != null)
                    {
                        string Loai = result.ToString();
                        PhanQuyen.ChucVu = Loai;
                        PhanQuyen.TenNguoiDung = txtTenTaiKhoan.Text.Trim();

                        MessageBox.Show($"Đăng nhập thành công với vai trò: {Loai}", "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);

                        // Mở FrmMain và truyền quyền — FrmMain rỗng, phân quyền xử lý ở đây nếu cần
                        this.Hide();
                        FrmMain main = new FrmMain();
                        main.Text = $"Quản lý thư viện - {PhanQuyen.TenNguoiDung} ({PhanQuyen.ChucVu})";

                        // Bạn muốn phân quyền chức năng nào cho từng vai trò, thêm ở đây.
                        if (Loai.ToLower().Contains("quản") || Loai.ToLower().Contains("quan"))
                        {
                            // admin: full access — nothing to disable on empty main
                        }
                        else if (Loai.ToLower().Contains("thủ") || Loai.ToLower().Contains("thu"))
                        {
                            // librarian: limited access — add disabling logic here
                        }
                        else if (Loai.ToLower().Contains("độc") || Loai.ToLower().Contains("doc"))
                        {
                            // reader: minimal access
                        }

                        main.ShowDialog();
                        this.Close();
                    }
                    else
                    {
                        MessageBox.Show("Tên đăng nhập hoặc mật khẩu không đúng.", "Đăng nhập thất bại", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi kết nối đến cơ sở dữ liệu: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                if (ketnoi != null && ketnoi.State == ConnectionState.Open)
                    ketnoi.Close();
            }
        }

        private void FrmDangNhap_Load(object sender, EventArgs e)
        {

        }

        private void txtTenTaiKhoan_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsLetter(e.KeyChar) && e.KeyChar != ' ' && !char.IsControl(e.KeyChar))
                e.Handled = true;
        }
    }
}
