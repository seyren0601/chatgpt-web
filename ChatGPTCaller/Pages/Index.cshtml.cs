using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;

namespace ChatGPTCaller.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ChatGPTService _chatGPTService;
        public IndexModel(ChatGPTService chatGPTService)
        {
            _chatGPTService = chatGPTService;
        }
        [BindProperty]
        public BindingModel Input { get; set; }
        public APIResponse Completion { get; set; }
        public void OnGet()
        {
        }

        public async Task<IActionResult> OnPost()
        {
            if (ModelState.IsValid)
            {
                Completion = await _chatGPTService.GetAPIResponse(Input.Prompt);
                return Page();
            }
            return RedirectToPage("Index");
        }

        public class BindingModel
        {
            [Required]
            public string Prompt { get; set; }
        }
    }
}
