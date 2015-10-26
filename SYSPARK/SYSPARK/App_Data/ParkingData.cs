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
    public class ParkingData : DataBaseConnection
    {
        public DataTable DataTableParking()
        {
            DataTable dataTableCondition = new DataTable();
            SqlConnection connection = ManageDatabaseConnection("Open");
            try
            {
                using (SqlCommand selectCondition = new SqlCommand(@"SelectParking", connection))
                {
                    selectCondition.CommandType = CommandType.StoredProcedure;
                    SqlDataAdapter adap = new SqlDataAdapter(selectCondition);
                    adap.Fill(dataTableCondition);
                    connection = ManageDatabaseConnection("Close");
                    return dataTableCondition;
                }
            }
            catch
            {
                connection = ManageDatabaseConnection("Close");
                return dataTableCondition;
            }

        }

        public void InsertParking(Parking parking)
        {
            SqlConnection connection = ManageDatabaseConnection("Open");
            try
            {
                using (SqlCommand insert = new SqlCommand(@"InsertParking", connection))
                {
                    insert.CommandType = CommandType.StoredProcedure;
                    insert.Parameters.Add("@Name", SqlDbType.VarChar).Value = parking.Name;
                    insert.Parameters.Add("@TotalSpace", SqlDbType.Int).Value = parking.TotalSpace;
                    insert.Parameters.Add("@CarSpace", SqlDbType.Int).Value = parking.CarSpace;
                    insert.Parameters.Add("@MotorcycleSpace", SqlDbType.Int).Value = parking.MotorcycleSpace;
                    insert.Parameters.Add("@HandicapSpace", SqlDbType.Int).Value = parking.HandicapSpace;
                    insert.Parameters.Add("@BusSpace", SqlDbType.Int).Value = parking.BusSpace;
                    insert.ExecuteNonQuery();
                }
                connection = ManageDatabaseConnection("Close");
            }
            catch (SqlException)
            {
                connection = ManageDatabaseConnection("Close");
            }

        }
    }
}