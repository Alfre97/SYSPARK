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
    public class VehicleData : DataBaseConnection
    {
        public void InsertVehicle(Vehicle vehicle, int userId)
        {
            SqlConnection connection = ManageDatabaseConnection("Open");
            using (SqlCommand insert = new SqlCommand(@"InsertVehicle", connection))
            {
                insert.CommandType = CommandType.StoredProcedure;
                insert.Parameters.Add("@TypeId", SqlDbType.Int).Value = vehicle.Type.Id;
                insert.Parameters.Add("@License", SqlDbType.VarChar).Value = vehicle.License;
                insert.Parameters.Add("@Owner", SqlDbType.Int).Value = userId;
                insert.ExecuteNonQuery();
            }
            connection = ManageDatabaseConnection("Close");
        }

        public DataTable GetUserVehicle(int userId)
        {
            DataTable dataTableuservehicle = new DataTable();
            SqlConnection connection = ManageDatabaseConnection("Open");
            using (SqlCommand select = new SqlCommand(@"SelectUserVehicle", connection))
            {
                select.CommandType = CommandType.StoredProcedure;
                select.Parameters.Add("@Owner", SqlDbType.Int).Value = userId;
                SqlDataAdapter adap = new SqlDataAdapter(select);
                adap.Fill(dataTableuservehicle);
            }
            connection = ManageDatabaseConnection("Close");
            return dataTableuservehicle;
        }
    }
}