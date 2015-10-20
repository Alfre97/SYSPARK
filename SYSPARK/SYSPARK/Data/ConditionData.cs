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
        public DataSet DataSetCondition()
        {
            DataSet dataSetCondition = new DataSet();
            SqlConnection connection = ManageDatabaseConnection("Open");
            using (SqlCommand selectCondition = new SqlCommand(@"SelectCondition", connection))
            {
                selectCondition.CommandType = CommandType.StoredProcedure;
                SqlDataAdapter adap = new SqlDataAdapter(selectCondition);
                adap.Fill(dataSetCondition, "Condition");
                connection = ManageDatabaseConnection("Close");
                return dataSetCondition;
            }


        }
    }
}