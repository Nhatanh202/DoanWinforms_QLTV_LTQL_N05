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
        }

        // Tạo thêm phương thức hoặc constructor để nhận report
        public void SetReport(object rpt)
        {
            crystalReportViewer1.ReportSource = rpt;
            crystalReportViewer1.Refresh();
        }
    }
}
