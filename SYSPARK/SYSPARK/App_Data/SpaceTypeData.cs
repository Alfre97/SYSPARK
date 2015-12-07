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
    public class SpaceTypeData : DataBaseConnection
    {
        SqlConnection connection = new SqlConnection();

        public DataTable DataTableSpaceType()
        {
            DataTable dataTableSpaceType = new DataTable();
            SqlConnection connection = ManageDatabaseConnection("Open");
            using (SqlCommand select = new SqlCommand(@"SelectSpaceType", connection))
            {
                select.CommandType = CommandType.StoredProcedure;
                SqlDataAdapter adap = new SqlDataAdapter(select);
                adap.Fill(dataTableSpaceType);
            }
            connection = ManageDatabaseConnection("Close");
            return dataTableSpaceType;
        }

        public void InsertSpaceType(SpaceType spaceType)
        {
            connection = ManageDatabaseConnection("Open");
            using (SqlCommand insert = new SqlCommand(@"InsertSpaceType", connection))
            {
                insert.CommandType = CommandType.StoredProcedure;
                insert.Parameters.Add("@Name", SqlDbType.VarChar).Value = spaceType.Name;
                insert.ExecuteNonQuery();
            }
            connection = ManageDatabaseConnection("Close");
        }
    }
}