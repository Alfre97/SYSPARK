using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SYSPARK
{
    public partial class ParkingReservation : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["User-UserName"] == null)
                Response.Redirect("Default.aspx");

            FillSelectParking();
        }

        protected void FillSelectParking()
        {
            //Select parking
            ParkingData parkingData = new ParkingData();
            DataTable dataTableParking = parkingData.DataTableParking(Convert.ToInt32(Session["User-CampusId"]));
            selectParking.DataSource = dataTableParking;
            selectParking.DataValueField = "Name";
            selectParking.DataTextField = "Name";
            selectParking.DataBind();
        }
    }
}