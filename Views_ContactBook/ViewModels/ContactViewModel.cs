using ContactBook.Models;

namespace ContactBook.ViewModels
{
    public class ContactViewModel
    {
        public List<Contact> ListOfContacts { get; set; }
        public ContactViewModel()
        {
            ListOfContacts = new List<Contact>();
        }
    }
}
