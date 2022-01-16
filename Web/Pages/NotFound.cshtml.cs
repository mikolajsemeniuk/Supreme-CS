using Microsoft.AspNetCore.Mvc.RazorPages;
using Service.Interfaces;

namespace Web.Pages;

public class NotFoundModel : PageModel
{
    private readonly IUnitOfWork _unit;
    public string? Message { get; set; }

    public NotFoundModel(IUnitOfWork unit)
    {
        _unit = unit;
    }

    public void OnGet(string? message = null)
    {
        Message = message;
    }
}