using SYSPARK.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SYSPARK.App_Entities
{
    public class Campus
    {
        private int id;
        private string name;
        private List<Parking> parkingList;

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

        public string Name
        {
            get
            {
                return name;
            }

            set
            {
                name = value;
            }
        }

        public List<Parking> ParkingList
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