using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SYSPARK.Entities
{
    public class Vehicle
    {
        private string vehiclePlate;
        private VehicleType type;

        public string VehiclePlate
        {
            get
            {
                return vehiclePlate;
            }

            set
            {
                vehiclePlate = value;
            }
        }

        public VehicleType Type
        {
            get
            {
                return type;
            }

            set
            {
                type = value;
            }
        }
    }
}
        