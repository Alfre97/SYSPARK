using SYSPARK.App_BussinessRules;
using SYSPARK.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SYSPARK
{
    public partial class Paking : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["User-Id"] == null)
                Response.Redirect("Default.aspx");
        }

        public void AddParking_Click(object sender, EventArgs e)
        {
            InsertAndExceptions();
        }

        public Parking createParking()
        {
            Parking parking = new Parking();
            ParkingBussinessRules parkingBussinessRules = new ParkingBussinessRules();
            SpaceData spaceData = new SpaceData();
            try
            {
                if (textboxTotalSpace.Value.Equals(string.Empty))
                {
                    buttonErrorsStyleRed();
                    buttonErrors.Value = "The total space field can't be empty.";
                }
                else if (textboxCarSpace.Value.Equals(string.Empty))
                {
                    buttonErrorsStyleRed();
                    buttonErrors.Value = "The car space field can't be empty.";
                }
                else if (textboxMotorCycleSpace.Value.Equals(string.Empty))
                {
                    buttonErrorsStyleWhite();
                    buttonErrors.Value = "The motorcycle space field can't be empty.";
                }
                else if (textboxBusSpace.Value.Equals(string.Empty))
                {
                    buttonErrorsStyleRed();
                    buttonErrors.Value = "The bus space field can't be empty.";
                }
                else if (textboxHandicapSpace.Value.Equals(string.Empty))
                {
                    buttonErrorsStyleRed();
                    buttonErrors.Value = "The handicap space field can't be empty.";
                }
                parking.Name = textboxParkingName.Value;
                parking.TotalSpace = Convert.ToInt32(textboxTotalSpace.Value);
                parking.CarSpace = Convert.ToInt32(textboxCarSpace.Value);
                parking.MotorcycleSpace = Convert.ToInt32(textboxMotorCycleSpace.Value);
                parking.HandicapSpace = Convert.ToInt32(textboxHandicapSpace.Value);
                parking.BusSpace = Convert.ToInt32(textboxBusSpace.Value);
                return parking;
            }
            catch (FormatException)
            {
                buttonErrorsStyleRed();
                buttonErrors.Value = "Invalid data. You can't enter \n only numbers in parking name," + "\n" + "letters in space fields," + "\n " + "or empty fields.";
                return null;
            }
        }

        public void InsertAndExceptions()
        {
            Parking parking = createParking();
            ParkingBussinessRules parkingBussinessRules = new ParkingBussinessRules();
            SpaceData spaceData = new SpaceData();
            if (parking != null)
            {
                switch (parkingBussinessRules.InsertParking(parking))
                {
                    case 0:
                        clearControls();
                        buttonErrorsStyleBlue();
                        buttonErrors.Value = "Parking created sucessfully.";
                        break;
                    case 1:
                        buttonErrorsStyleRed();
                        buttonErrors.Value = "The parking name field is empty.";
                        break;
                    case 2:
                        buttonErrorsStyleRed();
                        buttonErrors.Value = "The total space field can't be less or equal than zero";
                        break;
                    case 3:
                        buttonErrorsStyleRed();
                        buttonErrors.Value = "The car space field can't be less than zero.";
                        break;
                    case 4:
                        buttonErrorsStyleWhite();
                        buttonErrors.Value = "The motorcycle space field can't be less than zero.";
                        break;
                    case 5:
                        buttonErrorsStyleRed();
                        buttonErrors.Value = "The handicap space field can't be less than zero.";
                        break;
                    case 6:
                        buttonErrorsStyleWhite();
                        buttonErrors.Value = "The bus space field can't be less than zero.";
                        break;
                    case 7:
                        buttonErrorsStyleRed();
                        buttonErrors.Value = "You can't enter zero in all space fields.";
                        break;
                    case 8:
                        buttonErrorsStyleWhite();
                        buttonErrors.Value = "The sum of space fields can't be higher than the total space.";
                        break;
                }
            }
        }

        protected void buttonErrorsStyleRed()
        {
            buttonErrors.Visible = true;
            buttonErrors.Style.Add("background-color", "red");
            buttonErrors.Style.Add("color", "white");
        }

        protected void buttonErrorsStyleBlue()
        {
            buttonErrors.Visible = true;
            buttonErrors.Style.Add("background-color", "blue");
            buttonErrors.Style.Add("color", "white");
        }

        protected void buttonErrorsStyleWhite()
        {
            buttonErrors.Visible = true;
            buttonErrors.Style.Add("background-color", "white");
            buttonErrors.Style.Add("color", "red");
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