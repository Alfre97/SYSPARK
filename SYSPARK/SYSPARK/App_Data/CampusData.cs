using SYSPARK.App_Entities;
using SYSPARK.DataBase;
using SYSPARK.Entities;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace SYSPARK
{
    public class CampusData : DataBaseConnection
    {
        SqlConnection connection = new SqlConnection();

        public DataTable DataTableCampus()
        {
            DataTable dataTableCampus = new DataTable();
            connection = ManageDatabaseConnection("Open");
            using (SqlCommand select = new SqlCommand(@"SelectCampus", connection))
            {
                select.CommandType = CommandType.StoredProcedure;
                SqlDataAdapter adap = new SqlDataAdapter(select);
                adap.Fill(dataTableCampus);
            }
            connection = ManageDatabaseConnection("Close");
            return dataTableCampus;
        }

        public DataTable GetUserCampus(string userName)
        {
            DataTable dataTableUserCampus = new DataTable();
            connection = ManageDatabaseConnection("Open");
            using (SqlCommand select = new SqlCommand(@"SelectUserCampus", connection))
            {
                select.CommandType = CommandType.StoredProcedure;
                select.Parameters.Add("@UserName", SqlDbType.VarChar).Value = userName;
                SqlDataAdapter adap = new SqlDataAdapter(select);
                adap.Fill(dataTableUserCampus);
            }
            connection = ManageDatabaseConnection("Close");
            return dataTableUserCampus;
        }

        public List<int> SendCampusList(DataTable dataTableUserCampus)
        {
            List<int> campusList = new List<int>();
            for (int i = 0; i < dataTableUserCampus.Rows.Count; i++)
            {
                campusList.Add(Convert.ToInt32(dataTableUserCampus.Rows[i]["CampusId"]));
            }
            return campusList;
        }

        public DataTable DataTableUserCampus(List<int> campusList)
        {
            DataTable dataTableUserCampus = new DataTable();

            for (int i = 0; i < campusList.Count; i++)
            {
                using (SqlCommand select = new SqlCommand(@"SelectOneCampus", connection))
                {
                    connection = ManageDatabaseConnection("Open");

                    select.CommandType = CommandType.StoredProcedure;
                    select.Parameters.Add("@CampusId", SqlDbType.Int).Value = campusList[i];
                    SqlDataAdapter adap = new SqlDataAdapter(select);
                    adap.Fill(dataTableUserCampus);

                    connection = ManageDatabaseConnection("Close");
                }
            }
            return dataTableUserCampus;
        }

        public void InsertCampus(Campus campus)
        {
            SqlConnection connection = ManageDatabaseConnection("Open");
            using (SqlCommand insert = new SqlCommand(@"InsertCampus", connection))
            {
                insert.CommandType = CommandType.StoredProcedure;
                insert.Parameters.Add("@Name", SqlDbType.VarChar).Value = campus.Name;
                insert.ExecuteNonQuery();
            }
            connection = ManageDatabaseConnection("Close");
        }

        public void DeleteCampus(int campusId)
        {
            SqlConnection connection = ManageDatabaseConnection("Open");
            using (SqlCommand delete = new SqlCommand(@"DeleteCampus", connection))
            {
                delete.CommandType = CommandType.StoredProcedure;
                delete.Parameters.Add("@Id", SqlDbType.Int).Value = campusId;
                delete.ExecuteNonQuery();
            }
            connection = ManageDatabaseConnection("Close");
        }

        public void UpdateCampus(Campus campus)
        {
            connection = ManageDatabaseConnection("Open");
            using (SqlCommand update = new SqlCommand(@"UpdateCampus", connection))
            {
                update.CommandType = CommandType.StoredProcedure;
                update.Parameters.Add("@Id", SqlDbType.Int).Value = campus.Id;
                update.Parameters.Add("@Name", SqlDbType.VarChar).Value = campus.Name;
                update.ExecuteNonQuery();
            }
            connection = ManageDatabaseConnection("Close");
        }

    }
}