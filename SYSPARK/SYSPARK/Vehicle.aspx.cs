using SYSPARK.Data;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SYSPARK
{
    public partial class AddNewCar : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            VehicleTypeData vehicleTypedata = new VehicleTypeData();
            DataTable Condition = vehicleTypedata.DataTableVehicleType();
            selectType.DataSource = Condition;
            selectType.DataTextField = "Description";
            selectType.DataValueField = "Id";
            selectType.DataBind();
        }
    }
}