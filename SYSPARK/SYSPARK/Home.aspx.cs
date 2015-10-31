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
            if (Session["User-Id"] == null)
                Response.Redirect("Default.aspx");

            int userCondition = Convert.ToInt32(Session["User-ConditionId"]);
            if(userCondition < 2)
            {
                trReports.Visible = false;
                trParking.Visible = false;
                trRole.Visible = false;
                trVehicleType.Visible = false;
                trCode.Visible = false;
            }
        }

        protected void buttonLogout_Click(object sender, EventArgs e)
        {
            Session.Clear();
            Response.Redirect("Default.aspx");
        }
    }
}