using SYSPARK.App_Entities;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace SYSPARK.App_BussinessRules
{
    public class LapseBussinessRules
    {
        LapseData lapseData = new LapseData();

        public int InsertLapse(Lapse lapse)
        {
            try
            {
                if (lapse.Name.Equals(string.Empty))
                    return 1;
                else if (lapse.InitialDate.Equals(string.Empty))
                    return 3;
                else if (lapse.FinalDate.Equals(string.Empty))
                    return 4;
                else if (lapse.Status.Equals(string.Empty))
                    return 5;
                else if (lapse.InitialDate > lapse.FinalDate)
                    return 6;
                else if (lapse.InitialDate < DateTime.Today)
                    return 7;
                else
                {
                    lapseData.InsertLapse(lapse);
                    return 0;
                }
            }
            catch (SqlException)
            {
                return 2;
            }
        }

        public int DeleteLapse(int lapseId)
        {
            try
            {
                if (lapseId.Equals(string.Empty))
                    return 2;
                else
                {
                    lapseData.DeleteLapse(lapseId);
                    return 0;
                }
            }
            catch (SqlException)
            {
                return 1;
            }
        }

        public int UpdateLapse(Lapse lapse)
        {
            try
            {
                if (lapse.Name.Equals(string.Empty))
                    return 1;
                else if (lapse.InitialDate.Equals(string.Empty))
                    return 3;
                else if (lapse.FinalDate.Equals(string.Empty))
                    return 4;
                else if (lapse.Status.Equals(string.Empty))
                    return 5;
                else if (lapse.InitialDate > lapse.FinalDate)
                    return 6;
                else if (lapse.InitialDate < DateTime.Today)
                    return 7;
                else
                {
                    lapseData.UpdateLapse(lapse);
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