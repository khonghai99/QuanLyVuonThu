using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyVuonThu.Model
{
    class ModelTrangThai
    {
        string maTrangThai;
        string tenTrangThai;
        string ghiChu;

        public string MaTrangThai { get => maTrangThai; set => maTrangThai = value; }
        public string TenTrangThai { get => tenTrangThai; set => tenTrangThai = value; }
        public string GhiChu { get => ghiChu; set => ghiChu = value; }

        public ModelTrangThai()
        {
        }

        public ModelTrangThai(string maTrangThai, string tenTrangThai, string ghiChu)
        {
            this.maTrangThai = maTrangThai;
            this.tenTrangThai = tenTrangThai;
            this.ghiChu = ghiChu;
        }
    }
}
