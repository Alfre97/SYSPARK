using SYSPARK.Entities;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace SYSPARK.BussinessRules
{
    public class ReservationBussinessRules
    {
        ReservationData reservationData = new ReservationData();

        public int InsertReservation(Reservation reservation)
        {
            try
            {
                if (reservation.Space.Id.Equals(string.Empty))
                    return 1;
                else if (reservation.Space.ParkingId.Equals(string.Empty))
                    return 2;
                else if (reservation.Space.ParkingCampusId.Equals(string.Empty))
                    return 3;
                else if (reservation.User.Username.Equals(string.Empty))
                    return 4;
                else if (reservation.Vehicle.VehiclePlate.Equals(string.Empty))
                    return 6;
                else if (reservation.CheckIn.Equals(string.Empty))
                    return 7;
                else if (reservation.CheckOut.Equals(string.Empty))
                    return 8;
                else if (reservation.CheckIn > reservation.CheckOut)
                    return 9;
                else if (reservation.CheckIn < DateTime.Now)
                    return 10;
                else
                {
                    reservationData.InsertReservation(reservation);
                    return 0;
                }
            }
            catch (SqlException)
            {
                return 5;
            }
        }

    }
}