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
        public void InsertVehicle(Vehicle vehicle)
        {
            SqlConnection connection = ManageDatabaseConnection("Open");
            using (SqlCommand insert = new SqlCommand(@"InsertVehicle", connection))
            {
                insert.CommandType = CommandType.StoredProcedure;
                insert.Parameters.Add("@TypeId", SqlDbType.Int).Value = vehicle.Type.Id;
                insert.Parameters.Add("@Lisence", SqlDbType.Int).Value = vehicle.Lisence;
                insert.ExecuteNonQuery();
            }
            connection = ManageDatabaseConnection("Close");
        }

        public DataTable SelectVehicleByUser(User user)
        {
            SqlConnection connection = ManageDatabaseConnection("Open");
            DataTable dataTableVehicleId = new DataTable();
            using (SqlCommand select = new SqlCommand(@"SelectVehicleByUser", connection))
            {
                select.CommandType = CommandType.StoredProcedure;
                select.Parameters.Add("@UserId", SqlDbType.Int).Value = user.Id;
                SqlDataAdapter adap = new SqlDataAdapter(select);
                adap.Fill(dataTableVehicleId);
            }
            connection = ManageDatabaseConnection("Close");
            return dataTableVehicleId;
        }

        public DataSet SelectByVehicleId(DataTable dataTableVehicleId)
        {
            SqlConnection connection = ManageDatabaseConnection("Open");
            DataSet dataSetVehicle = new DataSet();
            DataTable dataTableVehicle = new DataTable();
            using (SqlCommand select = new SqlCommand(@"SelectVehicleById", connection))
            {
                SqlDataAdapter adap = new SqlDataAdapter();
                for (int i = 0; i > dataTableVehicleId.Rows.Count; i++)
                {
                    select.CommandType = CommandType.StoredProcedure;
                    select.Parameters.Add("@VehicleId", SqlDbType.Int).Value = Convert.ToInt32(dataTableVehicleId.Rows[i]);
                    adap = new SqlDataAdapter(select);
                    dataTableVehicle.Rows.Add(adap);
                }
                dataSetVehicle.Tables.Add(dataTableVehicle);
            }
            connection = ManageDatabaseConnection("Close");
            return dataSetVehicle;
        }

    }
}