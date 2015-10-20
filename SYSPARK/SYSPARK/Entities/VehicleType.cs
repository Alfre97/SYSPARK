using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SYSPARK.Entities
{
    public class VehicleType
    {
        private int id;
        private string description;

        public int Id
        {
            get
            {
                return Id;
            }

            set
            {
                Id = value;
            }
        }

        public string Description
        {
            get
            {
                return description;
            }

            set
            {
                description = value;
            }
        }
    }
}