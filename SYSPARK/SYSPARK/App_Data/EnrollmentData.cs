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
    public class EnrollmentData : DataBaseConnection
    {
        public DataTable DataTableUserEnrollment(string enrollmentUniqueIdentifier)
        {
            DataTable dataTableEnrollment = new DataTable();
            SqlConnection connection = ManageDatabaseConnection("Open");
            using (SqlCommand select = new SqlCommand(@"SelectUserEnrollment", connection))
            {
                select.CommandType = CommandType.StoredProcedure;
                select.Parameters.Add("@EnrollmentUniqueIdentifier", SqlDbType.VarChar).Value = enrollmentUniqueIdentifier;
                SqlDataAdapter adap = new SqlDataAdapter(select);
                adap.Fill(dataTableEnrollment);
            }
            connection = ManageDatabaseConnection("Close");
            return dataTableEnrollment;
        }

        public Enrollment SendUserEnrollment(DataTable dataTableEnrollment)
        {
            Enrollment enrollment = new Enrollment();
            enrollment.UniqueIdentifier = dataTableEnrollment.Rows[0]["UniqueIdentifier"].ToString();
            enrollment.Lapse.Id = Convert.ToInt32(dataTableEnrollment.Rows[0]["LapseId"]);
            return enrollment;
        }

        public void InsertEnrollment(Enrollment enrollment)
        {
            SqlConnection connection = ManageDatabaseConnection("Open");
            using (SqlCommand insert = new SqlCommand(@"InsertEnrollment", connection))
            {
                insert.CommandType = CommandType.StoredProcedure;
                insert.Parameters.Add("@UniqueIdentifier", SqlDbType.Int).Value = enrollment.UniqueIdentifier;
                insert.Parameters.Add("@LapseId", SqlDbType.Int).Value = enrollment.Lapse.Id;
                insert.ExecuteNonQuery();
            }
            connection = ManageDatabaseConnection("Close");
        }

        public void UpdateEnrollment(Enrollment enrollment)
        {
            SqlConnection connection = ManageDatabaseConnection("Open");
            using (SqlCommand update = new SqlCommand(@"UpdateEnrollment", connection))
            {
                update.CommandType = CommandType.StoredProcedure;
                update.Parameters.Add("@UniqueIdentifier", SqlDbType.VarChar).Value = enrollment.UniqueIdentifier;
                update.Parameters.Add("@LapseId", SqlDbType.Int).Value = enrollment.Lapse.Id;
                update.ExecuteNonQuery();
            }
            connection = ManageDatabaseConnection("Close");
        }
    }
}