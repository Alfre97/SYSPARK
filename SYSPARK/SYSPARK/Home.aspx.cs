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
            int userCondition = Convert.ToInt32(Session["User-ConditionId"]);
            if(userCondition == 1)
            {
                buttonCondition.Visible = false;
                buttonParking.Visible = false;
                buttonReports.Visible = false;
                buttonVehicleType.Visible = false;
                buttonCode.Visible = false;
            }
        }

        protected void buttonLogout_Click(object sender, EventArgs e)
        {
            Response.Redirect("Default.aspx");
        }
    }
}