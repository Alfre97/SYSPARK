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
            ConditionData conditionData = new ConditionData();
            DataTable Condition = conditionData.DataTableCondition();
            selectCondition.DataSource = Condition;
            selectCondition.DataTextField = "Description";
            selectCondition.DataValueField = "Id";
            selectCondition.DataBind();

            //Fill select my cars
            VehicleData vehicleData = new VehicleData();
            DataTable dataTableVehicleOfUser = vehicleData.GetUserVehicle(Convert.ToInt32(Session["User-Id"]));
            myCars.DataSource = dataTableVehicleOfUser;
            myCars.DataTextField = "License";
            myCars.DataValueField = "Id";
            myCars.DataBind();
        }

        public void FillTableWithUserInfo()
        {
            textboxName.Value = Session["User-Name"].ToString();
            textboxLastName.Value = Session["User-LastName"].ToString();
            textboxUsername.Value = Session["User-UserName"].ToString();
            textboxPasswordShowed.Value = Session["User-Password"].ToString();
            selectCondition.SelectedIndex = Convert.ToInt32(Session["User-ConditionId"]);
        }
    }
}