using SYSPARK.App_Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SYSPARK.Entities
{
    public class Space
    {
        private int id;
        private string name;
        private int parkingId;
        private string parkingName;
        private int parkingCampusId;
        private SpaceType spaceType;

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

        public int ParkingId
        {
            get
            {
                return parkingId;
            }

            set
            {
                parkingId = value;
            }
        }

        public string ParkingName
        {
            get
            {
                return parkingName;
            }

            set
            {
                parkingName = value;
            }
        }

        public int ParkingCampusId
        {
            get
            {
                return parkingCampusId;
            }

            set
            {
                parkingCampusId = value;
            }
        }

        public SpaceType SpaceType
        {
            get
            {
                return spaceType;
            }

            set
            {
                spaceType = value;
            }
        }
    }
}