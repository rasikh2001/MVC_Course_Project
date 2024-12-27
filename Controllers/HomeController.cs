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
        public IActionResult Login(string username, string password)
        {
            // Your login logic here
            if (username == "admin" && password == "admin")  // Example
            {
                return RedirectToAction("HomePage", "Home");
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


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
