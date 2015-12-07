using SYSPARK.App_BussinessRules;
using SYSPARK.App_Entities;
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
    public partial class SpacePage : System.Web.UI.Page
    {
        ButtonStyle buttonStyle = new ButtonStyle();
        SpaceBussinessRules spaceRules = new SpaceBussinessRules();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["User-UserName"] == null)
                Response.Redirect("Default.aspx");

            FillSelectSpaceType();
            FillSelectCampus();
            FillSelectCampusToView();
            FillSelectParking(sender, e);
            FillSelectParkingToView(sender, e);
            FillTable();
        }

        protected void FillSelectSpaceType()
        {
            //Select Campus
            SpaceTypeData spaceTypeData = new SpaceTypeData();
            selectSpaceType.DataSource = spaceTypeData.DataTableSpaceType();
            selectSpaceType.DataValueField = "Id";
            selectSpaceType.DataTextField = "Name";
            selectSpaceType.DataBind();
        }

        protected void FillSelectCampus()
        {
            //Select Campus
            CampusData campusData = new CampusData();
            selectCampus.DataSource = campusData.DataTableCampus();
            selectCampus.DataValueField = "Id";
            selectCampus.DataTextField = "Name";
            selectCampus.DataBind();
        }

        protected void FillSelectCampusToView()
        {
            //Select Campus
            CampusData campusData = new CampusData();
            selectCampusToView.DataSource = campusData.DataTableCampus();
            selectCampusToView.DataValueField = "Id";
            selectCampusToView.DataTextField = "Name";
            selectCampusToView.DataBind();
        }

        protected void FillSelectParking(object sender, EventArgs e)
        {
            //Select parking
            ParkingData parkingData = new ParkingData();
            DataTable dataTableParking = new DataTable();
            if (hiddenCampusValue.Value.Equals(string.Empty))
            {
                dataTableParking = parkingData.DataTableParking(Convert.ToInt32(selectCampus.Items[0].Value));
            }
            else
            {
                dataTableParking = parkingData.DataTableParking(Convert.ToInt32(hiddenCampusValue.Value));
            }
            selectParking.DataSource = dataTableParking;
            selectParking.DataValueField = "Id";
            selectParking.DataTextField = "Name";
            selectParking.DataBind();
        }

        protected void FillSelectParkingToView(object sender, EventArgs e)
        {
            //Select parking
            ParkingData parkingData = new ParkingData();
            DataTable dataTableParking = new DataTable();
            if (hiddenCampusToViewValue.Value.Equals(string.Empty))
            {
                dataTableParking = parkingData.DataTableParking(Convert.ToInt32(selectCampusToView.Items[0].Value));
            }
            else
            {
                dataTableParking = parkingData.DataTableParking(Convert.ToInt32(hiddenCampusToViewValue.Value));
            }
            selectParkingToView.DataSource = dataTableParking;
            selectParkingToView.DataValueField = "Id";
            selectParkingToView.DataTextField = "Name";
            selectParkingToView.DataBind();
        }

        protected void AddSpace_Click(object sender, EventArgs e)
        {
            InsertSpace(CreateSpaceList());
            FillTable();
        }

        protected void InsertSpace(List<Space> spaceList)
        {
            if (spaceList != null)
            {
                switch (spaceRules.InsertParkingSpace(spaceList))
                {
                    case 0:
                        buttonStyle.buttonStyleBlue(buttonErrors, "Space added sucessful.");
                        break;
                    case 1:
                        buttonStyle.buttonStyleWhite(buttonErrors, "An error ocurred inserting the space. ");
                        break;
                }
            }
            else
            {
                buttonStyle.buttonStyleRed(buttonErrors, "Invalid data, please check it.");
            }
        }

        protected List<Space> CreateSpaceList()
        {
            try
            {
                //Creating the vehicle
                List<Space> spaceList = new List<Space>();
                SpaceType spaceType = new SpaceType();
                SpaceData spaceData = new SpaceData();
                DataTable dataTableAmountOfSpace = new DataTable();
                int parkingId;
                string parkingName;
                int campusId;
                int spaceTypeId;
                string spaceTypeName;
                bool status = true;
                int amount = 1;

                try
                {
                    amount = Convert.ToInt32(textboxAmount.Value);
                }
                catch (FormatException)
                {
                    buttonStyle.buttonStyleRed(buttonErrors, "You can't enter numbers in amount field.");
                    return null;
                }

                if (hiddenCampusValue.Value.Equals(string.Empty))
                    campusId = Convert.ToInt32(selectCampus.Items[0].Value);
                else
                    campusId = Convert.ToInt32(hiddenCampusValue.Value);

                if (hiddenPakingValue.Value.Equals(string.Empty))
                {
                    parkingId = Convert.ToInt32(selectParking.Items[0].Value);
                    parkingName = selectParking.Items[0].Text;
                }
                else
                {
                    parkingId = Convert.ToInt32(hiddenPakingValue.Value);
                    parkingName = selectParking.Items.FindByValue(hiddenPakingValue.Value).Text;
                }

                if (hiddenSpaceTypeValue.Value.Equals(string.Empty))
                {
                    dataTableAmountOfSpace = spaceData.DataTableParkingTypeSpace(parkingId, Convert.ToInt32(selectSpaceType.Items[0].Value));
                    spaceTypeId = Convert.ToInt32(selectSpaceType.Items[0].Value);
                    spaceTypeName = selectSpaceType.Items[0].Text;
                }
                else
                {
                    dataTableAmountOfSpace = spaceData.DataTableParkingTypeSpace(parkingId, Convert.ToInt32(hiddenSpaceTypeValue.Value));
                    spaceTypeId = Convert.ToInt32(hiddenSpaceTypeValue.Value);
                    spaceTypeName = selectSpaceType.Items.FindByValue(hiddenSpaceTypeValue.Value).Text;
                }

                if (hiddenStatusValue.Value.Equals(string.Empty))
                {
                    buttonStyle.buttonStyleRed(buttonErrors, "Please, input the status of the lapse.");
                    return null;
                }
                else
                {
                    if (hiddenStatusValue.Value.Equals("true"))
                        status = true;
                    else if
                        (hiddenStatusValue.Value.Equals("false"))
                        status = false;
                }

                spaceType.Id = spaceTypeId;
                spaceType.Name = spaceTypeName;

                if (amount == 0)
                {
                    buttonStyle.buttonStyleRed(buttonErrors, "The amount can't be zero.");
                    return null;
                }
                else if (amount < 0)
                {
                    buttonStyle.buttonStyleRed(buttonErrors, "The amount can't be less than zero.");
                    return null;
                }

                for (int i = dataTableAmountOfSpace.Rows.Count + 1; i < dataTableAmountOfSpace.Rows.Count + 1 + amount; i++)
                {
                    Space space = new Space();
                    space.Name = spaceTypeName + "-" + i;
                    space.ParkingId = parkingId;
                    space.ParkingName = parkingName;
                    space.ParkingCampusId = campusId;
                    space.SpaceType = spaceType;
                    space.Status = status;
                    spaceList.Add(space);
                }

                return spaceList;
            }
            catch (FormatException)
            {
                return null;
            }
        }

        protected void FillTable()
        {
            SpaceData spaceData = new SpaceData();
            DataTable dt = new DataTable();
            int campusId = 0;
            int parkingId = 0;

            //Populating a DataTable from database.
            try
            {
                if (hiddenCampusToViewValue.Value.Equals(string.Empty))
                    campusId = Convert.ToInt32(selectCampusToView.Items[0].Value);
                else
                    campusId = Convert.ToInt32(hiddenCampusToViewValue.Value);
                buttonStyle.buttonStyleBlue(buttonInfoSpaceTable, "Please, after any operation select one space!");
            }
            catch (Exception)
            {
                buttonStyle.buttonStyleRed(buttonInfoSpaceTable, "No campus or parking available to view.");
            }

            try
            {
                if (hiddenParkingToViewValue.Value.Equals(string.Empty))
                    parkingId = Convert.ToInt32(selectParkingToView.Items[0].Value);
                else
                    parkingId = Convert.ToInt32(hiddenParkingToViewValue.Value);
                buttonStyle.buttonStyleBlue(buttonInfoSpaceTable, "Please, after any operation select one space!");
            }
            catch (Exception)
            {
                buttonStyle.buttonStyleRed(buttonInfoSpaceTable, "No parking available to view.");
            }

            dt = spaceData.DataTableParkingSpace(campusId, parkingId);

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
            placeHolderTableSpace.Controls.Clear();
            placeHolderTableSpace.Controls.Add(new Literal { Text = html.ToString() });
        }

        protected void Delete_Click(object sender, EventArgs e)
        {
            if (hiddenSpaceId.Value != string.Empty)
            {
                DeleteSpace();
                FillTable();
                hiddenSpaceId.Value = "";
                hiddenSpaceTypeId.Value = "";
            }
            else
                buttonStyle.buttonStyleRed(buttonInfoSpaceTable, "Please, select a space to delete.");
        }

        protected void DeleteSpace()
        {
            int parkingId = 0;

            if (hiddenParkingToViewValue.Value.Equals(string.Empty))
                parkingId = Convert.ToInt32(selectParkingToView.Items[0].Value);
            else
                parkingId = Convert.ToInt32(hiddenParkingToViewValue.Value);

            try
            {
                switch (spaceRules.DeleteSpace(Convert.ToInt32(hiddenSpaceId.Value), parkingId, Convert.ToInt32(hiddenSpaceTypeId.Value)))
                {
                    case 0:
                        FillTable();
                        buttonStyle.buttonStyleBlue(buttonInfoSpaceTable, "Space deleted successful.");
                        break;
                    case 1:
                        FillTable();
                        buttonStyle.buttonStyleRed(buttonInfoSpaceTable, "This space has linked data can not be deleted.");
                        break;
                    case 2:
                        buttonStyle.buttonStyleWhite(buttonInfoSpaceTable, "Please, select a space to delete.");
                        break;
                }
            }
            catch (FormatException)
            {
                buttonStyle.buttonStyleRed(buttonInfoSpaceTable, "Invalid space, parking or type id.");
            }
        }
    }
}