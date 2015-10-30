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
    }
}