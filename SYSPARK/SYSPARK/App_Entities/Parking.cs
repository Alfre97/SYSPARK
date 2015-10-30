using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SYSPARK.Entities
{
    public class Parking
    {
        private int id;
        private string name;
        private int totalSpace;
        private int carSpace;
        private int motorcycleSpace;
        private int handicapSpace;
        private int busSpace;
        private List<Space> spaceList = new List<Space>();

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

        public int TotalSpace
        {
            get
            {
                return totalSpace;
            }

            set
            {
                totalSpace = value;
            }
        }

        public int CarSpace
        {
            get
            {
                return carSpace;
            }

            set
            {
                carSpace = value;
            }
        }

        public int MotorcycleSpace
        {
            get
            {
                return motorcycleSpace;
            }

            set
            {
                motorcycleSpace = value;
            }
        }

        public int HandicapSpace
        {
            get
            {
                return handicapSpace;
            }

            set
            {
                handicapSpace = value;
            }
        }

        public int BusSpace
        {
            get
            {
                return busSpace;
            }

            set
            {
                busSpace = value;
            }
        }

        public List<Space> SpaceList
        {
            get
            {
                return spaceList;
            }

            set
            {
                spaceList = value;
            }
        }
    }
}