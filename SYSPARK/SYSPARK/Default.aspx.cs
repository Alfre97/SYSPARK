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
            Session["UserName"] = textBoxUsername.Value;
            LoginBussinessRules login = new LoginBussinessRules();
            string username = textBoxUsername.Value;
            string password = textBoxPassword.Value;

            switch(login.ValidateFields(username, password)) {
                case 0:
                    buttonErrors.Style.Add("background-color", "white");
                    buttonErrors.Value = "The username field is empty.";
                    break;
                case 1:
                    buttonErrors.Style.Add("background-color", "white");
                    buttonErrors.Value = "The password field is empty.";
                    break;
                case 2:
                    if (login.LoginUserName(username) == true)
                    {
                        if (login.LoginPassword(password, username) == true)
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
                    break;
            }
        }
    }
}