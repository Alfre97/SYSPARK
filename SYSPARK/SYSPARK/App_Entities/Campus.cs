using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SYSPARK.App_Entities
{
    public class Campus
    {
        private int id;
        private string description;

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

        public string Description
        {
            get
            {
                return description;
            }

            set
            {
                description = value;
            }
        }
    }
}