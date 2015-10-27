using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SYSPARK.Entities
{
    public class Vehicle
    {
        private int id;
        private VehicleType type;
        private string vehiclePlate;

        public int Id
        {
            get
            {
                return id;
            }

            set
            {
                id = value;
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
    }
}
        