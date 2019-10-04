using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using QuanLyVuonThu.Model; 

namespace QuanLyVuonThu.Controller
{
    class TrangThaiController
    {
        ProcessDataBase dtBase = new ProcessDataBase();
        public List<ModelTrangThai> listTrangThai()
        {
            List<ModelTrangThai> trangThais = new List<ModelTrangThai>();
            SqlDataReader reader = dtBase.command("select matrangthai,tentrangthai,ghichu from trangthai").ExecuteReader();
            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    trangThais.Add(new ModelTrangThai(reader[0].ToString(), reader[1].ToString(), reader[2].ToString()));
                }
            }
            return trangThais;

        }
        public string getMaTrangThai(string tenTrangThai)
        {
            string maTrangThai = "";
            List<ModelTrangThai> ListTrangThai = new List<ModelTrangThai>();
            ListTrangThai = listTrangThai();
            for (int i = 0; i < ListTrangThai.Count; i++)
            {
                if (tenTrangThai.Equals(ListTrangThai[i].TenTrangThai))
                {
                    maTrangThai = ListTrangThai[i].MaTrangThai;
                }
            }
            return maTrangThai;
        }
        public string getTenTrangThai(string maTrangThai)
        {
            string tenTrangThai = "";
            List<ModelTrangThai> ListTrangthai = new List<ModelTrangThai>();
            ListTrangthai = listTrangThai();
            for (int i = 0; i < ListTrangthai.Count; i++)
            {
                if (maTrangThai.Equals(ListTrangthai[i].MaTrangThai))
                {
                    tenTrangThai = ListTrangthai[i].TenTrangThai;
                }
            }
            return tenTrangThai;
        }
    }
}
