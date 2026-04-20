using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace Doan_QLTV.Froms
{
    public partial class FrmTimKiem : Form
    {
        Database db;
        DataTable dt;
        bool kt = false;

        public FrmTimKiem()
        {
            InitializeComponent();
        }

        void loadTreeview()
        {
            tvTheLoai.Nodes.Clear();
            DataTable dt = db.laydl("Select MaLoai, TenLoai From TheLoai");

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                TreeNode n = new TreeNode();
                n.Text = dt.Rows[i][1].ToString();
                n.Tag = dt.Rows[i][0].ToString();
                
                DataTable dt1 = db.laydl("Select MaSach, TenSach FROM Sach where MaLoai='" + n.Tag.ToString() + "'");
                for (int j = 0; j < dt1.Rows.Count; j++)
                {
                    TreeNode m = new TreeNode();
                    m.Text = "Sách: " + dt1.Rows[j][1].ToString();
                    m.Tag = dt1.Rows[j][0].ToString();
                    n.Nodes.Add(m);
                }
                tvTheLoai.Nodes.Add(n);
            }
            tvTheLoai.ExpandAll();
        }

        private void FrmTimKiem_Load(object sender, EventArgs e)
        {
            db = new Database("ADMIN-PC\\SQLEXPRESS", "QuanLyThuVien");
            loadTreeview();

            DataTable dtTG = db.laydl("Select Distinct TacGia From Sach");
            DataRow row1 = dtTG.NewRow();
            row1[0] = "Tất cả";
            dtTG.Rows.InsertAt(row1, 0);

            cboTacGia.DataSource = dtTG;
            cboTacGia.DisplayMember = "TacGia";
            cboTacGia.ValueMember = "TacGia";

            loaddanhsach();
            xoadieukhien();
            kt = true;
        }

        void loaddanhsach()
        {
            string sql = "Select S.MaSach, S.TenSach, S.TacGia, T.TenLoai, S.NamXuatBan, S.SoLuongTon, S.MaLoai FROM Sach S, TheLoai T where S.MaLoai = T.MaLoai";
            dt = db.laydl(sql);
            dgvDanhSach.DataSource = dt;
        }

        void hienthidongbo(DataRow row)
        {
            txtMaSach.Text = row[0].ToString();
            txtTenSach.Text = row[1].ToString();
            txtTacGia.Text = row[2].ToString();
            txtTenLoai.Text = row[3].ToString(); 
            
            if (row[4].ToString() != "")
            {
                int nam = int.Parse(row[4].ToString());
                dtpNamSanXuat.Value = new DateTime(nam, 1, 1);
            }

            txtSoLuongTon.Text = row[5].ToString();
            picHinhAnh.Image = null; 
        }

        void xoadieukhien()
        {
            txtMaSach.Text = "";
            txtTenSach.Text = "";
            txtTacGia.Text = "";
            txtTenLoai.Text = "";
            txtSoLuongTon.Text = "";
            picHinhAnh.Image = null;
        }

        private void tvTheLoai_AfterSelect(object sender, TreeViewEventArgs e)
        {
            if (kt)
            {
                if (tvTheLoai.SelectedNode.Parent == null)
                {
                    string maloai = tvTheLoai.SelectedNode.Tag.ToString();
                    string sql = "Select S.MaSach, S.TenSach, S.TacGia, T.TenLoai, S.NamXuatBan, S.SoLuongTon, S.MaLoai FROM Sach S, TheLoai T where S.MaLoai = T.MaLoai and S.MaLoai ='" + maloai + "'";
                    DataTable dtloai = db.laydl(sql);
                    dgvDanhSach.DataSource = dtloai;
                    xoadieukhien();
                }
                else
                {
                    string masach = tvTheLoai.SelectedNode.Tag.ToString();
                    string sql1 = "Select S.MaSach, S.TenSach, S.TacGia, T.TenLoai, S.NamXuatBan, S.SoLuongTon, S.MaLoai FROM Sach S, TheLoai T where S.MaLoai = T.MaLoai and S.MaSach ='" + masach + "'";
                    DataTable dtsach = db.laydl(sql1);
                    dgvDanhSach.DataSource = dtsach;

                    if (dtsach.Rows.Count > 0)
                    {
                        hienthidongbo(dtsach.Rows[0]);
                    }
                }
            }
        }

        private void dgvDanhSach_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = dgvDanhSach.Rows[e.RowIndex];
                txtMaSach.Text = row.Cells["MaSach"].Value.ToString();
                txtTenSach.Text = row.Cells["TenSach"].Value.ToString();
                txtTacGia.Text = row.Cells["TacGia"].Value.ToString();
                txtTenLoai.Text = row.Cells["TenLoai"].Value.ToString();
                txtSoLuongTon.Text = row.Cells["SoLuongTon"].Value.ToString();
                
                if (row.Cells["NamXuatBan"].Value.ToString() != "")
                {
                    int nam = int.Parse(row.Cells["NamXuatBan"].Value.ToString());
                    dtpNamSanXuat.Value = new DateTime(nam, 1, 1);
                }
            }
        }

        void timkiem()
        {
            if (kt)
            {
                string sql = "Select S.MaSach, S.TenSach, S.TacGia, T.TenLoai, S.NamXuatBan, S.SoLuongTon, S.MaLoai FROM Sach S, TheLoai T where S.MaLoai = T.MaLoai";
                
                if (txtTimKiem.Text != "")
                {
                    sql = sql + " and S.TenSach like N'%" + txtTimKiem.Text + "%'";
                }
                
                if (cboTacGia.SelectedValue != null && cboTacGia.SelectedValue.ToString() != "Tất cả")
                {
                    string tacgia = cboTacGia.SelectedValue.ToString();
                    sql = sql + " and S.TacGia = N'" + tacgia + "'";
                }
                
                dt = db.laydl(sql);
                dgvDanhSach.DataSource = dt;
            }
        }

        private void txtTimKiem_TextChanged(object sender, EventArgs e)
        {
            tvTheLoai.SelectedNode = null;
            timkiem();
        }

        private void cboTacGia_SelectedIndexChanged(object sender, EventArgs e)
        {
            tvTheLoai.SelectedNode = null;
            timkiem();
        }

        private void tvTheLoai_NodeMouseClick(object sender, TreeNodeMouseClickEventArgs e)
        {

        }
    }
}
// 
