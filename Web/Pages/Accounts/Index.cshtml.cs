using Data.Entities;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Service.Interfaces;

namespace Web.Pages.Accounts;

public class IndexModel : PageModel
{
    private readonly IUnitOfWork _unit;
    public IEnumerable<Account> Accounts { get; set; } = new List<Account>();

    public IndexModel(IUnitOfWork unit)
    {
        _unit = unit;
    }

    public async Task OnGet()
    {
        Accounts = await _unit.Account.AllAsync();
    }
}