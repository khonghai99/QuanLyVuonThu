using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyVuonThu.Controller
{
    class modelNhanVien
    {
        string maNhanVien;
        string tenNhanVien;
        string diaChi;
        string gioiTinh;
        DateTime ngaySinh;
        string soDienThoai;
        string anhNhanVien;

        public string MaNhanVien { get => maNhanVien; set => maNhanVien = value; }
        public string TenNhanVien { get => tenNhanVien; set => tenNhanVien = value; }
        public string DiaChi { get => diaChi; set => diaChi = value; }
        public string GioiTinh { get => gioiTinh; set => gioiTinh = value; }
        public DateTime NgaySinh { get => ngaySinh; set => ngaySinh = value; }
        public string SoDienThoai { get => soDienThoai; set => soDienThoai = value; }
        public string AnhNhanVien { get => anhNhanVien; set => anhNhanVien = value; }

        public modelNhanVien()
        {
        }

        public modelNhanVien(string maNhanVien, string tenNhanVien, string diaChi, string gioiTinh, DateTime ngaySinh, string soDienThoai, string anhNhanVien)
        {
            this.maNhanVien = maNhanVien;
            this.tenNhanVien = tenNhanVien;
            this.diaChi = diaChi;
            this.gioiTinh = gioiTinh;
            this.ngaySinh = ngaySinh;
            this.soDienThoai = soDienThoai;
            this.anhNhanVien = anhNhanVien;
        }
    }
}
