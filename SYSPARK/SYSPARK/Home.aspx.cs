using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SYSPARK
{
    public partial class Home : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["User-UserName"] == null)
                Response.Redirect("Default.aspx");

            int userCondition = Convert.ToInt32(Session["User-RoleId"]);
            if(userCondition != 3)
            {
                Reports.Visible = false;
                User.Visible = false;
                Parking.Visible = false;
                Campus.Visible = false;
                Role.Visible = false;
                Vehicle.Visible = false;
                VehicleType.Visible = false;
                Lapse.Visible = false;
                Space.Visible = false;
            }
        }

        protected void buttonLogout_Click(object sender, EventArgs e)
        {
            Session.Clear();
            Response.Redirect("Default.aspx");
        }
    }
}