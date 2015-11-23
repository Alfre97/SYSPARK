using SYSPARK.App_Entities;
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
            Role role = new Role();
            Campus campus = new Campus();
            Enrollment enrollment = new Enrollment();
            user.Name = dataTableUserInfo.Rows[0]["Name"].ToString();
            user.LastName = dataTableUserInfo.Rows[0]["LastName"].ToString();
            user.Username = dataTableUserInfo.Rows[0]["UserName"].ToString();
            user.Password = dataTableUserInfo.Rows[0]["Password"].ToString();
            role.Id = Convert.ToInt32(dataTableUserInfo.Rows[0]["ConditionId"]);
            user.Role = role;
            user.UniversityCard = Convert.ToInt32(dataTableUserInfo.Rows[0]["UniversityCard"]);
            campus.Id = Convert.ToInt32(dataTableUserInfo.Rows[0]["CampusId"]);
            user.Campus = campus;
            enrollment.UniqueIdentifier = dataTableUserInfo.Rows[0]["EnrollmentUniqueIdentifier"].ToString();
            user.Enrollment = enrollment;
            return user;
        }

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
                insert.Parameters.Add("@ConditionId", SqlDbType.Int).Value = user.Role.Id;
                insert.Parameters.Add("@UniversityCard", SqlDbType.Int).Value = user.UniversityCard;
                insert.Parameters.Add("@CampusId", SqlDbType.Int).Value = user.Campus.Id;
                insert.ExecuteNonQuery();
            }
            connection = ManageDatabaseConnection("Close");
        }

        public void UpdateUser(User user)
        {
            SqlConnection connection = ManageDatabaseConnection("Open");
            using (SqlCommand update = new SqlCommand(@"UpdateUser", connection))
            {
                update.CommandType = CommandType.StoredProcedure;
                update.Parameters.Add("@Name", SqlDbType.VarChar).Value = user.Name;
                update.Parameters.Add("@LastName", SqlDbType.VarChar).Value = user.LastName;
                update.Parameters.Add("@UserName", SqlDbType.VarChar).Value = user.Username;
                update.Parameters.Add("@Password", SqlDbType.VarChar).Value = EncryptPassword(user.Password);
                update.Parameters.Add("@ConditionId", SqlDbType.Int).Value = user.Role.Id;
                update.Parameters.Add("@UniversityCard", SqlDbType.Int).Value = user.UniversityCard;
                update.Parameters.Add("@CampusId", SqlDbType.Int).Value = user.Campus.Id;
                update.ExecuteNonQuery();
            }
            connection = ManageDatabaseConnection("Close");
        }

        public void UpdateUserEnrollment(string userName, string enrollmentUniqueIdentifier)
        {
            SqlConnection connection = ManageDatabaseConnection("Open");
            using (SqlCommand update = new SqlCommand(@"UpdateUserEnrollment", connection))
            {
                update.CommandType = CommandType.StoredProcedure;
                update.Parameters.Add("@UserName", SqlDbType.VarChar).Value = userName;
                update.Parameters.Add("@EnrollmentUniqueIdentifier", SqlDbType.VarChar).Value = enrollmentUniqueIdentifier;
                update.ExecuteNonQuery();
            }
            connection = ManageDatabaseConnection("Close");
        }
    }
}