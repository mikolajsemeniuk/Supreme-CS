using Data.Entities;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Service.Interfaces;

namespace Web.Pages.Orders;

public class IndexModel : PageModel
{
    private readonly IUnitOfWork _unit;
    public IEnumerable<Order> Orders { get; set; } = new List<Order>();

    public IndexModel(IUnitOfWork unit)
    {
        _unit = unit;
    }

    public async Task OnGet(Guid accountid)
    {
        Orders = await _unit.Order.AllAsync(filter: order => order.AccountId == accountid);
    }
}