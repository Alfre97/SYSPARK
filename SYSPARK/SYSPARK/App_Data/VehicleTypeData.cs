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
        public DataTable DataTableAllVehicleType()
        {
            DataTable dataTableType = new DataTable();
            SqlConnection connection = ManageDatabaseConnection("Open");
            using (SqlCommand select = new SqlCommand(@"SelectVehicleType", connection))
            {
                select.CommandType = CommandType.StoredProcedure;
                SqlDataAdapter adap = new SqlDataAdapter(select);
                adap.Fill(dataTableType);
                connection = ManageDatabaseConnection("Close");
                return dataTableType;
            }
        }
    }
}