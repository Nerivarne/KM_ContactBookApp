using System.Text;
using ContactBook.Database;
using ContactBook.Interfaces;
using ContactBook.Models;

namespace ContactBook.Services
{
    public class UserService : IUserService
    {
        private AppDbContext database;
        public UserService(AppDbContext database)
        {
            this.database = database;
        }

        public User GetUserByEmail(string userEmail)
        {
            var searchedUser = database.Users.SingleOrDefault(u => u.Email == userEmail);
            return searchedUser;
        }

        public User GetUserById(int userId)
        {
            var searchedUser = database.Users.SingleOrDefault(u => u.Id == userId);
            return searchedUser;
        }

        public User CreateNewUser(string email, string password)
        {
            User newUser = new User(email, password);
            if (!DoesUserEmailExist(email))
            {
                database.Users.Add(newUser);
                database.SaveChanges();
            }
            return newUser;
        }

        public bool DoesUserEmailExist(string userEmail)
        {
            return database.Users.Where(u => u.Email == userEmail).Any();
        }

        public List<Contact> MyContacts(User user)
        {
            return database.Contacts.Where(c => c.UserId == user.Id).ToList();
        }

        public bool ValidationPassword(string email, string password)
        {
            var actualUser = GetUserByEmail(email);
            if (actualUser.Password == password)
                return true;
            return false;
        }

        public string GenerateRandomPassword()
        {
            int length = 10;
            string valid = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890";
            StringBuilder sb = new StringBuilder();
            Random random = new Random();
            while (0 < length--)
            {
                sb.Append(valid[random.Next(valid.Length)]);
            }
            return sb.ToString();
        }
        public string ForgottenPassword(string userEmail)
        {
            var userByEmail = GetUserByEmail(userEmail);
            if (userByEmail == null)
                return null;
            var newPassword = GenerateRandomPassword();
            userByEmail.Password = newPassword;
            database.SaveChanges();
            return newPassword;
        }

        public void EditUser(User editedUser, string newEmail, string newPassword)
        {
            if (!IsUserEmailExists(newEmail))
            {
                if (!string.IsNullOrEmpty(newEmail))
                    editedUser.Email = newEmail;
                if (!string.IsNullOrEmpty(newPassword))
                    editedUser.Password = newPassword;
                database.SaveChanges();
            }
        }

        public bool IsUserEmailExists(string userEmail)
        {
            return database.Users.Where(u => u.Email == userEmail).Any();
        }
    }
}
