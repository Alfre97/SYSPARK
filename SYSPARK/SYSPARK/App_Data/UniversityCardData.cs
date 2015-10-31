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
    public class UniversityCardData : DataBaseConnection
    {
        public DataTable getCard(UniversityCard universityCard)
        {
            DataTable dataTableCode = new DataTable();
            SqlConnection connection = ManageDatabaseConnection("Open");
            using (SqlCommand select = new SqlCommand(@"SelectUniversityCard", connection))
            {
                select.CommandType = CommandType.StoredProcedure;
                select.Parameters.Add("@UniversityCard", SqlDbType.VarChar).Value = universityCard.Card;
                SqlDataAdapter adap = new SqlDataAdapter(select);
                adap.Fill(dataTableCode);
                connection = ManageDatabaseConnection("Close");
                return dataTableCode;
            }
        }

        public void UpdateCard(UniversityCard universityCard)
        {
            SqlConnection connection = ManageDatabaseConnection("Open");
            using (SqlCommand update = new SqlCommand(@"SelectUniversityCard", connection))
            {
                update.CommandType = CommandType.StoredProcedure;
                update.Parameters.Add("@UniversityCard", SqlDbType.VarChar).Value = universityCard.Card;
                update.Parameters.Add("@Used", SqlDbType.Bit).Value = universityCard.Used;
                update.Parameters.Add("@UserId", SqlDbType.Int).Value = universityCard.UserId;
                update.ExecuteNonQuery();
                connection = ManageDatabaseConnection("Close");
            }
        }
    }
}