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
            try
            {
                foreach (Space space in spaceList)
                {
                    using (SqlCommand insert = new SqlCommand(@"InsertSpace", connection))
                    {
                        insert.CommandType = CommandType.StoredProcedure;
                        insert.Parameters.Add("@Name", SqlDbType.VarChar).Value = space.Name;
                        insert.Parameters.Add("@ParkingName", SqlDbType.Int).Value = space.ParkingName;
                        insert.Parameters.Add("@Type", SqlDbType.Int).Value = space.Type;
                        insert.ExecuteNonQuery();
                    }
                }
                connection = ManageDatabaseConnection("Close");
            }
            catch (SqlException)
            {
                connection = ManageDatabaseConnection("Close");
            }
        }

        public List<Space> createListCarSpace(string parkingName, int carSpace)
        {
            List<Space> spaceList = new List<Space>();
            for (int i = 1; i < carSpace; i++)
            {
                Space space = new Space();
                space.Name = parkingName + "-" + i;
                space.ParkingName = parkingName;
                space.Type = "Car";
                spaceList.Add(space);
            }
            return spaceList;
        }

        public List<Space> createListMotorcycleSpace(string parkingName, int motorcycleSpace)
        {
            List<Space> spaceList = new List<Space>();
            for (int i = 1; i < motorcycleSpace; i++)
            {
                Space space = new Space();
                space.Name = parkingName + "-" + i;
                space.ParkingName = parkingName;
                space.Type = "Motorcyle";
                spaceList.Add(space);
            }
            return spaceList;
        }

        public List<Space> createListHandicapSpace(string parkingName, int handicapSpace)
        {
            List<Space> spaceList = new List<Space>();

            for (int i = 1; i < handicapSpace; i++)
            {
                Space space = new Space();
                space.Name = parkingName + "-" + i;
                space.ParkingName = parkingName;
                space.Type = "Handicap";
                spaceList.Add(space);
            }
            return spaceList;
        }

        public List<Space> createListBusSpace(string parkingName, int busSpace)
        {
            List<Space> spaceList = new List<Space>();

            for (int i = 1; i < busSpace; i++)
            {
                Space space = new Space();
                space.Name = parkingName + "-" + i;
                space.ParkingName = parkingName;
                space.Type = "Bus";
                spaceList.Add(space);
            }
            return spaceList;
        }

        public List<Space> createSpaceList(List<Space> listCarSpace, List<Space> motorcycleSpace, List<Space> handicapSpace, List<Space> busSpace)
        {
            List<Space> allSpaceList = new List<Space>();
            foreach (Space space in listCarSpace)
                allSpaceList.Add(space);
            foreach (Space space in motorcycleSpace)
                allSpaceList.Add(space);
            foreach (Space space in handicapSpace)
                allSpaceList.Add(space);
            foreach (Space space in busSpace)
                allSpaceList.Add(space);

            return allSpaceList;
        }
    }
}