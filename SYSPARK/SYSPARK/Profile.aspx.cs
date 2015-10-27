using SYSPARK.BussinessRules;
using SYSPARK.Data;
using SYSPARK.Entities;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SYSPARK
{
    public partial class Profile : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["User-Id"] == null)
                Response.Redirect("Default.aspx");

            if (Session["VehicleInserted"] != null)
            {
                buttonErrorsStyleBlue();
                buttonErrors.Value = "Vehicle added successfully.";
            }

            //All controls are disable by default
            //Fill select condition
            RoleData roleData = new RoleData();
            DataTable Condition = roleData.DataTableRole();
            selectCondition.DataSource = Condition;
            selectCondition.DataValueField = "Id";
            selectCondition.DataTextField = "Description";
            selectCondition.DataBind();

            //Fill select my cars
            VehicleData vehicleData = new VehicleData();
            DataTable dataTableVehicleOfUser = vehicleData.GetUserVehicle(Convert.ToInt32(Session["User-Id"]));
            selectVehicle.DataSource = dataTableVehicleOfUser;
            selectVehicle.DataValueField = "Id";
            selectVehicle.DataTextField = "VehiclePlate";
            selectVehicle.DataBind();
            //Fill table with user info
            FillTableWithUserInfo();
        }

        protected void FillTableWithUserInfo()
        {
            textboxName.Value = Session["User-Name"].ToString();
            textboxLastName.Value = Session["User-LastName"].ToString();
            textboxUsername.Value = Session["User-UserName"].ToString();
            textboxPasswordShowed.Value = Session["User-Password"].ToString();
            selectCondition.Value = Session["User-ConditionId"].ToString();
        }

        protected void ButtonUpdateMyInfo_Click(object sender, EventArgs e)
        {
            enableControls();
        }

        protected void ButtonUpdate_Click(object sender, EventArgs e)
        {
            updateUser();
        }

        protected void ButtonCancelUpdate_Click(object sender, EventArgs e)
        {
            disablingControls();
            FillTableWithUserInfo();
        }

        protected void updateUser()
        {
            //Declare variables
            UserBussinessRules userBussinessRules = new UserBussinessRules();
            CodeData codeData = new CodeData();
            try
            {
                //For password validation
                string newPassword = textboxPasswordShowed.Value;
                string passwordHashed = Session["User-PasswordHashed"].ToString();
                bool verify = BCrypt.Net.BCrypt.Verify(newPassword, passwordHashed);
                //Updating user
                switch (codeData.sendDecision(codeData.getCode(textboxCode.Value)))
                {
                    case 0:
                        codeData.updateCode(textboxCode.Value, 1);
                        switch (userBussinessRules.UpdateRules(createUser()))
                        {
                            case 0:
                                if (textboxUsername.Value != Session["User-UserName"].ToString())
                                {
                                    hiddenUpdate.Value = "Transaction successful.";
                                    Session["UpdateTransaction"] = hiddenUpdate.Value;
                                    Response.Redirect("Default.aspx");
                                }
                                else if (verify == false)
                                {
                                    hiddenUpdate.Value = "Transaction successful.";
                                    Session["UpdateTransaction"] = hiddenUpdate.Value;
                                    Response.Redirect("Default.aspx");
                                }
                                else
                                {
                                    Session["User-Name"] = textboxName.Value;
                                    Session["User-LastName"] = textboxLastName.Value;
                                    Session["User-ConditionId"] = hiddenConditionValue.Value;
                                    Response.Redirect("Profile.aspx");
                                }

                                break;
                            case 1:
                                buttonErrorsStyleWhite();
                                buttonErrors.Value = "The name field is empty.";
                                break;
                            case 2:
                                buttonErrorsStyleRed();
                                buttonErrors.Value = "The lastname field is empty.";
                                break;
                            case 3:
                                buttonErrorsStyleWhite();
                                buttonErrors.Value = "The username field is empty.";
                                break;
                            case 4:
                                buttonErrorsStyleRed();
                                buttonErrors.Value = "The password field is empty.";
                                break;
                            case 5:
                                buttonErrorsStyleWhite();
                                buttonErrors.Value = "An error ocurred during your registration.";
                                break;
                        }
                        break;
                    case 1:
                        buttonErrorsStyleRed();
                        buttonErrors.Value = "The provide a valid code.";
                        break;
                    case 2:
                        buttonErrorsStyleWhite();
                        buttonErrors.Value = "The entered code is already used.";
                        break;
                }
            }
            catch (FormatException)
            {
                buttonErrorsStyleRed();
                buttonErrors.Value = "Please, check your entered data." + "\n" +
                    "Remember you can't enter numbers in the fields.";
            }
        }
        protected User createUser()
        {
            User user = new Entities.User();
            Condition condition = new Condition();
            //Creating user
            user.Id = Convert.ToInt32(Session["User-Id"]);
            user.Name = textboxName.Value;
            user.LastName = textboxLastName.Value;
            user.Username = textboxUsername.Value;
            user.Password = textboxPasswordShowed.Value;
            condition.Id = Convert.ToInt32(selectCondition.Value);
            condition.Description = selectCondition.Items.FindByValue(selectCondition.Value).Text;
            user.Condition = condition;
            return user;
        }

        protected void disablingControls()
        {
            //Disabling controls
            textboxName.Disabled = true;
            textboxLastName.Disabled = true;
            textboxUsername.Disabled = true;
            textboxPasswordShowed.Disabled = true;
            buttonAddNewCar.Visible = true;
            buttonUpdateMyInfo.Visible = true;
            trUpdate.Visible = false;
            trErrors.Visible = false;
            trVehicle.Visible = true;
            trCode.Visible = false;
            selectCondition.Disabled = true;
        }

        protected void enableControls()
        {
            //Enabling controls
            textboxName.Disabled = false;
            textboxLastName.Disabled = false;
            textboxUsername.Disabled = false;
            textboxPasswordShowed.Disabled = false;
            selectVehicle.Disabled = true;
            buttonAddNewCar.Visible = false;
            buttonUpdateMyInfo.Visible = false;
            trUpdate.Visible = true;
            trErrors.Visible = true;
            trVehicle.Visible = false;
            trCode.Visible = true;
            selectCondition.Disabled = false;
        }

        protected void buttonErrorsStyleRed()
        {
            buttonErrors.Style.Add("background-color", "red");
            buttonErrors.Style.Add("color", "white");
        }

        protected void buttonErrorsStyleBlue()
        {
            buttonErrors.Style.Add("background-color", "blue");
            buttonErrors.Style.Add("color", "white");
        }

        protected void buttonErrorsStyleWhite()
        {
            buttonErrors.Style.Add("background-color", "white");
            buttonErrors.Style.Add("color", "red");
        }
    }
}