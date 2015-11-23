using SYSPARK.App_Utility;
using SYSPARK.BussinessRules;
using SYSPARK.Data;
using SYSPARK.Entities;
using System;
using System.Data;

namespace SYSPARK
{
    public partial class Profile : System.Web.UI.Page
    {
        ButtonStyle buttonStyle = new ButtonStyle();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["User-UserName"] == null)
                Response.Redirect("Default.aspx");

            if (Session["VehicleInserted"] != null)
            {
                buttonStyle.buttonStyleBlue(buttonErrors, "Vehicle added successfully.");
            }

            FillSelectCampus();
            FillSelectVehicle();
            FillSelectCondition();
            FillTableWithUserInfo();
        }

        protected void FillSelectCampus()
        {
            //Select Campus
            CampusData campusData = new CampusData();
            selectCampus.DataSource = campusData.DataTableCampus();
            selectCampus.DataValueField = "Id";
            selectCampus.DataTextField = "Description";
            selectCampus.DataBind();
        }

        protected void FillSelectVehicle()
        {
            //Fill select my cars
            VehicleData vehicleData = new VehicleData();
            DataTable dataTableVehicleOfUser = vehicleData.DataTableUserVehicle(vehicleData.SendVehicleList(vehicleData.GetUserVehicle(Session["User-UserName"].ToString())));
            selectVehicle.DataSource = dataTableVehicleOfUser;
            selectVehicle.DataValueField = "Id";
            selectVehicle.DataTextField = "VehiclePlate";
            selectVehicle.DataBind();
        }

        protected void FillSelectCondition()
        {
            //Fill select condition
            RoleData roleData = new RoleData();
            DataTable Condition = roleData.DataTableRole();
            selectCondition.DataSource = Condition;
            selectCondition.DataValueField = "Id";
            selectCondition.DataTextField = "Description";
            selectCondition.DataBind();
        }

        protected void FillTableWithUserInfo()
        {
            textboxName.Value = Session["User-Name"].ToString();
            textboxLastName.Value = Session["User-LastName"].ToString();
            textboxUsername.Value = Session["User-UserName"].ToString();
            textboxPasswordShowed.Value = Session["User-Password"].ToString();
            hiddenConditionValue.Value = Session["User-ConditionId"].ToString();
            selectCondition.Value = Session["User-ConditionId"].ToString();
            textboxUniversityCard.Value = Session["User-UniversityCard"].ToString();
            selectCampus.Value = Session["User-CampusId"].ToString();
        }

        protected void ButtonUpdateMyInfo_Click(object sender, EventArgs e)
        {
            EnableControls();
        }

        protected void ButtonUpdate_Click(object sender, EventArgs e)
        {
            UpdateUser();
        }

        protected void ButtonCancelUpdate_Click(object sender, EventArgs e)
        {
            DisablingControls();
            FillTableWithUserInfo();
        }

        protected void UpdateUser()
        {
            UserBussinessRules userBussinessRules = new UserBussinessRules();
            //For password validation
            string newPassword = textboxPasswordShowed.Value;
            string passwordHashed = Session["User-PasswordHashed"].ToString();
            bool verify = BCrypt.Net.BCrypt.Verify(newPassword, passwordHashed);
            //Updating user
            switch (userBussinessRules.UpdateRules(CreateUser()))
            {
                case 0:
                    if (verify == false)
                    {
                        hiddenUpdate.Value = "Transaction successful.";
                        Session["UpdateTransaction"] = hiddenUpdate.Value;
                        Response.Redirect("Default.aspx");
                    }
                    else
                    {
                        Session["User-LastName"] = textboxLastName.Value;
                        FillTableWithUserInfo();
                    }

                    break;
                case 1:
                    buttonStyle.buttonStyleWhite(buttonErrors, "The name field is empty.");
                    break;
                case 2:
                    buttonStyle.buttonStyleRed(buttonErrors, "The lastname field is empty.");
                    break;
                case 3:
                    buttonStyle.buttonStyleWhite(buttonErrors, "The username field is empty.");
                    break;
                case 4:
                    buttonStyle.buttonStyleRed(buttonErrors, "The password field is empty.");
                    break;
                case 5:
                    buttonStyle.buttonStyleWhite(buttonErrors, "An error ocurred during your update.");
                    break;
                case 6:
                    buttonStyle.buttonStyleRed(buttonErrors, "The university card field is empty.");
                    break;
            }
        }

        protected User CreateUser()
        {
            User user = new User();
            try
            {
                //Creating user
                user.Name = textboxName.Value;
                user.LastName = textboxLastName.Value;
                user.Username = textboxUsername.Value;
                user.Password = textboxPasswordShowed.Value;
                user.Role.Id = Convert.ToInt32(Session["User-ConditionId"]);
                user.UniversityCard = Convert.ToInt32(textboxUniversityCard.Value);
                user.Campus.Id = Convert.ToInt32(Session["User-CampusId"]);
                return user;
            }
            catch
            {
                buttonStyle.buttonStyleRed(buttonErrors, "An error ocurred validating your new data, please check it.");
                return null;
            }
        }

        protected void DisablingControls()
        {
            //Disabling controls
            textboxName.Disabled = true;
            textboxLastName.Disabled = true;
            textboxPasswordShowed.Disabled = true;
            buttonAddNewCar.Visible = true;
            buttonUpdateMyInfo.Visible = true;
            trUpdate.Visible = false;
            buttonErrors.Visible = false;
            trFirstOptions.Visible = true;
            trVehicle.Visible = true;
        }

        protected void EnableControls()
        {
            //Enabling controls
            textboxName.Disabled = false;
            textboxLastName.Disabled = false;
            textboxPasswordShowed.Disabled = false;
            selectVehicle.Disabled = false;
            buttonAddNewCar.Visible = false;
            buttonUpdateMyInfo.Visible = false;
            trUpdate.Visible = true;
            buttonErrors.Visible = true;
            trFirstOptions.Visible = false;
            trVehicle.Visible = false;
        }
    }
}