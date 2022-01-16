using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Web.Pages.Errors;

public class BadRequestModel : PageModel
{
    public string? Message { get; set; }
    public void OnGet(string? message = null)
    {
        Message = message;
    }
}