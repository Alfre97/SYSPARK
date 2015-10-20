using SYSPARK.BussinessRules;
using SYSPARK.Data;
using SYSPARK.Entities;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SYSPARK
{
    public partial class Register : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            ConditionData conditionData = new ConditionData();
            DataSet dsSelectCondition = conditionData.DataSetCondition();
            selectCondition.DataSource = conditionData.DataSetCondition().Tables["Condition"].DefaultView;
            selectCondition.DataTextField = "Description";
            selectCondition.DataValueField = "Id";
            selectCondition.DataBind();

            VehicleTypeData vehicletTypeData = new VehicleTypeData();
            DataSet dsSelectType = vehicletTypeData.DataSetVehicleType();
            selectType.DataSource = vehicletTypeData.DataSetVehicleType().Tables["VehicleType"].DefaultView;
            selectType.DataTextField = "Description";
            selectType.DataValueField = "Id";
            selectType.DataBind();
        }
        
        protected void buttonRegister_Click(object sender, EventArgs e)
        {
            //User
            User user = new User();
            user.Name = textboxName.Value;
            user.LastName = textboxLastName.Value;
            user.Username = textboxUsername.Value;
            user.Password = textboxPassword.Value;
            //Vehicle
            Vehicle vehicle = new Vehicle();
            vehicle.Lisence = Convert.ToInt16(textboxVehicle.Value);
            //Vehicle type
            Entities.VehicleType type = new Entities.VehicleType();
            type.Description = selectType.DataTextField;
            vehicle.Type = type;
            //Creating a list of vehicles
            List<Vehicle> vehicleList = new List<Vehicle>();
            vehicleList.Add(vehicle);
            //Adding list of vehicles to user list<Vehicle>
            user.VehicleList = vehicleList;
            //Condition
            Condition condition = new Condition();
            condition.Description = selectCondition.DataTextField;
            //Inserting registration data
            RegistrationBussinessRules registration = new RegistrationBussinessRules();
            registration.RegistrationRules(user, vehicle);
        }
    }
}