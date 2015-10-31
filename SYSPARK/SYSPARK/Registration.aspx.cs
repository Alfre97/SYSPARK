using SYSPARK.App_Entities;
using SYSPARK.App_Utility;
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
    public partial class Register : System.Web.UI.Page
    {
        ButtonStyle buttonStyle = new ButtonStyle();
        protected void Page_Load(object sender, EventArgs e)
        {
            Session["RegistrationUserName"] = string.Empty;
            Session["RegistrationPassword"] = string.Empty;
            Session["HiddenTransaction"] = string.Empty;
            //Select conditionType
            RoleData roleData = new RoleData();
            DataTable condition = roleData.DataTableRole();
            selectCondition.DataSource = condition;
            selectCondition.DataValueField = "Id";
            selectCondition.DataTextField = "Description";
            selectCondition.DataBind();
        }

        protected void buttonRegister_Click(object sender, EventArgs e)
        {
            Validation();
        }

        protected void Validation()
        {
            //Declare variables
            UniversityCardData UCardData = new UniversityCardData();
            DataTable dataTableUCard = new DataTable();
            if (hiddenVisibleValue.Value.Equals("1"))
            {
                if (textboxCode.Value.Equals(string.Empty))
                    buttonStyle.buttonStyleRed(buttonErrors, "University card field is empty.");
                else
                {
                    UniversityCard universityCard = new UniversityCard();
                    universityCard.Card = textboxCode.Value;
                    dataTableUCard = UCardData.getCard(universityCard);
                    if (dataTableUCard.Rows.Count > 0)
                    {
                        if (Convert.ToInt32(dataTableUCard.Rows[0]["Used"]) == 0)
                        {
                            if (Convert.ToInt32(dataTableUCard.Rows[0]["RoleId"]) == Convert.ToInt32(hiddenConditionValue.Value))
                            {
                                InsertUser();
                                UpdateUniversityCard(dataTableUCard);
                            }
                            else
                                buttonStyle.buttonStyleWhite(buttonErrors, "You can't use this code for this role.");
                        }
                        else
                            buttonStyle.buttonStyleRed(buttonErrors, "The entered code is already used.");
                    }
                    else
                        buttonStyle.buttonStyleWhite(buttonErrors, "The entered code does not exist.");
                }
            }
            else
            {
                InsertUser();
            }
        }

        protected void InsertUser()
        {
            User user = CreateUser();
            if (user != null)
            {
                UserBussinessRules userBussinessRules = new UserBussinessRules();
                switch (userBussinessRules.RegistrationRules(user))
                {
                    case 0:
                        hiddenTransaction.Value = "Transaction successful.";
                        Session["HiddenTransaction"] = hiddenTransaction.Value;
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
                }
            }
        }

        protected void UpdateUniversityCard(DataTable dataTableUCard)
        {
            UniversityCardData uCardData = new UniversityCardData();
            uCardData.UpdateCard(CreateUCard(dataTableUCard));
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
                //Role
                role.Id = Convert.ToInt32(hiddenConditionValue.Value);
                //Inserting registration data
                user.Role = role;
                return user;
            }
            catch (FormatException)
            {
                buttonStyle.buttonStyleRed(buttonErrors, "Invalid data, please check it or contact with us.");
                return null;
            }
        }

        protected UniversityCard CreateUCard(DataTable dataTableUCard)
        {
            UniversityCard uCard = new UniversityCard();
            UserData userData = new UserData();
            User user = userData.sendUser(userData.getUser(textboxUsernameR.Value));
            uCard.Card = dataTableUCard.Rows[0]["UniversityCard"].ToString();
            uCard.UserId = user.Id;
            uCard.Used = 1;
            return uCard;
        }
    }
}
