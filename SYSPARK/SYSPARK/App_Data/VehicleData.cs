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
                insert.Parameters.Add("@VehiclePlate", SqlDbType.VarChar).Value = vehicle.VehiclePlate;
                insert.Parameters.Add("@UserId", SqlDbType.Int).Value = userId;
                insert.Parameters.Add("@VehicleId", SqlDbType.Int).Value = getVehicleId(vehicle.VehiclePlate);
                insert.ExecuteNonQuery();
            }
            connection = ManageDatabaseConnection("Close");
        }

        public int getVehicleId(string vehiclePlate)
        {
            SqlConnection connection = ManageDatabaseConnection("Open");
            DataTable dataTableVehicleId = new DataTable();
            int vehicleId = 0;
            using (SqlCommand select = new SqlCommand(@"SelectVehicleId", connection))
            {
                select.CommandType = CommandType.StoredProcedure;
                select.Parameters.Add("@VehiclePlate", SqlDbType.Int).Value = vehicleId;
                SqlDataAdapter adap = new SqlDataAdapter(select);
                adap.Fill(dataTableVehicleId);
            }
            vehicleId = Convert.ToInt32(dataTableVehicleId.Rows[0]["Id"]);
            connection = ManageDatabaseConnection("Close");
            return vehicleId;
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