using SYSPARK.Data;
using SYSPARK.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SYSPARK.BussinessRules
{
    public class UserBussinessRules
    {
        UserData userData = new UserData();
        public int RegistrationRules(User user)
        {
            try
            {
                if (user.Name == "")
                    return 1;
                else if (user.LastName == "")
                    return 2;
                else if (user.Username == "")
                    return 3;
                else if (user.Password == "")
                    return 4;
                else
                {
                    userData.InsertUser(user);
                    return 0;
                }
            }
            catch (Exception)
            {
                return 5;
            }
        }

        public int UpdateRules(User user)
        {
            try
            {
                userData.UpdateUser(user);
                return 0;
            }
            catch (Exception)
            {
                return 1;
            }
        }
    }
}