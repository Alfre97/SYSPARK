using SYSPARK.App_BussinessRules;
using SYSPARK.App_Utility;
using SYSPARK.Data;
using SYSPARK.Entities;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SYSPARK
{
    public partial class VehicleTypePage : System.Web.UI.Page
    {
        VehicleTypeBussinessRules vehicleTypeRules = new VehicleTypeBussinessRules();
        ButtonStyle buttonStyle = new ButtonStyle();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["User-Id"] == null)
                Response.Redirect("Default.aspx");

            FillTable();
        }

        protected void FillTable()
        {
            VehicleTypeData vehicleTypeData = new VehicleTypeData();
            //Populating a DataTable from database.
            DataTable dt = vehicleTypeData.DataTableAllVehicleType();

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
                html.Append("<button onclick='setValues(this.parentNode.parentNode)' type='button'>Editar</button>");
                html.Append("</td>");
                html.Append("<td>");
                html.Append("<button onclick='deleteRole()' type='button'>Eliminar</button>");
                html.Append("</td>");
                html.Append("</tr>");
            }
            html.Append("</tbody>");

            //Append the HTML string to Placeholder.
            placeHolderTableRole.Controls.Clear();
            placeHolderTableRole.Controls.Add(new Literal { Text = html.ToString() });
        }

        protected void AddVehicleType_Click(object sender, EventArgs e)
        {
            InsertVehicleType(CreateVehicleType());
            FillTable();
        }

        protected VehicleType CreateVehicleType()
        {
            VehicleType vehicleType = new VehicleType();
            try
            {
                vehicleType.Description = textboxVehicleType.Value;
                return vehicleType;
            }
            catch (FormatException)
            {
                buttonStyle.buttonStyleRed(buttonErrors, "Invalid data, please check it or contact with us.");
                return null;
            }
        }

        protected void InsertVehicleType(VehicleType vehicleType)
        {
            if (vehicleType != null)
            {
                switch (vehicleTypeRules.InsertVehicleType(vehicleType))
                {
                    case 0:
                        textboxVehicleType.Value = "";
                        buttonStyle.buttonStyleBlue(buttonErrors, "Vehicle type added successful.");
                        break;
                    case 1:
                        buttonStyle.buttonStyleWhite(buttonErrors, "Vehicle type name field is empty.");
                        break;
                    case 2:
                        buttonStyle.buttonStyleRed(buttonErrors, "An error ocurred creating the Vehicle type, please check data or contact with us.");
                        break;

                }
            }
        }

        protected void Delete_Click(object sender, EventArgs e)
        {
            DeleteVehicleType();
            FillTable();
        }

        protected void DeleteVehicleType()
        {
            switch (vehicleTypeRules.DeleteVehicleType(Convert.ToInt32(hiddenVehicleTypeId.Value)))
            {
                case 0:
                    FillTable();
                    buttonStyle.buttonStyleBlue(buttonInfoVehicleTypeTable, "Vehicle type deleted successful.");
                    break;
                case 1:
                    FillTable();
                    buttonStyle.buttonStyleRed(buttonInfoVehicleTypeTable, "This vehicle type has linked data can not be deleted.");
                    break;
                case 2:
                    buttonStyle.buttonStyleRed(buttonInfoVehicleTypeTable, "Please, select a vehicle type to delete.");
                    break;
            }
        }

        protected void Update_Click(object sender, EventArgs e)
        {
            VehicleType vehicleType = CreateVehicleType();
            vehicleType.Id = Convert.ToInt32(hiddenVehicleTypeId.Value);
            UpdateVehicleType(vehicleType);
            buttonClear.Style.Add("visibility", "visible");
            buttonAddVehicleType.Style.Add("visibility", "visible");
            buttonUpdate.Style.Add("visibility", "hidden");
            FillTable();
        }

        protected void UpdateVehicleType(VehicleType vehicleType)
        {
            if (vehicleType != null)
            {
                switch (vehicleTypeRules.UpdateVehicleType(vehicleType))
                {
                    case 0:
                        textboxVehicleType.Value = "";
                        buttonStyle.buttonStyleBlue(buttonErrors, "Vehicle type updated successful.");
                        break;
                    case 1:
                        buttonStyle.buttonStyleWhite(buttonErrors, "Vehicle type name field is empty.");
                        break;
                    case 2:
                        buttonStyle.buttonStyleRed(buttonErrors, "An error ocurred updating the Vehicle type, please check data or contact we us.");
                        break;

                }
            }
        }
    }
}