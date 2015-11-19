﻿using SYSPARK.DataBase;
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
            user.Role.Id = Convert.ToInt32(dataTableUserInfo.Rows[0]["ConditionId"]);
            user.UniversityCard = Convert.ToInt32(dataTableUserInfo.Rows[0]["UniversityCard"]);
            user.Campus.Id = Convert.ToInt32(dataTableUserInfo.Rows[0]["CampusId"]);
            user.Enrollment.Id = Convert.ToInt32(dataTableUserInfo.Rows[0]["EnrollmentId"]);
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
            using (SqlCommand insert = new SqlCommand(@"UpdateUser", connection))
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
    }
}