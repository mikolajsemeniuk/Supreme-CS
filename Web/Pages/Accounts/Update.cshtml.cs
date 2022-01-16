using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Service.Enums;
using Service.Interfaces;
using Web.Inputs;

namespace Web.Pages.Accounts;

public class UpdateModel : PageModel
{
    private readonly IUnitOfWork _unit;
    
    [BindProperty]
    public UpdateAccountInput Input { get; set; } = new();

    public UpdateModel(IUnitOfWork unit)
    {
        _unit = unit;
    }

    public async Task<IActionResult> OnGet(Guid id)
    {
        var account = await _unit.Account.SingleAsync(account => account.Id == id, track: Track.NoTracking);
        if (account is null)
        {
            return RedirectToPage($"/NotFound", new { message = $"Account with id: {id} does not exist" });
        }
        Input.Id = account.Id;
        Input.FullName = account.FullName;
        Input.EmailAddress = account.EmailAddress;
        Input.PhoneNumber = account.PhoneNumber;
        Input.PersonalUrl = account.PersonalUrl;
        Input.YearsOfAge = account.YearsOfAge;
        Input.IsExternalContractor = account.IsExternalContractor;
        Input.RelationshipStatus = account.RelationshipStatus;
        Input.Note = account.Note;
        return Page();
    }

    public async Task<IActionResult> OnPost()
    {
        if (!ModelState.IsValid)
        {
            return Page();
        }
        var account = await _unit.Account.SingleAsync(account => account.Id == Input.Id);
        if (account is null)
        {
            return RedirectToPage($"/NotFound", new { message = $"Account with id: {Input.Id} does not exist" });
        }
        account.FullName = Input.FullName;
        account.EmailAddress = Input.EmailAddress;
        account.PhoneNumber = Input.PhoneNumber;
        account.PersonalUrl = Input.PersonalUrl;
        account.YearsOfAge = Input.YearsOfAge;
        account.IsExternalContractor = Input.IsExternalContractor;
        account.RelationshipStatus = Input.RelationshipStatus;
        account.Note = Input.Note;
        _unit.Account.Update(account);
        if (await _unit.SaveChangesAsync() > 0)
        {
            return RedirectToPage("/Accounts/Index");
        }
        return RedirectToPage($"/BadRequest", new { message = "Error occured with database, try again later" });
    }
}