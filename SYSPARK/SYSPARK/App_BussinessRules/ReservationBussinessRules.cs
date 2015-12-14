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
                else if (reservation.CheckIn == reservation.CheckOut)
                    return 11;
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

        public int SearchActiveReservation(Reservation reservation)
        {
            List<Reservation> activeReservationList = reservationData.SendActiveReservationList(reservationData.SearchActiveReservation(reservation.Space.ParkingCampusId, reservation.Space.ParkingId, reservation.Space.Id));
            List<DateTime> reservationInsideHoursList = new List<DateTime>();
            List<DateTime> activeReservationInsideHoursList = new List<DateTime>();
            if (activeReservationList !=  null)
            {
                for (DateTime i = reservation.CheckIn.AddHours(1); i < reservation.CheckOut; i = i.AddHours(1))
                {
                    reservationInsideHoursList.Add(i);
                }

                foreach (Reservation activeReservation in activeReservationList)
                {
                    if (reservation.CheckIn == activeReservation.CheckIn)
                        return 1;
                    else if (reservation.CheckOut == activeReservation.CheckOut)
                        return 1;

                    for (DateTime i = activeReservation.CheckIn.AddHours(1); i < activeReservation.CheckOut; i = i.AddHours(1))
                    {
                        activeReservationInsideHoursList.Add(i);
                    }

                    foreach (DateTime reservationHour in reservationInsideHoursList)
                    {
                        foreach (DateTime activeReservationHour in activeReservationInsideHoursList)
                        {
                            if (reservationHour == activeReservationHour)
                                return 1;
                        }
                    }
                }
            }
            return 0;
        }

        public int DeleteReservation(int reservationId)
        {
            try
            {
                if (reservationId.Equals(string.Empty))
                    return 2;
                reservationData.DeleteReservation(reservationId);
                return 0;
            }
            catch (SqlException)
            {
                return 1;
            }
        }
    }
}