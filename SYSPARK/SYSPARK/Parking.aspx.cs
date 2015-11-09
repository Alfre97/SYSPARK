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
using System.Data.SqlClient;

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

            FillTable();
        }

        protected void AddParking_Click(object sender, EventArgs e)
        {
            InsertParking(CreateParking());
            FillTable();
        }

        protected void Delete_Click(object sender, EventArgs e)
        {
            DeleteParking();
            FillTable();
        }

        protected void DeleteParking()
        {
            try
            {
                switch (parkingBussinessRules.DeleteParking(Convert.ToInt32(hiddenParkingName.Value)))
                {
                    case 0:
                        buttonStyle.buttonStyleBlue(buttonInfoParkingTable, "Parking deleted successful.");
                        break;
                    case 1:
                        buttonStyle.buttonStyleRed(buttonInfoParkingTable, "This parking has linked data can not be deleted.");
                        break;
                    case 2:
                        buttonStyle.buttonStyleRed(buttonInfoParkingTable, "Please, select a parking to delete.");
                        break;
                }
            }
            catch
            {
                buttonStyle.buttonStyleRed(buttonInfoParkingTable, "Please, select a parking to delete.");
            }

        }

        protected void FillTable()
        {
            ParkingData parkingData = new ParkingData();
            //Populating a DataTable from database.
            DataTable dt = parkingData.DataTableParking();

            //Building an HTML string.
            StringBuilder html = new StringBuilder();

            //Cleaning last table
            placeHolderTableParking.Controls.Clear();

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
                html.Append("<tr class='desmarcado' >");
                foreach (DataColumn column in dt.Columns)
                {
                    html.Append("<td style='cursor:pointer' onclick='getValue(this.parentNode)'>");
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
            placeHolderTableParking.Controls.Add(new Literal { Text = html.ToString() });

        }

        protected Parking CreateParking()
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

        protected void InsertParking(Parking parking)
        {
            if (parking != null)
            {
                switch (parkingBussinessRules.InsertParking(parking))
                {
                    case 0:
                        ClearControls();
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
                    case 11:
                        buttonStyle.buttonStyleWhite(buttonErrors, "The sum of space fields can't be less than the total space.");
                        break;
                }
            }
        }

        protected void ClearControls()
        {
            textboxParkingName.Value = string.Empty;
            textboxTotalSpace.Value = string.Empty;
            textboxCarSpace.Value = string.Empty;
            textboxMotorCycleSpace.Value = string.Empty;
            textboxHandicapSpace.Value = string.Empty;
            textboxBusSpace.Value = string.Empty;
        }

        protected void Edit_Click(object sender, EventArgs e)
        {
            SetValues(GetParking());
        }

        protected Parking GetParking()
        {
            if (hiddenParkingName.Value.Equals(string.Empty))
            {
                buttonStyle.buttonStyleRed(buttonInfoParkingTable, "Please, select a parking to edit.");
                return null;
            }
            else
            {
                ParkingData parkingData = new ParkingData();
                Parking parking = new Parking();
                try
                {
                    parkingData.SendParking(parkingData.GetParking(Convert.ToInt32(hiddenParkingName.Value)));
                }
                catch (SqlException)
                {
                    buttonStyle.buttonStyleWhite(buttonInfoParkingTable, "Ops, we have an error finding that parking.");
                }
                if (parking.Name.Equals(string.Empty))
                    parking = null;
                return parking;
            }
        }

        protected void SetValues(Parking parking)
        {
            if (parking != null)
            {
                textboxParkingName.Value = parking.Name;
                textboxTotalSpace.Value = parking.TotalSpace.ToString();
                textboxCarSpace.Value = parking.CarSpace.ToString();
                textboxMotorCycleSpace.Value = parking.MotorcycleSpace.ToString();
                textboxHandicapSpace.Value = parking.HandicapSpace.ToString();
                textboxBusSpace.Value = parking.BusSpace.ToString();
                buttonAddParking.Visible = false;
                buttonClear.Visible = false;
                buttonUpdate.Visible = true;
            }
        }

        protected void Update_Click(object sender, EventArgs e)
        {

        }

        protected void UpdateParking(Parking parking)
        {
            if (parking != null)
            {
                switch (parkingBussinessRules.UpdateParking(parking))
                {
                    case 0:
                        ClearControls();
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
                        buttonStyle.buttonStyleRed(buttonErrors, "You can't enter zero in all space fields or less than zero.");
                        break;
                    case 8:
                        buttonStyle.buttonStyleWhite(buttonErrors, "The sum of space fields can't be higher than the total space.");
                        break;
                    case 9:
                        buttonStyle.buttonStyleWhite(buttonErrors, "The sum of space fields can't be less than the total space.");
                        break;
                    case 10:
                        buttonStyle.buttonStyleWhite(buttonErrors, "An error ocurred updating the parking, please check data or contact with us.");
                        break;
                }
            }
        }
    }
}