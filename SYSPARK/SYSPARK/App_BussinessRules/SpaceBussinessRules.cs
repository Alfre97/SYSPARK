using SYSPARK.Entities;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace SYSPARK.App_BussinessRules
{
    public class SpaceBussinessRules
    {
        SpaceData spaceData = new SpaceData();

        public int InsertParkingSpace(List<Space> spaceList)
        {
            try
            {
                foreach (Space space in spaceList)
                {
                    spaceData.InsertSpace(space, 1);
                }
                return 0;
            }
            catch (SqlException)
            {
                return 1;
            }
        }

        public int DeleteSpace(int spaceId, int parkingId, int spaceTypeId)
        {
            try
            {
                if (spaceId.Equals(string.Empty))
                    return 2;
                else
                {
                    spaceData.DeleteSpace(spaceId, parkingId, spaceTypeId);
                    return 0;
                }
            }
            catch (SqlException)
            {
                return 1;
            }
        }
    }
}