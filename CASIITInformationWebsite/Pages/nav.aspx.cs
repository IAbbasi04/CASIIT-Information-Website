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
        protected void Page_Load(object sender, EventArgs e)
        {
            List<Class> courses = SQLQuerier.AllClasses();



            foreach (Class course in courses)
            {
                courseList.Items.Add(course.course_name);
            }
        }
    }
}