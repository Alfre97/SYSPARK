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
            CodeData registrationData = new CodeData();
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
            

            if(textboxCode.Value != "")
            {
                switch (registrationData.sendDecision(registrationData.getCode(textboxCode.Value)))
                {
                    case 0:
                        Response.Redirect("Default.aspx");
                        break;
                    case 1:
                        buttonErrors.Style.Add("background-color", "red");
                        buttonErrors.Style.Add("color", "white");
                        buttonErrors.Value = "The provide a valid code.";
                        break;
                    case 2:
                        buttonErrors.Style.Add("background-color", "white");
                        buttonErrors.Style.Add("color", "red");
                        buttonErrors.Value = "The entered code is already used.";
                        break;
                }
                switch (userBussinessRules.RegistrationRules(user))
                {
                    case 0:
                        Response.Redirect("Default.aspx");
                        break;
                    case 1:
                        buttonErrors.Style.Add("background-color", "red");
                        buttonErrors.Style.Add("color", "white");
                        buttonErrors.Value = "Please, check your entered data." + "\n" +
                            "Remember you can't enter numbers in the fields.";
                        break;
                }
            }

            
        }
    }
}