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
    class ThucAnController
    {

        ProcessDataBase dtBase = new ProcessDataBase();
        public List<ModelThucAn> ListThucAn()
        {
            List<ModelThucAn> thucAns = new List<ModelThucAn>();
            SqlDataReader reader = dtBase.command("select * from thucan").ExecuteReader();
            if (reader.HasRows)
            {
                // Đọc kết quả
                while (reader.Read())
                {
                    thucAns.Add(new ModelThucAn(reader[0].ToString(), reader[1].ToString(), reader[2].ToString(),reader[3].ToString()));
                }
            }
            return thucAns;
                
        }
        public DataTable ConvertListToDataTable(List<ModelThucAn> list)
        {
            // New table.
            DataTable table = new DataTable();

            // Get max columns.
            int columns = 0;
            foreach (var array in list)
            {
                if (4 > columns)
                {
                    columns = 4;
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
                table.Rows.Add(array.MaThucAn, array.TenThucAn, array.CongDung, array.MaDonVi);
            }

            return table;
        }
        public void themThucAn(string sql)
        {
            dtBase.CapNhatDuLieu(sql);
           
        }
        
    }
}
