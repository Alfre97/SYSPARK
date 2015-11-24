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
        public void InsertSpace(List<Space> spaceList)
        {
            SqlConnection connection = ManageDatabaseConnection("Open");

            foreach (Space space in spaceList)
            {
                using (SqlCommand insert = new SqlCommand(@"InsertSpace", connection))
                {
                    insert.CommandType = CommandType.StoredProcedure;
                    insert.Parameters.Add("@Name", SqlDbType.VarChar).Value = space.Name;
                    insert.Parameters.Add("@ParkingId", SqlDbType.Int).Value = space.ParkingId;
                    insert.Parameters.Add("@TypeId", SqlDbType.Int).Value = space.SpaceType.Id;
                    insert.Parameters.Add("@TypeName", SqlDbType.VarChar).Value = space.SpaceType.Name;
                    insert.ExecuteNonQuery();
                }
            }
            connection = ManageDatabaseConnection("Close");
        }
    }
}