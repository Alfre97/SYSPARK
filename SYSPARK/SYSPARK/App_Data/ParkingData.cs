using SYSPARK.DataBase;
using SYSPARK.Entities;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace SYSPARK
{
    public class ParkingData : DataBaseConnection
    {
        public DataTable DataTableParking()
        {
            DataTable dataTableParking = new DataTable();
            SqlConnection connection = ManageDatabaseConnection("Open");
            using (SqlCommand select = new SqlCommand(@"SelectParking", connection))
            {
                select.CommandType = CommandType.StoredProcedure;
                SqlDataAdapter adap = new SqlDataAdapter(select);
                adap.Fill(dataTableParking);
                connection = ManageDatabaseConnection("Close");
                return dataTableParking;
            }
        }

        public void InsertParking(Parking parking)
        {
            SqlConnection connection = ManageDatabaseConnection("Open");
            using (SqlCommand insert = new SqlCommand(@"InsertParking", connection))
            {
                insert.CommandType = CommandType.StoredProcedure;
                insert.Parameters.Add("@Name", SqlDbType.VarChar).Value = parking.Name;
                insert.Parameters.Add("@TotalSpace", SqlDbType.Int).Value = parking.TotalSpace;
                insert.Parameters.Add("@CarSpace", SqlDbType.Int).Value = parking.CarSpace;
                insert.Parameters.Add("@MotorcycleSpace", SqlDbType.Int).Value = parking.MotorcycleSpace;
                insert.Parameters.Add("@HandicapSpace", SqlDbType.Int).Value = parking.HandicapSpace;
                insert.Parameters.Add("@BusSpace", SqlDbType.Int).Value = parking.BusSpace;
                insert.ExecuteNonQuery();
            }
            connection = ManageDatabaseConnection("Close");
        }

        public void DeleteParking(string parkingName)
        {
            SqlConnection connection = ManageDatabaseConnection("Open");
            using (SqlCommand delete = new SqlCommand(@"DeleteParkingSpace", connection))
            {
                delete.CommandType = CommandType.StoredProcedure;
                delete.Parameters.Add("@ParkingName", SqlDbType.VarChar).Value = parkingName;
                delete.ExecuteNonQuery();
            }
            connection = ManageDatabaseConnection("Close");
        }

        public Parking getParkingId(Parking parking)
        {
            DataTable dataTableParking = new DataTable();
            SqlConnection connection = ManageDatabaseConnection("Open");
            using (SqlCommand select = new SqlCommand(@"GetParkingId", connection))
            {
                select.CommandType = CommandType.StoredProcedure;
                select.Parameters.Add("@ParkingName", SqlDbType.VarChar).Value = parking.Name;
                SqlDataAdapter adap = new SqlDataAdapter(select);
                adap.Fill(dataTableParking);
                if (dataTableParking.Rows.Count > 0)
                    parking.Id = Convert.ToInt32(dataTableParking.Rows[0]["Id"]);
                connection = ManageDatabaseConnection("Close");
                return parking;
            }
        }
    }
}