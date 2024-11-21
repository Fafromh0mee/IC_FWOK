using IC_FWOK.Data;
using IC_FWOK.Models;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace IC_FWOK.Services
{
    public class UserService
    {
        private readonly ApplicationDbContext _context;

        public UserService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<bool> RegisterUser(User user)
        {
            if (await _context.Users.AnyAsync(u => u.UserName == user.UserName || u.Email == user.Email))
            {
                return false; // User already exists
            }

            _context.Users.Add(user);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<User> LoginUser(string username, string password)
        {
            // Assuming PasswordHash is used for storing hashed passwords
            return await _context.Users.FirstOrDefaultAsync(u => u.UserName == username && u.PasswordHash == password);
        }
    }
}
