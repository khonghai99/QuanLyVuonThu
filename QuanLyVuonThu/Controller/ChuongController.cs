using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using QuanLyVuonThu.Model;
using System.Data.SqlClient;
using System.Data;

namespace QuanLyVuonThu.Controller
{
    class ChuongController
    {
        ProcessDataBase dtBase = new ProcessDataBase();
        public List<ModelChuong> chuongs ()
        {
            List<ModelChuong> chuongs = new List<ModelChuong>();
            SqlDataReader reader = dtBase.command("select * from chuong  order by machuong ASC").ExecuteReader();
            if (reader.HasRows)
            {
                // Đọc kết quả
                while (reader.Read())
                {
                    chuongs.Add(new ModelChuong(reader[0].ToString(), reader[1].ToString(), reader[2].ToString(), Convert.ToDouble(reader[3].ToString()), Convert.ToDouble(reader[4].ToString()), Convert.ToInt32(reader[5].ToString()), reader[6].ToString(), reader[7].ToString()));
                }
            }
            return chuongs;
        }
        public DataTable ConvertListToDataTable(List<ModelChuong> list)
        {
            // New table.
            DataTable table = new DataTable();

            // Get max columns.
            int columns = 0;
            foreach (var array in list)
            {
                if (9 > columns)
                {
                    columns = 9;
                }
            }

            // Add columns.
            for (int i = 0; i < columns; i++)
            {
                table.Columns.Add();
            }

            // Add rows.
            foreach (var array in list)
            {
                table.Rows.Add(array.MaChuong,array.MaLoai,array.MaKhu,array.DienTich,array.ChieuCao,array.SoLuongThu,array.MaTrangThai, array.NhanVien,array.GhiChu);
            }

            return table;
        }
    }
}
