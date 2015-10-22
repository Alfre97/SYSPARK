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
            User user = userData.sendUser(userData.getUser(Session["UserName"].ToString()));
            textboxName.Value = user.Name;
            textboxLastName.Value = user.LastName;
            textboxUsername.Value = user.Username;
            textboxPasswordShowed.Value = user.Password;
            selectCondition.SelectedIndex = user.Condition.Id;
        }
    }
}