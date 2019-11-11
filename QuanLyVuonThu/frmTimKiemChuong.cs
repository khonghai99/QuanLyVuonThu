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
    public partial class frmTimKiemChuong : Form
    {
        ProcessDataBase dtBase = new ProcessDataBase();
        public frmTimKiemChuong()
        {
            InitializeComponent();
        }

        private void FrmTimKiemChuong_Load(object sender, EventArgs e)
        {
            dtgvTimKiemChuong.DataSource = Golobal.GolobalThu.kquaTimKiemChuong;
            dtgvTimKiemChuong.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
        }

        private void BtnTimKiemChuong_Click(object sender, EventArgs e)
        {
            string sql = "select chuong.MaChuong,chuong.MaLoai,MaKhu,DienTich,ChieuCao,SoLuongThu,TenTrangThai,TenNhanVien,chuong.GhiChu from chuong, trangthai,nhanvien,thu,thu_chuong  where chuong.NhanVienTrongCoi = nhanvien.MaNhanVien " +
               "and chuong.matrangthai = trangthai.matrangthai   and (nhanvien.tennhanvien  like '%" + txtTimKiemChuong.Text + "%' or chuong.soluongthu like '%" + txtTimKiemChuong.Text + "%' or ( chuong.machuong = thu_chuong.machuong and thu_chuong.mathu= thu.mathu and thu.mathu like '%" + txtTimKiemChuong.Text + "%') or ( chuong.machuong = thu_chuong.machuong and thu_chuong.mathu= thu.mathu and thu.tenthu like '%" + txtTimKiemChuong.Text + "%') or chuong.machuong like '%" + txtTimKiemChuong.Text + "%' ) "
               + "group by chuong.machuong,chuong.maloai,chuong.MaKhu,chuong.DienTich,chuong.ChieuCao,chuong.SoLuongThu,trangthai.TenTrangThai,nhanvien.TenNhanVien,chuong.GhiChu";
            dtgvTimKiemChuong.DataSource = dtBase.DocDL(sql);
            dtgvTimKiemChuong.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
        }
    }
}
