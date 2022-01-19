using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Service.Interfaces;
using Web.Inputs;

namespace Web.Pages.Orders;

public class AddModel : PageModel
{
    private readonly IUnitOfWork _unit;

    [BindProperty]
    public OrderInput Input { get; set; } = new OrderInput();

    public AddModel(IUnitOfWork unit)
    {
        _unit = unit;
    }

    public async Task OnPost()
    {
        await Task.Delay(TimeSpan.FromSeconds(1));
    }
}