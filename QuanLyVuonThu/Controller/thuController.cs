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
    class thuController
    {
        ProcessDataBase dtBase = new ProcessDataBase();
        public DataTable TimKiemThu(string sql)
        {
            DataTable table = new DataTable();
            table = dtBase.DocDL(sql);
            return table;
        }
        
    }
}
