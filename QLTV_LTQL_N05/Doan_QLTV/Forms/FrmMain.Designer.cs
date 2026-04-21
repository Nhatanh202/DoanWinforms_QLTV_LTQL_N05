using System.Drawing;
using System.Windows.Forms;

namespace Doan_QLTV
{
    partial class FrmMain
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code
        private System.Windows.Forms.Panel panelSubMenu;

        

        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmMain));
            this.panelSubMenu = new System.Windows.Forms.Panel();
            this.panel1 = new System.Windows.Forms.Panel();
            this.pnlMoForm = new System.Windows.Forms.Panel();
            this.pnlSubMenuAvatar = new System.Windows.Forms.Panel();
            this.btnThoat = new System.Windows.Forms.Button();
            this.btnDangXuat = new System.Windows.Forms.Button();
            this.button6 = new System.Windows.Forms.Button();
            this.pnlChuyenForm = new System.Windows.Forms.Panel();
            this.pnlSubMenu = new System.Windows.Forms.Panel();
            this.btnTimKiemSach = new System.Windows.Forms.Button();
            this.btnDocGia = new System.Windows.Forms.Button();
            this.btnNhanVien = new System.Windows.Forms.Button();
            this.btnTheloai = new System.Windows.Forms.Button();
            this.button4 = new System.Windows.Forms.Button();
            this.btnQuanLy = new System.Windows.Forms.Button();
            this.imageList1 = new System.Windows.Forms.ImageList(this.components);
            this.btnTrangChu = new System.Windows.Forms.Button();
            this.btnBaoCaoTK = new System.Windows.Forms.Button();
            this.btnTaiKhoan = new System.Windows.Forms.Button();
            this.pnlDieuHuong = new System.Windows.Forms.Panel();
            this.lblDateTime = new System.Windows.Forms.Label();
            this.lblEmail = new System.Windows.Forms.Label();
            this.lblTenDangNhap = new System.Windows.Forms.Label();
            this.picAvatar = new System.Windows.Forms.PictureBox();
            this.label1 = new System.Windows.Forms.Label();
            this.button9 = new System.Windows.Forms.Button();
            this.imglistAvatar = new System.Windows.Forms.ImageList(this.components);
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.panel1.SuspendLayout();
            this.pnlMoForm.SuspendLayout();
            this.pnlSubMenuAvatar.SuspendLayout();
            this.pnlSubMenu.SuspendLayout();
            this.pnlDieuHuong.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picAvatar)).BeginInit();
            this.SuspendLayout();
            // 
            // panelSubMenu
            // 
            this.panelSubMenu.BackColor = System.Drawing.Color.Gainsboro;
            this.panelSubMenu.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelSubMenu.Location = new System.Drawing.Point(0, 0);
            this.panelSubMenu.Name = "panelSubMenu";
            this.panelSubMenu.Size = new System.Drawing.Size(1245, 0);
            this.panelSubMenu.TabIndex = 1;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.pnlMoForm);
            this.panel1.Controls.Add(this.pnlDieuHuong);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1245, 700);
            this.panel1.TabIndex = 3;
            // 
            // pnlMoForm
            // 
            this.pnlMoForm.BackColor = System.Drawing.Color.White;
            this.Controls.Add(this.pnlSubMenuAvatar);
            this.pnlMoForm.Controls.Add(this.pnlChuyenForm);
            this.pnlMoForm.Controls.Add(this.pnlSubMenu);
            this.pnlMoForm.Controls.Add(this.btnQuanLy);
            this.pnlMoForm.Controls.Add(this.btnTrangChu);
            this.pnlMoForm.Controls.Add(this.btnBaoCaoTK);
            this.pnlMoForm.Controls.Add(this.btnTaiKhoan);
            this.pnlMoForm.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlMoForm.Location = new System.Drawing.Point(0, 89);
            this.pnlMoForm.Name = "pnlMoForm";
            this.pnlMoForm.Size = new System.Drawing.Size(1245, 611);
            this.pnlMoForm.TabIndex = 1;
            // 
            // pnlSubMenuAvatar
            // 
            this.pnlSubMenuAvatar.Controls.Add(this.btnThoat);
            this.pnlSubMenuAvatar.Controls.Add(this.btnDangXuat);
            this.pnlSubMenuAvatar.Controls.Add(this.button6);
            this.pnlSubMenuAvatar.Location = new System.Drawing.Point(624, 148);
            this.pnlSubMenuAvatar.Name = "pnlSubMenuAvatar";
            this.pnlSubMenuAvatar.Size = new System.Drawing.Size(174, 109);
            this.pnlSubMenuAvatar.TabIndex = 6;
            this.pnlSubMenuAvatar.Visible = false;
            // 
            // btnThoat
            // 
            this.btnThoat.Dock = System.Windows.Forms.DockStyle.Top;
            this.btnThoat.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnThoat.Location = new System.Drawing.Point(0, 52);
            this.btnThoat.Name = "btnThoat";
            this.btnThoat.Size = new System.Drawing.Size(174, 52);
            this.btnThoat.TabIndex = 2;
            this.btnThoat.Text = "Thoát";
            this.btnThoat.UseVisualStyleBackColor = true;
            this.btnThoat.Click += new System.EventHandler(this.btnThoat_Click);
            // 
            // btnDangXuat
            // 
            this.btnDangXuat.Dock = System.Windows.Forms.DockStyle.Top;
            this.btnDangXuat.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnDangXuat.Location = new System.Drawing.Point(0, 0);
            this.btnDangXuat.Name = "btnDangXuat";
            this.btnDangXuat.Size = new System.Drawing.Size(174, 52);
            this.btnDangXuat.TabIndex = 1;
            this.btnDangXuat.Text = "Đăng xuất";
            this.btnDangXuat.UseVisualStyleBackColor = true;
            this.btnDangXuat.Click += new System.EventHandler(this.btnDangXuat_Click);
            // 
            // button6
            // 
            this.button6.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.button6.Location = new System.Drawing.Point(0, 0);
            this.button6.Name = "button6";
            this.button6.Size = new System.Drawing.Size(174, 52);
            this.button6.TabIndex = 0;
            this.button6.Text = "Sản phẩm sách";
            this.button6.UseVisualStyleBackColor = true;
            // 
            // pnlChuyenForm
            // 
            this.pnlChuyenForm.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pnlChuyenForm.BackColor = System.Drawing.Color.Black;
            this.pnlChuyenForm.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlChuyenForm.Location = new System.Drawing.Point(24, 119);
            this.pnlChuyenForm.Name = "pnlChuyenForm";
            this.pnlChuyenForm.Size = new System.Drawing.Size(1206, 480);
            this.pnlChuyenForm.TabIndex = 6;
            // 
            // pnlSubMenu
            // 
            this.pnlSubMenu.Controls.Add(this.btnTimKiemSach);
            this.pnlSubMenu.Controls.Add(this.btnDocGia);
            this.pnlSubMenu.Controls.Add(this.btnNhanVien);
            this.pnlSubMenu.Controls.Add(this.btnTheloai);
            this.pnlSubMenu.Controls.Add(this.button4);
            this.pnlSubMenu.Location = new System.Drawing.Point(1031, 15);
            this.pnlSubMenu.Name = "pnlSubMenu";
            this.pnlSubMenu.Size = new System.Drawing.Size(174, 209);
            this.pnlSubMenu.TabIndex = 5;
            this.pnlSubMenu.Visible = false;
            // 
            // btnTimKiemSach
            // 
            this.btnTimKiemSach.Dock = System.Windows.Forms.DockStyle.Top;
            this.btnTimKiemSach.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnTimKiemSach.Location = new System.Drawing.Point(0, 156);
            this.btnTimKiemSach.Name = "btnTimKiemSach";
            this.btnTimKiemSach.Size = new System.Drawing.Size(174, 49);
            this.btnTimKiemSach.TabIndex = 4;
            this.btnTimKiemSach.Text = "Tìm kiếm sách ...";
            this.btnTimKiemSach.UseVisualStyleBackColor = true;
            this.btnTimKiemSach.Click += new System.EventHandler(this.btnTimKiemSach_Click);
            // 
            // btnDocGia
            // 
            this.btnDocGia.Dock = System.Windows.Forms.DockStyle.Top;
            this.btnDocGia.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnDocGia.Location = new System.Drawing.Point(0, 104);
            this.btnDocGia.Name = "btnDocGia";
            this.btnDocGia.Size = new System.Drawing.Size(174, 52);
            this.btnDocGia.TabIndex = 3;
            this.btnDocGia.Text = "Đọc giả";
            this.btnDocGia.UseVisualStyleBackColor = true;
            this.btnDocGia.Click += new System.EventHandler(this.btnDocGia_Click);
            // 
            // btnNhanVien
            // 
            this.btnNhanVien.Dock = System.Windows.Forms.DockStyle.Top;
            this.btnNhanVien.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnNhanVien.Location = new System.Drawing.Point(0, 52);
            this.btnNhanVien.Name = "btnNhanVien";
            this.btnNhanVien.Size = new System.Drawing.Size(174, 52);
            this.btnNhanVien.TabIndex = 2;
            this.btnNhanVien.Text = "Nhân viên";
            this.btnNhanVien.UseVisualStyleBackColor = true;
            this.btnNhanVien.Click += new System.EventHandler(this.btnNhanVien_Click);
            // 
            // btnTheloai
            // 
            this.btnTheloai.Dock = System.Windows.Forms.DockStyle.Top;
            this.btnTheloai.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnTheloai.Location = new System.Drawing.Point(0, 0);
            this.btnTheloai.Name = "btnTheloai";
            this.btnTheloai.Size = new System.Drawing.Size(174, 52);
            this.btnTheloai.TabIndex = 1;
            this.btnTheloai.Text = "Thể loại";
            this.btnTheloai.UseVisualStyleBackColor = true;
            this.btnTheloai.Click += new System.EventHandler(this.btnTheloai_Click);
            // 
            // button4
            // 
            this.button4.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.button4.Location = new System.Drawing.Point(0, 0);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(174, 52);
            this.button4.TabIndex = 0;
            this.button4.Text = "Sản phẩm sách";
            this.button4.UseVisualStyleBackColor = true;
            // 
            // btnQuanLy
            // 
            this.btnQuanLy.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.btnQuanLy.BackColor = System.Drawing.Color.White;
            this.btnQuanLy.FlatAppearance.BorderColor = System.Drawing.Color.Gray;
            this.btnQuanLy.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnQuanLy.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnQuanLy.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.btnQuanLy.ImageIndex = 3;
            this.btnQuanLy.ImageList = this.imageList1;
            this.btnQuanLy.Location = new System.Drawing.Point(834, 15);
            this.btnQuanLy.Name = "btnQuanLy";
            this.btnQuanLy.Size = new System.Drawing.Size(171, 90);
            this.btnQuanLy.TabIndex = 4;
            this.btnQuanLy.Text = "Quản lý";
            this.btnQuanLy.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.btnQuanLy.UseVisualStyleBackColor = false;
            this.btnQuanLy.Click += new System.EventHandler(this.btnQuanLy_Click);
            // 
            // imageList1
            // 
            this.imageList1.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList1.ImageStream")));
            this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
            this.imageList1.Images.SetKeyName(0, "house.png");
            this.imageList1.Images.SetKeyName(1, "books.png");
            this.imageList1.Images.SetKeyName(2, "profile.png");
            this.imageList1.Images.SetKeyName(3, "management.png");
            this.imageList1.Images.SetKeyName(4, "report.png");
            // 
            // btnTrangChu
            // 
            this.btnTrangChu.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.btnTrangChu.BackColor = System.Drawing.Color.White;
            this.btnTrangChu.FlatAppearance.BorderColor = System.Drawing.Color.Black;
            this.btnTrangChu.FlatAppearance.BorderSize = 2;
            this.btnTrangChu.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnTrangChu.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnTrangChu.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.btnTrangChu.ImageIndex = 0;
            this.btnTrangChu.ImageList = this.imageList1;
            this.btnTrangChu.Location = new System.Drawing.Point(159, 15);
            this.btnTrangChu.Name = "btnTrangChu";
            this.btnTrangChu.Size = new System.Drawing.Size(151, 90);
            this.btnTrangChu.TabIndex = 0;
            this.btnTrangChu.Text = "Trang chủ";
            this.btnTrangChu.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.btnTrangChu.UseVisualStyleBackColor = false;
            this.btnTrangChu.Click += new System.EventHandler(this.btnTrangChu_Click);
            // 
            // btnBaoCaoTK
            // 
            this.btnBaoCaoTK.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.btnBaoCaoTK.BackColor = System.Drawing.Color.White;
            this.btnBaoCaoTK.FlatAppearance.BorderColor = System.Drawing.Color.Gray;
            this.btnBaoCaoTK.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnBaoCaoTK.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnBaoCaoTK.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.btnBaoCaoTK.ImageIndex = 4;
            this.btnBaoCaoTK.ImageList = this.imageList1;
            this.btnBaoCaoTK.Location = new System.Drawing.Point(543, 14);
            this.btnBaoCaoTK.Name = "btnBaoCaoTK";
            this.btnBaoCaoTK.Size = new System.Drawing.Size(255, 89);
            this.btnBaoCaoTK.TabIndex = 2;
            this.btnBaoCaoTK.Text = "Báo cáo thống kê";
            this.btnBaoCaoTK.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.btnBaoCaoTK.UseVisualStyleBackColor = false;
            this.btnBaoCaoTK.Click += new System.EventHandler(this.btnBaoCaoTK_Click);
            // 
            // btnTaiKhoan
            // 
            this.btnTaiKhoan.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.btnTaiKhoan.BackColor = System.Drawing.Color.White;
            this.btnTaiKhoan.FlatAppearance.BorderColor = System.Drawing.Color.Gray;
            this.btnTaiKhoan.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnTaiKhoan.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnTaiKhoan.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.btnTaiKhoan.ImageIndex = 2;
            this.btnTaiKhoan.ImageList = this.imageList1;
            this.btnTaiKhoan.Location = new System.Drawing.Point(346, 14);
            this.btnTaiKhoan.Name = "btnTaiKhoan";
            this.btnTaiKhoan.Size = new System.Drawing.Size(171, 90);
            this.btnTaiKhoan.TabIndex = 3;
            this.btnTaiKhoan.Text = "Tài khoản";
            this.btnTaiKhoan.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.btnTaiKhoan.UseVisualStyleBackColor = false;
            this.btnTaiKhoan.Click += new System.EventHandler(this.btnTaiKhoan_Click);
            // 
            // pnlDieuHuong
            // 
            this.pnlDieuHuong.Controls.Add(this.lblDateTime);
            this.pnlDieuHuong.Controls.Add(this.lblEmail);
            this.pnlDieuHuong.Controls.Add(this.lblTenDangNhap);
            this.pnlDieuHuong.Controls.Add(this.picAvatar);
            this.pnlDieuHuong.Controls.Add(this.label1);
            this.pnlDieuHuong.Controls.Add(this.button9);
            this.pnlDieuHuong.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlDieuHuong.Location = new System.Drawing.Point(0, 0);
            this.pnlDieuHuong.Name = "pnlDieuHuong";
            this.pnlDieuHuong.Size = new System.Drawing.Size(1245, 89);
            this.pnlDieuHuong.TabIndex = 0;
            // 
            // lblDateTime
            // 
            this.lblDateTime.AutoSize = true;
            this.lblDateTime.Font = new System.Drawing.Font("Times New Roman", 11F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDateTime.Location = new System.Drawing.Point(580, 38);
            this.lblDateTime.Name = "lblDateTime";
            this.lblDateTime.Size = new System.Drawing.Size(68, 25);
            this.lblDateTime.TabIndex = 5;
            this.lblDateTime.Text = "label4";
            // 
            // lblEmail
            // 
            this.lblEmail.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblEmail.AutoSize = true;
            this.lblEmail.Font = new System.Drawing.Font("Times New Roman", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblEmail.Location = new System.Drawing.Point(1011, 39);
            this.lblEmail.Name = "lblEmail";
            this.lblEmail.Size = new System.Drawing.Size(50, 20);
            this.lblEmail.TabIndex = 4;
            this.lblEmail.Text = "Email";
            // 
            // lblTenDangNhap
            // 
            this.lblTenDangNhap.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblTenDangNhap.AutoSize = true;
            this.lblTenDangNhap.Font = new System.Drawing.Font("Times New Roman", 11F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTenDangNhap.Location = new System.Drawing.Point(1010, 14);
            this.lblTenDangNhap.Name = "lblTenDangNhap";
            this.lblTenDangNhap.Size = new System.Drawing.Size(104, 25);
            this.lblTenDangNhap.TabIndex = 3;
            this.lblTenDangNhap.Text = "NVC Hao";
            // 
            // picAvatar
            // 
            this.picAvatar.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.picAvatar.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.picAvatar.Location = new System.Drawing.Point(905, 12);
            this.picAvatar.Name = "picAvatar";
            this.picAvatar.Size = new System.Drawing.Size(100, 68);
            this.picAvatar.TabIndex = 2;
            this.picAvatar.TabStop = false;
            this.picAvatar.Click += new System.EventHandler(this.picAvatar_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Times New Roman", 14F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(111, 32);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(177, 32);
            this.label1.TabIndex = 1;
            this.label1.Text = "Tên cửa hàng";
            // 
            // button9
            // 
            this.button9.BackColor = System.Drawing.Color.Beige;
            this.button9.ImageIndex = 0;
            this.button9.ImageList = this.imglistAvatar;
            this.button9.Location = new System.Drawing.Point(24, 12);
            this.button9.Name = "button9";
            this.button9.Size = new System.Drawing.Size(81, 68);
            this.button9.TabIndex = 0;
            this.button9.UseVisualStyleBackColor = false;
            // 
            // imglistAvatar
            // 
            this.imglistAvatar.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imglistAvatar.ImageStream")));
            this.imglistAvatar.TransparentColor = System.Drawing.Color.Transparent;
            this.imglistAvatar.Images.SetKeyName(0, "books.png");
            // 
            // timer1
            // 
            this.timer1.Enabled = true;
            this.timer1.Interval = 1000;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // FrmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1245, 700);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.panelSubMenu);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Name = "FrmMain";
            this.Text = "Quản Lý Thư Viện";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.FrmMain_Load);
            this.panel1.ResumeLayout(false);
            this.pnlMoForm.ResumeLayout(false);
            this.pnlSubMenuAvatar.ResumeLayout(false);
            this.pnlSubMenu.ResumeLayout(false);
            this.pnlDieuHuong.ResumeLayout(false);
            this.pnlDieuHuong.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picAvatar)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private Panel panel1;
        private Panel pnlMoForm;
        private Panel pnlDieuHuong;
        private ImageList imageList1;
        private Panel pnlSubMenu;
        private Button btnNhanVien;
        private Button btnTheloai;
        private Button button4;
        private Button btnTimKiemSach;
        private Button btnDocGia;
        private Button btnQuanLy;
        private Button btnTrangChu;
        private Button btnBaoCaoTK;
        private Button btnTaiKhoan;
        private Button button9;
        private Label lblEmail;
        private Label lblTenDangNhap;
        private PictureBox picAvatar;
        private Label label1;
        private ImageList imglistAvatar;
        private Panel pnlChuyenForm;
        private Label lblDateTime;
        private Timer timer1;
        private Panel pnlSubMenuAvatar;
        private Button btnThoat;
        private Button btnDangXuat;
        private Button button6;
    }
}

