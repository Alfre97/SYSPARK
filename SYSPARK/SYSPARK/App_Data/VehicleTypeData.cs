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

        public void InsertVehicleType(VehicleType vehicleType)
        {
            SqlConnection connection = ManageDatabaseConnection("Open");
            using (SqlCommand insert = new SqlCommand(@"InsertVehicleType", connection))
            {
                insert.CommandType = CommandType.StoredProcedure;
                insert.Parameters.Add("@Description", SqlDbType.VarChar).Value = vehicleType.Description;
                insert.ExecuteNonQuery();
            }
            connection = ManageDatabaseConnection("Close");
        }

        public void DeleteVehicleType(int vehicleTypeId)
        {
            SqlConnection connection = ManageDatabaseConnection("Open");
            using (SqlCommand delete = new SqlCommand(@"DeleteVehicleType", connection))
            {
                delete.CommandType = CommandType.StoredProcedure;
                delete.Parameters.Add("@VehicleTypeId", SqlDbType.Int).Value = vehicleTypeId;
                delete.ExecuteNonQuery();
            }
            connection = ManageDatabaseConnection("Close");
        }

        public void UpdateVehicleType(VehicleType vehicleType)
        {
            SqlConnection connection = ManageDatabaseConnection("Open");
            using (SqlCommand update = new SqlCommand(@"UpdateVehicleType", connection))
            {
                update.CommandType = CommandType.StoredProcedure;
                update.Parameters.Add("@Id", SqlDbType.Int).Value = vehicleType.Id;
                update.Parameters.Add("@Description", SqlDbType.VarChar).Value = vehicleType.Description;
                update.ExecuteNonQuery();
            }
            connection = ManageDatabaseConnection("Close");
        }
    }
}