using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace CASIITInformationWebsite.Pages
{
    public partial class AccountVerification : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            // On page load send the verification email
            Common_Elements.Csharp.EmailSender.Send_Verification_Email("kyle@houchin.us");
        }

        protected void bConfirmAccount_Click(object sender, EventArgs e)
        {

        }

        protected void bResendCode_Click(object sender, EventArgs e)
        {

        }
    }
}