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
using QuanLyVuonThu.Model;
using System.IO;
using System.Data.SqlClient;
using CrystalDecisions.Windows.Forms;


namespace QuanLyVuonThu
{
    public partial class fMain : DevExpress.XtraBars.Ribbon.RibbonForm
    {
        List<string> dsLoai = new List<string>();
        List<string> dsChuong = new List<string>();
        List<ModelThu> modelThus = new List<ModelThu>();
        OpenFileDialog openFileDialog = new OpenFileDialog();
        public bool IsDate(string str)
        {
            Regex regex = new Regex(@"^(0?[1-9]|1[012])[\/\-](0?[1-9]|[12][0-9]|3[01])[\/\-]\d{4}$");
            return regex.IsMatch(str);
        }
        ProcessDataBase dtBase = new ProcessDataBase();
        public void ResetValue()
        {
            btnLuuThu.Enabled = true;
            btnSuaThu.Enabled = false;
            txtMaThu.Text = "";
            txtTenThu.Text = "";
            cbbMaLoai.Text = "";
            cbbSoLuongThu.Text = "";
            cbbSachDoThu.Text = "";
            txtTenKHThu.Text = "";
            txtDacDiemThu.Text = "";
            txtTenTA.Text = "";
            txtTenTV.Text = "";
            txtKieuSinh.Text = "";
            cbbGioiTinhThu.Text = "";
            txtTuoiTho.Text = "";
            dtpNgayVaoThu.Text = "";
            txtNguonGoc.Text = "";
            dtpNgaySinhThu.Text = "";
            cbbMaChuong.Text = "";
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
            DataTable dtChatLieu = dtBase.DocDL("select   thu.MaThu,   tenThu,   thu.maLoai,   chuong.maChuong,soLuong,   sachDo,   thu.TenKhoaHoc,      tenTA,   tenTV,   kieuSinh, gioiTinh, thu.NgayVao, nguonGoc,dacDiem, ngaySinh,  tuoiTho,   Anh from thu, chuong, Thu_Chuong where Thu_Chuong.MaChuong = Chuong.MaChuong and Thu.MaThu = Thu_Chuong.MaThu order by maloai ASC");
            dtgvThu.DataSource = dtChatLieu;
            dtgvThu.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
            //Định dạng dataGrid
            dtgvThu.Columns[0].HeaderText = "Mã thú";
            dtgvThu.Columns[1].HeaderText = "Tên thú";
            dtgvThu.Columns[2].HeaderText = "Mã loài";
            dtgvThu.Columns[3].HeaderText = "Mã chuồng";
            dtgvThu.Columns[4].HeaderText = "Số lượng";
            dtgvThu.Columns[5].HeaderText = "Sách đỏ";
            dtgvThu.Columns[6].HeaderText = "Tên KH";
            dtgvThu.Columns[7].HeaderText = "Tên TA";
            dtgvThu.Columns[8].HeaderText = "Tên TV";
            dtgvThu.Columns[9].HeaderText = "Kiểu sinh";
            dtgvThu.Columns[10].HeaderText = "Giới tính";
            dtgvThu.Columns[11].HeaderText = "Ngày vào";
            dtgvThu.Columns[12].HeaderText = "Nguồn gốc";
            dtgvThu.Columns[13].HeaderText = "Đặc điểm";
            dtgvThu.Columns[14].HeaderText = "Ngày sinh";
            dtgvThu.Columns[16].HeaderText = "Ảnh";
            dtgvThu.Columns[15].HeaderText = "Tuổi thọ";

            dtgvThu.BackgroundColor = Color.LightBlue;
            dtChatLieu.Dispose();
            for (int i = 0; i < dtgvThu.Rows.Count - 1; i++)
            {
                modelThus.Add(new ModelThu(dtgvThu.Rows[i].Cells[0].Value.ToString(), dtgvThu.Rows[i].Cells[1].Value.ToString(), dtgvThu.Rows[i].Cells[2].Value.ToString(), dtgvThu.Rows[i].Cells[3].Value.ToString(), Convert.ToInt32(dtgvThu.Rows[i].Cells[4].Value.ToString()), dtgvThu.Rows[i].Cells[5].Value.ToString(), dtgvThu.Rows[i].Cells[6].Value.ToString(), dtgvThu.Rows[i].Cells[7].Value.ToString(), dtgvThu.Rows[i].Cells[8].Value.ToString(), dtgvThu.Rows[i].Cells[9].Value.ToString(), dtgvThu.Rows[i].Cells[10].Value.ToString(), Convert.ToDateTime(dtgvThu.Rows[i].Cells[11].Value.ToString()), dtgvThu.Rows[i].Cells[12].Value.ToString(), dtgvThu.Rows[i].Cells[13].Value.ToString(), Convert.ToDateTime(dtgvThu.Rows[i].Cells[14].Value.ToString()), Convert.ToInt32(dtgvThu.Rows[i].Cells[15].Value.ToString()), dtgvThu.Rows[i].Cells[16].Value.ToString()));
            }
            SqlDataReader reader = dtBase.command("select maLoai from loai").ExecuteReader();
            if (reader.HasRows)
            {
                // Đọc kết quả
                while (reader.Read())
                {
                    dsLoai.Add(reader[0].ToString());
                    cbbMaLoai.Items.Add(reader[0].ToString());
                }
            }
            if(cbbMaLoai.Text.Length !=0)
            {
                cbbMaChuong.Items.Clear();
                reader = dtBase.command("select machuong from chuong where maloai = '" + cbbMaLoai.Text + "'").ExecuteReader();
                if (reader.HasRows)
                {
                    // Đọc kết quả
                    while (reader.Read())
                    {
                       dsChuong.Add(reader[0].ToString());
                        cbbMaChuong.Items.Add(reader[0].ToString());
                    }
                }
            }
            dtBase.DongKetNoiCSDL();
            cbbSLThucAnSang.Items.Clear();
            cbbSLThucAnTrua.Items.Clear();
            cbbSLThucAnToi.Items.Clear();
            cbbSoLuongThu.Items.Clear();
            for (int i = 1;i<=10;i++)
            {
                cbbSLThucAnSang.Items.Add(i);
                cbbSLThucAnTrua.Items.Add(i);
                cbbSLThucAnToi.Items.Add(i);
                cbbSoLuongThu.Items.Add(i);
            }
            cbbThucAnSang.Items.Clear();
            cbbThucAnTrua.Items.Clear();
            cbbThucAntoi.Items.Clear();
            cbbSachDoThu.Items.Clear();
            cbbGioiTinhThu.Items.Clear();
            reader = dtBase.command("select tenthucan from thucan ").ExecuteReader();
            if (reader.HasRows)
            {
                // Đọc kết quả
                while (reader.Read())
                {
                    
                    cbbThucAnSang.Items.Add(reader[0].ToString());
                    cbbThucAnTrua.Items.Add(reader[0].ToString());
                    cbbThucAntoi.Items.Add(reader[0].ToString());
                }
            }
            cbbSachDoThu.Items.Add("có");
            cbbSachDoThu.Items.Add("Không");
            cbbGioiTinhThu.Items.Add("đực");
            cbbGioiTinhThu.Items.Add("cái");
            cbbGioiTinhThu.Items.Add("bê đê");
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
            DataTable dtThu = dtBase.DocDL("SELECT Loai.TenLoai, Loai.MaLoai," +
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
            else if
                (cbbMaLoai.Text == "")
            {
                MessageBox.Show("Bạn phải nhập mã loài");
                cbbMaLoai.Focus();
            }
            else if (cbbSoLuongThu.Text == "")
            {
                MessageBox.Show("Bạn phải nhập số lượng");
                cbbSoLuongThu.Focus();
            }
            else if (cbbSachDoThu.Text == "")
            {
                MessageBox.Show("Bạn phải nhập sách đỏ");
                cbbSachDoThu.Focus();
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
            else if (cbbGioiTinhThu.Text == "")
            {
                MessageBox.Show("Bạn phải nhập giới tính");
                cbbGioiTinhThu.Focus();
            }
            else if (dtpNgayVaoThu.Text == "")
            {
                MessageBox.Show("Bạn phải nhập ngày vào");
                dtpNgayVaoThu.Focus();
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
            else if (dtpNgaySinhThu.Text == "")
            {
                MessageBox.Show("Bạn phải nhập ngày sinh");
                dtpNgaySinhThu.Focus();
            }
            else if (txtTuoiTho.Text == "")
            {
                MessageBox.Show("Bạn phải nhập tuổi thọ");
                txtTuoiTho.Focus();
            }
            else if (IsDate(dtpNgayVaoThu.Text) == false)
            {
                MessageBox.Show("Ngày vào không hợp lệ (MM-DD-YYYY or MM/DD/YYYY) !");
                dtpNgayVaoThu.Focus();
            }
            else if (IsDate(dtpNgaySinhThu.Text) == false)
            {
                MessageBox.Show("Ngày sinh không hợp lệ (MM-DD-YYYY or MM/DD/YYYY) !");
                dtpNgaySinhThu.Focus();
            }
            else
            {
                DataTable dtChatLieu = dtBase.DocDL("Select * from Thu where" + " MaThu = '" + (txtMaThu.Text).Trim() + "'");
                if (dtChatLieu.Rows.Count > 0)
                {
                    MessageBox.Show("Mã thú này đã có, hãy nhập mã khác!");
                    txtMaThu.Focus();
                }
                else
                {
                    DataTable maLoai = dtBase.DocDL("Select MaLoai from Thu where" + " MaLoai = '" + (cbbMaLoai.Text).Trim() + "'");
                    if (maLoai.Rows.Count == 0)
                    {
                        MessageBox.Show("Mã loài không hợp lệ, hãy nhập mã khác!");
                        cbbMaLoai.Focus();
                    }
                    else
                    {
                        DataTable maChuong = dtBase.DocDL("Select MaChuong from Chuong where" + " MaChuong = '" + (cbbMaChuong.Text).Trim() + "'");
                        if (maChuong.Rows.Count == 0)
                        {
                            MessageBox.Show("Mã Chuồng không hợp lệ, hãy nhập mã khác!");
                            cbbMaChuong.Focus();
                        }
                        else
                        {
                            dtBase.CapNhatDuLieu("insert into Thu values(N'" +
                            txtMaThu.Text + "',N'" + txtTenThu.Text + "',N'" + cbbMaLoai.Text + "','" + Convert.ToInt32(cbbSoLuongThu.Text)
                            + "',N'" + cbbSachDoThu.Text + "',N'" + txtTenKHThu.Text + "',N'" + txtTenTA.Text
                            + "',N'" + txtTenTV.Text + "',N'" + txtKieuSinh.Text + "',N'" + cbbGioiTinhThu.Text
                            + "','" + Convert.ToDateTime(dtpNgayVaoThu.Text) + "',N'" + txtNguonGoc.Text
                            + "',N'" + txtDacDiemThu.Text + "','" + Convert.ToDateTime(dtpNgaySinhThu.Text) + "', '"
                            + ImageToBase64(openFileDialog.FileName) + "','" + Convert.ToInt32(txtTuoiTho.Text) + "')");

                            dtBase.CapNhatDuLieu("insert into Thu_chuong values('" + cbbMaChuong.Text + "','" + txtMaThu.Text + "','" + Convert.ToDateTime(dtpNgayVaoThu.Text) + "','" + txtLyDoVao.Text + "')");
                            MessageBox.Show("Bạn đã thêm mới thành công");
                            dtgvThu.DataSource = dtBase.DocDL("select   thu.MaThu,   tenThu,   thu.maLoai,   chuong.maChuong,soLuong,   sachDo,   thu.TenKhoaHoc,      tenTA,   tenTV,   kieuSinh, gioiTinh, thu.NgayVao, nguonGoc,dacDiem, ngaySinh,  tuoiTho,   Anh from thu, chuong, Thu_Chuong where Thu_Chuong.MaChuong = Chuong.MaChuong and Thu.MaThu = Thu_Chuong.MaThu");
                            pbThu.Image = null;
                            ResetValue();
                            btnLuuThu.Enabled = false;
                        }
                    }

                }
            }
        }

        private void DtgvThu_Click(object sender, EventArgs e)
        {
            btnSuaThu.Enabled = true;
            txtMaThu.Text = dtgvThu.CurrentRow.Cells[0].Value.ToString();
            cbbMaChuong.Text = dtgvThu.CurrentRow.Cells[3].Value.ToString();
            txtTenThu.Text = dtgvThu.CurrentRow.Cells[1].Value.ToString();
            cbbMaLoai.Text = dtgvThu.CurrentRow.Cells[2].Value.ToString();
            cbbSoLuongThu.Text = dtgvThu.CurrentRow.Cells[4].Value.ToString();
            cbbSachDoThu.Text = dtgvThu.CurrentRow.Cells[5].Value.ToString();
            txtTenKHThu.Text = dtgvThu.CurrentRow.Cells[6].Value.ToString();
            txtTenTA.Text = dtgvThu.CurrentRow.Cells[7].Value.ToString();
            txtTenTV.Text = dtgvThu.CurrentRow.Cells[8].Value.ToString();
            txtKieuSinh.Text = dtgvThu.CurrentRow.Cells[9].Value.ToString();
            cbbGioiTinhThu.Text = dtgvThu.CurrentRow.Cells[10].Value.ToString();
            dtpNgayVaoThu.Text = dtgvThu.CurrentRow.Cells[11].FormattedValue.ToString();
            txtNguonGoc.Text = dtgvThu.CurrentRow.Cells[12].Value.ToString();
            txtDacDiemThu.Text = dtgvThu.CurrentRow.Cells[13].Value.ToString();
            dtpNgaySinhThu.Text = dtgvThu.CurrentRow.Cells[14].FormattedValue.ToString();
            txtTuoiTho.Text = dtgvThu.CurrentRow.Cells[15].Value.ToString();
            if (dtgvThu.CurrentRow.Cells[16].Value.ToString().Length != 0)
            {
                pbThu.Image = Base64ToImage(dtgvThu.CurrentRow.Cells[16].Value.ToString());
            }

        }

        public string ImageToBase64(string images)
        {

            using (System.Drawing.Image image = System.Drawing.Image.FromFile(images))
            {
                using (MemoryStream m = new MemoryStream())
                {
                    image.Save(m, image.RawFormat);
                    byte[] imageBytes = m.ToArray();
                    string base64String = Convert.ToBase64String(imageBytes);
                    return base64String;
                }
            }
        }
        public Image Base64ToImage(string base64String)
        {
            byte[] imageBytes = Convert.FromBase64String(base64String);
            MemoryStream ms = new MemoryStream(imageBytes, 0, imageBytes.Length);
            ms.Write(imageBytes, 0, imageBytes.Length);
            System.Drawing.Image image = System.Drawing.Image.FromStream(ms, true);
            return image;
        }

        private void BtnHuyThu_Click(object sender, EventArgs e)
        {

        }

        private void Ribbon_Click(object sender, EventArgs e)
        {

        }

        private void PbThu_Click(object sender, EventArgs e)
        {

            openFileDialog.Filter = "Image Files(*.jpg; *.gif; *.png;) | *.jpg; *.gif; *.png;";
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                pbThu.Image = new Bitmap(openFileDialog.FileName);

            }
        }

        private void BtnTimKiemThu_Click(object sender, EventArgs e)
        {


        }

        private void BtnSuaThu_Click(object sender, EventArgs e)
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
            else if (cbbMaLoai.Text == "")
            {
                MessageBox.Show("Bạn phải nhập mã loài");
                cbbMaLoai.Focus();
            }
            else if (cbbSoLuongThu.Text == "")
            {
                MessageBox.Show("Bạn phải nhập số lượng");
                cbbSoLuongThu.Focus();
            }
            else if (cbbSachDoThu.Text == "")
            {
                MessageBox.Show("Bạn phải nhập sách đỏ");
                cbbSachDoThu.Focus();
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
            else if (cbbGioiTinhThu.Text == "")
            {
                MessageBox.Show("Bạn phải nhập giới tính");
                cbbGioiTinhThu.Focus();
            }
            else if (dtpNgayVaoThu.Text == "")
            {
                MessageBox.Show("Bạn phải nhập ngày vào");
                dtpNgayVaoThu.Focus();
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
            else if (dtpNgaySinhThu.Text == "")
            {
                MessageBox.Show("Bạn phải nhập ngày sinh");
                dtpNgaySinhThu.Focus();
            }
            else if (txtTuoiTho.Text == "")
            {
                MessageBox.Show("Bạn phải nhập tuổi thọ");
                txtTuoiTho.Focus();
            }
            else if (IsDate(dtpNgayVaoThu.Text) == false)
            {
                MessageBox.Show("Ngày vào không hợp lệ (MM-DD-YYYY or MM/DD/YYYY) !");
                dtpNgayVaoThu.Focus();
            }
            else if (IsDate(dtpNgaySinhThu.Text) == false)
            {
                MessageBox.Show("Ngày sinh không hợp lệ (MM-DD-YYYY or MM/DD/YYYY) !");
                dtpNgaySinhThu.Focus();
            }
            else
            {
                
                if(openFileDialog.FileName.Length == 0)
                {
                    dtBase.CapNhatDuLieu("update thu  set mathu =  N'" + txtMaThu.Text + "',tenthu =  N'" + txtTenThu.Text + "'," +
                        "maloai =  N'" + cbbMaLoai.Text + "',soluong =  N'" + Convert.ToInt32(cbbSoLuongThu.Text) + "',sachdo =  N'" 
                        + cbbSachDoThu.Text + "',tenkhoahoc =  N'" + txtTenKHThu.Text + "',tenTA =  N'" + txtTenTA.Text 
                        + "',tenTV =  N'" + txtTenTV.Text + "',kieusinh =  N'" + txtKieuSinh.Text + "',gioitinh =  N'" 
                        + cbbGioiTinhThu.Text + "',ngayvao =  '" + Convert.ToDateTime(dtpNgayVaoThu.Text) + "'," +
                        "nguongoc=  N'" + txtNguonGoc.Text + "',dacdiem =  N'" + txtDacDiemThu.Text + "'," +
                        "ngaysinh =  '" + Convert.ToDateTime(dtpNgaySinhThu.Text) + "',tuoitho =  '" 
                        +Convert.ToInt32( txtTuoiTho.Text) + "' where mathu ='" + txtMaThu.Text + "'");
                }
                else
                {
                    dtBase.CapNhatDuLieu("update thu  set mathu =  N'" + txtMaThu.Text + "',tenthu =  N'" 
                        + txtTenThu.Text + "',maloai =  N'" + cbbMaLoai.Text + "',soluong =  '" + Convert.ToInt32(cbbSoLuongThu.Text) 
                        + "',sachdo =  N'" + cbbSachDoThu.Text + "',tenkhoahoc =  N'" + txtTenKHThu.Text + "'," +
                        "tenTA =  N'" + txtTenTA.Text + "',tenTV =  N'" + txtTenTV.Text + "',kieusinh =  N'" 
                        + txtKieuSinh.Text + "',gioitinh =  N'" + cbbGioiTinhThu.Text + "',ngayvao =  '" 
                        + Convert.ToDateTime(dtpNgayVaoThu.Text) + "',nguongoc=  N'" + txtNguonGoc.Text 
                        + "',dacdiem =  N'" + txtDacDiemThu.Text + "',ngaysinh =  '" 
                        + Convert.ToDateTime(dtpNgaySinhThu.Text) + "',tuoitho =  '" + Convert.ToInt32(txtTuoiTho.Text )
                        + "', anh = '" + ImageToBase64(openFileDialog.FileName) + "' where mathu ='" 
                        + txtMaThu.Text + "'");
                }
                dtBase.CapNhatDuLieu("update thu_chuong  set machuong =  '" + cbbMaChuong.Text + "'," +
                    "mathu =  '" + txtMaThu.Text + "',ngayvao =  '" + Convert.ToDateTime(dtpNgayVaoThu.Text) 
                    + "',lydovao =  '" + txtLyDoVao.Text + "' where machuong = '"+cbbMaChuong.Text+"' and mathu = '"+txtMaThu.Text+"'" );
                MessageBox.Show("Bạn đã sửa thành công");
                dtgvThu.DataSource = dtBase.DocDL("select   thu.MaThu,   tenThu,   thu.maLoai," +
                    "   chuong.maChuong,soLuong,   sachDo,   thu.TenKhoaHoc,      tenTA,   tenTV, " +
                    "  kieuSinh, gioiTinh, thu.NgayVao, nguonGoc,dacDiem, ngaySinh,  tuoiTho,   Anh from thu," +
                    " chuong, Thu_Chuong where Thu_Chuong.MaChuong = Chuong.MaChuong and Thu.MaThu = Thu_Chuong.MaThu");
                ResetValue();
                btnSuaThu.Enabled = false;
            }
        }

        private void BtnXoaThu_Click(object sender, EventArgs e)
        {
            if (txtMaThu.Text.Length == 0) MessageBox.Show("bạn chưa nhập mã thú");
            else
            {
                if (DialogResult.Yes == MessageBox.Show("bạn có muốn xóa", "message", MessageBoxButtons.YesNo, MessageBoxIcon.Warning))
                {
                    dtBase.CapNhatDuLieu("delete from thu where mathu ='" + txtMaThu.Text + "'");
                    MessageBox.Show("Bạn đã xóa thành công");
                    dtgvThu.DataSource = dtBase.DocDL("select   thu.MaThu,   tenThu,   thu.maLoai,   chuong.maChuong,soLuong,   sachDo,   thu.TenKhoaHoc,      tenTA,   tenTV,   kieuSinh, gioiTinh, thu.NgayVao, nguonGoc,dacDiem, ngaySinh,  tuoiTho,   Anh from thu, chuong, Thu_Chuong where Thu_Chuong.MaChuong = Chuong.MaChuong and Thu.MaThu = Thu_Chuong.MaThu order by maloai ASC");
                    ResetValue();
                }
                
            }
        }

        private void CbbMaLoai_SelectedIndexChanged(object sender, EventArgs e)
        {
            cbbMaChuong.Items.Clear();
            SqlDataReader reader = dtBase.command("select machuong from chuong where maloai = '" + cbbMaLoai.Text + "'").ExecuteReader();
            if (reader.HasRows)
            {
                // Đọc kết quả
                while (reader.Read())
                {
                    dsChuong.Add(reader[0].ToString());
                    cbbMaChuong.Items.Add(reader[0].ToString());
                }
            }
        }
    }
}