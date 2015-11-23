using SYSPARK.App_Entities;
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
            //if (Session["User-UserName"] == null)
            //Response.Redirect("Default.aspx");

            if (Session["hiddenTransaction"] != null)
            {
                buttonStyle.buttonStyleBlue(buttonErrors, "Registration successful.");
                Session["HiddenTransaction"] = string.Empty;
            }

            FillSelectCondition();
            FillSelectCampus();
        }

        protected void FillSelectCondition()
        {
            //Select conditionType
            RoleData roleData = new RoleData();
            DataTable dataTableRole = roleData.DataTableRole();
            selectCondition.DataSource = dataTableRole;
            selectCondition.DataValueField = "Id";
            selectCondition.DataTextField = "Description";
            selectCondition.DataBind();
        }

        protected void FillSelectCampus()
        {
            //Select campus
            CampusData campusData = new CampusData();
            DataTable dataTableCampus = campusData.DataTableCampus();
            selectCampus.DataSource = dataTableCampus;
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
                        textboxName.Value = string.Empty;
                        textboxLastName.Value = string.Empty;
                        textboxUsernameR.Value = string.Empty;
                        textboxPasswordR.Value = string.Empty;
                        textboxUniversityCard.Value = string.Empty;
                        buttonStyle.buttonStyleBlue(buttonErrors, "User registered sucessful.");
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
            Role role = new Role();
            Campus campus = new Campus();
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
                    role.Id = Convert.ToInt32(hiddenConditionValue.Value);
                    user.Role = role;
                    if (hiddenCampusValue.Value.Equals(string.Empty))
                    {
                        buttonStyle.buttonStyleWhite(buttonErrors, "Select, one campus please.");
                        return null;
                    }
                    else
                        campus.Id = Convert.ToInt32(hiddenCampusValue.Value);
                    user.Campus = campus;
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
