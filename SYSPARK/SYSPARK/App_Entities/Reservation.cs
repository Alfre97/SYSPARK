using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SYSPARK.Entities
{
    public class Reservation
    {
        private int id;
        private Space space;
        private User user;
        private Vehicle vehicle;
        private DateTime checkIn;
        private DateTime checkOut;

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

        public Space Space
        {
            get
            {
                return space;
            }

            set
            {
                space = value;
            }
        }

        public User User
        {
            get
            {
                return user;
            }

            set
            {
                user = value;
            }
        }

        public Vehicle Vehicle
        {
            get
            {
                return vehicle;
            }

            set
            {
                vehicle = value;
            }
        }

        public DateTime CheckIn
        {
            get
            {
                return checkIn;
            }

            set
            {
                checkIn = value;
            }
        }

        public DateTime CheckOut
        {
            get
            {
                return checkOut;
            }

            set
            {
                checkOut = value;
            }
        }
    }
}