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
    public class RegistrationData : DataBaseConnection 
    {
        public string EncryptPassword(string password)
        {
            BCrypt.Net.BCrypt bCrypt = new BCrypt.Net.BCrypt();
            password = BCrypt.Net.BCrypt.HashPassword(password, BCrypt.Net.BCrypt.GenerateSalt(6));
            return password;
        }

        public void InsertUser(User user)
        {
            SqlConnection connection = ManageDatabaseConnection("Open");
            using (SqlCommand insert = new SqlCommand(@"InsertUser", connection))
            {
                insert.CommandType = CommandType.StoredProcedure;
                insert.Parameters.Add("@Name", SqlDbType.VarChar).Value = user.Name;
                insert.Parameters.Add("@LastName", SqlDbType.VarChar).Value = user.LastName;
                insert.Parameters.Add("@UserName", SqlDbType.VarChar).Value = user.Username;
                insert.Parameters.Add("@Password", SqlDbType.VarChar).Value = EncryptPassword(user.Password);
                insert.Parameters.Add("@Condition", SqlDbType.VarChar).Value = user.Condition.Id;
                insert.ExecuteNonQuery();
            }
            connection = ManageDatabaseConnection("Close");
        }
    }
}