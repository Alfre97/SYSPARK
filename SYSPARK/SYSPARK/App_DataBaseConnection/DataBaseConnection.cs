using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace SYSPARK.DataBase
{
    public class DataBaseConnection
    {
        public SqlConnection ManageDatabaseConnection(String actionToPerform)
        {
            SqlConnection sqlConnection = new SqlConnection();
            sqlConnection.ConnectionString = @"Data Source=ALFREDO-PC\SQLEXPRESS;Initial Catalog=SYSPARKDB;Integrated Security=True;Pooling=False";
            //sqlConnection.ConnectionString = @"Server=tcp:syspark.database.windows.net,1433;Database=SYSPARKDB;User ID=syspark@syspark;Password=Alfredo1997avila;Trusted_Connection=False;Encrypt=True;Connection Timeout=60;";
            try
            {
                //Decision to weather open or close the database connection
                if (actionToPerform.Equals("Open"))
                {
                    sqlConnection.Open();
                }
                else
                {
                    sqlConnection.Close();
                }
            }
            catch (SqlException)
            {
                return null;
            }

            return sqlConnection;
        }
    }
}