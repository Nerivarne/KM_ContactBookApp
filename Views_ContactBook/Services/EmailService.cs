using ContactBook.Interfaces;
using MailKit.Net.Smtp;
using MailKit.Security;
using MimeKit;
using MimeKit.Text;

namespace ContactBook.Services
{
    public class EmailService : IEmailService
    {
        private readonly IUserService userService;
        public EmailService(IUserService userService)
        {
            this.userService = userService;
        }
        public void SendEmail(string userEmail, string subject, string body)
        {
            var email = new MimeMessage();
            email.From.Add(MailboxAddress.Parse(Environment.GetEnvironmentVariable("EmailConfig_EmailUsername")));
            email.To.Add(MailboxAddress.Parse(userEmail));
            email.Subject = $"{subject}";
            email.Body = new TextPart(TextFormat.Html)
            {
                Text = $"{body}"
            };
            using var smtp = new SmtpClient();
            smtp.Connect(Environment.GetEnvironmentVariable("EmailConfig_EmailHost"), 25, SecureSocketOptions.StartTls);
            smtp.Authenticate(Environment.GetEnvironmentVariable("EmailConfig_EmailUsername"), Environment.GetEnvironmentVariable("EmailConfig_EmailPassword"));
            smtp.Send(email);
            smtp.Disconnect(true);

        }

        public void SendEmailWithNewPassword(string userEmail)
        {
            var newPassword = userService.ForgottenPassword(userEmail);
            if (newPassword != null)
            {
                string subject = "New password ContactBook";
                string body = $"Your new password is: <h3>{newPassword}</h3>";
                SendEmail(userEmail, subject, body);
            }
        }
    }
}
