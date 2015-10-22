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
    public partial class AddNewCar : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            VehicleTypeData vehicleTypedata = new VehicleTypeData();
            DataTable condition = vehicleTypedata.DataTableAllVehicleType();
            selectType.DataSource = condition;
            selectType.DataTextField = "Description";
            selectType.DataValueField = "Id";
            selectType.DataBind();
        }

        protected void ButtonVehicle_Click(object sender, EventArgs e)
        {
            //Creating th vehicle
            Vehicle vehicle = new Vehicle();
            vehicle.License = textboxLicense.Value;
            VehicleType vehicleType = new VehicleType();
            VehicleTypeData vehicleTypeData = new VehicleTypeData();
            vehicleType.Id = Convert.ToInt32(selectType.Value);
            vehicleType.Description = selectType.Items.FindByValue(selectType.Value).Text;
            vehicle.Type = vehicleType;
            try
            {
                //Inserting vehicle
                VehicleData vehicleData = new VehicleData();
                vehicleData.InsertVehicle(vehicle);
                UserVehicleData userVehicleData = new UserVehicleData();
                User user = new User();
                user.Id = Convert.ToInt32(Session["User-Id"]);
                userVehicleData.InsertUserVehicle(user, vehicle);
            }
            catch (FormatException)
            {
                buttonErrors.Style.Add("background-color", "white");
                buttonErrors.Value = "Please, provided a valid lincense.";
            }
        }
    }
}