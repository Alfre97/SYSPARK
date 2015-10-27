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
    public class RoleData : DataBaseConnection
    {
        public DataTable DataTableRole()
        {
            DataTable dataTableCondition = new DataTable();
            SqlConnection connection = ManageDatabaseConnection("Open");
            using (SqlCommand selectCondition = new SqlCommand(@"SelectRole", connection))
            {
                selectCondition.CommandType = CommandType.StoredProcedure;
                SqlDataAdapter adap = new SqlDataAdapter(selectCondition);
                adap.Fill(dataTableCondition);
                connection = ManageDatabaseConnection("Close");
                return dataTableCondition;
            }
        }
    }
}