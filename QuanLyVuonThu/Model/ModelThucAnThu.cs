using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyVuonThu.Model
{
    class ModelThucAnThu
    {
        string maThucAn;
        string maThucAnSang;
        string tenThucAnSang;
        string maThucAnTrua;
        string tenThucAnTrua;
        string maThucAnToi;
        string tenThucAnToi;

        public string MaThucAn { get => maThucAn; set => maThucAn = value; }
        public string MaThucAnSang { get => maThucAnSang; set => maThucAnSang = value; }
        public string TenThucAnSang { get => tenThucAnSang; set => tenThucAnSang = value; }
        public string MaThucAnTrua { get => maThucAnTrua; set => maThucAnTrua = value; }
        public string TenThucAnTrua { get => tenThucAnTrua; set => tenThucAnTrua = value; }
        public string MaThucAnToi { get => maThucAnToi; set => maThucAnToi = value; }
        public string TenThucAnToi { get => tenThucAnToi; set => tenThucAnToi = value; }

        public ModelThucAnThu(string maThucAn, string maThucAnSang, string tenThucAnSang, string maThucAnTrua, string tenThucAnTrua, string maThucAnToi, string tenThucAnToi)
        {
            this.MaThucAn = maThucAn;
            this.MaThucAnSang = maThucAnSang;
            this.TenThucAnSang = tenThucAnSang;
            this.MaThucAnTrua = maThucAnTrua;
            this.TenThucAnTrua = tenThucAnTrua;
            this.MaThucAnToi = maThucAnToi;
            this.TenThucAnToi = tenThucAnToi;
        }

        public ModelThucAnThu()
        {
        }
    }
}
