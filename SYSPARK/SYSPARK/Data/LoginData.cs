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
        public Boolean CheckUserName(string username)
        {
            DataTable dataTableUserName = new DataTable();
            SqlConnection connection = ManageDatabaseConnection("Open");
            string searchUserName = "SELECT UserName FROM [User] WHERE UserName= @username";

            SqlCommand search = new SqlCommand(searchUserName, connection);
            search.Parameters.AddWithValue("@username", username);
            SqlDataAdapter adap = new SqlDataAdapter(search);
            adap.Fill(dataTableUserName);

            if (dataTableUserName.Rows.Count >= 0)
            {
                string userNameFromDB = "";
                DataRow row = dataTableUserName.Rows[0];
                userNameFromDB = Convert.ToString(row["UserName"]);
                if (username == userNameFromDB)
                {
                    connection = ManageDatabaseConnection("Close");
                    dataTableUserName.Clear();
                    return true;
                }
            }
            connection = ManageDatabaseConnection("Close");
            return false;
        }

        public Boolean CheckPassword(string password)
        {
            DataTable dataTableUserPassword = new DataTable();
            SqlConnection connection = ManageDatabaseConnection("Open");
            string searchUser = "SELECT Password FROM [User] WHERE Password= @password";

            SqlCommand search = new SqlCommand(searchUser, connection);
            search.Parameters.AddWithValue("@password", password);
            SqlDataAdapter adap = new SqlDataAdapter(search);
            adap.Fill(dataTableUserPassword);

            if (dataTableUserPassword.Rows.Count > 0)
            {
                connection = ManageDatabaseConnection("Close");
                dataTableUserPassword.Clear();
                return true;
            }
            connection = ManageDatabaseConnection("Close");
            return false;
        }
    }
}