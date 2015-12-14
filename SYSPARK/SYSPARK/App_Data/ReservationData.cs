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

        public DataTable DataTableReservation(int campusId)
        {
            DataTable dataTableReservation = new DataTable();
            connection = ManageDatabaseConnection("Open");
            using (SqlCommand select = new SqlCommand(@"SelectReservation", connection))
            {
                select.CommandType = CommandType.StoredProcedure;
                select.Parameters.Add("@CampusId", SqlDbType.Int).Value = campusId;
                SqlDataAdapter adap = new SqlDataAdapter(select);
                adap.Fill(dataTableReservation);
            }
            connection = ManageDatabaseConnection("Close");
            return dataTableReservation;
        }

        public void DeleteReservation(int reservationId)
        {
            connection = ManageDatabaseConnection("Open");
            using (SqlCommand delete = new SqlCommand(@"DeleteReservation", connection))
            {
                delete.CommandType = CommandType.StoredProcedure;
                delete.Parameters.Add("@ReservationId", SqlDbType.Int).Value = reservationId;
                delete.ExecuteNonQuery();
            }
            connection = ManageDatabaseConnection("Close");
        }

        public DataTable SearchActiveReservation(int campusId, int parkingId, int spaceId)
        {
            DataTable dataTableActiveReservation = new DataTable();
            connection = ManageDatabaseConnection("Open");
            using (SqlCommand select = new SqlCommand(@"SelectSpaceReservation", connection))
            {
                select.CommandType = CommandType.StoredProcedure;
                SqlDataAdapter adap = new SqlDataAdapter(select);
                select.Parameters.Add("@CampusId", SqlDbType.Int).Value = campusId;
                select.Parameters.Add("@ParkingId", SqlDbType.Int).Value = parkingId;
                select.Parameters.Add("@SpaceId", SqlDbType.Int).Value = spaceId;
                adap.Fill(dataTableActiveReservation);
            }
            connection = ManageDatabaseConnection("Close");
            return dataTableActiveReservation;
        }

        public List<Reservation> SendActiveReservationList(DataTable dataTableReservation)
        {
            List<Reservation> reservationList = new List<Reservation>();
            if (dataTableReservation.Rows.Count > 0)
            {
                for (int i = 0; i < dataTableReservation.Rows.Count; i++)
                {
                    Reservation reservation = new Reservation();
                    reservation.CheckIn = Convert.ToDateTime(dataTableReservation.Rows[i]["CheckIn"]);
                    reservation.CheckOut = Convert.ToDateTime(dataTableReservation.Rows[i]["CheckOut"]);
                    reservationList.Add(reservation);
                }
                return reservationList;

            }
            else
                return null;
        }
    }
}