using SYSPARK.DataBase;
using SYSPARK.Entities;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace SYSPARK.Data
{
    public class CodeData : DataBaseConnection
    {
        public DataTable getCode(string code)
        {
            DataTable dataTableCode = new DataTable();
            SqlConnection connection = ManageDatabaseConnection("Open");
            using (SqlCommand select = new SqlCommand(@"SelectCode", connection))
            {
                select.CommandType = CommandType.StoredProcedure;
                select.Parameters.Add("@Code", SqlDbType.VarChar).Value = code;
                SqlDataAdapter adap = new SqlDataAdapter(select);
                adap.Fill(dataTableCode);
                connection = ManageDatabaseConnection("Close");
                return dataTableCode;
            }
        }

        public int sendDecision(DataTable dataTableCode)
        {
            if (dataTableCode.Rows.Count > 0)
            {
                if (Convert.ToByte(dataTableCode.Rows[0]["Used"]) == 0)
                    return 0;
                else if (Convert.ToByte(dataTableCode.Rows[0]["Used"]) == 1)
                    return 2;
            }
            return 1;
        }

        public void updateCode(string code, byte used)
        {
            SqlConnection connection = ManageDatabaseConnection("Open");
            using (SqlCommand update = new SqlCommand(@"UpdateCode", connection))
            {
                update.CommandType = CommandType.StoredProcedure;
                update.Parameters.Add("@Code", SqlDbType.VarChar).Value = code;
                update.Parameters.Add("@Used", SqlDbType.VarChar).Value = used;
                update.ExecuteNonQuery();
                connection = ManageDatabaseConnection("Close");
            }
        }

    }
}