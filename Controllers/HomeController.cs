using communityApp.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Login()
        {
            return View();
        }

        [HttpGet]
        public IActionResult sign_Up()
        {
            return View();
        }

        
        [HttpPost]    //CREATE OPERATION 
        public IActionResult sign_up(User user)
        {
            if (ModelState.IsValid)
            {
              

                _context.Users.Add(user);
                _context.SaveChanges();

                ViewBag.SuccessMessage = "Registration completed successfully! You can now log in.";
                return RedirectToAction("Index");
            }

         
            return View(user);
        }

        [HttpGet]
              //READ OPERATION 
        public IActionResult AdminPanel()
        { 
            var users = _context.Users.ToList();

            return View(users);
        }


        [HttpPost]
        public IActionResult Login(string username, string password)
        {
            // Check if the user is admin
            if (username == "admin" && password == "admin")
            {
                // Redirect to the AdminPanel view
                return RedirectToAction("AdminPanel");
            }

            // Check if the username exists in the database
            var user = _context.Users.FirstOrDefault(u => u.Username == username);

            if (user != null)
            {
                
                if (user.Password == password)
                {
                    
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    
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

        public IActionResult Chat()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Contact()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Contact(contact contact) //CREATE OPERATION
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
