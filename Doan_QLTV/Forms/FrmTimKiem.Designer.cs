namespace Doan_QLTV.Froms
{
    partial class FrmTimKiem
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        private void InitializeComponent()
        {
            this.gbTimKiem = new System.Windows.Forms.GroupBox();
            this.lblTimTen = new System.Windows.Forms.Label();
            this.txtTimKiem = new System.Windows.Forms.TextBox();
            this.lblTimTacGia = new System.Windows.Forms.Label();
            this.cboTacGia = new System.Windows.Forms.ComboBox();
            this.gbTheLoai = new System.Windows.Forms.GroupBox();
            this.tvTheLoai = new System.Windows.Forms.TreeView();
            this.gbThongTin = new System.Windows.Forms.GroupBox();
            this.picHinhAnh = new System.Windows.Forms.PictureBox();
            this.dtpNamSanXuat = new System.Windows.Forms.DateTimePicker();
            this.lblNamXuatBan = new System.Windows.Forms.Label();
            this.txtSoLuongTon = new System.Windows.Forms.TextBox();
            this.lblSoLuongTon = new System.Windows.Forms.Label();
            this.txtTenLoai = new System.Windows.Forms.TextBox();
            this.lblTenLoai = new System.Windows.Forms.Label();
            this.txtTacGia = new System.Windows.Forms.TextBox();
            this.lblTacGia = new System.Windows.Forms.Label();
            this.txtTenSach = new System.Windows.Forms.TextBox();
            this.lblTenSach = new System.Windows.Forms.Label();
            this.txtMaSach = new System.Windows.Forms.TextBox();
            this.lblMaSach = new System.Windows.Forms.Label();
            this.gbDanhSach = new System.Windows.Forms.GroupBox();
            this.dgvDanhSach = new System.Windows.Forms.DataGridView();
            this.lblTieuDe = new System.Windows.Forms.Label();
            this.gbTimKiem.SuspendLayout();
            this.gbTheLoai.SuspendLayout();
            this.gbThongTin.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picHinhAnh)).BeginInit();
            this.gbDanhSach.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvDanhSach)).BeginInit();
            this.SuspendLayout();
            // 
            // gbTimKiem
            // 
            this.gbTimKiem.Controls.Add(this.lblTimTen);
            this.gbTimKiem.Controls.Add(this.txtTimKiem);
            this.gbTimKiem.Controls.Add(this.lblTimTacGia);
            this.gbTimKiem.Controls.Add(this.cboTacGia);
            this.gbTimKiem.Location = new System.Drawing.Point(14, 56);
            this.gbTimKiem.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.gbTimKiem.Name = "gbTimKiem";
            this.gbTimKiem.Padding = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.gbTimKiem.Size = new System.Drawing.Size(1090, 88);
            this.gbTimKiem.TabIndex = 1;
            this.gbTimKiem.TabStop = false;
            this.gbTimKiem.Text = "Bộ lọc tìm kiếm";
            // 
            // lblTimTen
            // 
            this.lblTimTen.AutoSize = true;
            this.lblTimTen.Location = new System.Drawing.Point(34, 38);
            this.lblTimTen.Name = "lblTimTen";
            this.lblTimTen.Size = new System.Drawing.Size(101, 20);
            this.lblTimTen.TabIndex = 0;
            this.lblTimTen.Text = "Tìm theo tên:";
            // 
            // txtTimKiem
            // 
            this.txtTimKiem.Location = new System.Drawing.Point(158, 34);
            this.txtTimKiem.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.txtTimKiem.Name = "txtTimKiem";
            this.txtTimKiem.Size = new System.Drawing.Size(281, 26);
            this.txtTimKiem.TabIndex = 1;
            this.txtTimKiem.TextChanged += new System.EventHandler(this.txtTimKiem_TextChanged);
            // 
            // lblTimTacGia
            // 
            this.lblTimTacGia.AutoSize = true;
            this.lblTimTacGia.Location = new System.Drawing.Point(472, 38);
            this.lblTimTacGia.Name = "lblTimTacGia";
            this.lblTimTacGia.Size = new System.Drawing.Size(103, 20);
            this.lblTimTacGia.TabIndex = 2;
            this.lblTimTacGia.Text = "Chọn Tác giả:";
            // 
            // cboTacGia
            // 
            this.cboTacGia.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboTacGia.FormattingEnabled = true;
            this.cboTacGia.Location = new System.Drawing.Point(596, 34);
            this.cboTacGia.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.cboTacGia.Name = "cboTacGia";
            this.cboTacGia.Size = new System.Drawing.Size(168, 28);
            this.cboTacGia.TabIndex = 3;
            this.cboTacGia.SelectedIndexChanged += new System.EventHandler(this.cboTacGia_SelectedIndexChanged);
            // 
            // gbTheLoai
            // 
            this.gbTheLoai.Controls.Add(this.tvTheLoai);
            this.gbTheLoai.Location = new System.Drawing.Point(14, 156);
            this.gbTheLoai.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.gbTheLoai.Name = "gbTheLoai";
            this.gbTheLoai.Padding = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.gbTheLoai.Size = new System.Drawing.Size(281, 300);
            this.gbTheLoai.TabIndex = 2;
            this.gbTheLoai.TabStop = false;
            this.gbTheLoai.Text = "Danh sách thể loại";
            // 
            // tvTheLoai
            // 
            this.tvTheLoai.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tvTheLoai.Location = new System.Drawing.Point(3, 23);
            this.tvTheLoai.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.tvTheLoai.Name = "tvTheLoai";
            this.tvTheLoai.Size = new System.Drawing.Size(275, 273);
            this.tvTheLoai.TabIndex = 0;
            this.tvTheLoai.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.tvTheLoai_AfterSelect);
            this.tvTheLoai.NodeMouseClick += new System.Windows.Forms.TreeNodeMouseClickEventHandler(this.tvTheLoai_NodeMouseClick);
            // 
            // gbThongTin
            // 
            this.gbThongTin.Controls.Add(this.picHinhAnh);
            this.gbThongTin.Controls.Add(this.dtpNamSanXuat);
            this.gbThongTin.Controls.Add(this.lblNamXuatBan);
            this.gbThongTin.Controls.Add(this.txtSoLuongTon);
            this.gbThongTin.Controls.Add(this.lblSoLuongTon);
            this.gbThongTin.Controls.Add(this.txtTenLoai);
            this.gbThongTin.Controls.Add(this.lblTenLoai);
            this.gbThongTin.Controls.Add(this.txtTacGia);
            this.gbThongTin.Controls.Add(this.lblTacGia);
            this.gbThongTin.Controls.Add(this.txtTenSach);
            this.gbThongTin.Controls.Add(this.lblTenSach);
            this.gbThongTin.Controls.Add(this.txtMaSach);
            this.gbThongTin.Controls.Add(this.lblMaSach);
            this.gbThongTin.Location = new System.Drawing.Point(318, 156);
            this.gbThongTin.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.gbThongTin.Name = "gbThongTin";
            this.gbThongTin.Padding = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.gbThongTin.Size = new System.Drawing.Size(785, 300);
            this.gbThongTin.TabIndex = 3;
            this.gbThongTin.TabStop = false;
            this.gbThongTin.Text = "Thông tin sách chi tiết";
            // 
            // picHinhAnh
            // 
            this.picHinhAnh.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.picHinhAnh.Location = new System.Drawing.Point(585, 31);
            this.picHinhAnh.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.picHinhAnh.Name = "picHinhAnh";
            this.picHinhAnh.Size = new System.Drawing.Size(180, 250);
            this.picHinhAnh.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.picHinhAnh.TabIndex = 12;
            this.picHinhAnh.TabStop = false;
            // 
            // dtpNamSanXuat
            // 
            this.dtpNamSanXuat.CustomFormat = "yyyy";
            this.dtpNamSanXuat.Enabled = false;
            this.dtpNamSanXuat.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpNamSanXuat.Location = new System.Drawing.Point(394, 96);
            this.dtpNamSanXuat.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.dtpNamSanXuat.Name = "dtpNamSanXuat";
            this.dtpNamSanXuat.Size = new System.Drawing.Size(157, 26);
            this.dtpNamSanXuat.TabIndex = 9;
            // 
            // lblNamXuatBan
            // 
            this.lblNamXuatBan.AutoSize = true;
            this.lblNamXuatBan.Location = new System.Drawing.Point(304, 100);
            this.lblNamXuatBan.Name = "lblNamXuatBan";
            this.lblNamXuatBan.Size = new System.Drawing.Size(72, 20);
            this.lblNamXuatBan.TabIndex = 8;
            this.lblNamXuatBan.Text = "Năm XB:";
            // 
            // txtSoLuongTon
            // 
            this.txtSoLuongTon.Location = new System.Drawing.Point(394, 159);
            this.txtSoLuongTon.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.txtSoLuongTon.Name = "txtSoLuongTon";
            this.txtSoLuongTon.ReadOnly = true;
            this.txtSoLuongTon.Size = new System.Drawing.Size(157, 26);
            this.txtSoLuongTon.TabIndex = 11;
            // 
            // lblSoLuongTon
            // 
            this.lblSoLuongTon.AutoSize = true;
            this.lblSoLuongTon.Location = new System.Drawing.Point(304, 162);
            this.lblSoLuongTon.Name = "lblSoLuongTon";
            this.lblSoLuongTon.Size = new System.Drawing.Size(76, 20);
            this.lblSoLuongTon.TabIndex = 10;
            this.lblSoLuongTon.Text = "Số lượng:";
            // 
            // txtTenLoai
            // 
            this.txtTenLoai.Location = new System.Drawing.Point(394, 34);
            this.txtTenLoai.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.txtTenLoai.Name = "txtTenLoai";
            this.txtTenLoai.ReadOnly = true;
            this.txtTenLoai.Size = new System.Drawing.Size(157, 26);
            this.txtTenLoai.TabIndex = 7;
            // 
            // lblTenLoai
            // 
            this.lblTenLoai.AutoSize = true;
            this.lblTenLoai.Location = new System.Drawing.Point(304, 38);
            this.lblTenLoai.Name = "lblTenLoai";
            this.lblTenLoai.Size = new System.Drawing.Size(63, 20);
            this.lblTenLoai.TabIndex = 6;
            this.lblTenLoai.Text = "Tên loại:";
            // 
            // txtTacGia
            // 
            this.txtTacGia.Location = new System.Drawing.Point(112, 159);
            this.txtTacGia.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.txtTacGia.Name = "txtTacGia";
            this.txtTacGia.ReadOnly = true;
            this.txtTacGia.Size = new System.Drawing.Size(168, 26);
            this.txtTacGia.TabIndex = 5;
            // 
            // lblTacGia
            // 
            this.lblTacGia.AutoSize = true;
            this.lblTacGia.Location = new System.Drawing.Point(22, 162);
            this.lblTacGia.Name = "lblTacGia";
            this.lblTacGia.Size = new System.Drawing.Size(64, 20);
            this.lblTacGia.TabIndex = 4;
            this.lblTacGia.Text = "Tác giả:";
            // 
            // txtTenSach
            // 
            this.txtTenSach.Location = new System.Drawing.Point(112, 96);
            this.txtTenSach.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.txtTenSach.Name = "txtTenSach";
            this.txtTenSach.ReadOnly = true;
            this.txtTenSach.Size = new System.Drawing.Size(168, 26);
            this.txtTenSach.TabIndex = 3;
            // 
            // lblTenSach
            // 
            this.lblTenSach.AutoSize = true;
            this.lblTenSach.Location = new System.Drawing.Point(22, 100);
            this.lblTenSach.Name = "lblTenSach";
            this.lblTenSach.Size = new System.Drawing.Size(78, 20);
            this.lblTenSach.TabIndex = 2;
            this.lblTenSach.Text = "Tên sách:";
            // 
            // txtMaSach
            // 
            this.txtMaSach.Location = new System.Drawing.Point(112, 34);
            this.txtMaSach.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.txtMaSach.Name = "txtMaSach";
            this.txtMaSach.ReadOnly = true;
            this.txtMaSach.Size = new System.Drawing.Size(168, 26);
            this.txtMaSach.TabIndex = 1;
            // 
            // lblMaSach
            // 
            this.lblMaSach.AutoSize = true;
            this.lblMaSach.Location = new System.Drawing.Point(22, 38);
            this.lblMaSach.Name = "lblMaSach";
            this.lblMaSach.Size = new System.Drawing.Size(73, 20);
            this.lblMaSach.TabIndex = 0;
            this.lblMaSach.Text = "Mã sách:";
            // 
            // gbDanhSach
            // 
            this.gbDanhSach.Controls.Add(this.dgvDanhSach);
            this.gbDanhSach.Location = new System.Drawing.Point(14, 475);
            this.gbDanhSach.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.gbDanhSach.Name = "gbDanhSach";
            this.gbDanhSach.Padding = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.gbDanhSach.Size = new System.Drawing.Size(1090, 350);
            this.gbDanhSach.TabIndex = 4;
            this.gbDanhSach.TabStop = false;
            this.gbDanhSach.Text = "Danh sách sách";
            // 
            // dgvDanhSach
            // 
            this.dgvDanhSach.AllowUserToAddRows = false;
            this.dgvDanhSach.AllowUserToDeleteRows = false;
            this.dgvDanhSach.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvDanhSach.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvDanhSach.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvDanhSach.Location = new System.Drawing.Point(3, 23);
            this.dgvDanhSach.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.dgvDanhSach.Name = "dgvDanhSach";
            this.dgvDanhSach.ReadOnly = true;
            this.dgvDanhSach.RowHeadersWidth = 51;
            this.dgvDanhSach.RowTemplate.Height = 24;
            this.dgvDanhSach.Size = new System.Drawing.Size(1084, 323);
            this.dgvDanhSach.TabIndex = 0;
            this.dgvDanhSach.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvDanhSach_CellClick);
            // 
            // lblTieuDe
            // 
            this.lblTieuDe.AutoSize = true;
            this.lblTieuDe.Font = new System.Drawing.Font("Microsoft Sans Serif", 16F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTieuDe.Location = new System.Drawing.Point(411, 11);
            this.lblTieuDe.Name = "lblTieuDe";
            this.lblTieuDe.Size = new System.Drawing.Size(267, 37);
            this.lblTieuDe.TabIndex = 0;
            this.lblTieuDe.Text = "TÌM KIẾM SÁCH";
            // 
            // FrmTimKiem
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1117, 840);
            this.Controls.Add(this.lblTieuDe);
            this.Controls.Add(this.gbTimKiem);
            this.Controls.Add(this.gbDanhSach);
            this.Controls.Add(this.gbThongTin);
            this.Controls.Add(this.gbTheLoai);
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Name = "FrmTimKiem";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Tìm kiếm sách";
            this.Load += new System.EventHandler(this.FrmTimKiem_Load);
            this.gbTimKiem.ResumeLayout(false);
            this.gbTimKiem.PerformLayout();
            this.gbTheLoai.ResumeLayout(false);
            this.gbThongTin.ResumeLayout(false);
            this.gbThongTin.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picHinhAnh)).EndInit();
            this.gbDanhSach.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvDanhSach)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblTieuDe;
        private System.Windows.Forms.GroupBox gbTimKiem;
        private System.Windows.Forms.Label lblTimTen;
        private System.Windows.Forms.TextBox txtTimKiem;
        private System.Windows.Forms.Label lblTimTacGia;
        private System.Windows.Forms.ComboBox cboTacGia;
        private System.Windows.Forms.GroupBox gbTheLoai;
        private System.Windows.Forms.TreeView tvTheLoai;
        private System.Windows.Forms.GroupBox gbThongTin;
        private System.Windows.Forms.PictureBox picHinhAnh;
        private System.Windows.Forms.GroupBox gbDanhSach;
        private System.Windows.Forms.DataGridView dgvDanhSach;
        
        private System.Windows.Forms.Label lblMaSach;
        private System.Windows.Forms.TextBox txtMaSach;
        private System.Windows.Forms.Label lblTenSach;
        private System.Windows.Forms.TextBox txtTenSach;
        private System.Windows.Forms.Label lblTacGia;
        private System.Windows.Forms.TextBox txtTacGia;
        private System.Windows.Forms.Label lblTenLoai;
        private System.Windows.Forms.TextBox txtTenLoai;
        private System.Windows.Forms.Label lblNamXuatBan;
        private System.Windows.Forms.DateTimePicker dtpNamSanXuat;
        private System.Windows.Forms.Label lblSoLuongTon;
        private System.Windows.Forms.TextBox txtSoLuongTon;
    }
}