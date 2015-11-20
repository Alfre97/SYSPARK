using SYSPARK.App_Entities;
using SYSPARK.Data;
using System;
using System.Collections.Generic;
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
        Enrollment enrollment = new Enrollment();
        Lapse lapse = new Lapse();
        UserData userData = new UserData();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["User-UserName"] == null)
                Response.Redirect("Default.aspx");

            FillSelectCampus();
            SetEnrollmentValues();
        }

        protected void SetEnrollmentValues()
        {
            if (Session["User-EnrollmentUniqueIdentifier"].Equals(string.Empty))
            {
                ButtonActivateEnrollment.Disabled = true;
                //Setting values to the enrollment
                textboxName.Value = Session["User-Name"].ToString() + " " + Session["User-LastName"].ToString();
                textboxUnversityCard.Value = Session["User-UniversityCard"].ToString();
                selectCampus.Value = Session["User-CampusId"].ToString();
            }
            else
            {
                //Getting UserEnrollment and EnrollmentLapse 
                enrollment = enrollmentData.SendUserEnrollment(enrollmentData.DataTableUserEnrollment(Session["User-EnrollmentUniqueIdentifier"].ToString()));
                lapse = lapseData.SendLapse(lapseData.DataTableEnrollmentLapse(enrollment.Lapse.Id));
                hiddenEnrollmentId.Value = enrollment.UniqueIdentifier;
                dateInitialDate.Value = lapse.InitialDate.ToString();
                dateFinalDate.Value = lapse.FinalDate.ToString();

                if (lapse.Status == true)
                {
                    textboxStatus.Value = "On";
                    textboxStatus.Style.Add("color", "white");
                    textboxStatus.Style.Add("background-color", "green");
                }
                else
                {
                    ButtonActivateEnrollment.Disabled = false;
                    textboxStatus.Value = "Off";
                    textboxStatus.Style.Add("color", "white");
                    textboxStatus.Style.Add("background-color", "red");
                }

                //Setting values to the enrollment
                textboxName.Value = Session["User-Name"].ToString() + " " + Session["User-LastName"].ToString();
                textboxUnversityCard.Value = Session["User-UniversityCard"].ToString();
                selectCampus.Value = Session["User-CampusId"].ToString();
            }

        }

        protected void FillSelectCampus()
        {
            //Select campus
            selectCampus.DataSource = campusData.DataTableCampus();
            selectCampus.DataValueField = "Id";
            selectCampus.DataTextField = "Description";
            selectCampus.DataBind();
        }

        protected void ButtonActivateEnrollment_Click(object sender, EventArgs e)
        {
            ActivateEnrollment();
        }

        protected void ActivateEnrollment()
        {
            //Getting tha lapse who is on.
            lapse = lapseData.SendLapse(lapseData.DataTableLapseOn());
            enrollment.UniqueIdentifier = "Enrollment of: " + Session["User-UserName"].ToString();
            enrollment.Lapse.Id = lapse.Id;

            if (Session["User-EnrollmentUniqueIdentifier"].Equals(string.Empty))
            {
                enrollmentData.InsertEnrollment(enrollment);
                userData.UpdateUserEnrollment(Session["User-UserName"].ToString(), enrollment.UniqueIdentifier);
            }
            else
            {
                enrollmentData.UpdateEnrollment(enrollment);
            }
        }
    }
}