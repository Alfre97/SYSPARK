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

            FillSelectCondition();
            FillSelectCampus();
        }

        protected void FillSelectCondition()
        {
            //Select conditionType
            RoleData roleData = new RoleData();
            selectCondition.DataSource = roleData.DataTableRole();
            selectCondition.DataValueField = "Id";
            selectCondition.DataTextField = "Name";
            selectCondition.DataBind();
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

        protected void ButtonRegister_Click(object sender, EventArgs e)
        {
            InsertUser(CreateUser(), CreateCampus());
        }

        protected void InsertUser(User user, Campus campus)
        {
            if (user != null)
            {
                if (campus != null)
                {
                    UserBussinessRules userBussinessRules = new UserBussinessRules();
                    switch (userBussinessRules.RegistrationRules(user, campus))
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
                        case 7:
                            buttonStyle.buttonStyleWhite(buttonErrors, "The campus id field is empty.");
                            break;
                        case 8:
                            buttonStyle.buttonStyleRed(buttonErrors, "The campus name field is empty.");
                            break;
                    }
                }
            }
        }

        protected User CreateUser()
        {
            User user = new User();
            Role role = new Role();
            try
            {
                //User
                user.Name = textboxName.Value;
                user.LastName = textboxLastName.Value;
                user.Username = textboxUsernameR.Value;
                user.Password = textboxPasswordR.Value;
                if (hiddenConditionValue.Value.Equals(string.Empty))
                {
                    role.Id = Convert.ToInt32(selectCondition.Items[0].Value);
                    role.Name = selectCondition.Items[0].Text;
                }
                else
                {
                    role.Id = Convert.ToInt32(hiddenConditionValue.Value);
                    role.Name = selectCondition.Items.FindByValue(hiddenConditionValue.Value).Text;
                }
                user.Role = role;
                user.UniversityCard = Convert.ToInt32(textboxUniversityCard.Value);
                return user;
            }
            catch (FormatException)
            {
                buttonStyle.buttonStyleRed(buttonErrors, "Invalid data, please check it or contact with us.");
                return null;
            }
        }

        protected Campus CreateCampus()
        {
            try
            {
                Campus campus = new Campus();
                if (hiddenCampusValue.Value.Equals(string.Empty))
                {
                    campus.Id = Convert.ToInt32(selectCampus.Items[0].Value);
                    campus.Name = selectCampus.Items[0].Text;
                }
                else
                {
                    campus.Id = Convert.ToInt32(hiddenCampusValue.Value);
                    campus.Name = selectCampus.Items.FindByValue(hiddenCampusValue.Value).Text;
                }
                return campus;
            }
            catch (FormatException)
            {
                buttonStyle.buttonStyleRed(buttonErrors, "An error ocurred adding the user to a campus.");
                return null;
            }
        }
    }
}
