using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Net.Mail;
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

        private void Send_Verification_Email(string recipientEmail)
        {
            var fromAddress = new MailAddress("kyle@houchin.us", "From Name");
            var toAddress = new MailAddress("kyle@houchin.us", "To Name");
            const string fromPassword = "jdrccotbsqzanxfy";
            const string subject = "Subject";
            const string body = "Body";

            var smtp = new SmtpClient
            {
                Host = "smtp.gmail.com",
                Port = 587,
                EnableSsl = true,
                DeliveryMethod = SmtpDeliveryMethod.Network,
                UseDefaultCredentials = false,
                Credentials = new NetworkCredential(fromAddress.Address, fromPassword)
            };
            using (var message = new MailMessage(fromAddress, toAddress)
            {
                Subject = subject,
                Body = body
            })
            {
                smtp.Send(message);
            }

            //try
            //{
            //    MailMessage newMail = new MailMessage();
            //    // use the Gmail SMTP Host
            //    SmtpClient client = new SmtpClient("smtp.gmail.com");

            //    // Follow the RFS 5321 Email Standard
            //    newMail.From = new MailAddress("BattlefieldCasiit@gmail.com", "Battlefield Casiit");

            //    newMail.To.Add(recipientEmail);// declare the email subject

            //    newMail.Subject = "Test Subject"; // use HTML for the email body

            //    newMail.Body = "This is a really cool body";
            //    //newMail.IsBodyHtml = true; newMail.Body = "<h1> This is my first Templated Email in C# </h1>";

            //    // enable SSL for encryption across channels
            //    client.EnableSsl = true;
            //    // Port 465 for SSL communication
            //    client.Port = 465;
            //    // Provide authentication information with Gmail SMTP server to authenticate your sender account
            //    client.Credentials = new System.Net.NetworkCredential("BattlefieldCasiit@gmail.com", "AIzaSyC2Ml1ovmI0DhBZMM");

            //    client.Send(newMail); // Send the constructed mail
            //    Console.WriteLine("Email Sent");
            //}
            //catch
            //{
            //    return false;
            //}

            //return true;
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
                if (tbCreateAccountPassword.Text != tbCreateAccountPassword.Text)
                {
                    // add indicator
                    Label label = Create_Label("Password Doesn't Match", Color.Red);

                    tableCreateAccount.Rows[7].Cells[0].Controls.Add(label);
                }

                // password matches
            }
            Send_Verification_Email("kyle@houchin.us");


        }

        private bool Indicate_Empty_TextBoxes()
        {
            bool emptyTextBoxPresent = false;

            // indicate to the user if a text box is not filled out
            if (fName == "")
            {
                // add indicator
                Label label = Create_Label("Missing Values", Color.Red);
                tableCreateAccount.Rows[0].Cells[0].Controls.AddAt(0, label);

                //Add_Indicator(lbCreateAccountFirstName);

                emptyTextBoxPresent = true;
            }
            if (lName == "")
            {
                // add indicator
                Label label = Create_Label("Missing Values", Color.Red);
                tableCreateAccount.Rows[1].Cells[0].Controls.Add(label);

                emptyTextBoxPresent = true;
            }
            if (email == "")
            {
                // add indicator
                Label label = Create_Label("Missing Values", Color.Red);
                tableCreateAccount.Rows[3].Cells[0].Controls.Add(label);

                emptyTextBoxPresent = true;
            }
            if (password == "")
            {
                // add indicator
                Label label = Create_Label("Missing Values", Color.Red);
                tableCreateAccount.Rows[5].Cells[0].Controls.Add(label);

                emptyTextBoxPresent = true;
            }
            if (confirmPassword == "")
            {
                // add indicator
                Label label = Create_Label("Missing Values", Color.Red);
                tableCreateAccount.Rows[7].Cells[0].Controls.Add(label);

                emptyTextBoxPresent = true;
            }

            return emptyTextBoxPresent;
        }

        private Label Create_Label(string text, Color color)
        {
            Label label = new Label();
            label.Text = text;
            label.ForeColor = color;
            label.Height = 10;

            return label;
        }

        // ADD INDICATOR WITHOUT HARD CODE
        //private void Add_Indicator(Control control)
        //{
        //    Label indicator = Create_Indicator();
        //    System.Drawing.Point point = Get_Coordinate(tableCreateAccount, control);
        //    if (!point.Equals(new System.Drawing.Point(-1, -1))) // as long as the point exists
        //    {
        //        // add indicator
        //        tableCreateAccount.Rows[point.X].Cells[0].Controls.Add(indicator);
        //    }
        //    else
        //    {
        //        // error
        //    }
        //}
        //private Label Create_Indicator()
        //{
        //    Label lbMissingInformation = new Label();
        //    lbMissingInformation.Text = "Missing Values";
        //    lbMissingInformation.ForeColor = Color.White;
        //    lbMissingInformation.Height = 10;

        //    return lbMissingInformation;
        //}
        //private System.Drawing.Point Get_Coordinate(Table table, Control control)
        //{
        //    // loop through table rows
        //    for (int r = 0; r < table.Rows.Count; r++)
        //    {
        //        // loop through table cells in row
        //        TableRow tr = table.Rows[r];
        //        for (int c = 0; c < tr.Cells.Count; c++)
        //        {
        //            // loop through controls in table cell
        //            TableCell tc = tr.Cells[c];
        //            foreach (Control currControl in tc.Controls)
        //            {
        //                if (currControl == control) // if the current control is the control queried upon, return the coordinate
        //                {
        //                    return new System.Drawing.Point(r, c);
        //                }
        //            }
        //        }
        //    }
        //    // the control queried upon is not in the table
        //    return new System.Drawing.Point(-1, -1);
        //}
    }
}