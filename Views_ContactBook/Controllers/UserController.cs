using ContactBook.Interfaces;
using ContactBook.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace ContactBook.Controllers
{
    public class UserController : Controller
    {
        private readonly IUserService userService;
        private readonly IEmailService emailService;
        public UserController(IUserService userService, IEmailService emailService)
        {
            this.userService = userService;
            this.emailService = emailService;
        }

        [HttpGet("")]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost("")]
        public IActionResult Login(string email, string password)
        {
            var loggedInUser = userService.GetUserByEmail(email);
            if (loggedInUser == null)
                return RedirectToAction("Register");
            var loggedInUserId = loggedInUser.Id.ToString();
            Response.Cookies.Append("ActiveUser", loggedInUserId, new CookieOptions());
            if (!loggedInUser.PasswordCheck(password))
                return View();
            if (userService.DoesUserEmailExist(email) && loggedInUser.PasswordCheck(password))
            {
                return RedirectToAction("AllContacts", "Contact", new { area = "" });
            }
            return View();
        }

        [HttpGet("register")]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost("register")]
        public IActionResult Register(string email, string password)
        {
            var newUser = userService.CreateNewUser(email, password);
            var newUserId = newUser.Id.ToString();
            Response.Cookies.Append("ActiveUser", newUserId, new CookieOptions());
            return RedirectToAction("AllContacts", "Contact", new { area = "" });

        }

        [HttpGet("new-password")]
        public IActionResult ForgottenPassword()
        {
            return View();
        }

        [HttpPost("new-password")]
        public IActionResult ForgottenPassword(string userEmail)
        {
            emailService.SendEmailWithNewPassword(userEmail);
            return RedirectToAction("Login");
        }

        [HttpGet("edit-user")]
        public IActionResult EditUser()
        {
            return View();
        }

        [HttpPost("edit-user")]
        public IActionResult EditUser(string newEmail, string newPassword)
        {
            var loggedInUserId = Convert.ToInt32(Request.Cookies["ActiveUser"]);
            var loggedInUser = userService.GetUserById(loggedInUserId);
            userService.EditUser(loggedInUser, newEmail, newPassword);
            return RedirectToAction("AllContacts", "Contact", new { area = "" });
        }

        [HttpGet("logout")]
        public IActionResult Logout()
        {
            Response.Cookies.Delete("ActiveUser");
            return RedirectToAction("Login");
        }

    }
}
