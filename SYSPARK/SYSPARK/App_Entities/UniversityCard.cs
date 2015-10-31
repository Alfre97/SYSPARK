using SYSPARK.Entities;
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
        private int used;
        private int RoleId;

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

        public int Used
        {
            get
            {
                return used;
            }

            set
            {
                used = value;
            }
        }

        public int RoleId1
        {
            get
            {
                return RoleId;
            }

            set
            {
                RoleId = value;
            }
        }
    }
}