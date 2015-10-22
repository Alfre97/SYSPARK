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
        private string license;

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

        public string License
        {
            get
            {
                return license;
            }

            set
            {
                license = value;
            }
        }
    }
}
        