﻿using SYSPARK.Entities;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace SYSPARK.App_BussinessRules
{
    public class ParkingBussinessRules
    {
        ParkingData parkingData = new ParkingData();

        public int InsertParking(Parking parking)
        {
            SpaceData spaceData = new SpaceData();

            if (parking.Name.Equals(string.Empty))
                return 1;
            else if (parking.Height <= 0)
                return 13;
            else if (parking.Width <= 0)
                return 14;
            else if (parking.TotalSpace <= 0)
                return 2;
            else if (parking.CarSpace < 0)
                return 3;
            else if (parking.MotorcycleSpace < 0)
                return 4;
            else if (parking.HandicapSpace < 0)
                return 5;
            else if (parking.BusSpace < 0)
                return 6;
            else if (parking.CarSpace + parking.MotorcycleSpace + parking.HandicapSpace + parking.BusSpace <= 0)
                return 7;
            else if (parking.CarSpace + parking.MotorcycleSpace + parking.HandicapSpace + parking.BusSpace > parking.TotalSpace)
                return 8;
            else if (parking.CarSpace + parking.MotorcycleSpace + parking.HandicapSpace + parking.BusSpace < parking.TotalSpace)
                return 11;
            else if (parking.CampusId.Equals(string.Empty))
                return 12;
            else
            {
                try
                {
                    parkingData.InsertParking(parking);
                    return 0;
                }
                catch (SqlException)
                {
                    return 10;
                }
            }
        }

        public int InsertParkingSpace(Parking parking)
        {
            SpaceData spaceData = new SpaceData();
            try
            {
                parking.Id = parkingData.GetParkingId(parking.Name, parking.CampusId);
                foreach (Space space in parking.SpaceList)
                {
                    spaceData.InsertSpace(space, 0);
                }
                return 0;
            }
            catch (SqlException)
            {
                try
                {
                    parkingData.DeleteParking(parking.Id);
                    return 1;
                }
                catch (SqlException)
                {
                    return 2;
                }
            }
        }

        public int DeleteParking(int parkingId)
        {
            try
            {
                if (parkingId.Equals(string.Empty))
                    return 2;
                parkingData.DeleteParking(parkingId);
                return 0;
            }
            catch (SqlException)
            {
                return 1;
            }
        }

        public int UpdateParking(Parking parking)
        {
            if (parking.Name.Equals(string.Empty))
                return 1;
            else if (parking.TotalSpace <= 0)
                return 2;
            else if (parking.CarSpace < 0)
                return 3;
            else if (parking.MotorcycleSpace < 0)
                return 4;
            else if (parking.HandicapSpace < 0)
                return 5;
            else if (parking.BusSpace < 0)
                return 6;
            else if (parking.CarSpace + parking.MotorcycleSpace + parking.HandicapSpace + parking.BusSpace <= 0)
                return 7;
            else if (parking.CarSpace + parking.MotorcycleSpace + parking.HandicapSpace + parking.BusSpace > parking.TotalSpace)
                return 8;
            else if (parking.CarSpace + parking.MotorcycleSpace + parking.HandicapSpace + parking.BusSpace < parking.TotalSpace)
                return 9;
            else if (parking.CampusId.Equals(string.Empty))
                return 12;
            else
            {
                try
                {
                    parkingData.UpdateParking(parking);
                    return 0;
                }
                catch (SqlException)
                {
                    return 10;
                }
            }
        }
    }
}