using IC_FWOK.Data;
using IC_FWOK.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace IC_FWOK.Controllers
{
    public class AccountController : Controller
    {
        private readonly ApplicationDbContext _context;

        public AccountController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }

        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Login(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = _context.Users.SingleOrDefault(u => u.UserName == model.Username && u.PasswordHash == model.Password);
                if (user != null)
                {
                    // Handle successful login
                    return RedirectToAction("Index", "Home");
                }

                // Handle failed login
                ViewBag.ErrorMessage = "Invalid username or password";
            }

            return View(model);
        }

        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Register(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = new User
                {
                    Email = model.Email,
                    PasswordHash = model.Password,
                    FullName = model.FullName,
                    PhoneNumber = model.PhoneNumber,
                    UserName = model.Username // Fix for CS9035: Required member 'User.UserName' must be set in the object initializer or attribute constructor.
                };

                _context.Users.Add(user);
                _context.SaveChanges();

                return RedirectToAction("Login");
            }

            return View(model);
        }
    }
}
