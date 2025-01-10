using communityApp.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace communityApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly AppDbContext _context;

        // Constructor to initialize logger and database context
        public HomeController(ILogger<HomeController> logger, AppDbContext context)
        {
            _logger = logger;
            _context = context;
        }

        // Home page action
        public IActionResult Index()
        {
            return View();
        }

        // Render the login page
        public IActionResult Login()
        {
            return View();
        }

        // Handle login form submission
        [HttpPost]
        public IActionResult Login(string username, string password)
        {
            // Check if the username exists in the database
            var user = _context.Users.FirstOrDefault(u => u.Username == username);

            if (user != null)
            {
                // Validate the password
                if (user.Password == password)
                {
                    // Redirect to the home page if login is successful
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    // Display an error message for invalid password
                    ViewBag.ErrorMessage = "Incorrect password. Please try again.";
                }
            }
            else
            {
                // Display an error message if username is not found
                ViewBag.ErrorMessage = "Username not found. Consider registering first.";
            }

            return View();
        }

        // Render the sign-up page
        [HttpGet]
        public IActionResult sign_Up()
        {
            return View();
        }

        // Handle sign-up form submission
        [HttpPost]
        public IActionResult sign_up(User user)
        {
            if (ModelState.IsValid)
            {
                // Check if the username already exists in the database
                bool usernameExists = _context.Users.Any(u => u.Username == user.Username);
                if (usernameExists)
                {
                    ViewBag.ErrorMessage = "This username is already taken. Please choose a different one.";
                    return View(user);
                }

                // Save the new user to the database
                _context.Users.Add(user);
                _context.SaveChanges();

                ViewBag.SuccessMessage = "Registration completed successfully! You can now log in.";
                return RedirectToAction("Index");
            }

            // Return the view with validation errors
            return View(user);
        }

        // Render the chat page
        public IActionResult Chat()
        {
            return View();
        }

        // Render the contact page (GET request)
        [HttpGet]
        public IActionResult Contact()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Contact(contact contact)
        {
            if (ModelState.IsValid)
            {
                _context.Contacts.Add(contact);
                _context.SaveChanges();

                ViewBag.Success = "Your message has been sent successfully.";
                return View();
            }

            ViewBag.Error = "Please fill out all required fields.";
            return View();
        }


        // Handle errors
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            var errorViewModel = new ErrorViewModel
            {
                RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier
            };
            return View(errorViewModel);
        }
    }
}
