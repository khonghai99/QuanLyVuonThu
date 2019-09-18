using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;

namespace QuanLyVuonThu
{
    class DBSQLServerUtils
    {
        public static SqlConnection
        GetDBConnection(string datasource, string database, string username, string password)
        {
            //
            // Data Source=DESKTOP-0JQIJ6M;Initial Catalog=QuanLyVuonThu;Persist Security Info=True;User ID=sa;Password=123
            //
            string connString = @"Data Source=" + datasource + ";Initial Catalog="
                        + database + ";Persist Security Info=True;User ID=" + username + ";Password=" + password;
            SqlConnection conn = new SqlConnection(connString);


            return conn;
        }
    }
}
