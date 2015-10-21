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
            //All controls are disable by default
            FillTableWithUserInfo();
            //Fill select condition
            User user = new User();
            ConditionData conditionData = new ConditionData();
            DataTable Condition = conditionData.DataTableCondition();
            selectCondition.DataSource = Condition;
            selectCondition.DataTextField = "Description";
            selectCondition.DataValueField = "Id";
            selectCondition.DataBind();

            //Fill select my cars
            VehicleData vehicleData = new VehicleData();
            DataTable dataSetVehicleOfUser = vehicleData.SelectByVehicleId(vehicleData.SelectVehicleByUser(user));
            myCars.DataSource = dataSetVehicleOfUser;
            myCars.DataTextField = "Lisence";
            myCars.DataValueField = "Id";
            myCars.DataBind();
        }

        public void FillTableWithUserInfo()
        {
            UserData userData = new UserData();
            string sessionUserName = (string)(Session["UserName"]);
            DataTable dataTableUserInfo = userData.DataTableUserInfo(sessionUserName);
            textboxName.Value = dataTableUserInfo.Rows[0]["Name"].ToString();
            textboxLastName.Value = dataTableUserInfo.Rows[0]["LastName"].ToString();
            textboxUsername.Value = dataTableUserInfo.Rows[0]["UserName"].ToString();
            textboxPassword.Value = dataTableUserInfo.Rows[0]["Password"].ToString();
            selectCondition.SelectedIndex = Convert.ToInt32(dataTableUserInfo.Rows[0]["Condition"]);
        }
    }
}