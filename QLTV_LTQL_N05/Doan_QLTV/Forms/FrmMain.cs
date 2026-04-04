using Doan_QLTV.Froms;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing.Drawing2D;

namespace Doan_QLTV
{
    public partial class FrmMain : Form
    {
        private Font activeFont = new Font("Segoe UI", 12, FontStyle.Bold);
        private Font inactiveFont = new Font("Segoe UI", 12, FontStyle.Regular);
        private Color colorActive = Color.FromArgb(0, 120, 215);     // xanh hiện đại
        private Color colorHover = Color.FromArgb(30, 144, 255);     // xanh nhạt hơn
        private Color colorInactive = Color.FromArgb(240, 240, 240); // xám nhẹ

        private List<Button> danhSachNut;


        public FrmMain()
        {
            InitializeComponent();

            danhSachNut = new List<Button>()
{
                        btnBaoCaoTK,
                        btnQuanLy,
                        btnTrangChu,     
                        btnTaiKhoan,
                      
                    };
            KhoiTaoForm();
        }

        private void StyleButton(Button btn)
        {
            btn.FlatStyle = FlatStyle.Flat;
            btn.FlatAppearance.BorderSize = 0;

            btn.BackColor = colorInactive;
            btn.ForeColor = Color.Black;

            btn.Font = inactiveFont;

            // hover effect
            btn.MouseEnter += (s, e) =>
            {
                if (btn.BackColor != colorActive)
                    btn.BackColor = colorHover;
            };

            btn.MouseLeave += (s, e) =>
            {
                if (btn.BackColor != colorActive)
                    btn.BackColor = colorInactive;
            };
        }
      
    

private void BoGocButton(Button btn, int radius)
    {
        GraphicsPath path = new GraphicsPath();
        path.AddArc(0, 0, radius, radius, 180, 90);
        path.AddArc(btn.Width - radius, 0, radius, radius, 270, 90);
        path.AddArc(btn.Width - radius, btn.Height - radius, radius, radius, 0, 90);
        path.AddArc(0, btn.Height - radius, radius, radius, 90, 90);
        path.CloseAllFigures();

        btn.Region = new Region(path);
    }
    public void KhoiTaoForm()
        {
            lblDateTime.Text = DateTime.Now.ToString("hh:mm:ss tt");

            MoForm(new FrmSach());
        }

        private void FrmMain_Load(object sender, EventArgs e)
        {
            //dung de hien thi sau khi dang nhap
            foreach (Button btn in danhSachNut)
            {
                StyleButton(btn);
                BoGocButton(btn, 10);
            }
        }

        private void ActiveButton(Button btnActive)
        {
            foreach (Button btn in danhSachNut)
            {
                if (btn == btnActive)
                {
                    btn.BackColor = colorActive;
                    btn.ForeColor = Color.White;
                    btn.Font = activeFont;
                }
                else
                {
                    btn.BackColor = colorInactive;
                    btn.ForeColor = Color.Black;
                    btn.Font = inactiveFont;
                }
            }
        }
        public void HienThiSubMenu()
        {
            pnlSubMenu.Visible = false;
            pnlSubMenuAvatar.Visible = false;
        }

        private Form FormHienTai = null;

        private void MoForm(Form form)
        { //Dat pointer chi vao current form
          if (FormHienTai != null) FormHienTai.Close();

            FormHienTai = form;
           form.TopLevel = false; 
           form.FormBorderStyle = FormBorderStyle.None;
           form.Dock = DockStyle.Fill; 
            
            
           pnlChuyenForm.Controls.Add(form); 
            pnlChuyenForm.Tag = form; 
            form.BringToFront(); 
            form.Show(); }

        private void timer1_Tick(object sender, EventArgs e)
        {
            lblDateTime.Text = DateTime.Now.ToString("hh:mm:ss tt");
        }

        private void btnTrangChu_Click(object sender, EventArgs e)
        {



            ActiveButton(btnTrangChu);
            MoForm(new FrmSach());
            HienThiSubMenu();

        }

        private void btnTaiKhoan_Click(object sender, EventArgs e)
        {
            MoForm(new FrmTaiKhoan());
            ActiveButton(btnTaiKhoan);
            HienThiSubMenu();


        }

        private void btnBaoCaoTK_Click(object sender, EventArgs e)
        {
            MoForm(new FrmThongKe());
            ActiveButton(btnBaoCaoTK);
            HienThiSubMenu();
        }

        private void btnQuanLy_Click(object sender, EventArgs e)

        {
            ActiveButton(btnQuanLy);
            pnlSubMenu.Visible = !pnlSubMenu.Visible;
            pnlSubMenu.BringToFront();


            //đưa vị trí của sub menu về đúng vị trí của nút quản lý - hiện bên trái
            pnlSubMenu.Location = new Point(btnQuanLy.Location.X + btnQuanLy.Width+10, btnQuanLy.Location.Y +20);

        }

      

        private void btnTheloai_Click(object sender, EventArgs e)
        {
            MoForm(new FrmTheLoai());
            HienThiSubMenu();
        }

        private void btnNhanVien_Click(object sender, EventArgs e)
        {
            MoForm(new FrmNhanVien());
            HienThiSubMenu();
        }

        private void btnDocGia_Click(object sender, EventArgs e)
        {
            MoForm(new FrmDocGia());
            HienThiSubMenu();
        }

        private void btnTimKiemSach_Click(object sender, EventArgs e)
        {
            MoForm(new FrmTimKiem());
            HienThiSubMenu();
        }

        private void btnDangXuat_Click(object sender, EventArgs e)
        {

        }

        private void btnThoat_Click(object sender, EventArgs e)
        {
            DialogResult dg = MessageBox.Show("Bạn có chắc chắn muốn thoát?", "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if(dg == DialogResult.Yes)
            {
                Application.Exit();
            }
        }

        private void picAvatar_Click(object sender, EventArgs e)
        {
            //Hien thi submenu avatar
            pnlSubMenuAvatar.Visible = !pnlSubMenuAvatar.Visible;
            pnlSubMenuAvatar.BringToFront();

            //đưa vị trí của sub menu về đúng vị trí của nút quản lý - hiện bên trái
            pnlSubMenuAvatar.Location = new Point(picAvatar.Location.X - btnQuanLy.Width - 10, picAvatar.Location.Y + 20);
        }
    }
}
