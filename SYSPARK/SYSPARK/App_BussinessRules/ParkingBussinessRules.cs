using SYSPARK.Entities;
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
            List<Space> spaceList = new List<Space>();

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
            else if (parking.CarSpace + parking.MotorcycleSpace + parking.HandicapSpace + parking.BusSpace < 0)
                return 7;
            else if (parking.CarSpace + parking.MotorcycleSpace + parking.HandicapSpace + parking.BusSpace > parking.TotalSpace)
                return 8;
            else
            {
                try
                {
                    parkingData.InsertParking(parking);
                    try
                    {
                        parking = parkingData.getParkingId(parking);
                        spaceList = spaceData.createSpaceList(
                                            spaceData.createListCarSpace(parking),
                                            spaceData.createListMotorcycleSpace(parking),
                                            spaceData.createListHandicapSpace(parking),
                                            spaceData.createListBusSpace(parking)
                                            );
                        spaceData.InsertSpace(spaceList);
                        return 0;
                    }
                    catch (Exception)
                    {
                        parkingData.DeleteParking(parking.Name);
                        return 9;
                    }
                }
                catch (SqlException)
                {
                    return 10;
                }
            }
        }

        public int DeleteParking(string parkingName)
        {
            try
            {
                if (parkingName.Equals(string.Empty))
                    return 2;
                parkingData.DeleteParking(parkingName);
                return 0;
            }
            catch (SqlException)
            {
                return 1;
            }
        }
    }
}