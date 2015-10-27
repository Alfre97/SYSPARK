using SYSPARK.App_BussinessRules;
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
            int insertResult = vehicleBussinessRules.InsertVehicle(createVehicle(), Convert.ToInt32(Session["User-Id"]));
            switch (insertResult)
            {
                case 0:
                    Session["VehicleInserted"] = "VehicleInserted";
                    Response.Redirect("Profile.aspx");
                    break;
                case 1:
                    buttonErrorsStyleWhite();
                    buttonErrors.Value = "The license field is empty.";
                    break;
                case 2:
                    buttonErrorsStyleRed();
                    buttonErrors.Value = "The license can only contain seven characters.";
                    break;
            }
        }

        protected Vehicle createVehicle()
        {
            //Creating the vehicle
            Vehicle vehicle = new Vehicle();
            VehicleType vehicleType = new VehicleType(); 
            vehicle.VehiclePlate = textboxLicense.Value;
            vehicleType.Id = Convert.ToInt32(selectType.Value);
            vehicleType.Description = selectType.Items.FindByValue(selectType.Value).Text;
            vehicle.Type = vehicleType;
            return vehicle;
        }

        protected void buttonErrorsStyleRed()
        {
            buttonErrors.Style.Add("background-color", "red");
            buttonErrors.Style.Add("color", "white");
        }

        protected void buttonErrorsStyleBlue()
        {
            buttonErrors.Style.Add("background-color", "blue");
            buttonErrors.Style.Add("color", "white");
        }

        protected void buttonErrorsStyleWhite()
        {
            buttonErrors.Style.Add("background-color", "white");
            buttonErrors.Style.Add("color", "red");
        }
    }
}