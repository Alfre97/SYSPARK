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

        public void InsertRole(Role role)
        {
            SqlConnection connection = ManageDatabaseConnection("Open");
            using (SqlCommand insert = new SqlCommand(@"InsertRole", connection))
            {
                insert.CommandType = CommandType.StoredProcedure;
                insert.Parameters.Add("@Name", SqlDbType.VarChar).Value = role.Name;
                insert.ExecuteNonQuery();
            }
            connection = ManageDatabaseConnection("Close");
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

        public void UpdateRole(Role role)
        {
            SqlConnection connection = ManageDatabaseConnection("Open");
            using (SqlCommand update = new SqlCommand(@"UpdateRole", connection))
            {
                update.CommandType = CommandType.StoredProcedure;
                update.Parameters.Add("@Id", SqlDbType.Int).Value = role.Id;
                update.Parameters.Add("@Name", SqlDbType.VarChar).Value = role.Name;
                update.ExecuteNonQuery();
            }
            connection = ManageDatabaseConnection("Close");
        }
    }
}