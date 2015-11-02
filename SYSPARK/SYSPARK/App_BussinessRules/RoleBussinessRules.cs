using SYSPARK.Data;
using SYSPARK.Entities;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace SYSPARK.App_BussinessRules
{
    public class RoleBussinessRules
    {
        public int InsertRole(Role role)
        {
            RoleData roleData = new RoleData();
            try {
                roleData.InsertRole(role);
                return 0;
            }
            catch (SqlException)
            {
                return 1;
            }
            

        }
    }
}