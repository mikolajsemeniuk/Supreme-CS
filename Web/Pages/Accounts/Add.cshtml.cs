using Data.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Service.Interfaces;
using Web.Inputs;

namespace Web.Pages.Accounts;

public class AddModel : PageModel
{
    private readonly IUnitOfWork _unit;

    [BindProperty]
    public AddAccountInput Input { get; set; } = new();

    public AddModel(IUnitOfWork unit)
    {
        _unit = unit;
    }

    public async Task<IActionResult> OnPost()
    {
        if (!ModelState.IsValid)
        {
            return Page();    
        }
        var account = new Account(
                Input.FullName, Input.EmailAddress, Input.PhoneNumber,
                Input.PersonalUrl, Input.YearsOfAge,
                Input.IsExternalContractor, Input.RelationshipStatus,
                Input.Note);
        _unit.Account.Add(account);
        if (await _unit.SaveChangesAsync() > 0)
        {
            return RedirectToPage("/Accounts/Index");
        }
        return RedirectToPage($"/Errors/BadRequest", new { message = "Error occured with database, try again later" });
        
    }
}