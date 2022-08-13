using System.Net;
using System.Net.Mail;

namespace FoodLabellingSystem_Service.Auth.AuthMVC.Others
{
    public class EmailSender : IEmailSender
    {
        private string fromEmailPassword;
        private IConfiguration _config;
        public EmailSender(IConfiguration config)
        {

            _config = config;
            fromEmailPassword = config["EmailSender"];

            if (fromEmailPassword != null) {
                if (fromEmailPassword.Length < 1)
                {
                    Console.Error.WriteLine("Please set EmailSender Password in the users secret file");
                }
            }
            else
            {
                fromEmailPassword = string.Empty;
            }           
        }
        public async Task SendEmailAsync(string email, string subject, string message)
        {
            MailAddress from = new MailAddress("do-not-reply@nutritioninfo.co.uk");
            MailAddress to = new MailAddress(email);

            MailMessage mailMessage = new MailMessage(from, to);
            mailMessage.IsBodyHtml = true;
            mailMessage.SubjectEncoding = System.Text.Encoding.UTF8;
            mailMessage.Subject = subject;
            mailMessage.Body = message;
            mailMessage.DeliveryNotificationOptions = DeliveryNotificationOptions.Delay;
            await Execute(mailMessage, from);

        }
        public async Task Execute(MailMessage message, MailAddress from)
        {

            SmtpClient client = new SmtpClient
            {
                Host = "mailuk2.promailserver.com",
                EnableSsl = true,
                DeliveryMethod = SmtpDeliveryMethod.Network,
                UseDefaultCredentials = false,
               // Port = 587,
                Credentials = new NetworkCredential(from.Address, fromEmailPassword)
            };

            client.SendCompleted += (s, e) => {
                message.Dispose();
                client.Dispose();

            };

            await client.SendMailAsync(message);

        }
    }
}
