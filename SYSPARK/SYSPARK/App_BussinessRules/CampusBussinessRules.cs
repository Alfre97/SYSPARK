using SYSPARK.App_Entities;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace SYSPARK.App_BussinessRules
{
    public class CampusBussinessRules
    {
        CampusData campusData = new CampusData();

        public int InsertCampus(Campus campus)
        {
            try
            {
                if (campus.Name.Equals(string.Empty))
                    return 1;
                else
                {
                    campusData.InsertCampus(campus);
                    return 0;
                }
            }
            catch (SqlException)
            {
                return 2;
            }
        }

        public int DeleteCampus(int campusId)
        {
            try
            {
                if (campusId.Equals(string.Empty))
                    return 2;
                else
                {
                    campusData.DeleteCampus(campusId);
                    return 0;
                }
            }
            catch (SqlException)
            {
                return 1;
            }
        }

        public int UpdateCampus(Campus campus)
        {

            try
            {
                if (campus.Name.Equals(string.Empty))
                {
                    return 1;
                }
                else
                {
                    campusData.UpdateCampus(campus);
                    return 0;
                }
            }
            catch (SqlException)
            {
                return 2;
            }
        }

    }
}