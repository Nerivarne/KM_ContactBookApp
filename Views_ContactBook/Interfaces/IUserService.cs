using ContactBook.Models;

namespace ContactBook.Interfaces
{
    public interface IUserService
    {
        User GetUserByEmail(string userEmail);
        User GetUserById(int userId);
        User CreateNewUser(string email, string password);
        bool DoesUserEmailExist(string userEmail);
        List<Contact> MyContacts(User user);
        bool ValidationPassword(string email, string password);
        string GenerateRandomPassword();
        string ForgottenPassword(string userEmail);
        void EditUser(User editedUser, string newPassword, string newEmail);
        bool IsUserEmailExists(string userEmail);
    }
}
