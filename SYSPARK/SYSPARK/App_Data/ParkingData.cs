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
        SqlConnection connection = new SqlConnection();

        public DataTable DataTableParking(int campusId)
        {
            DataTable dataTableParking = new DataTable();
            SqlConnection connection = ManageDatabaseConnection("Open");
            using (SqlCommand select = new SqlCommand(@"SelectParking", connection))
            {
                select.CommandType = CommandType.StoredProcedure;

                SqlDataAdapter adap = new SqlDataAdapter(select);
                select.Parameters.Add("@CampusId", SqlDbType.Int).Value = campusId;
                adap.Fill(dataTableParking);
                connection = ManageDatabaseConnection("Close");
                return dataTableParking;
            }
        }

        public DataTable DataTableSearchParking(int campusId, string parkingName)
        {
            DataTable dataTableParking = new DataTable();
            SqlConnection connection = ManageDatabaseConnection("Open");
            using (SqlCommand select = new SqlCommand(@"SearchParking", connection))
            {
                select.CommandType = CommandType.StoredProcedure;

                SqlDataAdapter adap = new SqlDataAdapter(select);
                select.Parameters.Add("@CampusId", SqlDbType.Int).Value = campusId;
                select.Parameters.Add("@ParkingName", SqlDbType.VarChar).Value = parkingName;
                adap.Fill(dataTableParking);
            }
            connection = ManageDatabaseConnection("Close");
            return dataTableParking;
        }

        public void InsertParking(Parking parking)
        {
            connection = ManageDatabaseConnection("Open");
            using (SqlCommand insert = new SqlCommand(@"InsertParking", connection))
            {
                insert.CommandType = CommandType.StoredProcedure;
                insert.Parameters.Add("@Name", SqlDbType.VarChar).Value = parking.Name;
                insert.Parameters.Add("@CampusId", SqlDbType.Int).Value = parking.CampusId;
                insert.Parameters.Add("@CampusName", SqlDbType.VarChar).Value = parking.CampusName;
                insert.Parameters.Add("@Height", SqlDbType.Int).Value = parking.Height;
                insert.Parameters.Add("@Width", SqlDbType.Int).Value = parking.Width;
                insert.Parameters.Add("@TotalSpace", SqlDbType.Int).Value = parking.TotalSpace;
                insert.Parameters.Add("@FreeSpace", SqlDbType.Int).Value = parking.FreeSpace;
                insert.Parameters.Add("@CarSpace", SqlDbType.Int).Value = parking.CarSpace;
                insert.Parameters.Add("@MotorcycleSpace", SqlDbType.Int).Value = parking.MotorcycleSpace;
                insert.Parameters.Add("@HandicapSpace", SqlDbType.Int).Value = parking.HandicapSpace;
                insert.Parameters.Add("@BusSpace", SqlDbType.Int).Value = parking.BusSpace;
                
                insert.ExecuteNonQuery();
            }
            connection = ManageDatabaseConnection("Close");
        }

        public void DeleteParking(int parkingId)
        {
            connection = ManageDatabaseConnection("Open");
            using (SqlCommand delete = new SqlCommand(@"DeleteParkingSpace", connection))
            {
                delete.CommandType = CommandType.StoredProcedure;
                delete.Parameters.Add("@ParkingId", SqlDbType.Int).Value = parkingId;
                delete.ExecuteNonQuery();
            }
            connection = ManageDatabaseConnection("Close");
        }

        public int GetParkingId(string parkingName, int campusId)
        {
            DataTable dataTableParking = new DataTable();
            int parkingId = 0;

            connection = ManageDatabaseConnection("Open");
            using (SqlCommand select = new SqlCommand(@"GetParkingId", connection))
            {
                select.CommandType = CommandType.StoredProcedure;
                select.Parameters.Add("@ParkingName", SqlDbType.VarChar).Value = parkingName;
                select.Parameters.Add("@CampusId", SqlDbType.Int).Value = campusId;
                SqlDataAdapter adap = new SqlDataAdapter(select);
                adap.Fill(dataTableParking);
                if (dataTableParking.Rows.Count > 0)
                    parkingId = Convert.ToInt32(dataTableParking.Rows[0]["Id"]);
                connection = ManageDatabaseConnection("Close");             
            }
            return parkingId;
        }

        public void UpdateParking(Parking parking)
        {
            connection = ManageDatabaseConnection("Open");
            using (SqlCommand update = new SqlCommand(@"UpdateParking", connection))
            {
                update.CommandType = CommandType.StoredProcedure;
                update.Parameters.Add("@Id", SqlDbType.Int).Value = parking.Id;
                update.Parameters.Add("@Name", SqlDbType.VarChar).Value = parking.Name;
                update.Parameters.Add("@TotalSpace", SqlDbType.Int).Value = parking.TotalSpace;
                update.Parameters.Add("@CarSpace", SqlDbType.Int).Value = parking.CarSpace;
                update.Parameters.Add("@MotorcycleSpace", SqlDbType.Int).Value = parking.MotorcycleSpace;
                update.Parameters.Add("@HandicapSpace", SqlDbType.Int).Value = parking.HandicapSpace;
                update.Parameters.Add("@BusSpace", SqlDbType.Int).Value = parking.BusSpace;
                update.Parameters.Add("@CampusId", SqlDbType.Int).Value = parking.CampusId;
                update.ExecuteNonQuery();
            }
            connection = ManageDatabaseConnection("Close");
        }

        public DataTable GetParking(int id)
        {
            DataTable dataTableParkingInfo = new DataTable();
            connection = ManageDatabaseConnection("Open");
            using (SqlCommand select = new SqlCommand(@"GetParking", connection))
            {
                select.CommandType = CommandType.StoredProcedure;
                select.Parameters.Add("@Id", SqlDbType.Int).Value = id;
                SqlDataAdapter adap = new SqlDataAdapter(select);
                adap.Fill(dataTableParkingInfo);
                connection = ManageDatabaseConnection("Close");
                return dataTableParkingInfo;
            }
        }

        public Parking SendParkingInfo(DataTable dtParking)
        {
            Parking parking = new Parking();
            if (dtParking.Rows.Count > 0)
            {
                parking.Width = Convert.ToInt32(dtParking.Rows[0]["Width"]);
                parking.Height = Convert.ToInt32(dtParking.Rows[0]["Height"]);
                return parking;
            }
            else
                return null;
        }

        public Parking SendParking(DataTable dataTableParkingInfo)
        {
            Parking parking = new Parking();
            parking.Name = dataTableParkingInfo.Rows[0]["Name"].ToString();
            parking.TotalSpace = Convert.ToInt32(dataTableParkingInfo.Rows[0]["TotalSpace"]);
            parking.CarSpace = Convert.ToInt32(dataTableParkingInfo.Rows[0]["CarSpace"]);
            parking.MotorcycleSpace = Convert.ToInt32(dataTableParkingInfo.Rows[0]["MotorcycleSpace"]);
            parking.HandicapSpace = Convert.ToInt32(dataTableParkingInfo.Rows[0]["HandicapSpace"]);
            parking.BusSpace = Convert.ToInt32(dataTableParkingInfo.Rows[0]["BusSpace"]);
            parking.CampusId = Convert.ToInt32(dataTableParkingInfo.Rows[0]["CampusId"]);
            return parking;
        }
    }
}