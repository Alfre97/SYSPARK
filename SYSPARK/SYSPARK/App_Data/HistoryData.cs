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
    public class HistoryData : DataBaseConnection
    {
        SqlConnection connection = new SqlConnection();

        public DataTable DataTableUserHistory(string userName, int campusId)
        {
            DataTable dataTableUserHistory = new DataTable();
            connection = ManageDatabaseConnection("Open");
            using (SqlCommand select = new SqlCommand(@"SelectUserHistory", connection))
            {
                select.CommandType = CommandType.StoredProcedure;
                select.Parameters.Add("@UserName", SqlDbType.VarChar).Value = userName;
                select.Parameters.Add("@CampusId", SqlDbType.Int).Value = campusId;
                SqlDataAdapter adap = new SqlDataAdapter(select);
                adap.Fill(dataTableUserHistory);
            }
            connection = ManageDatabaseConnection("Close");
            return dataTableUserHistory;
        }

        public DataTable DataTableHistory(int campusId)
        {
            DataTable dataTableHistory = new DataTable();
            connection = ManageDatabaseConnection("Open");
            using (SqlCommand select = new SqlCommand(@"SelectHistory", connection))
            {
                select.CommandType = CommandType.StoredProcedure;
                select.Parameters.Add("@CampusId", SqlDbType.Int).Value = campusId;
                SqlDataAdapter adap = new SqlDataAdapter(select);
                adap.Fill(dataTableHistory);
            }
            connection = ManageDatabaseConnection("Close");
            return dataTableHistory;
        }
    }
}