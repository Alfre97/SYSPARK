﻿using SYSPARK.App_BussinessRules;
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
    public partial class LapsePage : System.Web.UI.Page
    {
        LapseBussinessRules lapseRules = new LapseBussinessRules();
        ButtonStyle buttonStyle = new ButtonStyle();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["User-UserName"] == null)
               Response.Redirect("Default.aspx");
        }

        protected void Delete_Click(object sender, EventArgs e)
        {

        }

        protected void FillTable()
        {
            LapseData lapseData = new LapseData();
            //Populating a DataTable from database.
            DataTable dt = lapseData.DataTableLapse();

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
            placeHolderTableLapse.Controls.Clear();
            placeHolderTableLapse.Controls.Add(new Literal { Text = html.ToString() });
        }

        protected Lapse CreateLapse()
        {
            try
            {
                Lapse lapse = new Lapse();
                lapse.Name = textboxLapse.Value;
                lapse.InitialDate = Convert.ToDateTime(dateInitialDate.Value);
                lapse.FinalDate = Convert.ToDateTime(dateFinalDate.Value);

                if (radioStatusOn.Checked)
                    lapse.Status = true;
                else if
                    (radioStatusOff.Checked)
                       lapse.Status = false;
                return lapse;
            }
            catch (FormatException)
            {
                buttonStyle.buttonStyleWhite(buttonErrors, "Invalid data, please check it or contact with us.");
                return null;
            }
        }

        protected void AddLapse_Click(object sender, EventArgs e)
        {
            InsertLapse(CreateLapse());
            FillTable();
        }

        protected void InsertLapse(Lapse lapse)
        {
            if (lapse != null)
            {
                switch (lapseRules.InsertLapse(lapse))
                {
                    case 0:
                        textboxLapse.Value = "";
                        buttonStyle.buttonStyleBlue(buttonErrors, "Lapse added successful.");
                        break;
                    case 1:
                        buttonStyle.buttonStyleWhite(buttonErrors, "Lapse name field is empty.");
                        break;
                    case 2:
                        buttonStyle.buttonStyleRed(buttonErrors, "An error ocurred creating the lapse, please check data or contact we us.");
                        break;
                    case 3:
                        buttonStyle.buttonStyleRed(buttonErrors, "Lapse initial date is not set.");
                        break;
                    case 4:
                        buttonStyle.buttonStyleRed(buttonErrors, "Lapse final date is not set.");
                        break;
                    case 5:
                        buttonStyle.buttonStyleRed(buttonErrors, "Lapse status is not set.");
                        break;
                    case 6:
                        buttonStyle.buttonStyleRed(buttonErrors, "Lapse initial date can't be highter than final date.");
                        break;
                    case 7:
                        buttonStyle.buttonStyleRed(buttonErrors, "Lapse initial date can't be less than today.");
                        break;
                }
            }
        }
    }
}