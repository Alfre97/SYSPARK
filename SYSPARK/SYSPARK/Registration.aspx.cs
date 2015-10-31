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
                    UniversityCard univarsityCard = new UniversityCard(); 
                    univarsityCard.Card = textboxCode.Value;
                    dataTableUCard = UCardData.getCard(univarsityCard); 
                    if (dataTableUCard.Rows.Count > 0)
                    {
                            InsertUser();
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
            UserBussinessRules userBussinessRules = new UserBussinessRules();
            switch (userBussinessRules.RegistrationRules(CreateUser()))
            {
                case 0:
                    hiddenTransaction.Value = "Transaction successful.";
                    Session["RegistrationUserName"] = textboxUsernameR.Value;
                    Session["RegistrationPassword"] = textboxPasswordR.Value;
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

        protected User CreateUser()
        {
            User user = new User();
            Condition condition = new Condition();
            //User
            user.Name = textboxName.Value;
            user.LastName = textboxLastName.Value;
            user.Username = textboxUsernameR.Value;
            user.Password = textboxPasswordR.Value;
            //Condition

            condition.Id = Convert.ToInt32(hiddenConditionValue.Value);
            condition.Description = selectCondition.Items.FindByValue(hiddenConditionValue.Value).Text;
            //Inserting registration data
            user.Condition = condition;
            return user;
        }
    }
}
