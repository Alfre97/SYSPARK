using SYSPARK.App_BussinessRules;
using SYSPARK.App_Utility;
using SYSPARK.Data;
using SYSPARK.Entities;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SYSPARK
{
    public partial class AddNewCar : System.Web.UI.Page
    {
        ButtonStyle buttonStyle = new ButtonStyle();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["User-Id"] == null)
                Response.Redirect("Default.aspx");

            VehicleTypeData vehicleTypedata = new VehicleTypeData();
            DataTable condition = vehicleTypedata.DataTableAllVehicleType();
            selectType.DataSource = condition;
            selectType.DataTextField = "Description";
            selectType.DataValueField = "Id";
            selectType.DataBind();
        }

        protected void ButtonVehicle_Click(object sender, EventArgs e)
        {

            insertVehicle();

        }

        protected void insertVehicle()
        {
            VehicleTypeData vehicleTypeData = new VehicleTypeData();
            VehicleBussinessRules vehicleBussinessRules = new VehicleBussinessRules();
            Vehicle vehicle = createVehicle();
            if (vehicle != null)
            {
                int insertResult = vehicleBussinessRules.InsertVehicle(vehicle, Convert.ToInt32(Session["User-Id"]));
                switch (insertResult)
                {
                    case 0:
                        Session["VehicleInserted"] = "VehicleInserted";
                        Response.Redirect("Profile.aspx");
                        break;
                    case 1:
                        buttonStyle.buttonStyleWhite(buttonErrors, "The license field is empty.");
                        break;
                    case 2:
                        buttonStyle.buttonStyleRed(buttonErrors, "The license can only contain seven characters.");
                        break;
                }
            }
            else
            {
                buttonStyle.buttonStyleRed(buttonErrors, "Invalid data, please check it.");
            }
        }

        protected Vehicle createVehicle()
        {
            try
            {
                //Creating the vehicle
                Vehicle vehicle = new Vehicle();
                VehicleType vehicleType = new VehicleType();
                vehicle.VehiclePlate = textboxLicense.Value;
                vehicleType.Id = Convert.ToInt32(hiddenTypeValue.Value);
                vehicleType.Description = selectType.Items.FindByValue(hiddenTypeValue.Value).Text;
                vehicle.Type = vehicleType;
                return vehicle;
            }
            catch (FormatException)
            {
                return null;
            }
        }
    }
}