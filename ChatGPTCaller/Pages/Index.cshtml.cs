using ChatGPTCaller.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using ChatGPTCaller.Models;

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
        public ChatGPT_API_Response.APIResponse Completion { get; set; }
        public Exception APIException { get; set; }
        public void OnGet()
        {
        }

        public async Task<IActionResult> OnPost()
        {
            if (ModelState.IsValid)
            {
                try
                {
					Completion = await _chatGPTService.GetAPIResponse(Input.Prompt);
				}
                catch (Exception ex){
                    APIException = ex;
                }
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
