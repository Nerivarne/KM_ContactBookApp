namespace ContactBook.Interfaces
{
    public interface IEmailService
    {
        void SendEmail(string userEmail, string subject, string body);
        void SendEmailWithNewPassword(string userEmail);
    }
}