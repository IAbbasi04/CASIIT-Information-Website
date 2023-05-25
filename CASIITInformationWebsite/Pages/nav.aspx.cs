using CASIITInformationWebsite.Common_Elements;
using CASIITInformationWebsite.Common_Elements.Csharp;
using EllipticCurve;
using MySqlX.XDevAPI.Relational;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace CASIITInformationWebsite.Pages
{
    public partial class nav : System.Web.UI.Page
    {
        private string GPA;
        private List<string> classes = new List<string>();
        private string grade;
        private List<string> mathClasses = new List<string>();
        protected void Page_Load(object sender, EventArgs e)
        {
            List<Class> courses = SQLQuerier.AllClasses();



            foreach (Class course in courses)
            {
                courseList.Items.Add(course.course_name);
            }
        }

        protected void SubmitInputs(object sender, EventArgs e)
        {
            setGPA();
            setGrade();
            setMath();
            setClasses();

            // when all the database stuff is correct, make sure to add the recordInDatabase method here.
            // I'm not adding it for now because I don't want to cause a database error if there is a primary key violation with 
            // repeated userIds, etc.
        }

        protected void recordInDatabase()
        {
            SQLQuerier.CreateTrack(SiteMaster.currentUserID, "" + SiteMaster.currentUserID);

            foreach (string course in classes)
            {
                SQLQuerier.InsertClassIntoTrack(SiteMaster.currentUserID, generateTrackID(), getClassID(course));
            }
        }

        protected void setGPA()
        {
            GPA = gpaInput.Text;
        }

        protected void setGrade()
        {
            grade = gradeInput.SelectedValue;
        }

        protected void setMath()
        {
            for (int i = 0; i < mathInput.Items.Count; i++)
            {
                mathClasses.Add(mathInput.Items[i].Text);
            }
        }

        protected void setClasses()
        {
            for (int i = 0; i < courseList.Items.Count; i++)
            {
                classes.Add(courseList.Items[i].Text);
            }
        }

        protected int generateTrackID()
        {
            // find a way to make a new trackID for each user using SQL
            return SiteMaster.currentUserID+10;
        }

        protected int getClassID(string className)
        {
            // use sql to get the classID for a class based on its name
            return 238778;
        }
    }
}