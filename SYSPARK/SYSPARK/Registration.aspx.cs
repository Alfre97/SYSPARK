using SYSPARK.App_Utility;
using SYSPARK.BussinessRules;
using SYSPARK.Data;
using SYSPARK.Entities;
using System;
using System.Data;

namespace SYSPARK
{
    public partial class Register : System.Web.UI.Page
    {
        ButtonStyle buttonStyle = new ButtonStyle();

        protected void Page_Load(object sender, EventArgs e)
        {
            //if (Session["User-Id"] == null)
            //Response.Redirect("Default.aspx");

            Session["RegistrationUserName"] = string.Empty;
            Session["RegistrationPassword"] = string.Empty;
            Session["HiddenTransaction"] = string.Empty;

            //Select conditionType
            RoleData roleData = new RoleData();
            selectCondition.DataSource = roleData.DataTableRole();
            selectCondition.DataValueField = "Id";
            selectCondition.DataTextField = "Description";
            selectCondition.DataBind();

            //Select campus
            CampusData campusData = new CampusData();
            selectCampus.DataSource = campusData.DataTableCampus();
            selectCampus.DataValueField = "Id";
            selectCampus.DataTextField = "Description";
            selectCampus.DataBind();
        }

        protected void ButtonRegister_Click(object sender, EventArgs e)
        {
            InsertUser(CreateUser());
        }

        protected void InsertUser(User user)
        {
            if (user != null)
            {
                UserBussinessRules userBussinessRules = new UserBussinessRules();
                switch (userBussinessRules.RegistrationRules(user))
                {
                    case 0:
                        Session["HiddenTransaction"] = 1;
                        Response.Redirect("Default.aspx");
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
                        buttonStyle.buttonStyleWhite(buttonErrors, "An error ocurred during your registration.");
                        break;
                    case 6:
                        buttonStyle.buttonStyleRed(buttonErrors, "The university card field is empty.");
                        break;
                }
            }
        }

        protected User CreateUser()
        {
            User user = new User();
            try
            {
                //User
                user.Name = textboxName.Value;
                user.LastName = textboxLastName.Value;
                user.Username = textboxUsernameR.Value;
                user.Password = textboxPasswordR.Value;
                if (hiddenConditionValue.Value.Equals(string.Empty))
                {
                    buttonStyle.buttonStyleWhite(buttonErrors, "Select, one role please.");
                    return null;
                }
                else
                {
                    user.Role.Id = Convert.ToInt32(hiddenConditionValue.Value);
                    if (hiddenCampusValue.Value.Equals(string.Empty))
                    {
                        buttonStyle.buttonStyleWhite(buttonErrors, "Select, one campus please.");
                        return null;
                    }
                    else
                        user.Campus.Id = Convert.ToInt32(hiddenCampusValue.Value);
                }
                user.UniversityCard = Convert.ToInt32(textboxUniversityCard.Value);
                return user;
            }
            catch (FormatException)
            {
                buttonStyle.buttonStyleRed(buttonErrors, "Invalid data, please check it or contact with us.");
                return null;
            }
        }
    }
}
