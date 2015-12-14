using SYSPARK.App_Utility;
using SYSPARK.BussinessRules;
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
    public partial class ParkingReservation : System.Web.UI.Page
    {
        ButtonStyle buttonStyle = new ButtonStyle();
        ReservationBussinessRules reservationRules = new ReservationBussinessRules();
        ReservationData reservationData = new ReservationData();
        ParkingData parkingData = new ParkingData();
        SpaceData spaceData = new SpaceData();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["User-UserName"] == null)
                Response.Redirect("http://syspark.azurewebsites.net/Default.aspx");

            FillSelectCampus(sender, e);
            FillSelectCampusToView();
            FillSelectVehicle();
            FillTable();
        }

        protected void FillSelectCampus(object sender, EventArgs e)
        {
            //Select Campus
            CampusData campusData = new CampusData();
            selectCampus.DataSource = campusData.DataTableUserCampus(campusData.SendCampusList(campusData.GetUserCampus(Session["User-UserName"].ToString())));
            selectCampus.DataValueField = "Id";
            selectCampus.DataTextField = "Name";
            selectCampus.DataBind();
            FillSelectParking(sender, e);
        }

        protected void FillSelectCampusToView()
        {
            //Select Campus
            CampusData campusData = new CampusData();
            selectCampusToView.DataSource = campusData.DataTableUserCampus(campusData.SendCampusList(campusData.GetUserCampus(Session["User-UserName"].ToString())));
            selectCampusToView.DataValueField = "Id";
            selectCampusToView.DataTextField = "Name";
            selectCampusToView.DataBind();
        }

        protected void FillSelectParking(object sender, EventArgs e)
        {
            if (selectCampus.Items.Count > 0)
            {
                //Select parking
                ParkingData parkingData = new ParkingData();
                DataTable dataTableParking = new DataTable();
                if (hiddenCampusValue.Value.Equals(string.Empty))
                    dataTableParking = parkingData.DataTableParking(Convert.ToInt32(selectCampus.Items[0].Value));
                else
                    dataTableParking = parkingData.DataTableParking(Convert.ToInt32(hiddenCampusValue.Value));
                selectParking.DataSource = dataTableParking;
                selectParking.DataValueField = "Id";
                selectParking.DataTextField = "Name";
                selectParking.DataBind();
                FillSelectSpace(sender, e);
            }
            else
            {
                buttonStyle.buttonStyleRed(buttonErrors, "No campus and parking avaible to show.");
            }
        }

        protected void FillSelectSpace(object sender, EventArgs e)
        {
            if (selectCampus.Items.Count > 0)
            {
                if (selectParking.Items.Count > 0)
                {
                    //Select parking
                    SpaceData spaceData = new SpaceData();
                    DataTable dataTableSpace = new DataTable();
                    if (hiddenCampusValue.Value.Equals(string.Empty))
                    {
                        if (hiddenParkingValue.Value.Equals(string.Empty))
                            dataTableSpace = spaceData.DataTableParkingSpace(Convert.ToInt32(selectCampus.Items[0].Value), Convert.ToInt32(selectParking.Items[0].Value));
                        else
                            dataTableSpace = spaceData.DataTableParkingSpace(Convert.ToInt32(selectCampus.Items[0].Value), Convert.ToInt32(hiddenParkingValue.Value));
                    }
                    else
                    {
                        if (hiddenParkingValue.Value.Equals(string.Empty))
                            dataTableSpace = spaceData.DataTableParkingSpace(Convert.ToInt32(hiddenParkingValue.Value), Convert.ToInt32(selectParking.Items[0].Value));
                        else
                            dataTableSpace = spaceData.DataTableParkingSpace(Convert.ToInt32(hiddenParkingValue.Value), Convert.ToInt32(hiddenParkingValue.Value));
                    }
                    selectSpace.DataSource = dataTableSpace;
                    selectSpace.DataValueField = "Id";
                    selectSpace.DataTextField = "Name";
                    selectSpace.DataBind();
                }
                else
                    buttonStyle.buttonStyleRed(buttonErrors, "No parking and space avaible to show.");
            }
            else
                buttonStyle.buttonStyleRed(buttonErrors, "No campus, parking and space avaible to show.");
        }

        protected void FillSelectVehicle()
        {
            //Fill select my cars
            VehicleData vehicleData = new VehicleData();
            DataTable dataTableVehicleOfUser = vehicleData.DataTableUserVehicle(vehicleData.SendVehicleList(vehicleData.GetUserVehicle(Session["User-UserName"].ToString())));
            selectVehicle.DataSource = dataTableVehicleOfUser;
            selectVehicle.DataValueField = "VehiclePlate";
            selectVehicle.DataTextField = "VehiclePlate";
            selectVehicle.DataBind();
        }

        protected Reservation CreateReservation()
        {
            Reservation reservation = new Reservation();
            Space space = new Space();
            User user = new User();
            Vehicle vehicle = new Vehicle();
            DateTime checkIn = DateTime.Now;
            DateTime checkOut = DateTime.Now;
            TimeSpan initialHour = new TimeSpan();
            TimeSpan finalHour = new TimeSpan();
            try
            {
                if (hiddenCampusValue.Value.Equals(string.Empty))
                    space.ParkingCampusId = Convert.ToInt32(selectCampus.Items[0].Value);
                else
                    space.ParkingCampusId = Convert.ToInt32(hiddenCampusValue.Value);
            }
            catch (FormatException)
            {
                buttonStyle.buttonStyleRed(buttonErrors, "The campus select is empty.");
            }

            try
            {
                if (hiddenParkingValue.Value.Equals(string.Empty))
                    space.ParkingId = Convert.ToInt32(selectParking.Items[0].Value);
                else
                    space.ParkingId = Convert.ToInt32(hiddenParkingValue.Value);
            }
            catch (FormatException)
            {
                buttonStyle.buttonStyleRed(buttonErrors, "The parking select is empty.");
            }

            try
            {
                if (hiddenSpaceValue.Value.Equals(string.Empty))
                    space.Id = Convert.ToInt32(selectSpace.Items[0].Value);
                else
                    space.Id = Convert.ToInt32(hiddenSpaceValue.Value);
            }
            catch (FormatException)
            {
                buttonStyle.buttonStyleRed(buttonErrors, "The space select is empty.");
            }

            try
            {
                if (hiddenVehicleValue.Value.Equals(string.Empty))
                    vehicle.VehiclePlate = selectVehicle.Items[0].Value;
                else
                    vehicle.VehiclePlate = hiddenVehicleValue.Value;
            }
            catch (FormatException)
            {
                buttonStyle.buttonStyleRed(buttonErrors, "The vehicle select is empty.");
            }

            user.Username = Session["User-UserName"].ToString();
            reservation.Space = space;
            reservation.User = user;
            reservation.Vehicle = vehicle;
            initialHour = CreateCheckIn(initialHour);
            finalHour = CreateCheckOut(finalHour);
            checkIn = checkIn.Date + initialHour;
            checkOut = checkOut.Date + finalHour;
            reservation.CheckIn = checkIn;
            reservation.CheckOut = checkOut;

            return reservation;
        }

        protected void GenerateMap_Click(object sender, EventArgs e)
        {
            GenerateMap();
        }

        protected void GenerateMap()
        {
            try
            {
                buttonErrors2.Visible = true;
                buttonErrors2.Value = "Click on a space " + "\n" + " to reserve it.";

                //Building an HTML string.
                StringBuilder html = new StringBuilder();
                Parking parking = new Parking();
                List<Space> spaceList = new List<Space>();
                int parkingHeight;
                int parkingWidth;
                int parkingId;
                int limit = 0;

                //Cleaning last table
                placeHolderMap.Controls.Clear();

                if (hiddenParkingValue.Value.Equals(string.Empty))
                    parkingId = Convert.ToInt32(selectParking.Items[0].Value);
                else
                    parkingId = Convert.ToInt32(hiddenParkingValue.Value);

                parking = parkingData.SendParkingInfo(parkingData.GetParking(parkingId));

                if (parking != null)
                {
                    if (spaceList != null)
                    {
                        for (int i = 0; i < parking.Width; i++)
                        {
                            html.Append("<tr>");
                            for (int j = 0; j < parking.Height; j++)
                            {
                                    html.Append("<td>");
                                    html.Append("<button type='button' onclick='setColorAndValue(this)' id='buttonSpace' value='""'></button>");
                                    html.Append("</td>");
                            }
                            html.Append("</tr>");
                        }
                        //Append the HTML string to Placeholder.
                        placeHolderMap.Controls.Add(new Literal { Text = html.ToString() });
                    }
                    else
                        buttonStyle.buttonStyleRed(buttonErrors2, "Spaces in parking not found.");
                }
                else
                    buttonStyle.buttonStyleRed(buttonErrors2, "Parking not found.");
            }
            catch (Exception)
            {
                buttonStyle.buttonStyleRed(buttonErrors2, "An error ocurred generating the parking.");
            }


        }

        protected TimeSpan CreateCheckIn(TimeSpan initialHour)
        {
            if (hiddenInitialHourValue.Value.Equals(string.Empty))
                initialHour = new TimeSpan(6, 0, 0);
            else
            {
                switch (Convert.ToInt32(hiddenInitialHourValue.Value))
                {
                    case 6:
                        initialHour = new TimeSpan(6, 0, 0);
                        break;
                    case 7:
                        initialHour = new TimeSpan(7, 0, 0);
                        break;
                    case 8:
                        initialHour = new TimeSpan(8, 0, 0);
                        break;
                    case 9:
                        initialHour = new TimeSpan(9, 0, 0);
                        break;
                    case 10:
                        initialHour = new TimeSpan(10, 0, 0);
                        break;
                    case 11:
                        initialHour = new TimeSpan(11, 0, 0);
                        break;
                    case 12:
                        initialHour = new TimeSpan(12, 0, 0);
                        break;
                    case 13:
                        initialHour = new TimeSpan(13, 0, 0);
                        break;
                    case 14:
                        initialHour = new TimeSpan(14, 0, 0);
                        break;
                    case 15:
                        initialHour = new TimeSpan(15, 0, 0);
                        break;
                    case 16:
                        initialHour = new TimeSpan(16, 0, 0);
                        break;
                    case 17:
                        initialHour = new TimeSpan(17, 0, 0);
                        break;
                    case 18:
                        initialHour = new TimeSpan(18, 0, 0);
                        break;
                    case 19:
                        initialHour = new TimeSpan(19, 0, 0);
                        break;
                    case 20:
                        initialHour = new TimeSpan(20, 0, 0);
                        break;
                    case 21:
                        initialHour = new TimeSpan(21, 0, 0);
                        break;
                    case 22:
                        initialHour = new TimeSpan(22, 0, 0);
                        break;
                }
            }
            return initialHour;
        }

        protected TimeSpan CreateCheckOut(TimeSpan finalHour)
        {
            if (hiddenFinalHourValue.Value.Equals(string.Empty))
                finalHour = new TimeSpan(6, 0, 0);
            else
            {
                switch (Convert.ToInt32(hiddenFinalHourValue.Value))
                {
                    case 6:
                        finalHour = new TimeSpan(6, 0, 0);
                        break;
                    case 7:
                        finalHour = new TimeSpan(7, 0, 0);
                        break;
                    case 8:
                        finalHour = new TimeSpan(8, 0, 0);
                        break;
                    case 9:
                        finalHour = new TimeSpan(9, 0, 0);
                        break;
                    case 10:
                        finalHour = new TimeSpan(10, 0, 0);
                        break;
                    case 11:
                        finalHour = new TimeSpan(11, 0, 0);
                        break;
                    case 12:
                        finalHour = new TimeSpan(12, 0, 0);
                        break;
                    case 13:
                        finalHour = new TimeSpan(13, 0, 0);
                        break;
                    case 14:
                        finalHour = new TimeSpan(14, 0, 0);
                        break;
                    case 15:
                        finalHour = new TimeSpan(15, 0, 0);
                        break;
                    case 16:
                        finalHour = new TimeSpan(16, 0, 0);
                        break;
                    case 17:
                        finalHour = new TimeSpan(17, 0, 0);
                        break;
                    case 18:
                        finalHour = new TimeSpan(18, 0, 0);
                        break;
                    case 19:
                        finalHour = new TimeSpan(19, 0, 0);
                        break;
                    case 20:
                        finalHour = new TimeSpan(20, 0, 0);
                        break;
                    case 21:
                        finalHour = new TimeSpan(21, 0, 0);
                        break;
                    case 22:
                        finalHour = new TimeSpan(22, 0, 0);
                        break;
                }
            }
            return finalHour;
        }

        protected void ButtonReserve_Click(object sender, EventArgs e)
        {
            CheckActiveReservation(CreateReservation());
            FillTable();
        }

        protected void FillTable()
        {
            DataTable dt = new DataTable();
            if (selectCampusToView.Items.Count > 0)
            {
                ReservationData reservationData = new ReservationData();
                int campusId;
                //Populating a DataTable from database.
                if (hiddenCampusToViewValue.Value.Equals(string.Empty))
                    campusId = Convert.ToInt32(selectCampusToView.Items[0].Value);
                else
                    campusId = Convert.ToInt32(hiddenCampusToViewValue.Value);

                if (Convert.ToInt32(Session["User-RoleId"]) == 3)
                    dt = reservationData.DataTableReservation(campusId);
                else
                    dt = reservationData.DataTableUserReservation(Session["User-UserName"].ToString());
            }
            //Building an HTML string.
            StringBuilder html = new StringBuilder();

            //Building the Header row.
            html.Append("<tbody>");
            html.Append("<tr>");
            html.Append("<tbody>");
            html.Append("<tr>");
            html.Append("<th>");
            html.Append("Id");
            html.Append("</th>");
            html.Append("<th>");
            html.Append("Space");
            html.Append("</th>");
            html.Append("<th>");
            html.Append("Parking");
            html.Append("</th>");
            html.Append("<th>");
            html.Append("Campus");
            html.Append("</th>");
            html.Append("<th>");
            html.Append("User");
            html.Append("</th>");
            html.Append("<th>");
            html.Append("vehicle Plate");
            html.Append("</th>");
            html.Append("<th>");
            html.Append("Initial Hour");
            html.Append("</th>");
            html.Append("<th>");
            html.Append("Final Hour");
            html.Append("</th>");
            /*foreach (DataColumn column in dt.Columns)
            {
                html.Append("<th>");
                html.Append(column.ColumnName);
                html.Append("</th>");
            }*/
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
            placeHolderTableReservations.Controls.Clear();
            placeHolderTableReservations.Controls.Add(new Literal { Text = html.ToString() });
        }

        protected void InsertReservation(Reservation reservation)
        {
            if (reservation != null)
            {
                switch (reservationRules.InsertReservation(reservation))
                {
                    case 0:
                        buttonStyle.buttonStyleBlue(buttonErrors, "Space reserved sucessful.");
                        break;
                    case 1:
                        buttonStyle.buttonStyleWhite(buttonErrors, "The campus select is empty.");
                        break;
                    case 2:
                        buttonStyle.buttonStyleRed(buttonErrors, "The parking select is empty.");
                        break;
                    case 3:
                        buttonStyle.buttonStyleWhite(buttonErrors, "The space select is empty.");
                        break;
                    case 4:
                        buttonStyle.buttonStyleRed(buttonErrors, "The user name is empty.");
                        break;
                    case 5:
                        buttonStyle.buttonStyleWhite(buttonErrors, "An error ocurred during your reservation.");
                        break;
                    case 6:
                        buttonStyle.buttonStyleRed(buttonErrors, "The vehicle plate empty.");
                        break;
                    case 7:
                        buttonStyle.buttonStyleWhite(buttonErrors, "The initial hour empty.");
                        break;
                    case 8:
                        buttonStyle.buttonStyleRed(buttonErrors, "The final hour is empty.");
                        break;
                    case 9:
                        buttonStyle.buttonStyleRed(buttonErrors, "The initial hour can't be highter than the final hour.");
                        break;
                    case 10:
                        buttonStyle.buttonStyleRed(buttonErrors, "The initial hour can't be less than the actual hour.");
                        break;
                    case 11:
                        buttonStyle.buttonStyleRed(buttonErrors, "The initial hour can't be equals than the final hour.");
                        break;
                }
            }
        }

        protected void CheckUserReservation(Reservation reservation)
        {
            DataTable dt = reservationData.DataTableUserReservation(Session["User-UserName"].ToString());
            if (dt.Rows.Count > 0)
            {
                if (Convert.ToDateTime(dt.Rows[0]["CheckOut"]) > DateTime.Now)
                {
                    buttonStyle.buttonStyleRed(buttonErrors, "You can't have more than one reserve active.");
                }
                else
                {
                    InsertReservation(reservation);
                }
            }
            else
                InsertReservation(reservation);

        }

        protected void CheckActiveReservation(Reservation reservation)
        {
            switch (reservationRules.SearchActiveReservation(reservation))
            {
                case 0:
                    CheckUserReservation(reservation);
                    break;
                case 1:
                    buttonStyle.buttonStyleRed(buttonErrors, "The space selected already have reservations in that hours.");
                    break;
            }
        }
    }
}

