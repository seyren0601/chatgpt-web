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
        public PromptRequest Input { get; set; }
        public ChatGPT_API_Response.APIResponse Completion { get; set; }
        public Exception APIException { get; set; }
        public void OnGet()
        {
        }

        public IActionResult OnPost()
        {
            if (ModelState.IsValid)
            {
                try
                {
                    Completion = _chatGPTService.GetAPIResponse(Input).Result.Item1;
                }
                catch (Exception ex)
                {
                    APIException = ex;
                }
                return Page();
            }
            return RedirectToPage("Index");
        }
    }
}
