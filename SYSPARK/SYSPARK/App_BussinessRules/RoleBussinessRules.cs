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
        RoleData roleData = new RoleData();

        public int InsertRole(Role role)
        {
            try
            {
                if (role.Description.Equals(string.Empty))
                {
                    return 1;
                }
                else
                {
                    roleData.InsertRole(role);
                    return 0;
                }
            }
            catch (SqlException)
            {
                return 2;
            }
        }

        public int DeleteRole(int roleId)
        {
            try
            {
                if (roleId.Equals(string.Empty))
                    return 2;
                else
                {
                    roleData.DeleteRole(roleId);
                    return 0;
                }
            }
            catch (SqlException)
            {
                return 1;
            }
        }

        public int UpdateRole(Role role)
        {

            try
            {
                if (role.Description.Equals(string.Empty))
                {
                    return 1;
                }
                else
                {
                    roleData.UpdateRole(role);
                    return 0;
                }
            }
            catch (SqlException)
            {
                return 2;
            }
        }
    }
}