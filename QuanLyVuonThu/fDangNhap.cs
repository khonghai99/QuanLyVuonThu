using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace QuanLyVuonThu
{
    public partial class fDangNhap : DevExpress.XtraEditors.XtraForm
    {
        public fDangNhap()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            this.MaximizeBox = false;
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            DevExpress.LookAndFeel.UserLookAndFeel.Default.SetSkinStyle("Springtime");
        }

        private void BtnDangNhap_Click(object sender, EventArgs e)
        {
            fMain fmain = new fMain();
            this.Hide();
            fmain.ShowDialog();
            this.Close();
        }

        private void BtnThoat_Click(object sender, EventArgs e)
        {

        }
    }
}
