using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyVuonThu.Model
{
    class ModelThucAn
    {
        string maThucAn;
        string tenThucAn;
        string congDung;
        string maDonVi;

        public ModelThucAn()
        {
        }

        public ModelThucAn(string maThucAn, string tenThucAn, string congDung, string maDonVi)
        {
            this.MaThucAn = maThucAn;
            this.TenThucAn = tenThucAn;
            this.CongDung = congDung;
            this.MaDonVi = maDonVi;
        }

        public ModelThucAn(string maThucAn, string tenThucAn)
        {
            this.maThucAn = maThucAn;
            this.tenThucAn = tenThucAn;
        }

        public string MaThucAn { get => maThucAn; set => maThucAn = value; }
        public string TenThucAn { get => tenThucAn; set => tenThucAn = value; }
        public string CongDung { get => congDung; set => congDung = value; }
        public string MaDonVi { get => maDonVi; set => maDonVi = value; }
    }
}
