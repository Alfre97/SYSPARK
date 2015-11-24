using SYSPARK.App_Entities;
using SYSPARK.Data;
using SYSPARK.Entities;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace SYSPARK.BussinessRules
{
    public class UserBussinessRules
    {
        UserData userData = new UserData();

        public int RegistrationRules(User user, Campus campus)
        {
            try
            {
                if (user.Name.Equals(string.Empty))
                    return 1;
                else if (user.LastName.Equals(string.Empty))
                    return 2;
                else if (user.Username.Equals(string.Empty))
                    return 3;
                else if (user.Password.Equals(string.Empty))
                    return 4;
                else if (user.UniversityCard.Equals(string.Empty))
                    return 6;
                else if (campus.Id.Equals(string.Empty))
                    return 7;
                else if (campus.Name.Equals(string.Empty))
                    return 8;
                else
                {
                    userData.InsertUser(user, campus);
                    return 0;
                }
            }
            catch (SqlException)
            {
                return 5;
            }
        }

        public int UpdateRules(User user)
        {
            try
            {
                if (user.Name.Equals(string.Empty))
                    return 1;
                else if (user.LastName.Equals(string.Empty))
                    return 2;
                else if (user.Username.Equals(string.Empty))
                    return 3;
                else if (user.Password.Equals(string.Empty))
                    return 4;
                else if (user.UniversityCard.Equals(string.Empty))
                    return 6;
                else
                {
                    userData.UpdateUser(user);
                    return 0;
                }
            }
            catch (SqlException)
            {
                return 5;
            }

        }
    }
}