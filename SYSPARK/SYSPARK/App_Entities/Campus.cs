using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SYSPARK.App_Entities
{
    public class Campus
    {
        private int id;
        private string description;
        private List<Campus> parkingList;

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

        public List<Campus> ParkingList
        {
            get
            {
                return parkingList;
            }

            set
            {
                parkingList = value;
            }
        }
    }
}