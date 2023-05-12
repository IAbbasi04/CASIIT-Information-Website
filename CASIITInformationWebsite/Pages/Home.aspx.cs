using CASIITInformationWebsite.Common_Elements.Csharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace CASIITInformationWebsite
{
    public partial class _Default : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string test = SQLQuerier.Select("id, name");
            string[] test2 = SQLQuerier.Parse("id, name");
        }
    }
}