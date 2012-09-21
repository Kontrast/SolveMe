using System.Net;
using System.Net.Mail;

namespace SolveMe.Models
{
    /// <summary>
    /// Help to send the email
    /// </summary>
    public class EmailSender
    {
        private string messageBody;
        private string emailTo;
        /// <summary>
        /// Constructor for EmailSender
        /// </summary>
        /// <param name="messageBody">Body of the message</param>
        /// <param name="emailTo">Receiver of the message</param>
        public EmailSender(string messageBody, string emailTo)
        {
            this.messageBody = messageBody;
            this.emailTo = emailTo;
        }
        /// <summary>
        /// Send the message to receiver
        /// </summary>
        public void Send()
        {
            SmtpClient client = new SmtpClient();
            NetworkCredential basicAuthenticationInfo = new NetworkCredential("ja.kontrast@gmail.com", "rjynhfcn8kontrast");
            client.Host = "smtp.gmail.com";
            client.UseDefaultCredentials = false;
            client.Credentials = basicAuthenticationInfo;
            client.EnableSsl = true;
            MailAddress to = new MailAddress(emailTo);
            MailAddress from = new MailAddress("ja.kontrast@gmail.com", "Account activation", System.Text.Encoding.UTF8);
            MailMessage message = new MailMessage(from, to);
            message.Body = messageBody;
            message.IsBodyHtml = true;
            message.BodyEncoding = System.Text.Encoding.UTF8;
            message.Subject = "Account activation";
            message.SubjectEncoding = System.Text.Encoding.UTF8;
            client.Send(message);
        }
    }
}