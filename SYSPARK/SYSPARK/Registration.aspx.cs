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
        protected void Page_Load(object sender, EventArgs e)
        {
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
            InsertUser(createUser());
        }

        protected void InsertUser(User user)
        {
            UserBussinessRules userBussinessRules = new UserBussinessRules();
            CodeData codeData = new CodeData();
            if (Convert.ToInt32(hiddenConditionValue.Value) > 1)
            {
                if (textboxCode.Value != "")
                {
                    switch (codeData.sendDecision(codeData.getCode(textboxCode.Value)))
                    {
                        case 0:
                            codeData.updateCode(textboxCode.Value, 1);
                            switch (userBussinessRules.RegistrationRules(user))
                            {
                                case 0:
                                    hiddenTransaction.Value = "Transaction successful.";
                                    Response.Redirect("Default.aspx");
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
                else
                {
                    buttonErrorsStyleRed();
                    buttonErrors.Value = "The code field is empty.";
                }
            }
            else
            {
                switch (userBussinessRules.RegistrationRules(user))
                {
                    case 0:
                        Response.Redirect("Default.aspx");
                        break;
                    case 1:
                        buttonErrorsStyleRed();
                        buttonErrors.Value = "The name field is empty.";
                        break;
                    case 2:
                        buttonErrorsStyleWhite();
                        buttonErrors.Value = "The lastname field is empty.";
                        break;
                    case 3:
                        buttonErrorsStyleRed();
                        buttonErrors.Value = "The username field is empty.";
                        break;
                    case 4:
                        buttonErrorsStyleWhite();
                        buttonErrors.Value = "The password field is empty.";
                        break;
                    case 5:
                        buttonErrorsStyleRed();
                        buttonErrors.Value = "An error ocurred during your registration.";
                        break;
                }
            }
        }

        protected User createUser()
        {
            User user = new User();
            Condition condition = new Condition();
            //User
            user.Name = textboxName.Value;
            user.LastName = textboxLastName.Value;
            user.Username = textboxUsername.Value;
            user.Password = textboxPassword.Value;
            //Condition

            condition.Id = Convert.ToInt32(hiddenConditionValue.Value);
            condition.Description = selectCondition.Items.FindByValue(hiddenConditionValue.Value).Text;
            //Inserting registration data
            user.Condition = condition;
            return user;
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
    }
}
