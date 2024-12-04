using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace IC_FWOK.Views.Email
{
    public class SettingsModel : PageModel
    {
        [BindProperty]
        public string Setting1 { get; set; }

        [BindProperty]
        public string Setting2 { get; set; }

        public void OnGet()
        {
            // Load settings
        }

        public IActionResult OnPost()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            // Save settings
            return RedirectToPage("/Index");
        }
    }
}