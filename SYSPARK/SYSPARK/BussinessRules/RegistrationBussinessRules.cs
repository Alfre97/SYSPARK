using SYSPARK.Data;
using SYSPARK.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SYSPARK.BussinessRules
{
    public class RegistrationBussinessRules
    {
        public void RegistrationRules(User user, Vehicle vehicle)
        {
            try
            {
                RegistrationData registrationData = new RegistrationData();
                registrationData.InsertUser(user);
                registrationData.InsertVehicle(vehicle);
                registrationData.InsertUserVehicle(user, vehicle);
            }
            catch (Exception)
            {
                
            }

        }

    }
}