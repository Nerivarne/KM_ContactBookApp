using ContactBook.Interfaces;
using ContactBook.Models;
using ContactBook.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace ContactBook.Controllers
{
    public class ContactController : Controller
	{
		private readonly IUserService userService;
		private readonly IContactService contactService;
		private readonly ContactViewModel contactViewModel;
		public ContactController(IUserService userService, IContactService contactService)
		{
			this.userService = userService;
			this.contactService = contactService;
			this.contactViewModel = new ContactViewModel();
		}

		[HttpGet("contacts")]
		public IActionResult AllContacts()
		{
			var loggedInUserId = Convert.ToInt32(Request.Cookies["ActiveUser"]);
			var loggedInUser = userService.GetUserById(loggedInUserId);
			contactViewModel.ListOfContacts = userService.MyContacts(loggedInUser);
			return View("AllContacts", contactViewModel);
		}


		[HttpPost("add")]
		public IActionResult AddContact(string firstName, string lastName, string telephoneNumber, string email, string address)
		{
            var loggedInUserId = Convert.ToInt32(Request.Cookies["ActiveUser"]);
            var loggedInUser = userService.GetUserById(loggedInUserId);
            contactService.CreateContact(loggedInUserId, firstName, lastName, telephoneNumber, email, address);
            contactViewModel.ListOfContacts = userService.MyContacts(loggedInUser);
			return View("AllContacts", contactViewModel);
		}

		[HttpGet("delete")]
		public IActionResult DeleteContact(int contactId)
		{
			var loggedInUserId = Convert.ToInt32(Request.Cookies["ActiveUser"]);
			var loggedInUser = userService.GetUserById(loggedInUserId);
			contactService.DeleteContact(contactId, loggedInUser);
			contactViewModel.ListOfContacts = userService.MyContacts(loggedInUser);
			return View("AllContacts", contactViewModel);

		}

		[HttpGet("edit")]
		public IActionResult EditContact(int contactId)
		{
			Contact editedContact = contactService.GetContactById(contactId);
			return View("EditContact", editedContact);
		}

		[HttpPost("edit")]
		public IActionResult EditContact(int contactId, string newFirstName, string newLastName, string newTelephoneNumber, string newEmail, string newAddress)
		{
			Contact editedContact = contactService.GetContactById(contactId);
			var loggedInUserId = Convert.ToInt32(Request.Cookies["ActiveUser"]);
			var loggedInUser = userService.GetUserById(loggedInUserId);
			contactService.EditContact(editedContact, newFirstName, newLastName, newTelephoneNumber, newEmail, newAddress);
			contactViewModel.ListOfContacts = userService.MyContacts(loggedInUser);
			return View("AllContacts", contactViewModel);
		}

	}
}
