using CASIITInformationWebsite.Pages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace CASIITInformationWebsite
{
    public partial class SiteMaster : MasterPage
    {
        public static bool loggedIn = false;
        public static int currentUserID = 1;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (loggedIn)
            {
                LoginButton.Text = "Sign Out";
            } else
            {
                LoginButton.Text = "Sign Up | Login";
            }
        }
    }
}