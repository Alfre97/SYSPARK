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
            selectVehicle.DataTextField = "License";
            selectVehicle.DataBind();
            //Fill table with user info
            FillTableWithUserInfo();
        }

        public void FillTableWithUserInfo()
        {
            textboxName.Value = Session["User-Name"].ToString();
            textboxLastName.Value = Session["User-LastName"].ToString();
            textboxUsername.Value = Session["User-UserName"].ToString();
            textboxPasswordShowed.Value = Session["User-Password"].ToString();
            selectCondition.Value = Session["User-ConditionId"].ToString();
        }

        public void ButtonUpdateMyInfo_Click(object sender, EventArgs e)
        {
            //Enabling controls
            textboxName.Disabled = false;
            textboxLastName.Disabled = false;
            textboxUsername.Disabled = false;
            textboxPasswordShowed.Disabled = false;
            selectVehicle.Disabled = true;
            buttonAddNewCar.Visible = false;
            buttonUpdateMyInfo.Visible = false;
            buttonUpdate.Visible = true;
            buttonErrors.Visible = true;
        }

        public void ButtonUpdate_Click(object sender, EventArgs e)
        {
            //Declare variables
            User user = new Entities.User();
            Condition condition = new Condition();
            UserBussinessRules userBussinessRules = new UserBussinessRules();
            CodeData codeData = new CodeData();
            try
            {
                //Creating user
                user.Id = Convert.ToInt32(Session["User-Id"]);
                user.Name = textboxName.Value;
                user.LastName = textboxLastName.Value;
                user.Username = textboxUsername.Value;
                user.Password = textboxPasswordShowed.Value;
                condition.Id = Convert.ToInt32(selectCondition.Value);
                condition.Description = selectCondition.Items.FindByValue(selectCondition.Value).Text;
                user.Condition = condition;
                //For password validation
                string newPassword = textboxPasswordShowed.Value;
                string passwordHashed = Session["User-PasswordHashed"].ToString();
                bool verify = BCrypt.Net.BCrypt.Verify(newPassword, passwordHashed);
                //Updating user
                switch (userBussinessRules.UpdateRules(user))
                {
                    case 0:
                        if (textboxUsername.Value != Session["User-UserName"].ToString())
                        {
                            Session.Clear();
                            Response.Redirect("Default.aspx");
                        }
                        else if (verify == false)
                        {
                            Session.Clear();
                            Response.Redirect("Default.aspx");
                        }
                        else
                        {
                            Session["User-Name"] = textboxName.Value;
                            Session["User-LastName"] = textboxLastName.Value;
                            Response.Redirect("Profile.aspx");
                            //Disabling controls
                            textboxName.Disabled = true;
                            textboxLastName.Disabled = true;
                            textboxUsername.Disabled = true;
                            textboxPasswordShowed.Disabled = true;
                            buttonAddNewCar.Visible = true;
                            buttonUpdateMyInfo.Visible = true;
                            buttonUpdate.Visible = false;
                            buttonErrors.Visible = false;
                        }

                        break;
                    case 1:
                        buttonErrors.Style.Add("background-color", "red");
                        buttonErrors.Style.Add("color", "white");
                        buttonErrors.Value = "Please, check your entered data." + "\n" +
                            "Remember you can't enter numbers in the fields.";
                        break;
                }
            }
            catch (FormatException)
            {
                buttonErrors.Style.Add("background-color", "red");
                buttonErrors.Style.Add("color", "white");
                buttonErrors.Value = "Please, check your entered data." + "\n" +
                    "Remember you can't enter numbers in the fields.";
            }

        }
    }
}