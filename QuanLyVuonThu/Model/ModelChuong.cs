using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyVuonThu.Model
{
    class ModelChuong
    {
        string maChuong;
        string maLoai;
        string maKhu;
        double dienTich;
        double chieuCao;
        int soLuongThu;
        string maTrangThai;
        string nhanVien;
        string ghiChu;

        public string MaChuong { get => maChuong; set => maChuong = value; }
        public string MaLoai { get => maLoai; set => maLoai = value; }
        public string MaKhu { get => maKhu; set => maKhu = value; }
        public double DienTich { get => dienTich; set => dienTich = value; }
        public double ChieuCao { get => chieuCao; set => chieuCao = value; }
        public int SoLuongThu { get => soLuongThu; set => soLuongThu = value; }
        public string MaTrangThai { get => maTrangThai; set => maTrangThai = value; }
        public string NhanVien { get => nhanVien; set => nhanVien = value; }
        public string GhiChu { get => ghiChu; set => ghiChu = value; }

        public ModelChuong()
        {
        }

        public ModelChuong(string maChuong, string maLoai, string maKhu, double dienTich, double chieuCao, int soLuongThu, string maTrangThai, string nhanVien)
        {
            this.maChuong = maChuong;
            this.maLoai = maLoai;
            this.maKhu = maKhu;
            this.dienTich = dienTich;
            this.chieuCao = chieuCao;
            this.soLuongThu = soLuongThu;
            this.maTrangThai = maTrangThai;
            this.nhanVien = nhanVien;
        }

        public ModelChuong(string maChuong, string maLoai, string maKhu, double dienTich, double chieuCao, int soLuongThu, string maTrangThai, string nhanVien, string ghiChu) : this(maChuong, maLoai, maKhu, dienTich, chieuCao, soLuongThu, maTrangThai, nhanVien)
        {
            this.ghiChu = ghiChu;
        }
    }
}
