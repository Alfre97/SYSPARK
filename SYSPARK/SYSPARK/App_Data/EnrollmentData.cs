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
    public class EnrollmentData : DataBaseConnection
    {
        public void InsertEnrollment(Enrollment enrollment)
        {
            SqlConnection connection = ManageDatabaseConnection("Open");
            using (SqlCommand insert = new SqlCommand(@"InsertEnrollment", connection))
            {
                insert.CommandType = CommandType.StoredProcedure;
                insert.Parameters.Add("@InitialDate", SqlDbType.Date).Value = enrollment.InitialDate;
                insert.Parameters.Add("@FinalDate", SqlDbType.Date).Value = enrollment.FinalDate;
                insert.Parameters.Add("@Status", SqlDbType.Date).Value = enrollment.Status;
                insert.ExecuteNonQuery();
            }
            connection = ManageDatabaseConnection("Close");
        }
    }
}