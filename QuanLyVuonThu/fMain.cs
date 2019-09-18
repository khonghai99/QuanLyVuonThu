using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevExpress.XtraBars;
using System.Text.RegularExpressions;
using CrystalDecisions.Windows.Forms;

namespace QuanLyVuonThu
{
    public partial class fMain : DevExpress.XtraBars.Ribbon.RibbonForm
    {
        public bool IsDate(string str)
        {
            Regex regex = new Regex(@"^(0?[1-9]|1[012])[\/\-](0?[1-9]|[12][0-9]|3[01])[\/\-]\d{4}$");
            return regex.IsMatch(str);
        }
        ProcessDataBase dtBase = new ProcessDataBase();
        public void ResetValue()
        {
            txtMaThu.Text = "";
            txtTenThu.Text = "";
            txtMaLoai.Text = "";
            txtSoLuong.Text = "";
            txtSachDoThu.Text = "";
            txtTenKHThu.Text = "";
            txtDacDiemThu.Text = "";
            txtTenTA.Text = "";
            txtTenTV.Text = "";
            txtKieuSinh.Text = "";
            txtGioiTinhThu.Text = "";
            txtTuoiTho.Text = "";
            txtNgayVao.Text = "";
            txtNguonGoc.Text = "";
            txtNgaySinhThu.Text = "";
        }
        public fMain()
        {
            InitializeComponent();
        }

        private void FMain_Load(object sender, EventArgs e)
        {
            tclDS.Appearance = TabAppearance.FlatButtons;
            tclDS.ItemSize = new Size(0, 1);
            tclDS.SizeMode = TabSizeMode.Fixed;
            //Gọi pt DocBang lấy dữ liệu của bảng tblChatLieu đổ vào DataTable
            DataTable dtThu = dtBase.DocInBang("select * from Thu");
            dtgvThu.DataSource = dtThu;

            //Định dạng dataGrid
            dtgvThu.Columns[0].HeaderText = "Mã thú";
            dtgvThu.Columns[1].HeaderText = "Tên thú";
            dtgvThu.Columns[2].HeaderText = "Mã loài";
            dtgvThu.Columns[3].HeaderText = "Số lượng";
            dtgvThu.Columns[4].HeaderText = "Sách đỏ";
            dtgvThu.Columns[5].HeaderText = "Tên KH";
            dtgvThu.Columns[6].HeaderText = "Tên TA";
            dtgvThu.Columns[7].HeaderText = "Tên TV";
            dtgvThu.Columns[8].HeaderText = "Kiểu sinh";
            dtgvThu.Columns[9].HeaderText = "Giới tính";
            dtgvThu.Columns[10].HeaderText = "Ngày vào";
            dtgvThu.Columns[11].HeaderText = "Nguồn gốc";
            dtgvThu.Columns[12].HeaderText = "Đặc điểm";
            dtgvThu.Columns[13].HeaderText = "Ngày sinh";
            dtgvThu.Columns[14].HeaderText = "Ảnh";
            dtgvThu.Columns[15].HeaderText = "Tuổi thọ";

            dtgvThu.BackgroundColor = Color.LightBlue;
            dtThu.Dispose();

        }

        private void BbtnDangXuat_ItemClick(object sender, ItemClickEventArgs e)
        {
            if (DialogResult.Yes == MessageBox.Show("Bạn có muốn đăng xuất không ?", "Thông báo",
                MessageBoxButtons.YesNo, MessageBoxIcon.Question))
            {
                this.Close();
                fDangNhap fdangnhap = new fDangNhap();
                this.Hide();
                fdangnhap.ShowDialog();
            }
        }

        private void BbtnDoiMatKhau_ItemClick(object sender, ItemClickEventArgs e)
        {
            fDoiMatKhau fdoimatkhau = new fDoiMatKhau();
            this.Hide();
            fdoimatkhau.ShowDialog();
            this.Show();
        }

        private void Label30_Click(object sender, EventArgs e)
        {

        }
        private void BbtnitNhanVien_ItemClick(object sender, ItemClickEventArgs e)
        {
            tclDS.SelectTab(1);
        }

        private void BbtnitThu_ItemClick(object sender, ItemClickEventArgs e)
        {
            tclDS.SelectTab(2);

        }

        private void BbtnitChuong_ItemClick(object sender, ItemClickEventArgs e)
        {
            tclDS.SelectTab(3);
        }

        private void BbtnitThucAn_ItemClick(object sender, ItemClickEventArgs e)
        {
            tclDS.SelectTab(4);
        }

        private void BbtnitHeThong_ItemClick(object sender, ItemClickEventArgs e)
        {
            tclDS.SelectTab(5);
        }

        private void BbtnitAdmin_ItemClick(object sender, ItemClickEventArgs e)
        {
            tclDS.SelectTab(6);
        }

        private void BbtnitLoai_ItemClick(object sender, ItemClickEventArgs e)
        {
            DataTable dtThu = dtBase.DocInBang("SELECT Loai.TenLoai, Loai.MaLoai," +
                " Thu.TenThu, Thu.SoLuong, Thu.GioiTinh, Thu.KieuSinh,Thu.NguonGoc," +
                " Thu.DacDiem FROM Loai INNER JOIN Thu ON Loai.MaLoai = Thu.MaLoai");
            rpTheoLoai rp = new rpTheoLoai();
            rp.SetDataSource(dtThu);
            crystalReportViewer1.ReportSource = rp;
            
            tclDS.SelectTab(7);

        }

        private void BbtnitSachDo_ItemClick(object sender, ItemClickEventArgs e)
        {
            tclDS.SelectTab(8);
        }

        private void BbtnitOm_ItemClick(object sender, ItemClickEventArgs e)
        {
            tclDS.SelectTab(9);
        }

        private void BbtnitChiTiet_ItemClick(object sender, ItemClickEventArgs e)
        {
            tclDS.SelectTab(10);
        }

        private void BbtnitThang_ItemClick(object sender, ItemClickEventArgs e)
        {
            tclDS.SelectTab(11);
        }

        private void BbtnitMaThu_ItemClick(object sender, ItemClickEventArgs e)
        {
            tclDS.SelectTab(12);
        }

        private void BtnThemThu_Click(object sender, EventArgs e)
        {
            ResetValue();

        }

        private void BtnLuuThu_Click(object sender, EventArgs e)
        {
            if (txtMaThu.Text == "")
            {
                MessageBox.Show("Bạn phải nhập mã thú");
                txtMaThu.Focus();
            }
            else if (txtTenThu.Text == "")
            {
                MessageBox.Show("Bạn phải nhập tên thú");
                txtTenThu.Focus();
            }
            else if (txtMaLoai.Text == "")
            {
                MessageBox.Show("Bạn phải nhập mã loài");
                txtMaLoai.Focus();
            }
            else if (txtSoLuong.Text == "")
            {
                MessageBox.Show("Bạn phải nhập số lượng");
                txtSoLuong.Focus();
            }
            else if (txtSachDoThu.Text == "")
            {
                MessageBox.Show("Bạn phải nhập sách đỏ");
                txtSachDoThu.Focus();
            }
            else if (txtTenKHThu.Text == "")
            {
                MessageBox.Show("Bạn phải nhập tên khoa học");
                txtTenKHThu.Focus();
            }
            else if (txtTenTA.Text == "")
            {
                MessageBox.Show("Bạn phải nhập tên tiếng anh");
                txtTenTA.Focus();
            }
            else if (txtTenTV.Text == "")
            {
                MessageBox.Show("Bạn phải nhập tên tiếng việt");
                txtTenTV.Focus();
            }
            else if (txtKieuSinh.Text == "")
            {
                MessageBox.Show("Bạn phải nhập kiểu sinh");
                txtKieuSinh.Focus();
            }
            else if (txtGioiTinhThu.Text == "")
            {
                MessageBox.Show("Bạn phải nhập giới tính");
                txtGioiTinhThu.Focus();
            }
            else if (txtNgayVao.Text == "")
            {
                MessageBox.Show("Bạn phải nhập ngày vào");
                txtNgayVao.Focus();
            }
            else if (txtNguonGoc.Text == "")
            {
                MessageBox.Show("Bạn phải nhập nguồn gốc");
                txtNguonGoc.Focus();
            }
            else if (txtDacDiemThu.Text == "")
            {
                MessageBox.Show("Bạn phải nhập đặc điểm");
                txtDacDiemThu.Focus();
            }
            else if (txtNgaySinhThu.Text == "")
            {
                MessageBox.Show("Bạn phải nhập ngày sinh");
                txtNgaySinhThu.Focus();
            }
            else if (txtTuoiTho.Text == "")
            {
                MessageBox.Show("Bạn phải nhập tuổi thọ");
                txtTuoiTho.Focus();
            }
            else if (IsDate(txtNgayVao.Text) == false)
            {
                MessageBox.Show("Ngày vào không hợp lệ (MM-DD-YYYY or MM/DD/YYYY) !");
                txtNgayVao.Focus();
            }
            else if (IsDate(txtNgaySinhThu.Text) == false)
            {
                MessageBox.Show("Ngày sinh không hợp lệ (MM-DD-YYYY or MM/DD/YYYY) !");
                txtNgaySinhThu.Focus();
            }
            else
            {
                DataTable dtChatLieu = dtBase.DocInBang("Select * from Thu where" + " MaThu = '" + (txtMaThu.Text).Trim() + "'");
                if (dtChatLieu.Rows.Count > 0)
                {
                    MessageBox.Show("Mã thú này đã có, hãy nhập mã khác!");
                    txtMaThu.Focus();
                }
                else
                {

                    dtBase.CapNhatDuLieu("insert into Thu values(N'" +
                   txtMaThu.Text + "',N'" + txtTenThu.Text + "',N'" + txtMaLoai.Text + "','" + Convert.ToInt16(txtSoLuong.Text)
                   + "',N'" + txtSachDoThu.Text + "',N'" + txtTenKHThu.Text + "',N'" + txtTenTA.Text
                   + "',N'" + txtTenTV.Text + "',N'" + txtKieuSinh.Text + "',N'" + txtGioiTinhThu.Text
                   + "','" + txtNgayVao.Text + "',N'" + txtNguonGoc.Text
                   + "',N'" + txtDacDiemThu.Text + "','" + txtNgaySinhThu.Text
                   + "',null,'" + Convert.ToDouble(txtTuoiTho.Text) + "')");
                    MessageBox.Show("Bạn đã thêm mới thành công");
                    dtgvThu.DataSource = dtBase.DocInBang("select * from  Thu");

                    ResetValue();
                }
            }
        }

        private void DtgvThu_Click(object sender, EventArgs e)
        {
            txtMaThu.Text = dtgvThu.CurrentRow.Cells[0].Value.ToString();
            txtTenThu.Text = dtgvThu.CurrentRow.Cells[1].Value.ToString();
            txtMaLoai.Text = dtgvThu.CurrentRow.Cells[2].Value.ToString();
            txtSoLuong.Text = dtgvThu.CurrentRow.Cells[3].Value.ToString();
            txtSachDoThu.Text = dtgvThu.CurrentRow.Cells[4].Value.ToString();
            txtTenKHThu.Text = dtgvThu.CurrentRow.Cells[5].Value.ToString();
            txtTenTA.Text = dtgvThu.CurrentRow.Cells[6].Value.ToString();
            txtTenTV.Text = dtgvThu.CurrentRow.Cells[7].Value.ToString();
            txtKieuSinh.Text = dtgvThu.CurrentRow.Cells[8].Value.ToString();
            txtGioiTinhThu.Text = dtgvThu.CurrentRow.Cells[9].Value.ToString();
            txtNgayVao.Text = dtgvThu.CurrentRow.Cells[10].FormattedValue.ToString();
            txtNguonGoc.Text = dtgvThu.CurrentRow.Cells[11].Value.ToString();
            txtDacDiemThu.Text = dtgvThu.CurrentRow.Cells[12].Value.ToString();
            txtNgaySinhThu.Text = dtgvThu.CurrentRow.Cells[13].FormattedValue.ToString();
            txtTuoiTho.Text = dtgvThu.CurrentRow.Cells[15].Value.ToString();
        }


        private void BtnHuyThu_Click(object sender, EventArgs e)
        {

        }
    }
}