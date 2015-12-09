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
    public class Jobs : DataBaseConnection
    {
        SqlConnection connection = new SqlConnection();

        public void DeleteInactiveReservation()
        {
            try
            {
                connection = ManageDatabaseConnection("Open");
                using (SqlCommand delete = new SqlCommand(@"DeleteInactiveReservation", connection))
                {
                    delete.CommandType = CommandType.StoredProcedure;
                    delete.ExecuteNonQuery();
                }
                connection = ManageDatabaseConnection("Close");
            }
            catch (SqlException)
            {
                
            }

        }

    }
}