using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Service.Interfaces;

namespace Web.Pages.Accounts;

public class RemoveModel : PageModel
{
    private readonly IUnitOfWork _unit;

    public RemoveModel(IUnitOfWork unit)
    {
        _unit = unit;
    }

    public async Task<IActionResult> OnGet(Guid accountId)
    {
        var account = await _unit.Account.SingleAsync(account => account.AccountId == accountId);
        if (account is null)
        {
            return RedirectToPage($"/Errors/NotFound", new { message = $"Account with id: {accountId} does not exist" });
        }
        _unit.Account.Remove(account);
        if (await _unit.SaveChangesAsync() > 0)
        {
            return RedirectToPage("/Accounts/Index");
        }
        return RedirectToPage($"/Errors/BadRequest", new { message = "Error occured with database, try again later" });
    }
}