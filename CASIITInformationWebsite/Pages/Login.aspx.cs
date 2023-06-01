using CASIITInformationWebsite.Common_Elements.Csharp;
using Microsoft.SqlServer.Server;
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
        public static Student currentStudent;

        public enum PersonType
        {
            STUDENT,
            COUNSELOR,
            ADMIN,
            GUEST
        }

        public static PersonType personType = PersonType.GUEST;

        public void Reset(object sender, EventArgs e)
        {
            LinkButton lb = sender as LinkButton;

            LoginEmailLabel.Visible = false;
            PasswordLabel.Visible = false;
            SignUpEmailLabel.Visible = false;
            SignUpPasswordLabel.Visible = false;
            SignUpFirstNameLabel.Visible = false;
            SignUpLastNameLabel.Visible = false;

            PersonTypeTable.Visible = true;
            SignUpTable.Visible = false;
            LoginTable.Visible = false;
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            LinkButton lb = Master.FindControl("LoginButton") as LinkButton;
            lb.Click += Reset;

            if (SiteMaster.loggedIn)
            {
                Server.TransferRequest("~/Pages/Home");
                SiteMaster.loggedIn = false;
            }
        }

        protected void Login_Click(object sender, EventArgs e)
        {
            bool canMoveOn = true;
            string desiredPassword = "";

            if (!IsValidEmail(LoginEmailBox.Text))
            {
                canMoveOn = false;
                LoginEmailLabel.Text = "Enter a valid email";
                LoginEmailLabel.Visible = true;
            } else if (!SQLQuerier.EmailAlreadyRegistered(LoginEmailBox.Text))
            {
                canMoveOn = false;
                LoginEmailLabel.Text = "Email doesn't exists";
                LoginEmailLabel.Visible = true;
            }
            else
            {
                LoginEmailLabel.Visible = false;
                desiredPassword = SQLQuerier.GetPasswordFromEmail(LoginEmailBox.Text);
            }



            if (LoginPasswordBox.Text == desiredPassword && desiredPassword != "")
            {
                PasswordLabel.Visible = false;
            } else
            {
                canMoveOn = false;
                PasswordLabel.Visible = true;
            }

            if (canMoveOn)
            {
                LinkButton lb = Master.FindControl("LoginButton") as LinkButton;
                lb.Text = "Sign Out";
                SiteMaster.loggedIn = true;
                SiteMaster.CURRENT_STUDENT = SQLQuerier.SelectStudent(SQLQuerier.GetUID(LoginEmailBox.Text, LoginPasswordBox.Text));
                Server.TransferRequest("~/Pages/Home");
            }
        }


        protected void PersonType_Click(object sender, EventArgs e)
        {
            Button button = sender as Button;

            switch (button.ID) {
                case "Student":
                    personType = PersonType.STUDENT;
                    break;
                case "Admin":
                    personType = PersonType.ADMIN;
                    break;
                case "Counselor":
                    personType = PersonType.COUNSELOR;
                    break;
            }

            PersonTypeTable.Visible = false;
            SignUpTable.Visible = true;
        }

        protected void SignIn_Click(object sender, EventArgs e)
        {
            PersonTypeTable.Visible = false;
            LoginTable.Visible = true;
        }

        protected void Guest_Click(object sender, EventArgs e)
        {
            Student student = new Student(
                "first",
                "last",
                0,
                1,
                4.0,
                0,
                "email@gmail.com",
                "password"
            );

            SQLQuerier.InsertStudent(student);

            Server.TransferRequest("~/Pages/Home");
            SiteMaster.loggedIn = false;
        }

        protected void SignUp_Click(object sender, EventArgs e)
        {
            bool canMoveOn = true;

            LoginTable.Visible = false;
            if (!IsValidEmail(SignUpEmailTextBox.Text))
            {
                SignUpEmailLabel.Visible = true;
                canMoveOn = false;
            } else
            {
                SignUpEmailLabel.Visible = false;
            }

            if (SignUpPasswordTextBox.Text == "")
            {
                SignUpPasswordLabel.Visible = true;
                canMoveOn = false;
            }
            else
            {
                SignUpPasswordLabel.Visible = false;
            }

            if (FirstNameTextBox.Text == "")
            {
                SignUpFirstNameLabel.Visible = true;
                canMoveOn = false;
            } else
            {
                SignUpFirstNameLabel.Visible = false;
            }

            if (LastNameTextBox.Text == "")
            {
                SignUpLastNameLabel.Visible = true;
                canMoveOn = false;
            }
            else
            {
                SignUpLastNameLabel.Visible = false;
            }

            if (canMoveOn)
            {
                SQLQuerier.InsertStudent(new Student(FirstNameTextBox.Text, LastNameTextBox.Text, 0, 4.0, SignUpEmailTextBox.Text, SignUpPasswordTextBox.Text));
                SignUpTable.Visible = false;
                LinkButton lb = Master.FindControl("LoginButton") as LinkButton;
                lb.Text = "Sign Out";
                SiteMaster.loggedIn = true;
                Server.TransferRequest("~/Pages/Home");
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

        private int GetUIDFromEmail(string email)
        {
            int uid = 0;



            return uid;
        }
    }
}