using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SYSPARK.Entities
{
    public class Parking
    {
        private string name;
        private int AmountOfSpace;

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

        public int AmountOfSpace1
        {
            get
            {
                return AmountOfSpace;
            }

            set
            {
                AmountOfSpace = value;
            }
        }

        public Parking(string name, int amountOfSpace)
        {
            this.name = name;
            AmountOfSpace = amountOfSpace;
        }
    }
}