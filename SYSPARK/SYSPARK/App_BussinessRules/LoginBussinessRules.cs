using SYSPARK.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SYSPARK.BussinessRules
{
    public class LoginBussinessRules
    {
        public int ValidateFields(string username, string password)
        {
            if (username == "")
            {
                return 0;
            }
            else if (password == "")
            {
                return 1;
            }
            else
            {
                return 2;
            }
        }

        public Boolean LoginUserName(string username)
        {
            LoginData login = new LoginData();
            if (login.CheckUserName(login.SearchUserName(username)))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public Boolean LoginPassword(string password, string username)
        {
            LoginData login = new LoginData();
            if (login.CheckPassword(login.SearchPassword(username), password) == true)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}