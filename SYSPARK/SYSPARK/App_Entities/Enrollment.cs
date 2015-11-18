using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SYSPARK.App_Entities
{
    public class Enrollment
    {
        private DateTime initialDate;
        private DateTime finalDate;
        private Campus campus;
        private bool status;

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

        public Campus Campus
        {
            get
            {
                return campus;
            }

            set
            {
                campus = value;
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