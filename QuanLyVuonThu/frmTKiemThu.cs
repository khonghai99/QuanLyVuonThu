using QuanLyVuonThu.Controller;
using QuanLyVuonThu.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QuanLyVuonThu
{
    public partial class frmTKiemThu : Form
    {
        int click;
        ChuongController ChuongController = new ChuongController();
        ThucAnController ThucAnController = new ThucAnController();
        nhanVienController nhanVienController = new nhanVienController();
        TrangThaiController TrangThaiController = new TrangThaiController();
        thuController thuController = new thuController();
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

        ProcessDataBase dtBase = new ProcessDataBase();

        public bool IsDate(string str)
        {
            Regex regex = new Regex(@"^(0?[1-9]|1[012])[\/\-](0?[1-9]|[12][0-9]|3[01])[\/\-]\d{4}$");
            return regex.IsMatch(str);
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
        public frmTKiemThu()
        {
            InitializeComponent();
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
                        dtgvThu1 = dtBase.DocDL("Select * from Thu where" + " MaThu = '" + (txtMaThu.Text).Trim() + "'");
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
                        }
                    }
                }
                return false;
            }
        }
        private void BtnSuaThu_Click(object sender, EventArgs e)
        {

            click = 1;
            if (check() == false) return;
            else
            {

                if (openFileDialog.FileName.Length == 0)
                {
                    dtBase.CapNhatDuLieu("update thu  set mathu =  N'" + txtMaThu.Text + "',tenthu =  N'" + txtTenThu.Text + "'," +
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
                    dtBase.CapNhatDuLieu("update thu  set mathu =  N'" + txtMaThu.Text + "',tenthu =  N'"
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
                dtBase.CapNhatDuLieu("update thu_chuong  set machuong =  '" + cbbMaChuong.Text + "'," +
                    "mathu =  N'" + txtMaThu.Text + "',ngayvao =  '" + Convert.ToDateTime(dtpNgayVaoThu.Text)
                    + "',lydovao =  N'" + txtLyDoVao.Text + "' where machuong = '" + cbbMaChuong.Text + "' and mathu = '" + txtMaThu.Text + "'");

                dtBase.CapNhatDuLieu("update thu_thucan  set mathu =  '" + txtMaThu.Text + "'," +
                    "mathucansang =  N'" + getMaThucAn(cbbThucAnSang.Text) + "',SLThucAnSang =  '" + Convert.ToInt32(cbbSLThucAnSang.Text)
                    + "',mathucantrua =  N'" + getMaThucAn(cbbThucAnTrua.Text) + "',SLThucAnTrua =  '" + Convert.ToInt32(cbbSLThucAnTrua.Text)
                    + "',mathucantoi =  N'" + getMaThucAn(cbbThucAntoi.Text) + "',SLThucAnToi =  '" + Convert.ToInt32(cbbSLThucAnToi.Text) + "' where  mathu = '" + maThu + "'");
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


                MessageBox.Show("Bạn đã sửa thành công");
                dtgvTimKiemThu.DataSource = dtBase.DocDL(" select   thu.MaThu,   tenThu,   thu.maLoai,   chuong.maChuong,soLuong,   sachDo,   thu.TenKhoaHoc,      tenTA,   tenTV,   kieuSinh, gioiTinh, thu.NgayVao, nguonGoc,dacDiem, ngaySinh,  tuoiTho,MaThucAnSang,SLThucAnSang,MaThucAnTrua,SLThucAnTrua,MaThucAnToi,SlThucAnToi ,  Anh from thu, chuong, Thu_Chuong,Thu_ThucAn  where Thu_Chuong.MaChuong = Chuong.MaChuong " +
                "and Thu.MaThu = Thu_Chuong.MaThu and Thu_ThucAn.MaThu = Thu.MaThu and (thu.mathu  like '%" + Golobal.GolobalThu.giatritimkiem + "%' or thu.tenthu like '%" + Golobal.GolobalThu.giatritimkiem + "%' )");
                ResetValue();
                btnSuaThu.Enabled = false;
            }


        }
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
            dtgvTimKiemThu.DataSource = Golobal.GolobalThu.kquaTimKiemThu;
            dtgvTimKiemThu.Columns[0].HeaderText = "Mã thú";
            dtgvTimKiemThu.Columns[1].HeaderText = "Tên thú";
            dtgvTimKiemThu.Columns[2].HeaderText = "Mã loài";
            dtgvTimKiemThu.Columns[3].HeaderText = "Mã chuồng";
            dtgvTimKiemThu.Columns[4].HeaderText = "Số lượng";
            dtgvTimKiemThu.Columns[5].HeaderText = "Sách đỏ";
            dtgvTimKiemThu.Columns[6].HeaderText = "Tên KH";
            dtgvTimKiemThu.Columns[7].HeaderText = "Tên TA";
            dtgvTimKiemThu.Columns[8].HeaderText = "Tên TV";
            dtgvTimKiemThu.Columns[9].HeaderText = "Kiểu sinh";
            dtgvTimKiemThu.Columns[10].HeaderText = "Giới tính";
            dtgvTimKiemThu.Columns[11].HeaderText = "Ngày vào";
            dtgvTimKiemThu.Columns[12].HeaderText = "Nguồn gốc";
            dtgvTimKiemThu.Columns[13].HeaderText = "Đặc điểm";
            dtgvTimKiemThu.Columns[14].HeaderText = "Ngày sinh";
            dtgvTimKiemThu.Columns[22].HeaderText = "Ảnh";
            dtgvTimKiemThu.Columns[15].HeaderText = "Tuổi thọ";
            dtgvTimKiemThu.Columns[16].HeaderText = "Mã TA sáng";
            dtgvTimKiemThu.Columns[17].HeaderText = "Số Lượng";
            dtgvTimKiemThu.Columns[18].HeaderText = "Mã TA Trưa";
            dtgvTimKiemThu.Columns[19].HeaderText = "Số lượng";
            dtgvTimKiemThu.Columns[20].HeaderText = "Mã TA tối";
            dtgvTimKiemThu.Columns[21].HeaderText = "Số lượng";

            dtgvTimKiemThu.BackgroundColor = Color.LightBlue;
        }
        private void FrmTKiemThu_Load(object sender, EventArgs e)
        {

            load();
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

        public Image Base64ToImage(string base64String)
        {
            byte[] imageBytes = Convert.FromBase64String(base64String);
            MemoryStream ms = new MemoryStream(imageBytes, 0, imageBytes.Length);
            ms.Write(imageBytes, 0, imageBytes.Length);
            System.Drawing.Image image = System.Drawing.Image.FromStream(ms, true);
            return image;
        }

        private void DtgvTimKiemThu_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dtgvTimKiemThu.CurrentRow.Cells[4].Value.ToString().Length != 0)
                soLuongThuPrevious = Convert.ToInt32(dtgvTimKiemThu.CurrentRow.Cells[4].Value.ToString());
            maThu = dtgvTimKiemThu.CurrentRow.Cells[0].Value.ToString();
            btnSuaThu.Enabled = true;
            txtMaThu.Text = dtgvTimKiemThu.CurrentRow.Cells[0].Value.ToString();
            cbbMaChuong.Text = dtgvTimKiemThu.CurrentRow.Cells[3].Value.ToString();
            txtTenThu.Text = dtgvTimKiemThu.CurrentRow.Cells[1].Value.ToString();
            cbbMaLoai.Text = dtgvTimKiemThu.CurrentRow.Cells[2].Value.ToString();
            cbbSoLuongThu.Text = dtgvTimKiemThu.CurrentRow.Cells[4].Value.ToString();
            cbbSachDoThu.Text = dtgvTimKiemThu.CurrentRow.Cells[5].Value.ToString();
            txtTenKHThu.Text = dtgvTimKiemThu.CurrentRow.Cells[6].Value.ToString();
            txtTenTA.Text = dtgvTimKiemThu.CurrentRow.Cells[7].Value.ToString();
            txtTenTV.Text = dtgvTimKiemThu.CurrentRow.Cells[8].Value.ToString();
            cbbKieuSinh.Text = dtgvTimKiemThu.CurrentRow.Cells[9].Value.ToString();
            cbbGioiTinhThu.Text = dtgvTimKiemThu.CurrentRow.Cells[10].Value.ToString();
            dtpNgayVaoThu.Text = dtgvTimKiemThu.CurrentRow.Cells[11].FormattedValue.ToString();
            txtNguonGoc.Text = dtgvTimKiemThu.CurrentRow.Cells[12].Value.ToString();
            txtDacDiemThu.Text = dtgvTimKiemThu.CurrentRow.Cells[13].Value.ToString();
            dtpNgaySinhThu.Text = dtgvTimKiemThu.CurrentRow.Cells[14].FormattedValue.ToString();
            txtTuoiTho.Text = dtgvTimKiemThu.CurrentRow.Cells[15].Value.ToString();
            cbbThucAnSang.Text = getTenThucAn(dtgvTimKiemThu.CurrentRow.Cells[16].Value.ToString());
            cbbSLThucAnSang.Text = dtgvTimKiemThu.CurrentRow.Cells[17].Value.ToString();
            cbbThucAnTrua.Text = getTenThucAn(dtgvTimKiemThu.CurrentRow.Cells[18].Value.ToString());
            cbbSLThucAnTrua.Text = dtgvTimKiemThu.CurrentRow.Cells[19].Value.ToString();
            cbbThucAntoi.Text = getTenThucAn(dtgvTimKiemThu.CurrentRow.Cells[20].Value.ToString());
            cbbSLThucAnToi.Text = dtgvTimKiemThu.CurrentRow.Cells[21].Value.ToString();
            if (dtgvTimKiemThu.CurrentRow.Cells[22].Value.ToString().Length == 0)
            {
                pbThu.Image = null;
            }
            else pbThu.Image = Base64ToImage(dtgvTimKiemThu.CurrentRow.Cells[22].Value.ToString());
            SqlDataReader reader = dtBase.command("select lydovao from thu_chuong where machuong = '" + dtgvTimKiemThu.CurrentRow.Cells[3].Value.ToString() + "' and mathu = '" + dtgvTimKiemThu.CurrentRow.Cells[0].Value.ToString() + "'").ExecuteReader();
            if (reader.HasRows)
            {
                // Đọc kết quả
                while (reader.Read())
                {
                    txtLyDoVao.Text = reader[0].ToString();
                }
            }
            for (int i = 0; i < dtgvTimKiemThu.Rows.Count - 1; i++)
            {
                modelThus.Add(new ModelThu(dtgvTimKiemThu.Rows[i].Cells[0].Value.ToString(), dtgvTimKiemThu.Rows[i].Cells[1].Value.ToString(), dtgvTimKiemThu.Rows[i].Cells[2].Value.ToString(), dtgvTimKiemThu.Rows[i].Cells[3].Value.ToString(), Convert.ToInt32(dtgvTimKiemThu.Rows[i].Cells[4].Value.ToString()), dtgvTimKiemThu.Rows[i].Cells[5].Value.ToString(), dtgvTimKiemThu.Rows[i].Cells[6].Value.ToString(), dtgvTimKiemThu.Rows[i].Cells[7].Value.ToString(), dtgvTimKiemThu.Rows[i].Cells[8].Value.ToString(), dtgvTimKiemThu.Rows[i].Cells[9].Value.ToString(), dtgvTimKiemThu.Rows[i].Cells[10].Value.ToString(), Convert.ToDateTime(dtgvTimKiemThu.Rows[i].Cells[11].Value.ToString()), dtgvTimKiemThu.Rows[i].Cells[12].Value.ToString(), dtgvTimKiemThu.Rows[i].Cells[13].Value.ToString(), Convert.ToDateTime(dtgvTimKiemThu.Rows[i].Cells[14].Value.ToString()), Convert.ToInt32(dtgvTimKiemThu.Rows[i].Cells[15].Value.ToString()), dtgvTimKiemThu.Rows[i].Cells[16].Value.ToString()));
            }
            SqlDataReader reader1 = dtBase.command("select maLoai from loai").ExecuteReader();
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
        }

        private void BtnXoaThu_Click(object sender, EventArgs e)
        {
            click = 1;
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
                    dtgvTimKiemThu.DataSource = dtBase.DocDL("select   thu.MaThu,   tenThu,   thu.maLoai,   chuong.maChuong,soLuong,   sachDo,   thu.TenKhoaHoc,      tenTA,   tenTV,   kieuSinh, gioiTinh, thu.NgayVao, nguonGoc,dacDiem, ngaySinh,  tuoiTho,MaThucAnSang,SLThucAnSang,MaThucAnTrua,SLThucAnTrua,MaThucAnToi,SlThucAnToi ,  Anh from thu, chuong, Thu_Chuong,Thu_ThucAn  where Thu_Chuong.MaChuong = Chuong.MaChuong " +
                "and Thu.MaThu = Thu_Chuong.MaThu and Thu_ThucAn.MaThu = Thu.MaThu order by maloai ASC");
                    ResetValue();
                }

            }
            load();
        }

        private void FrmTKiemThu_FormClosing(object sender, FormClosingEventArgs e)
        {

        }

        private void BtnTimKiemThu_Click(object sender, EventArgs e)
        {
            string sql = " select   thu.MaThu,   tenThu,   thu.maLoai,   chuong.maChuong,soLuong,   sachDo,   thu.TenKhoaHoc,      tenTA,   tenTV,   kieuSinh, gioiTinh, thu.NgayVao, nguonGoc,dacDiem, ngaySinh,  tuoiTho,MaThucAnSang,SLThucAnSang,MaThucAnTrua,SLThucAnTrua,MaThucAnToi,SlThucAnToi ,  Anh from loai,thu, chuong, Thu_Chuong,Thu_ThucAn  where Thu_Chuong.MaChuong = Chuong.MaChuong " +
               "and Thu.MaThu = Thu_Chuong.MaThu and Thu_ThucAn.MaThu = Thu.MaThu and loai.maloai = Thu.maloai and (thu.mathu  like '%" + txtTimKiemThu.Text + "%' or thu.tenthu like '%" + txtTimKiemThu.Text + "%' or loai.maloai like '%" + txtTimKiemThu.Text + "%' or thu.kieusinh like '%" + txtTimKiemThu.Text + "%' or thu.nguongoc like '%" + txtTimKiemThu.Text + "%' )";
            dtgvTimKiemThu.DataSource = dtBase.DocDL(sql);
            dtgvTimKiemThu.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
        }
    }
}
