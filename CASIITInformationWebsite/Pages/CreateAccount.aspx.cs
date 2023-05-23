using Microsoft.CodeDom.Providers.DotNetCompilerPlatform;
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

        //private bool Send_Verification_Email(string recipientEmail)
        //{
        //    Random rand = new Random();
        //    int verificationCode = rand.Next(100000, 1000000); // six digit code

        //    // try to send email
        //    try
        //    {
        //        var fromAddress = new MailAddress("BattlefieldCasiit@gmail.com", "Battlefield Casiit");
        //        var toAddress = new MailAddress(recipientEmail, "To Name");
        //        const string fromPassword = "vkjjgqhbdocmhatn"; // Account Settings > Security > 2Factor Authentication > API KEys
        //        const string subject = "Casiit Account Verification";
        //        string body = "<!DOCTYPE html>\r\n<html>\r\n<head>\r\n\r\n  <meta charset=\"utf-8\">\r\n  <meta http-equiv=\"x-ua-compatible\" content=\"ie=edge\">\r\n  <title>Email Confirmation</title>\r\n  <meta name=\"viewport\" content=\"width=device-width, initial-scale=1\">\r\n  <style type=\"text/css\">\r\n  /**\r\n   * Google webfonts. Recommended to include the .woff version for cross-client compatibility.\r\n   */\r\n  @media screen {\r\n    @font-face {\r\n      font-family: 'Source Sans Pro';\r\n      font-style: normal;\r\n      font-weight: 400;\r\n      src: local('Source Sans Pro Regular'), local('SourceSansPro-Regular'), url(https://fonts.gstatic.com/s/sourcesanspro/v10/ODelI1aHBYDBqgeIAH2zlBM0YzuT7MdOe03otPbuUS0.woff) format('woff');\r\n    }\r\n    @font-face {\r\n      font-family: 'Source Sans Pro';\r\n      font-style: normal;\r\n      font-weight: 700;\r\n      src: local('Source Sans Pro Bold'), local('SourceSansPro-Bold'), url(https://fonts.gstatic.com/s/sourcesanspro/v10/toadOcfmlt9b38dHJxOBGFkQc6VGVFSmCnC_l7QZG60.woff) format('woff');\r\n    }\r\n  }\r\n  /**\r\n   * Avoid browser level font resizing.\r\n   * 1. Windows Mobile\r\n   * 2. iOS / OSX\r\n   */\r\n  body,\r\n  table,\r\n  td,\r\n  a {\r\n    -ms-text-size-adjust: 100%; /* 1 */\r\n    -webkit-text-size-adjust: 100%; /* 2 */\r\n  }\r\n  /**\r\n   * Remove extra space added to tables and cells in Outlook.\r\n   */\r\n  table,\r\n  td {\r\n    mso-table-rspace: 0pt;\r\n    mso-table-lspace: 0pt;\r\n  }\r\n  /**\r\n   * Better fluid images in Internet Explorer.\r\n   */\r\n  img {\r\n    -ms-interpolation-mode: bicubic;\r\n  }\r\n  /**\r\n   * Remove blue links for iOS devices.\r\n   */\r\n  a[x-apple-data-detectors] {\r\n    font-family: inherit !important;\r\n    font-size: inherit !important;\r\n    font-weight: inherit !important;\r\n    line-height: inherit !important;\r\n    color: inherit !important;\r\n    text-decoration: none !important;\r\n  }\r\n  /**\r\n   * Fix centering issues in Android 4.4.\r\n   */\r\n  div[style*=\"margin: 16px 0;\"] {\r\n    margin: 0 !important;\r\n  }\r\n  body {\r\n    width: 100% !important;\r\n    height: 100% !important;\r\n    padding: 0 !important;\r\n    margin: 0 !important;\r\n  }\r\n  /**\r\n   * Collapse table borders to avoid space between cells.\r\n   */\r\n  table {\r\n    border-collapse: collapse !important;\r\n  }\r\n  a {\r\n    color: #1a82e2;\r\n  }\r\n  img {\r\n    height: auto;\r\n    line-height: 100%;\r\n    text-decoration: none;\r\n    border: 0;\r\n    outline: none;\r\n  }\r\n  </style>\r\n\r\n</head>\r\n<body style=\"background-color: #e9ecef;\">\r\n\r\n  <!-- start preheader -->\r\n  <div class=\"preheader\" style=\"display: none; max-width: 0; max-height: 0; overflow: hidden; font-size: 1px; line-height: 1px; color: #fff; opacity: 0;\">\r\n    A preheader is the short summary text that follows the subject line when an email is viewed in the inbox.\r\n  </div>\r\n  <!-- end preheader -->\r\n\r\n  <!-- start body -->\r\n  <table border=\"0\" cellpadding=\"0\" cellspacing=\"0\" width=\"100%\">\r\n\r\n    <!-- start logo -->\r\n    <tr>\r\n      <td align=\"center\" bgcolor=\"#e9ecef\">\r\n        <!--[if (gte mso 9)|(IE)]>\r\n        <table align=\"center\" border=\"0\" cellpadding=\"0\" cellspacing=\"0\" width=\"600\">\r\n        <tr>\r\n        <td align=\"center\" valign=\"top\" width=\"600\">\r\n        <![endif]-->\r\n        <table border=\"0\" cellpadding=\"0\" cellspacing=\"0\" width=\"100%\" style=\"max-width: 600px;\">\r\n          <tr>\r\n            <td align=\"center\" valign=\"top\" style=\"padding: 36px 24px;\">\r\n              <a href=\"https://www.blogdesire.com\" target=\"_blank\" style=\"display: inline-block;\">\r\n              </a>\r\n            </td>\r\n          </tr>\r\n        </table>\r\n        <!--[if (gte mso 9)|(IE)]>\r\n        </td>\r\n        </tr>\r\n        </table>\r\n        <![endif]-->\r\n      </td>\r\n    </tr>\r\n    <!-- end logo -->\r\n\r\n    <!-- start hero -->\r\n    <tr>\r\n      <td align=\"center\" bgcolor=\"#e9ecef\">\r\n        <!--[if (gte mso 9)|(IE)]>\r\n        <table align=\"center\" border=\"0\" cellpadding=\"0\" cellspacing=\"0\" width=\"600\">\r\n        <tr>\r\n        <td align=\"center\" valign=\"top\" width=\"600\">\r\n        <![endif]-->\r\n        <table border=\"0\" cellpadding=\"0\" cellspacing=\"0\" width=\"100%\" style=\"max-width: 600px;\">\r\n          <tr>\r\n            <td align=\"left\" bgcolor=\"#ffffff\" style=\"padding: 36px 24px 0; font-family: 'Source Sans Pro', Helvetica, Arial, sans-serif; border-top: 3px solid #d4dadf;\">\r\n              <h1 style=\"margin: 0; font-size: 32px; font-weight: 700; letter-spacing: -1px; line-height: 48px;\">Confirm Your Email Address</h1>\r\n            </td>\r\n          </tr>\r\n        </table>\r\n        <!--[if (gte mso 9)|(IE)]>\r\n        </td>\r\n        </tr>\r\n        </table>\r\n        <![endif]-->\r\n      </td>\r\n    </tr>\r\n    <!-- end hero -->\r\n\r\n    <!-- start copy block -->\r\n    <tr>\r\n      <td align=\"center\" bgcolor=\"#e9ecef\">\r\n        <!--[if (gte mso 9)|(IE)]>\r\n        <table align=\"center\" border=\"0\" cellpadding=\"0\" cellspacing=\"0\" width=\"600\">\r\n        <tr>\r\n        <td align=\"center\" valign=\"top\" width=\"600\">\r\n        <![endif]-->\r\n        <table border=\"0\" cellpadding=\"0\" cellspacing=\"0\" width=\"100%\" style=\"max-width: 600px;\">\r\n\r\n          <!-- start copy -->\r\n          <tr>\r\n            <td align=\"left\" bgcolor=\"#ffffff\" style=\"padding: 24px; font-family: 'Source Sans Pro', Helvetica, Arial, sans-serif; font-size: 16px; line-height: 24px;\">\r\n              <p style=\"margin: 0;\">Tap the button below to confirm your email address. If you didn't create an account with <a href=\"https://http://battlefieldcasiit.org/\">Battlefield Casiit</a>, you can safely delete this email.</p>\r\n            </td>\r\n          </tr>\r\n          <!-- end copy -->\r\n\r\n          <!-- start button -->\r\n          <tr>\r\n            <td align=\"left\" bgcolor=\"#ffffff\">\r\n              <table border=\"0\" cellpadding=\"0\" cellspacing=\"0\" width=\"100%\">\r\n                <tr>\r\n                  <td align=\"center\" bgcolor=\"#ffffff\" style=\"padding: 12px;\">\r\n                    <table border=\"0\" cellpadding=\"0\" cellspacing=\"0\">\r\n                      <tr>\r\n                        <td align=\"center\" bgcolor=\"#1a82e2\" style=\"border-radius: 6px;\">\r\n                          <a style=\"display: inline-block; padding: 16px 36px; font-family: 'Source Sans Pro', Helvetica, Arial, sans-serif; font-size: 16px; color: #ffffff; text-decoration: none; border-radius: 6px;\">" + verificationCode + "</a>\r\n                        </td>\r\n                      </tr>\r\n                    </table>\r\n                  </td>\r\n                </tr>\r\n              </table>\r\n            </td>\r\n          </tr>\r\n          <!-- end button -->\r\n\r\n          <!-- start copy -->\r\n \r\n          <tr>\r\n            <td align=\"left\" bgcolor=\"#ffffff\" style=\"padding: 24px; font-family: 'Source Sans Pro', Helvetica, Arial, sans-serif; font-size: 16px; line-height: 24px; border-bottom: 3px solid #d4dadf\">\r\n              <p style=\"margin: 0;\">Cheers,<br> Battlefield Casiit</p>\r\n            </td>\r\n          </tr>\r\n          <!-- end copy -->\r\n\r\n        </table>\r\n        <!--[if (gte mso 9)|(IE)]>\r\n        </td>\r\n        </tr>\r\n        </table>\r\n        <![endif]-->\r\n      </td>\r\n    </tr>\r\n    <!-- end copy block -->\r\n\r\n    <!-- start footer -->\r\n    <tr>\r\n      <td align=\"center\" bgcolor=\"#e9ecef\" style=\"padding: 24px;\">\r\n        <!--[if (gte mso 9)|(IE)]>\r\n        <table align=\"center\" border=\"0\" cellpadding=\"0\" cellspacing=\"0\" width=\"600\">\r\n        <tr>\r\n        <td align=\"center\" valign=\"top\" width=\"600\">\r\n        <![endif]-->\r\n        <table border=\"0\" cellpadding=\"0\" cellspacing=\"0\" width=\"100%\" style=\"max-width: 600px;\">\r\n\r\n          <!-- start permission -->\r\n          <tr>\r\n            <td align=\"center\" bgcolor=\"#e9ecef\" style=\"padding: 12px 24px; font-family: 'Source Sans Pro', Helvetica, Arial, sans-serif; font-size: 14px; line-height: 20px; color: #666;\">\r\n              <p style=\"margin: 0;\">You received this email because we received a request to create an account with the email " + recipientEmail + ". If you didn't make this request, you can safely delete this email.</p>\r\n            </td>\r\n          </tr>\r\n          <!-- end permission -->\r\n\r\n          <!-- start unsubscribe -->\r\n          <tr>\r\n            <td align=\"center\" bgcolor=\"#e9ecef\" style=\"padding: 12px 24px; font-family: 'Source Sans Pro', Helvetica, Arial, sans-serif; font-size: 14px; line-height: 20px; color: #666;\">\r\n              <p style=\"margin: 0;\">15000 Graduation Dr, Haymarket, VA 20169</p>\r\n            </td>\r\n          </tr>\r\n          <!-- end unsubscribe -->\r\n\r\n        </table>\r\n        <!--[if (gte mso 9)|(IE)]>\r\n        </td>\r\n        </tr>\r\n        </table>\r\n        <![endif]-->\r\n      </td>\r\n    </tr>\r\n    <!-- end footer -->\r\n\r\n  </table>\r\n  <!-- end body -->\r\n\r\n</body>\r\n</html>";
        //        var smtp = new SmtpClient
        //        {
        //            Host = "smtp.gmail.com",
        //            Port = 587,
        //            EnableSsl = true,
        //            DeliveryMethod = SmtpDeliveryMethod.Network,
        //            UseDefaultCredentials = false,
        //            Credentials = new NetworkCredential(fromAddress.Address, fromPassword)
        //        };
        //        using (var message = new MailMessage(fromAddress, toAddress) { Subject = subject, Body = body })
        //        {
        //            message.IsBodyHtml = true;
        //            smtp.Send(message);
        //        }
        //    }
        //    catch
        //    {
        //        // email failed to send
        //        return false;
        //    }

        //    // email sent successfully
        //    return true;
        //}

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