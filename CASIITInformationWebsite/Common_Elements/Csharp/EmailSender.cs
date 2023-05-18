using MailKit.Net.Smtp;
using MimeKit;

namespace CASIITInformationWebsite.Common_Elements.Csharp
{
    public class EmailSender
    {
        private const string sendername = "sender";
        private const string senderemail = "sender@email.com";

        // Sends the specified user an email to verifiy account creation
        public static void SendVerificationEmail(string username, string useremail)
        {
            MimeMessage email = new MimeMessage();
            email.From.Add(new MailboxAddress(sendername, senderemail));
            email.To.Add(new MailboxAddress(username, useremail));

            email.Subject = "Battlefield Casiit Website Email Verification"; 
            email.Body = new TextPart(MimeKit.Text.TextFormat.Plain) // Still need to add the link
            {
                Text = "Click on this verification link to confirm you signed into battlefieldcasiit.org",
            };

            using (SmtpClient smtp = new SmtpClient())
            {
                smtp.Connect("smtp.gmail.com", 587, false); // If we are not using gmail change this
                smtp.Authenticate("[ACCOUNT EMAIL]", "[ACCOUNT PASSWORD]");
                smtp.Send(email);
                smtp.Disconnect(true);
            }
        }
    }
}