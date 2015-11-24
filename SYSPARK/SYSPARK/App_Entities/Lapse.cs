using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SYSPARK.App_Entities
{
    public class Lapse
    {
        private int id;
        private string name;
        private DateTime initialDate;
        private DateTime finalDate;
        private bool status;

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

        public DateTime InitialDate
        {
            get
            {
                return initialDate;
            }

            set
            {
                initialDate = value;
            }
        }

        public DateTime FinalDate
        {
            get
            {
                return finalDate;
            }

            set
            {
                finalDate = value;
            }
        }

        public bool Status
        {
            get
            {
                return status;
            }

            set
            {
                status = value;
            }
        }
    }
}