using Microsoft.AspNetCore.Mvc;
using Service.Interfaces;

namespace Web.Controllers;

public class AccountController : BaseController
{
    private readonly IUnitOfWork _unit;

    public AccountController(IUnitOfWork unit)
    {
        _unit = unit;
    }

    /// <summary>
    /// Get All Customers
    /// </summary>
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ActionResult> Get()
    {
        return Ok(await _unit.Account.AllAsync());
    }

    /// <summary>
    /// Get Customer by id
    /// </summary>
    [HttpGet("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult> Get(Guid id)
    {
        var customer = await _unit.Account.SingleAsync(customer => customer.AccountId == id);
        if (customer is null)
        {
            return NotFound();
        }
        return Ok(customer);
    }
}
