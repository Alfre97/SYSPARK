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
                insert.Parameters.Add("@VehicleId", SqlDbType.Int).Value = GetVehicleId(vehicle.VehiclePlate);
                insert.ExecuteNonQuery();
            }
            connection = ManageDatabaseConnection("Close");
        }

        public int GetVehicleId(string vehiclePlate)
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

        public DataTable DataTableVehicle()
        {
            return null;
        }

        public void DeleteVehicle(int vehicleId)
        {
            SqlConnection connection = ManageDatabaseConnection("Open");
            using (SqlCommand delete = new SqlCommand(@"DeleteVehicle", connection))
            {
                delete.CommandType = CommandType.StoredProcedure;
                delete.Parameters.Add("@VehicleId", SqlDbType.Int).Value = vehicleId;
                delete.ExecuteNonQuery();
            }
            connection = ManageDatabaseConnection("Close");
        }

        public void UpdateVehicle(Vehicle vehicle)
        {
            SqlConnection connection = ManageDatabaseConnection("Open");
            using (SqlCommand update = new SqlCommand(@"UpdateVehicle", connection))
            {
                update.CommandType = CommandType.StoredProcedure;
                update.Parameters.Add("@Id", SqlDbType.Int).Value = vehicle.Id;
                update.Parameters.Add("@TypeId", SqlDbType.VarChar).Value = vehicle.Type.Id;
                update.Parameters.Add("@VehiclePlate", SqlDbType.Int).Value = vehicle.VehiclePlate;
                update.ExecuteNonQuery();
            }
            connection = ManageDatabaseConnection("Close");
        }
    }
}