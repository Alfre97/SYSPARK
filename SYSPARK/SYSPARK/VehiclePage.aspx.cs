using SYSPARK.App_BussinessRules;
using SYSPARK.App_Utility;
using SYSPARK.Data;
using SYSPARK.Entities;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SYSPARK
{
    public partial class VehiclePage1 : System.Web.UI.Page
    {
        VehicleBussinessRules vehicleRules = new VehicleBussinessRules();
        ButtonStyle buttonStyle = new ButtonStyle();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["User-UserName"] == null)
                Response.Redirect("Default.aspx");

            FillVehicleType();
            FillTable();
        }

        protected void FillVehicleType()
        {
            VehicleTypeData vehicleTypedata = new VehicleTypeData();
            DataTable condition = vehicleTypedata.DataTableAllVehicleType();
            selectType.DataSource = condition;
            selectType.DataTextField = "Name";
            selectType.DataValueField = "Id";
            selectType.DataBind();
        }

        protected void ButtonVehicle_Click(object sender, EventArgs e)
        {
            InsertVehicle(CreateVehicle());
        }

        protected void InsertVehicle(Vehicle vehicle)
        {
            VehicleTypeData vehicleTypeData = new VehicleTypeData();
            VehicleBussinessRules vehicleBussinessRules = new VehicleBussinessRules();

            if (vehicle != null)
            {
                int insertResult = vehicleBussinessRules.InsertVehicle(vehicle, Session["User-UserName"].ToString());
                switch (insertResult)
                {
                    case 0:
                        buttonStyle.buttonStyleBlue(buttonErrors, "Vehicle added sucessful.");
                        FillTable();
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

        protected Vehicle CreateVehicle()
        {
            try
            {
                //Creating the vehicle
                Vehicle vehicle = new Vehicle();
                VehicleType vehicleType = new VehicleType();
                vehicle.VehiclePlate = textboxLicense.Value;
                if (hiddenTypeValue.Value.Equals(string.Empty))
                {
                    vehicleType.Id = Convert.ToInt32(selectType.Items[0].Value);
                    vehicleType.Name = selectType.Items[0].Text;
                }
                else
                {
                    vehicleType.Id = Convert.ToInt32(hiddenTypeValue.Value);
                    vehicleType.Name = selectType.Items.FindByValue(hiddenTypeValue.Value).Text;
                }
                vehicle.Type = vehicleType;
                return vehicle;
            }
            catch (FormatException)
            {
                return null;
            }
        }

        protected void FillTable()
        {
            VehicleData vehicleData = new VehicleData();
            //Populating a DataTable from database.
            DataTable dt = vehicleData.DataTableVehicle();

            //Building an HTML string.
            StringBuilder html = new StringBuilder();

            //Building the Header row.
            html.Append("<tbody>");
            html.Append("<tr>");
            foreach (DataColumn column in dt.Columns)
            {
                html.Append("<th>");
                html.Append(column.ColumnName);
                html.Append("</th>");
            }
            html.Append("</tr>");

            //Building the Data rows.
            foreach (DataRow row in dt.Rows)
            {
                html.Append("<tr class='desmarcado'>");
                foreach (DataColumn column in dt.Columns)
                {
                    html.Append("<td onclick='getValue(this.parentNode)' style='cursor:pointer'>");
                    html.Append(row[column.ColumnName]);
                    html.Append("</td>");
                }
                html.Append("<td>");
                html.Append("<button onclick='setValues(this.parentNode.parentNode)' type='button'>Edit</button>");
                html.Append("</td>");
                html.Append("<td>");
                html.Append("<button onclick='deleteRole()' type='button'>Delete</button>");
                html.Append("</td>");
                html.Append("</tr>");
            }
            html.Append("</tbody>");

            //Append the HTML string to Placeholder.
            placeHolderTableVehicle.Controls.Clear();
            placeHolderTableVehicle.Controls.Add(new Literal { Text = html.ToString() });
        }

        protected void Delete_Click(object sender, EventArgs e)
        {
            if (hiddenVehiclePlate.Value != string.Empty)
            {
                DeleteVehicle();
                FillTable();
            }
            else
                buttonStyle.buttonStyleRed(buttonInfoVehicleTable, "Please, select a vehicle to delete.");

        }

        protected void DeleteVehicle()
        {
            switch (vehicleRules.DeleteVehicle(hiddenVehiclePlate.Value, Session["User-UserName"].ToString()))
            {
                case 0:
                    FillTable();
                    buttonStyle.buttonStyleBlue(buttonInfoVehicleTable, "Vehicle deleted successful.");
                    break;
                case 1:
                    FillTable();
                    buttonStyle.buttonStyleRed(buttonInfoVehicleTable, "This vehicle has linked data can't be deleted.");
                    break;
                case 2:
                    buttonStyle.buttonStyleWhite(buttonInfoVehicleTable, "Please, select a vehicle to delete.");
                    break;
            }
        }

        protected void Update_Click(object sender, EventArgs e)
        {
            Vehicle vehicle = CreateVehicle();
            vehicle.VehiclePlate = hiddenVehiclePlate.Value;
            UpdateVehicle(vehicle);
            buttonClear.Style.Add("visibility", "visible");
            buttonAddNewCar.Style.Add("visibility", "visible");
            buttonUpdate.Style.Add("visibility", "hidden");
            FillTable();
        }

        protected void UpdateVehicle(Vehicle vehicle)
        {
            if (vehicle != null)
            {
                switch (vehicleRules.UpdateVehicle(vehicle))
                {
                    case 0:
                        textboxLicense.Value = "";
                        buttonStyle.buttonStyleBlue(buttonErrors, "Vehicle updated successful.");
                        break;
                    case 1:
                        buttonStyle.buttonStyleWhite(buttonErrors, "Vehicle plate field is empty.");
                        break;
                    case 2:
                        buttonStyle.buttonStyleRed(buttonErrors, "An error ocurred updating the vehicle, please check data or contact we us.");
                        break;
                }
            }
        }
    }
}