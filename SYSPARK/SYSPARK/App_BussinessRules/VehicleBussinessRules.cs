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

        public int InsertVehicle(Vehicle vehicle, int userId)
        {
            try
            {
                if(vehicle.VehiclePlate.Equals(string.Empty))
                {
                    return 1;
                }
                //Inserting vehicle
                vehicleData.InsertVehicle(vehicle, userId);
            }
            catch (SqlException)
            {
                return 2;
            }
            return 0;
        }

        public int DeleteVehicle(int vehicleId)
        {
            try
            {
                if (vehicleId.Equals(string.Empty))
                    return 2;
                else
                {
                    vehicleData.DeleteVehicle(vehicleId);
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