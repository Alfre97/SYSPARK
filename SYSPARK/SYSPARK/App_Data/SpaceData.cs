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
        public List<Space> spaceList(Parking parking)
        {
            List<Space> spaceList = new List<Space>();
            
            foreach (Space space in createListCarSpace(parking))
                spaceList.Add(space);
            foreach (Space space in createListMotorcycleSpace(parking))
                spaceList.Add(space);
            foreach (Space space in createListHandicapSpace(parking))
                spaceList.Add(space);
            foreach (Space space in createListBusSpace(parking))
                spaceList.Add(space);

            return spaceList;
        }

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
                    insert.Parameters.Add("@Type", SqlDbType.VarChar).Value = space.Type;
                    insert.ExecuteNonQuery();
                }
            }
            connection = ManageDatabaseConnection("Close");
        }

        public List<Space> createListCarSpace(Parking parking)
        {
            List<Space> spaceList = new List<Space>();
            Space space = new Space();
            for (int i = 1; i <= parking.CarSpace; i++)
            {
                space.Name = parking.Name + "-" + i + "-Car";
                space.ParkingId = parking.Id;
                space.Type = "Car";
                spaceList.Add(space);
            }
            return spaceList;
        }

        public List<Space> createListMotorcycleSpace(Parking parking)
        {
            List<Space> spaceList = new List<Space>();
            Space space = new Space();
            for (int i = 1; i <= parking.MotorcycleSpace; i++)
            {
                space.Name = parking.Name + "-" + i + "-Motorcycle";
                space.ParkingId = parking.Id;
                space.Type = "Motorcyle";
                spaceList.Add(space);
            }
            return spaceList;
        }

        public List<Space> createListHandicapSpace(Parking parking)
        {
            List<Space> spaceList = new List<Space>();
            Space space = new Space();
            for (int i = 1; i <= parking.HandicapSpace; i++)
            {
                space.Name = parking.Name + "-" + i + "-Handicap";
                space.ParkingId = parking.Id;
                space.Type = "Handicap";
                spaceList.Add(space);
            }
            return spaceList;
        }

        public List<Space> createListBusSpace(Parking parking)
        {
            List<Space> spaceList = new List<Space>();
            Space space = new Space();
            for (int i = 1; i <= parking.BusSpace; i++)
            {
                space.Name = parking.Name + "-" + i + "-Bus";
                space.ParkingId = parking.Id;
                space.Type = "Bus";
                spaceList.Add(space);
            }
            return spaceList;
        }
    }
}