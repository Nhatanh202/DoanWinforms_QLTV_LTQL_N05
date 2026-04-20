using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Doan_QLTV.Forms
{
    public partial class FrmHienThiReport : Form
    {
        public FrmHienThiReport()
        {
            InitializeComponent();
            this.Load += FrmHienThiReport_Load;
        }

        // Tạo thêm phương thức hoặc constructor để nhận report
        public void SetReport(object rpt)
        {
            crystalReportViewer1.ReportSource = rpt;
        }

        private void FrmHienThiReport_Load(object sender, EventArgs e)
        {
            // Tự động làm mới và hiển thị giao diện báo cáo khi Form tải lên
            crystalReportViewer1.Refresh();
        }
    }
}
