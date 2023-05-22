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
        protected void bSendForgotPasswordEmailRequest_Click(object sender, EventArgs e)
        {

        }
        //public enum PersonType
        //{
        //    STUDENT,
        //    COUNSELOR,
        //    ADMIN,
        //    GUEST
        //}

        //public static PersonType personType = PersonType.GUEST;

        //public void Reset(object sender, EventArgs e)
        //{
        //    LoginEmailLabel.Visible = false;
        //    PasswordLabel.Visible = false;
        //    SignUpEmailLabel.Visible = false;
        //    SignUpPasswordLabel.Visible = false;
        //    SignUpFirstNameLabel.Visible = false;
        //    SignUpLastNameLabel.Visible = false;

        //    PersonTypeTable.Visible = true;
        //    SignUpTable.Visible = false;
        //    LoginTable.Visible = false;

        //    personType = PersonType.GUEST;
        //}

        //protected void Page_Load(object sender, EventArgs e)
        //{
        //    LinkButton lb = Master.FindControl("LoginButton") as LinkButton;
        //    lb.Click += Reset;
        //}

        //protected void Login_Click(object sender, EventArgs e)
        //{
        //    PasswordLabel.Visible = true;
        //    if (!IsValidEmail(LoginEmailBox.Text))
        //    {
        //        LoginEmailLabel.Visible = true;
        //    }
        //    else
        //    {
        //        LoginEmailLabel.Visible = false;
        //    }
        //}


        //private bool IsValidEmail(string email)
        //{
        //    string trimmedEmail = email.Trim();

        //    try
        //    {
        //        var addr = new System.Net.Mail.MailAddress(email);
        //        return addr.Address == trimmedEmail;
        //    }
        //    catch
        //    {
        //        return false;
        //    }
        //}

        //protected void PersonType_Click(object sender, EventArgs e)
        //{
        //    Button button = sender as Button;

        //    switch (button.Text)
        //    {
        //        case "Student":
        //            personType = PersonType.STUDENT;
        //            break;
        //        case "Admin":
        //            personType = PersonType.ADMIN;
        //            break;
        //        case "Counselor":
        //            personType = PersonType.COUNSELOR;
        //            break;
        //    }

        //    PersonTypeTable.Visible = false;
        //    SignUpTable.Visible = true;
        //}

        //protected void SignIn_Click(object sender, EventArgs e)
        //{
        //    PersonTypeTable.Visible = false;
        //    LoginTable.Visible = true;
        //}

        //protected void Guest_Click(object sender, EventArgs e)
        //{
        //    Server.TransferRequest("~/Pages/Home");
        //}

        //protected void SignUp_Click(object sender, EventArgs e)
        //{
        //    bool canMoveOn = true;

        //    LoginTable.Visible = false;
        //    if (!IsValidEmail(SignUpEmailTextBox.Text))
        //    {
        //        SignUpEmailLabel.Visible = true;
        //        canMoveOn = false;
        //    }
        //    else
        //    {
        //        SignUpEmailLabel.Visible = false;
        //    }

        //    if (SignUpPasswordTextBox.Text == "")
        //    {
        //        SignUpPasswordLabel.Visible = true;
        //        canMoveOn = false;
        //    }
        //    else
        //    {
        //        SignUpPasswordLabel.Visible = false;
        //    }

        //    if (FirstNameTextBox.Text == "")
        //    {
        //        SignUpFirstNameLabel.Visible = true;
        //        canMoveOn = false;
        //    }
        //    else
        //    {
        //        SignUpFirstNameLabel.Visible = false;
        //    }

        //    if (LastNameTextBox.Text == "")
        //    {
        //        SignUpLastNameLabel.Visible = true;
        //        canMoveOn = false;
        //    }
        //    else
        //    {
        //        SignUpLastNameLabel.Visible = false;
        //    }

        //    if (canMoveOn)
        //    {
        //        SignUpTable.Visible = false;
        //        LinkButton lb = Master.FindControl("LoginButton") as LinkButton;
        //        lb.Text = "Sign Out";
        //        Server.TransferRequest("~/Pages/Home");
        //    }

        //}
    }
}