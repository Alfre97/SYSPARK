using SYSPARK.App_BussinessRules;
using SYSPARK.App_Entities;
using SYSPARK.App_Utility;
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
    public partial class CampusPage : System.Web.UI.Page
    {
        CampusBussinessRules campusRules = new CampusBussinessRules();
        ButtonStyle buttonStyle = new ButtonStyle();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["User-UserName"] == null)
                Response.Redirect("http://syspark.azurewebsites.net/Default.aspx");

            FillTable();
        }

        protected Campus CreateCampus()
        {
            try
            {
                Campus campus = new Campus();
                campus.Name = textboxCampus.Value;
                return campus;
            }
            catch (FormatException)
            {
                buttonStyle.buttonStyleWhite(buttonErrors, "Invalid data, please check it or contact with us.");
                return null;
            }
        }

        protected void AddCampus_Click(object sender, EventArgs e)
        {
            InsertCampus(CreateCampus());
            FillTable();
        }

        protected void InsertCampus(Campus campus)
        {
            if (campus != null)
            {
                switch (campusRules.InsertCampus(campus))
                {
                    case 0:
                        textboxCampus.Value = "";
                        buttonStyle.buttonStyleBlue(buttonErrors, "Campus added successful.");
                        break;
                    case 1:
                        buttonStyle.buttonStyleWhite(buttonErrors, "Campus name field is empty.");
                        break;
                    case 2:
                        buttonStyle.buttonStyleRed(buttonErrors, "An error ocurred creating the campus, please check data or contact we us.");
                        break;

                }
            }
        }

        protected void FillTable()
        {
            CampusData campusData = new CampusData();
            //Populating a DataTable from database.
            DataTable dt = campusData.DataTableCampus();

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
            placeHolderTableCampus.Controls.Clear();
            placeHolderTableCampus.Controls.Add(new Literal { Text = html.ToString() });
        }

        protected void Delete_Click(object sender, EventArgs e)
        {
            DeleteCampus();
            FillTable();
        }

        protected void DeleteCampus()
        {
            try
            {
                switch (campusRules.DeleteCampus(Convert.ToInt32(hiddenCampusId.Value)))
                {
                    case 0:
                        FillTable();
                        buttonStyle.buttonStyleBlue(buttonInfoCampusTable, "Campus deleted successful.");
                        break;
                    case 1:
                        FillTable();
                        buttonStyle.buttonStyleRed(buttonInfoCampusTable, "This campus has linked data can not be deleted.");
                        break;
                    case 2:
                        buttonStyle.buttonStyleWhite(buttonInfoCampusTable, "Please, select a campus to delete.");
                        break;
                }
            }
            catch
            {
                buttonStyle.buttonStyleRed(buttonInfoCampusTable, "Please, select a campus to delete.");
            }
        }

        protected void Update_Click(object sender, EventArgs e)
        {
            try
            {
                Campus campus = CreateCampus();
                campus.Id = Convert.ToInt32(hiddenCampusId.Value);
                UpdateCampus(campus);
                buttonClear.Style.Add("visibility", "visible");
                buttonAddCampus.Style.Add("visibility", "visible");
                buttonUpdate.Style.Add("visibility", "hidden");
                FillTable();
            }
            catch (FormatException)
            {
                buttonStyle.buttonStyleRed(buttonInfoCampusTable, "Please, after any operation select one campus!");
            }
        }

        protected void UpdateCampus(Campus campus)
        {
            if (campus != null)
            {
                switch (campusRules.UpdateCampus(campus))
                {
                    case 0:
                        textboxCampus.Value = "";
                        buttonStyle.buttonStyleBlue(buttonErrors, "Campus updated successful.");
                        break;
                    case 1:
                        buttonStyle.buttonStyleWhite(buttonErrors, "Campus name field is empty.");
                        break;
                    case 2:
                        buttonStyle.buttonStyleRed(buttonErrors, "An error ocurred updating the campus, please check data or contact we us.");
                        break;

                }
            }
        }
    }
}