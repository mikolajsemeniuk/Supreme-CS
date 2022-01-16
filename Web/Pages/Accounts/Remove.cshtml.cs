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

    public async Task<IActionResult> OnGet(Guid id)
    {
        var account = await _unit.Account.SingleAsync(account => account.Id == id);
        if (account is null)
        {
            return RedirectToPage($"/NotFound", new { message = $"Account with id: {id} does not exist" });
        }
        _unit.Account.Remove(account);
        if (await _unit.SaveChangesAsync() > 0)
        {
            return RedirectToPage("/Accounts/Index");
        }
        return RedirectToPage($"/BadRequest", new { message = "Error occured with database, try again later" });
    }
}