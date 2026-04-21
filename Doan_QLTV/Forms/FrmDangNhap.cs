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

namespace Doan_QLTV.Froms
{
    
    public partial class FrmDangNhap : Form
    {
        Database db;
        SqlConnection ketnoi;
        public FrmDangNhap()
        {
            InitializeComponent();
            db = new Database("LAPTOP-KMJHV18D\\SQLEXPRESS", "QuanLyThuVien"); 
            ketnoi = db.cn;
        }

        private void btnThoat_Click(object sender, EventArgs e)
        {
            DialogResult traloi;
            traloi = MessageBox.Show("Bạn có muốn đóng chương trình không?", "Thông báo",
            MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
            if (traloi == DialogResult.OK)
                Application.Exit();
        }

        private void btnDangNhap_Click(object sender, EventArgs e)
        {
            if (txtTenTaiKhoan.Text == "" || txtMatKhau.Text == "")
            {
                MessageBox.Show("Vui lòng nhập thông tin đăng nhập", "Thông báo");
                return;
            }

            try
            {
                ketnoi.Open();
                string query = @"SELECT v.TenVaiTro 
                 FROM TaiKhoan tk
                 JOIN VaiTro v ON tk.MaVaiTro = v.MaVaiTro
                 WHERE tk.Username = @username 
                 AND tk.Password = @password";
                using (SqlCommand command = new SqlCommand(query, ketnoi))
                {

                    command.Parameters.AddWithValue("@username", txtTenTaiKhoan.Text);
                    command.Parameters.AddWithValue("@password", txtMatKhau.Text);


                    object result = command.ExecuteScalar();

                    // Kiểm tra kết quả
                    if (result != null)
                    {

                        string Loai = result.ToString();
                        PhanQuyen.ChucVu = Loai;
                        PhanQuyen.TenNguoiDung = txtTenTaiKhoan.Text;
                        MessageBox.Show($"Đăng nhập thành công với vai trò: {Loai}", "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        this.Hide();
                        FrmTaiKhoan f = new FrmTaiKhoan();
                        f.ShowDialog();

                        this.Close();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi kết nối đến cơ sở dữ liệu: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

    }
}
