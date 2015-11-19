using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SYSPARK
{
    public partial class Enrollment : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //Setting values to the enrollment
            textboxName.Value = Session["User-Name"].ToString() + " " + Session["User-LastName"].ToString();
            textboxUnversityCard.Value = Session["User-UniversityCard"].ToString();
            selectCampus.Value = Session["User-CampusId"].ToString();

            //Select campus
            CampusData campusData = new CampusData();
            selectCampus.DataSource = campusData.DataTableCampus();
            selectCampus.DataValueField = "Id";
            selectCampus.DataTextField = "Description";
            selectCampus.DataBind();
        }
    }
}