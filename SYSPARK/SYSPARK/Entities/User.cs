using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SYSPARK.Entities
{
    public class User
    {
        private int id;
        private string name;
        private string lastName;
        private string password;
        private int condition;
        private string describeCondition;

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

        public int Condition
        {
            get
            {
                return condition;
            }

            set
            {
                condition = value;
            }
        }

        public string DescribeCondition
        {
            get
            {
                return describeCondition;
            }

            set
            {
                describeCondition = value;
            }
        }

        public User(int id, string name, string lastName, string password, int condition, string describeCondition)
        {
            this.id = id;
            this.name = name;
            this.lastName = lastName;
            this.password = password;
            this.condition = condition;
            this.describeCondition = describeCondition;
        }

    }
}