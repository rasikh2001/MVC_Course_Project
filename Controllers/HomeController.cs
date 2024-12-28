using communityApp.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace communityApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
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
        public IActionResult Login(string username, string password)  // Capital L
        {
            // Your login logic here
            if (username == "admin" && password == "admin")  // Example
            {
                return RedirectToAction("Index", "Home");
            }
            else
            {
                ViewBag.ErrorMessage = "Invalid credentials";
                return View();
            }
        }


        public IActionResult sign_up()
        {
            return View();  // This will render the sign _up.cshtml view
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
