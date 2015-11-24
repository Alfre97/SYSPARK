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
        VehicleData vehicleData = new VehicleData();

        public int InsertVehicle(Vehicle vehicle, string userName)
        {
            try
            {
                if (vehicle.VehiclePlate.Equals(string.Empty))
                    return 1;
                else
                    vehicleData.InsertVehicle(vehicle, userName);
            }
            catch (SqlException)
            {
                return 2;
            }
            return 0;
        }

        public int DeleteVehicle(string vehiclePlate)
        {
            try
            {
                if (vehiclePlate.Equals(string.Empty))
                    return 2;
                else
                {
                    vehicleData.DeleteVehicle(vehiclePlate);
                    return 0;
                }
            }
            catch (SqlException)
            {
                return 1;
            }
        }

        public int UpdateVehicle(Vehicle vehicle)
        {

            try
            {
                if (vehicle.VehiclePlate.Equals(string.Empty))
                {
                    return 1;
                }
                else
                {
                    vehicleData.UpdateVehicle(vehicle);
                    return 0;
                }
            }
            catch (SqlException)
            {
                return 2;
            }
        }
    }
}