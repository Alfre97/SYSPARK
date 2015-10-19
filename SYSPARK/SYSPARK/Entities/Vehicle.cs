using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SYSPARK.Entities
{
    public class Vehicle
    {
        private int id;
        private int type;
        private string descriptionType;
        private int lisence;

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

        public int Type
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

        public string DescriptionType
        {
            get
            {
                return descriptionType;
            }

            set
            {
                descriptionType = value;
            }
        }

        public int Lisence
        {
            get
            {
                return lisence;
            }

            set
            {
                lisence = value;
            }
        }

        public Vehicle(int id, int type, string descriptionType, int lisence)
        {
            this.id = id;
            this.type = type;
            this.descriptionType = descriptionType;
            this.lisence = lisence;
        }
    }
}