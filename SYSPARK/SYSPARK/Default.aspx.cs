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
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void enterButton_Click(object sender, EventArgs e)
        {
            LoginData login = new LoginData();
            string username = textBoxUsername.Value;
            string password = textBoxPassword.Value;

            buttonErrors.Style.Add("background-color", "white");
            buttonErrors.Value = username;

            if (login.CheckUserName(username) == true)
                {
                if (login.CheckPassword(password) == true)
                {
                    Response.Redirect("Home.aspx");
                    buttonErrors.Value = "Access granted";
                }
                else
                {
                    buttonErrors.Style.Add("background-color", "white");
                    buttonErrors.Value = "Invalid password.";
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