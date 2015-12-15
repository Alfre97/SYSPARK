using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Script.Services;
using System.Web.Services;

namespace SiCAPV002
{
    public partial class googleChart : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }
         [WebMethod]
            [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
            public static object[] GetChartData()
            {
                List<DataBase> data = new List<DataBase>();
                //Here MyDatabaseEntities  is our dbContext
                using (MyDatabaseEntities dc = new MyDatabaseEntities())
                {
                    data = dc.GoogleChartDatas.ToList();
                }
 
                var chartData = new object[data.Count + 1];
                chartData[0] = new object[]{
                    "Product Category",
                    "Revenue Amount"
                };
                int j = 0;
                foreach (var i in data)
                {
                    j++;
                    chartData[j] = new object[] {i.ProductCategory, i.RevenueAmount };
                }
 
                return chartData;
            }
    }
}