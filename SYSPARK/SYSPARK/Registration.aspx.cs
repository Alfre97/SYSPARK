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
            //User
            User user = new User();
            user.Name = textboxName.Value;
            user.LastName = textboxLastName.Value;
            user.Username = textboxUsername.Value;
            user.Password = textboxPassword.Value;
            //Condition
            Condition condition = new Condition();
            condition.Id = Convert.ToInt32(selectCondition.DataValueField);
            condition.Description = selectCondition.DataTextField;
            //Inserting registration data
            user.Condition = condition;
            try
            {
                RegistrationBussinessRules registration = new RegistrationBussinessRules();
                registration.RegistrationRules(user);
                Response.Redirect("Default.aspx");
            }
            catch (Exception)
            {

            }
        }
    }
}