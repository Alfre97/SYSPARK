using SYSPARK.DataBase;
using SYSPARK.Entities;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace SYSPARK
{
    public class LapseData : DataBaseConnection
    {
        public DataTable DataTableLapse()
        {
            DataTable dataTableLapse = new DataTable();
            SqlConnection connection = ManageDatabaseConnection("Open");
            using (SqlCommand select = new SqlCommand(@"SelectLapse", connection))
            {
                select.CommandType = CommandType.StoredProcedure;
                SqlDataAdapter adap = new SqlDataAdapter(select);
                adap.Fill(dataTableLapse);
            }
            connection = ManageDatabaseConnection("Close");
            return dataTableLapse;
        }


    }
}