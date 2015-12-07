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
    public class SpaceData : DataBaseConnection
    {
        SqlConnection connection = new SqlConnection();

        public void InsertSpace(Space space, int initialInsertOrUpdate)
        {
            connection = ManageDatabaseConnection("Open");
            using (SqlCommand insert = new SqlCommand(@"InsertSpace", connection))
            {
                insert.CommandType = CommandType.StoredProcedure;
                insert.Parameters.Add("@ParkingId", SqlDbType.Int).Value = space.ParkingId;
                insert.Parameters.Add("@ParkingCampusId", SqlDbType.Int).Value = space.ParkingCampusId;
                insert.Parameters.Add("@ParkingName", SqlDbType.VarChar).Value = space.ParkingName;
                insert.Parameters.Add("@Name", SqlDbType.VarChar).Value = space.Name;
                insert.Parameters.Add("@SpaceTypeId", SqlDbType.Int).Value = space.SpaceType.Id;
                insert.Parameters.Add("@SpaceTypeName", SqlDbType.VarChar).Value = space.SpaceType.Name;
                insert.Parameters.Add("@Status", SqlDbType.Bit).Value = space.Status;
                insert.Parameters.Add("@InitialInsertOrUpdate", SqlDbType.Int).Value = initialInsertOrUpdate;
                insert.ExecuteNonQuery();
            }
            connection = ManageDatabaseConnection("Close");
        }

        public DataTable DataTableParkingSpace(int campusId, int parkingId)
        {
            DataTable dataTableParkingSpace = new DataTable();
            connection = ManageDatabaseConnection("Open");
            using (SqlCommand select = new SqlCommand(@"SelectParkingSpace", connection))
            {
                select.CommandType = CommandType.StoredProcedure;

                SqlDataAdapter adap = new SqlDataAdapter(select);
                select.Parameters.Add("@CampusId", SqlDbType.Int).Value = campusId;
                select.Parameters.Add("@ParkingId", SqlDbType.Int).Value = parkingId;
                adap.Fill(dataTableParkingSpace);
            }
            connection = ManageDatabaseConnection("Close");
            return dataTableParkingSpace;
        }

        public DataTable DataTableParkingTypeSpace(int parkingId, int spaceTypeId)
        {
            DataTable dataTableParkingSpace = new DataTable();
            connection = ManageDatabaseConnection("Open");
            using (SqlCommand select = new SqlCommand(@"SelectParkingTypeSpace", connection))
            {
                select.CommandType = CommandType.StoredProcedure;
                SqlDataAdapter adap = new SqlDataAdapter(select);
                select.Parameters.Add("@ParkingId", SqlDbType.Int).Value = parkingId;
                select.Parameters.Add("@SpaceTypeId", SqlDbType.Int).Value = spaceTypeId;
                adap.Fill(dataTableParkingSpace);
            }
            connection = ManageDatabaseConnection("Close");
            return dataTableParkingSpace;
        }

        public void DeleteSpace(int spaceId, int parkingId, int spaceTypeId)
        {
            connection = ManageDatabaseConnection("Open");
            using (SqlCommand delete = new SqlCommand(@"DeleteSpace", connection))
            {
                delete.CommandType = CommandType.StoredProcedure;
                delete.Parameters.Add("@SpaceId", SqlDbType.Int).Value = spaceId;
                delete.Parameters.Add("@ParkingId", SqlDbType.Int).Value = parkingId;
                delete.Parameters.Add("@SpaceTypeId", SqlDbType.Int).Value = spaceTypeId;
                delete.ExecuteNonQuery();
            }
            connection = ManageDatabaseConnection("Close");
        }

    }
}