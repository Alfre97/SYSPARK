using SYSPARK.App_Utility;
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
        ButtonStyle buttonStyle = new ButtonStyle();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["UpdateTransaction"] != null)
            {
                buttonStyle.buttonStyleBlue(buttonErrors, "Update successful.");
                Session.Clear();
            }
        }

        protected void EnterButton_Click(object sender, EventArgs e)
        {
            try
            {
                CheckUserNameAndPassword();
            }
            catch (Exception)
            {
                buttonStyle.buttonStyleRed(buttonErrors, "An error ocurred connecting to data base." + "\n" + "Please be patient.");
            }

        }

        protected void SaveSession()
        {
            //Saving th user data for use in all the app
            UserData userData = new UserData();
            User user = userData.sendUser(userData.getUser(textBoxUsername.Value));
            Session["User-Name"] = user.Name;
            Session["User-LastName"] = user.LastName;
            Session["User-UserName"] = user.Username;
            Session["User-Password"] = textBoxPassword.Value;
            Session["User-PasswordHashed"] = user.Password;
            Session["User-RoleId"] = user.Role.Id;
            Session["User-UniversityCard"] = user.UniversityCard;
        }

        protected void CheckUserNameAndPassword()
        {
            LoginBussinessRules login = new LoginBussinessRules();
            string username = textBoxUsername.Value;
            string password = textBoxPassword.Value;

            switch (login.ValidateFields(username, password))
            {
                case 0:
                    buttonStyle.buttonStyleWhite(buttonErrors, "The username field is empty.");
                    break;
                case 1:
                    buttonStyle.buttonStyleRed(buttonErrors, "The password field is empty.");
                    break;
                case 2:
                    if (login.LoginUserName(username) == true)
                    {
                        if (login.LoginPassword(password, username) == true)
                        {
                            SaveSession();
                            textBoxPassword.Value = string.Empty;
                            //Response.Redirect("http://syspark.azurewebsites.net/Home.aspx");
                            Response.Redirect("Home.aspx");

                        }
                        else
                        {
                            buttonStyle.buttonStyleWhite(buttonErrors, "Invalid password.");
                            textBoxPassword.Value = string.Empty;
                        }
                    }
                    else
                        buttonStyle.buttonStyleRed(buttonErrors, "Invalid username.");
                    break;
            }
        }
    }
}