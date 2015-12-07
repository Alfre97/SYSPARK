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
    public class ReservationData : DataBaseConnection
    {
        SqlConnection connection = new SqlConnection();

        public void InsertReservation(Reservation reservation)
        {
            connection = ManageDatabaseConnection("Open");
            using (SqlCommand insert = new SqlCommand(@"InsertReservation", connection))
            {
                insert.CommandType = CommandType.StoredProcedure;
                insert.Parameters.Add("@SpaceId", SqlDbType.Int).Value = reservation.Space.Id;
                insert.Parameters.Add("@SpaceParkingId", SqlDbType.Int).Value = reservation.Space.ParkingId;
                insert.Parameters.Add("@SpaceCampusId", SqlDbType.Int).Value = reservation.Space.ParkingCampusId;
                insert.Parameters.Add("@UserName", SqlDbType.VarChar).Value = reservation.User.Username;
                insert.Parameters.Add("@VehiclePlate", SqlDbType.VarChar).Value = reservation.Vehicle.VehiclePlate;
                insert.Parameters.Add("@CheckIn", SqlDbType.DateTime).Value = reservation.CheckIn;
                insert.Parameters.Add("@CheckOut", SqlDbType.DateTime).Value = reservation.CheckOut;
                insert.ExecuteNonQuery();
            }
            connection = ManageDatabaseConnection("Close");
        }

        public DataTable DataTableUserReservation(string userName)
        {
            DataTable dataTableUserReservation = new DataTable();
            connection = ManageDatabaseConnection("Open");
            using (SqlCommand select = new SqlCommand(@"SelectUserReservation", connection))
            {
                select.CommandType = CommandType.StoredProcedure;
                SqlDataAdapter adap = new SqlDataAdapter(select);
                select.Parameters.Add("@UserName", SqlDbType.VarChar).Value = userName;
                adap.Fill(dataTableUserReservation);
            }
            connection = ManageDatabaseConnection("Close");
            return dataTableUserReservation;
        }

        public DataTable DataTableReservation()
        {
            DataTable dataTableReservation = new DataTable();
            connection = ManageDatabaseConnection("Open");
            using (SqlCommand select = new SqlCommand(@"SelectReservation", connection))
            {
                select.CommandType = CommandType.StoredProcedure;
                SqlDataAdapter adap = new SqlDataAdapter(select);
                adap.Fill(dataTableReservation);
            }
            connection = ManageDatabaseConnection("Close");
            return dataTableReservation;
        }

        public void DeleteSpace(int spaceId, int parkingId, int spaceTypeId)
        {
            connection = ManageDatabaseConnection("Open");
            using (SqlCommand delete = new SqlCommand(@"DeleteSpace", connection))
            {
                delete.CommandType = CommandType.StoredProcedure;
                delete.Parameters.Add("@SpaceId", SqlDbType.Int).Value = spaceId;
                delete.Parameters.Add("@ParkingId", SqlDbType.Int).Value = parkingId;
                delete.Parameters.Add("@SpaceTypeId", SqlDbType.Int).Value = spaceTypeId;
                delete.ExecuteNonQuery();
            }
            connection = ManageDatabaseConnection("Close");
        }

    }
}