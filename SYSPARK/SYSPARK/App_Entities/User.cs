﻿using SYSPARK.App_Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SYSPARK.Entities
{
    public class User
    {
        private string name;
        private string lastName;
        private string username;
        private string password;
        private List<Vehicle> vehicleList;
        private Role role;
        private int universityCard;
        private Enrollment enrollment;

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

        public string LastName
        {
            get
            {
                return lastName;
            }

            set
            {
                lastName = value;
            }
        }

        public string Username
        {
            get
            {
                return username;
            }

            set
            {
                username = value;
            }
        }

        public string Password
        {
            get
            {
                return password;
            }

            set
            {
                password = value;
            }
        }

        public List<Vehicle> VehicleList
        {
            get
            {
                return vehicleList;
            }

            set
            {
                vehicleList = value;
            }
        }

        public Role Role
        {
            get
            {
                return role;
            }

            set
            {
                role = value;
            }
        }

        public int UniversityCard
        {
            get
            {
                return universityCard;
            }

            set
            {
                universityCard = value;
            }
        }

        public Enrollment Enrollment
        {
            get
            {
                return enrollment;
            }

            set
            {
                enrollment = value;
            }
        }
    }
}