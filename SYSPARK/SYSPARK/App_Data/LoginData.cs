using SYSPARK.DataBase;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace SYSPARK.Data
{
    public class LoginData : DataBaseConnection
    {
        public DataTable SearchUserName(string username)
        {
            DataTable dataTableUserName = new DataTable();
            SqlConnection connection = ManageDatabaseConnection("Open");

            using (SqlCommand search = new SqlCommand(@"SelectUserName", connection))
            {
                search.CommandType = CommandType.StoredProcedure;
                search.Parameters.Add("@UserName", SqlDbType.VarChar).Value = username;

                SqlDataAdapter adap = new SqlDataAdapter(search);
                adap.Fill(dataTableUserName);
                connection = ManageDatabaseConnection("Close");
                return dataTableUserName;
            }
        }

        public Boolean CheckUserName(DataTable dataTableUserName)
        {
            if (dataTableUserName.Rows.Count > 0)
            {
                dataTableUserName.Clear();
                return true;
            }
            else
            {
                return false;
            }
        }

        public DataTable SearchPassword(string username)
        {
            DataTable dataTableUserPassword = new DataTable();
            SqlConnection connection = ManageDatabaseConnection("Open");
            using (SqlCommand search = new SqlCommand(@"SelectPassword", connection))
            {
                search.CommandType = CommandType.StoredProcedure;
                search.Parameters.Add("@UserName", SqlDbType.VarChar).Value = username;

                SqlDataAdapter adap = new SqlDataAdapter(search);
                adap.Fill(dataTableUserPassword);
                connection = ManageDatabaseConnection("Close");
                return dataTableUserPassword;
            }
        }

        public Boolean CheckPassword(DataTable dataTableUserPassword, string passwordFromUser)
        {
            string passwordFromDB = "";
            if (dataTableUserPassword.Rows.Count > 0)
            {
                DataRow row = dataTableUserPassword.Rows[0];
                passwordFromDB = Convert.ToString(row["Password"]);
                bool doesPasswordMatch = BCrypt.Net.BCrypt.Verify(passwordFromUser, passwordFromDB);
                if (doesPasswordMatch == true)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return false;
            }
        }
    }
}