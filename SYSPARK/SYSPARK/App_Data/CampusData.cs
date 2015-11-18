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
    public class CampusData : DataBaseConnection
    {
        public DataTable DataTableCampus()
        {
            DataTable dataTableCampus = new DataTable();
            SqlConnection connection = ManageDatabaseConnection("Open");
            using (SqlCommand select = new SqlCommand(@"SelectCampus", connection))
            {
                select.CommandType = CommandType.StoredProcedure;
                SqlDataAdapter adap = new SqlDataAdapter(select);
                adap.Fill(dataTableCampus);
            }
            connection = ManageDatabaseConnection("Close");
            return dataTableCampus;
        }
    }
}