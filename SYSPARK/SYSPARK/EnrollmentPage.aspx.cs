using SYSPARK.App_Entities;
using SYSPARK.App_Utility;
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
    public partial class EnrollmentPage : System.Web.UI.Page
    {
        EnrollmentData enrollmentData = new EnrollmentData();
        LapseData lapseData = new LapseData();
        CampusData campusData = new CampusData();
        UserData userData = new UserData();
        ButtonStyle buttonStyle = new ButtonStyle();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["User-UserName"] == null)
                Response.Redirect("Default.aspx");

            FillSelectCampus();
            SetEnrollmentValues();
        }

        protected void SetEnrollmentValues()
        {
            DataTable dataTableUserEnrollement = enrollmentData.DataTableUserEnrollment(Session["User-UserName"].ToString());
            DataTable dataTableEnrollmentLapse = lapseData.DataTableEnrollmentLapse(Convert.ToInt32(dataTableUserEnrollement.Rows[0]["LapseId"]));

            if (dataTableUserEnrollement.Rows.Count > 0)
            {
                buttonActivateEnrollment.Disabled = true;
                //Setting values to the enrollment
                textboxName.Value = Session["User-Name"].ToString() + " " + Session["User-LastName"].ToString();
                textboxUnversityCard.Value = Session["User-UniversityCard"].ToString();
                textboxLapseName.Value = dataTableUserEnrollement.Rows[0]["LapseName"].ToString();
                dateInitialDate.Value = dataTableEnrollmentLapse.Rows[0]["InitialDate"].ToString();
                dateFinalDate.Value = dataTableEnrollmentLapse.Rows[0]["FinalDate"].ToString();

                if (Convert.ToInt32(dataTableEnrollmentLapse.Rows[0]["Status"]) == 0)
                {
                    buttonActivateEnrollment.Disabled = false;
                    textboxStatus.Value = "Off";
                    textboxStatus.Style.Add("color", "white");
                    textboxStatus.Style.Add("background-color", "red");
                    buttonStyle.buttonStyleWhite(buttonErrors, "Your enrollment is inactive." + "\n" + "Please click 'Activate enrollment'.");
                }
                else
                {
                    textboxStatus.Value = "On";
                    textboxStatus.Style.Add("color", "white");
                    textboxStatus.Style.Add("background-color", "green");
                }
            }
            else
            {
                buttonStyle.buttonStyleRed(buttonErrors, "You don't have an enrollment." + "\n" + "Please click 'Create enrollment'.");
                buttonCreateEnrollment.Visible = true;
                buttonActivateEnrollment.Visible = false;
            }
        }

        protected void FillSelectCampus()
        {
            //Select campus
            selectCampus.DataSource = campusData.DataTableUserCampus(campusData.SendCampusList(campusData.GetUserCampus(Session["User-UserName"].ToString())));
            selectCampus.DataValueField = "Id";
            selectCampus.DataTextField = "Name";
            selectCampus.DataBind();
        }

        protected void ButtonCreateEnrollment_Click(object sender, EventArgs e)
        {
            InsertEnrollment(CreateEnrollment());
            SetEnrollmentValues();
        }

        protected void ButtonActivateEnrollment_Click(object sender, EventArgs e)
        {
            ActivateEnrollment(CreateEnrollment());
            SetEnrollmentValues();
        }

        protected void ActivateEnrollment(Enrollment enrollment)
        {
            try
            {
                enrollmentData.UpdateEnrollment(enrollment);
                buttonActivateEnrollment.Disabled = true;
            }
            catch (Exception)
            {
                buttonStyle.buttonStyleRed(buttonErrors, "Ops an error ocurred activating your enrollment." + "\n" + "Please, try again.");
            }

        }

        protected Enrollment CreateEnrollment()
        {
            Lapse lapse = new Lapse();
            Enrollment enrollment = new Enrollment();
            lapse = lapseData.SendLapse(lapseData.DataTableLapseOn());
            enrollment.UserName = Session["User-UserName"].ToString();
            enrollment.Lapse = lapse;
            return enrollment;
        }

        protected void InsertEnrollment(Enrollment enrollment)
        {
            try
            {
                enrollmentData.InsertEnrollment(enrollment);
                buttonActivateEnrollment.Visible = true;
                buttonCreateEnrollment.Visible = false;
            }
            catch(Exception)
            {
                buttonStyle.buttonStyleRed(buttonErrors, "Ops an error ocurred creating your enrollment." + "\n" + "Please, try again.");
            }
        }
    }
}