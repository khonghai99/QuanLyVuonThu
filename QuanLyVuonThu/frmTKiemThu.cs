using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QuanLyVuonThu
{
    public partial class frmTKiemThu : Form
    {
        public frmTKiemThu()
        {
            InitializeComponent();
        }

        private void BtnSuaThu_Click(object sender, EventArgs e)
        {
           
        }

        private void FrmTKiemThu_Load(object sender, EventArgs e)
        {
            dtgvTimKiemThu.DataSource = Golobal.GolobalThu.kquaTimKiem;
        }
    }
}
