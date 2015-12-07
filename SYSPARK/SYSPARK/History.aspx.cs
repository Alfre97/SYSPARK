using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SYSPARK
{
    public partial class History : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["User-UserName"] == null)
                Response.Redirect("Default.aspx");

            FillSelectCampusToView();
            FillTable();
        }

        protected void FillSelectCampusToView()
        {
            //Select campus ti view
            CampusData campusData = new CampusData();
            DataTable dataTableCampus = campusData.DataTableCampus();
            hiddenCampusToViewValue.Value = dataTableCampus.Rows[0]["Id"].ToString();
            selectCampusToView.DataSource = dataTableCampus;
            selectCampusToView.DataValueField = "Id";
            selectCampusToView.DataTextField = "Name";
            selectCampusToView.DataBind();
        }

        protected void FillTable()
        {
            HistoryData historyData = new HistoryData();
            //Populating a DataTable from database.

            DataTable dt = new DataTable();

            if (Convert.ToInt32(Session["User-RoleId"]) == 3)
            {
                dt = historyData.DataTableHistory(Convert.ToInt32(hiddenCampusToViewValue.Value));
            }
            else
            {
                dt = historyData.DataTableUserHistory(Session["User-UserName"].ToString());
            }

            //Building an HTML string.
            StringBuilder html = new StringBuilder();

            //Building the Header row.
            html.Append("<tbody>");
            html.Append("<tr>");
            foreach (DataColumn column in dt.Columns)
            {
                html.Append("<th>");
                html.Append(column.ColumnName);
                html.Append("</th>");
            }
            html.Append("</tr>");

            //Building the Data rows.
            foreach (DataRow row in dt.Rows)
            {
                html.Append("<tr class='desmarcado'>");
                foreach (DataColumn column in dt.Columns)
                {
                    html.Append("<td onclick='getValue(this.parentNode)' style='cursor:pointer'>");
                    html.Append(row[column.ColumnName]);
                    html.Append("</td>");
                }
                html.Append("<td>");
                html.Append("<button onclick='setValues(this.parentNode.parentNode)' type='button'>Edit</button>");
                html.Append("</td>");
                html.Append("<td>");
                html.Append("<button onclick='deleteRole()' type='button'>Delete</button>");
                html.Append("</td>");
                html.Append("</tr>");
            }
            html.Append("</tbody>");

            //Append the HTML string to Placeholder.
            placeHolderTableHistory.Controls.Clear();
            placeHolderTableHistory.Controls.Add(new Literal { Text = html.ToString() });
        }
    }
}