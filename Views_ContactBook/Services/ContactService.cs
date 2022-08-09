using ContactBook.Database;
using ContactBook.Interfaces;
using ContactBook.Models;

namespace ContactBook.Services
{
    public class ContactService : IContactService
    {
        private readonly AppDbContext database;
        public ContactService(AppDbContext database)
        {
            this.database = database;
        }
        public void CreateContact(int userId, string firstName, string lastName, string telephoneNumber, string email, string address)
        {
            var loggedInUser = database.Users.Find(userId);
            var newContact = new Contact()
            {
                FirstName = firstName,
                LastName = lastName,
                TelephoneNumber = telephoneNumber,
                Email = email,
                Address = address,
                UserId = loggedInUser.Id,
            };
            database.Contacts.Add(newContact);
            database.SaveChanges();
        }

		public void EditContact(Contact editedContact, string newFirstName, string newLastName, string newTelephoneNumber, string newEmail, string newAddress)
		{
			if (editedContact != null)
			{
				if (!string.IsNullOrEmpty(newFirstName))
					editedContact.FirstName = newFirstName;
				if (!string.IsNullOrEmpty(newLastName))
					editedContact.LastName = newLastName;
				if (!string.IsNullOrEmpty(newTelephoneNumber))
					editedContact.TelephoneNumber = newTelephoneNumber;
				if (!string.IsNullOrEmpty(newEmail))
					editedContact.Email = newEmail;
				if (!string.IsNullOrEmpty(newAddress))
					editedContact.Address = newAddress;
				database.SaveChanges();
            }
        }

		public Contact GetContactById(int contactId)
        {
            return database.Contacts.SingleOrDefault(c => c.Id == contactId);
        }

        public void DeleteContact(int contactId, User user)
        {
            var contact = GetContactById(contactId);
            if (contact != null && contact.UserId == user.Id)
            {
                database.Contacts.Remove(contact);
                database.SaveChanges();
            }
        }
    }
}





