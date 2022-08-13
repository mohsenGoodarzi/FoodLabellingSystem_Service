using System.Net.Mail;

namespace FoodLabellingSystem_Service.Auth.AuthMVC.Others
{
    public interface IEmailSender
    {
        public Task SendEmailAsync(string email, string subject, string message);
        public Task Execute(MailMessage message, MailAddress from);
    }
}