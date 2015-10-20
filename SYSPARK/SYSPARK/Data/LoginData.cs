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

            using (SqlCommand search = new SqlCommand(@"selectUserNameForLogin", connection))
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

        public string EncryptPassword(string password)
        {

            password = BCrypt.Net.BCrypt.HashPassword(password, BCrypt.Net.BCrypt.GenerateSalt());
            //bool verify = BCrypt.Net.BCrypt.Verify(password, passwordDataBase);

            return password;
        }

        public DataTable SearchPassword(string password)
        {
            DataTable dataTableUserPassword = new DataTable();
            SqlConnection connection = ManageDatabaseConnection("Open");
            using (SqlCommand search = new SqlCommand(@"selectPasswordForLogin", connection))
            {
                search.CommandType = CommandType.StoredProcedure;
                search.Parameters.Add("@Password", SqlDbType.VarChar).Value = password;

                SqlDataAdapter adap = new SqlDataAdapter(search);
                adap.Fill(dataTableUserPassword);
                connection = ManageDatabaseConnection("Close");
                return dataTableUserPassword;
            }
        }

        public Boolean CheckPassword(DataTable dataTableUserPassword)
        {
            if (dataTableUserPassword.Rows.Count > 0)
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