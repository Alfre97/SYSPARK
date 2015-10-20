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
    public class ConditionData : DataBaseConnection
    {
        public DataTable DataTableCondition()
        {
            DataTable dataTableCondition = new DataTable();
            SqlConnection connection = ManageDatabaseConnection("Open");
            using (SqlCommand selectCondition = new SqlCommand(@"SelectCondition", connection))
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