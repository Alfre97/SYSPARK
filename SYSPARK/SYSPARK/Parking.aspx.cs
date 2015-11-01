using SYSPARK.App_BussinessRules;
using SYSPARK.Entities;
using SYSPARK.App_Utility;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

namespace SYSPARK
{ 
    public partial class Paking : System.Web.UI.Page
    {
        ParkingBussinessRules parkingBussinessRules = new ParkingBussinessRules();
        ButtonStyle buttonStyle = new ButtonStyle();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["User-Id"] == null)
            Response.Redirect("Default.aspx");

            fillTable();
        }

        protected void AddParking_Click(object sender, EventArgs e)
        {
            InsertAndExceptions();
            fillTable();
        }

        protected void Delete_Click(object sender, EventArgs e)
        {
            deleteParking();
        }

        protected void deleteParking()
        {
            switch (parkingBussinessRules.DeleteParking(hiddenParkingName.Value))
            {
                case 0:
                    fillTable();
                    buttonStyle.buttonStyleBlue(buttonInfoParkingTable, "Parking deleted successful.");
                    break;
                case 1:
                    fillTable();
                    buttonStyle.buttonStyleRed(buttonInfoParkingTable, "This parking has linked data can not be deleted.");
                    break;
                case 2:
                    buttonStyle.buttonStyleRed(buttonInfoParkingTable, "Please, select a parking to delete.");
                    break;
            }
        }

        protected void fillTable()
        {
            ParkingData parkingData = new ParkingData();
            //Populating a DataTable from database.
            DataTable dt = parkingData.DataTableParking();

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
                html.Append("<tr onclick='getValue(this)' style='cursor:pointer' class='desmarcado'>");
                foreach (DataColumn column in dt.Columns)
                {
                    html.Append("<td>");
                    html.Append(row[column.ColumnName]);
                    html.Append("</td>");
                }
                html.Append("<td>");
                html.Append("<button onclick='' type='button'>Editar</button>");
                html.Append("</td>");
                html.Append("<td>");
                html.Append("<button onclick='deleteParking()' type='button'>Eliminar</button>");
                html.Append("</td>");
                html.Append("</tr>");
            }
            html.Append("</tbody>");

            //Append the HTML string to Placeholder.
            placeHolderTableParking.Controls.Clear();
            placeHolderTableParking.Controls.Add(new Literal { Text = html.ToString() });

        }

        protected Parking createParking()
        {
            Parking parking = new Parking();
            try
            {
                if (textboxParkingName.Value.Equals(string.Empty))
                    buttonStyle.buttonStyleRed(buttonErrors, "The parking name space field can't be empty.");
                else if (textboxTotalSpace.Value.Equals(string.Empty))
                    buttonStyle.buttonStyleWhite(buttonErrors, "The total space field can't be empty.");
                else if (textboxCarSpace.Value.Equals(string.Empty))
                    buttonStyle.buttonStyleRed(buttonErrors, "The car space field can't be empty.");
                else if (textboxMotorCycleSpace.Value.Equals(string.Empty))
                    buttonStyle.buttonStyleWhite(buttonErrors, "The motorcycle space field can't be empty.");
                else if (textboxBusSpace.Value.Equals(string.Empty))
                    buttonStyle.buttonStyleRed(buttonErrors, "The bus space field can't be empty.");
                else if (textboxHandicapSpace.Value.Equals(string.Empty))
                    buttonStyle.buttonStyleWhite(buttonErrors, "The handicap space field can't be empty.");
                else
                {
                    parking.Name = textboxParkingName.Value;
                    parking.TotalSpace = Convert.ToInt32(textboxTotalSpace.Value);
                    parking.CarSpace = Convert.ToInt32(textboxCarSpace.Value);
                    parking.MotorcycleSpace = Convert.ToInt32(textboxMotorCycleSpace.Value);
                    parking.HandicapSpace = Convert.ToInt32(textboxHandicapSpace.Value);
                    parking.BusSpace = Convert.ToInt32(textboxBusSpace.Value);
                    return parking;
                }
                return null;
            }
            catch (FormatException)
            {
                buttonStyle.buttonStyleRed(buttonErrors, "Invalid data. You can't enter \n only numbers in parking name," + "\n" + "letters in space fields," + "\n " + "or empty fields.");
                return null;
            }
        }

        protected void InsertAndExceptions()
        {
            Parking parking = createParking();
            SpaceData spaceData = new SpaceData();
            if (parking != null)
            {
                switch (parkingBussinessRules.InsertParking(parking))
                {
                    case 0:
                        clearControls();
                        buttonStyle.buttonStyleBlue(buttonErrors, "Parking created sucessfully.");
                        break;
                    case 1:
                        buttonStyle.buttonStyleRed(buttonErrors, "The parking name field is empty.");
                        break;
                    case 2:
                        buttonStyle.buttonStyleWhite(buttonErrors, "The total space field can't be less or equal than zero");
                        break;
                    case 3:
                        buttonStyle.buttonStyleRed(buttonErrors, "The car space field can't be less than zero.");
                        break;
                    case 4:
                        buttonStyle.buttonStyleWhite(buttonErrors, "The motorcycle space field can't be less than zero.");
                        break;
                    case 5:
                        buttonStyle.buttonStyleBlue(buttonErrors, "The handicap space field can't be less than zero.");
                        break;
                    case 6:
                        buttonStyle.buttonStyleWhite(buttonErrors, "The bus space field can't be less than zero.");
                        break;
                    case 7:
                        buttonStyle.buttonStyleRed(buttonErrors, "You can't enter zero in all space fields.");
                        break;
                    case 8:
                        buttonStyle.buttonStyleWhite(buttonErrors, "The sum of space fields can't be higher than the total space.");
                        break;
                    case 9:
                        buttonStyle.buttonStyleRed(buttonErrors, "An error ocurred creating spaces, please check data or contact with us.");
                        break;
                    case 10:
                        buttonStyle.buttonStyleWhite(buttonErrors, "An error ocurred creating the parking, please check data or contact with us.");
                        break;
                }
            }
        }

        protected void clearControls()
        {
            textboxParkingName.Value = string.Empty;
            textboxTotalSpace.Value = string.Empty;
            textboxCarSpace.Value = string.Empty;
            textboxMotorCycleSpace.Value = string.Empty;
            textboxHandicapSpace.Value = string.Empty;
            textboxBusSpace.Value = string.Empty;
        }
    }
}