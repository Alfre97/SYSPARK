using SYSPARK.DataBase;
using SYSPARK.Entities;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace SYSPARK.Data
{
    public class RoleData : DataBaseConnection
    {
        public DataTable DataTableRole()
        {
            DataTable dataTableCondition = new DataTable();
            SqlConnection connection = ManageDatabaseConnection("Open");
            using (SqlCommand selectCondition = new SqlCommand(@"SelectRole", connection))
            {
                selectCondition.CommandType = CommandType.StoredProcedure;
                SqlDataAdapter adap = new SqlDataAdapter(selectCondition);
                adap.Fill(dataTableCondition);
            }
            connection = ManageDatabaseConnection("Close");
            return dataTableCondition;
        }

        public int InsertRole(Role role)
        {
            SqlConnection connection = ManageDatabaseConnection("Open");
            try
            {
                using (SqlCommand insert = new SqlCommand(@"InsertRole", connection))
                {
                    insert.CommandType = CommandType.StoredProcedure;
                    insert.Parameters.Add("@Description", SqlDbType.VarChar).Value = role.Description;
                    insert.ExecuteNonQuery();
                }
                connection = ManageDatabaseConnection("Close");
                return 0;
            }
            catch
            {
                connection = ManageDatabaseConnection("Close");
                return 1;
            }
        }

        public void DeleteRole(int roleId)
        {
            SqlConnection connection = ManageDatabaseConnection("Open");
            using (SqlCommand delete = new SqlCommand(@"DeleteRole", connection))
            {
                delete.CommandType = CommandType.StoredProcedure;
                delete.Parameters.Add("@RoleId", SqlDbType.Int).Value = roleId;
                delete.ExecuteNonQuery();
            }
            connection = ManageDatabaseConnection("Close");
        }
    }
}