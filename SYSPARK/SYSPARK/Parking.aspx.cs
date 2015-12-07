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
using SYSPARK.App_Entities;

namespace SYSPARK
{
    public partial class Paking : System.Web.UI.Page
    {
        ParkingBussinessRules parkingBussinessRules = new ParkingBussinessRules();
        ButtonStyle buttonStyle = new ButtonStyle();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["User-UserName"] == null)
                Response.Redirect("Default.aspx");

            //CreateSpaceSelect();
            FillSelectCampus();
            FillSelectCampusToView();
            FillTable(sender, e);
        }

        protected void FillSelectCampusToView()
        {
            //Select campus ti view
            CampusData campusData = new CampusData();
            selectCampusToView.DataSource = campusData.DataTableCampus();
            selectCampusToView.DataValueField = "Id";
            selectCampusToView.DataTextField = "Name";
            selectCampusToView.DataBind();
        }

        protected void FillSelectCampus()
        {
            //Select campus
            CampusData campusData = new CampusData();
            selectCampus.DataSource = campusData.DataTableCampus();
            selectCampus.DataValueField = "Id";
            selectCampus.DataTextField = "Name";
            selectCampus.DataBind();
        }

        protected void AddParking_Click(object sender, EventArgs e)
        {
            ParkingData parkingData = new ParkingData();
            int campusId;

            if (hiddenCampusValue.Value.Equals(string.Empty))
                campusId = Convert.ToInt32(selectCampus.Items[0].Value);
            else
                campusId = Convert.ToInt32(hiddenCampusValue.Value);

            DataTable foundParking = parkingData.DataTableSearchParking(campusId, textboxParkingName.Value);

            if (foundParking.Rows.Count > 0)
            {
                buttonStyle.buttonStyleRed(buttonErrors, "A parking with that name already exists on that campus.");
            }
            else
            {
                InsertParking(CreateParking());
                hiddenCampusValue.Value = "";
                FillTable(sender, e);
            }
        }

        protected void Delete_Click(object sender, EventArgs e)
        {
            DeleteParking();
            FillTable(sender, e);
        }

        protected void DeleteParking()
        {
            try
            {
                switch (parkingBussinessRules.DeleteParking(Convert.ToInt32(hiddenParkingId.Value)))
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

        protected void FillTable(object sender, EventArgs e)
        {
            ParkingData parkingData = new ParkingData();
            //Populating a DataTable from database.
            DataTable dt = new DataTable();
            if (hiddenCampusToViewValue.Value.Equals(string.Empty))
                dt = parkingData.DataTableParking(Convert.ToInt32(selectCampusToView.Items[0].Value));
            else
                dt = parkingData.DataTableParking(Convert.ToInt32(hiddenCampusToViewValue.Value));
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
                html.Append("<tr class='desmarcado'>");
                foreach (DataColumn column in dt.Columns)
                {
                    html.Append("<td style='cursor:pointer' onclick='getValue(this.parentNode)'>");
                    html.Append(row[column.ColumnName]);
                    html.Append("</td>");
                }
                html.Append("<td>");
                html.Append("<button onclick='setValues(this.parentNode.parentNode)' type='button'>Edit</button>");
                html.Append("</td>");
                html.Append("<td>");
                html.Append("<button onclick='deleteParking()' type='button'>Delete</button>");
                html.Append("</td>");
                html.Append("</tr>");
            }
            html.Append("</tbody>");

            //Append the HTML string to Placeholder.
            placeHolderTableParking.Controls.Add(new Literal { Text = html.ToString() });

        }

        protected void CreateSpaceSelect()
        {
            SpaceTypeData spaceTypeData = new SpaceTypeData();
            //Populating a DataTable from database.
            DataTable dt = spaceTypeData.DataTableSpaceType();

            //Building an HTML string.
            StringBuilder html = new StringBuilder();

            //Building the Header row.
            for (int i = 0; i <= dt.Rows.Count; i++)
            {
                html.Append("<tr>");
                html.Append("<td>");
                html.Append("<input type='hidden' id='hiddenSpaceType" + i + "' runat='server' />");
                html.Append("<select id='selectSpaceType" + i + "' runat='server' onchange='setValue('selectSpaceType" + i + "', 'hiddenSpaceType" + i + "')'>");
                html.Append("</select>");
                html.Append("<input type='text' id='textboxAmount' placeholder=' Amount' runat='server' /><br /> ");
                html.Append("</td>");
                html.Append("</tr>");
            }

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
                    parking.FreeSpace = parking.TotalSpace;
                    parking.CarSpace = Convert.ToInt32(textboxCarSpace.Value);
                    parking.MotorcycleSpace = Convert.ToInt32(textboxMotorCycleSpace.Value);
                    parking.HandicapSpace = Convert.ToInt32(textboxHandicapSpace.Value);
                    parking.BusSpace = Convert.ToInt32(textboxBusSpace.Value);
                    if (hiddenCampusValue.Value.Equals(string.Empty))
                    {
                        parking.CampusId = Convert.ToInt32(selectCampus.Items[0].Value);
                        parking.CampusName = selectCampus.Items[0].Text;
                    }
                    else
                    {
                        parking.CampusId = Convert.ToInt32(hiddenCampusValue.Value);
                        parking.CampusName = selectCampus.Items.FindByValue(hiddenCampusValue.Value).ToString();
                    }
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

        protected List<Space> CreateSpaceList()
        {
            
            int parkingCampusId = 0;
            string parkingName = string.Empty;
            List<Space> spaceList = new List<Space>();
            ParkingData parkingData = new ParkingData();

            spaceList.Capacity = Convert.ToInt32(textboxTotalSpace.Value);
            parkingName = textboxParkingName.Value;

            if (hiddenCampusValue.Value.Equals(string.Empty))
                parkingCampusId = Convert.ToInt32(selectCampus.Items[0].Value);
            else
                parkingCampusId = Convert.ToInt32(hiddenCampusValue.Value);

            int parkingId = parkingData.GetParkingId(textboxParkingName.Value, parkingCampusId);

            for (int i = 1; i <= Convert.ToInt32(textboxCarSpace.Value); i++)
            {
                Space space = new Space();
                SpaceType spaceType = new SpaceType();
                space.ParkingId = parkingId;
                space.ParkingName = parkingName;
                space.ParkingCampusId = parkingCampusId;
                space.Name = "Car" + "-" + i;
                spaceType.Id = 1;
                spaceType.Name = "Car";
                space.SpaceType = spaceType;
                space.Status = true;
                spaceList.Add(space);
            }

            for (int i = 1; i <= Convert.ToInt32(textboxMotorCycleSpace.Value); i++)
            {
                Space space = new Space();
                SpaceType spaceType = new SpaceType();
                space.ParkingId = parkingId;
                space.ParkingName = parkingName;
                space.ParkingCampusId = parkingCampusId;
                space.Name = "Motorcycle" + "-" + i;
                spaceType.Id = 2;
                spaceType.Name = "Motorcycle";
                space.SpaceType = spaceType;
                space.Status = true;
                spaceList.Add(space);
            }

            for (int i = 1; i <= Convert.ToInt32(textboxHandicapSpace.Value); i++)
            {
                Space space = new Space();
                SpaceType spaceType = new SpaceType();
                space.ParkingId = parkingId;
                space.ParkingName = parkingName;
                space.ParkingCampusId = parkingCampusId;
                space.Name = "Handicap" + "-" + i;
                spaceType.Id = 3;
                spaceType.Name = "Handicap";
                space.SpaceType = spaceType;
                space.Status = false;
                spaceList.Add(space);
            }

            for (int i = 1; i <= Convert.ToInt32(textboxBusSpace.Value); i++)
            {
                Space space = new Space();
                SpaceType spaceType = new SpaceType();
                space.ParkingId = parkingId;
                space.ParkingName = parkingName;
                space.ParkingCampusId = parkingCampusId;
                space.Name = "Bus" + "-" + i;
                spaceType.Id = 4;
                spaceType.Name = "Bus";
                space.SpaceType = spaceType;
                space.Status = true;
                spaceList.Add(space);
            }
            return spaceList;
        }

        protected void InsertParking(Parking parking)
        {
            if (parking != null)
            {
                switch (parkingBussinessRules.InsertParking(parking))
                {
                    case 0:
                        parking.SpaceList = CreateSpaceList();
                        if (parking.SpaceList != null)
                        {
                            switch (parkingBussinessRules.InsertParkingSpace(parking))
                            {
                                case 0:
                                    ClearControls();
                                    buttonStyle.buttonStyleBlue(buttonErrors, "Parking created sucessfully.");
                                    break;
                                case 1:
                                    buttonStyle.buttonStyleRed(buttonErrors, "An error ocurred creating the parking spaces.");
                                    break;
                                case 2:
                                    buttonStyle.buttonStyleWhite(buttonErrors, "An error ocurred creating the parking spaces and deleting the parking.");
                                    break;
                            }
                        }
                        else
                            buttonStyle.buttonStyleRed(buttonErrors, "An error ocurred creating the parking spaces.");
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
            if (hiddenParkingId.Value.Equals(string.Empty))
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
                    parkingData.SendParking(parkingData.GetParking(Convert.ToInt32(hiddenParkingId.Value)));
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