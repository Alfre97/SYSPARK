using SYSPARK.App_BussinessRules;
using SYSPARK.App_Utility;
using SYSPARK.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SYSPARK
{
    public partial class AddNewCondition : System.Web.UI.Page
    {
        ButtonStyle buttonStyle = new ButtonStyle();

        protected void Page_Load(object sender, EventArgs e)
        {
            //if (Session["User-Id"] == null)
            //Response.Redirect("Default.aspx");
        }

        protected void AddRole_Click(object sender, EventArgs e)
        {
            InsertRole(createRole());
        }

        protected void InsertRole(Role role)
        {
            RoleBussinessRules roleRules = new RoleBussinessRules();
            if (role != null)
            {
                if (textboxRole.Value.Equals(string.Empty))
                    buttonStyle.buttonStyleRed(buttonErrors, "The role name field is empty.");
                else
                    roleRules.InsertRole(role);
            }

        }

        protected Role createRole()
        {
            Role role = new Role();
            try
            {
                role.Description = textboxRole.Value;
                return role;
            }
            catch (FormatException)
            {
                buttonStyle.buttonStyleWhite(buttonErrors, "Invalid data, please check it or contact with us.");
                return null;
            }
        }


    }
}