using Microsoft.AspNetCore.Mvc;
using Service.Interfaces;

namespace Web.Controllers;

[ApiController]
[Route("[controller]")]
public class CustomerController : ControllerBase
{
    private readonly IUnitOfWork _unit;

    public CustomerController(IUnitOfWork unit)
    {
        _unit = unit;
    }

    [HttpGet]
    public async Task<ActionResult> GetAsync()
    {
        return Ok(await _unit.Customer.AllAsync());
    }
}
