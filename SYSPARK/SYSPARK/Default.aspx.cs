using SYSPARK.BussinessRules;
using SYSPARK.Data;
using SYSPARK.Entities;
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
            CheckUserNameAndPassword();
        }

        protected void saveSession()
        {
            //Saving th user data for use in all the app
            UserData userData = new UserData();
            User user = userData.sendUser(userData.getUser(textBoxUsername.Value));
            Session["User-Id"] = user.Id;
            Session["User-Name"] = user.Name;
            Session["User-LastName"] = user.LastName;
            Session["User-UserName"] = user.Username;
            Session["User-Password"] = textBoxPassword.Value;
            Session["User-PasswordHashed"] = user.Password;
            Session["User-ConditionId"] = user.Condition.Id;
        }

        protected void buttonErrorsStyleRed()
        {
            buttonErrors.Style.Add("background-color", "red");
            buttonErrors.Style.Add("color", "white");
        }

        protected void buttonErrorsStyleBlue()
        {
            buttonErrors.Style.Add("background-color", "blue");
            buttonErrors.Style.Add("color", "white");
        }

        protected void buttonErrorsStyleWhite()
        {
            buttonErrors.Style.Add("background-color", "white");
            buttonErrors.Style.Add("color", "red");
        }

        protected void CheckUserNameAndPassword()
        {
            LoginBussinessRules login = new LoginBussinessRules();
            string username = textBoxUsername.Value;
            string password = textBoxPassword.Value;

            switch (login.ValidateFields(username, password))
            {
                case 0:
                    buttonErrorsStyleWhite();
                    buttonErrors.Value = "The username field is empty.";
                    break;
                case 1:
                    buttonErrorsStyleRed();
                    buttonErrors.Value = "The password field is empty.";
                    break;
                case 2:
                    if (login.LoginUserName(username) == true)
                    {
                        if (login.LoginPassword(password, username) == true)
                        {
                            saveSession();
                            textBoxPassword.Value = string.Empty;
                            Response.Redirect("Home.aspx");
                        }
                        else
                        {
                            buttonErrorsStyleWhite();
                            buttonErrors.Value = "Invalid password.";
                            textBoxPassword.Value = string.Empty;
                        }
                    }
                    else
                    {
                        buttonErrorsStyleRed();
                        buttonErrors.Value = "Invalid username.";
                    }
                    break;
            }
        }
    }
}