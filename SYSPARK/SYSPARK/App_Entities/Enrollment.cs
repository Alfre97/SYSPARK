using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SYSPARK.App_Entities
{
    public class Enrollment
    {
        private string uniqueIdentifier;
        private Lapse lapse;

        public string UniqueIdentifier
        {
            get
            {
                return uniqueIdentifier;
            }

            set
            {
                uniqueIdentifier = value;
            }
        }

        public Lapse Lapse
        {
            get
            {
                return lapse;
            }

            set
            {
                lapse = value;
            }
        }
    }
}