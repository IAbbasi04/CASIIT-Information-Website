using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace CASIITInformationWebsite.Pages
{
    public partial class CreateAccount : System.Web.UI.Page
    {
        string fName;
        string lName;
        string email;
        string password;
        string confirmPassword;
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void bCreateAccount_Click(object sender, EventArgs e)
        {
            // save data
            Save_Member_Data();
            
        }

        private void Save_Member_Data()
        {
            // convert info to lowercase so it is saved uniformly only if changing the case doesn't change its meaning
            fName = tbCreateAccountFirstName.Text.ToLower();
            lName = tbCreateAccountLastName.Text.ToLower();
            email = tbCreateAccountEmail.Text.ToLower();

            // case in a password does matter
            password = tbCreateAccountPassword.Text;
            confirmPassword = tbCreateAccountConfirmPassword.Text;

            if (!Indicate_Empty_TextBoxes()) // if there are no empty textboxes that need to be indicated continue
            {
                // idicate to the user if the password and confirm password do not match

            }



        }

        private bool Indicate_Empty_TextBoxes()
        {
            bool emptyTextBoxPresent = false;

            // indicate to the user if a text box is not filled out
            if (fName == "")
            {
                // add indicator

                emptyTextBoxPresent = true;
            }
            if (lName == "")
            {
                // add indicator

                emptyTextBoxPresent = true;
            }
            if (email == "")
            {
                // add indicator

                emptyTextBoxPresent = true;
            }
            if (password == "")
            {
                // add indicator

                emptyTextBoxPresent = true;
            }
            if (confirmPassword == "")
            {
                // add indicator

                emptyTextBoxPresent = true;
            }

            return emptyTextBoxPresent;
        }
    }
}