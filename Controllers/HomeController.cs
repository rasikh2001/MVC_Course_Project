using communityApp.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using BCrypt.Net;

namespace communityApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly AppDbContext _context; // Add the database context

        public HomeController(ILogger<HomeController> logger, AppDbContext context)
        {
            _logger = logger;
            _context = context; // Initialize the context
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Login()
        {
            return View();  // This will render the Login.cshtml view
        }

        [HttpPost]
        public IActionResult Login(string username, string password)
        {
            // Find the user in the database by username
            var user = _context.Users.FirstOrDefault(u => u.Username == username);

            if (user != null)
            {
                // Check if the password matches the stored password
                if (user.Password == password)
                {
                    // If credentials are valid, redirect to the home page
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    // If the password doesn't match, show an error message
                    ViewBag.ErrorMessage = "Invalid credentials. Please try again.";
                    return View();
                }
            }
            else
            {
                // If the user doesn't exist, show a registration prompt
                ViewBag.ErrorMessage = "Username not found. Please register.";
                return View();
            }
        }



        // Sign-up GET action to render the form
        [HttpGet]
        public IActionResult sign_up()
        {
            return View();  // This will render the sign_up.cshtml view
        }

        // Sign-up POST action to process the form submission
        [HttpPost]
        public IActionResult sign_up(User user)
        {
            if (ModelState.IsValid)
            {
                // Check if the username already exists
                if (_context.Users.Any(u => u.Username == user.Username))
                {
                    ViewBag.ErrorMessage = "Username already exists. Please choose another one.";
                    return View(user);
                }

                // Save the user to the database
                _context.Users.Add(user);
                _context.SaveChanges();

                ViewBag.SuccessMessage = "Registration successful! You can now log in.";
                return RedirectToAction("Index");
            }

            return View(user); // Return the form with validation errors if the model is invalid
        }

        public IActionResult chat()
        {
            return View();  // This will render the chat.cshtml view
        }

        public IActionResult contact()
        {
            return View();
        }

        [HttpGet]
        public ActionResult Contact()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Contact(string firstName, string lastName, string email, string phoneNumber, string topic, string message)
        {
            // Validation logic
            if (string.IsNullOrEmpty(firstName) || string.IsNullOrEmpty(lastName))
            {
                ViewBag.Error = "First Name and Last Name are required.";
                return View();
            }

            if (string.IsNullOrEmpty(email) || !email.Contains("@"))
            {
                ViewBag.Error = "Please enter a valid email address.";
                return View();
            }

            if (string.IsNullOrEmpty(topic))
            {
                ViewBag.Error = "Please select a topic.";
                return View();
            }

            if (string.IsNullOrEmpty(message))
            {
                ViewBag.Error = "Message cannot be empty.";
                return View();
            }

            // If all validations pass
            ViewBag.Success = "Please wait patiently. We will get back to you shortly on your provided phone number and email address.";
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
