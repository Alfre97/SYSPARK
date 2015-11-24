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
    }
}