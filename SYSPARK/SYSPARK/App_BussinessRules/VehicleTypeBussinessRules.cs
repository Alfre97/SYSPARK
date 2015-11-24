using SYSPARK.Data;
using SYSPARK.Entities;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace SYSPARK.App_BussinessRules
{
    public class VehicleTypeBussinessRules
    {
        VehicleTypeData vehicleTypeData = new VehicleTypeData();

        public int InsertVehicleType(VehicleType vehicleType)
        {
            try
            {
                if (vehicleType.Name.Equals(string.Empty))
                {
                    return 1;
                }
                else
                {
                    vehicleTypeData.InsertVehicleType(vehicleType);
                    return 0;
                }
            }
            catch (SqlException)
            {
                return 2;
            }
        }

        public int DeleteVehicleType(int vehicleTypeId)
        {
            try
            {
                if (vehicleTypeId.Equals(string.Empty))
                    return 2;
                else
                {
                    vehicleTypeData.DeleteVehicleType(vehicleTypeId);
                    return 0;
                }
            }
            catch (SqlException)
            {
                return 1;
            }
        }

        public int UpdateVehicleType(VehicleType vehicleType)
        {
            try
            {
                if (vehicleType.Name.Equals(string.Empty))
                {
                    return 1;
                }
                else
                {
                    vehicleTypeData.UpdateVehicleType(vehicleType);
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