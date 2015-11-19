using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SYSPARK.App_Entities
{
    public class Enrollment
    {
        private int id;
        private Lapse lapse;

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