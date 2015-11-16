using SYSPARK.App_BussinessRules;
using SYSPARK.App_Utility;
using SYSPARK.Data;
using SYSPARK.Entities;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SYSPARK
{
    public partial class RolePage : System.Web.UI.Page
    {
        RoleBussinessRules roleRules = new RoleBussinessRules();
        ButtonStyle buttonStyle = new ButtonStyle();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["User-Id"] == null)
            Response.Redirect("Default.aspx");
            FillTable();
        }

        protected void AddRole_Click(object sender, EventArgs e)
        {
            InsertRole(CreateRole());
            FillTable();
        }

        protected void InsertRole(Role role)
        {
            if (role != null)
            {
                switch (roleRules.InsertRole(role))
                {
                    case 0:
                        textboxRole.Value = "";
                        buttonStyle.buttonStyleBlue(buttonErrors, "Role added successful.");
                        break;
                    case 1:
                        buttonStyle.buttonStyleWhite(buttonErrors, "Role name field is empty.");
                        break;
                    case 2:
                        buttonStyle.buttonStyleRed(buttonErrors, "An error ocurred creating the role, please check data or contact we us.");
                        break;

                }
            }
        }

        protected Role CreateRole()
        {
            Role role = new Role();
            try
            {
                role.Description = textboxRole.Value;
                return role;
            }
            catch (FormatException)
            {
                buttonStyle.buttonStyleWhite(buttonErrors, "Invalid data, please check it or contact with us.");
                return null;
            }
        }

        protected void FillTable()
        {
            RoleData roleData = new RoleData();
            //Populating a DataTable from database.
            DataTable dt = roleData.DataTableRole();

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
                html.Append("<button onclick='setValues(this.parentNode.parentNode)' type='button'>Editar</button>");
                html.Append("</td>");
                html.Append("<td>");
                html.Append("<button onclick='deleteRole()' type='button'>Eliminar</button>");
                html.Append("</td>");
                html.Append("</tr>");
            }
            html.Append("</tbody>");

            //Append the HTML string to Placeholder.
            placeHolderTableRole.Controls.Clear();
            placeHolderTableRole.Controls.Add(new Literal { Text = html.ToString() });
        }

        protected void Delete_Click(object sender, EventArgs e)
        {
            DeleteRole();
            FillTable();
        }

        protected void DeleteRole()
        {
            switch (roleRules.DeleteRole(Convert.ToInt32(hiddenRoleId.Value)))
            {
                case 0:
                    FillTable();
                    buttonStyle.buttonStyleBlue(buttonInfoRoleTable, "Role deleted successful.");
                    break;
                case 1:
                    FillTable();
                    buttonStyle.buttonStyleRed(buttonInfoRoleTable, "This role has linked data can not be deleted.");
                    break;
                case 2:
                    buttonStyle.buttonStyleRed(buttonInfoRoleTable, "Please, select a role to delete.");
                    break;
            }
        }

        protected void Update_Click(object sender, EventArgs e)
        {
            Role role = CreateRole();
            role.Id = Convert.ToInt32(hiddenRoleId.Value);
            UpdateRole(CreateRole());
            buttonClear.Style.Add("visibility", "visible");
            buttonAddRole.Style.Add("visibility", "visible");
            buttonUpdate.Style.Add("visibility", "hidden");
            FillTable();
        }
        
        protected void UpdateRole(Role role)
        {
            if (role != null)
            {
                switch (roleRules.UpdateRole(role))
                {
                    case 0:
                        textboxRole.Value = "";
                        buttonStyle.buttonStyleBlue(buttonErrors, "Role updated successful.");
                        break;
                    case 1:
                        buttonStyle.buttonStyleWhite(buttonErrors, "Role name field is empty.");
                        break;
                    case 2:
                        buttonStyle.buttonStyleRed(buttonErrors, "An error ocurred updating the role, please check data or contact we us.");
                        break;

                }
            }
        }
    }
}