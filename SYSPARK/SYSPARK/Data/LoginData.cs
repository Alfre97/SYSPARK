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

            using (SqlCommand search = new SqlCommand(@"selectUserNameForLogin", connection))
            {
                search.CommandType = CommandType.StoredProcedure;
                search.Parameters.Add("@UserName", SqlDbType.VarChar).Value = username;

                SqlDataAdapter adap = new SqlDataAdapter(search);
                adap.Fill(dataTableUserName);
            }

            if (dataTableUserName.Rows.Count > 0)
            {
                string userNameFromDB = "";
                DataRow row = dataTableUserName.Rows[0];
                userNameFromDB = Convert.ToString(row["UserName"]);
                if (username == userNameFromDB)
                {
                    connection = ManageDatabaseConnection("Close");
                    dataTableUserName.Clear();
                    return true;
                } else
                {
                    return false;
                }
            }
            connection = ManageDatabaseConnection("Close");
            return false;
        }

        public bool Encrypt(string password, string passwordDataBase)
        {

            password = BCrypt.Net.BCrypt.HashPassword(password, BCrypt.Net.BCrypt.GenerateSalt());
            bool verify = BCrypt.Net.BCrypt.Verify(password, passwordDataBase);

            return verify;

        }

        public Boolean CheckPassword(string password)
        {
            DataTable dataTableUserPassword = new DataTable();
            SqlConnection connection = ManageDatabaseConnection("Open");
            using (SqlCommand search = new SqlCommand(@"selectPasswordForLogin", connection))
            {
                search.CommandType = CommandType.StoredProcedure;
                search.Parameters.Add("@Password", SqlDbType.VarChar).Value = password;

                SqlDataAdapter adap = new SqlDataAdapter(search);
                adap.Fill(dataTableUserPassword);
            }

            if (dataTableUserPassword.Rows.Count > 0)
            {
                string passwordFromDB = "";
                DataRow row = dataTableUserPassword.Rows[0];
                passwordFromDB = Convert.ToString(row["Password"]);
                if (Encrypt(password, passwordFromDB) == true)
                {
                    connection = ManageDatabaseConnection("Close");
                    dataTableUserPassword.Clear();
                    return true;
                } else
                {
                    connection = ManageDatabaseConnection("Close");
                    return false;
                }
            }
            connection = ManageDatabaseConnection("Close");
            return false;
        }
    }
}