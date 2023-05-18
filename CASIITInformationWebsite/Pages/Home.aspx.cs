using CASIITInformationWebsite.Common_Elements.Csharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using static CASIITInformationWebsite.Common_Elements.Csharp.SQLQuerier;

namespace CASIITInformationWebsite
{
    public partial class Home : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //string[] test = SQLQuerier.Parse("*", "users");
            //string test = SQLQuerier.ParseAll();
            //string[] cols = SQLQuerier.GetColNames("users");
            SQLQuerier.InsertStudent("Ibrahim Abbasi", "Ibrahimzabbasi04@gmail.com", "Password");
            string[] names = SQLQuerier.GetAllStudents();
        }
    }
}