using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SYSPARK.App_Entities
{
    public class UniversityCard
    {
        private string card;
        private int userId;

        public string Card
        {
            get
            {
                return card;
            }

            set
            {
                card = value;
            }
        }

        public int UserId
        {
            get
            {
                return userId;
            }

            set
            {
                userId = value;
            }
        }
    }
}