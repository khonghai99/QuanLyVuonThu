using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;

namespace QuanLyVuonThu
{
    class ProcessDataBase
    {
        SqlConnection conn = null;
        //Hàm mở kết nối CSDL
        private void KetNoiCSDL()
        {
            conn = DBUtils.GetDBConnection();
            if (conn.State != ConnectionState.Open)
                conn.Open();
        }
        private void DongKetNoiCSDL()
        {
            if (conn.State != ConnectionState.Closed)
                conn.Close();
            conn.Dispose();
        }
        //Hàm thực thi câu lệnh dạng Select trả về một DataTable
        public DataTable DocInBang(string sql)
        {
            DataTable dtBang = new DataTable();
            KetNoiCSDL();
            SqlDataAdapter sqldataAdapte = new SqlDataAdapter(sql, conn);
            sqldataAdapte.Fill(dtBang);
            DongKetNoiCSDL();
            return dtBang;
        }
        //Hàm thực lệnh insert hoặc update hoặc delete
        public void CapNhatDuLieu(string sql)
        {
            KetNoiCSDL();
            SqlCommand sqlcommand = new SqlCommand();
            sqlcommand.Connection = conn;
            sqlcommand.CommandText = sql;
            sqlcommand.ExecuteNonQuery();
            DongKetNoiCSDL();
        }
    }
}
