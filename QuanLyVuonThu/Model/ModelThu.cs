using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyVuonThu.Model
{
    class ModelThu
    {
        string maThu;
        string tenThu;
        string maLoai;
        string maChuong;
        Int32 soLuong;
        string sachDo;
        string tenKH;
        string dacDiem;
        string tenTA;
        string tenTV;
        string kieuSinh;
        Int32 tuoiTho;
        string gioiTinh;
        DateTime ngayVao;
        string nguonGoc;
        DateTime ngaySinh;
        string anhThu;

        public string MaThu { get => maThu; set => maThu = value; }
        public string TenThu { get => tenThu; set => tenThu = value; }
        public string MaLoai { get => maLoai; set => maLoai = value; }
        public string MaChuong { get => maChuong; set => maChuong = value; }
        public int SoLuong { get => soLuong; set => soLuong = value; }
        public string SachDo { get => sachDo; set => sachDo = value; }
        public string TenKH { get => tenKH; set => tenKH = value; }
        public string DacDiem { get => dacDiem; set => dacDiem = value; }
        public string TenTA { get => tenTA; set => tenTA = value; }
        public string TenTV { get => tenTV; set => tenTV = value; }
        public string KieuSinh { get => kieuSinh; set => kieuSinh = value; }
        public Int32 TuoiTho { get => tuoiTho; set => tuoiTho = value; }
        public string GioiTinh { get => gioiTinh; set => gioiTinh = value; }
        public DateTime NgayVao { get => ngayVao; set => ngayVao = value; }
        public string NguonGoc { get => nguonGoc; set => nguonGoc = value; }
        public DateTime NgaySinh { get => ngaySinh; set => ngaySinh = value; }
        public string AnhThu { get => anhThu; set => anhThu = value; }

        public ModelThu(string maThu, string tenThu, string maLoai, string maChuong, Int32 soLuong, string sachDo, string tenKH, string tenTA, string tenTV, string kieuSinh,  string gioiTinh, DateTime ngayVao, string nguonGoc, string dacDiem, DateTime ngaySinh, Int32 tuoiTho, string anhThu)
        {
            MaThu = maThu;
            TenThu = tenThu;
            MaLoai = maLoai;
            MaChuong = maChuong;
            SoLuong = soLuong;
            SachDo = sachDo;
            TenKH = tenKH;
            DacDiem = dacDiem;
            TenTA = tenTA;
            TenTV = tenTV;
            KieuSinh = kieuSinh;
            TuoiTho = tuoiTho;
            GioiTinh = gioiTinh;
            NgayVao = ngayVao;
            NguonGoc = nguonGoc;
            NgaySinh = ngaySinh;
            AnhThu = anhThu;
           
        }

        public ModelThu()
        {
        }
    }
}
