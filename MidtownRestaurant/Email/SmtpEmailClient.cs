using MidtownRestaurantSystem.Config;
using System.Net.Mail;

namespace MidtownRestaurantSystem.Email
{
    public class SmtpEmailClient : IEmailClient
    {
        public void SendMail(string clientEmail, string subject, string bodyText, string attachmentLocation)
        {
            MailMessage mail = new MailMessage();
            SmtpClient SmtpServer = new SmtpClient(EmailConfig.smtpServer);

            mail.From = new MailAddress(EmailConfig.sendFromEmail);
            mail.To.Add(clientEmail);
            mail.Subject = subject;
            mail.Body = bodyText;

            Attachment attachment = new Attachment($"{attachmentLocation}");
            mail.Attachments.Add(attachment);

            SmtpServer.Port = EmailConfig.serverPort;
            SmtpServer.Credentials = new System.Net.NetworkCredential(EmailConfig.userName, EmailConfig.password);
            SmtpServer.EnableSsl = true;

            SmtpServer.Send(mail);
        }
    }
}
