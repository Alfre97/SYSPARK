using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SYSPARK.App_Entities
{
    public class Enrollment
    {
        private int id;
        private string userName;
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

        public string UserName
        {
            get
            {
                return userName;
            }

            set
            {
                userName = value;
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