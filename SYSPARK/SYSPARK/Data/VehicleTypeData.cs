using SYSPARK.DataBase;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace SYSPARK.Data
{
    public class VehicleTypeData : DataBaseConnection
    {
        public DataSet DataSetVehicleType()
        {
            DataSet dataSetType = new DataSet();
            SqlConnection connection = ManageDatabaseConnection("Open");
            using (SqlCommand selectCondition = new SqlCommand(@"SelectVehicleType", connection))
            {
                selectCondition.CommandType = CommandType.StoredProcedure;
                SqlDataAdapter adap = new SqlDataAdapter(selectCondition);
                adap.Fill(dataSetType, "VehicleType");
                connection = ManageDatabaseConnection("Close");
                return dataSetType;
            }
        }
    }
}