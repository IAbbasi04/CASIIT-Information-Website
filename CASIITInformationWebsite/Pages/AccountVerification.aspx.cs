using System;
using System.Collections.Generic;
using System.Drawing;
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
            if (!this.IsPostBack) // only automatically send on the first load
            {
                Send_Verification_Code("kyle@houchin.us");
            }
        }

        protected void bConfirmAccount_Click(object sender, EventArgs e)
        {
            if (tbAccountVerificationInputCode.Text == "" + ViewState["VerificationCode"]) // if the input code matches the sent code
            {
                // make the user verified

                // indicate that the user is verified
                lbAccountVerificationHeader.Text = "Account Verification Complete!";
                lbAccountVerificationDescription.Text = "You can now explore the website and save your work for later";

                tbAccountVerificationInputCode.Visible = false;
                bConfirmAccount.Visible = false;
                bResendCode.Visible = false;
            }
            else
            {
                // indicate that the user put in the wrong code
                Label label = new Label
                {
                    Text = "Incorrect Code",
                    ForeColor = Color.Red,
                    Height = 10,
                };
                tableVerifyAccount.Rows[2].Cells[0].Controls.Add(label);


            }
        }

        protected void bResendCode_Click(object sender, EventArgs e)
        {
            Send_Verification_Code("kyle@houchin.us");
        }

        private void Send_Verification_Code(string recipientEmail)
        {
            // Generate random code 
            Random rand = new Random();
            int verificationCode = rand.Next(100000, 1000000); // six digit code
            // save code
            ViewState["VerificationCode"] = verificationCode;

            // On page load send the verification email
            Common_Elements.Csharp.EmailSender.Send_Verification_Email(recipientEmail, verificationCode);
        }
    }
}