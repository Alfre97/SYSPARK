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
            if (login.CheckUserName(textBoxUsername.) == true)
            {
                if (login.CheckPassword(textBoxPassword.Value) == true)
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