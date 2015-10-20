using SYSPARK.BussinessRules;
using SYSPARK.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Script.Services;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SYSPARK
{
    public partial class Login : Page
    {
        protected void enterButton_Click(object sender, EventArgs e)
        {
            LoginBussinessRules login = new LoginBussinessRules();
            string username = textBoxUsername.Value;
            string password = textBoxPassword.Value;

            if (login.LoginUserName(username) == true)
            {
                if (login.LoginPassword(password) == true)
                {
                    Response.Redirect("Home.aspx");
                    buttonErrors.Value = "Access granted";
                    textBoxPassword.Value = "";
                }
                else
                {
                    buttonErrors.Style.Add("background-color", "white");
                    buttonErrors.Value = "Invalid password.";
                    textBoxPassword.Value = "";
                }
            }
            else
            {
                buttonErrors.Style.Add("background-color", "white");
                buttonErrors.Value = "Invalid username.";
            }
        }
    }
}