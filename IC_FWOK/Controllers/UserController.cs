using IC_FWOK.Data;
using IC_FWOK.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace IC_FWOK.Controllers
{
    public class UserController : Controller
    {
        private readonly ApplicationDbContext _context;

        public UserController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: User/Login
        public IActionResult Login()
        {
            return View();
        }

        // POST: User/Login
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = await _context.Users
                    .FirstOrDefaultAsync(u => u.Username == model.Username && u.Password == model.Password);

                if (user == null)
                {
                    ModelState.AddModelError("", "Invalid login attempt. Please check your username and password.");
                    return View(model);
                }

                // Set user session or authentication here
                // For simplicity, we are just redirecting to a home page
                return RedirectToAction("Index", "Home");
            }
            return View(model);
        }
    }
}
