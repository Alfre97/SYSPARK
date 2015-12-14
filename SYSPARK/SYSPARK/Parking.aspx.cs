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
            //if (Session["User-UserName"] == null)
            //    Response.Redirect("http://syspark.azurewebsites.net/Default.aspx");

            //CreateSpaceSelect();
            buttonCancel.Visible = false;
            tableGray2.Visible = false;
            FillSelectSpaceType();
            h1ParkingMap.Visible = false;
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

        protected void FillSelectSpaceType()
        {
            //Select Campus
            SpaceTypeData spaceTypeData = new SpaceTypeData();
            selectType.DataSource = spaceTypeData.DataTableSpaceType();
            selectType.DataValueField = "Id";
            selectType.DataTextField = "Name";
            selectType.DataBind();
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
                OpenControls();
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
            //Building the Header row.
            html.Append("<tbody>");
            html.Append("<tr>");
            html.Append("<th>");
            html.Append("Id");
            html.Append("</th>");
            html.Append("<th>");
            html.Append("Name");
            html.Append("</th>");
            html.Append("<th>");
            html.Append("Campus Id");
            html.Append("</th>");
            html.Append("<th>");
            html.Append("Campus Name");
            html.Append("</th>");
            html.Append("<th>");
            html.Append("Height");
            html.Append("</th>");
            html.Append("<th>");
            html.Append("Width");
            html.Append("</th>");
            html.Append("<th>");
            html.Append("Total Space");
            html.Append("</th>");
            html.Append("<th>");
            html.Append("Free Space");
            html.Append("</th>");
            html.Append("<th>");
            html.Append("Car Space");
            html.Append("</th>");
            html.Append("<th>");
            html.Append("Motorcycle Space");
            html.Append("</th>");
            html.Append("<th>");
            html.Append("Handicap Space");
            html.Append("</th>");
            html.Append("<th>");
            html.Append("Bus Space");
            html.Append("</th>");
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

        protected void GenerateMap_Click(object sender, EventArgs e)
        {
            if (CheckValueBeforeMap() == 0)
                GenerateMap();
        }

        protected int CheckValueBeforeMap()
        {
            try
            {
                int total = Convert.ToInt32(textboxTotalSpace.Value);
                int car = Convert.ToInt32(textboxCarSpace.Value);
                int motorcycle = Convert.ToInt32(textboxMotorCycleSpace.Value);
                int handicap = Convert.ToInt32(textboxHandicapSpace.Value);
                int bus = Convert.ToInt32(textboxBusSpace.Value);
                int height = Convert.ToInt32(textHeight.Value);
                int width = Convert.ToInt32(textWidth.Value);

                if (height <= 0)
                {
                    buttonStyle.buttonStyleRed(buttonErrors, "The height field can't be less or equals than zero.");
                    return 1;
                }

                else if (width <= 0)
                {
                    buttonStyle.buttonStyleRed(buttonErrors, "The width field can't be less or equals than zero.");
                    return 1;
                }

                else if (total <= 0)
                {
                    buttonStyle.buttonStyleRed(buttonErrors, "The total space field can't be less or equals than zero..");
                    return 1;
                }

                else if (car < 0)
                {
                    buttonStyle.buttonStyleRed(buttonErrors, "The car space field can't be less than zero.");
                    return 1;
                }

                else if (motorcycle < 0)
                {
                    buttonStyle.buttonStyleRed(buttonErrors, "The motorcyle space field can't be less than zero.");
                    return 1;
                }

                else if (handicap < 0)
                {
                    buttonStyle.buttonStyleRed(buttonErrors, "The handicap space field can't be less than zero.");
                    return 1;
                }

                else if (bus < 0)
                {
                    buttonStyle.buttonStyleRed(buttonErrors, "The bus space field can't be less than zero.");
                    return 1;
                }

                else if (car + motorcycle + handicap + bus <= 0)
                {
                    buttonStyle.buttonStyleRed(buttonErrors, "The sum of car, motorcycle, handicap," + "\n" + " bus space field can't be less or equals than zero.");
                    return 1;
                }

                else if (car + motorcycle + handicap + bus > total)
                {
                    buttonStyle.buttonStyleRed(buttonErrors, "The sum of car, motorcycle, handicap," + "\n" + " bus space field can't be highter than total.");
                    return 1;
                }

                else if (car + motorcycle + handicap + bus < total)
                {
                    buttonStyle.buttonStyleRed(buttonErrors, "The sum of car, motorcycle, handicap," + "\n" + " bus space field can't be highter than total.");
                    return 1;
                }
                else if (height > 20)
                {
                    buttonStyle.buttonStyleRed(buttonErrors, "The width field can't be highter than twenty.");
                    return 1;
                }
                else if (width > 15)
                {
                    buttonStyle.buttonStyleRed(buttonErrors, "The width field can't be highter than fitteen.");
                    return 1;
                }
                return 0;
            }
            catch (FormatException)
            {
                buttonStyle.buttonStyleRed(buttonErrors, "Invalid data." + "\n" + "Please, check it.");
                return 1;
            }
        }

        protected void GenerateMap()
        {
            int hidden = 1;
            int limit = 20;

            spanCar.InnerHtml = textboxCarSpace.Value + " ";
            spanMotorcycle.InnerHtml = textboxMotorCycleSpace.Value + " ";
            spanHandicap.InnerHtml = textboxHandicapSpace.Value + " ";
            spanBus.InnerHtml = textboxBusSpace.Value + " ";
            textboxParkingName.Disabled = true;
            buttonCancel.Visible = true;
            buttonErrors2.Visible = true;
            buttonErrors2.Value = "Click in select to choose a " + "\n" + " space type then click on a space.";
            textboxTotalSpace.Disabled = true;
            textboxCarSpace.Disabled = true;
            textboxMotorCycleSpace.Disabled = true;
            textboxHandicapSpace.Disabled = true;
            textboxBusSpace.Disabled = true;
            textHeight.Disabled = true;
            textWidth.Disabled = true;
            buttonGenerateMap.Disabled = true;
            buttonClear.Visible = false;
            h1ParkingMap.Visible = true;
            tableGray2.Visible = true;

            //Building an HTML string.
            StringBuilder html = new StringBuilder();

            //Cleaning last table
            placeHolderMap.Controls.Clear();

            for (int i = 0; i < Convert.ToInt32(textWidth.Value); i++)
            {
                html.Append("<tr>");
                for (int j = 0; j < Convert.ToInt32(textHeight.Value); j++)
                {
                    html.Append("<td>");
                    html.Append("<button type='button' onclick='setColorAndValue(this)' runat='server' name='hidden" + hidden + "' id='buttonSpace' value='" + i + "," + j + "'>Clear</button>");
                    html.Append("</td>");
                    hidden++;
                }
                html.Append("</tr>");

                if (hidden < limit)
                {
                    hidden = limit + 1;
                    limit += 20;
                }
            }
            //Append the HTML string to Placeholder.
            placeHolderMap.Controls.Add(new Literal { Text = html.ToString() });
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
                    parking.Height = Convert.ToInt32(textHeight.Value);
                    parking.Width = Convert.ToInt32(textWidth.Value);
                    if (hiddenCampusValue.Value.Equals(string.Empty))
                    {
                        try
                        {
                            parking.CampusId = Convert.ToInt32(selectCampus.Items[0].Value);
                            parking.CampusName = selectCampus.Items[0].Text;
                        }
                        catch (Exception)
                        {
                            buttonStyle.buttonStyleRed(buttonErrors, "The select campus is empty.");
                            return null;
                        }
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

        protected List<Space> CreateSpaceListWithMap()
        {
            try
            {
                //Declare variables
                List<Space> spaceList = new List<Space>();
                int parkingCampusId = 0;
                int carcounter = 1;
                int motoCounter = 1;
                int handicapCounter = 1;
                int busCounter = 1;
                int streetCounter = 1;
                int clearCounter = 1;
                string parkingName = textboxParkingName.Value;
                ParkingData parkingData = new ParkingData();
                SpaceType spaceTypeClear = new SpaceType();
                SpaceType spaceTypeStreet = new SpaceType();
                SpaceType spaceTypeBus = new SpaceType();
                SpaceType spaceTypeHandicap = new SpaceType();
                SpaceType spaceTypeMoto = new SpaceType();
                SpaceType spaceTypeCar = new SpaceType();
                //Getting campus and parking ID
                if (hiddenCampusValue.Value.Equals(string.Empty))
                    parkingCampusId = Convert.ToInt32(selectCampus.Items[0].Value);
                else
                    parkingCampusId = Convert.ToInt32(hiddenCampusValue.Value);

                int parkingId = parkingData.GetParkingId(textboxParkingName.Value, parkingCampusId);
                //Setting SpaceTypes Data
                spaceTypeCar.Id = 1;
                spaceTypeCar.Name = "Car";

                spaceTypeMoto.Id = 2;
                spaceTypeMoto.Name = "Motorcycle";

                spaceTypeHandicap.Id = 3;
                spaceTypeHandicap.Name = "Handicap";

                spaceTypeBus.Id = 4;
                spaceTypeBus.Name = "Bus";

                spaceTypeStreet.Id = 5;
                spaceTypeStreet.Name = "Street";

                spaceTypeClear.Id = 6;
                spaceTypeClear.Name = "Clear";
                //Creating and filling the space list
                for (int i = 0; i < Convert.ToInt32(textWidth.Value); i++)
                {
                    for (int j = 0; j < Convert.ToInt32(textHeight.Value); j++)
                    {
                        Space space = new Space();
                        space.Position = i + "," + j;
                        space.SpaceType = spaceTypeClear;
                        space.Name = "Clear";
                        spaceList.Add(space);
                    }
                }
                //Getting and setting 
                foreach (Space space in spaceList)
                {
                    space.ParkingId = parkingId;
                    space.ParkingName = parkingName;
                    space.ParkingCampusId = parkingCampusId;

                    switch (space.Position)
                    {
                        case "0,0":
                            switch (hidden1.Value)
                            {
                                case "Car":
                                    space.SpaceType = spaceTypeCar;
                                    space.Name = "Car-" + carcounter;
                                    carcounter++;
                                    break;
                                case "Motorcycle":
                                    space.SpaceType = spaceTypeMoto;
                                    space.Name = "Motorcycle-" + motoCounter;
                                    motoCounter++;
                                    break;
                                case "Handicap":
                                    space.SpaceType = spaceTypeHandicap;
                                    space.Name = "Handicap-" + handicapCounter;
                                    handicapCounter++;
                                    break;
                                case "Bus":
                                    space.SpaceType = spaceTypeBus;
                                    space.Name = "Bus-" + busCounter;
                                    busCounter++;
                                    break;
                                case "Street":
                                    space.SpaceType = spaceTypeStreet;
                                    space.Name = "Street-" + streetCounter;
                                    streetCounter++;
                                    break;
                                case "Clear":
                                    space.SpaceType = spaceTypeClear;
                                    space.Name = "Clear-" + clearCounter;
                                    clearCounter++;
                                    break;
                            }
                            break;

                        case "0,1":
                            switch (hidden2.Value)
                            {
                                case "Car":
                                    space.SpaceType = spaceTypeCar;
                                    space.Name = "Car-" + carcounter;
                                    carcounter++;
                                    break;
                                case "Motorcycle":
                                    space.SpaceType = spaceTypeMoto;
                                    space.Name = "Motorcycle-" + motoCounter;
                                    motoCounter++;
                                    break;
                                case "Handicap":
                                    space.SpaceType = spaceTypeHandicap;
                                    space.Name = "Handicap-" + handicapCounter;
                                    handicapCounter++;
                                    break;
                                case "Bus":
                                    space.SpaceType = spaceTypeBus;
                                    space.Name = "Bus-" + busCounter;
                                    busCounter++;
                                    break;
                                case "Street":
                                    space.SpaceType = spaceTypeStreet;
                                    space.Name = "Street-" + streetCounter;
                                    streetCounter++;
                                    break;
                                case "Clear":
                                    space.SpaceType = spaceTypeClear;
                                    space.Name = "Clear-" + clearCounter;
                                    clearCounter++;
                                    break;
                            }
                            break;
                        case "0,2":
                            switch (hidden3.Value)
                            {
                                case "Car":
                                    space.SpaceType = spaceTypeCar;
                                    space.Name = "Car-" + carcounter;
                                    carcounter++;
                                    break;
                                case "Motorcycle":
                                    space.SpaceType = spaceTypeMoto;
                                    space.Name = "Motorcycle-" + motoCounter;
                                    motoCounter++;
                                    break;
                                case "Handicap":
                                    space.SpaceType = spaceTypeHandicap;
                                    space.Name = "Handicap-" + handicapCounter;
                                    handicapCounter++;
                                    break;
                                case "Bus":
                                    space.SpaceType = spaceTypeBus;
                                    space.Name = "Bus-" + busCounter;
                                    busCounter++;
                                    break;
                                case "Street":
                                    space.SpaceType = spaceTypeStreet;
                                    space.Name = "Street-" + streetCounter;
                                    streetCounter++;
                                    break;
                                case "Clear":
                                    space.SpaceType = spaceTypeClear;
                                    space.Name = "Clear-" + clearCounter;
                                    clearCounter++;
                                    break;
                            }
                            break;
                        case "0,3":
                            switch (hidden4.Value)
                            {
                                case "Car":
                                    space.SpaceType = spaceTypeCar;
                                    space.Name = "Car-" + carcounter;
                                    carcounter++;
                                    break;
                                case "Motorcycle":
                                    space.SpaceType = spaceTypeMoto;
                                    space.Name = "Motorcycle-" + motoCounter;
                                    motoCounter++;
                                    break;
                                case "Handicap":
                                    space.SpaceType = spaceTypeHandicap;
                                    space.Name = "Handicap-" + handicapCounter;
                                    handicapCounter++;
                                    break;
                                case "Bus":
                                    space.SpaceType = spaceTypeBus;
                                    space.Name = "Bus-" + busCounter;
                                    busCounter++;
                                    break;
                                case "Street":
                                    space.SpaceType = spaceTypeStreet;
                                    space.Name = "Street-" + streetCounter;
                                    streetCounter++;
                                    break;
                                case "Clear":
                                    space.SpaceType = spaceTypeClear;
                                    space.Name = "Clear-" + clearCounter;
                                    clearCounter++;
                                    break;
                            }
                            break;
                        case "0,4":
                            switch (hidden5.Value)
                            {
                                case "Car":
                                    space.SpaceType = spaceTypeCar;
                                    space.Name = "Car-" + carcounter;
                                    carcounter++;
                                    break;
                                case "Motorcycle":
                                    space.SpaceType = spaceTypeMoto;
                                    space.Name = "Motorcycle-" + motoCounter;
                                    motoCounter++;
                                    break;
                                case "Handicap":
                                    space.SpaceType = spaceTypeHandicap;
                                    space.Name = "Handicap-" + handicapCounter;
                                    handicapCounter++;
                                    break;
                                case "Bus":
                                    space.SpaceType = spaceTypeBus;
                                    space.Name = "Bus-" + busCounter;
                                    busCounter++;
                                    break;
                                case "Street":
                                    space.SpaceType = spaceTypeStreet;
                                    space.Name = "Street-" + streetCounter;
                                    streetCounter++;
                                    break;
                                case "Clear":
                                    space.SpaceType = spaceTypeClear;
                                    space.Name = "Clear-" + clearCounter;
                                    clearCounter++;
                                    break;
                            }
                            break;
                        case "0,5":
                            switch (hidden6.Value)
                            {
                                case "Car":
                                    space.SpaceType = spaceTypeCar;
                                    space.Name = "Car-" + carcounter;
                                    carcounter++;
                                    break;
                                case "Motorcycle":
                                    space.SpaceType = spaceTypeMoto;
                                    space.Name = "Motorcycle-" + motoCounter;
                                    motoCounter++;
                                    break;
                                case "Handicap":
                                    space.SpaceType = spaceTypeHandicap;
                                    space.Name = "Handicap-" + handicapCounter;
                                    handicapCounter++;
                                    break;
                                case "Bus":
                                    space.SpaceType = spaceTypeBus;
                                    space.Name = "Bus-" + busCounter;
                                    busCounter++;
                                    break;
                                case "Street":
                                    space.SpaceType = spaceTypeStreet;
                                    space.Name = "Street-" + streetCounter;
                                    streetCounter++;
                                    break;
                                case "Clear":
                                    space.SpaceType = spaceTypeClear;
                                    space.Name = "Clear-" + clearCounter;
                                    clearCounter++;
                                    break;
                            }
                            break;
                        case "0,6":
                            switch (hidden7.Value)
                            {
                                case "Car":
                                    space.SpaceType = spaceTypeCar;
                                    space.Name = "Car-" + carcounter;
                                    carcounter++;
                                    break;
                                case "Motorcycle":
                                    space.SpaceType = spaceTypeMoto;
                                    space.Name = "Motorcycle-" + motoCounter;
                                    motoCounter++;
                                    break;
                                case "Handicap":
                                    space.SpaceType = spaceTypeHandicap;
                                    space.Name = "Handicap-" + handicapCounter;
                                    handicapCounter++;
                                    break;
                                case "Bus":
                                    space.SpaceType = spaceTypeBus;
                                    space.Name = "Bus-" + busCounter;
                                    busCounter++;
                                    break;
                                case "Street":
                                    space.SpaceType = spaceTypeStreet;
                                    space.Name = "Street-" + streetCounter;
                                    streetCounter++;
                                    break;
                                case "Clear":
                                    space.SpaceType = spaceTypeClear;
                                    space.Name = "Clear-" + clearCounter;
                                    clearCounter++;
                                    break;
                            }
                            break;
                        case "0,7":
                            switch (hidden8.Value)
                            {
                                case "Car":
                                    space.SpaceType = spaceTypeCar;
                                    space.Name = "Car-" + carcounter;
                                    carcounter++;
                                    break;
                                case "Motorcycle":
                                    space.SpaceType = spaceTypeMoto;
                                    space.Name = "Motorcycle-" + motoCounter;
                                    motoCounter++;
                                    break;
                                case "Handicap":
                                    space.SpaceType = spaceTypeHandicap;
                                    space.Name = "Handicap-" + handicapCounter;
                                    handicapCounter++;
                                    break;
                                case "Bus":
                                    space.SpaceType = spaceTypeBus;
                                    space.Name = "Bus-" + busCounter;
                                    busCounter++;
                                    break;
                                case "Street":
                                    space.SpaceType = spaceTypeStreet;
                                    space.Name = "Street-" + streetCounter;
                                    streetCounter++;
                                    break;
                                case "Clear":
                                    space.SpaceType = spaceTypeClear;
                                    space.Name = "Clear-" + clearCounter;
                                    clearCounter++;
                                    break;
                            }
                            break;
                        case "0,8":
                            switch (hidden9.Value)
                            {
                                case "Car":
                                    space.SpaceType = spaceTypeCar;
                                    space.Name = "Car-" + carcounter;
                                    carcounter++;
                                    break;
                                case "Motorcycle":
                                    space.SpaceType = spaceTypeMoto;
                                    space.Name = "Motorcycle-" + motoCounter;
                                    motoCounter++;
                                    break;
                                case "Handicap":
                                    space.SpaceType = spaceTypeHandicap;
                                    space.Name = "Handicap-" + handicapCounter;
                                    handicapCounter++;
                                    break;
                                case "Bus":
                                    space.SpaceType = spaceTypeBus;
                                    space.Name = "Bus-" + busCounter;
                                    busCounter++;
                                    break;
                                case "Street":
                                    space.SpaceType = spaceTypeStreet;
                                    space.Name = "Street-" + streetCounter;
                                    streetCounter++;
                                    break;
                                case "Clear":
                                    space.SpaceType = spaceTypeClear;
                                    space.Name = "Clear-" + clearCounter;
                                    clearCounter++;
                                    break;
                            }
                            break;
                        case "0,9":
                            switch (hidden10.Value)
                            {
                                case "Car":
                                    space.SpaceType = spaceTypeCar;
                                    space.Name = "Car-" + carcounter;
                                    carcounter++;
                                    break;
                                case "Motorcycle":
                                    space.SpaceType = spaceTypeMoto;
                                    space.Name = "Motorcycle-" + motoCounter;
                                    motoCounter++;
                                    break;
                                case "Handicap":
                                    space.SpaceType = spaceTypeHandicap;
                                    space.Name = "Handicap-" + handicapCounter;
                                    handicapCounter++;
                                    break;
                                case "Bus":
                                    space.SpaceType = spaceTypeBus;
                                    space.Name = "Bus-" + busCounter;
                                    busCounter++;
                                    break;
                                case "Street":
                                    space.SpaceType = spaceTypeStreet;
                                    space.Name = "Street-" + streetCounter;
                                    streetCounter++;
                                    break;
                                case "Clear":
                                    space.SpaceType = spaceTypeClear;
                                    space.Name = "Clear-" + clearCounter;
                                    clearCounter++;
                                    break;
                            }
                            break;
                        case "0,10":
                            switch (hidden11.Value)
                            {
                                case "Car":
                                    space.SpaceType = spaceTypeCar;
                                    space.Name = "Car-" + carcounter;
                                    carcounter++;
                                    break;
                                case "Motorcycle":
                                    space.SpaceType = spaceTypeMoto;
                                    space.Name = "Motorcycle-" + motoCounter;
                                    motoCounter++;
                                    break;
                                case "Handicap":
                                    space.SpaceType = spaceTypeHandicap;
                                    space.Name = "Handicap-" + handicapCounter;
                                    handicapCounter++;
                                    break;
                                case "Bus":
                                    space.SpaceType = spaceTypeBus;
                                    space.Name = "Bus-" + busCounter;
                                    busCounter++;
                                    break;
                                case "Street":
                                    space.SpaceType = spaceTypeStreet;
                                    space.Name = "Street-" + streetCounter;
                                    streetCounter++;
                                    break;
                                case "Clear":
                                    space.SpaceType = spaceTypeClear;
                                    space.Name = "Clear-" + clearCounter;
                                    clearCounter++;
                                    break;
                            }
                            break;
                        case "0,11":
                            switch (hidden12.Value)
                            {
                                case "Car":
                                    space.SpaceType = spaceTypeCar;
                                    space.Name = "Car-" + carcounter;
                                    carcounter++;
                                    break;
                                case "Motorcycle":
                                    space.SpaceType = spaceTypeMoto;
                                    space.Name = "Motorcycle-" + motoCounter;
                                    motoCounter++;
                                    break;
                                case "Handicap":
                                    space.SpaceType = spaceTypeHandicap;
                                    space.Name = "Handicap-" + handicapCounter;
                                    handicapCounter++;
                                    break;
                                case "Bus":
                                    space.SpaceType = spaceTypeBus;
                                    space.Name = "Bus-" + busCounter;
                                    busCounter++;
                                    break;
                                case "Street":
                                    space.SpaceType = spaceTypeStreet;
                                    space.Name = "Street-" + streetCounter;
                                    streetCounter++;
                                    break;
                                case "Clear":
                                    space.SpaceType = spaceTypeClear;
                                    space.Name = "Clear-" + clearCounter;
                                    clearCounter++;
                                    break;
                            }
                            break;
                        case "0,12":
                            switch (hidden13.Value)
                            {
                                case "Car":
                                    space.SpaceType = spaceTypeCar;
                                    space.Name = "Car-" + carcounter;
                                    carcounter++;
                                    break;
                                case "Motorcycle":
                                    space.SpaceType = spaceTypeMoto;
                                    space.Name = "Motorcycle-" + motoCounter;
                                    motoCounter++;
                                    break;
                                case "Handicap":
                                    space.SpaceType = spaceTypeHandicap;
                                    space.Name = "Handicap-" + handicapCounter;
                                    handicapCounter++;
                                    break;
                                case "Bus":
                                    space.SpaceType = spaceTypeBus;
                                    space.Name = "Bus-" + busCounter;
                                    busCounter++;
                                    break;
                                case "Street":
                                    space.SpaceType = spaceTypeStreet;
                                    space.Name = "Street-" + streetCounter;
                                    streetCounter++;
                                    break;
                                case "Clear":
                                    space.SpaceType = spaceTypeClear;
                                    space.Name = "Clear-" + clearCounter;
                                    clearCounter++;
                                    break;
                            }
                            break;
                        case "0,13":
                            switch (hidden14.Value)
                            {
                                case "Car":
                                    space.SpaceType = spaceTypeCar;
                                    space.Name = "Car-" + carcounter;
                                    carcounter++;
                                    break;
                                case "Motorcycle":
                                    space.SpaceType = spaceTypeMoto;
                                    space.Name = "Motorcycle-" + motoCounter;
                                    motoCounter++;
                                    break;
                                case "Handicap":
                                    space.SpaceType = spaceTypeHandicap;
                                    space.Name = "Handicap-" + handicapCounter;
                                    handicapCounter++;
                                    break;
                                case "Bus":
                                    space.SpaceType = spaceTypeBus;
                                    space.Name = "Bus-" + busCounter;
                                    busCounter++;
                                    break;
                                case "Street":
                                    space.SpaceType = spaceTypeStreet;
                                    space.Name = "Street-" + streetCounter;
                                    streetCounter++;
                                    break;
                                case "Clear":
                                    space.SpaceType = spaceTypeClear;
                                    space.Name = "Clear-" + clearCounter;
                                    clearCounter++;
                                    break;
                            }
                            break;
                        case "0,14":
                            switch (hidden15.Value)
                            {
                                case "Car":
                                    space.SpaceType = spaceTypeCar;
                                    space.Name = "Car-" + carcounter;
                                    carcounter++;
                                    break;
                                case "Motorcycle":
                                    space.SpaceType = spaceTypeMoto;
                                    space.Name = "Motorcycle-" + motoCounter;
                                    motoCounter++;
                                    break;
                                case "Handicap":
                                    space.SpaceType = spaceTypeHandicap;
                                    space.Name = "Handicap-" + handicapCounter;
                                    handicapCounter++;
                                    break;
                                case "Bus":
                                    space.SpaceType = spaceTypeBus;
                                    space.Name = "Bus-" + busCounter;
                                    busCounter++;
                                    break;
                                case "Street":
                                    space.SpaceType = spaceTypeStreet;
                                    space.Name = "Street-" + streetCounter;
                                    streetCounter++;
                                    break;
                                case "Clear":
                                    space.SpaceType = spaceTypeClear;
                                    space.Name = "Clear-" + clearCounter;
                                    clearCounter++;
                                    break;
                            }
                            break;
                        case "0,15":
                            switch (hidden16.Value)
                            {
                                case "Car":
                                    space.SpaceType = spaceTypeCar;
                                    space.Name = "Car-" + carcounter;
                                    carcounter++;
                                    break;
                                case "Motorcycle":
                                    space.SpaceType = spaceTypeMoto;
                                    space.Name = "Motorcycle-" + motoCounter;
                                    motoCounter++;
                                    break;
                                case "Handicap":
                                    space.SpaceType = spaceTypeHandicap;
                                    space.Name = "Handicap-" + handicapCounter;
                                    handicapCounter++;
                                    break;
                                case "Bus":
                                    space.SpaceType = spaceTypeBus;
                                    space.Name = "Bus-" + busCounter;
                                    busCounter++;
                                    break;
                                case "Street":
                                    space.SpaceType = spaceTypeStreet;
                                    space.Name = "Street-" + streetCounter;
                                    streetCounter++;
                                    break;
                                case "Clear":
                                    space.SpaceType = spaceTypeClear;
                                    space.Name = "Clear-" + clearCounter;
                                    clearCounter++;
                                    break;
                            }
                            break;
                        case "0,16":
                            switch (hidden17.Value)
                            {
                                case "Car":
                                    space.SpaceType = spaceTypeCar;
                                    space.Name = "Car-" + carcounter;
                                    carcounter++;
                                    break;
                                case "Motorcycle":
                                    space.SpaceType = spaceTypeMoto;
                                    space.Name = "Motorcycle-" + motoCounter;
                                    motoCounter++;
                                    break;
                                case "Handicap":
                                    space.SpaceType = spaceTypeHandicap;
                                    space.Name = "Handicap-" + handicapCounter;
                                    handicapCounter++;
                                    break;
                                case "Bus":
                                    space.SpaceType = spaceTypeBus;
                                    space.Name = "Bus-" + busCounter;
                                    busCounter++;
                                    break;
                                case "Street":
                                    space.SpaceType = spaceTypeStreet;
                                    space.Name = "Street-" + streetCounter;
                                    streetCounter++;
                                    break;
                                case "Clear":
                                    space.SpaceType = spaceTypeClear;
                                    space.Name = "Clear-" + clearCounter;
                                    clearCounter++;
                                    break;
                            }
                            break;
                        case "0,17":
                            switch (hidden18.Value)
                            {
                                case "Car":
                                    space.SpaceType = spaceTypeCar;
                                    space.Name = "Car-" + carcounter;
                                    carcounter++;
                                    break;
                                case "Motorcycle":
                                    space.SpaceType = spaceTypeMoto;
                                    space.Name = "Motorcycle-" + motoCounter;
                                    motoCounter++;
                                    break;
                                case "Handicap":
                                    space.SpaceType = spaceTypeHandicap;
                                    space.Name = "Handicap-" + handicapCounter;
                                    handicapCounter++;
                                    break;
                                case "Bus":
                                    space.SpaceType = spaceTypeBus;
                                    space.Name = "Bus-" + busCounter;
                                    busCounter++;
                                    break;
                                case "Street":
                                    space.SpaceType = spaceTypeStreet;
                                    space.Name = "Street-" + streetCounter;
                                    streetCounter++;
                                    break;
                                case "Clear":
                                    space.SpaceType = spaceTypeClear;
                                    space.Name = "Clear-" + clearCounter;
                                    clearCounter++;
                                    break;
                            }
                            break;
                        case "0,18":
                            switch (hidden19.Value)
                            {
                                case "Car":
                                    space.SpaceType = spaceTypeCar;
                                    space.Name = "Car-" + carcounter;
                                    carcounter++;
                                    break;
                                case "Motorcycle":
                                    space.SpaceType = spaceTypeMoto;
                                    space.Name = "Motorcycle-" + motoCounter;
                                    motoCounter++;
                                    break;
                                case "Handicap":
                                    space.SpaceType = spaceTypeHandicap;
                                    space.Name = "Handicap-" + handicapCounter;
                                    handicapCounter++;
                                    break;
                                case "Bus":
                                    space.SpaceType = spaceTypeBus;
                                    space.Name = "Bus-" + busCounter;
                                    busCounter++;
                                    break;
                                case "Street":
                                    space.SpaceType = spaceTypeStreet;
                                    space.Name = "Street-" + streetCounter;
                                    streetCounter++;
                                    break;
                                case "Clear":
                                    space.SpaceType = spaceTypeClear;
                                    space.Name = "Clear-" + clearCounter;
                                    clearCounter++;
                                    break;
                            }
                            break;
                        case "0,19":
                            switch (hidden20.Value)
                            {
                                case "Car":
                                    space.SpaceType = spaceTypeCar;
                                    space.Name = "Car-" + carcounter;
                                    carcounter++;
                                    break;
                                case "Motorcycle":
                                    space.SpaceType = spaceTypeMoto;
                                    space.Name = "Motorcycle-" + motoCounter;
                                    motoCounter++;
                                    break;
                                case "Handicap":
                                    space.SpaceType = spaceTypeHandicap;
                                    space.Name = "Handicap-" + handicapCounter;
                                    handicapCounter++;
                                    break;
                                case "Bus":
                                    space.SpaceType = spaceTypeBus;
                                    space.Name = "Bus-" + busCounter;
                                    busCounter++;
                                    break;
                                case "Street":
                                    space.SpaceType = spaceTypeStreet;
                                    space.Name = "Street-" + streetCounter;
                                    streetCounter++;
                                    break;
                                case "Clear":
                                    space.SpaceType = spaceTypeClear;
                                    space.Name = "Clear-" + clearCounter;
                                    clearCounter++;
                                    break;
                            }
                            break;
                        case "1,0":
                            switch (hidden21.Value)
                            {
                                case "Car":
                                    space.SpaceType = spaceTypeCar;
                                    space.Name = "Car-" + carcounter;
                                    carcounter++;
                                    break;
                                case "Motorcycle":
                                    space.SpaceType = spaceTypeMoto;
                                    space.Name = "Motorcycle-" + motoCounter;
                                    motoCounter++;
                                    break;
                                case "Handicap":
                                    space.SpaceType = spaceTypeHandicap;
                                    space.Name = "Handicap-" + handicapCounter;
                                    handicapCounter++;
                                    break;
                                case "Bus":
                                    space.SpaceType = spaceTypeBus;
                                    space.Name = "Bus-" + busCounter;
                                    busCounter++;
                                    break;
                                case "Street":
                                    space.SpaceType = spaceTypeStreet;
                                    space.Name = "Street-" + streetCounter;
                                    streetCounter++;
                                    break;
                                case "Clear":
                                    space.SpaceType = spaceTypeClear;
                                    space.Name = "Clear-" + clearCounter;
                                    clearCounter++;
                                    break;
                            }
                            break;
                        case "1,1":
                            switch (hidden22.Value)
                            {
                                case "Car":
                                    space.SpaceType = spaceTypeCar;
                                    space.Name = "Car-" + carcounter;
                                    carcounter++;
                                    break;
                                case "Motorcycle":
                                    space.SpaceType = spaceTypeMoto;
                                    space.Name = "Motorcycle-" + motoCounter;
                                    motoCounter++;
                                    break;
                                case "Handicap":
                                    space.SpaceType = spaceTypeHandicap;
                                    space.Name = "Handicap-" + handicapCounter;
                                    handicapCounter++;
                                    break;
                                case "Bus":
                                    space.SpaceType = spaceTypeBus;
                                    space.Name = "Bus-" + busCounter;
                                    busCounter++;
                                    break;
                                case "Street":
                                    space.SpaceType = spaceTypeStreet;
                                    space.Name = "Street-" + streetCounter;
                                    streetCounter++;
                                    break;
                                case "Clear":
                                    space.SpaceType = spaceTypeClear;
                                    space.Name = "Clear-" + clearCounter;
                                    clearCounter++;
                                    break;
                            }
                            break;
                        case "1,2":
                            switch (hidden23.Value)
                            {
                                case "Car":
                                    space.SpaceType = spaceTypeCar;
                                    space.Name = "Car-" + carcounter;
                                    carcounter++;
                                    break;
                                case "Motorcycle":
                                    space.SpaceType = spaceTypeMoto;
                                    space.Name = "Motorcycle-" + motoCounter;
                                    motoCounter++;
                                    break;
                                case "Handicap":
                                    space.SpaceType = spaceTypeHandicap;
                                    space.Name = "Handicap-" + handicapCounter;
                                    handicapCounter++;
                                    break;
                                case "Bus":
                                    space.SpaceType = spaceTypeBus;
                                    space.Name = "Bus-" + busCounter;
                                    busCounter++;
                                    break;
                                case "Street":
                                    space.SpaceType = spaceTypeStreet;
                                    space.Name = "Street-" + streetCounter;
                                    streetCounter++;
                                    break;
                                case "Clear":
                                    space.SpaceType = spaceTypeClear;
                                    space.Name = "Clear-" + clearCounter;
                                    clearCounter++;
                                    break;
                            }
                            break;
                        case "1,3":
                            switch (hidden24.Value)
                            {
                                case "Car":
                                    space.SpaceType = spaceTypeCar;
                                    space.Name = "Car-" + carcounter;
                                    carcounter++;
                                    break;
                                case "Motorcycle":
                                    space.SpaceType = spaceTypeMoto;
                                    space.Name = "Motorcycle-" + motoCounter;
                                    motoCounter++;
                                    break;
                                case "Handicap":
                                    space.SpaceType = spaceTypeHandicap;
                                    space.Name = "Handicap-" + handicapCounter;
                                    handicapCounter++;
                                    break;
                                case "Bus":
                                    space.SpaceType = spaceTypeBus;
                                    space.Name = "Bus-" + busCounter;
                                    busCounter++;
                                    break;
                                case "Street":
                                    space.SpaceType = spaceTypeStreet;
                                    space.Name = "Street-" + streetCounter;
                                    streetCounter++;
                                    break;
                                case "Clear":
                                    space.SpaceType = spaceTypeClear;
                                    space.Name = "Clear-" + clearCounter;
                                    clearCounter++;
                                    break;
                            }
                            break;
                        case "1,4":
                            switch (hidden25.Value)
                            {
                                case "Car":
                                    space.SpaceType = spaceTypeCar;
                                    space.Name = "Car-" + carcounter;
                                    carcounter++;
                                    break;
                                case "Motorcycle":
                                    space.SpaceType = spaceTypeMoto;
                                    space.Name = "Motorcycle-" + motoCounter;
                                    motoCounter++;
                                    break;
                                case "Handicap":
                                    space.SpaceType = spaceTypeHandicap;
                                    space.Name = "Handicap-" + handicapCounter;
                                    handicapCounter++;
                                    break;
                                case "Bus":
                                    space.SpaceType = spaceTypeBus;
                                    space.Name = "Bus-" + busCounter;
                                    busCounter++;
                                    break;
                                case "Street":
                                    space.SpaceType = spaceTypeStreet;
                                    space.Name = "Street-" + streetCounter;
                                    streetCounter++;
                                    break;
                                case "Clear":
                                    space.SpaceType = spaceTypeClear;
                                    space.Name = "Clear-" + clearCounter;
                                    clearCounter++;
                                    break;
                            }
                            break;
                        case "1,5":
                            switch (hidden26.Value)
                            {
                                case "Car":
                                    space.SpaceType = spaceTypeCar;
                                    space.Name = "Car-" + carcounter;
                                    carcounter++;
                                    break;
                                case "Motorcycle":
                                    space.SpaceType = spaceTypeMoto;
                                    space.Name = "Motorcycle-" + motoCounter;
                                    motoCounter++;
                                    break;
                                case "Handicap":
                                    space.SpaceType = spaceTypeHandicap;
                                    space.Name = "Handicap-" + handicapCounter;
                                    handicapCounter++;
                                    break;
                                case "Bus":
                                    space.SpaceType = spaceTypeBus;
                                    space.Name = "Bus-" + busCounter;
                                    busCounter++;
                                    break;
                                case "Street":
                                    space.SpaceType = spaceTypeStreet;
                                    space.Name = "Street-" + streetCounter;
                                    streetCounter++;
                                    break;
                                case "Clear":
                                    space.SpaceType = spaceTypeClear;
                                    space.Name = "Clear-" + clearCounter;
                                    clearCounter++;
                                    break;
                            }
                            break;
                        case "1,6":
                            switch (hidden27.Value)
                            {
                                case "Car":
                                    space.SpaceType = spaceTypeCar;
                                    space.Name = "Car-" + carcounter;
                                    carcounter++;
                                    break;
                                case "Motorcycle":
                                    space.SpaceType = spaceTypeMoto;
                                    space.Name = "Motorcycle-" + motoCounter;
                                    motoCounter++;
                                    break;
                                case "Handicap":
                                    space.SpaceType = spaceTypeHandicap;
                                    space.Name = "Handicap-" + handicapCounter;
                                    handicapCounter++;
                                    break;
                                case "Bus":
                                    space.SpaceType = spaceTypeBus;
                                    space.Name = "Bus-" + busCounter;
                                    busCounter++;
                                    break;
                                case "Street":
                                    space.SpaceType = spaceTypeStreet;
                                    space.Name = "Street-" + streetCounter;
                                    streetCounter++;
                                    break;
                                case "Clear":
                                    space.SpaceType = spaceTypeClear;
                                    space.Name = "Clear-" + clearCounter;
                                    clearCounter++;
                                    break;
                            }
                            break;
                        case "1,7":
                            switch (hidden28.Value)
                            {
                                case "Car":
                                    space.SpaceType = spaceTypeCar;
                                    space.Name = "Car-" + carcounter;
                                    carcounter++;
                                    break;
                                case "Motorcycle":
                                    space.SpaceType = spaceTypeMoto;
                                    space.Name = "Motorcycle-" + motoCounter;
                                    motoCounter++;
                                    break;
                                case "Handicap":
                                    space.SpaceType = spaceTypeHandicap;
                                    space.Name = "Handicap-" + handicapCounter;
                                    handicapCounter++;
                                    break;
                                case "Bus":
                                    space.SpaceType = spaceTypeBus;
                                    space.Name = "Bus-" + busCounter;
                                    busCounter++;
                                    break;
                                case "Street":
                                    space.SpaceType = spaceTypeStreet;
                                    space.Name = "Street-" + streetCounter;
                                    streetCounter++;
                                    break;
                                case "Clear":
                                    space.SpaceType = spaceTypeClear;
                                    space.Name = "Clear-" + clearCounter;
                                    clearCounter++;
                                    break;
                            }
                            break;
                        case "1,8":
                            switch (hidden29.Value)
                            {
                                case "Car":
                                    space.SpaceType = spaceTypeCar;
                                    space.Name = "Car-" + carcounter;
                                    carcounter++;
                                    break;
                                case "Motorcycle":
                                    space.SpaceType = spaceTypeMoto;
                                    space.Name = "Motorcycle-" + motoCounter;
                                    motoCounter++;
                                    break;
                                case "Handicap":
                                    space.SpaceType = spaceTypeHandicap;
                                    space.Name = "Handicap-" + handicapCounter;
                                    handicapCounter++;
                                    break;
                                case "Bus":
                                    space.SpaceType = spaceTypeBus;
                                    space.Name = "Bus-" + busCounter;
                                    busCounter++;
                                    break;
                                case "Street":
                                    space.SpaceType = spaceTypeStreet;
                                    space.Name = "Street-" + streetCounter;
                                    streetCounter++;
                                    break;
                                case "Clear":
                                    space.SpaceType = spaceTypeClear;
                                    space.Name = "Clear-" + clearCounter;
                                    clearCounter++;
                                    break;
                            }
                            break;
                        case "1,9":
                            switch (hidden30.Value)
                            {
                                case "Car":
                                    space.SpaceType = spaceTypeCar;
                                    space.Name = "Car-" + carcounter;
                                    carcounter++;
                                    break;
                                case "Motorcycle":
                                    space.SpaceType = spaceTypeMoto;
                                    space.Name = "Motorcycle-" + motoCounter;
                                    motoCounter++;
                                    break;
                                case "Handicap":
                                    space.SpaceType = spaceTypeHandicap;
                                    space.Name = "Handicap-" + handicapCounter;
                                    handicapCounter++;
                                    break;
                                case "Bus":
                                    space.SpaceType = spaceTypeBus;
                                    space.Name = "Bus-" + busCounter;
                                    busCounter++;
                                    break;
                                case "Street":
                                    space.SpaceType = spaceTypeStreet;
                                    space.Name = "Street-" + streetCounter;
                                    streetCounter++;
                                    break;
                                case "Clear":
                                    space.SpaceType = spaceTypeClear;
                                    space.Name = "Clear-" + clearCounter;
                                    clearCounter++;
                                    break;
                            }
                            break;
                        case "1,10":
                            switch (hidden31.Value)
                            {
                                case "Car":
                                    space.SpaceType = spaceTypeCar;
                                    space.Name = "Car-" + carcounter;
                                    carcounter++;
                                    break;
                                case "Motorcycle":
                                    space.SpaceType = spaceTypeMoto;
                                    space.Name = "Motorcycle-" + motoCounter;
                                    motoCounter++;
                                    break;
                                case "Handicap":
                                    space.SpaceType = spaceTypeHandicap;
                                    space.Name = "Handicap-" + handicapCounter;
                                    handicapCounter++;
                                    break;
                                case "Bus":
                                    space.SpaceType = spaceTypeBus;
                                    space.Name = "Bus-" + busCounter;
                                    busCounter++;
                                    break;
                                case "Street":
                                    space.SpaceType = spaceTypeStreet;
                                    space.Name = "Street-" + streetCounter;
                                    streetCounter++;
                                    break;
                                case "Clear":
                                    space.SpaceType = spaceTypeClear;
                                    space.Name = "Clear-" + clearCounter;
                                    clearCounter++;
                                    break;
                            }
                            break;
                        case "1,11":
                            switch (hidden32.Value)
                            {
                                case "Car":
                                    space.SpaceType = spaceTypeCar;
                                    space.Name = "Car-" + carcounter;
                                    carcounter++;
                                    break;
                                case "Motorcycle":
                                    space.SpaceType = spaceTypeMoto;
                                    space.Name = "Motorcycle-" + motoCounter;
                                    motoCounter++;
                                    break;
                                case "Handicap":
                                    space.SpaceType = spaceTypeHandicap;
                                    space.Name = "Handicap-" + handicapCounter;
                                    handicapCounter++;
                                    break;
                                case "Bus":
                                    space.SpaceType = spaceTypeBus;
                                    space.Name = "Bus-" + busCounter;
                                    busCounter++;
                                    break;
                                case "Street":
                                    space.SpaceType = spaceTypeStreet;
                                    space.Name = "Street-" + streetCounter;
                                    streetCounter++;
                                    break;
                                case "Clear":
                                    space.SpaceType = spaceTypeClear;
                                    space.Name = "Clear-" + clearCounter;
                                    clearCounter++;
                                    break;
                            }
                            break;
                        case "1,12":
                            switch (hidden33.Value)
                            {
                                case "Car":
                                    space.SpaceType = spaceTypeCar;
                                    space.Name = "Car-" + carcounter;
                                    carcounter++;
                                    break;
                                case "Motorcycle":
                                    space.SpaceType = spaceTypeMoto;
                                    space.Name = "Motorcycle-" + motoCounter;
                                    motoCounter++;
                                    break;
                                case "Handicap":
                                    space.SpaceType = spaceTypeHandicap;
                                    space.Name = "Handicap-" + handicapCounter;
                                    handicapCounter++;
                                    break;
                                case "Bus":
                                    space.SpaceType = spaceTypeBus;
                                    space.Name = "Bus-" + busCounter;
                                    busCounter++;
                                    break;
                                case "Street":
                                    space.SpaceType = spaceTypeStreet;
                                    space.Name = "Street-" + streetCounter;
                                    streetCounter++;
                                    break;
                                case "Clear":
                                    space.SpaceType = spaceTypeClear;
                                    space.Name = "Clear-" + clearCounter;
                                    clearCounter++;
                                    break;
                            }
                            break;
                        case "1,13":
                            switch (hidden34.Value)
                            {
                                case "Car":
                                    space.SpaceType = spaceTypeCar;
                                    space.Name = "Car-" + carcounter;
                                    carcounter++;
                                    break;
                                case "Motorcycle":
                                    space.SpaceType = spaceTypeMoto;
                                    space.Name = "Motorcycle-" + motoCounter;
                                    motoCounter++;
                                    break;
                                case "Handicap":
                                    space.SpaceType = spaceTypeHandicap;
                                    space.Name = "Handicap-" + handicapCounter;
                                    handicapCounter++;
                                    break;
                                case "Bus":
                                    space.SpaceType = spaceTypeBus;
                                    space.Name = "Bus-" + busCounter;
                                    busCounter++;
                                    break;
                                case "Street":
                                    space.SpaceType = spaceTypeStreet;
                                    space.Name = "Street-" + streetCounter;
                                    streetCounter++;
                                    break;
                                case "Clear":
                                    space.SpaceType = spaceTypeClear;
                                    space.Name = "Clear-" + clearCounter;
                                    clearCounter++;
                                    break;
                            }
                            break;
                        case "1,14":
                            switch (hidden35.Value)
                            {
                                case "Car":
                                    space.SpaceType = spaceTypeCar;
                                    space.Name = "Car-" + carcounter;
                                    carcounter++;
                                    break;
                                case "Motorcycle":
                                    space.SpaceType = spaceTypeMoto;
                                    space.Name = "Motorcycle-" + motoCounter;
                                    motoCounter++;
                                    break;
                                case "Handicap":
                                    space.SpaceType = spaceTypeHandicap;
                                    space.Name = "Handicap-" + handicapCounter;
                                    handicapCounter++;
                                    break;
                                case "Bus":
                                    space.SpaceType = spaceTypeBus;
                                    space.Name = "Bus-" + busCounter;
                                    busCounter++;
                                    break;
                                case "Street":
                                    space.SpaceType = spaceTypeStreet;
                                    space.Name = "Street-" + streetCounter;
                                    streetCounter++;
                                    break;
                                case "Clear":
                                    space.SpaceType = spaceTypeClear;
                                    space.Name = "Clear-" + clearCounter;
                                    clearCounter++;
                                    break;
                            }
                            break;
                        case "1,15":
                            switch (hidden36.Value)
                            {
                                case "Car":
                                    space.SpaceType = spaceTypeCar;
                                    space.Name = "Car-" + carcounter;
                                    carcounter++;
                                    break;
                                case "Motorcycle":
                                    space.SpaceType = spaceTypeMoto;
                                    space.Name = "Motorcycle-" + motoCounter;
                                    motoCounter++;
                                    break;
                                case "Handicap":
                                    space.SpaceType = spaceTypeHandicap;
                                    space.Name = "Handicap-" + handicapCounter;
                                    handicapCounter++;
                                    break;
                                case "Bus":
                                    space.SpaceType = spaceTypeBus;
                                    space.Name = "Bus-" + busCounter;
                                    busCounter++;
                                    break;
                                case "Street":
                                    space.SpaceType = spaceTypeStreet;
                                    space.Name = "Street-" + streetCounter;
                                    streetCounter++;
                                    break;
                                case "Clear":
                                    space.SpaceType = spaceTypeClear;
                                    space.Name = "Clear-" + clearCounter;
                                    clearCounter++;
                                    break;
                            }
                            break;
                        case "1,16":
                            switch (hidden37.Value)
                            {
                                case "Car":
                                    space.SpaceType = spaceTypeCar;
                                    space.Name = "Car-" + carcounter;
                                    carcounter++;
                                    break;
                                case "Motorcycle":
                                    space.SpaceType = spaceTypeMoto;
                                    space.Name = "Motorcycle-" + motoCounter;
                                    motoCounter++;
                                    break;
                                case "Handicap":
                                    space.SpaceType = spaceTypeHandicap;
                                    space.Name = "Handicap-" + handicapCounter;
                                    handicapCounter++;
                                    break;
                                case "Bus":
                                    space.SpaceType = spaceTypeBus;
                                    space.Name = "Bus-" + busCounter;
                                    busCounter++;
                                    break;
                                case "Street":
                                    space.SpaceType = spaceTypeStreet;
                                    space.Name = "Street-" + streetCounter;
                                    streetCounter++;
                                    break;
                                case "Clear":
                                    space.SpaceType = spaceTypeClear;
                                    space.Name = "Clear-" + clearCounter;
                                    clearCounter++;
                                    break;
                            }
                            break;
                        case "1,17":
                            switch (hidden38.Value)
                            {
                                case "Car":
                                    space.SpaceType = spaceTypeCar;
                                    space.Name = "Car-" + carcounter;
                                    carcounter++;
                                    break;
                                case "Motorcycle":
                                    space.SpaceType = spaceTypeMoto;
                                    space.Name = "Motorcycle-" + motoCounter;
                                    motoCounter++;
                                    break;
                                case "Handicap":
                                    space.SpaceType = spaceTypeHandicap;
                                    space.Name = "Handicap-" + handicapCounter;
                                    handicapCounter++;
                                    break;
                                case "Bus":
                                    space.SpaceType = spaceTypeBus;
                                    space.Name = "Bus-" + busCounter;
                                    busCounter++;
                                    break;
                                case "Street":
                                    space.SpaceType = spaceTypeStreet;
                                    space.Name = "Street-" + streetCounter;
                                    streetCounter++;
                                    break;
                                case "Clear":
                                    space.SpaceType = spaceTypeClear;
                                    space.Name = "Clear-" + clearCounter;
                                    clearCounter++;
                                    break;
                            }
                            break;
                        case "1,18":
                            switch (hidden39.Value)
                            {
                                case "Car":
                                    space.SpaceType = spaceTypeCar;
                                    space.Name = "Car-" + carcounter;
                                    carcounter++;
                                    break;
                                case "Motorcycle":
                                    space.SpaceType = spaceTypeMoto;
                                    space.Name = "Motorcycle-" + motoCounter;
                                    motoCounter++;
                                    break;
                                case "Handicap":
                                    space.SpaceType = spaceTypeHandicap;
                                    space.Name = "Handicap-" + handicapCounter;
                                    handicapCounter++;
                                    break;
                                case "Bus":
                                    space.SpaceType = spaceTypeBus;
                                    space.Name = "Bus-" + busCounter;
                                    busCounter++;
                                    break;
                                case "Street":
                                    space.SpaceType = spaceTypeStreet;
                                    space.Name = "Street-" + streetCounter;
                                    streetCounter++;
                                    break;
                                case "Clear":
                                    space.SpaceType = spaceTypeClear;
                                    space.Name = "Clear-" + clearCounter;
                                    clearCounter++;
                                    break;
                            }
                            break;
                        case "1,19":
                            switch (hidden40.Value)
                            {
                                case "Car":
                                    space.SpaceType = spaceTypeCar;
                                    space.Name = "Car-" + carcounter;
                                    carcounter++;
                                    break;
                                case "Motorcycle":
                                    space.SpaceType = spaceTypeMoto;
                                    space.Name = "Motorcycle-" + motoCounter;
                                    motoCounter++;
                                    break;
                                case "Handicap":
                                    space.SpaceType = spaceTypeHandicap;
                                    space.Name = "Handicap-" + handicapCounter;
                                    handicapCounter++;
                                    break;
                                case "Bus":
                                    space.SpaceType = spaceTypeBus;
                                    space.Name = "Bus-" + busCounter;
                                    busCounter++;
                                    break;
                                case "Street":
                                    space.SpaceType = spaceTypeStreet;
                                    space.Name = "Street-" + streetCounter;
                                    streetCounter++;
                                    break;
                                case "Clear":
                                    space.SpaceType = spaceTypeClear;
                                    space.Name = "Clear-" + clearCounter;
                                    clearCounter++;
                                    break;
                            }
                            break;
                        case "2,0":
                            switch (hidden41.Value)
                            {
                                case "Car":
                                    space.SpaceType = spaceTypeCar;
                                    space.Name = "Car-" + carcounter;
                                    carcounter++;
                                    break;
                                case "Motorcycle":
                                    space.SpaceType = spaceTypeMoto;
                                    space.Name = "Motorcycle-" + motoCounter;
                                    motoCounter++;
                                    break;
                                case "Handicap":
                                    space.SpaceType = spaceTypeHandicap;
                                    space.Name = "Handicap-" + handicapCounter;
                                    handicapCounter++;
                                    break;
                                case "Bus":
                                    space.SpaceType = spaceTypeBus;
                                    space.Name = "Bus-" + busCounter;
                                    busCounter++;
                                    break;
                                case "Street":
                                    space.SpaceType = spaceTypeStreet;
                                    space.Name = "Street-" + streetCounter;
                                    streetCounter++;
                                    break;
                                case "Clear":
                                    space.SpaceType = spaceTypeClear;
                                    space.Name = "Clear-" + clearCounter;
                                    clearCounter++;
                                    break;
                            }
                            break;
                        case "2,1":
                            switch (hidden42.Value)
                            {
                                case "Car":
                                    space.SpaceType = spaceTypeCar;
                                    space.Name = "Car-" + carcounter;
                                    carcounter++;
                                    break;
                                case "Motorcycle":
                                    space.SpaceType = spaceTypeMoto;
                                    space.Name = "Motorcycle-" + motoCounter;
                                    motoCounter++;
                                    break;
                                case "Handicap":
                                    space.SpaceType = spaceTypeHandicap;
                                    space.Name = "Handicap-" + handicapCounter;
                                    handicapCounter++;
                                    break;
                                case "Bus":
                                    space.SpaceType = spaceTypeBus;
                                    space.Name = "Bus-" + busCounter;
                                    busCounter++;
                                    break;
                                case "Street":
                                    space.SpaceType = spaceTypeStreet;
                                    space.Name = "Street-" + streetCounter;
                                    streetCounter++;
                                    break;
                                case "Clear":
                                    space.SpaceType = spaceTypeClear;
                                    space.Name = "Clear-" + clearCounter;
                                    clearCounter++;
                                    break;
                            }
                            break;
                        case "2,2":
                            switch (hidden43.Value)
                            {
                                case "Car":
                                    space.SpaceType = spaceTypeCar;
                                    space.Name = "Car-" + carcounter;
                                    carcounter++;
                                    break;
                                case "Motorcycle":
                                    space.SpaceType = spaceTypeMoto;
                                    space.Name = "Motorcycle-" + motoCounter;
                                    motoCounter++;
                                    break;
                                case "Handicap":
                                    space.SpaceType = spaceTypeHandicap;
                                    space.Name = "Handicap-" + handicapCounter;
                                    handicapCounter++;
                                    break;
                                case "Bus":
                                    space.SpaceType = spaceTypeBus;
                                    space.Name = "Bus-" + busCounter;
                                    busCounter++;
                                    break;
                                case "Street":
                                    space.SpaceType = spaceTypeStreet;
                                    space.Name = "Street-" + streetCounter;
                                    streetCounter++;
                                    break;
                                case "Clear":
                                    space.SpaceType = spaceTypeClear;
                                    space.Name = "Clear-" + clearCounter;
                                    clearCounter++;
                                    break;
                            }
                            break;
                        case "2,3":
                            switch (hidden44.Value)
                            {
                                case "Car":
                                    space.SpaceType = spaceTypeCar;
                                    space.Name = "Car-" + carcounter;
                                    carcounter++;
                                    break;
                                case "Motorcycle":
                                    space.SpaceType = spaceTypeMoto;
                                    space.Name = "Motorcycle-" + motoCounter;
                                    motoCounter++;
                                    break;
                                case "Handicap":
                                    space.SpaceType = spaceTypeHandicap;
                                    space.Name = "Handicap-" + handicapCounter;
                                    handicapCounter++;
                                    break;
                                case "Bus":
                                    space.SpaceType = spaceTypeBus;
                                    space.Name = "Bus-" + busCounter;
                                    busCounter++;
                                    break;
                                case "Street":
                                    space.SpaceType = spaceTypeStreet;
                                    space.Name = "Street-" + streetCounter;
                                    streetCounter++;
                                    break;
                                case "Clear":
                                    space.SpaceType = spaceTypeClear;
                                    space.Name = "Clear-" + clearCounter;
                                    clearCounter++;
                                    break;
                            }
                            break;
                        case "2,4":
                            switch (hidden45.Value)
                            {
                                case "Car":
                                    space.SpaceType = spaceTypeCar;
                                    space.Name = "Car-" + carcounter;
                                    carcounter++;
                                    break;
                                case "Motorcycle":
                                    space.SpaceType = spaceTypeMoto;
                                    space.Name = "Motorcycle-" + motoCounter;
                                    motoCounter++;
                                    break;
                                case "Handicap":
                                    space.SpaceType = spaceTypeHandicap;
                                    space.Name = "Handicap-" + handicapCounter;
                                    handicapCounter++;
                                    break;
                                case "Bus":
                                    space.SpaceType = spaceTypeBus;
                                    space.Name = "Bus-" + busCounter;
                                    busCounter++;
                                    break;
                                case "Street":
                                    space.SpaceType = spaceTypeStreet;
                                    space.Name = "Street-" + streetCounter;
                                    streetCounter++;
                                    break;
                                case "Clear":
                                    space.SpaceType = spaceTypeClear;
                                    space.Name = "Clear-" + clearCounter;
                                    clearCounter++;
                                    break;
                            }
                            break;
                        case "2,5":
                            switch (hidden46.Value)
                            {
                                case "Car":
                                    space.SpaceType = spaceTypeCar;
                                    space.Name = "Car-" + carcounter;
                                    carcounter++;
                                    break;
                                case "Motorcycle":
                                    space.SpaceType = spaceTypeMoto;
                                    space.Name = "Motorcycle-" + motoCounter;
                                    motoCounter++;
                                    break;
                                case "Handicap":
                                    space.SpaceType = spaceTypeHandicap;
                                    space.Name = "Handicap-" + handicapCounter;
                                    handicapCounter++;
                                    break;
                                case "Bus":
                                    space.SpaceType = spaceTypeBus;
                                    space.Name = "Bus-" + busCounter;
                                    busCounter++;
                                    break;
                                case "Street":
                                    space.SpaceType = spaceTypeStreet;
                                    space.Name = "Street-" + streetCounter;
                                    streetCounter++;
                                    break;
                                case "Clear":
                                    space.SpaceType = spaceTypeClear;
                                    space.Name = "Clear-" + clearCounter;
                                    clearCounter++;
                                    break;
                            }
                            break;
                        case "2,6":
                            switch (hidden47.Value)
                            {
                                case "Car":
                                    space.SpaceType = spaceTypeCar;
                                    space.Name = "Car-" + carcounter;
                                    carcounter++;
                                    break;
                                case "Motorcycle":
                                    space.SpaceType = spaceTypeMoto;
                                    space.Name = "Motorcycle-" + motoCounter;
                                    motoCounter++;
                                    break;
                                case "Handicap":
                                    space.SpaceType = spaceTypeHandicap;
                                    space.Name = "Handicap-" + handicapCounter;
                                    handicapCounter++;
                                    break;
                                case "Bus":
                                    space.SpaceType = spaceTypeBus;
                                    space.Name = "Bus-" + busCounter;
                                    busCounter++;
                                    break;
                                case "Street":
                                    space.SpaceType = spaceTypeStreet;
                                    space.Name = "Street-" + streetCounter;
                                    streetCounter++;
                                    break;
                                case "Clear":
                                    space.SpaceType = spaceTypeClear;
                                    space.Name = "Clear-" + clearCounter;
                                    clearCounter++;
                                    break;
                            }
                            break;
                        case "2,7":
                            switch (hidden48.Value)
                            {
                                case "Car":
                                    space.SpaceType = spaceTypeCar;
                                    space.Name = "Car-" + carcounter;
                                    carcounter++;
                                    break;
                                case "Motorcycle":
                                    space.SpaceType = spaceTypeMoto;
                                    space.Name = "Motorcycle-" + motoCounter;
                                    motoCounter++;
                                    break;
                                case "Handicap":
                                    space.SpaceType = spaceTypeHandicap;
                                    space.Name = "Handicap-" + handicapCounter;
                                    handicapCounter++;
                                    break;
                                case "Bus":
                                    space.SpaceType = spaceTypeBus;
                                    space.Name = "Bus-" + busCounter;
                                    busCounter++;
                                    break;
                                case "Street":
                                    space.SpaceType = spaceTypeStreet;
                                    space.Name = "Street-" + streetCounter;
                                    streetCounter++;
                                    break;
                                case "Clear":
                                    space.SpaceType = spaceTypeClear;
                                    space.Name = "Clear-" + clearCounter;
                                    clearCounter++;
                                    break;
                            }
                            break;
                        case "2,8":
                            switch (hidden49.Value)
                            {
                                case "Car":
                                    space.SpaceType = spaceTypeCar;
                                    space.Name = "Car-" + carcounter;
                                    carcounter++;
                                    break;
                                case "Motorcycle":
                                    space.SpaceType = spaceTypeMoto;
                                    space.Name = "Motorcycle-" + motoCounter;
                                    motoCounter++;
                                    break;
                                case "Handicap":
                                    space.SpaceType = spaceTypeHandicap;
                                    space.Name = "Handicap-" + handicapCounter;
                                    handicapCounter++;
                                    break;
                                case "Bus":
                                    space.SpaceType = spaceTypeBus;
                                    space.Name = "Bus-" + busCounter;
                                    busCounter++;
                                    break;
                                case "Street":
                                    space.SpaceType = spaceTypeStreet;
                                    space.Name = "Street-" + streetCounter;
                                    streetCounter++;
                                    break;
                                case "Clear":
                                    space.SpaceType = spaceTypeClear;
                                    space.Name = "Clear-" + clearCounter;
                                    clearCounter++;
                                    break;
                            }
                            break;
                        case "2,9":
                            switch (hidden50.Value)
                            {
                                case "Car":
                                    space.SpaceType = spaceTypeCar;
                                    space.Name = "Car-" + carcounter;
                                    carcounter++;
                                    break;
                                case "Motorcycle":
                                    space.SpaceType = spaceTypeMoto;
                                    space.Name = "Motorcycle-" + motoCounter;
                                    motoCounter++;
                                    break;
                                case "Handicap":
                                    space.SpaceType = spaceTypeHandicap;
                                    space.Name = "Handicap-" + handicapCounter;
                                    handicapCounter++;
                                    break;
                                case "Bus":
                                    space.SpaceType = spaceTypeBus;
                                    space.Name = "Bus-" + busCounter;
                                    busCounter++;
                                    break;
                                case "Street":
                                    space.SpaceType = spaceTypeStreet;
                                    space.Name = "Street-" + streetCounter;
                                    streetCounter++;
                                    break;
                                case "Clear":
                                    space.SpaceType = spaceTypeClear;
                                    space.Name = "Clear-" + clearCounter;
                                    clearCounter++;
                                    break;
                            }
                            break;
                        case "2,10":
                            switch (hidden51.Value)
                            {
                                case "Car":
                                    space.SpaceType = spaceTypeCar;
                                    space.Name = "Car-" + carcounter;
                                    carcounter++;
                                    break;
                                case "Motorcycle":
                                    space.SpaceType = spaceTypeMoto;
                                    space.Name = "Motorcycle-" + motoCounter;
                                    motoCounter++;
                                    break;
                                case "Handicap":
                                    space.SpaceType = spaceTypeHandicap;
                                    space.Name = "Handicap-" + handicapCounter;
                                    handicapCounter++;
                                    break;
                                case "Bus":
                                    space.SpaceType = spaceTypeBus;
                                    space.Name = "Bus-" + busCounter;
                                    busCounter++;
                                    break;
                                case "Street":
                                    space.SpaceType = spaceTypeStreet;
                                    space.Name = "Street-" + streetCounter;
                                    streetCounter++;
                                    break;
                                case "Clear":
                                    space.SpaceType = spaceTypeClear;
                                    space.Name = "Clear-" + clearCounter;
                                    clearCounter++;
                                    break;
                            }
                            break;
                        case "2,11":
                            switch (hidden52.Value)
                            {
                                case "Car":
                                    space.SpaceType = spaceTypeCar;
                                    space.Name = "Car-" + carcounter;
                                    carcounter++;
                                    break;
                                case "Motorcycle":
                                    space.SpaceType = spaceTypeMoto;
                                    space.Name = "Motorcycle-" + motoCounter;
                                    motoCounter++;
                                    break;
                                case "Handicap":
                                    space.SpaceType = spaceTypeHandicap;
                                    space.Name = "Handicap-" + handicapCounter;
                                    handicapCounter++;
                                    break;
                                case "Bus":
                                    space.SpaceType = spaceTypeBus;
                                    space.Name = "Bus-" + busCounter;
                                    busCounter++;
                                    break;
                                case "Street":
                                    space.SpaceType = spaceTypeStreet;
                                    space.Name = "Street-" + streetCounter;
                                    streetCounter++;
                                    break;
                                case "Clear":
                                    space.SpaceType = spaceTypeClear;
                                    space.Name = "Clear-" + clearCounter;
                                    clearCounter++;
                                    break;
                            }
                            break;
                        case "2,12":
                            switch (hidden53.Value)
                            {
                                case "Car":
                                    space.SpaceType = spaceTypeCar;
                                    space.Name = "Car-" + carcounter;
                                    carcounter++;
                                    break;
                                case "Motorcycle":
                                    space.SpaceType = spaceTypeMoto;
                                    space.Name = "Motorcycle-" + motoCounter;
                                    motoCounter++;
                                    break;
                                case "Handicap":
                                    space.SpaceType = spaceTypeHandicap;
                                    space.Name = "Handicap-" + handicapCounter;
                                    handicapCounter++;
                                    break;
                                case "Bus":
                                    space.SpaceType = spaceTypeBus;
                                    space.Name = "Bus-" + busCounter;
                                    busCounter++;
                                    break;
                                case "Street":
                                    space.SpaceType = spaceTypeStreet;
                                    space.Name = "Street-" + streetCounter;
                                    streetCounter++;
                                    break;
                                case "Clear":
                                    space.SpaceType = spaceTypeClear;
                                    space.Name = "Clear-" + clearCounter;
                                    clearCounter++;
                                    break;
                            }
                            break;
                        case "2,13":
                            switch (hidden54.Value)
                            {
                                case "Car":
                                    space.SpaceType = spaceTypeCar;
                                    space.Name = "Car-" + carcounter;
                                    carcounter++;
                                    break;
                                case "Motorcycle":
                                    space.SpaceType = spaceTypeMoto;
                                    space.Name = "Motorcycle-" + motoCounter;
                                    motoCounter++;
                                    break;
                                case "Handicap":
                                    space.SpaceType = spaceTypeHandicap;
                                    space.Name = "Handicap-" + handicapCounter;
                                    handicapCounter++;
                                    break;
                                case "Bus":
                                    space.SpaceType = spaceTypeBus;
                                    space.Name = "Bus-" + busCounter;
                                    busCounter++;
                                    break;
                                case "Street":
                                    space.SpaceType = spaceTypeStreet;
                                    space.Name = "Street-" + streetCounter;
                                    streetCounter++;
                                    break;
                                case "Clear":
                                    space.SpaceType = spaceTypeClear;
                                    space.Name = "Clear-" + clearCounter;
                                    clearCounter++;
                                    break;
                            }
                            break;
                        case "2,14":
                            switch (hidden55.Value)
                            {
                                case "Car":
                                    space.SpaceType = spaceTypeCar;
                                    space.Name = "Car-" + carcounter;
                                    carcounter++;
                                    break;
                                case "Motorcycle":
                                    space.SpaceType = spaceTypeMoto;
                                    space.Name = "Motorcycle-" + motoCounter;
                                    motoCounter++;
                                    break;
                                case "Handicap":
                                    space.SpaceType = spaceTypeHandicap;
                                    space.Name = "Handicap-" + handicapCounter;
                                    handicapCounter++;
                                    break;
                                case "Bus":
                                    space.SpaceType = spaceTypeBus;
                                    space.Name = "Bus-" + busCounter;
                                    busCounter++;
                                    break;
                                case "Street":
                                    space.SpaceType = spaceTypeStreet;
                                    space.Name = "Street-" + streetCounter;
                                    streetCounter++;
                                    break;
                                case "Clear":
                                    space.SpaceType = spaceTypeClear;
                                    space.Name = "Clear-" + clearCounter;
                                    clearCounter++;
                                    break;
                            }
                            break;
                        case "2,15":
                            switch (hidden56.Value)
                            {
                                case "Car":
                                    space.SpaceType = spaceTypeCar;
                                    space.Name = "Car-" + carcounter;
                                    carcounter++;
                                    break;
                                case "Motorcycle":
                                    space.SpaceType = spaceTypeMoto;
                                    space.Name = "Motorcycle-" + motoCounter;
                                    motoCounter++;
                                    break;
                                case "Handicap":
                                    space.SpaceType = spaceTypeHandicap;
                                    space.Name = "Handicap-" + handicapCounter;
                                    handicapCounter++;
                                    break;
                                case "Bus":
                                    space.SpaceType = spaceTypeBus;
                                    space.Name = "Bus-" + busCounter;
                                    busCounter++;
                                    break;
                                case "Street":
                                    space.SpaceType = spaceTypeStreet;
                                    space.Name = "Street-" + streetCounter;
                                    streetCounter++;
                                    break;
                                case "Clear":
                                    space.SpaceType = spaceTypeClear;
                                    space.Name = "Clear-" + clearCounter;
                                    clearCounter++;
                                    break;
                            }
                            break;
                        case "2,16":
                            switch (hidden57.Value)
                            {
                                case "Car":
                                    space.SpaceType = spaceTypeCar;
                                    space.Name = "Car-" + carcounter;
                                    carcounter++;
                                    break;
                                case "Motorcycle":
                                    space.SpaceType = spaceTypeMoto;
                                    space.Name = "Motorcycle-" + motoCounter;
                                    motoCounter++;
                                    break;
                                case "Handicap":
                                    space.SpaceType = spaceTypeHandicap;
                                    space.Name = "Handicap-" + handicapCounter;
                                    handicapCounter++;
                                    break;
                                case "Bus":
                                    space.SpaceType = spaceTypeBus;
                                    space.Name = "Bus-" + busCounter;
                                    busCounter++;
                                    break;
                                case "Street":
                                    space.SpaceType = spaceTypeStreet;
                                    space.Name = "Street-" + streetCounter;
                                    streetCounter++;
                                    break;
                                case "Clear":
                                    space.SpaceType = spaceTypeClear;
                                    space.Name = "Clear-" + clearCounter;
                                    clearCounter++;
                                    break;
                            }
                            break;
                        case "2,17":
                            switch (hidden58.Value)
                            {
                                case "Car":
                                    space.SpaceType = spaceTypeCar;
                                    space.Name = "Car-" + carcounter;
                                    carcounter++;
                                    break;
                                case "Motorcycle":
                                    space.SpaceType = spaceTypeMoto;
                                    space.Name = "Motorcycle-" + motoCounter;
                                    motoCounter++;
                                    break;
                                case "Handicap":
                                    space.SpaceType = spaceTypeHandicap;
                                    space.Name = "Handicap-" + handicapCounter;
                                    handicapCounter++;
                                    break;
                                case "Bus":
                                    space.SpaceType = spaceTypeBus;
                                    space.Name = "Bus-" + busCounter;
                                    busCounter++;
                                    break;
                                case "Street":
                                    space.SpaceType = spaceTypeStreet;
                                    space.Name = "Street-" + streetCounter;
                                    streetCounter++;
                                    break;
                                case "Clear":
                                    space.SpaceType = spaceTypeClear;
                                    space.Name = "Clear-" + clearCounter;
                                    clearCounter++;
                                    break;
                            }
                            break;
                        case "2,18":
                            switch (hidden59.Value)
                            {
                                case "Car":
                                    space.SpaceType = spaceTypeCar;
                                    space.Name = "Car-" + carcounter;
                                    carcounter++;
                                    break;
                                case "Motorcycle":
                                    space.SpaceType = spaceTypeMoto;
                                    space.Name = "Motorcycle-" + motoCounter;
                                    motoCounter++;
                                    break;
                                case "Handicap":
                                    space.SpaceType = spaceTypeHandicap;
                                    space.Name = "Handicap-" + handicapCounter;
                                    handicapCounter++;
                                    break;
                                case "Bus":
                                    space.SpaceType = spaceTypeBus;
                                    space.Name = "Bus-" + busCounter;
                                    busCounter++;
                                    break;
                                case "Street":
                                    space.SpaceType = spaceTypeStreet;
                                    space.Name = "Street-" + streetCounter;
                                    streetCounter++;
                                    break;
                                case "Clear":
                                    space.SpaceType = spaceTypeClear;
                                    space.Name = "Clear-" + clearCounter;
                                    clearCounter++;
                                    break;
                            }
                            break;
                        case "2,19":
                            switch (hidden60.Value)
                            {
                                case "Car":
                                    space.SpaceType = spaceTypeCar;
                                    space.Name = "Car-" + carcounter;
                                    carcounter++;
                                    break;
                                case "Motorcycle":
                                    space.SpaceType = spaceTypeMoto;
                                    space.Name = "Motorcycle-" + motoCounter;
                                    motoCounter++;
                                    break;
                                case "Handicap":
                                    space.SpaceType = spaceTypeHandicap;
                                    space.Name = "Handicap-" + handicapCounter;
                                    handicapCounter++;
                                    break;
                                case "Bus":
                                    space.SpaceType = spaceTypeBus;
                                    space.Name = "Bus-" + busCounter;
                                    busCounter++;
                                    break;
                                case "Street":
                                    space.SpaceType = spaceTypeStreet;
                                    space.Name = "Street-" + streetCounter;
                                    streetCounter++;
                                    break;
                                case "Clear":
                                    space.SpaceType = spaceTypeClear;
                                    space.Name = "Clear-" + clearCounter;
                                    clearCounter++;
                                    break;
                            }
                            break;
                        case "3,0":
                            switch (hidden61.Value)
                            {
                                case "Car":
                                    space.SpaceType = spaceTypeCar;
                                    space.Name = "Car-" + carcounter;
                                    carcounter++;
                                    break;
                                case "Motorcycle":
                                    space.SpaceType = spaceTypeMoto;
                                    space.Name = "Motorcycle-" + motoCounter;
                                    motoCounter++;
                                    break;
                                case "Handicap":
                                    space.SpaceType = spaceTypeHandicap;
                                    space.Name = "Handicap-" + handicapCounter;
                                    handicapCounter++;
                                    break;
                                case "Bus":
                                    space.SpaceType = spaceTypeBus;
                                    space.Name = "Bus-" + busCounter;
                                    busCounter++;
                                    break;
                                case "Street":
                                    space.SpaceType = spaceTypeStreet;
                                    space.Name = "Street-" + streetCounter;
                                    streetCounter++;
                                    break;
                                case "Clear":
                                    space.SpaceType = spaceTypeClear;
                                    space.Name = "Clear-" + clearCounter;
                                    clearCounter++;
                                    break;
                            }
                            break;
                        case "3,1":
                            switch (hidden62.Value)
                            {
                                case "Car":
                                    space.SpaceType = spaceTypeCar;
                                    space.Name = "Car-" + carcounter;
                                    carcounter++;
                                    break;
                                case "Motorcycle":
                                    space.SpaceType = spaceTypeMoto;
                                    space.Name = "Motorcycle-" + motoCounter;
                                    motoCounter++;
                                    break;
                                case "Handicap":
                                    space.SpaceType = spaceTypeHandicap;
                                    space.Name = "Handicap-" + handicapCounter;
                                    handicapCounter++;
                                    break;
                                case "Bus":
                                    space.SpaceType = spaceTypeBus;
                                    space.Name = "Bus-" + busCounter;
                                    busCounter++;
                                    break;
                                case "Street":
                                    space.SpaceType = spaceTypeStreet;
                                    space.Name = "Street-" + streetCounter;
                                    streetCounter++;
                                    break;
                                case "Clear":
                                    space.SpaceType = spaceTypeClear;
                                    space.Name = "Clear-" + clearCounter;
                                    clearCounter++;
                                    break;
                            }
                            break;
                        case "3,2":
                            switch (hidden63.Value)
                            {
                                case "Car":
                                    space.SpaceType = spaceTypeCar;
                                    space.Name = "Car-" + carcounter;
                                    carcounter++;
                                    break;
                                case "Motorcycle":
                                    space.SpaceType = spaceTypeMoto;
                                    space.Name = "Motorcycle-" + motoCounter;
                                    motoCounter++;
                                    break;
                                case "Handicap":
                                    space.SpaceType = spaceTypeHandicap;
                                    space.Name = "Handicap-" + handicapCounter;
                                    handicapCounter++;
                                    break;
                                case "Bus":
                                    space.SpaceType = spaceTypeBus;
                                    space.Name = "Bus-" + busCounter;
                                    busCounter++;
                                    break;
                                case "Street":
                                    space.SpaceType = spaceTypeStreet;
                                    space.Name = "Street-" + streetCounter;
                                    streetCounter++;
                                    break;
                                case "Clear":
                                    space.SpaceType = spaceTypeClear;
                                    space.Name = "Clear-" + clearCounter;
                                    clearCounter++;
                                    break;
                            }
                            break;
                        case "3,3":
                            switch (hidden64.Value)
                            {
                                case "Car":
                                    space.SpaceType = spaceTypeCar;
                                    space.Name = "Car-" + carcounter;
                                    carcounter++;
                                    break;
                                case "Motorcycle":
                                    space.SpaceType = spaceTypeMoto;
                                    space.Name = "Motorcycle-" + motoCounter;
                                    motoCounter++;
                                    break;
                                case "Handicap":
                                    space.SpaceType = spaceTypeHandicap;
                                    space.Name = "Handicap-" + handicapCounter;
                                    handicapCounter++;
                                    break;
                                case "Bus":
                                    space.SpaceType = spaceTypeBus;
                                    space.Name = "Bus-" + busCounter;
                                    busCounter++;
                                    break;
                                case "Street":
                                    space.SpaceType = spaceTypeStreet;
                                    space.Name = "Street-" + streetCounter;
                                    streetCounter++;
                                    break;
                                case "Clear":
                                    space.SpaceType = spaceTypeClear;
                                    space.Name = "Clear-" + clearCounter;
                                    clearCounter++;
                                    break;
                            }
                            break;
                        case "3,4":
                            switch (hidden65.Value)
                            {
                                case "Car":
                                    space.SpaceType = spaceTypeCar;
                                    space.Name = "Car-" + carcounter;
                                    carcounter++;
                                    break;
                                case "Motorcycle":
                                    space.SpaceType = spaceTypeMoto;
                                    space.Name = "Motorcycle-" + motoCounter;
                                    motoCounter++;
                                    break;
                                case "Handicap":
                                    space.SpaceType = spaceTypeHandicap;
                                    space.Name = "Handicap-" + handicapCounter;
                                    handicapCounter++;
                                    break;
                                case "Bus":
                                    space.SpaceType = spaceTypeBus;
                                    space.Name = "Bus-" + busCounter;
                                    busCounter++;
                                    break;
                                case "Street":
                                    space.SpaceType = spaceTypeStreet;
                                    space.Name = "Street-" + streetCounter;
                                    streetCounter++;
                                    break;
                                case "Clear":
                                    space.SpaceType = spaceTypeClear;
                                    space.Name = "Clear-" + clearCounter;
                                    clearCounter++;
                                    break;
                            }
                            break;
                        case "3,5":
                            switch (hidden66.Value)
                            {
                                case "Car":
                                    space.SpaceType = spaceTypeCar;
                                    space.Name = "Car-" + carcounter;
                                    carcounter++;
                                    break;
                                case "Motorcycle":
                                    space.SpaceType = spaceTypeMoto;
                                    space.Name = "Motorcycle-" + motoCounter;
                                    motoCounter++;
                                    break;
                                case "Handicap":
                                    space.SpaceType = spaceTypeHandicap;
                                    space.Name = "Handicap-" + handicapCounter;
                                    handicapCounter++;
                                    break;
                                case "Bus":
                                    space.SpaceType = spaceTypeBus;
                                    space.Name = "Bus-" + busCounter;
                                    busCounter++;
                                    break;
                                case "Street":
                                    space.SpaceType = spaceTypeStreet;
                                    space.Name = "Street-" + streetCounter;
                                    streetCounter++;
                                    break;
                                case "Clear":
                                    space.SpaceType = spaceTypeClear;
                                    space.Name = "Clear-" + clearCounter;
                                    clearCounter++;
                                    break;
                            }
                            break;
                        case "3,6":
                            switch (hidden67.Value)
                            {
                                case "Car":
                                    space.SpaceType = spaceTypeCar;
                                    space.Name = "Car-" + carcounter;
                                    carcounter++;
                                    break;
                                case "Motorcycle":
                                    space.SpaceType = spaceTypeMoto;
                                    space.Name = "Motorcycle-" + motoCounter;
                                    motoCounter++;
                                    break;
                                case "Handicap":
                                    space.SpaceType = spaceTypeHandicap;
                                    space.Name = "Handicap-" + handicapCounter;
                                    handicapCounter++;
                                    break;
                                case "Bus":
                                    space.SpaceType = spaceTypeBus;
                                    space.Name = "Bus-" + busCounter;
                                    busCounter++;
                                    break;
                                case "Street":
                                    space.SpaceType = spaceTypeStreet;
                                    space.Name = "Street-" + streetCounter;
                                    streetCounter++;
                                    break;
                                case "Clear":
                                    space.SpaceType = spaceTypeClear;
                                    space.Name = "Clear-" + clearCounter;
                                    clearCounter++;
                                    break;
                            }
                            break;
                        case "3,7":
                            switch (hidden68.Value)
                            {
                                case "Car":
                                    space.SpaceType = spaceTypeCar;
                                    space.Name = "Car-" + carcounter;
                                    carcounter++;
                                    break;
                                case "Motorcycle":
                                    space.SpaceType = spaceTypeMoto;
                                    space.Name = "Motorcycle-" + motoCounter;
                                    motoCounter++;
                                    break;
                                case "Handicap":
                                    space.SpaceType = spaceTypeHandicap;
                                    space.Name = "Handicap-" + handicapCounter;
                                    handicapCounter++;
                                    break;
                                case "Bus":
                                    space.SpaceType = spaceTypeBus;
                                    space.Name = "Bus-" + busCounter;
                                    busCounter++;
                                    break;
                                case "Street":
                                    space.SpaceType = spaceTypeStreet;
                                    space.Name = "Street-" + streetCounter;
                                    streetCounter++;
                                    break;
                                case "Clear":
                                    space.SpaceType = spaceTypeClear;
                                    space.Name = "Clear-" + clearCounter;
                                    clearCounter++;
                                    break;
                            }
                            break;
                        case "3,8":
                            switch (hidden69.Value)
                            {
                                case "Car":
                                    space.SpaceType = spaceTypeCar;
                                    space.Name = "Car-" + carcounter;
                                    carcounter++;
                                    break;
                                case "Motorcycle":
                                    space.SpaceType = spaceTypeMoto;
                                    space.Name = "Motorcycle-" + motoCounter;
                                    motoCounter++;
                                    break;
                                case "Handicap":
                                    space.SpaceType = spaceTypeHandicap;
                                    space.Name = "Handicap-" + handicapCounter;
                                    handicapCounter++;
                                    break;
                                case "Bus":
                                    space.SpaceType = spaceTypeBus;
                                    space.Name = "Bus-" + busCounter;
                                    busCounter++;
                                    break;
                                case "Street":
                                    space.SpaceType = spaceTypeStreet;
                                    space.Name = "Street-" + streetCounter;
                                    streetCounter++;
                                    break;
                                case "Clear":
                                    space.SpaceType = spaceTypeClear;
                                    space.Name = "Clear-" + clearCounter;
                                    clearCounter++;
                                    break;
                            }
                            break;
                        case "3,9":
                            switch (hidden70.Value)
                            {
                                case "Car":
                                    space.SpaceType = spaceTypeCar;
                                    space.Name = "Car-" + carcounter;
                                    carcounter++;
                                    break;
                                case "Motorcycle":
                                    space.SpaceType = spaceTypeMoto;
                                    space.Name = "Motorcycle-" + motoCounter;
                                    motoCounter++;
                                    break;
                                case "Handicap":
                                    space.SpaceType = spaceTypeHandicap;
                                    space.Name = "Handicap-" + handicapCounter;
                                    handicapCounter++;
                                    break;
                                case "Bus":
                                    space.SpaceType = spaceTypeBus;
                                    space.Name = "Bus-" + busCounter;
                                    busCounter++;
                                    break;
                                case "Street":
                                    space.SpaceType = spaceTypeStreet;
                                    space.Name = "Street-" + streetCounter;
                                    streetCounter++;
                                    break;
                                case "Clear":
                                    space.SpaceType = spaceTypeClear;
                                    space.Name = "Clear-" + clearCounter;
                                    clearCounter++;
                                    break;
                            }
                            break;
                        case "3,10":
                            switch (hidden71.Value)
                            {
                                case "Car":
                                    space.SpaceType = spaceTypeCar;
                                    space.Name = "Car-" + carcounter;
                                    carcounter++;
                                    break;
                                case "Motorcycle":
                                    space.SpaceType = spaceTypeMoto;
                                    space.Name = "Motorcycle-" + motoCounter;
                                    motoCounter++;
                                    break;
                                case "Handicap":
                                    space.SpaceType = spaceTypeHandicap;
                                    space.Name = "Handicap-" + handicapCounter;
                                    handicapCounter++;
                                    break;
                                case "Bus":
                                    space.SpaceType = spaceTypeBus;
                                    space.Name = "Bus-" + busCounter;
                                    busCounter++;
                                    break;
                                case "Street":
                                    space.SpaceType = spaceTypeStreet;
                                    space.Name = "Street-" + streetCounter;
                                    streetCounter++;
                                    break;
                                case "Clear":
                                    space.SpaceType = spaceTypeClear;
                                    space.Name = "Clear-" + clearCounter;
                                    clearCounter++;
                                    break;
                            }
                            break;
                        case "3,11":
                            switch (hidden72.Value)
                            {
                                case "Car":
                                    space.SpaceType = spaceTypeCar;
                                    space.Name = "Car-" + carcounter;
                                    carcounter++;
                                    break;
                                case "Motorcycle":
                                    space.SpaceType = spaceTypeMoto;
                                    space.Name = "Motorcycle-" + motoCounter;
                                    motoCounter++;
                                    break;
                                case "Handicap":
                                    space.SpaceType = spaceTypeHandicap;
                                    space.Name = "Handicap-" + handicapCounter;
                                    handicapCounter++;
                                    break;
                                case "Bus":
                                    space.SpaceType = spaceTypeBus;
                                    space.Name = "Bus-" + busCounter;
                                    busCounter++;
                                    break;
                                case "Street":
                                    space.SpaceType = spaceTypeStreet;
                                    space.Name = "Street-" + streetCounter;
                                    streetCounter++;
                                    break;
                                case "Clear":
                                    space.SpaceType = spaceTypeClear;
                                    space.Name = "Clear-" + clearCounter;
                                    clearCounter++;
                                    break;
                            }
                            break;
                        case "3,12":
                            switch (hidden73.Value)
                            {
                                case "Car":
                                    space.SpaceType = spaceTypeCar;
                                    space.Name = "Car-" + carcounter;
                                    carcounter++;
                                    break;
                                case "Motorcycle":
                                    space.SpaceType = spaceTypeMoto;
                                    space.Name = "Motorcycle-" + motoCounter;
                                    motoCounter++;
                                    break;
                                case "Handicap":
                                    space.SpaceType = spaceTypeHandicap;
                                    space.Name = "Handicap-" + handicapCounter;
                                    handicapCounter++;
                                    break;
                                case "Bus":
                                    space.SpaceType = spaceTypeBus;
                                    space.Name = "Bus-" + busCounter;
                                    busCounter++;
                                    break;
                                case "Street":
                                    space.SpaceType = spaceTypeStreet;
                                    space.Name = "Street-" + streetCounter;
                                    streetCounter++;
                                    break;
                                case "Clear":
                                    space.SpaceType = spaceTypeClear;
                                    space.Name = "Clear-" + clearCounter;
                                    clearCounter++;
                                    break;
                            }
                            break;
                        case "3,13":
                            switch (hidden74.Value)
                            {
                                case "Car":
                                    space.SpaceType = spaceTypeCar;
                                    space.Name = "Car-" + carcounter;
                                    carcounter++;
                                    break;
                                case "Motorcycle":
                                    space.SpaceType = spaceTypeMoto;
                                    space.Name = "Motorcycle-" + motoCounter;
                                    motoCounter++;
                                    break;
                                case "Handicap":
                                    space.SpaceType = spaceTypeHandicap;
                                    space.Name = "Handicap-" + handicapCounter;
                                    handicapCounter++;
                                    break;
                                case "Bus":
                                    space.SpaceType = spaceTypeBus;
                                    space.Name = "Bus-" + busCounter;
                                    busCounter++;
                                    break;
                                case "Street":
                                    space.SpaceType = spaceTypeStreet;
                                    space.Name = "Street-" + streetCounter;
                                    streetCounter++;
                                    break;
                                case "Clear":
                                    space.SpaceType = spaceTypeClear;
                                    space.Name = "Clear-" + clearCounter;
                                    clearCounter++;
                                    break;
                            }
                            break;
                        case "3,14":
                            switch (hidden75.Value)
                            {
                                case "Car":
                                    space.SpaceType = spaceTypeCar;
                                    space.Name = "Car-" + carcounter;
                                    carcounter++;
                                    break;
                                case "Motorcycle":
                                    space.SpaceType = spaceTypeMoto;
                                    space.Name = "Motorcycle-" + motoCounter;
                                    motoCounter++;
                                    break;
                                case "Handicap":
                                    space.SpaceType = spaceTypeHandicap;
                                    space.Name = "Handicap-" + handicapCounter;
                                    handicapCounter++;
                                    break;
                                case "Bus":
                                    space.SpaceType = spaceTypeBus;
                                    space.Name = "Bus-" + busCounter;
                                    busCounter++;
                                    break;
                                case "Street":
                                    space.SpaceType = spaceTypeStreet;
                                    space.Name = "Street-" + streetCounter;
                                    streetCounter++;
                                    break;
                                case "Clear":
                                    space.SpaceType = spaceTypeClear;
                                    space.Name = "Clear-" + clearCounter;
                                    clearCounter++;
                                    break;
                            }
                            break;
                        case "3,15":
                            switch (hidden76.Value)
                            {
                                case "Car":
                                    space.SpaceType = spaceTypeCar;
                                    space.Name = "Car-" + carcounter;
                                    carcounter++;
                                    break;
                                case "Motorcycle":
                                    space.SpaceType = spaceTypeMoto;
                                    space.Name = "Motorcycle-" + motoCounter;
                                    motoCounter++;
                                    break;
                                case "Handicap":
                                    space.SpaceType = spaceTypeHandicap;
                                    space.Name = "Handicap-" + handicapCounter;
                                    handicapCounter++;
                                    break;
                                case "Bus":
                                    space.SpaceType = spaceTypeBus;
                                    space.Name = "Bus-" + busCounter;
                                    busCounter++;
                                    break;
                                case "Street":
                                    space.SpaceType = spaceTypeStreet;
                                    space.Name = "Street-" + streetCounter;
                                    streetCounter++;
                                    break;
                                case "Clear":
                                    space.SpaceType = spaceTypeClear;
                                    space.Name = "Clear-" + clearCounter;
                                    clearCounter++;
                                    break;
                            }
                            break;
                        case "3,16":
                            switch (hidden77.Value)
                            {
                                case "Car":
                                    space.SpaceType = spaceTypeCar;
                                    space.Name = "Car-" + carcounter;
                                    carcounter++;
                                    break;
                                case "Motorcycle":
                                    space.SpaceType = spaceTypeMoto;
                                    space.Name = "Motorcycle-" + motoCounter;
                                    motoCounter++;
                                    break;
                                case "Handicap":
                                    space.SpaceType = spaceTypeHandicap;
                                    space.Name = "Handicap-" + handicapCounter;
                                    handicapCounter++;
                                    break;
                                case "Bus":
                                    space.SpaceType = spaceTypeBus;
                                    space.Name = "Bus-" + busCounter;
                                    busCounter++;
                                    break;
                                case "Street":
                                    space.SpaceType = spaceTypeStreet;
                                    space.Name = "Street-" + streetCounter;
                                    streetCounter++;
                                    break;
                                case "Clear":
                                    space.SpaceType = spaceTypeClear;
                                    space.Name = "Clear-" + clearCounter;
                                    clearCounter++;
                                    break;
                            }
                            break;
                        case "3,17":
                            switch (hidden78.Value)
                            {
                                case "Car":
                                    space.SpaceType = spaceTypeCar;
                                    space.Name = "Car-" + carcounter;
                                    carcounter++;
                                    break;
                                case "Motorcycle":
                                    space.SpaceType = spaceTypeMoto;
                                    space.Name = "Motorcycle-" + motoCounter;
                                    motoCounter++;
                                    break;
                                case "Handicap":
                                    space.SpaceType = spaceTypeHandicap;
                                    space.Name = "Handicap-" + handicapCounter;
                                    handicapCounter++;
                                    break;
                                case "Bus":
                                    space.SpaceType = spaceTypeBus;
                                    space.Name = "Bus-" + busCounter;
                                    busCounter++;
                                    break;
                                case "Street":
                                    space.SpaceType = spaceTypeStreet;
                                    space.Name = "Street-" + streetCounter;
                                    streetCounter++;
                                    break;
                                case "Clear":
                                    space.SpaceType = spaceTypeClear;
                                    space.Name = "Clear-" + clearCounter;
                                    clearCounter++;
                                    break;
                            }
                            break;
                        case "3,18":
                            switch (hidden79.Value)
                            {
                                case "Car":
                                    space.SpaceType = spaceTypeCar;
                                    space.Name = "Car-" + carcounter;
                                    carcounter++;
                                    break;
                                case "Motorcycle":
                                    space.SpaceType = spaceTypeMoto;
                                    space.Name = "Motorcycle-" + motoCounter;
                                    motoCounter++;
                                    break;
                                case "Handicap":
                                    space.SpaceType = spaceTypeHandicap;
                                    space.Name = "Handicap-" + handicapCounter;
                                    handicapCounter++;
                                    break;
                                case "Bus":
                                    space.SpaceType = spaceTypeBus;
                                    space.Name = "Bus-" + busCounter;
                                    busCounter++;
                                    break;
                                case "Street":
                                    space.SpaceType = spaceTypeStreet;
                                    space.Name = "Street-" + streetCounter;
                                    streetCounter++;
                                    break;
                                case "Clear":
                                    space.SpaceType = spaceTypeClear;
                                    space.Name = "Clear-" + clearCounter;
                                    clearCounter++;
                                    break;
                            }
                            break;
                        case "3,19":
                            switch (hidden80.Value)
                            {
                                case "Car":
                                    space.SpaceType = spaceTypeCar;
                                    space.Name = "Car-" + carcounter;
                                    carcounter++;
                                    break;
                                case "Motorcycle":
                                    space.SpaceType = spaceTypeMoto;
                                    space.Name = "Motorcycle-" + motoCounter;
                                    motoCounter++;
                                    break;
                                case "Handicap":
                                    space.SpaceType = spaceTypeHandicap;
                                    space.Name = "Handicap-" + handicapCounter;
                                    handicapCounter++;
                                    break;
                                case "Bus":
                                    space.SpaceType = spaceTypeBus;
                                    space.Name = "Bus-" + busCounter;
                                    busCounter++;
                                    break;
                                case "Street":
                                    space.SpaceType = spaceTypeStreet;
                                    space.Name = "Street-" + streetCounter;
                                    streetCounter++;
                                    break;
                                case "Clear":
                                    space.SpaceType = spaceTypeClear;
                                    space.Name = "Clear-" + clearCounter;
                                    clearCounter++;
                                    break;
                            }
                            break;
                        case "4,0":
                            switch (hidden81.Value)
                            {
                                case "Car":
                                    space.SpaceType = spaceTypeCar;
                                    space.Name = "Car-" + carcounter;
                                    carcounter++;
                                    break;
                                case "Motorcycle":
                                    space.SpaceType = spaceTypeMoto;
                                    space.Name = "Motorcycle-" + motoCounter;
                                    motoCounter++;
                                    break;
                                case "Handicap":
                                    space.SpaceType = spaceTypeHandicap;
                                    space.Name = "Handicap-" + handicapCounter;
                                    handicapCounter++;
                                    break;
                                case "Bus":
                                    space.SpaceType = spaceTypeBus;
                                    space.Name = "Bus-" + busCounter;
                                    busCounter++;
                                    break;
                                case "Street":
                                    space.SpaceType = spaceTypeStreet;
                                    space.Name = "Street-" + streetCounter;
                                    streetCounter++;
                                    break;
                                case "Clear":
                                    space.SpaceType = spaceTypeClear;
                                    space.Name = "Clear-" + clearCounter;
                                    clearCounter++;
                                    break;
                            }
                            break;
                        case "4,1":
                            switch (hidden82.Value)
                            {
                                case "Car":
                                    space.SpaceType = spaceTypeCar;
                                    space.Name = "Car-" + carcounter;
                                    carcounter++;
                                    break;
                                case "Motorcycle":
                                    space.SpaceType = spaceTypeMoto;
                                    space.Name = "Motorcycle-" + motoCounter;
                                    motoCounter++;
                                    break;
                                case "Handicap":
                                    space.SpaceType = spaceTypeHandicap;
                                    space.Name = "Handicap-" + handicapCounter;
                                    handicapCounter++;
                                    break;
                                case "Bus":
                                    space.SpaceType = spaceTypeBus;
                                    space.Name = "Bus-" + busCounter;
                                    busCounter++;
                                    break;
                                case "Street":
                                    space.SpaceType = spaceTypeStreet;
                                    space.Name = "Street-" + streetCounter;
                                    streetCounter++;
                                    break;
                                case "Clear":
                                    space.SpaceType = spaceTypeClear;
                                    space.Name = "Clear-" + clearCounter;
                                    clearCounter++;
                                    break;
                            }
                            break;
                        case "4,2":
                            switch (hidden83.Value)
                            {
                                case "Car":
                                    space.SpaceType = spaceTypeCar;
                                    space.Name = "Car-" + carcounter;
                                    carcounter++;
                                    break;
                                case "Motorcycle":
                                    space.SpaceType = spaceTypeMoto;
                                    space.Name = "Motorcycle-" + motoCounter;
                                    motoCounter++;
                                    break;
                                case "Handicap":
                                    space.SpaceType = spaceTypeHandicap;
                                    space.Name = "Handicap-" + handicapCounter;
                                    handicapCounter++;
                                    break;
                                case "Bus":
                                    space.SpaceType = spaceTypeBus;
                                    space.Name = "Bus-" + busCounter;
                                    busCounter++;
                                    break;
                                case "Street":
                                    space.SpaceType = spaceTypeStreet;
                                    space.Name = "Street-" + streetCounter;
                                    streetCounter++;
                                    break;
                                case "Clear":
                                    space.SpaceType = spaceTypeClear;
                                    space.Name = "Clear-" + clearCounter;
                                    clearCounter++;
                                    break;
                            }
                            break;
                        case "4,3":
                            switch (hidden84.Value)
                            {
                                case "Car":
                                    space.SpaceType = spaceTypeCar;
                                    space.Name = "Car-" + carcounter;
                                    carcounter++;
                                    break;
                                case "Motorcycle":
                                    space.SpaceType = spaceTypeMoto;
                                    space.Name = "Motorcycle-" + motoCounter;
                                    motoCounter++;
                                    break;
                                case "Handicap":
                                    space.SpaceType = spaceTypeHandicap;
                                    space.Name = "Handicap-" + handicapCounter;
                                    handicapCounter++;
                                    break;
                                case "Bus":
                                    space.SpaceType = spaceTypeBus;
                                    space.Name = "Bus-" + busCounter;
                                    busCounter++;
                                    break;
                                case "Street":
                                    space.SpaceType = spaceTypeStreet;
                                    space.Name = "Street-" + streetCounter;
                                    streetCounter++;
                                    break;
                                case "Clear":
                                    space.SpaceType = spaceTypeClear;
                                    space.Name = "Clear-" + clearCounter;
                                    clearCounter++;
                                    break;
                            }
                            break;
                        case "4,4":
                            switch (hidden85.Value)
                            {
                                case "Car":
                                    space.SpaceType = spaceTypeCar;
                                    space.Name = "Car-" + carcounter;
                                    carcounter++;
                                    break;
                                case "Motorcycle":
                                    space.SpaceType = spaceTypeMoto;
                                    space.Name = "Motorcycle-" + motoCounter;
                                    motoCounter++;
                                    break;
                                case "Handicap":
                                    space.SpaceType = spaceTypeHandicap;
                                    space.Name = "Handicap-" + handicapCounter;
                                    handicapCounter++;
                                    break;
                                case "Bus":
                                    space.SpaceType = spaceTypeBus;
                                    space.Name = "Bus-" + busCounter;
                                    busCounter++;
                                    break;
                                case "Street":
                                    space.SpaceType = spaceTypeStreet;
                                    space.Name = "Street-" + streetCounter;
                                    streetCounter++;
                                    break;
                                case "Clear":
                                    space.SpaceType = spaceTypeClear;
                                    space.Name = "Clear-" + clearCounter;
                                    clearCounter++;
                                    break;
                            }
                            break;
                        case "4,5":
                            switch (hidden86.Value)
                            {
                                case "Car":
                                    space.SpaceType = spaceTypeCar;
                                    space.Name = "Car-" + carcounter;
                                    carcounter++;
                                    break;
                                case "Motorcycle":
                                    space.SpaceType = spaceTypeMoto;
                                    space.Name = "Motorcycle-" + motoCounter;
                                    motoCounter++;
                                    break;
                                case "Handicap":
                                    space.SpaceType = spaceTypeHandicap;
                                    space.Name = "Handicap-" + handicapCounter;
                                    handicapCounter++;
                                    break;
                                case "Bus":
                                    space.SpaceType = spaceTypeBus;
                                    space.Name = "Bus-" + busCounter;
                                    busCounter++;
                                    break;
                                case "Street":
                                    space.SpaceType = spaceTypeStreet;
                                    space.Name = "Street-" + streetCounter;
                                    streetCounter++;
                                    break;
                                case "Clear":
                                    space.SpaceType = spaceTypeClear;
                                    space.Name = "Clear-" + clearCounter;
                                    clearCounter++;
                                    break;
                            }
                            break;
                        case "4,6":
                            switch (hidden87.Value)
                            {
                                case "Car":
                                    space.SpaceType = spaceTypeCar;
                                    space.Name = "Car-" + carcounter;
                                    carcounter++;
                                    break;
                                case "Motorcycle":
                                    space.SpaceType = spaceTypeMoto;
                                    space.Name = "Motorcycle-" + motoCounter;
                                    motoCounter++;
                                    break;
                                case "Handicap":
                                    space.SpaceType = spaceTypeHandicap;
                                    space.Name = "Handicap-" + handicapCounter;
                                    handicapCounter++;
                                    break;
                                case "Bus":
                                    space.SpaceType = spaceTypeBus;
                                    space.Name = "Bus-" + busCounter;
                                    busCounter++;
                                    break;
                                case "Street":
                                    space.SpaceType = spaceTypeStreet;
                                    space.Name = "Street-" + streetCounter;
                                    streetCounter++;
                                    break;
                                case "Clear":
                                    space.SpaceType = spaceTypeClear;
                                    space.Name = "Clear-" + clearCounter;
                                    clearCounter++;
                                    break;
                            }
                            break;
                        case "4,7":
                            switch (hidden88.Value)
                            {
                                case "Car":
                                    space.SpaceType = spaceTypeCar;
                                    space.Name = "Car-" + carcounter;
                                    carcounter++;
                                    break;
                                case "Motorcycle":
                                    space.SpaceType = spaceTypeMoto;
                                    space.Name = "Motorcycle-" + motoCounter;
                                    motoCounter++;
                                    break;
                                case "Handicap":
                                    space.SpaceType = spaceTypeHandicap;
                                    space.Name = "Handicap-" + handicapCounter;
                                    handicapCounter++;
                                    break;
                                case "Bus":
                                    space.SpaceType = spaceTypeBus;
                                    space.Name = "Bus-" + busCounter;
                                    busCounter++;
                                    break;
                                case "Street":
                                    space.SpaceType = spaceTypeStreet;
                                    space.Name = "Street-" + streetCounter;
                                    streetCounter++;
                                    break;
                                case "Clear":
                                    space.SpaceType = spaceTypeClear;
                                    space.Name = "Clear-" + clearCounter;
                                    clearCounter++;
                                    break;
                            }
                            break;
                        case "4,8":
                            switch (hidden89.Value)
                            {
                                case "Car":
                                    space.SpaceType = spaceTypeCar;
                                    space.Name = "Car-" + carcounter;
                                    carcounter++;
                                    break;
                                case "Motorcycle":
                                    space.SpaceType = spaceTypeMoto;
                                    space.Name = "Motorcycle-" + motoCounter;
                                    motoCounter++;
                                    break;
                                case "Handicap":
                                    space.SpaceType = spaceTypeHandicap;
                                    space.Name = "Handicap-" + handicapCounter;
                                    handicapCounter++;
                                    break;
                                case "Bus":
                                    space.SpaceType = spaceTypeBus;
                                    space.Name = "Bus-" + busCounter;
                                    busCounter++;
                                    break;
                                case "Street":
                                    space.SpaceType = spaceTypeStreet;
                                    space.Name = "Street-" + streetCounter;
                                    streetCounter++;
                                    break;
                                case "Clear":
                                    space.SpaceType = spaceTypeClear;
                                    space.Name = "Clear-" + clearCounter;
                                    clearCounter++;
                                    break;
                            }
                            break;
                        case "4,9":
                            switch (hidden90.Value)
                            {
                                case "Car":
                                    space.SpaceType = spaceTypeCar;
                                    space.Name = "Car-" + carcounter;
                                    carcounter++;
                                    break;
                                case "Motorcycle":
                                    space.SpaceType = spaceTypeMoto;
                                    space.Name = "Motorcycle-" + motoCounter;
                                    motoCounter++;
                                    break;
                                case "Handicap":
                                    space.SpaceType = spaceTypeHandicap;
                                    space.Name = "Handicap-" + handicapCounter;
                                    handicapCounter++;
                                    break;
                                case "Bus":
                                    space.SpaceType = spaceTypeBus;
                                    space.Name = "Bus-" + busCounter;
                                    busCounter++;
                                    break;
                                case "Street":
                                    space.SpaceType = spaceTypeStreet;
                                    space.Name = "Street-" + streetCounter;
                                    streetCounter++;
                                    break;
                                case "Clear":
                                    space.SpaceType = spaceTypeClear;
                                    space.Name = "Clear-" + clearCounter;
                                    clearCounter++;
                                    break;
                            }
                            break;
                        case "4,10":
                            switch (hidden91.Value)
                            {
                                case "Car":
                                    space.SpaceType = spaceTypeCar;
                                    space.Name = "Car-" + carcounter;
                                    carcounter++;
                                    break;
                                case "Motorcycle":
                                    space.SpaceType = spaceTypeMoto;
                                    space.Name = "Motorcycle-" + motoCounter;
                                    motoCounter++;
                                    break;
                                case "Handicap":
                                    space.SpaceType = spaceTypeHandicap;
                                    space.Name = "Handicap-" + handicapCounter;
                                    handicapCounter++;
                                    break;
                                case "Bus":
                                    space.SpaceType = spaceTypeBus;
                                    space.Name = "Bus-" + busCounter;
                                    busCounter++;
                                    break;
                                case "Street":
                                    space.SpaceType = spaceTypeStreet;
                                    space.Name = "Street-" + streetCounter;
                                    streetCounter++;
                                    break;
                                case "Clear":
                                    space.SpaceType = spaceTypeClear;
                                    space.Name = "Clear-" + clearCounter;
                                    clearCounter++;
                                    break;
                            }
                            break;
                        case "4,11":
                            switch (hidden92.Value)
                            {
                                case "Car":
                                    space.SpaceType = spaceTypeCar;
                                    space.Name = "Car-" + carcounter;
                                    carcounter++;
                                    break;
                                case "Motorcycle":
                                    space.SpaceType = spaceTypeMoto;
                                    space.Name = "Motorcycle-" + motoCounter;
                                    motoCounter++;
                                    break;
                                case "Handicap":
                                    space.SpaceType = spaceTypeHandicap;
                                    space.Name = "Handicap-" + handicapCounter;
                                    handicapCounter++;
                                    break;
                                case "Bus":
                                    space.SpaceType = spaceTypeBus;
                                    space.Name = "Bus-" + busCounter;
                                    busCounter++;
                                    break;
                                case "Street":
                                    space.SpaceType = spaceTypeStreet;
                                    space.Name = "Street-" + streetCounter;
                                    streetCounter++;
                                    break;
                                case "Clear":
                                    space.SpaceType = spaceTypeClear;
                                    space.Name = "Clear-" + clearCounter;
                                    clearCounter++;
                                    break;
                            }
                            break;
                        case "4,12":
                            switch (hidden93.Value)
                            {
                                case "Car":
                                    space.SpaceType = spaceTypeCar;
                                    space.Name = "Car-" + carcounter;
                                    carcounter++;
                                    break;
                                case "Motorcycle":
                                    space.SpaceType = spaceTypeMoto;
                                    space.Name = "Motorcycle-" + motoCounter;
                                    motoCounter++;
                                    break;
                                case "Handicap":
                                    space.SpaceType = spaceTypeHandicap;
                                    space.Name = "Handicap-" + handicapCounter;
                                    handicapCounter++;
                                    break;
                                case "Bus":
                                    space.SpaceType = spaceTypeBus;
                                    space.Name = "Bus-" + busCounter;
                                    busCounter++;
                                    break;
                                case "Street":
                                    space.SpaceType = spaceTypeStreet;
                                    space.Name = "Street-" + streetCounter;
                                    streetCounter++;
                                    break;
                                case "Clear":
                                    space.SpaceType = spaceTypeClear;
                                    space.Name = "Clear-" + clearCounter;
                                    clearCounter++;
                                    break;
                            }
                            break;
                        case "4,13":
                            switch (hidden94.Value)
                            {
                                case "Car":
                                    space.SpaceType = spaceTypeCar;
                                    space.Name = "Car-" + carcounter;
                                    carcounter++;
                                    break;
                                case "Motorcycle":
                                    space.SpaceType = spaceTypeMoto;
                                    space.Name = "Motorcycle-" + motoCounter;
                                    motoCounter++;
                                    break;
                                case "Handicap":
                                    space.SpaceType = spaceTypeHandicap;
                                    space.Name = "Handicap-" + handicapCounter;
                                    handicapCounter++;
                                    break;
                                case "Bus":
                                    space.SpaceType = spaceTypeBus;
                                    space.Name = "Bus-" + busCounter;
                                    busCounter++;
                                    break;
                                case "Street":
                                    space.SpaceType = spaceTypeStreet;
                                    space.Name = "Street-" + streetCounter;
                                    streetCounter++;
                                    break;
                                case "Clear":
                                    space.SpaceType = spaceTypeClear;
                                    space.Name = "Clear-" + clearCounter;
                                    clearCounter++;
                                    break;
                            }
                            break;
                        case "4,14":
                            switch (hidden95.Value)
                            {
                                case "Car":
                                    space.SpaceType = spaceTypeCar;
                                    space.Name = "Car-" + carcounter;
                                    carcounter++;
                                    break;
                                case "Motorcycle":
                                    space.SpaceType = spaceTypeMoto;
                                    space.Name = "Motorcycle-" + motoCounter;
                                    motoCounter++;
                                    break;
                                case "Handicap":
                                    space.SpaceType = spaceTypeHandicap;
                                    space.Name = "Handicap-" + handicapCounter;
                                    handicapCounter++;
                                    break;
                                case "Bus":
                                    space.SpaceType = spaceTypeBus;
                                    space.Name = "Bus-" + busCounter;
                                    busCounter++;
                                    break;
                                case "Street":
                                    space.SpaceType = spaceTypeStreet;
                                    space.Name = "Street-" + streetCounter;
                                    streetCounter++;
                                    break;
                                case "Clear":
                                    space.SpaceType = spaceTypeClear;
                                    space.Name = "Clear-" + clearCounter;
                                    clearCounter++;
                                    break;
                            }
                            break;
                        case "4,15":
                            switch (hidden96.Value)
                            {
                                case "Car":
                                    space.SpaceType = spaceTypeCar;
                                    space.Name = "Car-" + carcounter;
                                    carcounter++;
                                    break;
                                case "Motorcycle":
                                    space.SpaceType = spaceTypeMoto;
                                    space.Name = "Motorcycle-" + motoCounter;
                                    motoCounter++;
                                    break;
                                case "Handicap":
                                    space.SpaceType = spaceTypeHandicap;
                                    space.Name = "Handicap-" + handicapCounter;
                                    handicapCounter++;
                                    break;
                                case "Bus":
                                    space.SpaceType = spaceTypeBus;
                                    space.Name = "Bus-" + busCounter;
                                    busCounter++;
                                    break;
                                case "Street":
                                    space.SpaceType = spaceTypeStreet;
                                    space.Name = "Street-" + streetCounter;
                                    streetCounter++;
                                    break;
                                case "Clear":
                                    space.SpaceType = spaceTypeClear;
                                    space.Name = "Clear-" + clearCounter;
                                    clearCounter++;
                                    break;
                            }
                            break;
                        case "4,16":
                            switch (hidden97.Value)
                            {
                                case "Car":
                                    space.SpaceType = spaceTypeCar;
                                    space.Name = "Car-" + carcounter;
                                    carcounter++;
                                    break;
                                case "Motorcycle":
                                    space.SpaceType = spaceTypeMoto;
                                    space.Name = "Motorcycle-" + motoCounter;
                                    motoCounter++;
                                    break;
                                case "Handicap":
                                    space.SpaceType = spaceTypeHandicap;
                                    space.Name = "Handicap-" + handicapCounter;
                                    handicapCounter++;
                                    break;
                                case "Bus":
                                    space.SpaceType = spaceTypeBus;
                                    space.Name = "Bus-" + busCounter;
                                    busCounter++;
                                    break;
                                case "Street":
                                    space.SpaceType = spaceTypeStreet;
                                    space.Name = "Street-" + streetCounter;
                                    streetCounter++;
                                    break;
                                case "Clear":
                                    space.SpaceType = spaceTypeClear;
                                    space.Name = "Clear-" + clearCounter;
                                    clearCounter++;
                                    break;
                            }
                            break;
                        case "4,17":
                            switch (hidden98.Value)
                            {
                                case "Car":
                                    space.SpaceType = spaceTypeCar;
                                    space.Name = "Car-" + carcounter;
                                    carcounter++;
                                    break;
                                case "Motorcycle":
                                    space.SpaceType = spaceTypeMoto;
                                    space.Name = "Motorcycle-" + motoCounter;
                                    motoCounter++;
                                    break;
                                case "Handicap":
                                    space.SpaceType = spaceTypeHandicap;
                                    space.Name = "Handicap-" + handicapCounter;
                                    handicapCounter++;
                                    break;
                                case "Bus":
                                    space.SpaceType = spaceTypeBus;
                                    space.Name = "Bus-" + busCounter;
                                    busCounter++;
                                    break;
                                case "Street":
                                    space.SpaceType = spaceTypeStreet;
                                    space.Name = "Street-" + streetCounter;
                                    streetCounter++;
                                    break;
                                case "Clear":
                                    space.SpaceType = spaceTypeClear;
                                    space.Name = "Clear-" + clearCounter;
                                    clearCounter++;
                                    break;
                            }
                            break;
                        case "4,18":
                            switch (hidden99.Value)
                            {
                                case "Car":
                                    space.SpaceType = spaceTypeCar;
                                    space.Name = "Car-" + carcounter;
                                    carcounter++;
                                    break;
                                case "Motorcycle":
                                    space.SpaceType = spaceTypeMoto;
                                    space.Name = "Motorcycle-" + motoCounter;
                                    motoCounter++;
                                    break;
                                case "Handicap":
                                    space.SpaceType = spaceTypeHandicap;
                                    space.Name = "Handicap-" + handicapCounter;
                                    handicapCounter++;
                                    break;
                                case "Bus":
                                    space.SpaceType = spaceTypeBus;
                                    space.Name = "Bus-" + busCounter;
                                    busCounter++;
                                    break;
                                case "Street":
                                    space.SpaceType = spaceTypeStreet;
                                    space.Name = "Street-" + streetCounter;
                                    streetCounter++;
                                    break;
                                case "Clear":
                                    space.SpaceType = spaceTypeClear;
                                    space.Name = "Clear-" + clearCounter;
                                    clearCounter++;
                                    break;
                            }
                            break;
                        case "4,19":
                            switch (hidden100.Value)
                            {
                                case "Car":
                                    space.SpaceType = spaceTypeCar;
                                    space.Name = "Car-" + carcounter;
                                    carcounter++;
                                    break;
                                case "Motorcycle":
                                    space.SpaceType = spaceTypeMoto;
                                    space.Name = "Motorcycle-" + motoCounter;
                                    motoCounter++;
                                    break;
                                case "Handicap":
                                    space.SpaceType = spaceTypeHandicap;
                                    space.Name = "Handicap-" + handicapCounter;
                                    handicapCounter++;
                                    break;
                                case "Bus":
                                    space.SpaceType = spaceTypeBus;
                                    space.Name = "Bus-" + busCounter;
                                    busCounter++;
                                    break;
                                case "Street":
                                    space.SpaceType = spaceTypeStreet;
                                    space.Name = "Street-" + streetCounter;
                                    streetCounter++;
                                    break;
                                case "Clear":
                                    space.SpaceType = spaceTypeClear;
                                    space.Name = "Clear-" + clearCounter;
                                    clearCounter++;
                                    break;
                            }
                            break;

                        case "5,0":
                            switch (hidden101.Value)
                            {
                                case "Car":
                                    space.SpaceType = spaceTypeCar;
                                    space.Name = "Car-" + carcounter;
                                    carcounter++;
                                    break;
                                case "Motorcycle":
                                    space.SpaceType = spaceTypeMoto;
                                    space.Name = "Motorcycle-" + motoCounter;
                                    motoCounter++;
                                    break;
                                case "Handicap":
                                    space.SpaceType = spaceTypeHandicap;
                                    space.Name = "Handicap-" + handicapCounter;
                                    handicapCounter++;
                                    break;
                                case "Bus":
                                    space.SpaceType = spaceTypeBus;
                                    space.Name = "Bus-" + busCounter;
                                    busCounter++;
                                    break;
                                case "Street":
                                    space.SpaceType = spaceTypeStreet;
                                    space.Name = "Street-" + streetCounter;
                                    streetCounter++;
                                    break;
                                case "Clear":
                                    space.SpaceType = spaceTypeClear;
                                    space.Name = "Clear-" + clearCounter;
                                    clearCounter++;
                                    break;
                            }
                            break;
                        case "5,1":
                            switch (hidden102.Value)
                            {
                                case "Car":
                                    space.SpaceType = spaceTypeCar;
                                    space.Name = "Car-" + carcounter;
                                    carcounter++;
                                    break;
                                case "Motorcycle":
                                    space.SpaceType = spaceTypeMoto;
                                    space.Name = "Motorcycle-" + motoCounter;
                                    motoCounter++;
                                    break;
                                case "Handicap":
                                    space.SpaceType = spaceTypeHandicap;
                                    space.Name = "Handicap-" + handicapCounter;
                                    handicapCounter++;
                                    break;
                                case "Bus":
                                    space.SpaceType = spaceTypeBus;
                                    space.Name = "Bus-" + busCounter;
                                    busCounter++;
                                    break;
                                case "Street":
                                    space.SpaceType = spaceTypeStreet;
                                    space.Name = "Street-" + streetCounter;
                                    streetCounter++;
                                    break;
                                case "Clear":
                                    space.SpaceType = spaceTypeClear;
                                    space.Name = "Clear-" + clearCounter;
                                    clearCounter++;
                                    break;
                            }
                            break;
                        case "5,2":
                            switch (hidden103.Value)
                            {
                                case "Car":
                                    space.SpaceType = spaceTypeCar;
                                    space.Name = "Car-" + carcounter;
                                    carcounter++;
                                    break;
                                case "Motorcycle":
                                    space.SpaceType = spaceTypeMoto;
                                    space.Name = "Motorcycle-" + motoCounter;
                                    motoCounter++;
                                    break;
                                case "Handicap":
                                    space.SpaceType = spaceTypeHandicap;
                                    space.Name = "Handicap-" + handicapCounter;
                                    handicapCounter++;
                                    break;
                                case "Bus":
                                    space.SpaceType = spaceTypeBus;
                                    space.Name = "Bus-" + busCounter;
                                    busCounter++;
                                    break;
                                case "Street":
                                    space.SpaceType = spaceTypeStreet;
                                    space.Name = "Street-" + streetCounter;
                                    streetCounter++;
                                    break;
                                case "Clear":
                                    space.SpaceType = spaceTypeClear;
                                    space.Name = "Clear-" + clearCounter;
                                    clearCounter++;
                                    break;
                            }
                            break;
                        case "5,3":
                            switch (hidden104.Value)
                            {
                                case "Car":
                                    space.SpaceType = spaceTypeCar;
                                    space.Name = "Car-" + carcounter;
                                    carcounter++;
                                    break;
                                case "Motorcycle":
                                    space.SpaceType = spaceTypeMoto;
                                    space.Name = "Motorcycle-" + motoCounter;
                                    motoCounter++;
                                    break;
                                case "Handicap":
                                    space.SpaceType = spaceTypeHandicap;
                                    space.Name = "Handicap-" + handicapCounter;
                                    handicapCounter++;
                                    break;
                                case "Bus":
                                    space.SpaceType = spaceTypeBus;
                                    space.Name = "Bus-" + busCounter;
                                    busCounter++;
                                    break;
                                case "Street":
                                    space.SpaceType = spaceTypeStreet;
                                    space.Name = "Street-" + streetCounter;
                                    streetCounter++;
                                    break;
                                case "Clear":
                                    space.SpaceType = spaceTypeClear;
                                    space.Name = "Clear-" + clearCounter;
                                    clearCounter++;
                                    break;
                            }
                            break;
                        case "5,4":
                            switch (hidden105.Value)
                            {
                                case "Car":
                                    space.SpaceType = spaceTypeCar;
                                    space.Name = "Car-" + carcounter;
                                    carcounter++;
                                    break;
                                case "Motorcycle":
                                    space.SpaceType = spaceTypeMoto;
                                    space.Name = "Motorcycle-" + motoCounter;
                                    motoCounter++;
                                    break;
                                case "Handicap":
                                    space.SpaceType = spaceTypeHandicap;
                                    space.Name = "Handicap-" + handicapCounter;
                                    handicapCounter++;
                                    break;
                                case "Bus":
                                    space.SpaceType = spaceTypeBus;
                                    space.Name = "Bus-" + busCounter;
                                    busCounter++;
                                    break;
                                case "Street":
                                    space.SpaceType = spaceTypeStreet;
                                    space.Name = "Street-" + streetCounter;
                                    streetCounter++;
                                    break;
                                case "Clear":
                                    space.SpaceType = spaceTypeClear;
                                    space.Name = "Clear-" + clearCounter;
                                    clearCounter++;
                                    break;
                            }
                            break;
                        case "5,5":
                            switch (hidden106.Value)
                            {
                                case "Car":
                                    space.SpaceType = spaceTypeCar;
                                    space.Name = "Car-" + carcounter;
                                    carcounter++;
                                    break;
                                case "Motorcycle":
                                    space.SpaceType = spaceTypeMoto;
                                    space.Name = "Motorcycle-" + motoCounter;
                                    motoCounter++;
                                    break;
                                case "Handicap":
                                    space.SpaceType = spaceTypeHandicap;
                                    space.Name = "Handicap-" + handicapCounter;
                                    handicapCounter++;
                                    break;
                                case "Bus":
                                    space.SpaceType = spaceTypeBus;
                                    space.Name = "Bus-" + busCounter;
                                    busCounter++;
                                    break;
                                case "Street":
                                    space.SpaceType = spaceTypeStreet;
                                    space.Name = "Street-" + streetCounter;
                                    streetCounter++;
                                    break;
                                case "Clear":
                                    space.SpaceType = spaceTypeClear;
                                    space.Name = "Clear-" + clearCounter;
                                    clearCounter++;
                                    break;
                            }
                            break;
                        case "5,6":
                            switch (hidden107.Value)
                            {
                                case "Car":
                                    space.SpaceType = spaceTypeCar;
                                    space.Name = "Car-" + carcounter;
                                    carcounter++;
                                    break;
                                case "Motorcycle":
                                    space.SpaceType = spaceTypeMoto;
                                    space.Name = "Motorcycle-" + motoCounter;
                                    motoCounter++;
                                    break;
                                case "Handicap":
                                    space.SpaceType = spaceTypeHandicap;
                                    space.Name = "Handicap-" + handicapCounter;
                                    handicapCounter++;
                                    break;
                                case "Bus":
                                    space.SpaceType = spaceTypeBus;
                                    space.Name = "Bus-" + busCounter;
                                    busCounter++;
                                    break;
                                case "Street":
                                    space.SpaceType = spaceTypeStreet;
                                    space.Name = "Street-" + streetCounter;
                                    streetCounter++;
                                    break;
                                case "Clear":
                                    space.SpaceType = spaceTypeClear;
                                    space.Name = "Clear-" + clearCounter;
                                    clearCounter++;
                                    break;
                            }
                            break;
                        case "5,7":
                            switch (hidden108.Value)
                            {
                                case "Car":
                                    space.SpaceType = spaceTypeCar;
                                    space.Name = "Car-" + carcounter;
                                    carcounter++;
                                    break;
                                case "Motorcycle":
                                    space.SpaceType = spaceTypeMoto;
                                    space.Name = "Motorcycle-" + motoCounter;
                                    motoCounter++;
                                    break;
                                case "Handicap":
                                    space.SpaceType = spaceTypeHandicap;
                                    space.Name = "Handicap-" + handicapCounter;
                                    handicapCounter++;
                                    break;
                                case "Bus":
                                    space.SpaceType = spaceTypeBus;
                                    space.Name = "Bus-" + busCounter;
                                    busCounter++;
                                    break;
                                case "Street":
                                    space.SpaceType = spaceTypeStreet;
                                    space.Name = "Street-" + streetCounter;
                                    streetCounter++;
                                    break;
                                case "Clear":
                                    space.SpaceType = spaceTypeClear;
                                    space.Name = "Clear-" + clearCounter;
                                    clearCounter++;
                                    break;
                            }
                            break;
                        case "5,8":
                            switch (hidden109.Value)
                            {
                                case "Car":
                                    space.SpaceType = spaceTypeCar;
                                    space.Name = "Car-" + carcounter;
                                    carcounter++;
                                    break;
                                case "Motorcycle":
                                    space.SpaceType = spaceTypeMoto;
                                    space.Name = "Motorcycle-" + motoCounter;
                                    motoCounter++;
                                    break;
                                case "Handicap":
                                    space.SpaceType = spaceTypeHandicap;
                                    space.Name = "Handicap-" + handicapCounter;
                                    handicapCounter++;
                                    break;
                                case "Bus":
                                    space.SpaceType = spaceTypeBus;
                                    space.Name = "Bus-" + busCounter;
                                    busCounter++;
                                    break;
                                case "Street":
                                    space.SpaceType = spaceTypeStreet;
                                    space.Name = "Street-" + streetCounter;
                                    streetCounter++;
                                    break;
                                case "Clear":
                                    space.SpaceType = spaceTypeClear;
                                    space.Name = "Clear-" + clearCounter;
                                    clearCounter++;
                                    break;
                            }
                            break;
                        case "5,9":
                            switch (hidden110.Value)
                            {
                                case "Car":
                                    space.SpaceType = spaceTypeCar;
                                    space.Name = "Car-" + carcounter;
                                    carcounter++;
                                    break;
                                case "Motorcycle":
                                    space.SpaceType = spaceTypeMoto;
                                    space.Name = "Motorcycle-" + motoCounter;
                                    motoCounter++;
                                    break;
                                case "Handicap":
                                    space.SpaceType = spaceTypeHandicap;
                                    space.Name = "Handicap-" + handicapCounter;
                                    handicapCounter++;
                                    break;
                                case "Bus":
                                    space.SpaceType = spaceTypeBus;
                                    space.Name = "Bus-" + busCounter;
                                    busCounter++;
                                    break;
                                case "Street":
                                    space.SpaceType = spaceTypeStreet;
                                    space.Name = "Street-" + streetCounter;
                                    streetCounter++;
                                    break;
                                case "Clear":
                                    space.SpaceType = spaceTypeClear;
                                    space.Name = "Clear-" + clearCounter;
                                    clearCounter++;
                                    break;
                            }
                            break;
                        case "5,10":
                            switch (hidden111.Value)
                            {
                                case "Car":
                                    space.SpaceType = spaceTypeCar;
                                    space.Name = "Car-" + carcounter;
                                    carcounter++;
                                    break;
                                case "Motorcycle":
                                    space.SpaceType = spaceTypeMoto;
                                    space.Name = "Motorcycle-" + motoCounter;
                                    motoCounter++;
                                    break;
                                case "Handicap":
                                    space.SpaceType = spaceTypeHandicap;
                                    space.Name = "Handicap-" + handicapCounter;
                                    handicapCounter++;
                                    break;
                                case "Bus":
                                    space.SpaceType = spaceTypeBus;
                                    space.Name = "Bus-" + busCounter;
                                    busCounter++;
                                    break;
                                case "Street":
                                    space.SpaceType = spaceTypeStreet;
                                    space.Name = "Street-" + streetCounter;
                                    streetCounter++;
                                    break;
                                case "Clear":
                                    space.SpaceType = spaceTypeClear;
                                    space.Name = "Clear-" + clearCounter;
                                    clearCounter++;
                                    break;
                            }
                            break;
                        case "5,11":
                            switch (hidden112.Value)
                            {
                                case "Car":
                                    space.SpaceType = spaceTypeCar;
                                    space.Name = "Car-" + carcounter;
                                    carcounter++;
                                    break;
                                case "Motorcycle":
                                    space.SpaceType = spaceTypeMoto;
                                    space.Name = "Motorcycle-" + motoCounter;
                                    motoCounter++;
                                    break;
                                case "Handicap":
                                    space.SpaceType = spaceTypeHandicap;
                                    space.Name = "Handicap-" + handicapCounter;
                                    handicapCounter++;
                                    break;
                                case "Bus":
                                    space.SpaceType = spaceTypeBus;
                                    space.Name = "Bus-" + busCounter;
                                    busCounter++;
                                    break;
                                case "Street":
                                    space.SpaceType = spaceTypeStreet;
                                    space.Name = "Street-" + streetCounter;
                                    streetCounter++;
                                    break;
                                case "Clear":
                                    space.SpaceType = spaceTypeClear;
                                    space.Name = "Clear-" + clearCounter;
                                    clearCounter++;
                                    break;
                            }
                            break;
                        case "5,12":
                            switch (hidden113.Value)
                            {
                                case "Car":
                                    space.SpaceType = spaceTypeCar;
                                    space.Name = "Car-" + carcounter;
                                    carcounter++;
                                    break;
                                case "Motorcycle":
                                    space.SpaceType = spaceTypeMoto;
                                    space.Name = "Motorcycle-" + motoCounter;
                                    motoCounter++;
                                    break;
                                case "Handicap":
                                    space.SpaceType = spaceTypeHandicap;
                                    space.Name = "Handicap-" + handicapCounter;
                                    handicapCounter++;
                                    break;
                                case "Bus":
                                    space.SpaceType = spaceTypeBus;
                                    space.Name = "Bus-" + busCounter;
                                    busCounter++;
                                    break;
                                case "Street":
                                    space.SpaceType = spaceTypeStreet;
                                    space.Name = "Street-" + streetCounter;
                                    streetCounter++;
                                    break;
                                case "Clear":
                                    space.SpaceType = spaceTypeClear;
                                    space.Name = "Clear-" + clearCounter;
                                    clearCounter++;
                                    break;
                            }
                            break;
                        case "5,13":
                            switch (hidden114.Value)
                            {
                                case "Car":
                                    space.SpaceType = spaceTypeCar;
                                    space.Name = "Car-" + carcounter;
                                    carcounter++;
                                    break;
                                case "Motorcycle":
                                    space.SpaceType = spaceTypeMoto;
                                    space.Name = "Motorcycle-" + motoCounter;
                                    motoCounter++;
                                    break;
                                case "Handicap":
                                    space.SpaceType = spaceTypeHandicap;
                                    space.Name = "Handicap-" + handicapCounter;
                                    handicapCounter++;
                                    break;
                                case "Bus":
                                    space.SpaceType = spaceTypeBus;
                                    space.Name = "Bus-" + busCounter;
                                    busCounter++;
                                    break;
                                case "Street":
                                    space.SpaceType = spaceTypeStreet;
                                    space.Name = "Street-" + streetCounter;
                                    streetCounter++;
                                    break;
                                case "Clear":
                                    space.SpaceType = spaceTypeClear;
                                    space.Name = "Clear-" + clearCounter;
                                    clearCounter++;
                                    break;
                            }
                            break;
                        case "5,14":
                            switch (hidden115.Value)
                            {
                                case "Car":
                                    space.SpaceType = spaceTypeCar;
                                    space.Name = "Car-" + carcounter;
                                    carcounter++;
                                    break;
                                case "Motorcycle":
                                    space.SpaceType = spaceTypeMoto;
                                    space.Name = "Motorcycle-" + motoCounter;
                                    motoCounter++;
                                    break;
                                case "Handicap":
                                    space.SpaceType = spaceTypeHandicap;
                                    space.Name = "Handicap-" + handicapCounter;
                                    handicapCounter++;
                                    break;
                                case "Bus":
                                    space.SpaceType = spaceTypeBus;
                                    space.Name = "Bus-" + busCounter;
                                    busCounter++;
                                    break;
                                case "Street":
                                    space.SpaceType = spaceTypeStreet;
                                    space.Name = "Street-" + streetCounter;
                                    streetCounter++;
                                    break;
                                case "Clear":
                                    space.SpaceType = spaceTypeClear;
                                    space.Name = "Clear-" + clearCounter;
                                    clearCounter++;
                                    break;
                            }
                            break;
                        case "5,15":
                            switch (hidden116.Value)
                            {
                                case "Car":
                                    space.SpaceType = spaceTypeCar;
                                    space.Name = "Car-" + carcounter;
                                    carcounter++;
                                    break;
                                case "Motorcycle":
                                    space.SpaceType = spaceTypeMoto;
                                    space.Name = "Motorcycle-" + motoCounter;
                                    motoCounter++;
                                    break;
                                case "Handicap":
                                    space.SpaceType = spaceTypeHandicap;
                                    space.Name = "Handicap-" + handicapCounter;
                                    handicapCounter++;
                                    break;
                                case "Bus":
                                    space.SpaceType = spaceTypeBus;
                                    space.Name = "Bus-" + busCounter;
                                    busCounter++;
                                    break;
                                case "Street":
                                    space.SpaceType = spaceTypeStreet;
                                    space.Name = "Street-" + streetCounter;
                                    streetCounter++;
                                    break;
                                case "Clear":
                                    space.SpaceType = spaceTypeClear;
                                    space.Name = "Clear-" + clearCounter;
                                    clearCounter++;
                                    break;
                            }
                            break;
                        case "5,16":
                            switch (hidden117.Value)
                            {
                                case "Car":
                                    space.SpaceType = spaceTypeCar;
                                    space.Name = "Car-" + carcounter;
                                    carcounter++;
                                    break;
                                case "Motorcycle":
                                    space.SpaceType = spaceTypeMoto;
                                    space.Name = "Motorcycle-" + motoCounter;
                                    motoCounter++;
                                    break;
                                case "Handicap":
                                    space.SpaceType = spaceTypeHandicap;
                                    space.Name = "Handicap-" + handicapCounter;
                                    handicapCounter++;
                                    break;
                                case "Bus":
                                    space.SpaceType = spaceTypeBus;
                                    space.Name = "Bus-" + busCounter;
                                    busCounter++;
                                    break;
                                case "Street":
                                    space.SpaceType = spaceTypeStreet;
                                    space.Name = "Street-" + streetCounter;
                                    streetCounter++;
                                    break;
                                case "Clear":
                                    space.SpaceType = spaceTypeClear;
                                    space.Name = "Clear-" + clearCounter;
                                    clearCounter++;
                                    break;
                            }
                            break;
                        case "5,17":
                            switch (hidden118.Value)
                            {
                                case "Car":
                                    space.SpaceType = spaceTypeCar;
                                    space.Name = "Car-" + carcounter;
                                    carcounter++;
                                    break;
                                case "Motorcycle":
                                    space.SpaceType = spaceTypeMoto;
                                    space.Name = "Motorcycle-" + motoCounter;
                                    motoCounter++;
                                    break;
                                case "Handicap":
                                    space.SpaceType = spaceTypeHandicap;
                                    space.Name = "Handicap-" + handicapCounter;
                                    handicapCounter++;
                                    break;
                                case "Bus":
                                    space.SpaceType = spaceTypeBus;
                                    space.Name = "Bus-" + busCounter;
                                    busCounter++;
                                    break;
                                case "Street":
                                    space.SpaceType = spaceTypeStreet;
                                    space.Name = "Street-" + streetCounter;
                                    streetCounter++;
                                    break;
                                case "Clear":
                                    space.SpaceType = spaceTypeClear;
                                    space.Name = "Clear-" + clearCounter;
                                    clearCounter++;
                                    break;
                            }
                            break;
                        case "5,18":
                            switch (hidden119.Value)
                            {
                                case "Car":
                                    space.SpaceType = spaceTypeCar;
                                    space.Name = "Car-" + carcounter;
                                    carcounter++;
                                    break;
                                case "Motorcycle":
                                    space.SpaceType = spaceTypeMoto;
                                    space.Name = "Motorcycle-" + motoCounter;
                                    motoCounter++;
                                    break;
                                case "Handicap":
                                    space.SpaceType = spaceTypeHandicap;
                                    space.Name = "Handicap-" + handicapCounter;
                                    handicapCounter++;
                                    break;
                                case "Bus":
                                    space.SpaceType = spaceTypeBus;
                                    space.Name = "Bus-" + busCounter;
                                    busCounter++;
                                    break;
                                case "Street":
                                    space.SpaceType = spaceTypeStreet;
                                    space.Name = "Street-" + streetCounter;
                                    streetCounter++;
                                    break;
                                case "Clear":
                                    space.SpaceType = spaceTypeClear;
                                    space.Name = "Clear-" + clearCounter;
                                    clearCounter++;
                                    break;
                            }
                            break;
                        case "5,19":
                            switch (hidden120.Value)
                            {
                                case "Car":
                                    space.SpaceType = spaceTypeCar;
                                    space.Name = "Car-" + carcounter;
                                    carcounter++;
                                    break;
                                case "Motorcycle":
                                    space.SpaceType = spaceTypeMoto;
                                    space.Name = "Motorcycle-" + motoCounter;
                                    motoCounter++;
                                    break;
                                case "Handicap":
                                    space.SpaceType = spaceTypeHandicap;
                                    space.Name = "Handicap-" + handicapCounter;
                                    handicapCounter++;
                                    break;
                                case "Bus":
                                    space.SpaceType = spaceTypeBus;
                                    space.Name = "Bus-" + busCounter;
                                    busCounter++;
                                    break;
                                case "Street":
                                    space.SpaceType = spaceTypeStreet;
                                    space.Name = "Street-" + streetCounter;
                                    streetCounter++;
                                    break;
                                case "Clear":
                                    space.SpaceType = spaceTypeClear;
                                    space.Name = "Clear-" + clearCounter;
                                    clearCounter++;
                                    break;
                            }
                            break;

                        case "6,0":
                            switch (hidden121.Value)
                            {
                                case "Car":
                                    space.SpaceType = spaceTypeCar;
                                    space.Name = "Car-" + carcounter;
                                    carcounter++;
                                    break;
                                case "Motorcycle":
                                    space.SpaceType = spaceTypeMoto;
                                    space.Name = "Motorcycle-" + motoCounter;
                                    motoCounter++;
                                    break;
                                case "Handicap":
                                    space.SpaceType = spaceTypeHandicap;
                                    space.Name = "Handicap-" + handicapCounter;
                                    handicapCounter++;
                                    break;
                                case "Bus":
                                    space.SpaceType = spaceTypeBus;
                                    space.Name = "Bus-" + busCounter;
                                    busCounter++;
                                    break;
                                case "Street":
                                    space.SpaceType = spaceTypeStreet;
                                    space.Name = "Street-" + streetCounter;
                                    streetCounter++;
                                    break;
                                case "Clear":
                                    space.SpaceType = spaceTypeClear;
                                    space.Name = "Clear-" + clearCounter;
                                    clearCounter++;
                                    break;
                            }
                            break;
                        case "6,1":
                            switch (hidden122.Value)
                            {
                                case "Car":
                                    space.SpaceType = spaceTypeCar;
                                    space.Name = "Car-" + carcounter;
                                    carcounter++;
                                    break;
                                case "Motorcycle":
                                    space.SpaceType = spaceTypeMoto;
                                    space.Name = "Motorcycle-" + motoCounter;
                                    motoCounter++;
                                    break;
                                case "Handicap":
                                    space.SpaceType = spaceTypeHandicap;
                                    space.Name = "Handicap-" + handicapCounter;
                                    handicapCounter++;
                                    break;
                                case "Bus":
                                    space.SpaceType = spaceTypeBus;
                                    space.Name = "Bus-" + busCounter;
                                    busCounter++;
                                    break;
                                case "Street":
                                    space.SpaceType = spaceTypeStreet;
                                    space.Name = "Street-" + streetCounter;
                                    streetCounter++;
                                    break;
                                case "Clear":
                                    space.SpaceType = spaceTypeClear;
                                    space.Name = "Clear-" + clearCounter;
                                    clearCounter++;
                                    break;
                            }
                            break;
                        case "6,2":
                            switch (hidden123.Value)
                            {
                                case "Car":
                                    space.SpaceType = spaceTypeCar;
                                    space.Name = "Car-" + carcounter;
                                    carcounter++;
                                    break;
                                case "Motorcycle":
                                    space.SpaceType = spaceTypeMoto;
                                    space.Name = "Motorcycle-" + motoCounter;
                                    motoCounter++;
                                    break;
                                case "Handicap":
                                    space.SpaceType = spaceTypeHandicap;
                                    space.Name = "Handicap-" + handicapCounter;
                                    handicapCounter++;
                                    break;
                                case "Bus":
                                    space.SpaceType = spaceTypeBus;
                                    space.Name = "Bus-" + busCounter;
                                    busCounter++;
                                    break;
                                case "Street":
                                    space.SpaceType = spaceTypeStreet;
                                    space.Name = "Street-" + streetCounter;
                                    streetCounter++;
                                    break;
                                case "Clear":
                                    space.SpaceType = spaceTypeClear;
                                    space.Name = "Clear-" + clearCounter;
                                    clearCounter++;
                                    break;
                            }
                            break;
                        case "6,3":
                            switch (hidden124.Value)
                            {
                                case "Car":
                                    space.SpaceType = spaceTypeCar;
                                    space.Name = "Car-" + carcounter;
                                    carcounter++;
                                    break;
                                case "Motorcycle":
                                    space.SpaceType = spaceTypeMoto;
                                    space.Name = "Motorcycle-" + motoCounter;
                                    motoCounter++;
                                    break;
                                case "Handicap":
                                    space.SpaceType = spaceTypeHandicap;
                                    space.Name = "Handicap-" + handicapCounter;
                                    handicapCounter++;
                                    break;
                                case "Bus":
                                    space.SpaceType = spaceTypeBus;
                                    space.Name = "Bus-" + busCounter;
                                    busCounter++;
                                    break;
                                case "Street":
                                    space.SpaceType = spaceTypeStreet;
                                    space.Name = "Street-" + streetCounter;
                                    streetCounter++;
                                    break;
                                case "Clear":
                                    space.SpaceType = spaceTypeClear;
                                    space.Name = "Clear-" + clearCounter;
                                    clearCounter++;
                                    break;
                            }
                            break;
                        case "6,4":
                            switch (hidden125.Value)
                            {
                                case "Car":
                                    space.SpaceType = spaceTypeCar;
                                    space.Name = "Car-" + carcounter;
                                    carcounter++;
                                    break;
                                case "Motorcycle":
                                    space.SpaceType = spaceTypeMoto;
                                    space.Name = "Motorcycle-" + motoCounter;
                                    motoCounter++;
                                    break;
                                case "Handicap":
                                    space.SpaceType = spaceTypeHandicap;
                                    space.Name = "Handicap-" + handicapCounter;
                                    handicapCounter++;
                                    break;
                                case "Bus":
                                    space.SpaceType = spaceTypeBus;
                                    space.Name = "Bus-" + busCounter;
                                    busCounter++;
                                    break;
                                case "Street":
                                    space.SpaceType = spaceTypeStreet;
                                    space.Name = "Street-" + streetCounter;
                                    streetCounter++;
                                    break;
                                case "Clear":
                                    space.SpaceType = spaceTypeClear;
                                    space.Name = "Clear-" + clearCounter;
                                    clearCounter++;
                                    break;
                            }
                            break;
                        case "6,5":
                            switch (hidden126.Value)
                            {
                                case "Car":
                                    space.SpaceType = spaceTypeCar;
                                    space.Name = "Car-" + carcounter;
                                    carcounter++;
                                    break;
                                case "Motorcycle":
                                    space.SpaceType = spaceTypeMoto;
                                    space.Name = "Motorcycle-" + motoCounter;
                                    motoCounter++;
                                    break;
                                case "Handicap":
                                    space.SpaceType = spaceTypeHandicap;
                                    space.Name = "Handicap-" + handicapCounter;
                                    handicapCounter++;
                                    break;
                                case "Bus":
                                    space.SpaceType = spaceTypeBus;
                                    space.Name = "Bus-" + busCounter;
                                    busCounter++;
                                    break;
                                case "Street":
                                    space.SpaceType = spaceTypeStreet;
                                    space.Name = "Street-" + streetCounter;
                                    streetCounter++;
                                    break;
                                case "Clear":
                                    space.SpaceType = spaceTypeClear;
                                    space.Name = "Clear-" + clearCounter;
                                    clearCounter++;
                                    break;
                            }
                            break;
                        case "6,6":
                            switch (hidden127.Value)
                            {
                                case "Car":
                                    space.SpaceType = spaceTypeCar;
                                    space.Name = "Car-" + carcounter;
                                    carcounter++;
                                    break;
                                case "Motorcycle":
                                    space.SpaceType = spaceTypeMoto;
                                    space.Name = "Motorcycle-" + motoCounter;
                                    motoCounter++;
                                    break;
                                case "Handicap":
                                    space.SpaceType = spaceTypeHandicap;
                                    space.Name = "Handicap-" + handicapCounter;
                                    handicapCounter++;
                                    break;
                                case "Bus":
                                    space.SpaceType = spaceTypeBus;
                                    space.Name = "Bus-" + busCounter;
                                    busCounter++;
                                    break;
                                case "Street":
                                    space.SpaceType = spaceTypeStreet;
                                    space.Name = "Street-" + streetCounter;
                                    streetCounter++;
                                    break;
                                case "Clear":
                                    space.SpaceType = spaceTypeClear;
                                    space.Name = "Clear-" + clearCounter;
                                    clearCounter++;
                                    break;
                            }
                            break;
                        case "6,7":
                            switch (hidden128.Value)
                            {
                                case "Car":
                                    space.SpaceType = spaceTypeCar;
                                    space.Name = "Car-" + carcounter;
                                    carcounter++;
                                    break;
                                case "Motorcycle":
                                    space.SpaceType = spaceTypeMoto;
                                    space.Name = "Motorcycle-" + motoCounter;
                                    motoCounter++;
                                    break;
                                case "Handicap":
                                    space.SpaceType = spaceTypeHandicap;
                                    space.Name = "Handicap-" + handicapCounter;
                                    handicapCounter++;
                                    break;
                                case "Bus":
                                    space.SpaceType = spaceTypeBus;
                                    space.Name = "Bus-" + busCounter;
                                    busCounter++;
                                    break;
                                case "Street":
                                    space.SpaceType = spaceTypeStreet;
                                    space.Name = "Street-" + streetCounter;
                                    streetCounter++;
                                    break;
                                case "Clear":
                                    space.SpaceType = spaceTypeClear;
                                    space.Name = "Clear-" + clearCounter;
                                    clearCounter++;
                                    break;
                            }
                            break;
                        case "6,8":
                            switch (hidden129.Value)
                            {
                                case "Car":
                                    space.SpaceType = spaceTypeCar;
                                    space.Name = "Car-" + carcounter;
                                    carcounter++;
                                    break;
                                case "Motorcycle":
                                    space.SpaceType = spaceTypeMoto;
                                    space.Name = "Motorcycle-" + motoCounter;
                                    motoCounter++;
                                    break;
                                case "Handicap":
                                    space.SpaceType = spaceTypeHandicap;
                                    space.Name = "Handicap-" + handicapCounter;
                                    handicapCounter++;
                                    break;
                                case "Bus":
                                    space.SpaceType = spaceTypeBus;
                                    space.Name = "Bus-" + busCounter;
                                    busCounter++;
                                    break;
                                case "Street":
                                    space.SpaceType = spaceTypeStreet;
                                    space.Name = "Street-" + streetCounter;
                                    streetCounter++;
                                    break;
                                case "Clear":
                                    space.SpaceType = spaceTypeClear;
                                    space.Name = "Clear-" + clearCounter;
                                    clearCounter++;
                                    break;
                            }
                            break;
                        case "6,9":
                            switch (hidden130.Value)
                            {
                                case "Car":
                                    space.SpaceType = spaceTypeCar;
                                    space.Name = "Car-" + carcounter;
                                    carcounter++;
                                    break;
                                case "Motorcycle":
                                    space.SpaceType = spaceTypeMoto;
                                    space.Name = "Motorcycle-" + motoCounter;
                                    motoCounter++;
                                    break;
                                case "Handicap":
                                    space.SpaceType = spaceTypeHandicap;
                                    space.Name = "Handicap-" + handicapCounter;
                                    handicapCounter++;
                                    break;
                                case "Bus":
                                    space.SpaceType = spaceTypeBus;
                                    space.Name = "Bus-" + busCounter;
                                    busCounter++;
                                    break;
                                case "Street":
                                    space.SpaceType = spaceTypeStreet;
                                    space.Name = "Street-" + streetCounter;
                                    streetCounter++;
                                    break;
                                case "Clear":
                                    space.SpaceType = spaceTypeClear;
                                    space.Name = "Clear-" + clearCounter;
                                    clearCounter++;
                                    break;
                            }
                            break;
                        case "6,10":
                            switch (hidden131.Value)
                            {
                                case "Car":
                                    space.SpaceType = spaceTypeCar;
                                    space.Name = "Car-" + carcounter;
                                    carcounter++;
                                    break;
                                case "Motorcycle":
                                    space.SpaceType = spaceTypeMoto;
                                    space.Name = "Motorcycle-" + motoCounter;
                                    motoCounter++;
                                    break;
                                case "Handicap":
                                    space.SpaceType = spaceTypeHandicap;
                                    space.Name = "Handicap-" + handicapCounter;
                                    handicapCounter++;
                                    break;
                                case "Bus":
                                    space.SpaceType = spaceTypeBus;
                                    space.Name = "Bus-" + busCounter;
                                    busCounter++;
                                    break;
                                case "Street":
                                    space.SpaceType = spaceTypeStreet;
                                    space.Name = "Street-" + streetCounter;
                                    streetCounter++;
                                    break;
                                case "Clear":
                                    space.SpaceType = spaceTypeClear;
                                    space.Name = "Clear-" + clearCounter;
                                    clearCounter++;
                                    break;
                            }
                            break;
                        case "6,11":
                            switch (hidden132.Value)
                            {
                                case "Car":
                                    space.SpaceType = spaceTypeCar;
                                    space.Name = "Car-" + carcounter;
                                    carcounter++;
                                    break;
                                case "Motorcycle":
                                    space.SpaceType = spaceTypeMoto;
                                    space.Name = "Motorcycle-" + motoCounter;
                                    motoCounter++;
                                    break;
                                case "Handicap":
                                    space.SpaceType = spaceTypeHandicap;
                                    space.Name = "Handicap-" + handicapCounter;
                                    handicapCounter++;
                                    break;
                                case "Bus":
                                    space.SpaceType = spaceTypeBus;
                                    space.Name = "Bus-" + busCounter;
                                    busCounter++;
                                    break;
                                case "Street":
                                    space.SpaceType = spaceTypeStreet;
                                    space.Name = "Street-" + streetCounter;
                                    streetCounter++;
                                    break;
                                case "Clear":
                                    space.SpaceType = spaceTypeClear;
                                    space.Name = "Clear-" + clearCounter;
                                    clearCounter++;
                                    break;
                            }
                            break;
                        case "6,12":
                            switch (hidden133.Value)
                            {
                                case "Car":
                                    space.SpaceType = spaceTypeCar;
                                    space.Name = "Car-" + carcounter;
                                    carcounter++;
                                    break;
                                case "Motorcycle":
                                    space.SpaceType = spaceTypeMoto;
                                    space.Name = "Motorcycle-" + motoCounter;
                                    motoCounter++;
                                    break;
                                case "Handicap":
                                    space.SpaceType = spaceTypeHandicap;
                                    space.Name = "Handicap-" + handicapCounter;
                                    handicapCounter++;
                                    break;
                                case "Bus":
                                    space.SpaceType = spaceTypeBus;
                                    space.Name = "Bus-" + busCounter;
                                    busCounter++;
                                    break;
                                case "Street":
                                    space.SpaceType = spaceTypeStreet;
                                    space.Name = "Street-" + streetCounter;
                                    streetCounter++;
                                    break;
                                case "Clear":
                                    space.SpaceType = spaceTypeClear;
                                    space.Name = "Clear-" + clearCounter;
                                    clearCounter++;
                                    break;
                            }
                            break;
                        case "6,13":
                            switch (hidden134.Value)
                            {
                                case "Car":
                                    space.SpaceType = spaceTypeCar;
                                    space.Name = "Car-" + carcounter;
                                    carcounter++;
                                    break;
                                case "Motorcycle":
                                    space.SpaceType = spaceTypeMoto;
                                    space.Name = "Motorcycle-" + motoCounter;
                                    motoCounter++;
                                    break;
                                case "Handicap":
                                    space.SpaceType = spaceTypeHandicap;
                                    space.Name = "Handicap-" + handicapCounter;
                                    handicapCounter++;
                                    break;
                                case "Bus":
                                    space.SpaceType = spaceTypeBus;
                                    space.Name = "Bus-" + busCounter;
                                    busCounter++;
                                    break;
                                case "Street":
                                    space.SpaceType = spaceTypeStreet;
                                    space.Name = "Street-" + streetCounter;
                                    streetCounter++;
                                    break;
                                case "Clear":
                                    space.SpaceType = spaceTypeClear;
                                    space.Name = "Clear-" + clearCounter;
                                    clearCounter++;
                                    break;
                            }
                            break;
                        case "6,14":
                            switch (hidden135.Value)
                            {
                                case "Car":
                                    space.SpaceType = spaceTypeCar;
                                    space.Name = "Car-" + carcounter;
                                    carcounter++;
                                    break;
                                case "Motorcycle":
                                    space.SpaceType = spaceTypeMoto;
                                    space.Name = "Motorcycle-" + motoCounter;
                                    motoCounter++;
                                    break;
                                case "Handicap":
                                    space.SpaceType = spaceTypeHandicap;
                                    space.Name = "Handicap-" + handicapCounter;
                                    handicapCounter++;
                                    break;
                                case "Bus":
                                    space.SpaceType = spaceTypeBus;
                                    space.Name = "Bus-" + busCounter;
                                    busCounter++;
                                    break;
                                case "Street":
                                    space.SpaceType = spaceTypeStreet;
                                    space.Name = "Street-" + streetCounter;
                                    streetCounter++;
                                    break;
                                case "Clear":
                                    space.SpaceType = spaceTypeClear;
                                    space.Name = "Clear-" + clearCounter;
                                    clearCounter++;
                                    break;
                            }
                            break;
                        case "6,15":
                            switch (hidden136.Value)
                            {
                                case "Car":
                                    space.SpaceType = spaceTypeCar;
                                    space.Name = "Car-" + carcounter;
                                    carcounter++;
                                    break;
                                case "Motorcycle":
                                    space.SpaceType = spaceTypeMoto;
                                    space.Name = "Motorcycle-" + motoCounter;
                                    motoCounter++;
                                    break;
                                case "Handicap":
                                    space.SpaceType = spaceTypeHandicap;
                                    space.Name = "Handicap-" + handicapCounter;
                                    handicapCounter++;
                                    break;
                                case "Bus":
                                    space.SpaceType = spaceTypeBus;
                                    space.Name = "Bus-" + busCounter;
                                    busCounter++;
                                    break;
                                case "Street":
                                    space.SpaceType = spaceTypeStreet;
                                    space.Name = "Street-" + streetCounter;
                                    streetCounter++;
                                    break;
                                case "Clear":
                                    space.SpaceType = spaceTypeClear;
                                    space.Name = "Clear-" + clearCounter;
                                    clearCounter++;
                                    break;
                            }
                            break;
                        case "6,16":
                            switch (hidden137.Value)
                            {
                                case "Car":
                                    space.SpaceType = spaceTypeCar;
                                    space.Name = "Car-" + carcounter;
                                    carcounter++;
                                    break;
                                case "Motorcycle":
                                    space.SpaceType = spaceTypeMoto;
                                    space.Name = "Motorcycle-" + motoCounter;
                                    motoCounter++;
                                    break;
                                case "Handicap":
                                    space.SpaceType = spaceTypeHandicap;
                                    space.Name = "Handicap-" + handicapCounter;
                                    handicapCounter++;
                                    break;
                                case "Bus":
                                    space.SpaceType = spaceTypeBus;
                                    space.Name = "Bus-" + busCounter;
                                    busCounter++;
                                    break;
                                case "Street":
                                    space.SpaceType = spaceTypeStreet;
                                    space.Name = "Street-" + streetCounter;
                                    streetCounter++;
                                    break;
                                case "Clear":
                                    space.SpaceType = spaceTypeClear;
                                    space.Name = "Clear-" + clearCounter;
                                    clearCounter++;
                                    break;
                            }
                            break;
                        case "6,17":
                            switch (hidden138.Value)
                            {
                                case "Car":
                                    space.SpaceType = spaceTypeCar;
                                    space.Name = "Car-" + carcounter;
                                    carcounter++;
                                    break;
                                case "Motorcycle":
                                    space.SpaceType = spaceTypeMoto;
                                    space.Name = "Motorcycle-" + motoCounter;
                                    motoCounter++;
                                    break;
                                case "Handicap":
                                    space.SpaceType = spaceTypeHandicap;
                                    space.Name = "Handicap-" + handicapCounter;
                                    handicapCounter++;
                                    break;
                                case "Bus":
                                    space.SpaceType = spaceTypeBus;
                                    space.Name = "Bus-" + busCounter;
                                    busCounter++;
                                    break;
                                case "Street":
                                    space.SpaceType = spaceTypeStreet;
                                    space.Name = "Street-" + streetCounter;
                                    streetCounter++;
                                    break;
                                case "Clear":
                                    space.SpaceType = spaceTypeClear;
                                    space.Name = "Clear-" + clearCounter;
                                    clearCounter++;
                                    break;
                            }
                            break;
                        case "6,18":
                            switch (hidden139.Value)
                            {
                                case "Car":
                                    space.SpaceType = spaceTypeCar;
                                    space.Name = "Car-" + carcounter;
                                    carcounter++;
                                    break;
                                case "Motorcycle":
                                    space.SpaceType = spaceTypeMoto;
                                    space.Name = "Motorcycle-" + motoCounter;
                                    motoCounter++;
                                    break;
                                case "Handicap":
                                    space.SpaceType = spaceTypeHandicap;
                                    space.Name = "Handicap-" + handicapCounter;
                                    handicapCounter++;
                                    break;
                                case "Bus":
                                    space.SpaceType = spaceTypeBus;
                                    space.Name = "Bus-" + busCounter;
                                    busCounter++;
                                    break;
                                case "Street":
                                    space.SpaceType = spaceTypeStreet;
                                    space.Name = "Street-" + streetCounter;
                                    streetCounter++;
                                    break;
                                case "Clear":
                                    space.SpaceType = spaceTypeClear;
                                    space.Name = "Clear-" + clearCounter;
                                    clearCounter++;
                                    break;
                            }
                            break;
                        case "6,19":
                            switch (hidden140.Value)
                            {
                                case "Car":
                                    space.SpaceType = spaceTypeCar;
                                    space.Name = "Car-" + carcounter;
                                    carcounter++;
                                    break;
                                case "Motorcycle":
                                    space.SpaceType = spaceTypeMoto;
                                    space.Name = "Motorcycle-" + motoCounter;
                                    motoCounter++;
                                    break;
                                case "Handicap":
                                    space.SpaceType = spaceTypeHandicap;
                                    space.Name = "Handicap-" + handicapCounter;
                                    handicapCounter++;
                                    break;
                                case "Bus":
                                    space.SpaceType = spaceTypeBus;
                                    space.Name = "Bus-" + busCounter;
                                    busCounter++;
                                    break;
                                case "Street":
                                    space.SpaceType = spaceTypeStreet;
                                    space.Name = "Street-" + streetCounter;
                                    streetCounter++;
                                    break;
                                case "Clear":
                                    space.SpaceType = spaceTypeClear;
                                    space.Name = "Clear-" + clearCounter;
                                    clearCounter++;
                                    break;
                            }
                            break;

                        case "7,0":
                            switch (hidden141.Value)
                            {
                                case "Car":
                                    space.SpaceType = spaceTypeCar;
                                    space.Name = "Car-" + carcounter;
                                    carcounter++;
                                    break;
                                case "Motorcycle":
                                    space.SpaceType = spaceTypeMoto;
                                    space.Name = "Motorcycle-" + motoCounter;
                                    motoCounter++;
                                    break;
                                case "Handicap":
                                    space.SpaceType = spaceTypeHandicap;
                                    space.Name = "Handicap-" + handicapCounter;
                                    handicapCounter++;
                                    break;
                                case "Bus":
                                    space.SpaceType = spaceTypeBus;
                                    space.Name = "Bus-" + busCounter;
                                    busCounter++;
                                    break;
                                case "Street":
                                    space.SpaceType = spaceTypeStreet;
                                    space.Name = "Street-" + streetCounter;
                                    streetCounter++;
                                    break;
                                case "Clear":
                                    space.SpaceType = spaceTypeClear;
                                    space.Name = "Clear-" + clearCounter;
                                    clearCounter++;
                                    break;
                            }
                            break;
                        case "7,1":
                            switch (hidden142.Value)
                            {
                                case "Car":
                                    space.SpaceType = spaceTypeCar;
                                    space.Name = "Car-" + carcounter;
                                    carcounter++;
                                    break;
                                case "Motorcycle":
                                    space.SpaceType = spaceTypeMoto;
                                    space.Name = "Motorcycle-" + motoCounter;
                                    motoCounter++;
                                    break;
                                case "Handicap":
                                    space.SpaceType = spaceTypeHandicap;
                                    space.Name = "Handicap-" + handicapCounter;
                                    handicapCounter++;
                                    break;
                                case "Bus":
                                    space.SpaceType = spaceTypeBus;
                                    space.Name = "Bus-" + busCounter;
                                    busCounter++;
                                    break;
                                case "Street":
                                    space.SpaceType = spaceTypeStreet;
                                    space.Name = "Street-" + streetCounter;
                                    streetCounter++;
                                    break;
                                case "Clear":
                                    space.SpaceType = spaceTypeClear;
                                    space.Name = "Clear-" + clearCounter;
                                    clearCounter++;
                                    break;
                            }
                            break;
                        case "7,2":
                            switch (hidden143.Value)
                            {
                                case "Car":
                                    space.SpaceType = spaceTypeCar;
                                    space.Name = "Car-" + carcounter;
                                    carcounter++;
                                    break;
                                case "Motorcycle":
                                    space.SpaceType = spaceTypeMoto;
                                    space.Name = "Motorcycle-" + motoCounter;
                                    motoCounter++;
                                    break;
                                case "Handicap":
                                    space.SpaceType = spaceTypeHandicap;
                                    space.Name = "Handicap-" + handicapCounter;
                                    handicapCounter++;
                                    break;
                                case "Bus":
                                    space.SpaceType = spaceTypeBus;
                                    space.Name = "Bus-" + busCounter;
                                    busCounter++;
                                    break;
                                case "Street":
                                    space.SpaceType = spaceTypeStreet;
                                    space.Name = "Street-" + streetCounter;
                                    streetCounter++;
                                    break;
                                case "Clear":
                                    space.SpaceType = spaceTypeClear;
                                    space.Name = "Clear-" + clearCounter;
                                    clearCounter++;
                                    break;
                            }
                            break;
                        case "7,3":
                            switch (hidden144.Value)
                            {
                                case "Car":
                                    space.SpaceType = spaceTypeCar;
                                    space.Name = "Car-" + carcounter;
                                    carcounter++;
                                    break;
                                case "Motorcycle":
                                    space.SpaceType = spaceTypeMoto;
                                    space.Name = "Motorcycle-" + motoCounter;
                                    motoCounter++;
                                    break;
                                case "Handicap":
                                    space.SpaceType = spaceTypeHandicap;
                                    space.Name = "Handicap-" + handicapCounter;
                                    handicapCounter++;
                                    break;
                                case "Bus":
                                    space.SpaceType = spaceTypeBus;
                                    space.Name = "Bus-" + busCounter;
                                    busCounter++;
                                    break;
                                case "Street":
                                    space.SpaceType = spaceTypeStreet;
                                    space.Name = "Street-" + streetCounter;
                                    streetCounter++;
                                    break;
                                case "Clear":
                                    space.SpaceType = spaceTypeClear;
                                    space.Name = "Clear-" + clearCounter;
                                    clearCounter++;
                                    break;
                            }
                            break;
                        case "7,4":
                            switch (hidden145.Value)
                            {
                                case "Car":
                                    space.SpaceType = spaceTypeCar;
                                    space.Name = "Car-" + carcounter;
                                    carcounter++;
                                    break;
                                case "Motorcycle":
                                    space.SpaceType = spaceTypeMoto;
                                    space.Name = "Motorcycle-" + motoCounter;
                                    motoCounter++;
                                    break;
                                case "Handicap":
                                    space.SpaceType = spaceTypeHandicap;
                                    space.Name = "Handicap-" + handicapCounter;
                                    handicapCounter++;
                                    break;
                                case "Bus":
                                    space.SpaceType = spaceTypeBus;
                                    space.Name = "Bus-" + busCounter;
                                    busCounter++;
                                    break;
                                case "Street":
                                    space.SpaceType = spaceTypeStreet;
                                    space.Name = "Street-" + streetCounter;
                                    streetCounter++;
                                    break;
                                case "Clear":
                                    space.SpaceType = spaceTypeClear;
                                    space.Name = "Clear-" + clearCounter;
                                    clearCounter++;
                                    break;
                            }
                            break;
                        case "7,5":
                            switch (hidden156.Value)
                            {
                                case "Car":
                                    space.SpaceType = spaceTypeCar;
                                    space.Name = "Car-" + carcounter;
                                    carcounter++;
                                    break;
                                case "Motorcycle":
                                    space.SpaceType = spaceTypeMoto;
                                    space.Name = "Motorcycle-" + motoCounter;
                                    motoCounter++;
                                    break;
                                case "Handicap":
                                    space.SpaceType = spaceTypeHandicap;
                                    space.Name = "Handicap-" + handicapCounter;
                                    handicapCounter++;
                                    break;
                                case "Bus":
                                    space.SpaceType = spaceTypeBus;
                                    space.Name = "Bus-" + busCounter;
                                    busCounter++;
                                    break;
                                case "Street":
                                    space.SpaceType = spaceTypeStreet;
                                    space.Name = "Street-" + streetCounter;
                                    streetCounter++;
                                    break;
                                case "Clear":
                                    space.SpaceType = spaceTypeClear;
                                    space.Name = "Clear-" + clearCounter;
                                    clearCounter++;
                                    break;
                            }
                            break;
                        case "7,6":
                            switch (hidden147.Value)
                            {
                                case "Car":
                                    space.SpaceType = spaceTypeCar;
                                    space.Name = "Car-" + carcounter;
                                    carcounter++;
                                    break;
                                case "Motorcycle":
                                    space.SpaceType = spaceTypeMoto;
                                    space.Name = "Motorcycle-" + motoCounter;
                                    motoCounter++;
                                    break;
                                case "Handicap":
                                    space.SpaceType = spaceTypeHandicap;
                                    space.Name = "Handicap-" + handicapCounter;
                                    handicapCounter++;
                                    break;
                                case "Bus":
                                    space.SpaceType = spaceTypeBus;
                                    space.Name = "Bus-" + busCounter;
                                    busCounter++;
                                    break;
                                case "Street":
                                    space.SpaceType = spaceTypeStreet;
                                    space.Name = "Street-" + streetCounter;
                                    streetCounter++;
                                    break;
                                case "Clear":
                                    space.SpaceType = spaceTypeClear;
                                    space.Name = "Clear-" + clearCounter;
                                    clearCounter++;
                                    break;
                            }
                            break;
                        case "7,7":
                            switch (hidden148.Value)
                            {
                                case "Car":
                                    space.SpaceType = spaceTypeCar;
                                    space.Name = "Car-" + carcounter;
                                    carcounter++;
                                    break;
                                case "Motorcycle":
                                    space.SpaceType = spaceTypeMoto;
                                    space.Name = "Motorcycle-" + motoCounter;
                                    motoCounter++;
                                    break;
                                case "Handicap":
                                    space.SpaceType = spaceTypeHandicap;
                                    space.Name = "Handicap-" + handicapCounter;
                                    handicapCounter++;
                                    break;
                                case "Bus":
                                    space.SpaceType = spaceTypeBus;
                                    space.Name = "Bus-" + busCounter;
                                    busCounter++;
                                    break;
                                case "Street":
                                    space.SpaceType = spaceTypeStreet;
                                    space.Name = "Street-" + streetCounter;
                                    streetCounter++;
                                    break;
                                case "Clear":
                                    space.SpaceType = spaceTypeClear;
                                    space.Name = "Clear-" + clearCounter;
                                    clearCounter++;
                                    break;
                            }
                            break;
                        case "7,8":
                            switch (hidden149.Value)
                            {
                                case "Car":
                                    space.SpaceType = spaceTypeCar;
                                    space.Name = "Car-" + carcounter;
                                    carcounter++;
                                    break;
                                case "Motorcycle":
                                    space.SpaceType = spaceTypeMoto;
                                    space.Name = "Motorcycle-" + motoCounter;
                                    motoCounter++;
                                    break;
                                case "Handicap":
                                    space.SpaceType = spaceTypeHandicap;
                                    space.Name = "Handicap-" + handicapCounter;
                                    handicapCounter++;
                                    break;
                                case "Bus":
                                    space.SpaceType = spaceTypeBus;
                                    space.Name = "Bus-" + busCounter;
                                    busCounter++;
                                    break;
                                case "Street":
                                    space.SpaceType = spaceTypeStreet;
                                    space.Name = "Street-" + streetCounter;
                                    streetCounter++;
                                    break;
                                case "Clear":
                                    space.SpaceType = spaceTypeClear;
                                    space.Name = "Clear-" + clearCounter;
                                    clearCounter++;
                                    break;
                            }
                            break;
                        case "7,9":
                            switch (hidden150.Value)
                            {
                                case "Car":
                                    space.SpaceType = spaceTypeCar;
                                    space.Name = "Car-" + carcounter;
                                    carcounter++;
                                    break;
                                case "Motorcycle":
                                    space.SpaceType = spaceTypeMoto;
                                    space.Name = "Motorcycle-" + motoCounter;
                                    motoCounter++;
                                    break;
                                case "Handicap":
                                    space.SpaceType = spaceTypeHandicap;
                                    space.Name = "Handicap-" + handicapCounter;
                                    handicapCounter++;
                                    break;
                                case "Bus":
                                    space.SpaceType = spaceTypeBus;
                                    space.Name = "Bus-" + busCounter;
                                    busCounter++;
                                    break;
                                case "Street":
                                    space.SpaceType = spaceTypeStreet;
                                    space.Name = "Street-" + streetCounter;
                                    streetCounter++;
                                    break;
                                case "Clear":
                                    space.SpaceType = spaceTypeClear;
                                    space.Name = "Clear-" + clearCounter;
                                    clearCounter++;
                                    break;
                            }
                            break;
                        case "7,10":
                            switch (hidden151.Value)
                            {
                                case "Car":
                                    space.SpaceType = spaceTypeCar;
                                    space.Name = "Car-" + carcounter;
                                    carcounter++;
                                    break;
                                case "Motorcycle":
                                    space.SpaceType = spaceTypeMoto;
                                    space.Name = "Motorcycle-" + motoCounter;
                                    motoCounter++;
                                    break;
                                case "Handicap":
                                    space.SpaceType = spaceTypeHandicap;
                                    space.Name = "Handicap-" + handicapCounter;
                                    handicapCounter++;
                                    break;
                                case "Bus":
                                    space.SpaceType = spaceTypeBus;
                                    space.Name = "Bus-" + busCounter;
                                    busCounter++;
                                    break;
                                case "Street":
                                    space.SpaceType = spaceTypeStreet;
                                    space.Name = "Street-" + streetCounter;
                                    streetCounter++;
                                    break;
                                case "Clear":
                                    space.SpaceType = spaceTypeClear;
                                    space.Name = "Clear-" + clearCounter;
                                    clearCounter++;
                                    break;
                            }
                            break;
                        case "7,11":
                            switch (hidden152.Value)
                            {
                                case "Car":
                                    space.SpaceType = spaceTypeCar;
                                    space.Name = "Car-" + carcounter;
                                    carcounter++;
                                    break;
                                case "Motorcycle":
                                    space.SpaceType = spaceTypeMoto;
                                    space.Name = "Motorcycle-" + motoCounter;
                                    motoCounter++;
                                    break;
                                case "Handicap":
                                    space.SpaceType = spaceTypeHandicap;
                                    space.Name = "Handicap-" + handicapCounter;
                                    handicapCounter++;
                                    break;
                                case "Bus":
                                    space.SpaceType = spaceTypeBus;
                                    space.Name = "Bus-" + busCounter;
                                    busCounter++;
                                    break;
                                case "Street":
                                    space.SpaceType = spaceTypeStreet;
                                    space.Name = "Street-" + streetCounter;
                                    streetCounter++;
                                    break;
                                case "Clear":
                                    space.SpaceType = spaceTypeClear;
                                    space.Name = "Clear-" + clearCounter;
                                    clearCounter++;
                                    break;
                            }
                            break;
                        case "7,12":
                            switch (hidden153.Value)
                            {
                                case "Car":
                                    space.SpaceType = spaceTypeCar;
                                    space.Name = "Car-" + carcounter;
                                    carcounter++;
                                    break;
                                case "Motorcycle":
                                    space.SpaceType = spaceTypeMoto;
                                    space.Name = "Motorcycle-" + motoCounter;
                                    motoCounter++;
                                    break;
                                case "Handicap":
                                    space.SpaceType = spaceTypeHandicap;
                                    space.Name = "Handicap-" + handicapCounter;
                                    handicapCounter++;
                                    break;
                                case "Bus":
                                    space.SpaceType = spaceTypeBus;
                                    space.Name = "Bus-" + busCounter;
                                    busCounter++;
                                    break;
                                case "Street":
                                    space.SpaceType = spaceTypeStreet;
                                    space.Name = "Street-" + streetCounter;
                                    streetCounter++;
                                    break;
                                case "Clear":
                                    space.SpaceType = spaceTypeClear;
                                    space.Name = "Clear-" + clearCounter;
                                    clearCounter++;
                                    break;
                            }
                            break;
                        case "7,13":
                            switch (hidden154.Value)
                            {
                                case "Car":
                                    space.SpaceType = spaceTypeCar;
                                    space.Name = "Car-" + carcounter;
                                    carcounter++;
                                    break;
                                case "Motorcycle":
                                    space.SpaceType = spaceTypeMoto;
                                    space.Name = "Motorcycle-" + motoCounter;
                                    motoCounter++;
                                    break;
                                case "Handicap":
                                    space.SpaceType = spaceTypeHandicap;
                                    space.Name = "Handicap-" + handicapCounter;
                                    handicapCounter++;
                                    break;
                                case "Bus":
                                    space.SpaceType = spaceTypeBus;
                                    space.Name = "Bus-" + busCounter;
                                    busCounter++;
                                    break;
                                case "Street":
                                    space.SpaceType = spaceTypeStreet;
                                    space.Name = "Street-" + streetCounter;
                                    streetCounter++;
                                    break;
                                case "Clear":
                                    space.SpaceType = spaceTypeClear;
                                    space.Name = "Clear-" + clearCounter;
                                    clearCounter++;
                                    break;
                            }
                            break;
                        case "7,14":
                            switch (hidden155.Value)
                            {
                                case "Car":
                                    space.SpaceType = spaceTypeCar;
                                    space.Name = "Car-" + carcounter;
                                    carcounter++;
                                    break;
                                case "Motorcycle":
                                    space.SpaceType = spaceTypeMoto;
                                    space.Name = "Motorcycle-" + motoCounter;
                                    motoCounter++;
                                    break;
                                case "Handicap":
                                    space.SpaceType = spaceTypeHandicap;
                                    space.Name = "Handicap-" + handicapCounter;
                                    handicapCounter++;
                                    break;
                                case "Bus":
                                    space.SpaceType = spaceTypeBus;
                                    space.Name = "Bus-" + busCounter;
                                    busCounter++;
                                    break;
                                case "Street":
                                    space.SpaceType = spaceTypeStreet;
                                    space.Name = "Street-" + streetCounter;
                                    streetCounter++;
                                    break;
                                case "Clear":
                                    space.SpaceType = spaceTypeClear;
                                    space.Name = "Clear-" + clearCounter;
                                    clearCounter++;
                                    break;
                            }
                            break;
                        case "7,15":
                            switch (hidden156.Value)
                            {
                                case "Car":
                                    space.SpaceType = spaceTypeCar;
                                    space.Name = "Car-" + carcounter;
                                    carcounter++;
                                    break;
                                case "Motorcycle":
                                    space.SpaceType = spaceTypeMoto;
                                    space.Name = "Motorcycle-" + motoCounter;
                                    motoCounter++;
                                    break;
                                case "Handicap":
                                    space.SpaceType = spaceTypeHandicap;
                                    space.Name = "Handicap-" + handicapCounter;
                                    handicapCounter++;
                                    break;
                                case "Bus":
                                    space.SpaceType = spaceTypeBus;
                                    space.Name = "Bus-" + busCounter;
                                    busCounter++;
                                    break;
                                case "Street":
                                    space.SpaceType = spaceTypeStreet;
                                    space.Name = "Street-" + streetCounter;
                                    streetCounter++;
                                    break;
                                case "Clear":
                                    space.SpaceType = spaceTypeClear;
                                    space.Name = "Clear-" + clearCounter;
                                    clearCounter++;
                                    break;
                            }
                            break;
                        case "7,16":
                            switch (hidden157.Value)
                            {
                                case "Car":
                                    space.SpaceType = spaceTypeCar;
                                    space.Name = "Car-" + carcounter;
                                    carcounter++;
                                    break;
                                case "Motorcycle":
                                    space.SpaceType = spaceTypeMoto;
                                    space.Name = "Motorcycle-" + motoCounter;
                                    motoCounter++;
                                    break;
                                case "Handicap":
                                    space.SpaceType = spaceTypeHandicap;
                                    space.Name = "Handicap-" + handicapCounter;
                                    handicapCounter++;
                                    break;
                                case "Bus":
                                    space.SpaceType = spaceTypeBus;
                                    space.Name = "Bus-" + busCounter;
                                    busCounter++;
                                    break;
                                case "Street":
                                    space.SpaceType = spaceTypeStreet;
                                    space.Name = "Street-" + streetCounter;
                                    streetCounter++;
                                    break;
                                case "Clear":
                                    space.SpaceType = spaceTypeClear;
                                    space.Name = "Clear-" + clearCounter;
                                    clearCounter++;
                                    break;
                            }
                            break;
                        case "7,17":
                            switch (hidden158.Value)
                            {
                                case "Car":
                                    space.SpaceType = spaceTypeCar;
                                    space.Name = "Car-" + carcounter;
                                    carcounter++;
                                    break;
                                case "Motorcycle":
                                    space.SpaceType = spaceTypeMoto;
                                    space.Name = "Motorcycle-" + motoCounter;
                                    motoCounter++;
                                    break;
                                case "Handicap":
                                    space.SpaceType = spaceTypeHandicap;
                                    space.Name = "Handicap-" + handicapCounter;
                                    handicapCounter++;
                                    break;
                                case "Bus":
                                    space.SpaceType = spaceTypeBus;
                                    space.Name = "Bus-" + busCounter;
                                    busCounter++;
                                    break;
                                case "Street":
                                    space.SpaceType = spaceTypeStreet;
                                    space.Name = "Street-" + streetCounter;
                                    streetCounter++;
                                    break;
                                case "Clear":
                                    space.SpaceType = spaceTypeClear;
                                    space.Name = "Clear-" + clearCounter;
                                    clearCounter++;
                                    break;
                            }
                            break;
                        case "7,18":
                            switch (hidden159.Value)
                            {
                                case "Car":
                                    space.SpaceType = spaceTypeCar;
                                    space.Name = "Car-" + carcounter;
                                    carcounter++;
                                    break;
                                case "Motorcycle":
                                    space.SpaceType = spaceTypeMoto;
                                    space.Name = "Motorcycle-" + motoCounter;
                                    motoCounter++;
                                    break;
                                case "Handicap":
                                    space.SpaceType = spaceTypeHandicap;
                                    space.Name = "Handicap-" + handicapCounter;
                                    handicapCounter++;
                                    break;
                                case "Bus":
                                    space.SpaceType = spaceTypeBus;
                                    space.Name = "Bus-" + busCounter;
                                    busCounter++;
                                    break;
                                case "Street":
                                    space.SpaceType = spaceTypeStreet;
                                    space.Name = "Street-" + streetCounter;
                                    streetCounter++;
                                    break;
                                case "Clear":
                                    space.SpaceType = spaceTypeClear;
                                    space.Name = "Clear-" + clearCounter;
                                    clearCounter++;
                                    break;
                            }
                            break;
                        case "7,19":
                            switch (hidden160.Value)
                            {
                                case "Car":
                                    space.SpaceType = spaceTypeCar;
                                    space.Name = "Car-" + carcounter;
                                    carcounter++;
                                    break;
                                case "Motorcycle":
                                    space.SpaceType = spaceTypeMoto;
                                    space.Name = "Motorcycle-" + motoCounter;
                                    motoCounter++;
                                    break;
                                case "Handicap":
                                    space.SpaceType = spaceTypeHandicap;
                                    space.Name = "Handicap-" + handicapCounter;
                                    handicapCounter++;
                                    break;
                                case "Bus":
                                    space.SpaceType = spaceTypeBus;
                                    space.Name = "Bus-" + busCounter;
                                    busCounter++;
                                    break;
                                case "Street":
                                    space.SpaceType = spaceTypeStreet;
                                    space.Name = "Street-" + streetCounter;
                                    streetCounter++;
                                    break;
                                case "Clear":
                                    space.SpaceType = spaceTypeClear;
                                    space.Name = "Clear-" + clearCounter;
                                    clearCounter++;
                                    break;
                            }
                            break;

                        case "8,0":
                            switch (hidden161.Value)
                            {
                                case "Car":
                                    space.SpaceType = spaceTypeCar;
                                    space.Name = "Car-" + carcounter;
                                    carcounter++;
                                    break;
                                case "Motorcycle":
                                    space.SpaceType = spaceTypeMoto;
                                    space.Name = "Motorcycle-" + motoCounter;
                                    motoCounter++;
                                    break;
                                case "Handicap":
                                    space.SpaceType = spaceTypeHandicap;
                                    space.Name = "Handicap-" + handicapCounter;
                                    handicapCounter++;
                                    break;
                                case "Bus":
                                    space.SpaceType = spaceTypeBus;
                                    space.Name = "Bus-" + busCounter;
                                    busCounter++;
                                    break;
                                case "Street":
                                    space.SpaceType = spaceTypeStreet;
                                    space.Name = "Street-" + streetCounter;
                                    streetCounter++;
                                    break;
                                case "Clear":
                                    space.SpaceType = spaceTypeClear;
                                    space.Name = "Clear-" + clearCounter;
                                    clearCounter++;
                                    break;
                            }
                            break;
                        case "8,1":
                            switch (hidden162.Value)
                            {
                                case "Car":
                                    space.SpaceType = spaceTypeCar;
                                    space.Name = "Car-" + carcounter;
                                    carcounter++;
                                    break;
                                case "Motorcycle":
                                    space.SpaceType = spaceTypeMoto;
                                    space.Name = "Motorcycle-" + motoCounter;
                                    motoCounter++;
                                    break;
                                case "Handicap":
                                    space.SpaceType = spaceTypeHandicap;
                                    space.Name = "Handicap-" + handicapCounter;
                                    handicapCounter++;
                                    break;
                                case "Bus":
                                    space.SpaceType = spaceTypeBus;
                                    space.Name = "Bus-" + busCounter;
                                    busCounter++;
                                    break;
                                case "Street":
                                    space.SpaceType = spaceTypeStreet;
                                    space.Name = "Street-" + streetCounter;
                                    streetCounter++;
                                    break;
                                case "Clear":
                                    space.SpaceType = spaceTypeClear;
                                    space.Name = "Clear-" + clearCounter;
                                    clearCounter++;
                                    break;
                            }
                            break;
                        case "8,2":
                            switch (hidden163.Value)
                            {
                                case "Car":
                                    space.SpaceType = spaceTypeCar;
                                    space.Name = "Car-" + carcounter;
                                    carcounter++;
                                    break;
                                case "Motorcycle":
                                    space.SpaceType = spaceTypeMoto;
                                    space.Name = "Motorcycle-" + motoCounter;
                                    motoCounter++;
                                    break;
                                case "Handicap":
                                    space.SpaceType = spaceTypeHandicap;
                                    space.Name = "Handicap-" + handicapCounter;
                                    handicapCounter++;
                                    break;
                                case "Bus":
                                    space.SpaceType = spaceTypeBus;
                                    space.Name = "Bus-" + busCounter;
                                    busCounter++;
                                    break;
                                case "Street":
                                    space.SpaceType = spaceTypeStreet;
                                    space.Name = "Street-" + streetCounter;
                                    streetCounter++;
                                    break;
                                case "Clear":
                                    space.SpaceType = spaceTypeClear;
                                    space.Name = "Clear-" + clearCounter;
                                    clearCounter++;
                                    break;
                            }
                            break;
                        case "8,3":
                            switch (hidden164.Value)
                            {
                                case "Car":
                                    space.SpaceType = spaceTypeCar;
                                    space.Name = "Car-" + carcounter;
                                    carcounter++;
                                    break;
                                case "Motorcycle":
                                    space.SpaceType = spaceTypeMoto;
                                    space.Name = "Motorcycle-" + motoCounter;
                                    motoCounter++;
                                    break;
                                case "Handicap":
                                    space.SpaceType = spaceTypeHandicap;
                                    space.Name = "Handicap-" + handicapCounter;
                                    handicapCounter++;
                                    break;
                                case "Bus":
                                    space.SpaceType = spaceTypeBus;
                                    space.Name = "Bus-" + busCounter;
                                    busCounter++;
                                    break;
                                case "Street":
                                    space.SpaceType = spaceTypeStreet;
                                    space.Name = "Street-" + streetCounter;
                                    streetCounter++;
                                    break;
                                case "Clear":
                                    space.SpaceType = spaceTypeClear;
                                    space.Name = "Clear-" + clearCounter;
                                    clearCounter++;
                                    break;
                            }
                            break;
                        case "8,4":
                            switch (hidden165.Value)
                            {
                                case "Car":
                                    space.SpaceType = spaceTypeCar;
                                    space.Name = "Car-" + carcounter;
                                    carcounter++;
                                    break;
                                case "Motorcycle":
                                    space.SpaceType = spaceTypeMoto;
                                    space.Name = "Motorcycle-" + motoCounter;
                                    motoCounter++;
                                    break;
                                case "Handicap":
                                    space.SpaceType = spaceTypeHandicap;
                                    space.Name = "Handicap-" + handicapCounter;
                                    handicapCounter++;
                                    break;
                                case "Bus":
                                    space.SpaceType = spaceTypeBus;
                                    space.Name = "Bus-" + busCounter;
                                    busCounter++;
                                    break;
                                case "Street":
                                    space.SpaceType = spaceTypeStreet;
                                    space.Name = "Street-" + streetCounter;
                                    streetCounter++;
                                    break;
                                case "Clear":
                                    space.SpaceType = spaceTypeClear;
                                    space.Name = "Clear-" + clearCounter;
                                    clearCounter++;
                                    break;
                            }
                            break;
                        case "8,5":
                            switch (hidden166.Value)
                            {
                                case "Car":
                                    space.SpaceType = spaceTypeCar;
                                    space.Name = "Car-" + carcounter;
                                    carcounter++;
                                    break;
                                case "Motorcycle":
                                    space.SpaceType = spaceTypeMoto;
                                    space.Name = "Motorcycle-" + motoCounter;
                                    motoCounter++;
                                    break;
                                case "Handicap":
                                    space.SpaceType = spaceTypeHandicap;
                                    space.Name = "Handicap-" + handicapCounter;
                                    handicapCounter++;
                                    break;
                                case "Bus":
                                    space.SpaceType = spaceTypeBus;
                                    space.Name = "Bus-" + busCounter;
                                    busCounter++;
                                    break;
                                case "Street":
                                    space.SpaceType = spaceTypeStreet;
                                    space.Name = "Street-" + streetCounter;
                                    streetCounter++;
                                    break;
                                case "Clear":
                                    space.SpaceType = spaceTypeClear;
                                    space.Name = "Clear-" + clearCounter;
                                    clearCounter++;
                                    break;
                            }
                            break;
                        case "8,6":
                            switch (hidden167.Value)
                            {
                                case "Car":
                                    space.SpaceType = spaceTypeCar;
                                    space.Name = "Car-" + carcounter;
                                    carcounter++;
                                    break;
                                case "Motorcycle":
                                    space.SpaceType = spaceTypeMoto;
                                    space.Name = "Motorcycle-" + motoCounter;
                                    motoCounter++;
                                    break;
                                case "Handicap":
                                    space.SpaceType = spaceTypeHandicap;
                                    space.Name = "Handicap-" + handicapCounter;
                                    handicapCounter++;
                                    break;
                                case "Bus":
                                    space.SpaceType = spaceTypeBus;
                                    space.Name = "Bus-" + busCounter;
                                    busCounter++;
                                    break;
                                case "Street":
                                    space.SpaceType = spaceTypeStreet;
                                    space.Name = "Street-" + streetCounter;
                                    streetCounter++;
                                    break;
                                case "Clear":
                                    space.SpaceType = spaceTypeClear;
                                    space.Name = "Clear-" + clearCounter;
                                    clearCounter++;
                                    break;
                            }
                            break;
                        case "8,7":
                            switch (hidden168.Value)
                            {
                                case "Car":
                                    space.SpaceType = spaceTypeCar;
                                    space.Name = "Car-" + carcounter;
                                    carcounter++;
                                    break;
                                case "Motorcycle":
                                    space.SpaceType = spaceTypeMoto;
                                    space.Name = "Motorcycle-" + motoCounter;
                                    motoCounter++;
                                    break;
                                case "Handicap":
                                    space.SpaceType = spaceTypeHandicap;
                                    space.Name = "Handicap-" + handicapCounter;
                                    handicapCounter++;
                                    break;
                                case "Bus":
                                    space.SpaceType = spaceTypeBus;
                                    space.Name = "Bus-" + busCounter;
                                    busCounter++;
                                    break;
                                case "Street":
                                    space.SpaceType = spaceTypeStreet;
                                    space.Name = "Street-" + streetCounter;
                                    streetCounter++;
                                    break;
                                case "Clear":
                                    space.SpaceType = spaceTypeClear;
                                    space.Name = "Clear-" + clearCounter;
                                    clearCounter++;
                                    break;
                            }
                            break;
                        case "8,8":
                            switch (hidden169.Value)
                            {
                                case "Car":
                                    space.SpaceType = spaceTypeCar;
                                    space.Name = "Car-" + carcounter;
                                    carcounter++;
                                    break;
                                case "Motorcycle":
                                    space.SpaceType = spaceTypeMoto;
                                    space.Name = "Motorcycle-" + motoCounter;
                                    motoCounter++;
                                    break;
                                case "Handicap":
                                    space.SpaceType = spaceTypeHandicap;
                                    space.Name = "Handicap-" + handicapCounter;
                                    handicapCounter++;
                                    break;
                                case "Bus":
                                    space.SpaceType = spaceTypeBus;
                                    space.Name = "Bus-" + busCounter;
                                    busCounter++;
                                    break;
                                case "Street":
                                    space.SpaceType = spaceTypeStreet;
                                    space.Name = "Street-" + streetCounter;
                                    streetCounter++;
                                    break;
                                case "Clear":
                                    space.SpaceType = spaceTypeClear;
                                    space.Name = "Clear-" + clearCounter;
                                    clearCounter++;
                                    break;
                            }
                            break;
                        case "8,9":
                            switch (hidden170.Value)
                            {
                                case "Car":
                                    space.SpaceType = spaceTypeCar;
                                    space.Name = "Car-" + carcounter;
                                    carcounter++;
                                    break;
                                case "Motorcycle":
                                    space.SpaceType = spaceTypeMoto;
                                    space.Name = "Motorcycle-" + motoCounter;
                                    motoCounter++;
                                    break;
                                case "Handicap":
                                    space.SpaceType = spaceTypeHandicap;
                                    space.Name = "Handicap-" + handicapCounter;
                                    handicapCounter++;
                                    break;
                                case "Bus":
                                    space.SpaceType = spaceTypeBus;
                                    space.Name = "Bus-" + busCounter;
                                    busCounter++;
                                    break;
                                case "Street":
                                    space.SpaceType = spaceTypeStreet;
                                    space.Name = "Street-" + streetCounter;
                                    streetCounter++;
                                    break;
                                case "Clear":
                                    space.SpaceType = spaceTypeClear;
                                    space.Name = "Clear-" + clearCounter;
                                    clearCounter++;
                                    break;
                            }
                            break;
                        case "8,10":
                            switch (hidden171.Value)
                            {
                                case "Car":
                                    space.SpaceType = spaceTypeCar;
                                    space.Name = "Car-" + carcounter;
                                    carcounter++;
                                    break;
                                case "Motorcycle":
                                    space.SpaceType = spaceTypeMoto;
                                    space.Name = "Motorcycle-" + motoCounter;
                                    motoCounter++;
                                    break;
                                case "Handicap":
                                    space.SpaceType = spaceTypeHandicap;
                                    space.Name = "Handicap-" + handicapCounter;
                                    handicapCounter++;
                                    break;
                                case "Bus":
                                    space.SpaceType = spaceTypeBus;
                                    space.Name = "Bus-" + busCounter;
                                    busCounter++;
                                    break;
                                case "Street":
                                    space.SpaceType = spaceTypeStreet;
                                    space.Name = "Street-" + streetCounter;
                                    streetCounter++;
                                    break;
                                case "Clear":
                                    space.SpaceType = spaceTypeClear;
                                    space.Name = "Clear-" + clearCounter;
                                    clearCounter++;
                                    break;
                            }
                            break;
                        case "8,11":
                            switch (hidden172.Value)
                            {
                                case "Car":
                                    space.SpaceType = spaceTypeCar;
                                    space.Name = "Car-" + carcounter;
                                    carcounter++;
                                    break;
                                case "Motorcycle":
                                    space.SpaceType = spaceTypeMoto;
                                    space.Name = "Motorcycle-" + motoCounter;
                                    motoCounter++;
                                    break;
                                case "Handicap":
                                    space.SpaceType = spaceTypeHandicap;
                                    space.Name = "Handicap-" + handicapCounter;
                                    handicapCounter++;
                                    break;
                                case "Bus":
                                    space.SpaceType = spaceTypeBus;
                                    space.Name = "Bus-" + busCounter;
                                    busCounter++;
                                    break;
                                case "Street":
                                    space.SpaceType = spaceTypeStreet;
                                    space.Name = "Street-" + streetCounter;
                                    streetCounter++;
                                    break;
                                case "Clear":
                                    space.SpaceType = spaceTypeClear;
                                    space.Name = "Clear-" + clearCounter;
                                    clearCounter++;
                                    break;
                            }
                            break;
                        case "8,12":
                            switch (hidden173.Value)
                            {
                                case "Car":
                                    space.SpaceType = spaceTypeCar;
                                    space.Name = "Car-" + carcounter;
                                    carcounter++;
                                    break;
                                case "Motorcycle":
                                    space.SpaceType = spaceTypeMoto;
                                    space.Name = "Motorcycle-" + motoCounter;
                                    motoCounter++;
                                    break;
                                case "Handicap":
                                    space.SpaceType = spaceTypeHandicap;
                                    space.Name = "Handicap-" + handicapCounter;
                                    handicapCounter++;
                                    break;
                                case "Bus":
                                    space.SpaceType = spaceTypeBus;
                                    space.Name = "Bus-" + busCounter;
                                    busCounter++;
                                    break;
                                case "Street":
                                    space.SpaceType = spaceTypeStreet;
                                    space.Name = "Street-" + streetCounter;
                                    streetCounter++;
                                    break;
                                case "Clear":
                                    space.SpaceType = spaceTypeClear;
                                    space.Name = "Clear-" + clearCounter;
                                    clearCounter++;
                                    break;
                            }
                            break;
                        case "8,13":
                            switch (hidden174.Value)
                            {
                                case "Car":
                                    space.SpaceType = spaceTypeCar;
                                    space.Name = "Car-" + carcounter;
                                    carcounter++;
                                    break;
                                case "Motorcycle":
                                    space.SpaceType = spaceTypeMoto;
                                    space.Name = "Motorcycle-" + motoCounter;
                                    motoCounter++;
                                    break;
                                case "Handicap":
                                    space.SpaceType = spaceTypeHandicap;
                                    space.Name = "Handicap-" + handicapCounter;
                                    handicapCounter++;
                                    break;
                                case "Bus":
                                    space.SpaceType = spaceTypeBus;
                                    space.Name = "Bus-" + busCounter;
                                    busCounter++;
                                    break;
                                case "Street":
                                    space.SpaceType = spaceTypeStreet;
                                    space.Name = "Street-" + streetCounter;
                                    streetCounter++;
                                    break;
                                case "Clear":
                                    space.SpaceType = spaceTypeClear;
                                    space.Name = "Clear-" + clearCounter;
                                    clearCounter++;
                                    break;
                            }
                            break;
                        case "8,14":
                            switch (hidden175.Value)
                            {
                                case "Car":
                                    space.SpaceType = spaceTypeCar;
                                    space.Name = "Car-" + carcounter;
                                    carcounter++;
                                    break;
                                case "Motorcycle":
                                    space.SpaceType = spaceTypeMoto;
                                    space.Name = "Motorcycle-" + motoCounter;
                                    motoCounter++;
                                    break;
                                case "Handicap":
                                    space.SpaceType = spaceTypeHandicap;
                                    space.Name = "Handicap-" + handicapCounter;
                                    handicapCounter++;
                                    break;
                                case "Bus":
                                    space.SpaceType = spaceTypeBus;
                                    space.Name = "Bus-" + busCounter;
                                    busCounter++;
                                    break;
                                case "Street":
                                    space.SpaceType = spaceTypeStreet;
                                    space.Name = "Street-" + streetCounter;
                                    streetCounter++;
                                    break;
                                case "Clear":
                                    space.SpaceType = spaceTypeClear;
                                    space.Name = "Clear-" + clearCounter;
                                    clearCounter++;
                                    break;
                            }
                            break;
                        case "8,15":
                            switch (hidden176.Value)
                            {
                                case "Car":
                                    space.SpaceType = spaceTypeCar;
                                    space.Name = "Car-" + carcounter;
                                    carcounter++;
                                    break;
                                case "Motorcycle":
                                    space.SpaceType = spaceTypeMoto;
                                    space.Name = "Motorcycle-" + motoCounter;
                                    motoCounter++;
                                    break;
                                case "Handicap":
                                    space.SpaceType = spaceTypeHandicap;
                                    space.Name = "Handicap-" + handicapCounter;
                                    handicapCounter++;
                                    break;
                                case "Bus":
                                    space.SpaceType = spaceTypeBus;
                                    space.Name = "Bus-" + busCounter;
                                    busCounter++;
                                    break;
                                case "Street":
                                    space.SpaceType = spaceTypeStreet;
                                    space.Name = "Street-" + streetCounter;
                                    streetCounter++;
                                    break;
                                case "Clear":
                                    space.SpaceType = spaceTypeClear;
                                    space.Name = "Clear-" + clearCounter;
                                    clearCounter++;
                                    break;
                            }
                            break;
                        case "8,16":
                            switch (hidden177.Value)
                            {
                                case "Car":
                                    space.SpaceType = spaceTypeCar;
                                    space.Name = "Car-" + carcounter;
                                    carcounter++;
                                    break;
                                case "Motorcycle":
                                    space.SpaceType = spaceTypeMoto;
                                    space.Name = "Motorcycle-" + motoCounter;
                                    motoCounter++;
                                    break;
                                case "Handicap":
                                    space.SpaceType = spaceTypeHandicap;
                                    space.Name = "Handicap-" + handicapCounter;
                                    handicapCounter++;
                                    break;
                                case "Bus":
                                    space.SpaceType = spaceTypeBus;
                                    space.Name = "Bus-" + busCounter;
                                    busCounter++;
                                    break;
                                case "Street":
                                    space.SpaceType = spaceTypeStreet;
                                    space.Name = "Street-" + streetCounter;
                                    streetCounter++;
                                    break;
                                case "Clear":
                                    space.SpaceType = spaceTypeClear;
                                    space.Name = "Clear-" + clearCounter;
                                    clearCounter++;
                                    break;
                            }
                            break;
                        case "8,17":
                            switch (hidden178.Value)
                            {
                                case "Car":
                                    space.SpaceType = spaceTypeCar;
                                    space.Name = "Car-" + carcounter;
                                    carcounter++;
                                    break;
                                case "Motorcycle":
                                    space.SpaceType = spaceTypeMoto;
                                    space.Name = "Motorcycle-" + motoCounter;
                                    motoCounter++;
                                    break;
                                case "Handicap":
                                    space.SpaceType = spaceTypeHandicap;
                                    space.Name = "Handicap-" + handicapCounter;
                                    handicapCounter++;
                                    break;
                                case "Bus":
                                    space.SpaceType = spaceTypeBus;
                                    space.Name = "Bus-" + busCounter;
                                    busCounter++;
                                    break;
                                case "Street":
                                    space.SpaceType = spaceTypeStreet;
                                    space.Name = "Street-" + streetCounter;
                                    streetCounter++;
                                    break;
                                case "Clear":
                                    space.SpaceType = spaceTypeClear;
                                    space.Name = "Clear-" + clearCounter;
                                    clearCounter++;
                                    break;
                            }
                            break;
                        case "8,18":
                            switch (hidden179.Value)
                            {
                                case "Car":
                                    space.SpaceType = spaceTypeCar;
                                    space.Name = "Car-" + carcounter;
                                    carcounter++;
                                    break;
                                case "Motorcycle":
                                    space.SpaceType = spaceTypeMoto;
                                    space.Name = "Motorcycle-" + motoCounter;
                                    motoCounter++;
                                    break;
                                case "Handicap":
                                    space.SpaceType = spaceTypeHandicap;
                                    space.Name = "Handicap-" + handicapCounter;
                                    handicapCounter++;
                                    break;
                                case "Bus":
                                    space.SpaceType = spaceTypeBus;
                                    space.Name = "Bus-" + busCounter;
                                    busCounter++;
                                    break;
                                case "Street":
                                    space.SpaceType = spaceTypeStreet;
                                    space.Name = "Street-" + streetCounter;
                                    streetCounter++;
                                    break;
                                case "Clear":
                                    space.SpaceType = spaceTypeClear;
                                    space.Name = "Clear-" + clearCounter;
                                    clearCounter++;
                                    break;
                            }
                            break;
                        case "8,19":
                            switch (hidden180.Value)
                            {
                                case "Car":
                                    space.SpaceType = spaceTypeCar;
                                    space.Name = "Car-" + carcounter;
                                    carcounter++;
                                    break;
                                case "Motorcycle":
                                    space.SpaceType = spaceTypeMoto;
                                    space.Name = "Motorcycle-" + motoCounter;
                                    motoCounter++;
                                    break;
                                case "Handicap":
                                    space.SpaceType = spaceTypeHandicap;
                                    space.Name = "Handicap-" + handicapCounter;
                                    handicapCounter++;
                                    break;
                                case "Bus":
                                    space.SpaceType = spaceTypeBus;
                                    space.Name = "Bus-" + busCounter;
                                    busCounter++;
                                    break;
                                case "Street":
                                    space.SpaceType = spaceTypeStreet;
                                    space.Name = "Street-" + streetCounter;
                                    streetCounter++;
                                    break;
                                case "Clear":
                                    space.SpaceType = spaceTypeClear;
                                    space.Name = "Clear-" + clearCounter;
                                    clearCounter++;
                                    break;
                            }
                            break;

                        case "9,0":
                            switch (hidden181.Value)
                            {
                                case "Car":
                                    space.SpaceType = spaceTypeCar;
                                    space.Name = "Car-" + carcounter;
                                    carcounter++;
                                    break;
                                case "Motorcycle":
                                    space.SpaceType = spaceTypeMoto;
                                    space.Name = "Motorcycle-" + motoCounter;
                                    motoCounter++;
                                    break;
                                case "Handicap":
                                    space.SpaceType = spaceTypeHandicap;
                                    space.Name = "Handicap-" + handicapCounter;
                                    handicapCounter++;
                                    break;
                                case "Bus":
                                    space.SpaceType = spaceTypeBus;
                                    space.Name = "Bus-" + busCounter;
                                    busCounter++;
                                    break;
                                case "Street":
                                    space.SpaceType = spaceTypeStreet;
                                    space.Name = "Street-" + streetCounter;
                                    streetCounter++;
                                    break;
                                case "Clear":
                                    space.SpaceType = spaceTypeClear;
                                    space.Name = "Clear-" + clearCounter;
                                    clearCounter++;
                                    break;
                            }
                            break;
                        case "9,1":
                            switch (hidden182.Value)
                            {
                                case "Car":
                                    space.SpaceType = spaceTypeCar;
                                    space.Name = "Car-" + carcounter;
                                    carcounter++;
                                    break;
                                case "Motorcycle":
                                    space.SpaceType = spaceTypeMoto;
                                    space.Name = "Motorcycle-" + motoCounter;
                                    motoCounter++;
                                    break;
                                case "Handicap":
                                    space.SpaceType = spaceTypeHandicap;
                                    space.Name = "Handicap-" + handicapCounter;
                                    handicapCounter++;
                                    break;
                                case "Bus":
                                    space.SpaceType = spaceTypeBus;
                                    space.Name = "Bus-" + busCounter;
                                    busCounter++;
                                    break;
                                case "Street":
                                    space.SpaceType = spaceTypeStreet;
                                    space.Name = "Street-" + streetCounter;
                                    streetCounter++;
                                    break;
                                case "Clear":
                                    space.SpaceType = spaceTypeClear;
                                    space.Name = "Clear-" + clearCounter;
                                    clearCounter++;
                                    break;
                            }
                            break;
                        case "9,2":
                            switch (hidden183.Value)
                            {
                                case "Car":
                                    space.SpaceType = spaceTypeCar;
                                    space.Name = "Car-" + carcounter;
                                    carcounter++;
                                    break;
                                case "Motorcycle":
                                    space.SpaceType = spaceTypeMoto;
                                    space.Name = "Motorcycle-" + motoCounter;
                                    motoCounter++;
                                    break;
                                case "Handicap":
                                    space.SpaceType = spaceTypeHandicap;
                                    space.Name = "Handicap-" + handicapCounter;
                                    handicapCounter++;
                                    break;
                                case "Bus":
                                    space.SpaceType = spaceTypeBus;
                                    space.Name = "Bus-" + busCounter;
                                    busCounter++;
                                    break;
                                case "Street":
                                    space.SpaceType = spaceTypeStreet;
                                    space.Name = "Street-" + streetCounter;
                                    streetCounter++;
                                    break;
                                case "Clear":
                                    space.SpaceType = spaceTypeClear;
                                    space.Name = "Clear-" + clearCounter;
                                    clearCounter++;
                                    break;
                            }
                            break;
                        case "9,3":
                            switch (hidden184.Value)
                            {
                                case "Car":
                                    space.SpaceType = spaceTypeCar;
                                    space.Name = "Car-" + carcounter;
                                    carcounter++;
                                    break;
                                case "Motorcycle":
                                    space.SpaceType = spaceTypeMoto;
                                    space.Name = "Motorcycle-" + motoCounter;
                                    motoCounter++;
                                    break;
                                case "Handicap":
                                    space.SpaceType = spaceTypeHandicap;
                                    space.Name = "Handicap-" + handicapCounter;
                                    handicapCounter++;
                                    break;
                                case "Bus":
                                    space.SpaceType = spaceTypeBus;
                                    space.Name = "Bus-" + busCounter;
                                    busCounter++;
                                    break;
                                case "Street":
                                    space.SpaceType = spaceTypeStreet;
                                    space.Name = "Street-" + streetCounter;
                                    streetCounter++;
                                    break;
                                case "Clear":
                                    space.SpaceType = spaceTypeClear;
                                    space.Name = "Clear-" + clearCounter;
                                    clearCounter++;
                                    break;
                            }
                            break;
                        case "9,4":
                            switch (hidden185.Value)
                            {
                                case "Car":
                                    space.SpaceType = spaceTypeCar;
                                    space.Name = "Car-" + carcounter;
                                    carcounter++;
                                    break;
                                case "Motorcycle":
                                    space.SpaceType = spaceTypeMoto;
                                    space.Name = "Motorcycle-" + motoCounter;
                                    motoCounter++;
                                    break;
                                case "Handicap":
                                    space.SpaceType = spaceTypeHandicap;
                                    space.Name = "Handicap-" + handicapCounter;
                                    handicapCounter++;
                                    break;
                                case "Bus":
                                    space.SpaceType = spaceTypeBus;
                                    space.Name = "Bus-" + busCounter;
                                    busCounter++;
                                    break;
                                case "Street":
                                    space.SpaceType = spaceTypeStreet;
                                    space.Name = "Street-" + streetCounter;
                                    streetCounter++;
                                    break;
                                case "Clear":
                                    space.SpaceType = spaceTypeClear;
                                    space.Name = "Clear-" + clearCounter;
                                    clearCounter++;
                                    break;
                            }
                            break;
                        case "9,5":
                            switch (hidden186.Value)
                            {
                                case "Car":
                                    space.SpaceType = spaceTypeCar;
                                    space.Name = "Car-" + carcounter;
                                    carcounter++;
                                    break;
                                case "Motorcycle":
                                    space.SpaceType = spaceTypeMoto;
                                    space.Name = "Motorcycle-" + motoCounter;
                                    motoCounter++;
                                    break;
                                case "Handicap":
                                    space.SpaceType = spaceTypeHandicap;
                                    space.Name = "Handicap-" + handicapCounter;
                                    handicapCounter++;
                                    break;
                                case "Bus":
                                    space.SpaceType = spaceTypeBus;
                                    space.Name = "Bus-" + busCounter;
                                    busCounter++;
                                    break;
                                case "Street":
                                    space.SpaceType = spaceTypeStreet;
                                    space.Name = "Street-" + streetCounter;
                                    streetCounter++;
                                    break;
                                case "Clear":
                                    space.SpaceType = spaceTypeClear;
                                    space.Name = "Clear-" + clearCounter;
                                    clearCounter++;
                                    break;
                            }
                            break;
                        case "9,6":
                            switch (hidden187.Value)
                            {
                                case "Car":
                                    space.SpaceType = spaceTypeCar;
                                    space.Name = "Car-" + carcounter;
                                    carcounter++;
                                    break;
                                case "Motorcycle":
                                    space.SpaceType = spaceTypeMoto;
                                    space.Name = "Motorcycle-" + motoCounter;
                                    motoCounter++;
                                    break;
                                case "Handicap":
                                    space.SpaceType = spaceTypeHandicap;
                                    space.Name = "Handicap-" + handicapCounter;
                                    handicapCounter++;
                                    break;
                                case "Bus":
                                    space.SpaceType = spaceTypeBus;
                                    space.Name = "Bus-" + busCounter;
                                    busCounter++;
                                    break;
                                case "Street":
                                    space.SpaceType = spaceTypeStreet;
                                    space.Name = "Street-" + streetCounter;
                                    streetCounter++;
                                    break;
                                case "Clear":
                                    space.SpaceType = spaceTypeClear;
                                    space.Name = "Clear-" + clearCounter;
                                    clearCounter++;
                                    break;
                            }
                            break;
                        case "9,7":
                            switch (hidden188.Value)
                            {
                                case "Car":
                                    space.SpaceType = spaceTypeCar;
                                    space.Name = "Car-" + carcounter;
                                    carcounter++;
                                    break;
                                case "Motorcycle":
                                    space.SpaceType = spaceTypeMoto;
                                    space.Name = "Motorcycle-" + motoCounter;
                                    motoCounter++;
                                    break;
                                case "Handicap":
                                    space.SpaceType = spaceTypeHandicap;
                                    space.Name = "Handicap-" + handicapCounter;
                                    handicapCounter++;
                                    break;
                                case "Bus":
                                    space.SpaceType = spaceTypeBus;
                                    space.Name = "Bus-" + busCounter;
                                    busCounter++;
                                    break;
                                case "Street":
                                    space.SpaceType = spaceTypeStreet;
                                    space.Name = "Street-" + streetCounter;
                                    streetCounter++;
                                    break;
                                case "Clear":
                                    space.SpaceType = spaceTypeClear;
                                    space.Name = "Clear-" + clearCounter;
                                    clearCounter++;
                                    break;
                            }
                            break;
                        case "9,8":
                            switch (hidden189.Value)
                            {
                                case "Car":
                                    space.SpaceType = spaceTypeCar;
                                    space.Name = "Car-" + carcounter;
                                    carcounter++;
                                    break;
                                case "Motorcycle":
                                    space.SpaceType = spaceTypeMoto;
                                    space.Name = "Motorcycle-" + motoCounter;
                                    motoCounter++;
                                    break;
                                case "Handicap":
                                    space.SpaceType = spaceTypeHandicap;
                                    space.Name = "Handicap-" + handicapCounter;
                                    handicapCounter++;
                                    break;
                                case "Bus":
                                    space.SpaceType = spaceTypeBus;
                                    space.Name = "Bus-" + busCounter;
                                    busCounter++;
                                    break;
                                case "Street":
                                    space.SpaceType = spaceTypeStreet;
                                    space.Name = "Street-" + streetCounter;
                                    streetCounter++;
                                    break;
                                case "Clear":
                                    space.SpaceType = spaceTypeClear;
                                    space.Name = "Clear-" + clearCounter;
                                    clearCounter++;
                                    break;
                            }
                            break;
                        case "9,9":
                            switch (hidden190.Value)
                            {
                                case "Car":
                                    space.SpaceType = spaceTypeCar;
                                    space.Name = "Car-" + carcounter;
                                    carcounter++;
                                    break;
                                case "Motorcycle":
                                    space.SpaceType = spaceTypeMoto;
                                    space.Name = "Motorcycle-" + motoCounter;
                                    motoCounter++;
                                    break;
                                case "Handicap":
                                    space.SpaceType = spaceTypeHandicap;
                                    space.Name = "Handicap-" + handicapCounter;
                                    handicapCounter++;
                                    break;
                                case "Bus":
                                    space.SpaceType = spaceTypeBus;
                                    space.Name = "Bus-" + busCounter;
                                    busCounter++;
                                    break;
                                case "Street":
                                    space.SpaceType = spaceTypeStreet;
                                    space.Name = "Street-" + streetCounter;
                                    streetCounter++;
                                    break;
                                case "Clear":
                                    space.SpaceType = spaceTypeClear;
                                    space.Name = "Clear-" + clearCounter;
                                    clearCounter++;
                                    break;
                            }
                            break;
                        case "9,10":
                            switch (hidden191.Value)
                            {
                                case "Car":
                                    space.SpaceType = spaceTypeCar;
                                    space.Name = "Car-" + carcounter;
                                    carcounter++;
                                    break;
                                case "Motorcycle":
                                    space.SpaceType = spaceTypeMoto;
                                    space.Name = "Motorcycle-" + motoCounter;
                                    motoCounter++;
                                    break;
                                case "Handicap":
                                    space.SpaceType = spaceTypeHandicap;
                                    space.Name = "Handicap-" + handicapCounter;
                                    handicapCounter++;
                                    break;
                                case "Bus":
                                    space.SpaceType = spaceTypeBus;
                                    space.Name = "Bus-" + busCounter;
                                    busCounter++;
                                    break;
                                case "Street":
                                    space.SpaceType = spaceTypeStreet;
                                    space.Name = "Street-" + streetCounter;
                                    streetCounter++;
                                    break;
                                case "Clear":
                                    space.SpaceType = spaceTypeClear;
                                    space.Name = "Clear-" + clearCounter;
                                    clearCounter++;
                                    break;
                            }
                            break;
                        case "9,11":
                            switch (hidden192.Value)
                            {
                                case "Car":
                                    space.SpaceType = spaceTypeCar;
                                    space.Name = "Car-" + carcounter;
                                    carcounter++;
                                    break;
                                case "Motorcycle":
                                    space.SpaceType = spaceTypeMoto;
                                    space.Name = "Motorcycle-" + motoCounter;
                                    motoCounter++;
                                    break;
                                case "Handicap":
                                    space.SpaceType = spaceTypeHandicap;
                                    space.Name = "Handicap-" + handicapCounter;
                                    handicapCounter++;
                                    break;
                                case "Bus":
                                    space.SpaceType = spaceTypeBus;
                                    space.Name = "Bus-" + busCounter;
                                    busCounter++;
                                    break;
                                case "Street":
                                    space.SpaceType = spaceTypeStreet;
                                    space.Name = "Street-" + streetCounter;
                                    streetCounter++;
                                    break;
                                case "Clear":
                                    space.SpaceType = spaceTypeClear;
                                    space.Name = "Clear-" + clearCounter;
                                    clearCounter++;
                                    break;
                            }
                            break;
                        case "9,12":
                            switch (hidden193.Value)
                            {
                                case "Car":
                                    space.SpaceType = spaceTypeCar;
                                    space.Name = "Car-" + carcounter;
                                    carcounter++;
                                    break;
                                case "Motorcycle":
                                    space.SpaceType = spaceTypeMoto;
                                    space.Name = "Motorcycle-" + motoCounter;
                                    motoCounter++;
                                    break;
                                case "Handicap":
                                    space.SpaceType = spaceTypeHandicap;
                                    space.Name = "Handicap-" + handicapCounter;
                                    handicapCounter++;
                                    break;
                                case "Bus":
                                    space.SpaceType = spaceTypeBus;
                                    space.Name = "Bus-" + busCounter;
                                    busCounter++;
                                    break;
                                case "Street":
                                    space.SpaceType = spaceTypeStreet;
                                    space.Name = "Street-" + streetCounter;
                                    streetCounter++;
                                    break;
                                case "Clear":
                                    space.SpaceType = spaceTypeClear;
                                    space.Name = "Clear-" + clearCounter;
                                    clearCounter++;
                                    break;
                            }
                            break;
                        case "9,13":
                            switch (hidden194.Value)
                            {
                                case "Car":
                                    space.SpaceType = spaceTypeCar;
                                    space.Name = "Car-" + carcounter;
                                    carcounter++;
                                    break;
                                case "Motorcycle":
                                    space.SpaceType = spaceTypeMoto;
                                    space.Name = "Motorcycle-" + motoCounter;
                                    motoCounter++;
                                    break;
                                case "Handicap":
                                    space.SpaceType = spaceTypeHandicap;
                                    space.Name = "Handicap-" + handicapCounter;
                                    handicapCounter++;
                                    break;
                                case "Bus":
                                    space.SpaceType = spaceTypeBus;
                                    space.Name = "Bus-" + busCounter;
                                    busCounter++;
                                    break;
                                case "Street":
                                    space.SpaceType = spaceTypeStreet;
                                    space.Name = "Street-" + streetCounter;
                                    streetCounter++;
                                    break;
                                case "Clear":
                                    space.SpaceType = spaceTypeClear;
                                    space.Name = "Clear-" + clearCounter;
                                    clearCounter++;
                                    break;
                            }
                            break;
                        case "9,14":
                            switch (hidden195.Value)
                            {
                                case "Car":
                                    space.SpaceType = spaceTypeCar;
                                    space.Name = "Car-" + carcounter;
                                    carcounter++;
                                    break;
                                case "Motorcycle":
                                    space.SpaceType = spaceTypeMoto;
                                    space.Name = "Motorcycle-" + motoCounter;
                                    motoCounter++;
                                    break;
                                case "Handicap":
                                    space.SpaceType = spaceTypeHandicap;
                                    space.Name = "Handicap-" + handicapCounter;
                                    handicapCounter++;
                                    break;
                                case "Bus":
                                    space.SpaceType = spaceTypeBus;
                                    space.Name = "Bus-" + busCounter;
                                    busCounter++;
                                    break;
                                case "Street":
                                    space.SpaceType = spaceTypeStreet;
                                    space.Name = "Street-" + streetCounter;
                                    streetCounter++;
                                    break;
                                case "Clear":
                                    space.SpaceType = spaceTypeClear;
                                    space.Name = "Clear-" + clearCounter;
                                    clearCounter++;
                                    break;
                            }
                            break;
                        case "9,15":
                            switch (hidden196.Value)
                            {
                                case "Car":
                                    space.SpaceType = spaceTypeCar;
                                    space.Name = "Car-" + carcounter;
                                    carcounter++;
                                    break;
                                case "Motorcycle":
                                    space.SpaceType = spaceTypeMoto;
                                    space.Name = "Motorcycle-" + motoCounter;
                                    motoCounter++;
                                    break;
                                case "Handicap":
                                    space.SpaceType = spaceTypeHandicap;
                                    space.Name = "Handicap-" + handicapCounter;
                                    handicapCounter++;
                                    break;
                                case "Bus":
                                    space.SpaceType = spaceTypeBus;
                                    space.Name = "Bus-" + busCounter;
                                    busCounter++;
                                    break;
                                case "Street":
                                    space.SpaceType = spaceTypeStreet;
                                    space.Name = "Street-" + streetCounter;
                                    streetCounter++;
                                    break;
                                case "Clear":
                                    space.SpaceType = spaceTypeClear;
                                    space.Name = "Clear-" + clearCounter;
                                    clearCounter++;
                                    break;
                            }
                            break;
                        case "9,16":
                            switch (hidden197.Value)
                            {
                                case "Car":
                                    space.SpaceType = spaceTypeCar;
                                    space.Name = "Car-" + carcounter;
                                    carcounter++;
                                    break;
                                case "Motorcycle":
                                    space.SpaceType = spaceTypeMoto;
                                    space.Name = "Motorcycle-" + motoCounter;
                                    motoCounter++;
                                    break;
                                case "Handicap":
                                    space.SpaceType = spaceTypeHandicap;
                                    space.Name = "Handicap-" + handicapCounter;
                                    handicapCounter++;
                                    break;
                                case "Bus":
                                    space.SpaceType = spaceTypeBus;
                                    space.Name = "Bus-" + busCounter;
                                    busCounter++;
                                    break;
                                case "Street":
                                    space.SpaceType = spaceTypeStreet;
                                    space.Name = "Street-" + streetCounter;
                                    streetCounter++;
                                    break;
                                case "Clear":
                                    space.SpaceType = spaceTypeClear;
                                    space.Name = "Clear-" + clearCounter;
                                    clearCounter++;
                                    break;
                            }
                            break;
                        case "9,17":
                            switch (hidden198.Value)
                            {
                                case "Car":
                                    space.SpaceType = spaceTypeCar;
                                    space.Name = "Car-" + carcounter;
                                    carcounter++;
                                    break;
                                case "Motorcycle":
                                    space.SpaceType = spaceTypeMoto;
                                    space.Name = "Motorcycle-" + motoCounter;
                                    motoCounter++;
                                    break;
                                case "Handicap":
                                    space.SpaceType = spaceTypeHandicap;
                                    space.Name = "Handicap-" + handicapCounter;
                                    handicapCounter++;
                                    break;
                                case "Bus":
                                    space.SpaceType = spaceTypeBus;
                                    space.Name = "Bus-" + busCounter;
                                    busCounter++;
                                    break;
                                case "Street":
                                    space.SpaceType = spaceTypeStreet;
                                    space.Name = "Street-" + streetCounter;
                                    streetCounter++;
                                    break;
                                case "Clear":
                                    space.SpaceType = spaceTypeClear;
                                    space.Name = "Clear-" + clearCounter;
                                    clearCounter++;
                                    break;
                            }
                            break;
                        case "9,18":
                            switch (hidden199.Value)
                            {
                                case "Car":
                                    space.SpaceType = spaceTypeCar;
                                    space.Name = "Car-" + carcounter;
                                    carcounter++;
                                    break;
                                case "Motorcycle":
                                    space.SpaceType = spaceTypeMoto;
                                    space.Name = "Motorcycle-" + motoCounter;
                                    motoCounter++;
                                    break;
                                case "Handicap":
                                    space.SpaceType = spaceTypeHandicap;
                                    space.Name = "Handicap-" + handicapCounter;
                                    handicapCounter++;
                                    break;
                                case "Bus":
                                    space.SpaceType = spaceTypeBus;
                                    space.Name = "Bus-" + busCounter;
                                    busCounter++;
                                    break;
                                case "Street":
                                    space.SpaceType = spaceTypeStreet;
                                    space.Name = "Street-" + streetCounter;
                                    streetCounter++;
                                    break;
                                case "Clear":
                                    space.SpaceType = spaceTypeClear;
                                    space.Name = "Clear-" + clearCounter;
                                    clearCounter++;
                                    break;
                            }
                            break;
                        case "9,19":
                            switch (hidden200.Value)
                            {
                                case "Car":
                                    space.SpaceType = spaceTypeCar;
                                    space.Name = "Car-" + carcounter;
                                    carcounter++;
                                    break;
                                case "Motorcycle":
                                    space.SpaceType = spaceTypeMoto;
                                    space.Name = "Motorcycle-" + motoCounter;
                                    motoCounter++;
                                    break;
                                case "Handicap":
                                    space.SpaceType = spaceTypeHandicap;
                                    space.Name = "Handicap-" + handicapCounter;
                                    handicapCounter++;
                                    break;
                                case "Bus":
                                    space.SpaceType = spaceTypeBus;
                                    space.Name = "Bus-" + busCounter;
                                    busCounter++;
                                    break;
                                case "Street":
                                    space.SpaceType = spaceTypeStreet;
                                    space.Name = "Street-" + streetCounter;
                                    streetCounter++;
                                    break;
                                case "Clear":
                                    space.SpaceType = spaceTypeClear;
                                    space.Name = "Clear-" + clearCounter;
                                    clearCounter++;
                                    break;
                            }
                            break;

                        case "10,0":
                            switch (hidden201.Value)
                            {
                                case "Car":
                                    space.SpaceType = spaceTypeCar;
                                    space.Name = "Car-" + carcounter;
                                    carcounter++;
                                    break;
                                case "Motorcycle":
                                    space.SpaceType = spaceTypeMoto;
                                    space.Name = "Motorcycle-" + motoCounter;
                                    motoCounter++;
                                    break;
                                case "Handicap":
                                    space.SpaceType = spaceTypeHandicap;
                                    space.Name = "Handicap-" + handicapCounter;
                                    handicapCounter++;
                                    break;
                                case "Bus":
                                    space.SpaceType = spaceTypeBus;
                                    space.Name = "Bus-" + busCounter;
                                    busCounter++;
                                    break;
                                case "Street":
                                    space.SpaceType = spaceTypeStreet;
                                    space.Name = "Street-" + streetCounter;
                                    streetCounter++;
                                    break;
                                case "Clear":
                                    space.SpaceType = spaceTypeClear;
                                    space.Name = "Clear-" + clearCounter;
                                    clearCounter++;
                                    break;
                            }
                            break;
                        case "10,1":
                            switch (hidden202.Value)
                            {
                                case "Car":
                                    space.SpaceType = spaceTypeCar;
                                    space.Name = "Car-" + carcounter;
                                    carcounter++;
                                    break;
                                case "Motorcycle":
                                    space.SpaceType = spaceTypeMoto;
                                    space.Name = "Motorcycle-" + motoCounter;
                                    motoCounter++;
                                    break;
                                case "Handicap":
                                    space.SpaceType = spaceTypeHandicap;
                                    space.Name = "Handicap-" + handicapCounter;
                                    handicapCounter++;
                                    break;
                                case "Bus":
                                    space.SpaceType = spaceTypeBus;
                                    space.Name = "Bus-" + busCounter;
                                    busCounter++;
                                    break;
                                case "Street":
                                    space.SpaceType = spaceTypeStreet;
                                    space.Name = "Street-" + streetCounter;
                                    streetCounter++;
                                    break;
                                case "Clear":
                                    space.SpaceType = spaceTypeClear;
                                    space.Name = "Clear-" + clearCounter;
                                    clearCounter++;
                                    break;
                            }
                            break;
                        case "10,2":
                            switch (hidden203.Value)
                            {
                                case "Car":
                                    space.SpaceType = spaceTypeCar;
                                    space.Name = "Car-" + carcounter;
                                    carcounter++;
                                    break;
                                case "Motorcycle":
                                    space.SpaceType = spaceTypeMoto;
                                    space.Name = "Motorcycle-" + motoCounter;
                                    motoCounter++;
                                    break;
                                case "Handicap":
                                    space.SpaceType = spaceTypeHandicap;
                                    space.Name = "Handicap-" + handicapCounter;
                                    handicapCounter++;
                                    break;
                                case "Bus":
                                    space.SpaceType = spaceTypeBus;
                                    space.Name = "Bus-" + busCounter;
                                    busCounter++;
                                    break;
                                case "Street":
                                    space.SpaceType = spaceTypeStreet;
                                    space.Name = "Street-" + streetCounter;
                                    streetCounter++;
                                    break;
                                case "Clear":
                                    space.SpaceType = spaceTypeClear;
                                    space.Name = "Clear-" + clearCounter;
                                    clearCounter++;
                                    break;
                            }
                            break;
                        case "10,3":
                            switch (hidden204.Value)
                            {
                                case "Car":
                                    space.SpaceType = spaceTypeCar;
                                    space.Name = "Car-" + carcounter;
                                    carcounter++;
                                    break;
                                case "Motorcycle":
                                    space.SpaceType = spaceTypeMoto;
                                    space.Name = "Motorcycle-" + motoCounter;
                                    motoCounter++;
                                    break;
                                case "Handicap":
                                    space.SpaceType = spaceTypeHandicap;
                                    space.Name = "Handicap-" + handicapCounter;
                                    handicapCounter++;
                                    break;
                                case "Bus":
                                    space.SpaceType = spaceTypeBus;
                                    space.Name = "Bus-" + busCounter;
                                    busCounter++;
                                    break;
                                case "Street":
                                    space.SpaceType = spaceTypeStreet;
                                    space.Name = "Street-" + streetCounter;
                                    streetCounter++;
                                    break;
                                case "Clear":
                                    space.SpaceType = spaceTypeClear;
                                    space.Name = "Clear-" + clearCounter;
                                    clearCounter++;
                                    break;
                            }
                            break;
                        case "10,4":
                            switch (hidden205.Value)
                            {
                                case "Car":
                                    space.SpaceType = spaceTypeCar;
                                    space.Name = "Car-" + carcounter;
                                    carcounter++;
                                    break;
                                case "Motorcycle":
                                    space.SpaceType = spaceTypeMoto;
                                    space.Name = "Motorcycle-" + motoCounter;
                                    motoCounter++;
                                    break;
                                case "Handicap":
                                    space.SpaceType = spaceTypeHandicap;
                                    space.Name = "Handicap-" + handicapCounter;
                                    handicapCounter++;
                                    break;
                                case "Bus":
                                    space.SpaceType = spaceTypeBus;
                                    space.Name = "Bus-" + busCounter;
                                    busCounter++;
                                    break;
                                case "Street":
                                    space.SpaceType = spaceTypeStreet;
                                    space.Name = "Street-" + streetCounter;
                                    streetCounter++;
                                    break;
                                case "Clear":
                                    space.SpaceType = spaceTypeClear;
                                    space.Name = "Clear-" + clearCounter;
                                    clearCounter++;
                                    break;
                            }
                            break;
                        case "10,5":
                            switch (hidden206.Value)
                            {
                                case "Car":
                                    space.SpaceType = spaceTypeCar;
                                    space.Name = "Car-" + carcounter;
                                    carcounter++;
                                    break;
                                case "Motorcycle":
                                    space.SpaceType = spaceTypeMoto;
                                    space.Name = "Motorcycle-" + motoCounter;
                                    motoCounter++;
                                    break;
                                case "Handicap":
                                    space.SpaceType = spaceTypeHandicap;
                                    space.Name = "Handicap-" + handicapCounter;
                                    handicapCounter++;
                                    break;
                                case "Bus":
                                    space.SpaceType = spaceTypeBus;
                                    space.Name = "Bus-" + busCounter;
                                    busCounter++;
                                    break;
                                case "Street":
                                    space.SpaceType = spaceTypeStreet;
                                    space.Name = "Street-" + streetCounter;
                                    streetCounter++;
                                    break;
                                case "Clear":
                                    space.SpaceType = spaceTypeClear;
                                    space.Name = "Clear-" + clearCounter;
                                    clearCounter++;
                                    break;
                            }
                            break;
                        case "10,6":
                            switch (hidden207.Value)
                            {
                                case "Car":
                                    space.SpaceType = spaceTypeCar;
                                    space.Name = "Car-" + carcounter;
                                    carcounter++;
                                    break;
                                case "Motorcycle":
                                    space.SpaceType = spaceTypeMoto;
                                    space.Name = "Motorcycle-" + motoCounter;
                                    motoCounter++;
                                    break;
                                case "Handicap":
                                    space.SpaceType = spaceTypeHandicap;
                                    space.Name = "Handicap-" + handicapCounter;
                                    handicapCounter++;
                                    break;
                                case "Bus":
                                    space.SpaceType = spaceTypeBus;
                                    space.Name = "Bus-" + busCounter;
                                    busCounter++;
                                    break;
                                case "Street":
                                    space.SpaceType = spaceTypeStreet;
                                    space.Name = "Street-" + streetCounter;
                                    streetCounter++;
                                    break;
                                case "Clear":
                                    space.SpaceType = spaceTypeClear;
                                    space.Name = "Clear-" + clearCounter;
                                    clearCounter++;
                                    break;
                            }
                            break;
                        case "10,7":
                            switch (hidden208.Value)
                            {
                                case "Car":
                                    space.SpaceType = spaceTypeCar;
                                    space.Name = "Car-" + carcounter;
                                    carcounter++;
                                    break;
                                case "Motorcycle":
                                    space.SpaceType = spaceTypeMoto;
                                    space.Name = "Motorcycle-" + motoCounter;
                                    motoCounter++;
                                    break;
                                case "Handicap":
                                    space.SpaceType = spaceTypeHandicap;
                                    space.Name = "Handicap-" + handicapCounter;
                                    handicapCounter++;
                                    break;
                                case "Bus":
                                    space.SpaceType = spaceTypeBus;
                                    space.Name = "Bus-" + busCounter;
                                    busCounter++;
                                    break;
                                case "Street":
                                    space.SpaceType = spaceTypeStreet;
                                    space.Name = "Street-" + streetCounter;
                                    streetCounter++;
                                    break;
                                case "Clear":
                                    space.SpaceType = spaceTypeClear;
                                    space.Name = "Clear-" + clearCounter;
                                    clearCounter++;
                                    break;
                            }
                            break;
                        case "10,8":
                            switch (hidden209.Value)
                            {
                                case "Car":
                                    space.SpaceType = spaceTypeCar;
                                    space.Name = "Car-" + carcounter;
                                    carcounter++;
                                    break;
                                case "Motorcycle":
                                    space.SpaceType = spaceTypeMoto;
                                    space.Name = "Motorcycle-" + motoCounter;
                                    motoCounter++;
                                    break;
                                case "Handicap":
                                    space.SpaceType = spaceTypeHandicap;
                                    space.Name = "Handicap-" + handicapCounter;
                                    handicapCounter++;
                                    break;
                                case "Bus":
                                    space.SpaceType = spaceTypeBus;
                                    space.Name = "Bus-" + busCounter;
                                    busCounter++;
                                    break;
                                case "Street":
                                    space.SpaceType = spaceTypeStreet;
                                    space.Name = "Street-" + streetCounter;
                                    streetCounter++;
                                    break;
                                case "Clear":
                                    space.SpaceType = spaceTypeClear;
                                    space.Name = "Clear-" + clearCounter;
                                    clearCounter++;
                                    break;
                            }
                            break;
                        case "10,9":
                            switch (hidden210.Value)
                            {
                                case "Car":
                                    space.SpaceType = spaceTypeCar;
                                    space.Name = "Car-" + carcounter;
                                    carcounter++;
                                    break;
                                case "Motorcycle":
                                    space.SpaceType = spaceTypeMoto;
                                    space.Name = "Motorcycle-" + motoCounter;
                                    motoCounter++;
                                    break;
                                case "Handicap":
                                    space.SpaceType = spaceTypeHandicap;
                                    space.Name = "Handicap-" + handicapCounter;
                                    handicapCounter++;
                                    break;
                                case "Bus":
                                    space.SpaceType = spaceTypeBus;
                                    space.Name = "Bus-" + busCounter;
                                    busCounter++;
                                    break;
                                case "Street":
                                    space.SpaceType = spaceTypeStreet;
                                    space.Name = "Street-" + streetCounter;
                                    streetCounter++;
                                    break;
                                case "Clear":
                                    space.SpaceType = spaceTypeClear;
                                    space.Name = "Clear-" + clearCounter;
                                    clearCounter++;
                                    break;
                            }
                            break;
                        case "10,10":
                            switch (hidden211.Value)
                            {
                                case "Car":
                                    space.SpaceType = spaceTypeCar;
                                    space.Name = "Car-" + carcounter;
                                    carcounter++;
                                    break;
                                case "Motorcycle":
                                    space.SpaceType = spaceTypeMoto;
                                    space.Name = "Motorcycle-" + motoCounter;
                                    motoCounter++;
                                    break;
                                case "Handicap":
                                    space.SpaceType = spaceTypeHandicap;
                                    space.Name = "Handicap-" + handicapCounter;
                                    handicapCounter++;
                                    break;
                                case "Bus":
                                    space.SpaceType = spaceTypeBus;
                                    space.Name = "Bus-" + busCounter;
                                    busCounter++;
                                    break;
                                case "Street":
                                    space.SpaceType = spaceTypeStreet;
                                    space.Name = "Street-" + streetCounter;
                                    streetCounter++;
                                    break;
                                case "Clear":
                                    space.SpaceType = spaceTypeClear;
                                    space.Name = "Clear-" + clearCounter;
                                    clearCounter++;
                                    break;
                            }
                            break;
                        case "10,11":
                            switch (hidden212.Value)
                            {
                                case "Car":
                                    space.SpaceType = spaceTypeCar;
                                    space.Name = "Car-" + carcounter;
                                    carcounter++;
                                    break;
                                case "Motorcycle":
                                    space.SpaceType = spaceTypeMoto;
                                    space.Name = "Motorcycle-" + motoCounter;
                                    motoCounter++;
                                    break;
                                case "Handicap":
                                    space.SpaceType = spaceTypeHandicap;
                                    space.Name = "Handicap-" + handicapCounter;
                                    handicapCounter++;
                                    break;
                                case "Bus":
                                    space.SpaceType = spaceTypeBus;
                                    space.Name = "Bus-" + busCounter;
                                    busCounter++;
                                    break;
                                case "Street":
                                    space.SpaceType = spaceTypeStreet;
                                    space.Name = "Street-" + streetCounter;
                                    streetCounter++;
                                    break;
                                case "Clear":
                                    space.SpaceType = spaceTypeClear;
                                    space.Name = "Clear-" + clearCounter;
                                    clearCounter++;
                                    break;
                            }
                            break;
                        case "10,12":
                            switch (hidden213.Value)
                            {
                                case "Car":
                                    space.SpaceType = spaceTypeCar;
                                    space.Name = "Car-" + carcounter;
                                    carcounter++;
                                    break;
                                case "Motorcycle":
                                    space.SpaceType = spaceTypeMoto;
                                    space.Name = "Motorcycle-" + motoCounter;
                                    motoCounter++;
                                    break;
                                case "Handicap":
                                    space.SpaceType = spaceTypeHandicap;
                                    space.Name = "Handicap-" + handicapCounter;
                                    handicapCounter++;
                                    break;
                                case "Bus":
                                    space.SpaceType = spaceTypeBus;
                                    space.Name = "Bus-" + busCounter;
                                    busCounter++;
                                    break;
                                case "Street":
                                    space.SpaceType = spaceTypeStreet;
                                    space.Name = "Street-" + streetCounter;
                                    streetCounter++;
                                    break;
                                case "Clear":
                                    space.SpaceType = spaceTypeClear;
                                    space.Name = "Clear-" + clearCounter;
                                    clearCounter++;
                                    break;
                            }
                            break;
                        case "10,13":
                            switch (hidden214.Value)
                            {
                                case "Car":
                                    space.SpaceType = spaceTypeCar;
                                    space.Name = "Car-" + carcounter;
                                    carcounter++;
                                    break;
                                case "Motorcycle":
                                    space.SpaceType = spaceTypeMoto;
                                    space.Name = "Motorcycle-" + motoCounter;
                                    motoCounter++;
                                    break;
                                case "Handicap":
                                    space.SpaceType = spaceTypeHandicap;
                                    space.Name = "Handicap-" + handicapCounter;
                                    handicapCounter++;
                                    break;
                                case "Bus":
                                    space.SpaceType = spaceTypeBus;
                                    space.Name = "Bus-" + busCounter;
                                    busCounter++;
                                    break;
                                case "Street":
                                    space.SpaceType = spaceTypeStreet;
                                    space.Name = "Street-" + streetCounter;
                                    streetCounter++;
                                    break;
                                case "Clear":
                                    space.SpaceType = spaceTypeClear;
                                    space.Name = "Clear-" + clearCounter;
                                    clearCounter++;
                                    break;
                            }
                            break;
                        case "10,14":
                            switch (hidden215.Value)
                            {
                                case "Car":
                                    space.SpaceType = spaceTypeCar;
                                    space.Name = "Car-" + carcounter;
                                    carcounter++;
                                    break;
                                case "Motorcycle":
                                    space.SpaceType = spaceTypeMoto;
                                    space.Name = "Motorcycle-" + motoCounter;
                                    motoCounter++;
                                    break;
                                case "Handicap":
                                    space.SpaceType = spaceTypeHandicap;
                                    space.Name = "Handicap-" + handicapCounter;
                                    handicapCounter++;
                                    break;
                                case "Bus":
                                    space.SpaceType = spaceTypeBus;
                                    space.Name = "Bus-" + busCounter;
                                    busCounter++;
                                    break;
                                case "Street":
                                    space.SpaceType = spaceTypeStreet;
                                    space.Name = "Street-" + streetCounter;
                                    streetCounter++;
                                    break;
                                case "Clear":
                                    space.SpaceType = spaceTypeClear;
                                    space.Name = "Clear-" + clearCounter;
                                    clearCounter++;
                                    break;
                            }
                            break;
                        case "10,15":
                            switch (hidden216.Value)
                            {
                                case "Car":
                                    space.SpaceType = spaceTypeCar;
                                    space.Name = "Car-" + carcounter;
                                    carcounter++;
                                    break;
                                case "Motorcycle":
                                    space.SpaceType = spaceTypeMoto;
                                    space.Name = "Motorcycle-" + motoCounter;
                                    motoCounter++;
                                    break;
                                case "Handicap":
                                    space.SpaceType = spaceTypeHandicap;
                                    space.Name = "Handicap-" + handicapCounter;
                                    handicapCounter++;
                                    break;
                                case "Bus":
                                    space.SpaceType = spaceTypeBus;
                                    space.Name = "Bus-" + busCounter;
                                    busCounter++;
                                    break;
                                case "Street":
                                    space.SpaceType = spaceTypeStreet;
                                    space.Name = "Street-" + streetCounter;
                                    streetCounter++;
                                    break;
                                case "Clear":
                                    space.SpaceType = spaceTypeClear;
                                    space.Name = "Clear-" + clearCounter;
                                    clearCounter++;
                                    break;
                            }
                            break;
                        case "10,16":
                            switch (hidden217.Value)
                            {
                                case "Car":
                                    space.SpaceType = spaceTypeCar;
                                    space.Name = "Car-" + carcounter;
                                    carcounter++;
                                    break;
                                case "Motorcycle":
                                    space.SpaceType = spaceTypeMoto;
                                    space.Name = "Motorcycle-" + motoCounter;
                                    motoCounter++;
                                    break;
                                case "Handicap":
                                    space.SpaceType = spaceTypeHandicap;
                                    space.Name = "Handicap-" + handicapCounter;
                                    handicapCounter++;
                                    break;
                                case "Bus":
                                    space.SpaceType = spaceTypeBus;
                                    space.Name = "Bus-" + busCounter;
                                    busCounter++;
                                    break;
                                case "Street":
                                    space.SpaceType = spaceTypeStreet;
                                    space.Name = "Street-" + streetCounter;
                                    streetCounter++;
                                    break;
                                case "Clear":
                                    space.SpaceType = spaceTypeClear;
                                    space.Name = "Clear-" + clearCounter;
                                    clearCounter++;
                                    break;
                            }
                            break;
                        case "10,17":
                            switch (hidden218.Value)
                            {
                                case "Car":
                                    space.SpaceType = spaceTypeCar;
                                    space.Name = "Car-" + carcounter;
                                    carcounter++;
                                    break;
                                case "Motorcycle":
                                    space.SpaceType = spaceTypeMoto;
                                    space.Name = "Motorcycle-" + motoCounter;
                                    motoCounter++;
                                    break;
                                case "Handicap":
                                    space.SpaceType = spaceTypeHandicap;
                                    space.Name = "Handicap-" + handicapCounter;
                                    handicapCounter++;
                                    break;
                                case "Bus":
                                    space.SpaceType = spaceTypeBus;
                                    space.Name = "Bus-" + busCounter;
                                    busCounter++;
                                    break;
                                case "Street":
                                    space.SpaceType = spaceTypeStreet;
                                    space.Name = "Street-" + streetCounter;
                                    streetCounter++;
                                    break;
                                case "Clear":
                                    space.SpaceType = spaceTypeClear;
                                    space.Name = "Clear-" + clearCounter;
                                    clearCounter++;
                                    break;
                            }
                            break;
                        case "10,18":
                            switch (hidden219.Value)
                            {
                                case "Car":
                                    space.SpaceType = spaceTypeCar;
                                    space.Name = "Car-" + carcounter;
                                    carcounter++;
                                    break;
                                case "Motorcycle":
                                    space.SpaceType = spaceTypeMoto;
                                    space.Name = "Motorcycle-" + motoCounter;
                                    motoCounter++;
                                    break;
                                case "Handicap":
                                    space.SpaceType = spaceTypeHandicap;
                                    space.Name = "Handicap-" + handicapCounter;
                                    handicapCounter++;
                                    break;
                                case "Bus":
                                    space.SpaceType = spaceTypeBus;
                                    space.Name = "Bus-" + busCounter;
                                    busCounter++;
                                    break;
                                case "Street":
                                    space.SpaceType = spaceTypeStreet;
                                    space.Name = "Street-" + streetCounter;
                                    streetCounter++;
                                    break;
                                case "Clear":
                                    space.SpaceType = spaceTypeClear;
                                    space.Name = "Clear-" + clearCounter;
                                    clearCounter++;
                                    break;
                            }
                            break;
                        case "10,19":
                            switch (hidden220.Value)
                            {
                                case "Car":
                                    space.SpaceType = spaceTypeCar;
                                    space.Name = "Car-" + carcounter;
                                    carcounter++;
                                    break;
                                case "Motorcycle":
                                    space.SpaceType = spaceTypeMoto;
                                    space.Name = "Motorcycle-" + motoCounter;
                                    motoCounter++;
                                    break;
                                case "Handicap":
                                    space.SpaceType = spaceTypeHandicap;
                                    space.Name = "Handicap-" + handicapCounter;
                                    handicapCounter++;
                                    break;
                                case "Bus":
                                    space.SpaceType = spaceTypeBus;
                                    space.Name = "Bus-" + busCounter;
                                    busCounter++;
                                    break;
                                case "Street":
                                    space.SpaceType = spaceTypeStreet;
                                    space.Name = "Street-" + streetCounter;
                                    streetCounter++;
                                    break;
                                case "Clear":
                                    space.SpaceType = spaceTypeClear;
                                    space.Name = "Clear-" + clearCounter;
                                    clearCounter++;
                                    break;
                            }
                            break;

                        case "11,0":
                            switch (hidden221.Value)
                            {
                                case "Car":
                                    space.SpaceType = spaceTypeCar;
                                    space.Name = "Car-" + carcounter;
                                    carcounter++;
                                    break;
                                case "Motorcycle":
                                    space.SpaceType = spaceTypeMoto;
                                    space.Name = "Motorcycle-" + motoCounter;
                                    motoCounter++;
                                    break;
                                case "Handicap":
                                    space.SpaceType = spaceTypeHandicap;
                                    space.Name = "Handicap-" + handicapCounter;
                                    handicapCounter++;
                                    break;
                                case "Bus":
                                    space.SpaceType = spaceTypeBus;
                                    space.Name = "Bus-" + busCounter;
                                    busCounter++;
                                    break;
                                case "Street":
                                    space.SpaceType = spaceTypeStreet;
                                    space.Name = "Street-" + streetCounter;
                                    streetCounter++;
                                    break;
                                case "Clear":
                                    space.SpaceType = spaceTypeClear;
                                    space.Name = "Clear-" + clearCounter;
                                    clearCounter++;
                                    break;
                            }
                            break;

                        case "11,1":
                            switch (hidden222.Value)
                            {
                                case "Car":
                                    space.SpaceType = spaceTypeCar;
                                    space.Name = "Car-" + carcounter;
                                    carcounter++;
                                    break;
                                case "Motorcycle":
                                    space.SpaceType = spaceTypeMoto;
                                    space.Name = "Motorcycle-" + motoCounter;
                                    motoCounter++;
                                    break;
                                case "Handicap":
                                    space.SpaceType = spaceTypeHandicap;
                                    space.Name = "Handicap-" + handicapCounter;
                                    handicapCounter++;
                                    break;
                                case "Bus":
                                    space.SpaceType = spaceTypeBus;
                                    space.Name = "Bus-" + busCounter;
                                    busCounter++;
                                    break;
                                case "Street":
                                    space.SpaceType = spaceTypeStreet;
                                    space.Name = "Street-" + streetCounter;
                                    streetCounter++;
                                    break;
                                case "Clear":
                                    space.SpaceType = spaceTypeClear;
                                    space.Name = "Clear-" + clearCounter;
                                    clearCounter++;
                                    break;
                            }
                            break;
                        case "11,2":
                            switch (hidden223.Value)
                            {
                                case "Car":
                                    space.SpaceType = spaceTypeCar;
                                    space.Name = "Car-" + carcounter;
                                    carcounter++;
                                    break;
                                case "Motorcycle":
                                    space.SpaceType = spaceTypeMoto;
                                    space.Name = "Motorcycle-" + motoCounter;
                                    motoCounter++;
                                    break;
                                case "Handicap":
                                    space.SpaceType = spaceTypeHandicap;
                                    space.Name = "Handicap-" + handicapCounter;
                                    handicapCounter++;
                                    break;
                                case "Bus":
                                    space.SpaceType = spaceTypeBus;
                                    space.Name = "Bus-" + busCounter;
                                    busCounter++;
                                    break;
                                case "Street":
                                    space.SpaceType = spaceTypeStreet;
                                    space.Name = "Street-" + streetCounter;
                                    streetCounter++;
                                    break;
                                case "Clear":
                                    space.SpaceType = spaceTypeClear;
                                    space.Name = "Clear-" + clearCounter;
                                    clearCounter++;
                                    break;
                            }
                            break;
                        case "11,3":
                            switch (hidden224.Value)
                            {
                                case "Car":
                                    space.SpaceType = spaceTypeCar;
                                    space.Name = "Car-" + carcounter;
                                    carcounter++;
                                    break;
                                case "Motorcycle":
                                    space.SpaceType = spaceTypeMoto;
                                    space.Name = "Motorcycle-" + motoCounter;
                                    motoCounter++;
                                    break;
                                case "Handicap":
                                    space.SpaceType = spaceTypeHandicap;
                                    space.Name = "Handicap-" + handicapCounter;
                                    handicapCounter++;
                                    break;
                                case "Bus":
                                    space.SpaceType = spaceTypeBus;
                                    space.Name = "Bus-" + busCounter;
                                    busCounter++;
                                    break;
                                case "Street":
                                    space.SpaceType = spaceTypeStreet;
                                    space.Name = "Street-" + streetCounter;
                                    streetCounter++;
                                    break;
                                case "Clear":
                                    space.SpaceType = spaceTypeClear;
                                    space.Name = "Clear-" + clearCounter;
                                    clearCounter++;
                                    break;
                            }
                            break;
                        case "11,4":
                            switch (hidden225.Value)
                            {
                                case "Car":
                                    space.SpaceType = spaceTypeCar;
                                    space.Name = "Car-" + carcounter;
                                    carcounter++;
                                    break;
                                case "Motorcycle":
                                    space.SpaceType = spaceTypeMoto;
                                    space.Name = "Motorcycle-" + motoCounter;
                                    motoCounter++;
                                    break;
                                case "Handicap":
                                    space.SpaceType = spaceTypeHandicap;
                                    space.Name = "Handicap-" + handicapCounter;
                                    handicapCounter++;
                                    break;
                                case "Bus":
                                    space.SpaceType = spaceTypeBus;
                                    space.Name = "Bus-" + busCounter;
                                    busCounter++;
                                    break;
                                case "Street":
                                    space.SpaceType = spaceTypeStreet;
                                    space.Name = "Street-" + streetCounter;
                                    streetCounter++;
                                    break;
                                case "Clear":
                                    space.SpaceType = spaceTypeClear;
                                    space.Name = "Clear-" + clearCounter;
                                    clearCounter++;
                                    break;
                            }
                            break;
                        case "11,5":
                            switch (hidden226.Value)
                            {
                                case "Car":
                                    space.SpaceType = spaceTypeCar;
                                    space.Name = "Car-" + carcounter;
                                    carcounter++;
                                    break;
                                case "Motorcycle":
                                    space.SpaceType = spaceTypeMoto;
                                    space.Name = "Motorcycle-" + motoCounter;
                                    motoCounter++;
                                    break;
                                case "Handicap":
                                    space.SpaceType = spaceTypeHandicap;
                                    space.Name = "Handicap-" + handicapCounter;
                                    handicapCounter++;
                                    break;
                                case "Bus":
                                    space.SpaceType = spaceTypeBus;
                                    space.Name = "Bus-" + busCounter;
                                    busCounter++;
                                    break;
                                case "Street":
                                    space.SpaceType = spaceTypeStreet;
                                    space.Name = "Street-" + streetCounter;
                                    streetCounter++;
                                    break;
                                case "Clear":
                                    space.SpaceType = spaceTypeClear;
                                    space.Name = "Clear-" + clearCounter;
                                    clearCounter++;
                                    break;
                            }
                            break;
                        case "11,6":
                            switch (hidden227.Value)
                            {
                                case "Car":
                                    space.SpaceType = spaceTypeCar;
                                    space.Name = "Car-" + carcounter;
                                    carcounter++;
                                    break;
                                case "Motorcycle":
                                    space.SpaceType = spaceTypeMoto;
                                    space.Name = "Motorcycle-" + motoCounter;
                                    motoCounter++;
                                    break;
                                case "Handicap":
                                    space.SpaceType = spaceTypeHandicap;
                                    space.Name = "Handicap-" + handicapCounter;
                                    handicapCounter++;
                                    break;
                                case "Bus":
                                    space.SpaceType = spaceTypeBus;
                                    space.Name = "Bus-" + busCounter;
                                    busCounter++;
                                    break;
                                case "Street":
                                    space.SpaceType = spaceTypeStreet;
                                    space.Name = "Street-" + streetCounter;
                                    streetCounter++;
                                    break;
                                case "Clear":
                                    space.SpaceType = spaceTypeClear;
                                    space.Name = "Clear-" + clearCounter;
                                    clearCounter++;
                                    break;
                            }
                            break;
                        case "11,7":
                            switch (hidden228.Value)
                            {
                                case "Car":
                                    space.SpaceType = spaceTypeCar;
                                    space.Name = "Car-" + carcounter;
                                    carcounter++;
                                    break;
                                case "Motorcycle":
                                    space.SpaceType = spaceTypeMoto;
                                    space.Name = "Motorcycle-" + motoCounter;
                                    motoCounter++;
                                    break;
                                case "Handicap":
                                    space.SpaceType = spaceTypeHandicap;
                                    space.Name = "Handicap-" + handicapCounter;
                                    handicapCounter++;
                                    break;
                                case "Bus":
                                    space.SpaceType = spaceTypeBus;
                                    space.Name = "Bus-" + busCounter;
                                    busCounter++;
                                    break;
                                case "Street":
                                    space.SpaceType = spaceTypeStreet;
                                    space.Name = "Street-" + streetCounter;
                                    streetCounter++;
                                    break;
                                case "Clear":
                                    space.SpaceType = spaceTypeClear;
                                    space.Name = "Clear-" + clearCounter;
                                    clearCounter++;
                                    break;
                            }
                            break;
                        case "11,8":
                            switch (hidden229.Value)
                            {
                                case "Car":
                                    space.SpaceType = spaceTypeCar;
                                    space.Name = "Car-" + carcounter;
                                    carcounter++;
                                    break;
                                case "Motorcycle":
                                    space.SpaceType = spaceTypeMoto;
                                    space.Name = "Motorcycle-" + motoCounter;
                                    motoCounter++;
                                    break;
                                case "Handicap":
                                    space.SpaceType = spaceTypeHandicap;
                                    space.Name = "Handicap-" + handicapCounter;
                                    handicapCounter++;
                                    break;
                                case "Bus":
                                    space.SpaceType = spaceTypeBus;
                                    space.Name = "Bus-" + busCounter;
                                    busCounter++;
                                    break;
                                case "Street":
                                    space.SpaceType = spaceTypeStreet;
                                    space.Name = "Street-" + streetCounter;
                                    streetCounter++;
                                    break;
                                case "Clear":
                                    space.SpaceType = spaceTypeClear;
                                    space.Name = "Clear-" + clearCounter;
                                    clearCounter++;
                                    break;
                            }
                            break;
                        case "11,9":
                            switch (hidden230.Value)
                            {
                                case "Car":
                                    space.SpaceType = spaceTypeCar;
                                    space.Name = "Car-" + carcounter;
                                    carcounter++;
                                    break;
                                case "Motorcycle":
                                    space.SpaceType = spaceTypeMoto;
                                    space.Name = "Motorcycle-" + motoCounter;
                                    motoCounter++;
                                    break;
                                case "Handicap":
                                    space.SpaceType = spaceTypeHandicap;
                                    space.Name = "Handicap-" + handicapCounter;
                                    handicapCounter++;
                                    break;
                                case "Bus":
                                    space.SpaceType = spaceTypeBus;
                                    space.Name = "Bus-" + busCounter;
                                    busCounter++;
                                    break;
                                case "Street":
                                    space.SpaceType = spaceTypeStreet;
                                    space.Name = "Street-" + streetCounter;
                                    streetCounter++;
                                    break;
                                case "Clear":
                                    space.SpaceType = spaceTypeClear;
                                    space.Name = "Clear-" + clearCounter;
                                    clearCounter++;
                                    break;
                            }
                            break;
                        case "11,10":
                            switch (hidden231.Value)
                            {
                                case "Car":
                                    space.SpaceType = spaceTypeCar;
                                    space.Name = "Car-" + carcounter;
                                    carcounter++;
                                    break;
                                case "Motorcycle":
                                    space.SpaceType = spaceTypeMoto;
                                    space.Name = "Motorcycle-" + motoCounter;
                                    motoCounter++;
                                    break;
                                case "Handicap":
                                    space.SpaceType = spaceTypeHandicap;
                                    space.Name = "Handicap-" + handicapCounter;
                                    handicapCounter++;
                                    break;
                                case "Bus":
                                    space.SpaceType = spaceTypeBus;
                                    space.Name = "Bus-" + busCounter;
                                    busCounter++;
                                    break;
                                case "Street":
                                    space.SpaceType = spaceTypeStreet;
                                    space.Name = "Street-" + streetCounter;
                                    streetCounter++;
                                    break;
                                case "Clear":
                                    space.SpaceType = spaceTypeClear;
                                    space.Name = "Clear-" + clearCounter;
                                    clearCounter++;
                                    break;
                            }
                            break;
                        case "11,11":
                            switch (hidden232.Value)
                            {
                                case "Car":
                                    space.SpaceType = spaceTypeCar;
                                    space.Name = "Car-" + carcounter;
                                    carcounter++;
                                    break;
                                case "Motorcycle":
                                    space.SpaceType = spaceTypeMoto;
                                    space.Name = "Motorcycle-" + motoCounter;
                                    motoCounter++;
                                    break;
                                case "Handicap":
                                    space.SpaceType = spaceTypeHandicap;
                                    space.Name = "Handicap-" + handicapCounter;
                                    handicapCounter++;
                                    break;
                                case "Bus":
                                    space.SpaceType = spaceTypeBus;
                                    space.Name = "Bus-" + busCounter;
                                    busCounter++;
                                    break;
                                case "Street":
                                    space.SpaceType = spaceTypeStreet;
                                    space.Name = "Street-" + streetCounter;
                                    streetCounter++;
                                    break;
                                case "Clear":
                                    space.SpaceType = spaceTypeClear;
                                    space.Name = "Clear-" + clearCounter;
                                    clearCounter++;
                                    break;
                            }
                            break;
                        case "11,12":
                            switch (hidden233.Value)
                            {
                                case "Car":
                                    space.SpaceType = spaceTypeCar;
                                    space.Name = "Car-" + carcounter;
                                    carcounter++;
                                    break;
                                case "Motorcycle":
                                    space.SpaceType = spaceTypeMoto;
                                    space.Name = "Motorcycle-" + motoCounter;
                                    motoCounter++;
                                    break;
                                case "Handicap":
                                    space.SpaceType = spaceTypeHandicap;
                                    space.Name = "Handicap-" + handicapCounter;
                                    handicapCounter++;
                                    break;
                                case "Bus":
                                    space.SpaceType = spaceTypeBus;
                                    space.Name = "Bus-" + busCounter;
                                    busCounter++;
                                    break;
                                case "Street":
                                    space.SpaceType = spaceTypeStreet;
                                    space.Name = "Street-" + streetCounter;
                                    streetCounter++;
                                    break;
                                case "Clear":
                                    space.SpaceType = spaceTypeClear;
                                    space.Name = "Clear-" + clearCounter;
                                    clearCounter++;
                                    break;
                            }
                            break;
                        case "11,13":
                            switch (hidden234.Value)
                            {
                                case "Car":
                                    space.SpaceType = spaceTypeCar;
                                    space.Name = "Car-" + carcounter;
                                    carcounter++;
                                    break;
                                case "Motorcycle":
                                    space.SpaceType = spaceTypeMoto;
                                    space.Name = "Motorcycle-" + motoCounter;
                                    motoCounter++;
                                    break;
                                case "Handicap":
                                    space.SpaceType = spaceTypeHandicap;
                                    space.Name = "Handicap-" + handicapCounter;
                                    handicapCounter++;
                                    break;
                                case "Bus":
                                    space.SpaceType = spaceTypeBus;
                                    space.Name = "Bus-" + busCounter;
                                    busCounter++;
                                    break;
                                case "Street":
                                    space.SpaceType = spaceTypeStreet;
                                    space.Name = "Street-" + streetCounter;
                                    streetCounter++;
                                    break;
                                case "Clear":
                                    space.SpaceType = spaceTypeClear;
                                    space.Name = "Clear-" + clearCounter;
                                    clearCounter++;
                                    break;
                            }
                            break;
                        case "11,14":
                            switch (hidden235.Value)
                            {
                                case "Car":
                                    space.SpaceType = spaceTypeCar;
                                    space.Name = "Car-" + carcounter;
                                    carcounter++;
                                    break;
                                case "Motorcycle":
                                    space.SpaceType = spaceTypeMoto;
                                    space.Name = "Motorcycle-" + motoCounter;
                                    motoCounter++;
                                    break;
                                case "Handicap":
                                    space.SpaceType = spaceTypeHandicap;
                                    space.Name = "Handicap-" + handicapCounter;
                                    handicapCounter++;
                                    break;
                                case "Bus":
                                    space.SpaceType = spaceTypeBus;
                                    space.Name = "Bus-" + busCounter;
                                    busCounter++;
                                    break;
                                case "Street":
                                    space.SpaceType = spaceTypeStreet;
                                    space.Name = "Street-" + streetCounter;
                                    streetCounter++;
                                    break;
                                case "Clear":
                                    space.SpaceType = spaceTypeClear;
                                    space.Name = "Clear-" + clearCounter;
                                    clearCounter++;
                                    break;
                            }
                            break;
                        case "11,15":
                            switch (hidden236.Value)
                            {
                                case "Car":
                                    space.SpaceType = spaceTypeCar;
                                    space.Name = "Car-" + carcounter;
                                    carcounter++;
                                    break;
                                case "Motorcycle":
                                    space.SpaceType = spaceTypeMoto;
                                    space.Name = "Motorcycle-" + motoCounter;
                                    motoCounter++;
                                    break;
                                case "Handicap":
                                    space.SpaceType = spaceTypeHandicap;
                                    space.Name = "Handicap-" + handicapCounter;
                                    handicapCounter++;
                                    break;
                                case "Bus":
                                    space.SpaceType = spaceTypeBus;
                                    space.Name = "Bus-" + busCounter;
                                    busCounter++;
                                    break;
                                case "Street":
                                    space.SpaceType = spaceTypeStreet;
                                    space.Name = "Street-" + streetCounter;
                                    streetCounter++;
                                    break;
                                case "Clear":
                                    space.SpaceType = spaceTypeClear;
                                    space.Name = "Clear-" + clearCounter;
                                    clearCounter++;
                                    break;
                            }
                            break;
                        case "11,16":
                            switch (hidden237.Value)
                            {
                                case "Car":
                                    space.SpaceType = spaceTypeCar;
                                    space.Name = "Car-" + carcounter;
                                    carcounter++;
                                    break;
                                case "Motorcycle":
                                    space.SpaceType = spaceTypeMoto;
                                    space.Name = "Motorcycle-" + motoCounter;
                                    motoCounter++;
                                    break;
                                case "Handicap":
                                    space.SpaceType = spaceTypeHandicap;
                                    space.Name = "Handicap-" + handicapCounter;
                                    handicapCounter++;
                                    break;
                                case "Bus":
                                    space.SpaceType = spaceTypeBus;
                                    space.Name = "Bus-" + busCounter;
                                    busCounter++;
                                    break;
                                case "Street":
                                    space.SpaceType = spaceTypeStreet;
                                    space.Name = "Street-" + streetCounter;
                                    streetCounter++;
                                    break;
                                case "Clear":
                                    space.SpaceType = spaceTypeClear;
                                    space.Name = "Clear-" + clearCounter;
                                    clearCounter++;
                                    break;
                            }
                            break;
                        case "11,17":
                            switch (hidden238.Value)
                            {
                                case "Car":
                                    space.SpaceType = spaceTypeCar;
                                    space.Name = "Car-" + carcounter;
                                    carcounter++;
                                    break;
                                case "Motorcycle":
                                    space.SpaceType = spaceTypeMoto;
                                    space.Name = "Motorcycle-" + motoCounter;
                                    motoCounter++;
                                    break;
                                case "Handicap":
                                    space.SpaceType = spaceTypeHandicap;
                                    space.Name = "Handicap-" + handicapCounter;
                                    handicapCounter++;
                                    break;
                                case "Bus":
                                    space.SpaceType = spaceTypeBus;
                                    space.Name = "Bus-" + busCounter;
                                    busCounter++;
                                    break;
                                case "Street":
                                    space.SpaceType = spaceTypeStreet;
                                    space.Name = "Street-" + streetCounter;
                                    streetCounter++;
                                    break;
                                case "Clear":
                                    space.SpaceType = spaceTypeClear;
                                    space.Name = "Clear-" + clearCounter;
                                    clearCounter++;
                                    break;
                            }
                            break;
                        case "11,18":
                            switch (hidden239.Value)
                            {
                                case "Car":
                                    space.SpaceType = spaceTypeCar;
                                    space.Name = "Car-" + carcounter;
                                    carcounter++;
                                    break;
                                case "Motorcycle":
                                    space.SpaceType = spaceTypeMoto;
                                    space.Name = "Motorcycle-" + motoCounter;
                                    motoCounter++;
                                    break;
                                case "Handicap":
                                    space.SpaceType = spaceTypeHandicap;
                                    space.Name = "Handicap-" + handicapCounter;
                                    handicapCounter++;
                                    break;
                                case "Bus":
                                    space.SpaceType = spaceTypeBus;
                                    space.Name = "Bus-" + busCounter;
                                    busCounter++;
                                    break;
                                case "Street":
                                    space.SpaceType = spaceTypeStreet;
                                    space.Name = "Street-" + streetCounter;
                                    streetCounter++;
                                    break;
                                case "Clear":
                                    space.SpaceType = spaceTypeClear;
                                    space.Name = "Clear-" + clearCounter;
                                    clearCounter++;
                                    break;
                            }
                            break;
                        case "11,19":
                            switch (hidden240.Value)
                            {
                                case "Car":
                                    space.SpaceType = spaceTypeCar;
                                    space.Name = "Car-" + carcounter;
                                    carcounter++;
                                    break;
                                case "Motorcycle":
                                    space.SpaceType = spaceTypeMoto;
                                    space.Name = "Motorcycle-" + motoCounter;
                                    motoCounter++;
                                    break;
                                case "Handicap":
                                    space.SpaceType = spaceTypeHandicap;
                                    space.Name = "Handicap-" + handicapCounter;
                                    handicapCounter++;
                                    break;
                                case "Bus":
                                    space.SpaceType = spaceTypeBus;
                                    space.Name = "Bus-" + busCounter;
                                    busCounter++;
                                    break;
                                case "Street":
                                    space.SpaceType = spaceTypeStreet;
                                    space.Name = "Street-" + streetCounter;
                                    streetCounter++;
                                    break;
                                case "Clear":
                                    space.SpaceType = spaceTypeClear;
                                    space.Name = "Clear-" + clearCounter;
                                    clearCounter++;
                                    break;
                            }
                            break;

                        case "12,0":
                            switch (hidden241.Value)
                            {
                                case "Car":
                                    space.SpaceType = spaceTypeCar;
                                    space.Name = "Car-" + carcounter;
                                    carcounter++;
                                    break;
                                case "Motorcycle":
                                    space.SpaceType = spaceTypeMoto;
                                    space.Name = "Motorcycle-" + motoCounter;
                                    motoCounter++;
                                    break;
                                case "Handicap":
                                    space.SpaceType = spaceTypeHandicap;
                                    space.Name = "Handicap-" + handicapCounter;
                                    handicapCounter++;
                                    break;
                                case "Bus":
                                    space.SpaceType = spaceTypeBus;
                                    space.Name = "Bus-" + busCounter;
                                    busCounter++;
                                    break;
                                case "Street":
                                    space.SpaceType = spaceTypeStreet;
                                    space.Name = "Street-" + streetCounter;
                                    streetCounter++;
                                    break;
                                case "Clear":
                                    space.SpaceType = spaceTypeClear;
                                    space.Name = "Clear-" + clearCounter;
                                    clearCounter++;
                                    break;
                            }
                            break;
                        case "12,1":
                            switch (hidden242.Value)
                            {
                                case "Car":
                                    space.SpaceType = spaceTypeCar;
                                    space.Name = "Car-" + carcounter;
                                    carcounter++;
                                    break;
                                case "Motorcycle":
                                    space.SpaceType = spaceTypeMoto;
                                    space.Name = "Motorcycle-" + motoCounter;
                                    motoCounter++;
                                    break;
                                case "Handicap":
                                    space.SpaceType = spaceTypeHandicap;
                                    space.Name = "Handicap-" + handicapCounter;
                                    handicapCounter++;
                                    break;
                                case "Bus":
                                    space.SpaceType = spaceTypeBus;
                                    space.Name = "Bus-" + busCounter;
                                    busCounter++;
                                    break;
                                case "Street":
                                    space.SpaceType = spaceTypeStreet;
                                    space.Name = "Street-" + streetCounter;
                                    streetCounter++;
                                    break;
                                case "Clear":
                                    space.SpaceType = spaceTypeClear;
                                    space.Name = "Clear-" + clearCounter;
                                    clearCounter++;
                                    break;
                            }
                            break;
                        case "12,2":
                            switch (hidden243.Value)
                            {
                                case "Car":
                                    space.SpaceType = spaceTypeCar;
                                    space.Name = "Car-" + carcounter;
                                    carcounter++;
                                    break;
                                case "Motorcycle":
                                    space.SpaceType = spaceTypeMoto;
                                    space.Name = "Motorcycle-" + motoCounter;
                                    motoCounter++;
                                    break;
                                case "Handicap":
                                    space.SpaceType = spaceTypeHandicap;
                                    space.Name = "Handicap-" + handicapCounter;
                                    handicapCounter++;
                                    break;
                                case "Bus":
                                    space.SpaceType = spaceTypeBus;
                                    space.Name = "Bus-" + busCounter;
                                    busCounter++;
                                    break;
                                case "Street":
                                    space.SpaceType = spaceTypeStreet;
                                    space.Name = "Street-" + streetCounter;
                                    streetCounter++;
                                    break;
                                case "Clear":
                                    space.SpaceType = spaceTypeClear;
                                    space.Name = "Clear-" + clearCounter;
                                    clearCounter++;
                                    break;
                            }
                            break;
                        case "12,3":
                            switch (hidden244.Value)
                            {
                                case "Car":
                                    space.SpaceType = spaceTypeCar;
                                    space.Name = "Car-" + carcounter;
                                    carcounter++;
                                    break;
                                case "Motorcycle":
                                    space.SpaceType = spaceTypeMoto;
                                    space.Name = "Motorcycle-" + motoCounter;
                                    motoCounter++;
                                    break;
                                case "Handicap":
                                    space.SpaceType = spaceTypeHandicap;
                                    space.Name = "Handicap-" + handicapCounter;
                                    handicapCounter++;
                                    break;
                                case "Bus":
                                    space.SpaceType = spaceTypeBus;
                                    space.Name = "Bus-" + busCounter;
                                    busCounter++;
                                    break;
                                case "Street":
                                    space.SpaceType = spaceTypeStreet;
                                    space.Name = "Street-" + streetCounter;
                                    streetCounter++;
                                    break;
                                case "Clear":
                                    space.SpaceType = spaceTypeClear;
                                    space.Name = "Clear-" + clearCounter;
                                    clearCounter++;
                                    break;
                            }
                            break;
                        case "12,4":
                            switch (hidden245.Value)
                            {
                                case "Car":
                                    space.SpaceType = spaceTypeCar;
                                    space.Name = "Car-" + carcounter;
                                    carcounter++;
                                    break;
                                case "Motorcycle":
                                    space.SpaceType = spaceTypeMoto;
                                    space.Name = "Motorcycle-" + motoCounter;
                                    motoCounter++;
                                    break;
                                case "Handicap":
                                    space.SpaceType = spaceTypeHandicap;
                                    space.Name = "Handicap-" + handicapCounter;
                                    handicapCounter++;
                                    break;
                                case "Bus":
                                    space.SpaceType = spaceTypeBus;
                                    space.Name = "Bus-" + busCounter;
                                    busCounter++;
                                    break;
                                case "Street":
                                    space.SpaceType = spaceTypeStreet;
                                    space.Name = "Street-" + streetCounter;
                                    streetCounter++;
                                    break;
                                case "Clear":
                                    space.SpaceType = spaceTypeClear;
                                    space.Name = "Clear-" + clearCounter;
                                    clearCounter++;
                                    break;
                            }
                            break;
                        case "12,5":
                            switch (hidden246.Value)
                            {
                                case "Car":
                                    space.SpaceType = spaceTypeCar;
                                    space.Name = "Car-" + carcounter;
                                    carcounter++;
                                    break;
                                case "Motorcycle":
                                    space.SpaceType = spaceTypeMoto;
                                    space.Name = "Motorcycle-" + motoCounter;
                                    motoCounter++;
                                    break;
                                case "Handicap":
                                    space.SpaceType = spaceTypeHandicap;
                                    space.Name = "Handicap-" + handicapCounter;
                                    handicapCounter++;
                                    break;
                                case "Bus":
                                    space.SpaceType = spaceTypeBus;
                                    space.Name = "Bus-" + busCounter;
                                    busCounter++;
                                    break;
                                case "Street":
                                    space.SpaceType = spaceTypeStreet;
                                    space.Name = "Street-" + streetCounter;
                                    streetCounter++;
                                    break;
                                case "Clear":
                                    space.SpaceType = spaceTypeClear;
                                    space.Name = "Clear-" + clearCounter;
                                    clearCounter++;
                                    break;
                            }
                            break;
                        case "12,6":
                            switch (hidden247.Value)
                            {
                                case "Car":
                                    space.SpaceType = spaceTypeCar;
                                    space.Name = "Car-" + carcounter;
                                    carcounter++;
                                    break;
                                case "Motorcycle":
                                    space.SpaceType = spaceTypeMoto;
                                    space.Name = "Motorcycle-" + motoCounter;
                                    motoCounter++;
                                    break;
                                case "Handicap":
                                    space.SpaceType = spaceTypeHandicap;
                                    space.Name = "Handicap-" + handicapCounter;
                                    handicapCounter++;
                                    break;
                                case "Bus":
                                    space.SpaceType = spaceTypeBus;
                                    space.Name = "Bus-" + busCounter;
                                    busCounter++;
                                    break;
                                case "Street":
                                    space.SpaceType = spaceTypeStreet;
                                    space.Name = "Street-" + streetCounter;
                                    streetCounter++;
                                    break;
                                case "Clear":
                                    space.SpaceType = spaceTypeClear;
                                    space.Name = "Clear-" + clearCounter;
                                    clearCounter++;
                                    break;
                            }
                            break;
                        case "12,7":
                            switch (hidden248.Value)
                            {
                                case "Car":
                                    space.SpaceType = spaceTypeCar;
                                    space.Name = "Car-" + carcounter;
                                    carcounter++;
                                    break;
                                case "Motorcycle":
                                    space.SpaceType = spaceTypeMoto;
                                    space.Name = "Motorcycle-" + motoCounter;
                                    motoCounter++;
                                    break;
                                case "Handicap":
                                    space.SpaceType = spaceTypeHandicap;
                                    space.Name = "Handicap-" + handicapCounter;
                                    handicapCounter++;
                                    break;
                                case "Bus":
                                    space.SpaceType = spaceTypeBus;
                                    space.Name = "Bus-" + busCounter;
                                    busCounter++;
                                    break;
                                case "Street":
                                    space.SpaceType = spaceTypeStreet;
                                    space.Name = "Street-" + streetCounter;
                                    streetCounter++;
                                    break;
                                case "Clear":
                                    space.SpaceType = spaceTypeClear;
                                    space.Name = "Clear-" + clearCounter;
                                    clearCounter++;
                                    break;
                            }
                            break;
                        case "12,8":
                            switch (hidden249.Value)
                            {
                                case "Car":
                                    space.SpaceType = spaceTypeCar;
                                    space.Name = "Car-" + carcounter;
                                    carcounter++;
                                    break;
                                case "Motorcycle":
                                    space.SpaceType = spaceTypeMoto;
                                    space.Name = "Motorcycle-" + motoCounter;
                                    motoCounter++;
                                    break;
                                case "Handicap":
                                    space.SpaceType = spaceTypeHandicap;
                                    space.Name = "Handicap-" + handicapCounter;
                                    handicapCounter++;
                                    break;
                                case "Bus":
                                    space.SpaceType = spaceTypeBus;
                                    space.Name = "Bus-" + busCounter;
                                    busCounter++;
                                    break;
                                case "Street":
                                    space.SpaceType = spaceTypeStreet;
                                    space.Name = "Street-" + streetCounter;
                                    streetCounter++;
                                    break;
                                case "Clear":
                                    space.SpaceType = spaceTypeClear;
                                    space.Name = "Clear-" + clearCounter;
                                    clearCounter++;
                                    break;
                            }
                            break;
                        case "12,9":
                            switch (hidden250.Value)
                            {
                                case "Car":
                                    space.SpaceType = spaceTypeCar;
                                    space.Name = "Car-" + carcounter;
                                    carcounter++;
                                    break;
                                case "Motorcycle":
                                    space.SpaceType = spaceTypeMoto;
                                    space.Name = "Motorcycle-" + motoCounter;
                                    motoCounter++;
                                    break;
                                case "Handicap":
                                    space.SpaceType = spaceTypeHandicap;
                                    space.Name = "Handicap-" + handicapCounter;
                                    handicapCounter++;
                                    break;
                                case "Bus":
                                    space.SpaceType = spaceTypeBus;
                                    space.Name = "Bus-" + busCounter;
                                    busCounter++;
                                    break;
                                case "Street":
                                    space.SpaceType = spaceTypeStreet;
                                    space.Name = "Street-" + streetCounter;
                                    streetCounter++;
                                    break;
                                case "Clear":
                                    space.SpaceType = spaceTypeClear;
                                    space.Name = "Clear-" + clearCounter;
                                    clearCounter++;
                                    break;
                            }
                            break;
                        case "12,10":
                            switch (hidden251.Value)
                            {
                                case "Car":
                                    space.SpaceType = spaceTypeCar;
                                    space.Name = "Car-" + carcounter;
                                    carcounter++;
                                    break;
                                case "Motorcycle":
                                    space.SpaceType = spaceTypeMoto;
                                    space.Name = "Motorcycle-" + motoCounter;
                                    motoCounter++;
                                    break;
                                case "Handicap":
                                    space.SpaceType = spaceTypeHandicap;
                                    space.Name = "Handicap-" + handicapCounter;
                                    handicapCounter++;
                                    break;
                                case "Bus":
                                    space.SpaceType = spaceTypeBus;
                                    space.Name = "Bus-" + busCounter;
                                    busCounter++;
                                    break;
                                case "Street":
                                    space.SpaceType = spaceTypeStreet;
                                    space.Name = "Street-" + streetCounter;
                                    streetCounter++;
                                    break;
                                case "Clear":
                                    space.SpaceType = spaceTypeClear;
                                    space.Name = "Clear-" + clearCounter;
                                    clearCounter++;
                                    break;
                            }
                            break;
                        case "12,11":
                            switch (hidden252.Value)
                            {
                                case "Car":
                                    space.SpaceType = spaceTypeCar;
                                    space.Name = "Car-" + carcounter;
                                    carcounter++;
                                    break;
                                case "Motorcycle":
                                    space.SpaceType = spaceTypeMoto;
                                    space.Name = "Motorcycle-" + motoCounter;
                                    motoCounter++;
                                    break;
                                case "Handicap":
                                    space.SpaceType = spaceTypeHandicap;
                                    space.Name = "Handicap-" + handicapCounter;
                                    handicapCounter++;
                                    break;
                                case "Bus":
                                    space.SpaceType = spaceTypeBus;
                                    space.Name = "Bus-" + busCounter;
                                    busCounter++;
                                    break;
                                case "Street":
                                    space.SpaceType = spaceTypeStreet;
                                    space.Name = "Street-" + streetCounter;
                                    streetCounter++;
                                    break;
                                case "Clear":
                                    space.SpaceType = spaceTypeClear;
                                    space.Name = "Clear-" + clearCounter;
                                    clearCounter++;
                                    break;
                            }
                            break;
                        case "12,12":
                            switch (hidden253.Value)
                            {
                                case "Car":
                                    space.SpaceType = spaceTypeCar;
                                    space.Name = "Car-" + carcounter;
                                    carcounter++;
                                    break;
                                case "Motorcycle":
                                    space.SpaceType = spaceTypeMoto;
                                    space.Name = "Motorcycle-" + motoCounter;
                                    motoCounter++;
                                    break;
                                case "Handicap":
                                    space.SpaceType = spaceTypeHandicap;
                                    space.Name = "Handicap-" + handicapCounter;
                                    handicapCounter++;
                                    break;
                                case "Bus":
                                    space.SpaceType = spaceTypeBus;
                                    space.Name = "Bus-" + busCounter;
                                    busCounter++;
                                    break;
                                case "Street":
                                    space.SpaceType = spaceTypeStreet;
                                    space.Name = "Street-" + streetCounter;
                                    streetCounter++;
                                    break;
                                case "Clear":
                                    space.SpaceType = spaceTypeClear;
                                    space.Name = "Clear-" + clearCounter;
                                    clearCounter++;
                                    break;
                            }
                            break;
                        case "12,13":
                            switch (hidden254.Value)
                            {
                                case "Car":
                                    space.SpaceType = spaceTypeCar;
                                    space.Name = "Car-" + carcounter;
                                    carcounter++;
                                    break;
                                case "Motorcycle":
                                    space.SpaceType = spaceTypeMoto;
                                    space.Name = "Motorcycle-" + motoCounter;
                                    motoCounter++;
                                    break;
                                case "Handicap":
                                    space.SpaceType = spaceTypeHandicap;
                                    space.Name = "Handicap-" + handicapCounter;
                                    handicapCounter++;
                                    break;
                                case "Bus":
                                    space.SpaceType = spaceTypeBus;
                                    space.Name = "Bus-" + busCounter;
                                    busCounter++;
                                    break;
                                case "Street":
                                    space.SpaceType = spaceTypeStreet;
                                    space.Name = "Street-" + streetCounter;
                                    streetCounter++;
                                    break;
                                case "Clear":
                                    space.SpaceType = spaceTypeClear;
                                    space.Name = "Clear-" + clearCounter;
                                    clearCounter++;
                                    break;
                            }
                            break;
                        case "12,14":
                            switch (hidden255.Value)
                            {
                                case "Car":
                                    space.SpaceType = spaceTypeCar;
                                    space.Name = "Car-" + carcounter;
                                    carcounter++;
                                    break;
                                case "Motorcycle":
                                    space.SpaceType = spaceTypeMoto;
                                    space.Name = "Motorcycle-" + motoCounter;
                                    motoCounter++;
                                    break;
                                case "Handicap":
                                    space.SpaceType = spaceTypeHandicap;
                                    space.Name = "Handicap-" + handicapCounter;
                                    handicapCounter++;
                                    break;
                                case "Bus":
                                    space.SpaceType = spaceTypeBus;
                                    space.Name = "Bus-" + busCounter;
                                    busCounter++;
                                    break;
                                case "Street":
                                    space.SpaceType = spaceTypeStreet;
                                    space.Name = "Street-" + streetCounter;
                                    streetCounter++;
                                    break;
                                case "Clear":
                                    space.SpaceType = spaceTypeClear;
                                    space.Name = "Clear-" + clearCounter;
                                    clearCounter++;
                                    break;
                            }
                            break;
                        case "12,15":
                            switch (hidden256.Value)
                            {
                                case "Car":
                                    space.SpaceType = spaceTypeCar;
                                    space.Name = "Car-" + carcounter;
                                    carcounter++;
                                    break;
                                case "Motorcycle":
                                    space.SpaceType = spaceTypeMoto;
                                    space.Name = "Motorcycle-" + motoCounter;
                                    motoCounter++;
                                    break;
                                case "Handicap":
                                    space.SpaceType = spaceTypeHandicap;
                                    space.Name = "Handicap-" + handicapCounter;
                                    handicapCounter++;
                                    break;
                                case "Bus":
                                    space.SpaceType = spaceTypeBus;
                                    space.Name = "Bus-" + busCounter;
                                    busCounter++;
                                    break;
                                case "Street":
                                    space.SpaceType = spaceTypeStreet;
                                    space.Name = "Street-" + streetCounter;
                                    streetCounter++;
                                    break;
                                case "Clear":
                                    space.SpaceType = spaceTypeClear;
                                    space.Name = "Clear-" + clearCounter;
                                    clearCounter++;
                                    break;
                            }
                            break;
                        case "12,16":
                            switch (hidden257.Value)
                            {
                                case "Car":
                                    space.SpaceType = spaceTypeCar;
                                    space.Name = "Car-" + carcounter;
                                    carcounter++;
                                    break;
                                case "Motorcycle":
                                    space.SpaceType = spaceTypeMoto;
                                    space.Name = "Motorcycle-" + motoCounter;
                                    motoCounter++;
                                    break;
                                case "Handicap":
                                    space.SpaceType = spaceTypeHandicap;
                                    space.Name = "Handicap-" + handicapCounter;
                                    handicapCounter++;
                                    break;
                                case "Bus":
                                    space.SpaceType = spaceTypeBus;
                                    space.Name = "Bus-" + busCounter;
                                    busCounter++;
                                    break;
                                case "Street":
                                    space.SpaceType = spaceTypeStreet;
                                    space.Name = "Street-" + streetCounter;
                                    streetCounter++;
                                    break;
                                case "Clear":
                                    space.SpaceType = spaceTypeClear;
                                    space.Name = "Clear-" + clearCounter;
                                    clearCounter++;
                                    break;
                            }
                            break;
                        case "12,17":
                            switch (hidden258.Value)
                            {
                                case "Car":
                                    space.SpaceType = spaceTypeCar;
                                    space.Name = "Car-" + carcounter;
                                    carcounter++;
                                    break;
                                case "Motorcycle":
                                    space.SpaceType = spaceTypeMoto;
                                    space.Name = "Motorcycle-" + motoCounter;
                                    motoCounter++;
                                    break;
                                case "Handicap":
                                    space.SpaceType = spaceTypeHandicap;
                                    space.Name = "Handicap-" + handicapCounter;
                                    handicapCounter++;
                                    break;
                                case "Bus":
                                    space.SpaceType = spaceTypeBus;
                                    space.Name = "Bus-" + busCounter;
                                    busCounter++;
                                    break;
                                case "Street":
                                    space.SpaceType = spaceTypeStreet;
                                    space.Name = "Street-" + streetCounter;
                                    streetCounter++;
                                    break;
                                case "Clear":
                                    space.SpaceType = spaceTypeClear;
                                    space.Name = "Clear-" + clearCounter;
                                    clearCounter++;
                                    break;
                            }
                            break;
                        case "12,18":
                            switch (hidden259.Value)
                            {
                                case "Car":
                                    space.SpaceType = spaceTypeCar;
                                    space.Name = "Car-" + carcounter;
                                    carcounter++;
                                    break;
                                case "Motorcycle":
                                    space.SpaceType = spaceTypeMoto;
                                    space.Name = "Motorcycle-" + motoCounter;
                                    motoCounter++;
                                    break;
                                case "Handicap":
                                    space.SpaceType = spaceTypeHandicap;
                                    space.Name = "Handicap-" + handicapCounter;
                                    handicapCounter++;
                                    break;
                                case "Bus":
                                    space.SpaceType = spaceTypeBus;
                                    space.Name = "Bus-" + busCounter;
                                    busCounter++;
                                    break;
                                case "Street":
                                    space.SpaceType = spaceTypeStreet;
                                    space.Name = "Street-" + streetCounter;
                                    streetCounter++;
                                    break;
                                case "Clear":
                                    space.SpaceType = spaceTypeClear;
                                    space.Name = "Clear-" + clearCounter;
                                    clearCounter++;
                                    break;
                            }
                            break;
                        case "12,19":
                            switch (hidden260.Value)
                            {
                                case "Car":
                                    space.SpaceType = spaceTypeCar;
                                    space.Name = "Car-" + carcounter;
                                    carcounter++;
                                    break;
                                case "Motorcycle":
                                    space.SpaceType = spaceTypeMoto;
                                    space.Name = "Motorcycle-" + motoCounter;
                                    motoCounter++;
                                    break;
                                case "Handicap":
                                    space.SpaceType = spaceTypeHandicap;
                                    space.Name = "Handicap-" + handicapCounter;
                                    handicapCounter++;
                                    break;
                                case "Bus":
                                    space.SpaceType = spaceTypeBus;
                                    space.Name = "Bus-" + busCounter;
                                    busCounter++;
                                    break;
                                case "Street":
                                    space.SpaceType = spaceTypeStreet;
                                    space.Name = "Street-" + streetCounter;
                                    streetCounter++;
                                    break;
                                case "Clear":
                                    space.SpaceType = spaceTypeClear;
                                    space.Name = "Clear-" + clearCounter;
                                    clearCounter++;
                                    break;
                            }
                            break;

                        case "13,0":
                            switch (hidden261.Value)
                            {
                                case "Car":
                                    space.SpaceType = spaceTypeCar;
                                    space.Name = "Car-" + carcounter;
                                    carcounter++;
                                    break;
                                case "Motorcycle":
                                    space.SpaceType = spaceTypeMoto;
                                    space.Name = "Motorcycle-" + motoCounter;
                                    motoCounter++;
                                    break;
                                case "Handicap":
                                    space.SpaceType = spaceTypeHandicap;
                                    space.Name = "Handicap-" + handicapCounter;
                                    handicapCounter++;
                                    break;
                                case "Bus":
                                    space.SpaceType = spaceTypeBus;
                                    space.Name = "Bus-" + busCounter;
                                    busCounter++;
                                    break;
                                case "Street":
                                    space.SpaceType = spaceTypeStreet;
                                    space.Name = "Street-" + streetCounter;
                                    streetCounter++;
                                    break;
                                case "Clear":
                                    space.SpaceType = spaceTypeClear;
                                    space.Name = "Clear-" + clearCounter;
                                    clearCounter++;
                                    break;
                            }
                            break;
                        case "13,1":
                            switch (hidden262.Value)
                            {
                                case "Car":
                                    space.SpaceType = spaceTypeCar;
                                    space.Name = "Car-" + carcounter;
                                    carcounter++;
                                    break;
                                case "Motorcycle":
                                    space.SpaceType = spaceTypeMoto;
                                    space.Name = "Motorcycle-" + motoCounter;
                                    motoCounter++;
                                    break;
                                case "Handicap":
                                    space.SpaceType = spaceTypeHandicap;
                                    space.Name = "Handicap-" + handicapCounter;
                                    handicapCounter++;
                                    break;
                                case "Bus":
                                    space.SpaceType = spaceTypeBus;
                                    space.Name = "Bus-" + busCounter;
                                    busCounter++;
                                    break;
                                case "Street":
                                    space.SpaceType = spaceTypeStreet;
                                    space.Name = "Street-" + streetCounter;
                                    streetCounter++;
                                    break;
                                case "Clear":
                                    space.SpaceType = spaceTypeClear;
                                    space.Name = "Clear-" + clearCounter;
                                    clearCounter++;
                                    break;
                            }
                            break;
                        case "13,2":
                            switch (hidden263.Value)
                            {
                                case "Car":
                                    space.SpaceType = spaceTypeCar;
                                    space.Name = "Car-" + carcounter;
                                    carcounter++;
                                    break;
                                case "Motorcycle":
                                    space.SpaceType = spaceTypeMoto;
                                    space.Name = "Motorcycle-" + motoCounter;
                                    motoCounter++;
                                    break;
                                case "Handicap":
                                    space.SpaceType = spaceTypeHandicap;
                                    space.Name = "Handicap-" + handicapCounter;
                                    handicapCounter++;
                                    break;
                                case "Bus":
                                    space.SpaceType = spaceTypeBus;
                                    space.Name = "Bus-" + busCounter;
                                    busCounter++;
                                    break;
                                case "Street":
                                    space.SpaceType = spaceTypeStreet;
                                    space.Name = "Street-" + streetCounter;
                                    streetCounter++;
                                    break;
                                case "Clear":
                                    space.SpaceType = spaceTypeClear;
                                    space.Name = "Clear-" + clearCounter;
                                    clearCounter++;
                                    break;
                            }
                            break;
                        case "13,3":
                            switch (hidden264.Value)
                            {
                                case "Car":
                                    space.SpaceType = spaceTypeCar;
                                    space.Name = "Car-" + carcounter;
                                    carcounter++;
                                    break;
                                case "Motorcycle":
                                    space.SpaceType = spaceTypeMoto;
                                    space.Name = "Motorcycle-" + motoCounter;
                                    motoCounter++;
                                    break;
                                case "Handicap":
                                    space.SpaceType = spaceTypeHandicap;
                                    space.Name = "Handicap-" + handicapCounter;
                                    handicapCounter++;
                                    break;
                                case "Bus":
                                    space.SpaceType = spaceTypeBus;
                                    space.Name = "Bus-" + busCounter;
                                    busCounter++;
                                    break;
                                case "Street":
                                    space.SpaceType = spaceTypeStreet;
                                    space.Name = "Street-" + streetCounter;
                                    streetCounter++;
                                    break;
                                case "Clear":
                                    space.SpaceType = spaceTypeClear;
                                    space.Name = "Clear-" + clearCounter;
                                    clearCounter++;
                                    break;
                            }
                            break;
                        case "13,4":
                            switch (hidden265.Value)
                            {
                                case "Car":
                                    space.SpaceType = spaceTypeCar;
                                    space.Name = "Car-" + carcounter;
                                    carcounter++;
                                    break;
                                case "Motorcycle":
                                    space.SpaceType = spaceTypeMoto;
                                    space.Name = "Motorcycle-" + motoCounter;
                                    motoCounter++;
                                    break;
                                case "Handicap":
                                    space.SpaceType = spaceTypeHandicap;
                                    space.Name = "Handicap-" + handicapCounter;
                                    handicapCounter++;
                                    break;
                                case "Bus":
                                    space.SpaceType = spaceTypeBus;
                                    space.Name = "Bus-" + busCounter;
                                    busCounter++;
                                    break;
                                case "Street":
                                    space.SpaceType = spaceTypeStreet;
                                    space.Name = "Street-" + streetCounter;
                                    streetCounter++;
                                    break;
                                case "Clear":
                                    space.SpaceType = spaceTypeClear;
                                    space.Name = "Clear-" + clearCounter;
                                    clearCounter++;
                                    break;
                            }
                            break;
                        case "13,5":
                            switch (hidden266.Value)
                            {
                                case "Car":
                                    space.SpaceType = spaceTypeCar;
                                    space.Name = "Car-" + carcounter;
                                    carcounter++;
                                    break;
                                case "Motorcycle":
                                    space.SpaceType = spaceTypeMoto;
                                    space.Name = "Motorcycle-" + motoCounter;
                                    motoCounter++;
                                    break;
                                case "Handicap":
                                    space.SpaceType = spaceTypeHandicap;
                                    space.Name = "Handicap-" + handicapCounter;
                                    handicapCounter++;
                                    break;
                                case "Bus":
                                    space.SpaceType = spaceTypeBus;
                                    space.Name = "Bus-" + busCounter;
                                    busCounter++;
                                    break;
                                case "Street":
                                    space.SpaceType = spaceTypeStreet;
                                    space.Name = "Street-" + streetCounter;
                                    streetCounter++;
                                    break;
                                case "Clear":
                                    space.SpaceType = spaceTypeClear;
                                    space.Name = "Clear-" + clearCounter;
                                    clearCounter++;
                                    break;
                            }
                            break;
                        case "13,6":
                            switch (hidden267.Value)
                            {
                                case "Car":
                                    space.SpaceType = spaceTypeCar;
                                    space.Name = "Car-" + carcounter;
                                    carcounter++;
                                    break;
                                case "Motorcycle":
                                    space.SpaceType = spaceTypeMoto;
                                    space.Name = "Motorcycle-" + motoCounter;
                                    motoCounter++;
                                    break;
                                case "Handicap":
                                    space.SpaceType = spaceTypeHandicap;
                                    space.Name = "Handicap-" + handicapCounter;
                                    handicapCounter++;
                                    break;
                                case "Bus":
                                    space.SpaceType = spaceTypeBus;
                                    space.Name = "Bus-" + busCounter;
                                    busCounter++;
                                    break;
                                case "Street":
                                    space.SpaceType = spaceTypeStreet;
                                    space.Name = "Street-" + streetCounter;
                                    streetCounter++;
                                    break;
                                case "Clear":
                                    space.SpaceType = spaceTypeClear;
                                    space.Name = "Clear-" + clearCounter;
                                    clearCounter++;
                                    break;
                            }
                            break;
                        case "13,7":
                            switch (hidden268.Value)
                            {
                                case "Car":
                                    space.SpaceType = spaceTypeCar;
                                    space.Name = "Car-" + carcounter;
                                    carcounter++;
                                    break;
                                case "Motorcycle":
                                    space.SpaceType = spaceTypeMoto;
                                    space.Name = "Motorcycle-" + motoCounter;
                                    motoCounter++;
                                    break;
                                case "Handicap":
                                    space.SpaceType = spaceTypeHandicap;
                                    space.Name = "Handicap-" + handicapCounter;
                                    handicapCounter++;
                                    break;
                                case "Bus":
                                    space.SpaceType = spaceTypeBus;
                                    space.Name = "Bus-" + busCounter;
                                    busCounter++;
                                    break;
                                case "Street":
                                    space.SpaceType = spaceTypeStreet;
                                    space.Name = "Street-" + streetCounter;
                                    streetCounter++;
                                    break;
                                case "Clear":
                                    space.SpaceType = spaceTypeClear;
                                    space.Name = "Clear-" + clearCounter;
                                    clearCounter++;
                                    break;
                            }
                            break;
                        case "13,8":
                            switch (hidden269.Value)
                            {
                                case "Car":
                                    space.SpaceType = spaceTypeCar;
                                    space.Name = "Car-" + carcounter;
                                    carcounter++;
                                    break;
                                case "Motorcycle":
                                    space.SpaceType = spaceTypeMoto;
                                    space.Name = "Motorcycle-" + motoCounter;
                                    motoCounter++;
                                    break;
                                case "Handicap":
                                    space.SpaceType = spaceTypeHandicap;
                                    space.Name = "Handicap-" + handicapCounter;
                                    handicapCounter++;
                                    break;
                                case "Bus":
                                    space.SpaceType = spaceTypeBus;
                                    space.Name = "Bus-" + busCounter;
                                    busCounter++;
                                    break;
                                case "Street":
                                    space.SpaceType = spaceTypeStreet;
                                    space.Name = "Street-" + streetCounter;
                                    streetCounter++;
                                    break;
                                case "Clear":
                                    space.SpaceType = spaceTypeClear;
                                    space.Name = "Clear-" + clearCounter;
                                    clearCounter++;
                                    break;
                            }
                            break;
                        case "13,9":
                            switch (hidden270.Value)
                            {
                                case "Car":
                                    space.SpaceType = spaceTypeCar;
                                    space.Name = "Car-" + carcounter;
                                    carcounter++;
                                    break;
                                case "Motorcycle":
                                    space.SpaceType = spaceTypeMoto;
                                    space.Name = "Motorcycle-" + motoCounter;
                                    motoCounter++;
                                    break;
                                case "Handicap":
                                    space.SpaceType = spaceTypeHandicap;
                                    space.Name = "Handicap-" + handicapCounter;
                                    handicapCounter++;
                                    break;
                                case "Bus":
                                    space.SpaceType = spaceTypeBus;
                                    space.Name = "Bus-" + busCounter;
                                    busCounter++;
                                    break;
                                case "Street":
                                    space.SpaceType = spaceTypeStreet;
                                    space.Name = "Street-" + streetCounter;
                                    streetCounter++;
                                    break;
                                case "Clear":
                                    space.SpaceType = spaceTypeClear;
                                    space.Name = "Clear-" + clearCounter;
                                    clearCounter++;
                                    break;
                            }
                            break;
                        case "13,10":
                            switch (hidden271.Value)
                            {
                                case "Car":
                                    space.SpaceType = spaceTypeCar;
                                    space.Name = "Car-" + carcounter;
                                    carcounter++;
                                    break;
                                case "Motorcycle":
                                    space.SpaceType = spaceTypeMoto;
                                    space.Name = "Motorcycle-" + motoCounter;
                                    motoCounter++;
                                    break;
                                case "Handicap":
                                    space.SpaceType = spaceTypeHandicap;
                                    space.Name = "Handicap-" + handicapCounter;
                                    handicapCounter++;
                                    break;
                                case "Bus":
                                    space.SpaceType = spaceTypeBus;
                                    space.Name = "Bus-" + busCounter;
                                    busCounter++;
                                    break;
                                case "Street":
                                    space.SpaceType = spaceTypeStreet;
                                    space.Name = "Street-" + streetCounter;
                                    streetCounter++;
                                    break;
                                case "Clear":
                                    space.SpaceType = spaceTypeClear;
                                    space.Name = "Clear-" + clearCounter;
                                    clearCounter++;
                                    break;
                            }
                            break;
                        case "13,11":
                            switch (hidden272.Value)
                            {
                                case "Car":
                                    space.SpaceType = spaceTypeCar;
                                    space.Name = "Car-" + carcounter;
                                    carcounter++;
                                    break;
                                case "Motorcycle":
                                    space.SpaceType = spaceTypeMoto;
                                    space.Name = "Motorcycle-" + motoCounter;
                                    motoCounter++;
                                    break;
                                case "Handicap":
                                    space.SpaceType = spaceTypeHandicap;
                                    space.Name = "Handicap-" + handicapCounter;
                                    handicapCounter++;
                                    break;
                                case "Bus":
                                    space.SpaceType = spaceTypeBus;
                                    space.Name = "Bus-" + busCounter;
                                    busCounter++;
                                    break;
                                case "Street":
                                    space.SpaceType = spaceTypeStreet;
                                    space.Name = "Street-" + streetCounter;
                                    streetCounter++;
                                    break;
                                case "Clear":
                                    space.SpaceType = spaceTypeClear;
                                    space.Name = "Clear-" + clearCounter;
                                    clearCounter++;
                                    break;
                            }
                            break;
                        case "13,12":
                            switch (hidden273.Value)
                            {
                                case "Car":
                                    space.SpaceType = spaceTypeCar;
                                    space.Name = "Car-" + carcounter;
                                    carcounter++;
                                    break;
                                case "Motorcycle":
                                    space.SpaceType = spaceTypeMoto;
                                    space.Name = "Motorcycle-" + motoCounter;
                                    motoCounter++;
                                    break;
                                case "Handicap":
                                    space.SpaceType = spaceTypeHandicap;
                                    space.Name = "Handicap-" + handicapCounter;
                                    handicapCounter++;
                                    break;
                                case "Bus":
                                    space.SpaceType = spaceTypeBus;
                                    space.Name = "Bus-" + busCounter;
                                    busCounter++;
                                    break;
                                case "Street":
                                    space.SpaceType = spaceTypeStreet;
                                    space.Name = "Street-" + streetCounter;
                                    streetCounter++;
                                    break;
                                case "Clear":
                                    space.SpaceType = spaceTypeClear;
                                    space.Name = "Clear-" + clearCounter;
                                    clearCounter++;
                                    break;
                            }
                            break;
                        case "13,13":
                            switch (hidden274.Value)
                            {
                                case "Car":
                                    space.SpaceType = spaceTypeCar;
                                    space.Name = "Car-" + carcounter;
                                    carcounter++;
                                    break;
                                case "Motorcycle":
                                    space.SpaceType = spaceTypeMoto;
                                    space.Name = "Motorcycle-" + motoCounter;
                                    motoCounter++;
                                    break;
                                case "Handicap":
                                    space.SpaceType = spaceTypeHandicap;
                                    space.Name = "Handicap-" + handicapCounter;
                                    handicapCounter++;
                                    break;
                                case "Bus":
                                    space.SpaceType = spaceTypeBus;
                                    space.Name = "Bus-" + busCounter;
                                    busCounter++;
                                    break;
                                case "Street":
                                    space.SpaceType = spaceTypeStreet;
                                    space.Name = "Street-" + streetCounter;
                                    streetCounter++;
                                    break;
                                case "Clear":
                                    space.SpaceType = spaceTypeClear;
                                    space.Name = "Clear-" + clearCounter;
                                    clearCounter++;
                                    break;
                            }
                            break;
                        case "13,14":
                            switch (hidden275.Value)
                            {
                                case "Car":
                                    space.SpaceType = spaceTypeCar;
                                    space.Name = "Car-" + carcounter;
                                    carcounter++;
                                    break;
                                case "Motorcycle":
                                    space.SpaceType = spaceTypeMoto;
                                    space.Name = "Motorcycle-" + motoCounter;
                                    motoCounter++;
                                    break;
                                case "Handicap":
                                    space.SpaceType = spaceTypeHandicap;
                                    space.Name = "Handicap-" + handicapCounter;
                                    handicapCounter++;
                                    break;
                                case "Bus":
                                    space.SpaceType = spaceTypeBus;
                                    space.Name = "Bus-" + busCounter;
                                    busCounter++;
                                    break;
                                case "Street":
                                    space.SpaceType = spaceTypeStreet;
                                    space.Name = "Street-" + streetCounter;
                                    streetCounter++;
                                    break;
                                case "Clear":
                                    space.SpaceType = spaceTypeClear;
                                    space.Name = "Clear-" + clearCounter;
                                    clearCounter++;
                                    break;
                            }
                            break;
                        case "13,15":
                            switch (hidden276.Value)
                            {
                                case "Car":
                                    space.SpaceType = spaceTypeCar;
                                    space.Name = "Car-" + carcounter;
                                    carcounter++;
                                    break;
                                case "Motorcycle":
                                    space.SpaceType = spaceTypeMoto;
                                    space.Name = "Motorcycle-" + motoCounter;
                                    motoCounter++;
                                    break;
                                case "Handicap":
                                    space.SpaceType = spaceTypeHandicap;
                                    space.Name = "Handicap-" + handicapCounter;
                                    handicapCounter++;
                                    break;
                                case "Bus":
                                    space.SpaceType = spaceTypeBus;
                                    space.Name = "Bus-" + busCounter;
                                    busCounter++;
                                    break;
                                case "Street":
                                    space.SpaceType = spaceTypeStreet;
                                    space.Name = "Street-" + streetCounter;
                                    streetCounter++;
                                    break;
                                case "Clear":
                                    space.SpaceType = spaceTypeClear;
                                    space.Name = "Clear-" + clearCounter;
                                    clearCounter++;
                                    break;
                            }
                            break;
                        case "13,16":
                            switch (hidden277.Value)
                            {
                                case "Car":
                                    space.SpaceType = spaceTypeCar;
                                    space.Name = "Car-" + carcounter;
                                    carcounter++;
                                    break;
                                case "Motorcycle":
                                    space.SpaceType = spaceTypeMoto;
                                    space.Name = "Motorcycle-" + motoCounter;
                                    motoCounter++;
                                    break;
                                case "Handicap":
                                    space.SpaceType = spaceTypeHandicap;
                                    space.Name = "Handicap-" + handicapCounter;
                                    handicapCounter++;
                                    break;
                                case "Bus":
                                    space.SpaceType = spaceTypeBus;
                                    space.Name = "Bus-" + busCounter;
                                    busCounter++;
                                    break;
                                case "Street":
                                    space.SpaceType = spaceTypeStreet;
                                    space.Name = "Street-" + streetCounter;
                                    streetCounter++;
                                    break;
                                case "Clear":
                                    space.SpaceType = spaceTypeClear;
                                    space.Name = "Clear-" + clearCounter;
                                    clearCounter++;
                                    break;
                            }
                            break;
                        case "13,17":
                            switch (hidden278.Value)
                            {
                                case "Car":
                                    space.SpaceType = spaceTypeCar;
                                    space.Name = "Car-" + carcounter;
                                    carcounter++;
                                    break;
                                case "Motorcycle":
                                    space.SpaceType = spaceTypeMoto;
                                    space.Name = "Motorcycle-" + motoCounter;
                                    motoCounter++;
                                    break;
                                case "Handicap":
                                    space.SpaceType = spaceTypeHandicap;
                                    space.Name = "Handicap-" + handicapCounter;
                                    handicapCounter++;
                                    break;
                                case "Bus":
                                    space.SpaceType = spaceTypeBus;
                                    space.Name = "Bus-" + busCounter;
                                    busCounter++;
                                    break;
                                case "Street":
                                    space.SpaceType = spaceTypeStreet;
                                    space.Name = "Street-" + streetCounter;
                                    streetCounter++;
                                    break;
                                case "Clear":
                                    space.SpaceType = spaceTypeClear;
                                    space.Name = "Clear-" + clearCounter;
                                    clearCounter++;
                                    break;
                            }
                            break;
                        case "13,18":
                            switch (hidden279.Value)
                            {
                                case "Car":
                                    space.SpaceType = spaceTypeCar;
                                    space.Name = "Car-" + carcounter;
                                    carcounter++;
                                    break;
                                case "Motorcycle":
                                    space.SpaceType = spaceTypeMoto;
                                    space.Name = "Motorcycle-" + motoCounter;
                                    motoCounter++;
                                    break;
                                case "Handicap":
                                    space.SpaceType = spaceTypeHandicap;
                                    space.Name = "Handicap-" + handicapCounter;
                                    handicapCounter++;
                                    break;
                                case "Bus":
                                    space.SpaceType = spaceTypeBus;
                                    space.Name = "Bus-" + busCounter;
                                    busCounter++;
                                    break;
                                case "Street":
                                    space.SpaceType = spaceTypeStreet;
                                    space.Name = "Street-" + streetCounter;
                                    streetCounter++;
                                    break;
                                case "Clear":
                                    space.SpaceType = spaceTypeClear;
                                    space.Name = "Clear-" + clearCounter;
                                    clearCounter++;
                                    break;
                            }
                            break;
                        case "13,19":
                            switch (hidden280.Value)
                            {
                                case "Car":
                                    space.SpaceType = spaceTypeCar;
                                    space.Name = "Car-" + carcounter;
                                    carcounter++;
                                    break;
                                case "Motorcycle":
                                    space.SpaceType = spaceTypeMoto;
                                    space.Name = "Motorcycle-" + motoCounter;
                                    motoCounter++;
                                    break;
                                case "Handicap":
                                    space.SpaceType = spaceTypeHandicap;
                                    space.Name = "Handicap-" + handicapCounter;
                                    handicapCounter++;
                                    break;
                                case "Bus":
                                    space.SpaceType = spaceTypeBus;
                                    space.Name = "Bus-" + busCounter;
                                    busCounter++;
                                    break;
                                case "Street":
                                    space.SpaceType = spaceTypeStreet;
                                    space.Name = "Street-" + streetCounter;
                                    streetCounter++;
                                    break;
                                case "Clear":
                                    space.SpaceType = spaceTypeClear;
                                    space.Name = "Clear-" + clearCounter;
                                    clearCounter++;
                                    break;
                            }
                            break;

                        case "14,0":
                            switch (hidden281.Value)
                            {
                                case "Car":
                                    space.SpaceType = spaceTypeCar;
                                    space.Name = "Car-" + carcounter;
                                    carcounter++;
                                    break;
                                case "Motorcycle":
                                    space.SpaceType = spaceTypeMoto;
                                    space.Name = "Motorcycle-" + motoCounter;
                                    motoCounter++;
                                    break;
                                case "Handicap":
                                    space.SpaceType = spaceTypeHandicap;
                                    space.Name = "Handicap-" + handicapCounter;
                                    handicapCounter++;
                                    break;
                                case "Bus":
                                    space.SpaceType = spaceTypeBus;
                                    space.Name = "Bus-" + busCounter;
                                    busCounter++;
                                    break;
                                case "Street":
                                    space.SpaceType = spaceTypeStreet;
                                    space.Name = "Street-" + streetCounter;
                                    streetCounter++;
                                    break;
                                case "Clear":
                                    space.SpaceType = spaceTypeClear;
                                    space.Name = "Clear-" + clearCounter;
                                    clearCounter++;
                                    break;
                            }
                            break;
                        case "14,1":
                            switch (hidden282.Value)
                            {
                                case "Car":
                                    space.SpaceType = spaceTypeCar;
                                    space.Name = "Car-" + carcounter;
                                    carcounter++;
                                    break;
                                case "Motorcycle":
                                    space.SpaceType = spaceTypeMoto;
                                    space.Name = "Motorcycle-" + motoCounter;
                                    motoCounter++;
                                    break;
                                case "Handicap":
                                    space.SpaceType = spaceTypeHandicap;
                                    space.Name = "Handicap-" + handicapCounter;
                                    handicapCounter++;
                                    break;
                                case "Bus":
                                    space.SpaceType = spaceTypeBus;
                                    space.Name = "Bus-" + busCounter;
                                    busCounter++;
                                    break;
                                case "Street":
                                    space.SpaceType = spaceTypeStreet;
                                    space.Name = "Street-" + streetCounter;
                                    streetCounter++;
                                    break;
                                case "Clear":
                                    space.SpaceType = spaceTypeClear;
                                    space.Name = "Clear-" + clearCounter;
                                    clearCounter++;
                                    break;
                            }
                            break;
                        case "14,2":
                            switch (hidden283.Value)
                            {
                                case "Car":
                                    space.SpaceType = spaceTypeCar;
                                    space.Name = "Car-" + carcounter;
                                    carcounter++;
                                    break;
                                case "Motorcycle":
                                    space.SpaceType = spaceTypeMoto;
                                    space.Name = "Motorcycle-" + motoCounter;
                                    motoCounter++;
                                    break;
                                case "Handicap":
                                    space.SpaceType = spaceTypeHandicap;
                                    space.Name = "Handicap-" + handicapCounter;
                                    handicapCounter++;
                                    break;
                                case "Bus":
                                    space.SpaceType = spaceTypeBus;
                                    space.Name = "Bus-" + busCounter;
                                    busCounter++;
                                    break;
                                case "Street":
                                    space.SpaceType = spaceTypeStreet;
                                    space.Name = "Street-" + streetCounter;
                                    streetCounter++;
                                    break;
                                case "Clear":
                                    space.SpaceType = spaceTypeClear;
                                    space.Name = "Clear-" + clearCounter;
                                    clearCounter++;
                                    break;
                            }
                            break;
                        case "14,3":
                            switch (hidden284.Value)
                            {
                                case "Car":
                                    space.SpaceType = spaceTypeCar;
                                    space.Name = "Car-" + carcounter;
                                    carcounter++;
                                    break;
                                case "Motorcycle":
                                    space.SpaceType = spaceTypeMoto;
                                    space.Name = "Motorcycle-" + motoCounter;
                                    motoCounter++;
                                    break;
                                case "Handicap":
                                    space.SpaceType = spaceTypeHandicap;
                                    space.Name = "Handicap-" + handicapCounter;
                                    handicapCounter++;
                                    break;
                                case "Bus":
                                    space.SpaceType = spaceTypeBus;
                                    space.Name = "Bus-" + busCounter;
                                    busCounter++;
                                    break;
                                case "Street":
                                    space.SpaceType = spaceTypeStreet;
                                    space.Name = "Street-" + streetCounter;
                                    streetCounter++;
                                    break;
                                case "Clear":
                                    space.SpaceType = spaceTypeClear;
                                    space.Name = "Clear-" + clearCounter;
                                    clearCounter++;
                                    break;
                            }
                            break;
                        case "14,4":
                            switch (hidden285.Value)
                            {
                                case "Car":
                                    space.SpaceType = spaceTypeCar;
                                    space.Name = "Car-" + carcounter;
                                    carcounter++;
                                    break;
                                case "Motorcycle":
                                    space.SpaceType = spaceTypeMoto;
                                    space.Name = "Motorcycle-" + motoCounter;
                                    motoCounter++;
                                    break;
                                case "Handicap":
                                    space.SpaceType = spaceTypeHandicap;
                                    space.Name = "Handicap-" + handicapCounter;
                                    handicapCounter++;
                                    break;
                                case "Bus":
                                    space.SpaceType = spaceTypeBus;
                                    space.Name = "Bus-" + busCounter;
                                    busCounter++;
                                    break;
                                case "Street":
                                    space.SpaceType = spaceTypeStreet;
                                    space.Name = "Street-" + streetCounter;
                                    streetCounter++;
                                    break;
                                case "Clear":
                                    space.SpaceType = spaceTypeClear;
                                    space.Name = "Clear-" + clearCounter;
                                    clearCounter++;
                                    break;
                            }
                            break;
                        case "14,5":
                            switch (hidden286.Value)
                            {
                                case "Car":
                                    space.SpaceType = spaceTypeCar;
                                    space.Name = "Car-" + carcounter;
                                    carcounter++;
                                    break;
                                case "Motorcycle":
                                    space.SpaceType = spaceTypeMoto;
                                    space.Name = "Motorcycle-" + motoCounter;
                                    motoCounter++;
                                    break;
                                case "Handicap":
                                    space.SpaceType = spaceTypeHandicap;
                                    space.Name = "Handicap-" + handicapCounter;
                                    handicapCounter++;
                                    break;
                                case "Bus":
                                    space.SpaceType = spaceTypeBus;
                                    space.Name = "Bus-" + busCounter;
                                    busCounter++;
                                    break;
                                case "Street":
                                    space.SpaceType = spaceTypeStreet;
                                    space.Name = "Street-" + streetCounter;
                                    streetCounter++;
                                    break;
                                case "Clear":
                                    space.SpaceType = spaceTypeClear;
                                    space.Name = "Clear-" + clearCounter;
                                    clearCounter++;
                                    break;
                            }
                            break;
                        case "14,6":
                            switch (hidden287.Value)
                            {
                                case "Car":
                                    space.SpaceType = spaceTypeCar;
                                    space.Name = "Car-" + carcounter;
                                    carcounter++;
                                    break;
                                case "Motorcycle":
                                    space.SpaceType = spaceTypeMoto;
                                    space.Name = "Motorcycle-" + motoCounter;
                                    motoCounter++;
                                    break;
                                case "Handicap":
                                    space.SpaceType = spaceTypeHandicap;
                                    space.Name = "Handicap-" + handicapCounter;
                                    handicapCounter++;
                                    break;
                                case "Bus":
                                    space.SpaceType = spaceTypeBus;
                                    space.Name = "Bus-" + busCounter;
                                    busCounter++;
                                    break;
                                case "Street":
                                    space.SpaceType = spaceTypeStreet;
                                    space.Name = "Street-" + streetCounter;
                                    streetCounter++;
                                    break;
                                case "Clear":
                                    space.SpaceType = spaceTypeClear;
                                    space.Name = "Clear-" + clearCounter;
                                    clearCounter++;
                                    break;
                            }
                            break;
                        case "14,7":
                            switch (hidden288.Value)
                            {
                                case "Car":
                                    space.SpaceType = spaceTypeCar;
                                    space.Name = "Car-" + carcounter;
                                    carcounter++;
                                    break;
                                case "Motorcycle":
                                    space.SpaceType = spaceTypeMoto;
                                    space.Name = "Motorcycle-" + motoCounter;
                                    motoCounter++;
                                    break;
                                case "Handicap":
                                    space.SpaceType = spaceTypeHandicap;
                                    space.Name = "Handicap-" + handicapCounter;
                                    handicapCounter++;
                                    break;
                                case "Bus":
                                    space.SpaceType = spaceTypeBus;
                                    space.Name = "Bus-" + busCounter;
                                    busCounter++;
                                    break;
                                case "Street":
                                    space.SpaceType = spaceTypeStreet;
                                    space.Name = "Street-" + streetCounter;
                                    streetCounter++;
                                    break;
                                case "Clear":
                                    space.SpaceType = spaceTypeClear;
                                    space.Name = "Clear-" + clearCounter;
                                    clearCounter++;
                                    break;
                            }
                            break;
                        case "14,8":
                            switch (hidden289.Value)
                            {
                                case "Car":
                                    space.SpaceType = spaceTypeCar;
                                    space.Name = "Car-" + carcounter;
                                    carcounter++;
                                    break;
                                case "Motorcycle":
                                    space.SpaceType = spaceTypeMoto;
                                    space.Name = "Motorcycle-" + motoCounter;
                                    motoCounter++;
                                    break;
                                case "Handicap":
                                    space.SpaceType = spaceTypeHandicap;
                                    space.Name = "Handicap-" + handicapCounter;
                                    handicapCounter++;
                                    break;
                                case "Bus":
                                    space.SpaceType = spaceTypeBus;
                                    space.Name = "Bus-" + busCounter;
                                    busCounter++;
                                    break;
                                case "Street":
                                    space.SpaceType = spaceTypeStreet;
                                    space.Name = "Street-" + streetCounter;
                                    streetCounter++;
                                    break;
                                case "Clear":
                                    space.SpaceType = spaceTypeClear;
                                    space.Name = "Clear-" + clearCounter;
                                    clearCounter++;
                                    break;
                            }
                            break;
                        case "14,9":
                            switch (hidden290.Value)
                            {
                                case "Car":
                                    space.SpaceType = spaceTypeCar;
                                    space.Name = "Car-" + carcounter;
                                    carcounter++;
                                    break;
                                case "Motorcycle":
                                    space.SpaceType = spaceTypeMoto;
                                    space.Name = "Motorcycle-" + motoCounter;
                                    motoCounter++;
                                    break;
                                case "Handicap":
                                    space.SpaceType = spaceTypeHandicap;
                                    space.Name = "Handicap-" + handicapCounter;
                                    handicapCounter++;
                                    break;
                                case "Bus":
                                    space.SpaceType = spaceTypeBus;
                                    space.Name = "Bus-" + busCounter;
                                    busCounter++;
                                    break;
                                case "Street":
                                    space.SpaceType = spaceTypeStreet;
                                    space.Name = "Street-" + streetCounter;
                                    streetCounter++;
                                    break;
                                case "Clear":
                                    space.SpaceType = spaceTypeClear;
                                    space.Name = "Clear-" + clearCounter;
                                    clearCounter++;
                                    break;
                            }
                            break;
                        case "14,10":
                            switch (hidden291.Value)
                            {
                                case "Car":
                                    space.SpaceType = spaceTypeCar;
                                    space.Name = "Car-" + carcounter;
                                    carcounter++;
                                    break;
                                case "Motorcycle":
                                    space.SpaceType = spaceTypeMoto;
                                    space.Name = "Motorcycle-" + motoCounter;
                                    motoCounter++;
                                    break;
                                case "Handicap":
                                    space.SpaceType = spaceTypeHandicap;
                                    space.Name = "Handicap-" + handicapCounter;
                                    handicapCounter++;
                                    break;
                                case "Bus":
                                    space.SpaceType = spaceTypeBus;
                                    space.Name = "Bus-" + busCounter;
                                    busCounter++;
                                    break;
                                case "Street":
                                    space.SpaceType = spaceTypeStreet;
                                    space.Name = "Street-" + streetCounter;
                                    streetCounter++;
                                    break;
                                case "Clear":
                                    space.SpaceType = spaceTypeClear;
                                    space.Name = "Clear-" + clearCounter;
                                    clearCounter++;
                                    break;
                            }
                            break;
                        case "14,11":
                            switch (hidden292.Value)
                            {
                                case "Car":
                                    space.SpaceType = spaceTypeCar;
                                    space.Name = "Car-" + carcounter;
                                    carcounter++;
                                    break;
                                case "Motorcycle":
                                    space.SpaceType = spaceTypeMoto;
                                    space.Name = "Motorcycle-" + motoCounter;
                                    motoCounter++;
                                    break;
                                case "Handicap":
                                    space.SpaceType = spaceTypeHandicap;
                                    space.Name = "Handicap-" + handicapCounter;
                                    handicapCounter++;
                                    break;
                                case "Bus":
                                    space.SpaceType = spaceTypeBus;
                                    space.Name = "Bus-" + busCounter;
                                    busCounter++;
                                    break;
                                case "Street":
                                    space.SpaceType = spaceTypeStreet;
                                    space.Name = "Street-" + streetCounter;
                                    streetCounter++;
                                    break;
                                case "Clear":
                                    space.SpaceType = spaceTypeClear;
                                    space.Name = "Clear-" + clearCounter;
                                    clearCounter++;
                                    break;
                            }
                            break;
                        case "14,12":
                            switch (hidden293.Value)
                            {
                                case "Car":
                                    space.SpaceType = spaceTypeCar;
                                    space.Name = "Car-" + carcounter;
                                    carcounter++;
                                    break;
                                case "Motorcycle":
                                    space.SpaceType = spaceTypeMoto;
                                    space.Name = "Motorcycle-" + motoCounter;
                                    motoCounter++;
                                    break;
                                case "Handicap":
                                    space.SpaceType = spaceTypeHandicap;
                                    space.Name = "Handicap-" + handicapCounter;
                                    handicapCounter++;
                                    break;
                                case "Bus":
                                    space.SpaceType = spaceTypeBus;
                                    space.Name = "Bus-" + busCounter;
                                    busCounter++;
                                    break;
                                case "Street":
                                    space.SpaceType = spaceTypeStreet;
                                    space.Name = "Street-" + streetCounter;
                                    streetCounter++;
                                    break;
                                case "Clear":
                                    space.SpaceType = spaceTypeClear;
                                    space.Name = "Clear-" + clearCounter;
                                    clearCounter++;
                                    break;
                            }
                            break;
                        case "14,13":
                            switch (hidden294.Value)
                            {
                                case "Car":
                                    space.SpaceType = spaceTypeCar;
                                    space.Name = "Car-" + carcounter;
                                    carcounter++;
                                    break;
                                case "Motorcycle":
                                    space.SpaceType = spaceTypeMoto;
                                    space.Name = "Motorcycle-" + motoCounter;
                                    motoCounter++;
                                    break;
                                case "Handicap":
                                    space.SpaceType = spaceTypeHandicap;
                                    space.Name = "Handicap-" + handicapCounter;
                                    handicapCounter++;
                                    break;
                                case "Bus":
                                    space.SpaceType = spaceTypeBus;
                                    space.Name = "Bus-" + busCounter;
                                    busCounter++;
                                    break;
                                case "Street":
                                    space.SpaceType = spaceTypeStreet;
                                    space.Name = "Street-" + streetCounter;
                                    streetCounter++;
                                    break;
                                case "Clear":
                                    space.SpaceType = spaceTypeClear;
                                    space.Name = "Clear-" + clearCounter;
                                    clearCounter++;
                                    break;
                            }
                            break;
                        case "14,14":
                            switch (hidden295.Value)
                            {
                                case "Car":
                                    space.SpaceType = spaceTypeCar;
                                    space.Name = "Car-" + carcounter;
                                    carcounter++;
                                    break;
                                case "Motorcycle":
                                    space.SpaceType = spaceTypeMoto;
                                    space.Name = "Motorcycle-" + motoCounter;
                                    motoCounter++;
                                    break;
                                case "Handicap":
                                    space.SpaceType = spaceTypeHandicap;
                                    space.Name = "Handicap-" + handicapCounter;
                                    handicapCounter++;
                                    break;
                                case "Bus":
                                    space.SpaceType = spaceTypeBus;
                                    space.Name = "Bus-" + busCounter;
                                    busCounter++;
                                    break;
                                case "Street":
                                    space.SpaceType = spaceTypeStreet;
                                    space.Name = "Street-" + streetCounter;
                                    streetCounter++;
                                    break;
                                case "Clear":
                                    space.SpaceType = spaceTypeClear;
                                    space.Name = "Clear-" + clearCounter;
                                    clearCounter++;
                                    break;
                            }
                            break;
                        case "14,15":
                            switch (hidden296.Value)
                            {
                                case "Car":
                                    space.SpaceType = spaceTypeCar;
                                    space.Name = "Car-" + carcounter;
                                    carcounter++;
                                    break;
                                case "Motorcycle":
                                    space.SpaceType = spaceTypeMoto;
                                    space.Name = "Motorcycle-" + motoCounter;
                                    motoCounter++;
                                    break;
                                case "Handicap":
                                    space.SpaceType = spaceTypeHandicap;
                                    space.Name = "Handicap-" + handicapCounter;
                                    handicapCounter++;
                                    break;
                                case "Bus":
                                    space.SpaceType = spaceTypeBus;
                                    space.Name = "Bus-" + busCounter;
                                    busCounter++;
                                    break;
                                case "Street":
                                    space.SpaceType = spaceTypeStreet;
                                    space.Name = "Street-" + streetCounter;
                                    streetCounter++;
                                    break;
                                case "Clear":
                                    space.SpaceType = spaceTypeClear;
                                    space.Name = "Clear-" + clearCounter;
                                    clearCounter++;
                                    break;
                            }
                            break;
                        case "14,16":
                            switch (hidden297.Value)
                            {
                                case "Car":
                                    space.SpaceType = spaceTypeCar;
                                    space.Name = "Car-" + carcounter;
                                    carcounter++;
                                    break;
                                case "Motorcycle":
                                    space.SpaceType = spaceTypeMoto;
                                    space.Name = "Motorcycle-" + motoCounter;
                                    motoCounter++;
                                    break;
                                case "Handicap":
                                    space.SpaceType = spaceTypeHandicap;
                                    space.Name = "Handicap-" + handicapCounter;
                                    handicapCounter++;
                                    break;
                                case "Bus":
                                    space.SpaceType = spaceTypeBus;
                                    space.Name = "Bus-" + busCounter;
                                    busCounter++;
                                    break;
                                case "Street":
                                    space.SpaceType = spaceTypeStreet;
                                    space.Name = "Street-" + streetCounter;
                                    streetCounter++;
                                    break;
                                case "Clear":
                                    space.SpaceType = spaceTypeClear;
                                    space.Name = "Clear-" + clearCounter;
                                    clearCounter++;
                                    break;
                            }
                            break;
                        case "14,17":
                            switch (hidden298.Value)
                            {
                                case "Car":
                                    space.SpaceType = spaceTypeCar;
                                    space.Name = "Car-" + carcounter;
                                    carcounter++;
                                    break;
                                case "Motorcycle":
                                    space.SpaceType = spaceTypeMoto;
                                    space.Name = "Motorcycle-" + motoCounter;
                                    motoCounter++;
                                    break;
                                case "Handicap":
                                    space.SpaceType = spaceTypeHandicap;
                                    space.Name = "Handicap-" + handicapCounter;
                                    handicapCounter++;
                                    break;
                                case "Bus":
                                    space.SpaceType = spaceTypeBus;
                                    space.Name = "Bus-" + busCounter;
                                    busCounter++;
                                    break;
                                case "Street":
                                    space.SpaceType = spaceTypeStreet;
                                    space.Name = "Street-" + streetCounter;
                                    streetCounter++;
                                    break;
                                case "Clear":
                                    space.SpaceType = spaceTypeClear;
                                    space.Name = "Clear-" + clearCounter;
                                    clearCounter++;
                                    break;
                            }
                            break;
                        case "14,18":
                            switch (hidden299.Value)
                            {
                                case "Car":
                                    space.SpaceType = spaceTypeCar;
                                    space.Name = "Car-" + carcounter;
                                    carcounter++;
                                    break;
                                case "Motorcycle":
                                    space.SpaceType = spaceTypeMoto;
                                    space.Name = "Motorcycle-" + motoCounter;
                                    motoCounter++;
                                    break;
                                case "Handicap":
                                    space.SpaceType = spaceTypeHandicap;
                                    space.Name = "Handicap-" + handicapCounter;
                                    handicapCounter++;
                                    break;
                                case "Bus":
                                    space.SpaceType = spaceTypeBus;
                                    space.Name = "Bus-" + busCounter;
                                    busCounter++;
                                    break;
                                case "Street":
                                    space.SpaceType = spaceTypeStreet;
                                    space.Name = "Street-" + streetCounter;
                                    streetCounter++;
                                    break;
                                case "Clear":
                                    space.SpaceType = spaceTypeClear;
                                    space.Name = "Clear-" + clearCounter;
                                    clearCounter++;
                                    break;
                            }
                            break;
                        case "14,19":
                            switch (hidden300.Value)
                            {
                                case "Car":
                                    space.SpaceType = spaceTypeCar;
                                    space.Name = "Car-" + carcounter;
                                    carcounter++;
                                    break;
                                case "Motorcycle":
                                    space.SpaceType = spaceTypeMoto;
                                    space.Name = "Motorcycle-" + motoCounter;
                                    motoCounter++;
                                    break;
                                case "Handicap":
                                    space.SpaceType = spaceTypeHandicap;
                                    space.Name = "Handicap-" + handicapCounter;
                                    handicapCounter++;
                                    break;
                                case "Bus":
                                    space.SpaceType = spaceTypeBus;
                                    space.Name = "Bus-" + busCounter;
                                    busCounter++;
                                    break;
                                case "Street":
                                    space.SpaceType = spaceTypeStreet;
                                    space.Name = "Street-" + streetCounter;
                                    streetCounter++;
                                    break;
                                case "Clear":
                                    space.SpaceType = spaceTypeClear;
                                    space.Name = "Clear-" + clearCounter;
                                    clearCounter++;
                                    break;
                            }
                            break;

                        case "15,0":
                            switch (hidden301.Value)
                            {
                                case "Car":
                                    space.SpaceType = spaceTypeCar;
                                    space.Name = "Car-" + carcounter;
                                    carcounter++;
                                    break;
                                case "Motorcycle":
                                    space.SpaceType = spaceTypeMoto;
                                    space.Name = "Motorcycle-" + motoCounter;
                                    motoCounter++;
                                    break;
                                case "Handicap":
                                    space.SpaceType = spaceTypeHandicap;
                                    space.Name = "Handicap-" + handicapCounter;
                                    handicapCounter++;
                                    break;
                                case "Bus":
                                    space.SpaceType = spaceTypeBus;
                                    space.Name = "Bus-" + busCounter;
                                    busCounter++;
                                    break;
                                case "Street":
                                    space.SpaceType = spaceTypeStreet;
                                    space.Name = "Street-" + streetCounter;
                                    streetCounter++;
                                    break;
                                case "Clear":
                                    space.SpaceType = spaceTypeClear;
                                    space.Name = "Clear-" + clearCounter;
                                    clearCounter++;
                                    break;
                            }
                            break;
                        case "15,1":
                            switch (hidden302.Value)
                            {
                                case "Car":
                                    space.SpaceType = spaceTypeCar;
                                    space.Name = "Car-" + carcounter;
                                    carcounter++;
                                    break;
                                case "Motorcycle":
                                    space.SpaceType = spaceTypeMoto;
                                    space.Name = "Motorcycle-" + motoCounter;
                                    motoCounter++;
                                    break;
                                case "Handicap":
                                    space.SpaceType = spaceTypeHandicap;
                                    space.Name = "Handicap-" + handicapCounter;
                                    handicapCounter++;
                                    break;
                                case "Bus":
                                    space.SpaceType = spaceTypeBus;
                                    space.Name = "Bus-" + busCounter;
                                    busCounter++;
                                    break;
                                case "Street":
                                    space.SpaceType = spaceTypeStreet;
                                    space.Name = "Street-" + streetCounter;
                                    streetCounter++;
                                    break;
                                case "Clear":
                                    space.SpaceType = spaceTypeClear;
                                    space.Name = "Clear-" + clearCounter;
                                    clearCounter++;
                                    break;
                            }
                            break;
                        case "15,2":
                            switch (hidden303.Value)
                            {
                                case "Car":
                                    space.SpaceType = spaceTypeCar;
                                    space.Name = "Car-" + carcounter;
                                    carcounter++;
                                    break;
                                case "Motorcycle":
                                    space.SpaceType = spaceTypeMoto;
                                    space.Name = "Motorcycle-" + motoCounter;
                                    motoCounter++;
                                    break;
                                case "Handicap":
                                    space.SpaceType = spaceTypeHandicap;
                                    space.Name = "Handicap-" + handicapCounter;
                                    handicapCounter++;
                                    break;
                                case "Bus":
                                    space.SpaceType = spaceTypeBus;
                                    space.Name = "Bus-" + busCounter;
                                    busCounter++;
                                    break;
                                case "Street":
                                    space.SpaceType = spaceTypeStreet;
                                    space.Name = "Street-" + streetCounter;
                                    streetCounter++;
                                    break;
                                case "Clear":
                                    space.SpaceType = spaceTypeClear;
                                    space.Name = "Clear-" + clearCounter;
                                    clearCounter++;
                                    break;
                            }
                            break;
                        case "15,3":
                            switch (hidden304.Value)
                            {
                                case "Car":
                                    space.SpaceType = spaceTypeCar;
                                    space.Name = "Car-" + carcounter;
                                    carcounter++;
                                    break;
                                case "Motorcycle":
                                    space.SpaceType = spaceTypeMoto;
                                    space.Name = "Motorcycle-" + motoCounter;
                                    motoCounter++;
                                    break;
                                case "Handicap":
                                    space.SpaceType = spaceTypeHandicap;
                                    space.Name = "Handicap-" + handicapCounter;
                                    handicapCounter++;
                                    break;
                                case "Bus":
                                    space.SpaceType = spaceTypeBus;
                                    space.Name = "Bus-" + busCounter;
                                    busCounter++;
                                    break;
                                case "Street":
                                    space.SpaceType = spaceTypeStreet;
                                    space.Name = "Street-" + streetCounter;
                                    streetCounter++;
                                    break;
                                case "Clear":
                                    space.SpaceType = spaceTypeClear;
                                    space.Name = "Clear-" + clearCounter;
                                    clearCounter++;
                                    break;
                            }
                            break;
                        case "15,4":
                            switch (hidden305.Value)
                            {
                                case "Car":
                                    space.SpaceType = spaceTypeCar;
                                    space.Name = "Car-" + carcounter;
                                    carcounter++;
                                    break;
                                case "Motorcycle":
                                    space.SpaceType = spaceTypeMoto;
                                    space.Name = "Motorcycle-" + motoCounter;
                                    motoCounter++;
                                    break;
                                case "Handicap":
                                    space.SpaceType = spaceTypeHandicap;
                                    space.Name = "Handicap-" + handicapCounter;
                                    handicapCounter++;
                                    break;
                                case "Bus":
                                    space.SpaceType = spaceTypeBus;
                                    space.Name = "Bus-" + busCounter;
                                    busCounter++;
                                    break;
                                case "Street":
                                    space.SpaceType = spaceTypeStreet;
                                    space.Name = "Street-" + streetCounter;
                                    streetCounter++;
                                    break;
                                case "Clear":
                                    space.SpaceType = spaceTypeClear;
                                    space.Name = "Clear-" + clearCounter;
                                    clearCounter++;
                                    break;
                            }
                            break;
                        case "15,5":
                            switch (hidden306.Value)
                            {
                                case "Car":
                                    space.SpaceType = spaceTypeCar;
                                    space.Name = "Car-" + carcounter;
                                    carcounter++;
                                    break;
                                case "Motorcycle":
                                    space.SpaceType = spaceTypeMoto;
                                    space.Name = "Motorcycle-" + motoCounter;
                                    motoCounter++;
                                    break;
                                case "Handicap":
                                    space.SpaceType = spaceTypeHandicap;
                                    space.Name = "Handicap-" + handicapCounter;
                                    handicapCounter++;
                                    break;
                                case "Bus":
                                    space.SpaceType = spaceTypeBus;
                                    space.Name = "Bus-" + busCounter;
                                    busCounter++;
                                    break;
                                case "Street":
                                    space.SpaceType = spaceTypeStreet;
                                    space.Name = "Street-" + streetCounter;
                                    streetCounter++;
                                    break;
                                case "Clear":
                                    space.SpaceType = spaceTypeClear;
                                    space.Name = "Clear-" + clearCounter;
                                    clearCounter++;
                                    break;
                            }
                            break;
                        case "15,6":
                            switch (hidden307.Value)
                            {
                                case "Car":
                                    space.SpaceType = spaceTypeCar;
                                    space.Name = "Car-" + carcounter;
                                    carcounter++;
                                    break;
                                case "Motorcycle":
                                    space.SpaceType = spaceTypeMoto;
                                    space.Name = "Motorcycle-" + motoCounter;
                                    motoCounter++;
                                    break;
                                case "Handicap":
                                    space.SpaceType = spaceTypeHandicap;
                                    space.Name = "Handicap-" + handicapCounter;
                                    handicapCounter++;
                                    break;
                                case "Bus":
                                    space.SpaceType = spaceTypeBus;
                                    space.Name = "Bus-" + busCounter;
                                    busCounter++;
                                    break;
                                case "Street":
                                    space.SpaceType = spaceTypeStreet;
                                    space.Name = "Street-" + streetCounter;
                                    streetCounter++;
                                    break;
                                case "Clear":
                                    space.SpaceType = spaceTypeClear;
                                    space.Name = "Clear-" + clearCounter;
                                    clearCounter++;
                                    break;
                            }
                            break;
                        case "15,7":
                            switch (hidden308.Value)
                            {
                                case "Car":
                                    space.SpaceType = spaceTypeCar;
                                    space.Name = "Car-" + carcounter;
                                    carcounter++;
                                    break;
                                case "Motorcycle":
                                    space.SpaceType = spaceTypeMoto;
                                    space.Name = "Motorcycle-" + motoCounter;
                                    motoCounter++;
                                    break;
                                case "Handicap":
                                    space.SpaceType = spaceTypeHandicap;
                                    space.Name = "Handicap-" + handicapCounter;
                                    handicapCounter++;
                                    break;
                                case "Bus":
                                    space.SpaceType = spaceTypeBus;
                                    space.Name = "Bus-" + busCounter;
                                    busCounter++;
                                    break;
                                case "Street":
                                    space.SpaceType = spaceTypeStreet;
                                    space.Name = "Street-" + streetCounter;
                                    streetCounter++;
                                    break;
                                case "Clear":
                                    space.SpaceType = spaceTypeClear;
                                    space.Name = "Clear-" + clearCounter;
                                    clearCounter++;
                                    break;
                            }
                            break;
                        case "15,8":
                            switch (hidden309.Value)
                            {
                                case "Car":
                                    space.SpaceType = spaceTypeCar;
                                    space.Name = "Car-" + carcounter;
                                    carcounter++;
                                    break;
                                case "Motorcycle":
                                    space.SpaceType = spaceTypeMoto;
                                    space.Name = "Motorcycle-" + motoCounter;
                                    motoCounter++;
                                    break;
                                case "Handicap":
                                    space.SpaceType = spaceTypeHandicap;
                                    space.Name = "Handicap-" + handicapCounter;
                                    handicapCounter++;
                                    break;
                                case "Bus":
                                    space.SpaceType = spaceTypeBus;
                                    space.Name = "Bus-" + busCounter;
                                    busCounter++;
                                    break;
                                case "Street":
                                    space.SpaceType = spaceTypeStreet;
                                    space.Name = "Street-" + streetCounter;
                                    streetCounter++;
                                    break;
                                case "Clear":
                                    space.SpaceType = spaceTypeClear;
                                    space.Name = "Clear-" + clearCounter;
                                    clearCounter++;
                                    break;
                            }
                            break;
                        case "15,9":
                            switch (hidden310.Value)
                            {
                                case "Car":
                                    space.SpaceType = spaceTypeCar;
                                    space.Name = "Car-" + carcounter;
                                    carcounter++;
                                    break;
                                case "Motorcycle":
                                    space.SpaceType = spaceTypeMoto;
                                    space.Name = "Motorcycle-" + motoCounter;
                                    motoCounter++;
                                    break;
                                case "Handicap":
                                    space.SpaceType = spaceTypeHandicap;
                                    space.Name = "Handicap-" + handicapCounter;
                                    handicapCounter++;
                                    break;
                                case "Bus":
                                    space.SpaceType = spaceTypeBus;
                                    space.Name = "Bus-" + busCounter;
                                    busCounter++;
                                    break;
                                case "Street":
                                    space.SpaceType = spaceTypeStreet;
                                    space.Name = "Street-" + streetCounter;
                                    streetCounter++;
                                    break;
                                case "Clear":
                                    space.SpaceType = spaceTypeClear;
                                    space.Name = "Clear-" + clearCounter;
                                    clearCounter++;
                                    break;
                            }
                            break;
                        case "15,10":
                            switch (hidden311.Value)
                            {
                                case "Car":
                                    space.SpaceType = spaceTypeCar;
                                    space.Name = "Car-" + carcounter;
                                    carcounter++;
                                    break;
                                case "Motorcycle":
                                    space.SpaceType = spaceTypeMoto;
                                    space.Name = "Motorcycle-" + motoCounter;
                                    motoCounter++;
                                    break;
                                case "Handicap":
                                    space.SpaceType = spaceTypeHandicap;
                                    space.Name = "Handicap-" + handicapCounter;
                                    handicapCounter++;
                                    break;
                                case "Bus":
                                    space.SpaceType = spaceTypeBus;
                                    space.Name = "Bus-" + busCounter;
                                    busCounter++;
                                    break;
                                case "Street":
                                    space.SpaceType = spaceTypeStreet;
                                    space.Name = "Street-" + streetCounter;
                                    streetCounter++;
                                    break;
                                case "Clear":
                                    space.SpaceType = spaceTypeClear;
                                    space.Name = "Clear-" + clearCounter;
                                    clearCounter++;
                                    break;
                            }
                            break;
                        case "15,11":
                            switch (hidden312.Value)
                            {
                                case "Car":
                                    space.SpaceType = spaceTypeCar;
                                    space.Name = "Car-" + carcounter;
                                    carcounter++;
                                    break;
                                case "Motorcycle":
                                    space.SpaceType = spaceTypeMoto;
                                    space.Name = "Motorcycle-" + motoCounter;
                                    motoCounter++;
                                    break;
                                case "Handicap":
                                    space.SpaceType = spaceTypeHandicap;
                                    space.Name = "Handicap-" + handicapCounter;
                                    handicapCounter++;
                                    break;
                                case "Bus":
                                    space.SpaceType = spaceTypeBus;
                                    space.Name = "Bus-" + busCounter;
                                    busCounter++;
                                    break;
                                case "Street":
                                    space.SpaceType = spaceTypeStreet;
                                    space.Name = "Street-" + streetCounter;
                                    streetCounter++;
                                    break;
                                case "Clear":
                                    space.SpaceType = spaceTypeClear;
                                    space.Name = "Clear-" + clearCounter;
                                    clearCounter++;
                                    break;
                            }
                            break;
                        case "15,12":
                            switch (hidden313.Value)
                            {
                                case "Car":
                                    space.SpaceType = spaceTypeCar;
                                    space.Name = "Car-" + carcounter;
                                    carcounter++;
                                    break;
                                case "Motorcycle":
                                    space.SpaceType = spaceTypeMoto;
                                    space.Name = "Motorcycle-" + motoCounter;
                                    motoCounter++;
                                    break;
                                case "Handicap":
                                    space.SpaceType = spaceTypeHandicap;
                                    space.Name = "Handicap-" + handicapCounter;
                                    handicapCounter++;
                                    break;
                                case "Bus":
                                    space.SpaceType = spaceTypeBus;
                                    space.Name = "Bus-" + busCounter;
                                    busCounter++;
                                    break;
                                case "Street":
                                    space.SpaceType = spaceTypeStreet;
                                    space.Name = "Street-" + streetCounter;
                                    streetCounter++;
                                    break;
                                case "Clear":
                                    space.SpaceType = spaceTypeClear;
                                    space.Name = "Clear-" + clearCounter;
                                    clearCounter++;
                                    break;
                            }
                            break;
                        case "15,13":
                            switch (hidden314.Value)
                            {
                                case "Car":
                                    space.SpaceType = spaceTypeCar;
                                    space.Name = "Car-" + carcounter;
                                    carcounter++;
                                    break;
                                case "Motorcycle":
                                    space.SpaceType = spaceTypeMoto;
                                    space.Name = "Motorcycle-" + motoCounter;
                                    motoCounter++;
                                    break;
                                case "Handicap":
                                    space.SpaceType = spaceTypeHandicap;
                                    space.Name = "Handicap-" + handicapCounter;
                                    handicapCounter++;
                                    break;
                                case "Bus":
                                    space.SpaceType = spaceTypeBus;
                                    space.Name = "Bus-" + busCounter;
                                    busCounter++;
                                    break;
                                case "Street":
                                    space.SpaceType = spaceTypeStreet;
                                    space.Name = "Street-" + streetCounter;
                                    streetCounter++;
                                    break;
                                case "Clear":
                                    space.SpaceType = spaceTypeClear;
                                    space.Name = "Clear-" + clearCounter;
                                    clearCounter++;
                                    break;
                            }
                            break;
                        case "15,14":
                            switch (hidden315.Value)
                            {
                                case "Car":
                                    space.SpaceType = spaceTypeCar;
                                    space.Name = "Car-" + carcounter;
                                    carcounter++;
                                    break;
                                case "Motorcycle":
                                    space.SpaceType = spaceTypeMoto;
                                    space.Name = "Motorcycle-" + motoCounter;
                                    motoCounter++;
                                    break;
                                case "Handicap":
                                    space.SpaceType = spaceTypeHandicap;
                                    space.Name = "Handicap-" + handicapCounter;
                                    handicapCounter++;
                                    break;
                                case "Bus":
                                    space.SpaceType = spaceTypeBus;
                                    space.Name = "Bus-" + busCounter;
                                    busCounter++;
                                    break;
                                case "Street":
                                    space.SpaceType = spaceTypeStreet;
                                    space.Name = "Street-" + streetCounter;
                                    streetCounter++;
                                    break;
                                case "Clear":
                                    space.SpaceType = spaceTypeClear;
                                    space.Name = "Clear-" + clearCounter;
                                    clearCounter++;
                                    break;
                            }
                            break;
                        case "15,15":
                            switch (hidden316.Value)
                            {
                                case "Car":
                                    space.SpaceType = spaceTypeCar;
                                    space.Name = "Car-" + carcounter;
                                    carcounter++;
                                    break;
                                case "Motorcycle":
                                    space.SpaceType = spaceTypeMoto;
                                    space.Name = "Motorcycle-" + motoCounter;
                                    motoCounter++;
                                    break;
                                case "Handicap":
                                    space.SpaceType = spaceTypeHandicap;
                                    space.Name = "Handicap-" + handicapCounter;
                                    handicapCounter++;
                                    break;
                                case "Bus":
                                    space.SpaceType = spaceTypeBus;
                                    space.Name = "Bus-" + busCounter;
                                    busCounter++;
                                    break;
                                case "Street":
                                    space.SpaceType = spaceTypeStreet;
                                    space.Name = "Street-" + streetCounter;
                                    streetCounter++;
                                    break;
                                case "Clear":
                                    space.SpaceType = spaceTypeClear;
                                    space.Name = "Clear-" + clearCounter;
                                    clearCounter++;
                                    break;
                            }
                            break;
                        case "15,16":
                            switch (hidden317.Value)
                            {
                                case "Car":
                                    space.SpaceType = spaceTypeCar;
                                    space.Name = "Car-" + carcounter;
                                    carcounter++;
                                    break;
                                case "Motorcycle":
                                    space.SpaceType = spaceTypeMoto;
                                    space.Name = "Motorcycle-" + motoCounter;
                                    motoCounter++;
                                    break;
                                case "Handicap":
                                    space.SpaceType = spaceTypeHandicap;
                                    space.Name = "Handicap-" + handicapCounter;
                                    handicapCounter++;
                                    break;
                                case "Bus":
                                    space.SpaceType = spaceTypeBus;
                                    space.Name = "Bus-" + busCounter;
                                    busCounter++;
                                    break;
                                case "Street":
                                    space.SpaceType = spaceTypeStreet;
                                    space.Name = "Street-" + streetCounter;
                                    streetCounter++;
                                    break;
                                case "Clear":
                                    space.SpaceType = spaceTypeClear;
                                    space.Name = "Clear-" + clearCounter;
                                    clearCounter++;
                                    break;
                            }
                            break;
                        case "15,17":
                            switch (hidden318.Value)
                            {
                                case "Car":
                                    space.SpaceType = spaceTypeCar;
                                    space.Name = "Car-" + carcounter;
                                    carcounter++;
                                    break;
                                case "Motorcycle":
                                    space.SpaceType = spaceTypeMoto;
                                    space.Name = "Motorcycle-" + motoCounter;
                                    motoCounter++;
                                    break;
                                case "Handicap":
                                    space.SpaceType = spaceTypeHandicap;
                                    space.Name = "Handicap-" + handicapCounter;
                                    handicapCounter++;
                                    break;
                                case "Bus":
                                    space.SpaceType = spaceTypeBus;
                                    space.Name = "Bus-" + busCounter;
                                    busCounter++;
                                    break;
                                case "Street":
                                    space.SpaceType = spaceTypeStreet;
                                    space.Name = "Street-" + streetCounter;
                                    streetCounter++;
                                    break;
                                case "Clear":
                                    space.SpaceType = spaceTypeClear;
                                    space.Name = "Clear-" + clearCounter;
                                    clearCounter++;
                                    break;
                            }
                            break;
                        case "15,18":
                            switch (hidden319.Value)
                            {
                                case "Car":
                                    space.SpaceType = spaceTypeCar;
                                    space.Name = "Car-" + carcounter;
                                    carcounter++;
                                    break;
                                case "Motorcycle":
                                    space.SpaceType = spaceTypeMoto;
                                    space.Name = "Motorcycle-" + motoCounter;
                                    motoCounter++;
                                    break;
                                case "Handicap":
                                    space.SpaceType = spaceTypeHandicap;
                                    space.Name = "Handicap-" + handicapCounter;
                                    handicapCounter++;
                                    break;
                                case "Bus":
                                    space.SpaceType = spaceTypeBus;
                                    space.Name = "Bus-" + busCounter;
                                    busCounter++;
                                    break;
                                case "Street":
                                    space.SpaceType = spaceTypeStreet;
                                    space.Name = "Street-" + streetCounter;
                                    streetCounter++;
                                    break;
                                case "Clear":
                                    space.SpaceType = spaceTypeClear;
                                    space.Name = "Clear-" + clearCounter;
                                    clearCounter++;
                                    break;
                            }
                            break;
                        case "15,19":
                            switch (hidden320.Value)
                            {
                                case "Car":
                                    space.SpaceType = spaceTypeCar;
                                    space.Name = "Car-" + carcounter;
                                    carcounter++;
                                    break;
                                case "Motorcycle":
                                    space.SpaceType = spaceTypeMoto;
                                    space.Name = "Motorcycle-" + motoCounter;
                                    motoCounter++;
                                    break;
                                case "Handicap":
                                    space.SpaceType = spaceTypeHandicap;
                                    space.Name = "Handicap-" + handicapCounter;
                                    handicapCounter++;
                                    break;
                                case "Bus":
                                    space.SpaceType = spaceTypeBus;
                                    space.Name = "Bus-" + busCounter;
                                    busCounter++;
                                    break;
                                case "Street":
                                    space.SpaceType = spaceTypeStreet;
                                    space.Name = "Street-" + streetCounter;
                                    streetCounter++;
                                    break;
                                case "Clear":
                                    space.SpaceType = spaceTypeClear;
                                    space.Name = "Clear-" + clearCounter;
                                    clearCounter++;
                                    break;
                            }
                            break;
                    }
                }
                return spaceList;
            }
            catch (Exception)
            {
                buttonStyle.buttonStyleRed(buttonErrors, "An error ocurred creating the parking spaces.");
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
                        parking.SpaceList = CreateSpaceListWithMap();
                        if (parking.SpaceList != null)
                        {
                            switch (parkingBussinessRules.InsertParkingSpace(parking))
                            {
                                case 0:
                                    ClearControls();
                                    OpenControls();
                                    buttonStyle.buttonStyleBlue(buttonErrors, "Parking created sucessfully.");
                                    break;
                                case 1:
                                    OpenControls();
                                    buttonStyle.buttonStyleRed(buttonErrors, "An error ocurred creating the parking spaces.");
                                    break;
                                case 2:
                                    OpenControls();
                                    buttonStyle.buttonStyleWhite(buttonErrors, "An error ocurred creating the parking spaces and deleting the parking.");
                                    break;
                            }
                        }
                        else
                        {
                            OpenControls();
                            buttonStyle.buttonStyleRed(buttonErrors, "An error ocurred creating the parking spaces.");
                            parkingBussinessRules.DeleteParking(parking.Id);
                        }
                        break;
                    case 1:
                        OpenControls();
                        buttonStyle.buttonStyleRed(buttonErrors, "The parking name field is empty.");
                        break;
                    case 2:
                        OpenControls();
                        buttonStyle.buttonStyleWhite(buttonErrors, "The total space field can't be less or equal than zero");
                        break;
                    case 3:
                        OpenControls();
                        buttonStyle.buttonStyleRed(buttonErrors, "The car space field can't be less than zero.");
                        break;
                    case 4:
                        OpenControls();
                        buttonStyle.buttonStyleWhite(buttonErrors, "The motorcycle space field can't be less than zero.");
                        break;
                    case 5:
                        OpenControls();
                        buttonStyle.buttonStyleBlue(buttonErrors, "The handicap space field can't be less than zero.");
                        break;
                    case 6:
                        OpenControls();
                        buttonStyle.buttonStyleWhite(buttonErrors, "The bus space field can't be less than zero.");
                        break;
                    case 7:
                        OpenControls();
                        buttonStyle.buttonStyleRed(buttonErrors, "You can't enter zero in all space fields.");
                        break;
                    case 8:
                        OpenControls();
                        buttonStyle.buttonStyleWhite(buttonErrors, "The sum of space fields can't be higher than the total space.");
                        break;
                    case 10:
                        OpenControls();
                        buttonStyle.buttonStyleWhite(buttonErrors, "An error ocurred creating the parking, please check data or contact with us.");
                        break;
                    case 11:
                        OpenControls();
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

        protected void OpenControls()
        {
            buttonErrors2.Visible = false;
            textboxParkingName.Disabled = false;
            textboxTotalSpace.Disabled = false;
            textboxCarSpace.Disabled = false;
            textboxMotorCycleSpace.Disabled = false;
            textboxHandicapSpace.Disabled = false;
            textboxBusSpace.Disabled = false;
            buttonGenerateMap.Disabled = false;
            buttonClear.Visible = true;
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
