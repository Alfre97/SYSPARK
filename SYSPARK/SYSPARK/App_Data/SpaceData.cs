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
                        insert.Parameters.Add("@ParkingId", SqlDbType.Int).Value = space.Id;
                        insert.Parameters.Add("@Type", SqlDbType.VarChar).Value = space.Type;
                        insert.ExecuteNonQuery();
                    }
                }
                connection = ManageDatabaseConnection("Close");
        }

        public List<Space> createListCarSpace(Parking parking)
        {
            List<Space> spaceList = new List<Space>();
            for (int i = 1; i <= parking.CarSpace; i++)
            {
                Space space = new Space();
                space.Name = parking.Name + "-" + i;
                space.ParkingId = parking.Id;
                space.Type = "Car";
                spaceList.Add(space);
            }
            return spaceList;
        }

        public List<Space> createListMotorcycleSpace(Parking parking)
        {
            List<Space> spaceList = new List<Space>();
            for (int i = 1; i <= parking.MotorcycleSpace; i++)
            {
                Space space = new Space();
                space.Name = parking.Name + "-" + i;
                space.ParkingId = parking.Id;
                space.Type = "Motorcyle";
                spaceList.Add(space);
            }
            return spaceList;
        }

        public List<Space> createListHandicapSpace(Parking parking)
        {
            List<Space> spaceList = new List<Space>();

            for (int i = 1; i <= parking.HandicapSpace; i++)
            {
                Space space = new Space();
                space.Name = parking.Name + "-" + i;
                space.ParkingId = parking.Id;
                space.Type = "Handicap";
                spaceList.Add(space);
            }
            return spaceList;
        }

        public List<Space> createListBusSpace(Parking parking)
        {
            List<Space> spaceList = new List<Space>();

            for (int i = 1; i <= parking.BusSpace; i++)
            {
                Space space = new Space();
                space.Name = parking.Name + "-" + i;
                space.ParkingId = parking.Id;
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

        public void deleteSpace(string parkingName)
        {
            SqlConnection connection = ManageDatabaseConnection("Open");
            using (SqlCommand delete = new SqlCommand(@"DeleteSpace", connection))
            {
                delete.CommandType = CommandType.StoredProcedure;
                delete.Parameters.Add("@ParkingName", SqlDbType.VarChar).Value = parkingName;
                delete.ExecuteNonQuery();
            }
            connection = ManageDatabaseConnection("Close");
        }
    }
}