using Data.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Service.Enums;
using Service.Interfaces;

namespace Web.Pages.Accounts;

public class DetailsModel : PageModel
{
    private readonly IUnitOfWork _unit;
    public Account Account { get; set; } = null!;

    public DetailsModel(IUnitOfWork unit)
    {
        _unit = unit;
    }

    public async Task<IActionResult> OnGet(Guid accountId)
    {
        var account = await _unit.Account.SingleAsync(account => account.AccountId == accountId, track: Track.NoTracking);
        if (account is null)
        {
            return RedirectToPage($"/Errors/NotFound", new { message = $"Account with id: {accountId} does not exist" });
        }
        Account = account;
        return Page();
    }
}