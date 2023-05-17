using CASIITInformationWebsite.Common_Elements.Csharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace CASIITInformationWebsite.Pages
{
    public partial class Login : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            PasswordLabel.Visible = false;
        }

        protected void Login_Click(object sender, EventArgs e)
        {
            PasswordLabel.Visible = true;
        }

        protected void SignUp_Click(object sender, EventArgs e)
        {
            if (!IsValidEmail(EmailTextBox.Text))
            {
                EmailLabel.Visible = true;
            } else
            {
                EmailLabel.Visible = false;
            }
        }

        private bool IsValidEmail(string email)
        {
            string trimmedEmail = email.Trim();

            try
            {
                var addr = new System.Net.Mail.MailAddress(email);
                return addr.Address == trimmedEmail;
            }
            catch
            {
                return false;
            }
        }
    }
}