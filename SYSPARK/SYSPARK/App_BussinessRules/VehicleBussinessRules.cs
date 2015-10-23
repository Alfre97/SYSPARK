using SYSPARK.Data;
using SYSPARK.Entities;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace SYSPARK.App_BussinessRules
{
    public class VehicleBussinessRules
    {
        public int InsertVehicle(Vehicle vehicle, int userId)
        {
            try
            {
                if(vehicle.License == "")
                {
                    return 1;
                }
                //Inserting vehicle
                VehicleData vehicleData = new VehicleData();
                vehicleData.InsertVehicle(vehicle, userId);
            }
            catch (SqlException)
            {
                return 2;
            }
            return 0;
            
        }
    }
}