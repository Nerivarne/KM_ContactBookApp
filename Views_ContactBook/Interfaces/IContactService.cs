using ContactBook.Models;

namespace ContactBook.Interfaces
{
    public interface IContactService
    {
        void CreateContact(int userId, string firstName, string lastName, string telephoneNumber, string email, string address);
        Contact GetContactById(int contactId);
        void EditContact(Contact editedContact, string newFirstName, string newLastName, string newTelephoneNumber, string newEmail, string newAddress);
        void DeleteContact(int contactId, User user);
    }
}
