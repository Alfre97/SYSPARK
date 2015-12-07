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
    public class LapseData : DataBaseConnection
    {
        SqlConnection connection = new SqlConnection();

        public DataTable DataTableLapse()
        {
            DataTable dataTableLapse = new DataTable();
            connection = ManageDatabaseConnection("Open");
            using (SqlCommand select = new SqlCommand(@"SelectLapse", connection))
            {
                select.CommandType = CommandType.StoredProcedure;
                SqlDataAdapter adap = new SqlDataAdapter(select);
                adap.Fill(dataTableLapse);
            }
            connection = ManageDatabaseConnection("Close");
            return dataTableLapse;
        }

        public DataTable DataTableEnrollmentLapse(int lapseId)
        {
            DataTable dataTableLapse = new DataTable();
            connection = ManageDatabaseConnection("Open");
            using (SqlCommand select = new SqlCommand(@"SelectEnrollmentLapse", connection))
            {
                select.CommandType = CommandType.StoredProcedure;
                select.Parameters.Add("@LapseId", SqlDbType.Int).Value = lapseId;
                SqlDataAdapter adap = new SqlDataAdapter(select);
                adap.Fill(dataTableLapse);
            }
            connection = ManageDatabaseConnection("Close");
            return dataTableLapse;
        }

        public Lapse SendLapse(DataTable dataTableLapse)
        {
            Lapse lapse = new Lapse();
            lapse.Id = Convert.ToInt32(dataTableLapse.Rows[0]["Id"]);
            lapse.Name = dataTableLapse.Rows[0]["Name"].ToString();
            lapse.InitialDate = Convert.ToDateTime(dataTableLapse.Rows[0]["InitialDate"]);
            lapse.FinalDate = Convert.ToDateTime(dataTableLapse.Rows[0]["FinalDate"]);
            lapse.Status = Convert.ToBoolean(dataTableLapse.Rows[0]["Status"]);
            return lapse;
        }

        public DataTable DataTableLapseOn()
        {
            DataTable dataTableLapseOn = new DataTable();
            connection = ManageDatabaseConnection("Open");
            using (SqlCommand select = new SqlCommand(@"SelectLapseOn", connection))
            {
                select.CommandType = CommandType.StoredProcedure;
                SqlDataAdapter adap = new SqlDataAdapter(select);
                adap.Fill(dataTableLapseOn);
            }
            connection = ManageDatabaseConnection("Close");
            return dataTableLapseOn;
        }

        public void InsertLapse(Lapse lapse)
        {
            connection = ManageDatabaseConnection("Open");
            using (SqlCommand insert = new SqlCommand(@"InsertLapse", connection))
            {
                insert.CommandType = CommandType.StoredProcedure;
                insert.Parameters.Add("@Name", SqlDbType.VarChar).Value = lapse.Name;
                insert.Parameters.Add("@InitialDate", SqlDbType.Date).Value = lapse.InitialDate;
                insert.Parameters.Add("@FinalDate", SqlDbType.Date).Value = lapse.FinalDate;
                insert.Parameters.Add("@Status", SqlDbType.Bit).Value = lapse.Status;
                insert.ExecuteNonQuery();
            }
            connection = ManageDatabaseConnection("Close");
        }

        public void DeleteLapse(int lapseId)
        {
            connection = ManageDatabaseConnection("Open");
            using (SqlCommand delete = new SqlCommand(@"DeleteLapse", connection))
            {
                delete.CommandType = CommandType.StoredProcedure;
                delete.Parameters.Add("@Id", SqlDbType.Int).Value = lapseId;
                delete.ExecuteNonQuery();
            }
            connection = ManageDatabaseConnection("Close");
        }

        public void UpdateLapse(Lapse lapse)
        {
            connection = ManageDatabaseConnection("Open");
            using (SqlCommand update = new SqlCommand(@"UpdateLapse", connection))
            {
                update.CommandType = CommandType.StoredProcedure;
                update.Parameters.Add("@Id", SqlDbType.Int).Value = lapse.Id;
                update.Parameters.Add("@Name", SqlDbType.VarChar).Value = lapse.Name;
                update.Parameters.Add("@InitialDate", SqlDbType.Date).Value = lapse.InitialDate;
                update.Parameters.Add("@FinalDate", SqlDbType.Date).Value = lapse.FinalDate;
                update.Parameters.Add("@Status", SqlDbType.Bit).Value = lapse.Status;

                update.ExecuteNonQuery();
            }
            connection = ManageDatabaseConnection("Close");
        }
    }
}