using SYSPARK.Data;
using SYSPARK.Entities;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SYSPARK
{
    public partial class Profile : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            User user = new User();
            ConditionData conditionData = new ConditionData();
            DataSet dsSelectCondition = conditionData.DataSetCondition();
            selectCondition.DataSource = dsSelectCondition.Tables["Condition"].DefaultView;
            selectCondition.DataTextField = "Description";
            selectCondition.DataValueField = "Id";
            selectCondition.DataBind();

            VehicleData vehicleData = new VehicleData();
            DataSet dataSetVehicleOfUser= vehicleData.SelectByVehicleId(vehicleData.SelectVehicleByUser(user));
            selectVehicle.DataSource = dataSetVehicleOfUser.Tables["dataTableVehicle"].DefaultView;
            selectVehicle.DataTextField = "Lisence";
            selectVehicle.DataValueField = "Id";
            selectVehicle.DataBind();
        }

        public void FillTableWithUserInfo()
        {
            
        }
    }
}