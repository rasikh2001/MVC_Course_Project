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
        public HomeController(ILogger<HomeController> logger, AppDbContext DB_context)
        {
            _logger = logger;
            _context = DB_context;
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
                                   //************** CRUD OPERATION BELOW ******************

        
        [HttpPost]//USING POST BECASUE WE ARE SENDING DATA TO DATABASE                        
        public IActionResult sign_up(User user)//***************  CREATE  *******************
        {
            if (ModelState.IsValid)
            {
                _context.Users.Add(user);
                _context.SaveChanges();

                TempData["Username"] = user.Username; // STORE USERNAME

                ViewBag.SuccessMessage = "Registration completed successfully! You can now log in.";
                return RedirectToAction("Index");

            }

            return View(user);
        }


        [HttpGet] //USING GET BECAUSE WE ARE FETCHING FROM DATABASE
                                         
        public IActionResult AdminPanel() //***************  Read   *******************
        { 
            var users = _context.Users.ToList();

            return View(users);
        }


        [HttpPost]
        public async Task<IActionResult> DeleteUser(User user)  //***************  DELETE   *******************
        {
           
            var existingUser = await _context.Users.FindAsync(user.Username);

            if (existingUser != null)
            {
                
                _context.Users.Remove(existingUser);
                await _context.SaveChangesAsync();

        
                TempData["Success"] = $"User '{user.Username}' has been deleted successfully.";
            }
            else
            {
                // Redirect to the AdminPanel view with an error message
                TempData["Error"] = $"User '{user.Username}' not found.";
            }

            return RedirectToAction("AdminPanel");
        }



        [HttpPost]
        public IActionResult Login(string username, string password) //simple if else condition to render admin panel
                                                                     //page
        {
            
            if (username == "admin" && password == "admin")
            {
                
                return RedirectToAction("AdminPanel");
            }
            else
            {

                ViewBag.ErrorMessage = "Incorrect password or username. Please try again.";
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
        public IActionResult Contact(contact contact) //CREATE OPERATION   1)
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
