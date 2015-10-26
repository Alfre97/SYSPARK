using SYSPARK.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SYSPARK.App_BussinessRules
{
    public class ParkingBussinessRules
    {
        public int InsertParking(Parking parking)
        {
            ParkingData parkingData = new ParkingData();
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
                parkingData.InsertParking(parking);
                spaceList = spaceData.createSpaceList(
                    spaceData.createListCarSpace(parking.Name, parking.CarSpace),
                    spaceData.createListMotorcycleSpace(parking.Name, parking.MotorcycleSpace),
                    spaceData.createListHandicapSpace(parking.Name, parking.HandicapSpace),
                    spaceData.createListBusSpace(parking.Name, parking.BusSpace)
                    );
                spaceData.InsertSpace(spaceList);
                return 0;
            }
        }
    }
}