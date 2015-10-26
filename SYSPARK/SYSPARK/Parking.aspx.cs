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
                        buttonErrors.Value = "You can't enter zero in al space fields.";
                        break;
                    case 8:
                        buttonErrorsStyleWhite();
                        buttonErrors.Value = "The sum of space fields can't be more than the total space.";
                        break;
                }
            }
        }

        public void buttonErrorsStyleRed()
        {
            buttonErrors.Visible = true;
            buttonErrors.Style.Add("background-color", "red");
            buttonErrors.Style.Add("color", "white");
        }

        public void buttonErrorsStyleBlue()
        {
            buttonErrors.Visible = true;
            buttonErrors.Style.Add("background-color", "blue");
            buttonErrors.Style.Add("color", "white");
        }

        public void buttonErrorsStyleWhite()
        {
            buttonErrors.Visible = true;
            buttonErrors.Style.Add("background-color", "white");
            buttonErrors.Style.Add("color", "red");
        }

        public void clearControls()
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