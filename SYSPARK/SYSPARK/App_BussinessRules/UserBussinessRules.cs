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

                userData.InsertUser(user);
                return 0;
            }
            catch (Exception)
            {
                return 1;
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