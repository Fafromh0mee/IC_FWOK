using System.ComponentModel.DataAnnotations;

namespace IC_FWOK.Models
{
    public class RegisterViewModel
    {
        public string Email { get; set; }
        public string Password { get; set; }
        public string ConfirmPassword { get; set; }
        public string FullName { get; set; }
        public string PhoneNumber { get; set; }
        public string Username { get; set; } // Added Username property
    }
}
