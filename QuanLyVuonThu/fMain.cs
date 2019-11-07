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
using QuanLyVuonThu.Controller;


namespace QuanLyVuonThu
{
    public partial class fMain : DevExpress.XtraBars.Ribbon.RibbonForm
    {
        int click;
        ChuongController ChuongController = new ChuongController();
        ThucAnController ThucAnController = new ThucAnController();
        nhanVienController nhanVienController = new nhanVienController();
        TrangThaiController TrangThaiController = new TrangThaiController();
        thuController thuController = new thuController();
        string maCuongPrevious = "";
        string maNhanvien = "";
        string maThu = "";
        string maThucAn = "";
        string maChuong = "";
        int soLuongThuPrevious = 0;
        int soLuongThu = 0;
        List<ModelTrangThai> trangThais = new List<ModelTrangThai>();
        List<string> listMaThucAn = new List<string>();
        List<string> listMaChuong = new List<string>();
        List<string> ListmaNhanVien = new List<string>();
        List<ModelThucAn> modelThucAns = new List<ModelThucAn>();  // danh sach cac loai thuc an
        List<modelNhanVien> nhanViens = new List<modelNhanVien>();
        List<string> dsLoai = new List<string>();  // danh sach loai thu
                                                   // danh sach chuong thu
        List<ModelThu> modelThus = new List<ModelThu>();  // danh sach thu
        OpenFileDialog openFileDialog = new OpenFileDialog(); // mo file
        OpenFileDialog fileDialog = new OpenFileDialog();
        public bool IsDate(string str)
        {
            Regex regex = new Regex(@"^(0?[1-9]|1[012])[\/\-](0?[1-9]|[12][0-9]|3[01])[\/\-]\d{4}$");
            return regex.IsMatch(str);
        }

        ProcessDataBase dtBase = new ProcessDataBase();
        public void ResetValue() // reset button, cbb, rdb, dtp
        {
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
            cbbKieuSinh.Text = "";
            cbbGioiTinhThu.Text = "";
            txtTuoiTho.Text = "";
            dtpNgayVaoThu.Text = "";
            txtNguonGoc.Text = "";
            dtpNgaySinhThu.Text = "";
            cbbMaChuong.Text = "";
            cbbThucAnSang.Text = "";
            cbbThucAnTrua.Text = "";
            cbbThucAntoi.Text = "";
            cbbSLThucAnSang.Text = "";
            cbbSLThucAnTrua.Text = "";
            cbbSLThucAnToi.Text = "";
            txtLyDoVao.Text = "";
        }
        public fMain()
        {
            InitializeComponent();
        }
        private void load()
        {
            cbbSLThucAnSang.Items.Clear();
            cbbSLThucAnTrua.Items.Clear();
            cbbSLThucAnToi.Items.Clear();
            cbbSoLuongThu.Items.Clear();
            cbbThucAnSang.Items.Clear();
            cbbThucAnTrua.Items.Clear();
            cbbThucAntoi.Items.Clear();
            cbbSachDoThu.Items.Clear();
            cbbGioiTinhThu.Items.Clear();
            tclDS.Appearance = TabAppearance.FlatButtons;
            tclDS.ItemSize = new Size(0, 1);
            tclDS.SizeMode = TabSizeMode.Fixed;
            //Gọi pt DocBang lấy dữ liệu của bảng tblChatLieu đổ vào DataTable
            DataTable dtTableThu = dtBase.DocDL("select   thu.MaThu,   tenThu,   thu.maLoai,   chuong.maChuong,soLuong,   sachDo,   thu.TenKhoaHoc,      tenTA,   tenTV,   kieuSinh, gioiTinh, thu.NgayVao, nguonGoc,dacDiem, ngaySinh,  tuoiTho,MaThucAnSang,SLThucAnSang,MaThucAnTrua,SLThucAnTrua,MaThucAnToi,SlThucAnToi ,  Anh from thu, chuong, Thu_Chuong,Thu_ThucAn  where Thu_Chuong.MaChuong = Chuong.MaChuong " +
                "and Thu.MaThu = Thu_Chuong.MaThu and Thu_ThucAn.MaThu = Thu.MaThu order by maloai ASC");
            dtgvThu.DataSource = dtTableThu;
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
            dtgvThu.Columns[22].HeaderText = "Ảnh";
            dtgvThu.Columns[15].HeaderText = "Tuổi thọ";
            dtgvThu.Columns[16].HeaderText = "Mã TA sáng";
            dtgvThu.Columns[17].HeaderText = "Số Lượng";
            dtgvThu.Columns[18].HeaderText = "Mã TA Trưa";
            dtgvThu.Columns[19].HeaderText = "Số lượng";
            dtgvThu.Columns[20].HeaderText = "Mã TA tối";
            dtgvThu.Columns[21].HeaderText = "Số lượng";

            dtgvThu.BackgroundColor = Color.LightBlue;
            dtTableThu.Dispose();
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
            if (cbbMaLoai.Text.Length != 0)
            {
                cbbMaChuong.Items.Clear();
                reader = dtBase.command("select machuong from chuong where maloai = '" + cbbMaLoai.Text + "'").ExecuteReader();
                if (reader.HasRows)
                {
                    // Đọc kết quả
                    while (reader.Read())
                    {
                        cbbMaChuong.Items.Add(reader[0].ToString());
                    }
                }
            }
            dtBase.DongKetNoiCSDL();
            for (int i = 1; i <= 10; i++)
            {
                cbbSLThucAnSang.Items.Add(i);
                cbbSLThucAnTrua.Items.Add(i);
                cbbSLThucAnToi.Items.Add(i);
                cbbSoLuongThu.Items.Add(i);
            }

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
            reader = dtBase.command("select mathucan,tenthucan from thucan ").ExecuteReader();
            if (reader.HasRows)
            {
                // Đọc kết quả
                while (reader.Read())
                {
                    modelThucAns.Add(new ModelThucAn(reader[0].ToString(), reader[1].ToString()));
                }
            }
            nhanViens = nhanVienController.listNV();
            for (int i = 0; i < nhanViens.Count; i++)
            {
                cbbNhanVienTrongCoi.Items.Add(nhanViens[i].TenNhanVien);
            }
            trangThais = TrangThaiController.listTrangThai();
            for (int i = 0; i < trangThais.Count; i++)
            {
                cbbTrangthai.Items.Add(TrangThaiController.getTenTrangThai(trangThais[i].MaTrangThai));
            }
            List<ModelChuong> chuongs = new List<ModelChuong>();
            chuongs = ChuongController.chuongs();
            DataTable table = ChuongController.ConvertListToDataTable(chuongs);
            dtgvChuong.DataSource = table;
            dtgvChuong.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dtgvChuong.Columns[0].HeaderText = "Mã Chuồng";
            dtgvChuong.Columns[1].HeaderText = "Mã Loài";
            dtgvChuong.Columns[2].HeaderText = "Mã Khu";
            dtgvChuong.Columns[3].HeaderText = "Diện tích";
            dtgvChuong.Columns[4].HeaderText = "Chiều cao";
            dtgvChuong.Columns[5].HeaderText = "Số Lượng";
            dtgvChuong.Columns[6].HeaderText = "Mã trạng thái";
            dtgvChuong.Columns[7].HeaderText = "Mã Nhân viên";
            dtgvChuong.Columns[8].HeaderText = "Ghi chú";

            List<ModelThucAn> thucAns = new List<ModelThucAn>();
            thucAns = ThucAnController.ListThucAn();
            DataTable tableThucAn = ThucAnController.ConvertListToDataTable(thucAns);
            dtgvThucAn.DataSource = tableThucAn;
            dtgvThucAn.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dtgvThucAn.Columns[0].HeaderText = "Mã Thức ăn";
            dtgvThucAn.Columns[1].HeaderText = "Tên thức ăn";
            dtgvThucAn.Columns[2].HeaderText = "Công dụng";
            dtgvThucAn.Columns[3].HeaderText = "Nhà cung cấp";

            List<modelNhanVien> lstNV = new List<modelNhanVien>();
            lstNV = nhanVienController.listNV();
            DataTable tableNV = nhanVienController.ConvertListToDataTable(lstNV);
            dtgvNhanVien.DataSource = tableNV;
            dtgvNhanVien.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dtgvNhanVien.Columns[0].HeaderText = "mã nhân viên";
            dtgvNhanVien.Columns[1].HeaderText = "tên nhân viên";
            dtgvNhanVien.Columns[2].HeaderText = "địa chỉ";
            dtgvNhanVien.Columns[3].HeaderText = "giới tính";
            dtgvNhanVien.Columns[4].HeaderText = "ngày sinh";
            dtgvNhanVien.Columns[5].HeaderText = "số điện thoại";
            dtgvNhanVien.Columns[6].HeaderText = "ảnh";
        }

        // load du lieu cho form
        private void FMain_Load(object sender, EventArgs e)
        {
            load();
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
        private string getMaThucAn(string tenthucan)
        {
            string maTA = "";
            for (int i = 0; i < modelThucAns.Count; i++)
            {
                if (tenthucan.Equals(modelThucAns[i].TenThucAn))
                {
                    maTA = modelThucAns[i].MaThucAn;
                }
            }
            return maTA;
        }

        private string getTenThucAn(string mathucan)
        {
            string TenTA = "";
            for (int i = 0; i < modelThucAns.Count; i++)
            {
                if (mathucan.Equals(modelThucAns[i].MaThucAn))
                {
                    TenTA = modelThucAns[i].TenThucAn;
                }
            }
            return TenTA;
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

        private bool check()
        {
            if (txtMaThu.Text == "")
            {
                MessageBox.Show("Bạn phải nhập mã thú");
                txtMaThu.Focus();
                return false;
            }
            else if (txtTenThu.Text == "")
            {
                MessageBox.Show("Bạn phải nhập tên thú");
                txtTenThu.Focus();
                return false;
            }
            else if
                (cbbMaLoai.Text == "")
            {
                MessageBox.Show("Bạn phải nhập mã loài");
                cbbMaLoai.Focus();
                return false;
            }
            else if (cbbSoLuongThu.Text == "")
            {
                MessageBox.Show("Bạn phải nhập số lượng");
                cbbSoLuongThu.Focus();
                return false;
            }
            else if (cbbSachDoThu.Text == "")
            {
                MessageBox.Show("Bạn phải nhập sách đỏ");
                cbbSachDoThu.Focus();
                return false;
            }
            else if (txtTenKHThu.Text == "")
            {
                MessageBox.Show("Bạn phải nhập tên khoa học");
                txtTenKHThu.Focus();
                return false;
            }
            else if (txtTenTA.Text == "")
            {
                MessageBox.Show("Bạn phải nhập tên tiếng anh");
                txtTenTA.Focus();
                return false;
            }
            else if (txtTenTV.Text == "")
            {
                MessageBox.Show("Bạn phải nhập tên tiếng việt");
                txtTenTV.Focus();
                return false;
            }
            else if (cbbKieuSinh.Text == "")
            {
                MessageBox.Show("Bạn phải nhập kiểu sinh");
                cbbKieuSinh.Focus();
                return false;
            }
            else if (cbbGioiTinhThu.Text == "")
            {
                MessageBox.Show("Bạn phải nhập giới tính");
                cbbGioiTinhThu.Focus();
                return false;
            }
            else if (dtpNgayVaoThu.Text == "")
            {
                MessageBox.Show("Bạn phải nhập ngày vào");
                dtpNgayVaoThu.Focus();
                return false;
            }
            else if (txtNguonGoc.Text == "")
            {
                MessageBox.Show("Bạn phải nhập nguồn gốc");
                txtNguonGoc.Focus();
                return false;
            }
            else if (txtDacDiemThu.Text == "")
            {
                MessageBox.Show("Bạn phải nhập đặc điểm");
                txtDacDiemThu.Focus();
                return false;
            }
            else if (dtpNgaySinhThu.Text == "")
            {
                MessageBox.Show("Bạn phải nhập ngày sinh");
                dtpNgaySinhThu.Focus();
                return false;
            }
            else if (txtTuoiTho.Text == "")
            {
                MessageBox.Show("Bạn phải nhập tuổi thọ");
                txtTuoiTho.Focus();
                return false;
            }
            else if (IsDate(dtpNgayVaoThu.Text) == false)
            {
                MessageBox.Show("Ngày vào không hợp lệ (MM-DD-YYYY or MM/DD/YYYY) !");
                dtpNgayVaoThu.Focus();
                return false;
            }
            else if (IsDate(dtpNgaySinhThu.Text) == false)
            {
                MessageBox.Show("Ngày sinh không hợp lệ (MM-DD-YYYY or MM/DD/YYYY) !");
                dtpNgaySinhThu.Focus();
                return false;
            }
            else if (cbbMaChuong.Text.Length == 0)
            {
                MessageBox.Show("bạn chưa chọn mã chuồng !");
                cbbMaChuong.Focus();
                return false;
            }
            else if (txtLyDoVao.Text.Length == 0)
            {
                MessageBox.Show("bạn chưa nhập lý do vào!");
                txtLyDoVao.Focus();
                return false;
            }
            else if (cbbThucAnSang.Text.Length == 0)
            {
                MessageBox.Show("bạn chưa chọn thức ăn sáng");
                cbbThucAnSang.Focus();
                return false;
            }
            else if (cbbThucAnTrua.Text.Length == 0)
            {
                MessageBox.Show("b chưa chọn thức ăn trưa");
                cbbThucAnTrua.Focus();
                return false;
            }
            else if (cbbThucAntoi.Text.Length == 0)
            {
                MessageBox.Show("b chưa chọn thức ăn tối!");
                cbbThucAntoi.Focus();
                return false;
            }
            else if (cbbSLThucAnSang.Text.Length == 0)
            {
                MessageBox.Show("b chưa chọn số lượng thức ăn sáng!");
                cbbSLThucAnSang.Focus();
                return false;
            }
            else if (cbbSLThucAnTrua.Text.Length == 0)
            {
                MessageBox.Show("b chưa chọn số lượng thức ăn trưa!");
                cbbSLThucAnTrua.Focus();
                return false;
            }
            else if (cbbSLThucAnToi.Text.Length == 0)
            {
                MessageBox.Show("b chưa chọn số lượng thức ăn tối!");
                cbbSLThucAnToi.Focus();
                return false;
            }
            else
            {
                DataTable dtgvThu1 = null;

                if (click == 1)
                {
                    SqlDataReader reader = dtBase.command("Select mathu from Thu where" + " MaThu != '" + (txtMaThu.Text).Trim() + "'").ExecuteReader();
                    if (reader.HasRows)
                    {
                        // Đọc kết quả
                        while (reader.Read())
                        {
                            if (txtMaThu.Text.Equals(reader[0].ToString()))
                            {
                                MessageBox.Show("mã thú trùng với mã thú đã có");
                                break;
                            }
                        }
                    }
                    DataTable maLoai = dtBase.DocDL("Select MaLoai from Thu where" + " MaLoai = '" + (cbbMaLoai.Text).Trim() + "'");
                    if (maLoai.Rows.Count == 0)
                    {
                        MessageBox.Show("Mã loài không hợp lệ, hãy nhập mã khác!");
                        cbbMaLoai.Focus();
                        return false;
                    }
                    else
                    {
                        DataTable maChuong = dtBase.DocDL("Select MaChuong from Chuong where" + " MaChuong = '" + (cbbMaChuong.Text).Trim() + "'");
                        if (maChuong.Rows.Count == 0)
                        {
                            MessageBox.Show("Mã Chuồng không hợp lệ, hãy nhập mã khác!");
                            cbbMaChuong.Focus();
                            return false;
                        }
                        else return true;
                    }
                }
                else
                {
                    if (click == 0)
                    {
                        /*                        dtgvThu1 = dtBase.DocDL("Select * from Thu where" + " MaThu = '" + (txtMaThu.Text).Trim() + "'");
                                                if (dtgvThu1.Rows.Count > 0)
                                                {
                                                    MessageBox.Show("Mã thú này đã có, hãy nhập mã khác!");
                                                    txtMaThu.Focus();
                                                    return false;
                                                }
                                                DataTable maLoai = dtBase.DocDL("Select MaLoai from Thu where" + " MaLoai = '" + (cbbMaLoai.Text).Trim() + "'");
                                                if (maLoai.Rows.Count == 0)
                                                {
                                                    MessageBox.Show("Mã loài không hợp lệ, hãy nhập mã khác!");
                                                    cbbMaLoai.Focus();
                                                    return false;
                                                }
                                                else
                                                {
                                                    DataTable maChuong = dtBase.DocDL("Select MaChuong from Chuong where" + " MaChuong = '" + (cbbMaChuong.Text).Trim() + "'");
                                                    if (maChuong.Rows.Count == 0)
                                                    {
                                                        MessageBox.Show("Mã Chuồng không hợp lệ, hãy nhập mã khác!");
                                                        cbbMaChuong.Focus();
                                                        return false;
                                                    }
                                                    else return true;
                                                }*/
                        return true;
                    }
                }
                return false;
            }
        }
        private void BtnThemThu_Click(object sender, EventArgs e)
        {
            click = 0;
            int maxMathu = 0;
            List<int> LSTmathus = new List<int>();
            SqlDataReader readerMathu = dtBase.command("select mathu from thu").ExecuteReader();
            if (readerMathu.HasRows)
            {
                // Đọc kết quả
                while (readerMathu.Read())
                {
                    LSTmathus.Add(Convert.ToInt32(readerMathu[0].ToString().Substring(2)));
                }
            }
            if (LSTmathus.Count != 0)
            {
                maxMathu = LSTmathus[0];
                for (int i = 1; i < LSTmathus.Count; i++)
                {
                    if (maxMathu < LSTmathus[i]) maxMathu = LSTmathus[i];
                }
            }
            maxMathu += 1;
            if (check() == false) return;
            else
            {
                if (openFileDialog.FileName.Length != 0)
                {
                    dtBase.CapNhatDuLieu("insert into Thu values(N'MT" +
                maxMathu + "',N'" + txtTenThu.Text + "',N'" + cbbMaLoai.Text + "','" + Convert.ToInt32(cbbSoLuongThu.Text)
                + "',N'" + cbbSachDoThu.Text + "',N'" + txtTenKHThu.Text + "',N'" + txtTenTA.Text
                + "',N'" + txtTenTV.Text + "',N'" + cbbKieuSinh.Text + "',N'" + cbbGioiTinhThu.Text
                + "','" + Convert.ToDateTime(dtpNgayVaoThu.Text) + "',N'" + txtNguonGoc.Text
                + "',N'" + txtDacDiemThu.Text + "','" + Convert.ToDateTime(dtpNgaySinhThu.Text) + "', '"
                + ImageToBase64(openFileDialog.FileName) + "','" + Convert.ToInt32(txtTuoiTho.Text) + "')");

                }
                else
                {
                    dtBase.CapNhatDuLieu("insert into Thu values(N'MT" +
                maxMathu + "',N'" + txtTenThu.Text + "',N'" + cbbMaLoai.Text + "','" + Convert.ToInt32(cbbSoLuongThu.Text)
                + "',N'" + cbbSachDoThu.Text + "',N'" + txtTenKHThu.Text + "',N'" + txtTenTA.Text
                + "',N'" + txtTenTV.Text + "',N'" + cbbKieuSinh.Text + "',N'" + cbbGioiTinhThu.Text
                + "','" + Convert.ToDateTime(dtpNgayVaoThu.Text) + "',N'" + txtNguonGoc.Text
                + "',N'" + txtDacDiemThu.Text + "','" + Convert.ToDateTime(dtpNgaySinhThu.Text) + "', '','" + Convert.ToInt32(txtTuoiTho.Text) + "')");

                }

                dtBase.CapNhatDuLieu("insert into Thu_chuong values(N'" + cbbMaChuong.Text + "',N'MT" + maxMathu + "','" + Convert.ToDateTime(dtpNgayVaoThu.Text) + "',N'" + txtLyDoVao.Text + "')");
                dtBase.CapNhatDuLieu("insert into Thu_thucan values(N'MT" + maxMathu + "',N'"
                    + getMaThucAn(cbbThucAnSang.Text) + "',N'" + Convert.ToInt32(cbbSLThucAnSang.Text) + "',N'"
                    + getMaThucAn(cbbThucAnTrua.Text) + "','" + Convert.ToInt32(cbbSLThucAnTrua.Text) + "',N'"
                    + getMaThucAn(cbbThucAntoi.Text) + "','" + Convert.ToInt32(cbbSLThucAnToi.Text) + "')");

                SqlDataReader reader = dtBase.command("select soluongthu from chuong where machuong = '" + cbbMaChuong.Text + "' ").ExecuteReader();
                if (reader.HasRows)
                {
                    // Đọc kết quả
                    while (reader.Read())
                    {
                        soLuongThu = Convert.ToInt32(reader[0].ToString());
                    }
                }

                soLuongThu = soLuongThu + Convert.ToInt32(cbbSoLuongThu.Text);
                dtBase.CapNhatDuLieu("update chuong set soluongthu ='" + soLuongThu + "' where machuong ='" + cbbMaChuong.Text + "' ");

                MessageBox.Show("Bạn đã thêm mới thành công");
                dtgvThu.DataSource = dtBase.DocDL("select   thu.MaThu,   tenThu,   thu.maLoai,   chuong.maChuong,soLuong,   sachDo,   thu.TenKhoaHoc,      tenTA,   tenTV,   kieuSinh, gioiTinh, thu.NgayVao, nguonGoc,dacDiem, ngaySinh,  tuoiTho,MaThucAnSang,SLThucAnSang,MaThucAnTrua,SLThucAnTrua,MaThucAnToi,SlThucAnToi ,  Anh from thu, chuong, Thu_Chuong,Thu_ThucAn  where Thu_Chuong.MaChuong = Chuong.MaChuong " +
    "and Thu.MaThu = Thu_Chuong.MaThu and Thu_ThucAn.MaThu = Thu.MaThu order by maloai ASC");
                pbThu.Image = null;
                ResetValue();
            }
            load();
            soLuongThu = 0;
            soLuongThuPrevious = 0;
        }

        private void BtnLuuThu_Click(object sender, EventArgs e)
        {
            ResetValue();
        }

        private void DtgvThu_Click(object sender, EventArgs e)
        {
            if (dtgvThu.CurrentRow.Cells[4].Value.ToString().Length != 0)
                soLuongThuPrevious = Convert.ToInt32(dtgvThu.CurrentRow.Cells[4].Value.ToString());
            if (dtgvThu.CurrentRow.Cells[3].Value.ToString().Length != 0)
                maCuongPrevious = dtgvThu.CurrentRow.Cells[3].Value.ToString();
            maThu = dtgvThu.CurrentRow.Cells[0].Value.ToString();
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
            cbbKieuSinh.Text = dtgvThu.CurrentRow.Cells[9].Value.ToString();
            cbbGioiTinhThu.Text = dtgvThu.CurrentRow.Cells[10].Value.ToString();
            dtpNgayVaoThu.Text = dtgvThu.CurrentRow.Cells[11].FormattedValue.ToString();
            txtNguonGoc.Text = dtgvThu.CurrentRow.Cells[12].Value.ToString();
            txtDacDiemThu.Text = dtgvThu.CurrentRow.Cells[13].Value.ToString();
            dtpNgaySinhThu.Text = dtgvThu.CurrentRow.Cells[14].FormattedValue.ToString();
            txtTuoiTho.Text = dtgvThu.CurrentRow.Cells[15].Value.ToString();
            cbbThucAnSang.Text = getTenThucAn(dtgvThu.CurrentRow.Cells[16].Value.ToString());
            cbbSLThucAnSang.Text = dtgvThu.CurrentRow.Cells[17].Value.ToString();
            cbbThucAnTrua.Text = getTenThucAn(dtgvThu.CurrentRow.Cells[18].Value.ToString());
            cbbSLThucAnTrua.Text = dtgvThu.CurrentRow.Cells[19].Value.ToString();
            cbbThucAntoi.Text = getTenThucAn(dtgvThu.CurrentRow.Cells[20].Value.ToString());
            cbbSLThucAnToi.Text = dtgvThu.CurrentRow.Cells[21].Value.ToString();
            if (dtgvThu.CurrentRow.Cells[22].Value.ToString().Length == 0)
            {
                pbThu.Image = null;
            }
            else pbThu.Image = Base64ToImage(dtgvThu.CurrentRow.Cells[22].Value.ToString());
            SqlDataReader reader = dtBase.command("select lydovao from thu_chuong where machuong = '" + dtgvThu.CurrentRow.Cells[3].Value.ToString() + "' and mathu = '" + dtgvThu.CurrentRow.Cells[0].Value.ToString() + "'").ExecuteReader();
            if (reader.HasRows)
            {
                // Đọc kết quả
                while (reader.Read())
                {
                    txtLyDoVao.Text = reader[0].ToString();
                }
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
            // dtBase là class kết nối database khai báo trên đầu
            string sql = " select   thu.MaThu,   tenThu,   thu.maLoai,   chuong.maChuong,soLuong,   sachDo,   thu.TenKhoaHoc,      tenTA,   tenTV,   kieuSinh, gioiTinh, thu.NgayVao, nguonGoc,dacDiem, ngaySinh,  tuoiTho,MaThucAnSang,SLThucAnSang,MaThucAnTrua,SLThucAnTrua,MaThucAnToi,SlThucAnToi ,  Anh from loai,thu, chuong, Thu_Chuong,Thu_ThucAn  where Thu_Chuong.MaChuong = Chuong.MaChuong " +
                "and Thu.MaThu = Thu_Chuong.MaThu and Thu_ThucAn.MaThu = Thu.MaThu and loai.maloai = Thu.maloai and (thu.mathu  like '%" + txtTimKiemThu.Text + "%' or thu.tenthu like '%" + txtTimKiemThu.Text + "%' or loai.maloai like '%" + txtTimKiemThu.Text + "%' or thu.kieusinh like '%" + txtTimKiemThu.Text + "%' or thu.nguongoc like '%" + txtTimKiemThu.Text + "%' )";
            Golobal.GolobalThu.giatritimkiem = txtTimKiemThu.Text;
            Golobal.GolobalThu.kquaTimKiemThu = dtBase.DocDL(sql);
            frmTKiemThu frmTKiemThu = new frmTKiemThu();
            frmTKiemThu.ShowDialog();


        }

        private void BtnSuaThu_Click(object sender, EventArgs e)
        {
            click = 1;
            if (check() == false) return;
            else
            {

                if (openFileDialog.FileName.Length == 0)
                {
                    dtBase.CapNhatDuLieu("update thu  set tenthu =  N'" + txtTenThu.Text + "'," +
                        "maloai =  N'" + cbbMaLoai.Text + "',soluong =  N'" + Convert.ToInt32(cbbSoLuongThu.Text) + "',sachdo =  N'"
                        + cbbSachDoThu.Text + "',tenkhoahoc =  N'" + txtTenKHThu.Text + "',tenTA =  N'" + txtTenTA.Text
                        + "',tenTV =  N'" + txtTenTV.Text + "',kieusinh =  N'" + cbbKieuSinh.Text + "',gioitinh =  N'"
                        + cbbGioiTinhThu.Text + "',ngayvao =  '" + Convert.ToDateTime(dtpNgayVaoThu.Text) + "'," +
                        "nguongoc=  N'" + txtNguonGoc.Text + "',dacdiem =  N'" + txtDacDiemThu.Text + "'," +
                        "ngaysinh =  '" + Convert.ToDateTime(dtpNgaySinhThu.Text) + "',tuoitho =  '"
                        + Convert.ToInt32(txtTuoiTho.Text) + "' where mathu ='" + txtMaThu.Text + "'");
                }
                else
                {
                    dtBase.CapNhatDuLieu("update thu  set tenthu =  N'"
                        + txtTenThu.Text + "',maloai =  N'" + cbbMaLoai.Text + "',soluong =  '" + Convert.ToInt32(cbbSoLuongThu.Text)
                        + "',sachdo =  N'" + cbbSachDoThu.Text + "',tenkhoahoc =  N'" + txtTenKHThu.Text + "'," +
                        "tenTA =  N'" + txtTenTA.Text + "',tenTV =  N'" + txtTenTV.Text + "',kieusinh =  N'"
                        + cbbKieuSinh.Text + "',gioitinh =  N'" + cbbGioiTinhThu.Text + "',ngayvao =  '"
                        + Convert.ToDateTime(dtpNgayVaoThu.Text) + "',nguongoc=  N'" + txtNguonGoc.Text
                        + "',dacdiem =  N'" + txtDacDiemThu.Text + "',ngaysinh =  '"
                        + Convert.ToDateTime(dtpNgaySinhThu.Text) + "',tuoitho =  '" + Convert.ToInt32(txtTuoiTho.Text)
                        + "', anh = '" + ImageToBase64(openFileDialog.FileName) + "' where mathu ='"
                        + txtMaThu.Text + "'");
                }
                dtBase.CapNhatDuLieu("update thu_chuong  set machuong =  N'" + cbbMaChuong.Text + "'," +
                    "mathu =  N'" + txtMaThu.Text + "',ngayvao =  '" + Convert.ToDateTime(dtpNgayVaoThu.Text)
                    + "',lydovao =  N'" + txtLyDoVao.Text + "' where machuong = '" + maCuongPrevious + "' and mathu = '" + txtMaThu.Text + "'");

                dtBase.CapNhatDuLieu("update thu_thucan  set " +
                    "mathucansang =  N'" + getMaThucAn(cbbThucAnSang.Text) + "',SLThucAnSang =  '" + Convert.ToInt32(cbbSLThucAnSang.Text)
                    + "',mathucantrua =  N'" + getMaThucAn(cbbThucAnTrua.Text) + "',SLThucAnTrua =  '" + Convert.ToInt32(cbbSLThucAnTrua.Text)
                    + "',mathucantoi =  N'" + getMaThucAn(cbbThucAntoi.Text) + "',SLThucAnToi =  '" + Convert.ToInt32(cbbSLThucAnToi.Text) + "' where  mathu = '" + maThu + "'");

                if (cbbMaChuong.Text.Equals(maCuongPrevious))
                {
                    SqlDataReader reader = dtBase.command("select soluongthu from chuong where machuong = '" + cbbMaChuong.Text + "' ").ExecuteReader();
                    if (reader.HasRows)
                    {
                        // Đọc kết quả
                        while (reader.Read())
                        {
                            soLuongThu = Convert.ToInt32(reader[0].ToString());
                        }
                    }
                    soLuongThu = soLuongThu - soLuongThuPrevious + Convert.ToInt32(cbbSoLuongThu.Text);
                    dtBase.CapNhatDuLieu("update chuong set soluongthu ='" + soLuongThu + "' where machuong ='" + cbbMaChuong.Text + "' ");
                }
                else
                {
                    int solgthuChuongcu = 0;
                    SqlDataReader reader = dtBase.command("select soluongthu from chuong where machuong = '" + cbbMaChuong.Text + "' ").ExecuteReader();

                    if (reader.HasRows)
                    {
                        // Đọc kết quả
                        while (reader.Read())
                        {
                            soLuongThu = Convert.ToInt32(reader[0].ToString());
                        }
                    }
                    soLuongThu = soLuongThu + Convert.ToInt32(cbbSoLuongThu.Text);
                    dtBase.CapNhatDuLieu("update chuong set soluongthu ='" + soLuongThu + "' where machuong ='" + cbbMaChuong.Text + "' ");

                    SqlDataReader reader1 = dtBase.command("select soluongthu from chuong where machuong = '" + maCuongPrevious + "' ").ExecuteReader();
                    if (reader1.HasRows)
                    {
                        // Đọc kết quả
                        while (reader1.Read())
                        {
                            solgthuChuongcu = Convert.ToInt32(reader1[0].ToString());
                        }
                    }
                    solgthuChuongcu = solgthuChuongcu - soLuongThuPrevious;
                    dtBase.CapNhatDuLieu("update chuong set soluongthu ='" + solgthuChuongcu + "' where machuong ='" + maCuongPrevious + "' ");

                }

                MessageBox.Show("Bạn đã sửa thành công");
                dtgvThu.DataSource = dtBase.DocDL("select   thu.MaThu,   tenThu,   thu.maLoai,   chuong.maChuong,soLuong,   sachDo,   thu.TenKhoaHoc,      tenTA,   tenTV,   kieuSinh, gioiTinh, thu.NgayVao, nguonGoc,dacDiem, ngaySinh,  tuoiTho,MaThucAnSang,SLThucAnSang,MaThucAnTrua,SLThucAnTrua,MaThucAnToi,SlThucAnToi ,  Anh from thu, chuong, Thu_Chuong,Thu_ThucAn  where Thu_Chuong.MaChuong = Chuong.MaChuong " +
                "and Thu.MaThu = Thu_Chuong.MaThu and Thu_ThucAn.MaThu = Thu.MaThu order by maloai ASC");
                ResetValue();
                btnSuaThu.Enabled = false;
            }
            load();
            soLuongThu = 0;
            soLuongThuPrevious = 0;
        }

        private void BtnXoaThu_Click(object sender, EventArgs e)
        {
            if (txtMaThu.Text.Length == 0) MessageBox.Show("bạn chưa nhập mã thú");
            else
            {
                if (DialogResult.Yes == MessageBox.Show("bạn có muốn xóa", "message", MessageBoxButtons.YesNo, MessageBoxIcon.Warning))
                {
                    dtBase.CapNhatDuLieu("delete from thu where mathu ='" + txtMaThu.Text + "'");
                    dtBase.CapNhatDuLieu("delete from thu_chuong where mathu ='" + txtMaThu.Text + "' and machuong ='" + cbbMaChuong.Text + "'");
                    dtBase.CapNhatDuLieu("delete from thu_thucan where mathu ='" + txtMaThu.Text + "'");
                    SqlDataReader reader = dtBase.command("select soluongthu from chuong where machuong = '" + cbbMaChuong.Text + "' ").ExecuteReader();
                    if (reader.HasRows)
                    {
                        // Đọc kết quả
                        while (reader.Read())
                        {
                            soLuongThu = Convert.ToInt32(reader[0].ToString());
                        }
                    }

                    soLuongThu = soLuongThu - soLuongThuPrevious;
                    dtBase.CapNhatDuLieu("update chuong set soluongthu ='" + soLuongThu + "' where machuong ='" + cbbMaChuong.Text + "' ");

                    MessageBox.Show("Bạn đã xóa thành công");
                    dtgvThu.DataSource = dtBase.DocDL("select   thu.MaThu,   tenThu,   thu.maLoai,   chuong.maChuong,soLuong,   sachDo,   thu.TenKhoaHoc,      tenTA,   tenTV,   kieuSinh, gioiTinh, thu.NgayVao, nguonGoc,dacDiem, ngaySinh,  tuoiTho,MaThucAnSang,SLThucAnSang,MaThucAnTrua,SLThucAnTrua,MaThucAnToi,SlThucAnToi ,  Anh from thu, chuong, Thu_Chuong,Thu_ThucAn  where Thu_Chuong.MaChuong = Chuong.MaChuong " +
                "and Thu.MaThu = Thu_Chuong.MaThu and Thu_ThucAn.MaThu = Thu.MaThu order by maloai ASC");
                    ResetValue();
                }

            }
            load();
            soLuongThu = 0;
            soLuongThuPrevious = 0;
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

                    cbbMaChuong.Items.Add(reader[0].ToString());
                }
            }
        }


        private void DtgvChuong_Click(object sender, EventArgs e)
        {
            maChuong = dtgvChuong.CurrentRow.Cells[0].Value.ToString();
            txtMaChuong.Text = dtgvChuong.CurrentRow.Cells[0].Value.ToString();
            cbbTenLoai.Text = dtgvChuong.CurrentRow.Cells[1].Value.ToString();
            txtMaKhuChuong.Text = dtgvChuong.CurrentRow.Cells[2].Value.ToString();
            txtDienTichChuong.Text = dtgvChuong.CurrentRow.Cells[3].Value.ToString();
            txtChieuCaoChuong.Text = dtgvChuong.CurrentRow.Cells[4].Value.ToString();
            txtSoLuongThuChuong.Text = dtgvChuong.CurrentRow.Cells[5].Value.ToString();
            cbbTrangthai.Text = TrangThaiController.getTenTrangThai(dtgvChuong.CurrentRow.Cells[6].Value.ToString());
            cbbNhanVienTrongCoi.Text = nhanVienController.getTenNhanVien(dtgvChuong.CurrentRow.Cells[7].Value.ToString());
            txtGhiChu.Text = dtgvChuong.CurrentRow.Cells[8].Value.ToString();
        }

        private void DtgvThucAn_Click(object sender, EventArgs e)
        {
            maThucAn = dtgvThucAn.CurrentRow.Cells[0].Value.ToString();
            txtMaThucAn.Text = dtgvThucAn.CurrentRow.Cells[0].Value.ToString();
            txtTenThucAn.Text = dtgvThucAn.CurrentRow.Cells[1].Value.ToString();
            txtCongDung.Text = dtgvThucAn.CurrentRow.Cells[2].Value.ToString();
            txtMaDonViThucAn.Text = dtgvThucAn.CurrentRow.Cells[3].Value.ToString();
        }

        private void BtnClearTA_Click(object sender, EventArgs e)
        {
            txtMaThucAn.Text = "";
            txtTenThucAn.Text = "";
            txtCongDung.Text = "";
            txtMaDonViThucAn.Text = "";
        }

        private void BtnThemThucAn_Click(object sender, EventArgs e)
        {
            if (txtMaThucAn.Text.Length == 0)
            {
                MessageBox.Show("bạn chưa nhập mã thức ăn");
            }
            else if (txtTenThucAn.Text.Length == 0)
            {
                MessageBox.Show("bạn chưa nhập tên thức ăn");
            }
            else if (txtCongDung.Text.Length == 0)
            {
                MessageBox.Show("bạn chưa nhập công dụng mã thức ăn");
            }
            else if (txtMaDonViThucAn.Text.Length == 0)
            {
                MessageBox.Show("bạn chưa nhập đơn vị cung cấp thức ăn");
            }
            else
            {
                listMaThucAn.Clear();
                int check = 0;
                SqlDataReader reader = dtBase.command("select mathucan from thucan  ").ExecuteReader();
                if (reader.HasRows)
                {
                    // Đọc kết quả
                    while (reader.Read())
                    {
                        listMaThucAn.Add(reader[0].ToString());
                    }
                }
                for (int i = 0; i < listMaThucAn.Count; i++)
                {
                    if (txtMaThucAn.Text.Equals(listMaThucAn[i]))
                    {
                        check = 0;
                        break;
                    }
                    else check = 1;
                }
                if (check == 0) MessageBox.Show("mã thức ăn đã tồn tại");
                else if (check == 1)
                {
                    string sql = " insert into thucan values('" + txtMaThucAn.Text + "','" + txtTenThucAn.Text + "','" + txtCongDung.Text + "','" + txtMaDonViThucAn + "')";
                    ThucAnController.themThucAn(sql);
                    MessageBox.Show("bạn đã thêm thành công");
                    dtgvThucAn.DataSource = dtBase.DocDL("select *  from thucan");
                }
                load();
            }


        }

        private void BtnXoaThucAn_Click(object sender, EventArgs e)
        {
            if (txtMaThucAn.Text.Length == 0)
            {
                MessageBox.Show("bạn chưa nhập mã thức ăn");
            }
            else
            {
                string sql = " delete from thucan where mathucan = '" + txtMaThucAn.Text + "'";
                dtBase.CapNhatDuLieu(sql);
                MessageBox.Show("bạn đã xóa thành công");
                dtgvThucAn.DataSource = dtBase.DocDL("select * from  thucan");
            }
        }

        private void BtnSuaThucAn_Click(object sender, EventArgs e)
        {
            if (txtMaThucAn.Text.Length == 0)
            {
                MessageBox.Show("bạn chưa nhập mã thức ăn");
            }
            else if (txtTenThucAn.Text.Length == 0)
            {
                MessageBox.Show("bạn chưa nhập tên thức ăn");
            }
            else if (txtCongDung.Text.Length == 0)
            {
                MessageBox.Show("bạn chưa nhập công dụng mã thức ăn");
            }
            else if (txtMaDonViThucAn.Text.Length == 0)
            {
                MessageBox.Show("bạn chưa nhập đơn vị cung cấp thức ăn");
            }
            else
            {
                listMaThucAn.Clear();
                int check = 0;
                SqlDataReader reader = dtBase.command("select mathucan from thucan where mathucan != '" + txtMaThucAn.Text + "' ").ExecuteReader();
                if (reader.HasRows)
                {
                    // Đọc kết quả
                    while (reader.Read())
                    {
                        listMaThucAn.Add(reader[0].ToString());
                    }
                }
                for (int i = 0; i < listMaThucAn.Count; i++)
                {
                    if (txtMaThucAn.Text.Equals(listMaThucAn[i]))
                    {
                        check = 0;
                        break;
                    }
                    else check = 1;
                }
                if (check == 0) MessageBox.Show("mã thức ăn đã tồn tại");
                else if (check == 1)
                {
                    string sql = " update thucan set mathucan = '" + txtMaThucAn.Text + "',tenthucan = N'" + txtTenThucAn.Text + "',congdung = N'" + txtCongDung.Text + "',madonvi = N'" + txtMaDonViThucAn.Text + "'  where mathucan = '" + maThucAn + "'";
                    ThucAnController.themThucAn(sql);
                    reader = dtBase.command("select mathucansang,mathucantrua,mathucantoi from thu_thucan").ExecuteReader();
                    if (reader.HasRows)
                    {
                        // Đọc kết quả
                        while (reader.Read())
                        {
                            if (reader[0].ToString().Equals(maThucAn))
                            {
                                string sqlupdate = " update thu_thucan set mathucansang = '" + txtMaThucAn.Text + "'  where mathucansang = '" + maThucAn + "'";
                                ThucAnController.themThucAn(sqlupdate);
                            }
                            if (reader[1].ToString().Equals(maThucAn))
                            {
                                string sqlupdate = " update thu_thucan set mathucantrua = '" + txtMaThucAn.Text + "'  where mathucantrua = '" + maThucAn + "'";
                                ThucAnController.themThucAn(sqlupdate);
                            }
                            if (reader[2].ToString().Equals(maThucAn))
                            {
                                string sqlupdate = " update thu_thucan set mathucantoi = '" + txtMaThucAn.Text + "'  where mathucantoi = '" + maThucAn + "'";
                                dtBase.CapNhatDuLieu(sqlupdate);
                            }
                            listMaThucAn.Add(reader[0].ToString());
                            load();
                        }
                    }
                    MessageBox.Show("bạn đã sửa thành công");
                    dtgvThucAn.DataSource = dtBase.DocDL("select *  from thucan");
                }
            }
        }

        private void BtThemChuong_Click(object sender, EventArgs e)
        {
            if (txtMaChuong.Text.Length == 0)
            {
                MessageBox.Show("bạn chưa nhập mã chuồng");
            }
            else if (cbbTenLoai.Text.Length == 0)
            {
                MessageBox.Show("bạn chưa chọn tên loài");
            }
            else if (txtMaKhuChuong.Text.Length == 0)
            {
                MessageBox.Show("bạn chưa nhập mã khu");
            }
            else if (txtDienTichChuong.Text.Length == 0)
            {
                MessageBox.Show("bạn chưa nhập diện tích chuồng");
            }
            else if (txtChieuCaoChuong.Text.Length == 0)
            {
                MessageBox.Show("bạn chưa nhập chiều cao chuồng");
            }
            else if (txtSoLuongThuChuong.Text.Length == 0)
            {
                MessageBox.Show("bạn chưa nhập số lượng thú");
            }
            else if (cbbTrangthai.Text.Length == 0)
            {
                MessageBox.Show("bạn chưa chọn trạng thái");
            }
            else if (cbbNhanVienTrongCoi.Text.Length == 0)
            {
                MessageBox.Show("bạn chưa chọn nhân viên");
            }
            else
            {
                /*listMaChuong.Clear();
                int check = 0;
                SqlDataReader reader = dtBase.command("select machuong from chuong ").ExecuteReader();
                if (reader.HasRows)
                {
                    // Đọc kết quả
                    while (reader.Read())
                    {
                        listMaChuong.Add(reader[0].ToString());
                    }
                }
                for (int i = 0; i < listMaChuong.Count; i++)
                {
                    if (txtMaChuong.Text.Equals(listMaChuong[i]))
                    {
                        check = 0;
                        break;
                    }
                    else check = 1;
                }
                if (check == 0) MessageBox.Show("mã chuồng đã có");
                else if (check == 1)
                {
                    string sql = " insert into chuong values(N'" + txtMaChuong.Text + "',N'" + cbbTenLoai.Text + "',N'" + txtMaKhuChuong.Text + "','" + Convert.ToDouble(txtDienTichChuong.Text)
                        + "','" + Convert.ToDouble(txtChieuCaoChuong.Text) + "',N'" + txtSoLuongThuChuong.Text
                        + "',N'" + TrangThaiController.getMaTrangThai(cbbTrangthai.Text) + "',N'" + nhanVienController.getMaNhanVien(cbbNhanVienTrongCoi.Text)
                        + "',N'" + txtGhiChu.Text + "' )";
                    dtBase.CapNhatDuLieu(sql);
                    MessageBox.Show("bạn đã thêm thành công");
                    dtgvChuong.DataSource = dtBase.DocDL("select *  from chuong");
                }*/
                int maxMaChuong = 0;
                List<int> LSTmachuongs = new List<int>();
                SqlDataReader readerMaChuong = dtBase.command("select machuong from chuong").ExecuteReader();
                if (readerMaChuong.HasRows)
                {
                    // Đọc kết quả
                    while (readerMaChuong.Read())
                    {
                        LSTmachuongs.Add(Convert.ToInt32(readerMaChuong[0].ToString().Substring(1)));
                    }
                }
                if (LSTmachuongs.Count != 0)
                {
                    maxMaChuong = LSTmachuongs[0];
                    for (int i = 1; i < LSTmachuongs.Count; i++)
                    {
                        if (maxMaChuong < LSTmachuongs[i]) maxMaChuong = LSTmachuongs[i];
                    }
                }
                maxMaChuong += 1;
                string sql = " insert into chuong values(N'C" + maxMaChuong + "',N'" + cbbTenLoai.Text + "',N'" + txtMaKhuChuong.Text + "','" + Convert.ToDouble(txtDienTichChuong.Text)
                       + "','" + Convert.ToDouble(txtChieuCaoChuong.Text) + "',N'" + txtSoLuongThuChuong.Text
                       + "',N'" + TrangThaiController.getMaTrangThai(cbbTrangthai.Text) + "',N'" + nhanVienController.getMaNhanVien(cbbNhanVienTrongCoi.Text)
                       + "',N'" + txtGhiChu.Text + "' )";
                dtBase.CapNhatDuLieu(sql);
                MessageBox.Show("bạn đã thêm thành công");
                dtgvChuong.DataSource = dtBase.DocDL("select *  from chuong");
            }

            load();
        }

        private void BtnXoaChuong_Click(object sender, EventArgs e)
        {
            if (txtMaChuong.Text.Length == 0)
            {
                MessageBox.Show("bạn chưa nhập mã chuồng");
            }
            else
            {
                listMaChuong.Clear();
                int check = 0;
                SqlDataReader reader = dtBase.command("select machuong from thu_chuong ").ExecuteReader();
                if (reader.HasRows)
                {
                    // Đọc kết quả
                    while (reader.Read())
                    {
                        listMaChuong.Add(reader[0].ToString());
                    }
                }
                for (int i = 0; i < listMaChuong.Count; i++)
                {
                    if (txtMaChuong.Text.Equals(listMaChuong[i]))
                    {
                        check = 0;
                        break;
                    }
                    else check = 1;
                }
                if (check == 0) MessageBox.Show("Chuồng đang có thú không thể xóa");
                else if (check == 1)
                {
                    string sql = " delete from chuong where machuong = '" + txtMaChuong.Text + "'";
                    dtBase.CapNhatDuLieu(sql);
                    MessageBox.Show("bạn đã xóa thành công");
                    dtgvChuong.DataSource = dtBase.DocDL("select * from chuong");
                }
            }
            load();
        }

        private void BtnSuaChuong_Click(object sender, EventArgs e)
        {
            if (txtMaChuong.Text.Length == 0)
            {
                MessageBox.Show("bạn chưa nhập mã chuồng");
            }
            else if (cbbTenLoai.Text.Length == 0)
            {
                MessageBox.Show("bạn chưa chọn tên loài");
            }
            else if (txtMaKhuChuong.Text.Length == 0)
            {
                MessageBox.Show("bạn chưa nhập mã khu");
            }
            else if (txtDienTichChuong.Text.Length == 0)
            {
                MessageBox.Show("bạn chưa nhập diện tích chuồng");
            }
            else if (txtChieuCaoChuong.Text.Length == 0)
            {
                MessageBox.Show("bạn chưa nhập chiều cao chuồng");
            }
            else if (txtSoLuongThuChuong.Text.Length == 0)
            {
                MessageBox.Show("bạn chưa nhập số lượng thú");
            }
            else if (cbbTrangthai.Text.Length == 0)
            {
                MessageBox.Show("bạn chưa chọn trạng thái");
            }
            else if (cbbNhanVienTrongCoi.Text.Length == 0)
            {
                MessageBox.Show("bạn chưa chọn nhân viên");
            }
            else
            {
                /*listMaChuong.Clear();
                int check = 0;
                SqlDataReader reader = dtBase.command("select machuong from chuong where machuong !='" + txtMaChuong.Text + "' ").ExecuteReader();
                if (reader.HasRows)
                {
                    // Đọc kết quả
                    while (reader.Read())
                    {
                        listMaChuong.Add(reader[0].ToString());
                    }
                }
                for (int i = 0; i < listMaChuong.Count; i++)
                {
                    if (txtMaChuong.Text.Equals(listMaChuong[i]))
                    {
                        check = 0;
                        break;
                    }
                    else check = 1;
                }
                if (check == 0) MessageBox.Show("mã chuồng đã có");
                else if (check == 1)
                {
                    string sql = " update chuong set machuong =N'" + txtMaChuong.Text + "',maloai = N'" + cbbTenLoai.Text + "'"
                        + ",makhu = N'" + txtMaKhuChuong.Text + "',dientich = '" + Convert.ToDouble(txtDienTichChuong.Text)
                        + "',chieucao = '" + Convert.ToDouble(txtChieuCaoChuong.Text) + "',soluongthu = '" + txtSoLuongThuChuong.Text
                        + "',matrangthai = '" + TrangThaiController.getMaTrangThai(cbbTrangthai.Text) + "',nhanvientrongcoi = '" + nhanVienController.getMaNhanVien(cbbNhanVienTrongCoi.Text)
                        + "',ghichu = N'" + txtGhiChu.Text + "' where machuong = '" + maChuong + "'";
                    dtBase.CapNhatDuLieu(sql);
                    string sql1 = "update thu_chuong set machuong = N'" + txtMaChuong.Text + "' where machuong = '" + maChuong + "'";
                    dtBase.CapNhatDuLieu(sql1);
                    MessageBox.Show("bạn đã sửa thành công");
                    dtgvChuong.DataSource = dtBase.DocDL("select *  from chuong");
                }*/
                string sql = " update chuong set maloai = N'" + cbbTenLoai.Text + "'"
                        + ",makhu = N'" + txtMaKhuChuong.Text + "',dientich = '" + Convert.ToDouble(txtDienTichChuong.Text)
                        + "',chieucao = '" + Convert.ToDouble(txtChieuCaoChuong.Text) + "',soluongthu = '" + txtSoLuongThuChuong.Text
                        + "',matrangthai = '" + TrangThaiController.getMaTrangThai(cbbTrangthai.Text) + "',nhanvientrongcoi = '" + nhanVienController.getMaNhanVien(cbbNhanVienTrongCoi.Text)
                        + "',ghichu = N'" + txtGhiChu.Text + "' where machuong = '" + maChuong + "'";
                dtBase.CapNhatDuLieu(sql);
                MessageBox.Show("bạn đã sửa thành công");
                dtgvChuong.DataSource = dtBase.DocDL("select *  from chuong");
            }
            load();
        }

        private void BtnThemNV_Click(object sender, EventArgs e)
        {

            if (txtMaNhanVien.Text.Length == 0)
            {
                MessageBox.Show("b chưa nhập mã nhân viên");
                txtMaNhanVien.Focus();
            }
            else if (txtTenNhanVien.Text.Length == 0)
            {
                MessageBox.Show("b chưa nhập tên nhân viên");
                txtTenNhanVien.Focus();
            }
            else if (txtDiaChi.Text.Length == 0)
            {
                MessageBox.Show("b chưa nhập địa chỉ");
                txtDiaChi.Focus();
            }
            else if (txtGioiTinh.Text.Length == 0)
            {
                MessageBox.Show("b chưa nhập giới tính");
                txtGioiTinh.Focus();
            }
            else if (txtDienThoai.Text.Length == 0)
            {
                MessageBox.Show("b chưa nhập số điện thoại");
                txtDienThoai.Focus();
            }
            else
            {
                ListmaNhanVien.Clear();
                int check = 0;
                SqlDataReader reader = dtBase.command("select manhanvien from nhanvien  ").ExecuteReader();
                if (reader.HasRows)
                {
                    // Đọc kết quả
                    while (reader.Read())
                    {
                        ListmaNhanVien.Add(reader[0].ToString());
                    }
                }
                for (int i = 0; i < ListmaNhanVien.Count; i++)
                {
                    if (txtMaNhanVien.Text.Equals(ListmaNhanVien[i]))
                    {
                        check = 0;
                        break;
                    }
                    else check = 1;
                }
                if (check == 0) MessageBox.Show("mã nhân viên đã tồn tại");
                else if (check == 1)
                {
                    if (fileDialog.FileName.Length == 0)
                    {
                        dtBase.CapNhatDuLieu("insert into nhanvien values(N'" + txtMaNhanVien.Text + "',N'" + txtTenNhanVien.Text + "','" + Convert.ToDateTime(dtpNgaySinhNV.Text) + "',N'" + txtGioiTinh.Text + "',N'" + txtDiaChi.Text + "',N'" + txtDienThoai.Text + "','')");
                    }
                    else dtBase.CapNhatDuLieu("insert into nhanvien values(N'" + txtMaNhanVien.Text + "',N'" + txtTenNhanVien.Text + "','" + Convert.ToDateTime(dtpNgaySinhNV.Text) + "',N'" + txtGioiTinh.Text + "',N'" + txtDiaChi.Text + "',N'" + txtDienThoai.Text + "','" + ImageToBase64(fileDialog.FileName) + "')");
                }
                dtgvNhanVien.DataSource = dtBase.DocDL("select manhanvien, tennhanvien, diachi, gioitinh, ngaysinh, sodienthoai, anhnhanvien from nhanvien");
                pbAnhNhanVien.Image = null;
            }
        }

        private void DtgvNhanVien_Click(object sender, EventArgs e)
        {
            maNhanvien = dtgvNhanVien.CurrentRow.Cells[0].Value.ToString();
            txtMaNhanVien.Text = dtgvNhanVien.CurrentRow.Cells[0].Value.ToString();
            txtTenNhanVien.Text = dtgvNhanVien.CurrentRow.Cells[1].Value.ToString();
            txtDiaChi.Text = dtgvNhanVien.CurrentRow.Cells[2].Value.ToString();
            txtGioiTinh.Text = dtgvNhanVien.CurrentRow.Cells[3].Value.ToString();
            dtpNgaySinhNV.Text = dtgvNhanVien.CurrentRow.Cells[4].Value.ToString();
            txtDienThoai.Text = dtgvNhanVien.CurrentRow.Cells[5].Value.ToString();
            pbAnhNhanVien.Image = Base64ToImage(dtgvNhanVien.CurrentRow.Cells[6].Value.ToString());
        }

        private void PbAnhNhanVien_Click(object sender, EventArgs e)
        {
            fileDialog.Filter = "Image Files(*.jpg; *.gif; *.png;) | *.jpg; *.gif; *.png;";
            if (fileDialog.ShowDialog() == DialogResult.OK)
            {
                pbAnhNhanVien.Image = new Bitmap(fileDialog.FileName);

            }
        }

        private void BtnSuaNV_Click(object sender, EventArgs e)
        {
            if (txtMaNhanVien.Text.Length == 0)
            {
                MessageBox.Show("b chưa nhập mã nhân viên");
                txtMaNhanVien.Focus();
            }
            else if (txtTenNhanVien.Text.Length == 0)
            {
                MessageBox.Show("b chưa nhập tên nhân viên");
                txtTenNhanVien.Focus();
            }
            else if (txtDiaChi.Text.Length == 0)
            {
                MessageBox.Show("b chưa nhập địa chỉ");
                txtDiaChi.Focus();
            }
            else if (txtGioiTinh.Text.Length == 0)
            {
                MessageBox.Show("b chưa nhập giới tính");
                txtGioiTinh.Focus();
            }
            else if (txtDienThoai.Text.Length == 0)
            {
                MessageBox.Show("b chưa nhập số điện thoại");
                txtDienThoai.Focus();
            }
            else
            {
                ListmaNhanVien.Clear();
                int check = 0;
                SqlDataReader reader = dtBase.command("select manhanvien from nhanvien where manhanvien != '" + maNhanvien + "' ").ExecuteReader();
                if (reader.HasRows)
                {
                    // Đọc kết quả
                    while (reader.Read())
                    {
                        ListmaNhanVien.Add(reader[0].ToString());
                    }
                }
                for (int i = 0; i < ListmaNhanVien.Count; i++)
                {
                    if (txtMaNhanVien.Text.Equals(ListmaNhanVien[i]))
                    {
                        check = 0;
                        break;
                    }
                    else check = 1;
                }
                if (check == 0) MessageBox.Show("mã nhân viên đã tồn tại");
                else if (check == 1)
                {
                    if (fileDialog.FileName.Length == 0)
                    {
                        dtBase.CapNhatDuLieu("update  nhanvien set manhanvien = N'" + txtMaNhanVien.Text + "',tennhanvien = N'" + txtTenNhanVien.Text + "',ngaysinh = '" + Convert.ToDateTime(dtpNgaySinhNV.Text) + "',gioitinh =N'" + txtGioiTinh.Text + "',diachi = N'" + txtDiaChi.Text + "',sodienthoai = '" + txtDienThoai.Text + "' where manhanvien = '" + maNhanvien + "'");
                    }
                    else
                    {
                        dtBase.CapNhatDuLieu("update  nhanvien set manhanvien = N'" + txtMaNhanVien.Text + "',tennhanvien =N '" + txtTenNhanVien.Text + "',ngaysinh = '" + Convert.ToDateTime(dtpNgaySinhNV.Text) + "',gioitinh =N'" + txtGioiTinh.Text + "',diachi = N'" + txtDiaChi.Text + "',sodienthoai = '" + txtDienThoai.Text + "',anhnhanvien = '" + ImageToBase64(fileDialog.FileName) + "' where manhanvien = '" + maNhanvien + "'");
                    }
                    reader = dtBase.command("select nhanvientrongcoi from chuong").ExecuteReader();
                    if (reader.HasRows)
                    {
                        // Đọc kết quả
                        while (reader.Read())
                        {
                            if (reader[0].ToString().Equals(maNhanvien))
                            {
                                string sqlupdate = " update chuong set nhanvientrongcoi = '" + txtMaNhanVien.Text + "'  where nhanvientrongcoi = '" + maNhanvien + "'";
                                dtBase.CapNhatDuLieu(sqlupdate);
                            }
                        }
                    }
                    pbAnhNhanVien.Image = null;
                    dtgvNhanVien.DataSource = dtBase.DocDL("select manhanvien,tennhanvien,diachi,gioitinh,ngaysinh,sodienthoai,anhnhanvien from nhanvien");
                }
            }
        }

        private void DtgvNhanVien_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void BtnXoaNV_Click(object sender, EventArgs e)
        {
            if (txtMaNhanVien.Text.Length == 0)
            {
                MessageBox.Show("b chưa nhập mã nhân viên");
                txtMaNhanVien.Focus();
            }
            else
            {
                int check = 0;
                List<string> nhanvientrongcoi = new List<string>();
                SqlDataReader reader = dtBase.command("select nhanvientrongcoi from chuong").ExecuteReader();
                if (reader.HasRows)
                {
                    // Đọc kết quả
                    while (reader.Read())
                    {
                        nhanvientrongcoi.Add(reader[0].ToString());
                    }
                }
                for (int i = 0; i < nhanvientrongcoi.Count; i++)
                {
                    if (txtMaNhanVien.Text.Equals(nhanvientrongcoi[i]))
                    {
                        check = 0;
                        break;
                    }
                    else check = 1;
                }
                if (check == 0) MessageBox.Show("nhân viên đang trông coi, không thể xóa");
                else if (check == 1)
                {
                    string sql = " delete from nhanvien where manhanvien ='" + txtMaNhanVien.Text + "'";
                    dtBase.CapNhatDuLieu(sql);
                    MessageBox.Show("b đã xóa thành công nhân viên");
                    dtgvNhanVien.DataSource = dtBase.DocDL("select manhanvien,tennhanvien,diachi,gioitinh,ngaysinh,sodienthoai,anhnhanvien from nhanvien");
                }
            }
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            DataTable dtTableThu = dtBase.DocDL("select   thu.MaThu,   tenThu,   thu.maLoai,   chuong.maChuong,soLuong,   sachDo,   thu.TenKhoaHoc,      tenTA,   tenTV,   kieuSinh, gioiTinh, thu.NgayVao, nguonGoc,dacDiem, ngaySinh,  tuoiTho,MaThucAnSang,SLThucAnSang,MaThucAnTrua,SLThucAnTrua,MaThucAnToi,SlThucAnToi ,  Anh from thu, chuong, Thu_Chuong,Thu_ThucAn  where Thu_Chuong.MaChuong = Chuong.MaChuong " +
                "and Thu.MaThu = Thu_Chuong.MaThu and Thu_ThucAn.MaThu = Thu.MaThu order by maloai ASC");
            dtgvThu.DataSource = dtTableThu;
            dtgvThu.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
        }

        private void BtnRefreshChuong_Click(object sender, EventArgs e)
        {
            DataTable dtTableChuong = dtBase.DocDL("select * from chuong order by maloai ASC");
            dtgvChuong.DataSource = dtTableChuong;
            dtgvChuong.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
        }
        private void resetChuong()
        {
            txtMaChuong.Text = "";
            cbbTenLoai.Text = "";
            txtMaKhuChuong.Text = "";
            txtChieuCaoChuong.Text = "";
            txtDienTichChuong.Text = "";
            txtSoLuongThuChuong.Text = "";
            cbbTrangthai.Text = "";
            cbbNhanVienTrongCoi.Text = "";
            txtGhiChu.Text = "";
        }
        private void BtnClearChuong_Click(object sender, EventArgs e)
        {
            resetChuong();
        }

        private void BtnTimKiemChuong_Click(object sender, EventArgs e)
        {
            string sql = "select chuong.MaChuong,chuong.MaLoai,MaKhu,DienTich,ChieuCao,SoLuongThu,TenTrangThai,TenNhanVien,chuong.GhiChu from chuong, trangthai,nhanvien,thu,thu_chuong  where chuong.NhanVienTrongCoi = nhanvien.MaNhanVien " +
               "and chuong.matrangthai = trangthai.matrangthai   and (nhanvien.tennhanvien  like '%" + txtTimKiemChuong.Text + "%' or thu.soluong like '%" + txtTimKiemChuong.Text + "%' or ( chuong.machuong = thu_chuong.machuong and thu_chuong.mathu= thu.mathu and thu.mathu like '%" + txtTimKiemChuong.Text + "%') or ( chuong.machuong = thu_chuong.machuong and thu_chuong.mathu= thu.mathu and thu.tenthu like '%" + txtTimKiemChuong.Text + "%') or chuong.machuong like '%" + txtTimKiemChuong.Text + "%' ) "
               + "group by chuong.machuong,chuong.maloai,chuong.MaKhu,chuong.DienTich,chuong.ChieuCao,chuong.SoLuongThu,trangthai.TenTrangThai,nhanvien.TenNhanVien,chuong.GhiChu";
            Golobal.GolobalThu.giatritimkiem = txtTimKiemChuong.Text;
            Golobal.GolobalThu.kquaTimKiemChuong = dtBase.DocDL(sql);
            frmTimKiemChuong frmTimKiemChuong = new frmTimKiemChuong();
            frmTimKiemChuong.ShowDialog();
        }
    }
}