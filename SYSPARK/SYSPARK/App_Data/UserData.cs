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
    public class UserData : DataBaseConnection
    {
        public DataTable getUser(string username)
        {
            DataTable dataTableUserInfo = new DataTable();
            SqlConnection connection = ManageDatabaseConnection("Open");
            using (SqlCommand select = new SqlCommand(@"SelectUser", connection))
            {
                select.CommandType = CommandType.StoredProcedure;
                select.Parameters.Add("@UserName", SqlDbType.VarChar).Value = username;
                SqlDataAdapter adap = new SqlDataAdapter(select);
                adap.Fill(dataTableUserInfo);
                connection = ManageDatabaseConnection("Close");
                return dataTableUserInfo;
            }
        }

        public User sendUser(DataTable dataTableUserInfo)
        {
            User user = new User();
            user.Name = dataTableUserInfo.Rows[0]["Name"].ToString();
            user.LastName = dataTableUserInfo.Rows[0]["LastName"].ToString();
            user.Username = dataTableUserInfo.Rows[0]["UserName"].ToString();
            user.Password = dataTableUserInfo.Rows[0]["Password"].ToString();
            user.Condition.Id = Convert.ToInt32(dataTableUserInfo.Rows[0]["ConditionId"]);
            return user;
        }
    }
}