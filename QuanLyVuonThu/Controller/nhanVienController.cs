using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;

namespace QuanLyVuonThu.Controller
{
    class nhanVienController
    {
        ProcessDataBase dtBase = new ProcessDataBase();
        public List<modelNhanVien> listNV()
        {
            List<modelNhanVien> nhanViens = new List<modelNhanVien>();
            SqlDataReader reader = dtBase.command("select manhanvien,tennhanvien,diachi,gioitinh,ngaysinh,sodienthoai,anhnhanvien from nhanvien").ExecuteReader();
            if(reader.HasRows)
            {
                while(reader.Read())
                {
                    nhanViens.Add(new modelNhanVien(reader[0].ToString(), reader[1].ToString(), reader[2].ToString(), reader[3].ToString(), Convert.ToDateTime(reader[4].ToString()), reader[5].ToString(), reader[6].ToString()));
                }
            }
            return nhanViens;

        }
        public string getMaNhanVien(string tenNhanVien)
        {
            string maNV = "";
            List<modelNhanVien> ListNhanVien = new List<modelNhanVien>();
            ListNhanVien = listNV();
            for (int i = 0; i < ListNhanVien.Count; i++)
            {
                if (tenNhanVien.Equals(ListNhanVien[i].TenNhanVien))
                {
                    maNV = ListNhanVien[i].MaNhanVien;
                }
            }
            return maNV;
        }
        public string getTenNhanVien(string maNhanVien)
        {
            string tenNV = "";
            List<modelNhanVien> ListNhanVien = new List<modelNhanVien>();
            ListNhanVien = listNV();
            for (int i = 0; i < ListNhanVien.Count; i++)
            {
                if (maNhanVien.Equals(ListNhanVien[i].MaNhanVien))
                {
                    tenNV = ListNhanVien[i].TenNhanVien;
                }
            }
            return tenNV;
        }
        public DataTable ConvertListToDataTable(List<modelNhanVien> list)
        {
            // New table.
            DataTable table = new DataTable();

            // Get max columns.
            int columns = 0;
            foreach (var array in list)
            {
                if (7 > columns)
                {
                    columns = 7;
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
                table.Rows.Add(array.MaNhanVien,array.TenNhanVien,array.DiaChi,array.GioiTinh,array.NgaySinh,array.SoDienThoai,array.AnhNhanVien);
            }

            return table;
        }
    }
}
