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
            ConditionData conditionData = new ConditionData();
            DataTable condition = conditionData.DataTableCondition();
            selectCondition.DataSource = condition;
            selectCondition.DataValueField = "Id";
            selectCondition.DataTextField = "Description";
            selectCondition.DataBind();
        }

        protected void buttonRegister_Click(object sender, EventArgs e)
        {
            User user = new User();
            UserBussinessRules userBussinessRules = new UserBussinessRules();
            Condition condition = new Condition();
            CodeData codeData = new CodeData();
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
            
            if(Convert.ToInt32(hiddenConditionValue.Value) > 1)
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
                                    Response.Redirect("Default.aspx");
                                    break;
                                case 1:
                                    buttonErrors.Visible = true;
                                    buttonErrors.Style.Add("background-color", "red");
                                    buttonErrors.Style.Add("color", "white");
                                    buttonErrors.Value = "The name field is empty.";
                                    break;
                                case 2:
                                    buttonErrors.Visible = true;
                                    buttonErrors.Style.Add("background-color", "red");
                                    buttonErrors.Style.Add("color", "white");
                                    buttonErrors.Value = "The lastname field is empty.";
                                    break;
                                case 3:
                                    buttonErrors.Visible = true;
                                    buttonErrors.Style.Add("background-color", "red");
                                    buttonErrors.Style.Add("color", "white");
                                    buttonErrors.Value = "The username field is empty.";
                                    break;
                                case 4:
                                    buttonErrors.Visible = true;
                                    buttonErrors.Style.Add("background-color", "red");
                                    buttonErrors.Style.Add("color", "white");
                                    buttonErrors.Value = "The password field is empty.";
                                    break;
                                case 5:
                                    buttonErrors.Visible = true;
                                    buttonErrors.Style.Add("background-color", "red");
                                    buttonErrors.Style.Add("color", "white");
                                    buttonErrors.Value = "An error ocurred during your registration.";
                                    break;
                            }
                            break;
                        case 1:
                            buttonErrors.Visible = true;
                            buttonErrors.Style.Add("background-color", "red");
                            buttonErrors.Style.Add("color", "white");
                            buttonErrors.Value = "The provide a valid code.";
                            break;
                        case 2:
                            buttonErrors.Visible = true;
                            buttonErrors.Style.Add("background-color", "white");
                            buttonErrors.Style.Add("color", "red");
                            buttonErrors.Value = "The entered code is already used.";
                            break;
                    }
                }
                else
                {
                    buttonErrors.Visible = true;
                    buttonErrors.Style.Add("background-color", "red");
                    buttonErrors.Style.Add("color", "white");
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
                        buttonErrors.Visible = true;
                        buttonErrors.Style.Add("background-color", "red");
                        buttonErrors.Style.Add("color", "white");
                        buttonErrors.Value = "The name field is empty.";
                        break;
                    case 2:
                        buttonErrors.Visible = true;
                        buttonErrors.Style.Add("background-color", "red");
                        buttonErrors.Style.Add("color", "white");
                        buttonErrors.Value = "The lastname field is empty.";
                        break;
                    case 3:
                        buttonErrors.Visible = true;
                        buttonErrors.Style.Add("background-color", "red");
                        buttonErrors.Style.Add("color", "white");
                        buttonErrors.Value = "The username field is empty.";
                        break;
                    case 4:
                        buttonErrors.Visible = true;
                        buttonErrors.Style.Add("background-color", "red");
                        buttonErrors.Style.Add("color", "white");
                        buttonErrors.Value = "The password field is empty.";
                        break;
                    case 5:
                        buttonErrors.Visible = true;
                        buttonErrors.Style.Add("background-color", "red");
                        buttonErrors.Style.Add("color", "white");
                        buttonErrors.Value = "An error ocurred during your registration.";
                        break;
                }
            }
            
        }
    }
}