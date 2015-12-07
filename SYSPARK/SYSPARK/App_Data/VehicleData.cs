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
        SqlConnection connection = new SqlConnection();

        public DataTable DataTableVehicle()
        {
            DataTable dataTableVehicle = new DataTable();
            connection = ManageDatabaseConnection("Open");
            using (SqlCommand select = new SqlCommand(@"SelectAllVehicle", connection))
            {
                connection = ManageDatabaseConnection("Open");

                select.CommandType = CommandType.StoredProcedure;
                SqlDataAdapter adap = new SqlDataAdapter(select);
                adap.Fill(dataTableVehicle);

                connection = ManageDatabaseConnection("Close");
            }
            return dataTableVehicle;
        }

        public void InsertVehicle(Vehicle vehicle, string userName)
        {
            connection = ManageDatabaseConnection("Open");
            using (SqlCommand insert = new SqlCommand(@"InsertVehicle", connection))
            {
                insert.CommandType = CommandType.StoredProcedure;
                insert.Parameters.Add("@UserName", SqlDbType.VarChar).Value = userName;
                insert.Parameters.Add("@VehiclePlate", SqlDbType.VarChar).Value = vehicle.VehiclePlate;
                insert.Parameters.Add("@VehicleTypeId", SqlDbType.Int).Value = vehicle.Type.Id;
                insert.Parameters.Add("@VehicleTypeName", SqlDbType.VarChar).Value = vehicle.Type.Name;
                insert.ExecuteNonQuery();
            }
            connection = ManageDatabaseConnection("Close");
        }

        public int GetVehicleId(string vehiclePlate)
        {
            connection = ManageDatabaseConnection("Open");
            DataTable dataTableVehicleId = new DataTable();
            int vehicleId = 0;
            using (SqlCommand select = new SqlCommand(@"SelectVehiclePlate", connection))
            {
                select.CommandType = CommandType.StoredProcedure;
                select.Parameters.Add("@VehiclePlate", SqlDbType.Int).Value = vehicleId;
                SqlDataAdapter adap = new SqlDataAdapter(select);
                adap.Fill(dataTableVehicleId);
            }
            vehicleId = Convert.ToInt32(dataTableVehicleId.Rows[0]["VehiclePlate"]);
            connection = ManageDatabaseConnection("Close");
            return vehicleId;
        }

        public DataTable GetUserVehicle(string userName)
        {
            DataTable dataTableUserVehicle = new DataTable();
            connection = ManageDatabaseConnection("Open");
            using (SqlCommand select = new SqlCommand(@"SelectUserVehicle", connection))
            {
                select.CommandType = CommandType.StoredProcedure;
                select.Parameters.Add("@UserName", SqlDbType.VarChar).Value = userName;
                SqlDataAdapter adap = new SqlDataAdapter(select);
                adap.Fill(dataTableUserVehicle);
            }
            connection = ManageDatabaseConnection("Close");
            return dataTableUserVehicle;
        }

        public List<string> SendVehicleList(DataTable dataTableUserVehicles)
        {
            List<string> vehicleList = new List<string>();
            for (int i = 0; i < dataTableUserVehicles.Rows.Count; i++)
            {
                vehicleList.Add(dataTableUserVehicles.Rows[i]["VehiclePlate"].ToString());
            }
            return vehicleList;
        }

        public DataTable DataTableUserVehicle(List<string> vehicleList)
        {
            DataTable dataTableUserVehicle = new DataTable();
            //DataTable dataTableUserVehicleToSave = new DataTable();
            for (int i = 0; i < vehicleList.Count; i++)
            {
                using (SqlCommand select = new SqlCommand(@"SelectVehicle", connection))
                {
                    connection = ManageDatabaseConnection("Open");

                    select.CommandType = CommandType.StoredProcedure;
                    select.Parameters.Add("@Plate", SqlDbType.VarChar).Value = vehicleList[i];
                    SqlDataAdapter adap = new SqlDataAdapter(select);
                    adap.Fill(dataTableUserVehicle);

                    connection = ManageDatabaseConnection("Close");
                }
            }
            return dataTableUserVehicle;
        }

        public void DeleteVehicle(string vehiclePlate, string userName)
        {
            connection = ManageDatabaseConnection("Open");
            using (SqlCommand delete = new SqlCommand(@"DeleteVehicle", connection))
            {
                delete.CommandType = CommandType.StoredProcedure;
                delete.Parameters.Add("@Plate", SqlDbType.VarChar).Value = vehiclePlate;
                delete.Parameters.Add("@UserName", SqlDbType.VarChar).Value = userName;

                delete.ExecuteNonQuery();
            }
            connection = ManageDatabaseConnection("Close");
        }

        public void UpdateVehicle(Vehicle vehicle)
        {
            connection = ManageDatabaseConnection("Open");
            using (SqlCommand update = new SqlCommand(@"UpdateVehicle", connection))
            {
                update.CommandType = CommandType.StoredProcedure;
                update.Parameters.Add("@VehiclePlate", SqlDbType.Int).Value = vehicle.VehiclePlate;
                update.Parameters.Add("@VehicleTypeId", SqlDbType.Int).Value = vehicle.Type.Id;
                update.Parameters.Add("@VehicleTypeName", SqlDbType.VarChar).Value = vehicle.Type.Name;
                update.ExecuteNonQuery();
            }
            connection = ManageDatabaseConnection("Close");
        }
    }
}