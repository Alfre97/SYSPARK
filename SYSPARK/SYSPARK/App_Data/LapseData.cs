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
        public DataTable DataTableEnrollmentLapse(int lapseId)
        {
            DataTable dataTableLapse = new DataTable();
            SqlConnection connection = ManageDatabaseConnection("Open");
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
            SqlConnection connection = ManageDatabaseConnection("Open");
            using (SqlCommand select = new SqlCommand(@"SelectLapseOn", connection))
            {
                select.CommandType = CommandType.StoredProcedure;
                SqlDataAdapter adap = new SqlDataAdapter(select);
                adap.Fill(dataTableLapseOn);
            }
            connection = ManageDatabaseConnection("Close");
            return dataTableLapseOn;
        }
    }
}