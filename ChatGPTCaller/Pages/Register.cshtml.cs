using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ChatGPTCaller.Pages
{
    public class RegisterModel : PageModel
    {
        public void OnGet()
        {
        }
        public IActionResult OnPost()
        {
            if (ModelState.IsValid)
            {
                // Process form data
                return RedirectToPage("/Register");
            }

            return Page();
        }
    }
}
