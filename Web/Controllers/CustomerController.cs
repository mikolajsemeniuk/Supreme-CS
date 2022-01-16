using Data.Entities;
using Microsoft.AspNetCore.Mvc;
using Service.Enums;
using Service.Interfaces;
using Web.Inputs;

namespace Web.Controllers;

public class AccountController : BaseController
{
    private readonly IUnitOfWork _unit;

    public AccountController(IUnitOfWork unit)
    {
        _unit = unit;
    }

    /// <summary>
    /// Get All Accounts
    /// </summary>
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ActionResult> Get()
    {
        return Ok(await _unit.Account.AllAsync());
    }

    /// <summary>
    /// Get Account by id
    /// </summary>
    [HttpGet("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [Produces("application/json")]
    public async Task<ActionResult> Get(Guid id)
    {
        var customer = await _unit.Account.SingleAsync(customer => customer.Id == id, track: Track.NoTracking);
        if (customer is null)
        {
            return NotFound();
        }
        return Ok(customer);
    }

    /// <summary>
    /// Add account
    /// </summary>
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [Produces("application/json")]
    public async Task<ActionResult> Add([FromBody] AccountInput input)
    {
        var account = new Account(input.FirstName, input.LastName, 
            input.EmailAddress, input.PhoneNumber, 
            input.PersonalUrl, input.YearsOfAge, 
            input.RelationshipStatus);
        _unit.Account.Add(account);
        if (await _unit.SaveChangesAsync() > 0)
        {
            return Ok(account);
        }
        return BadRequest();
    }

    /// <summary>
    /// Update account
    /// </summary>
    [HttpPatch("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [Produces("application/json")]
    public async Task<ActionResult> Update(Guid id, [FromBody] AccountInput input)
    {
        var account = await _unit.Account.SingleAsync(account => account.Id == id);
        if (account is null)
        {
            return NotFound();
        }
        account.FirstName = input.FirstName;
        account.LastName = input.LastName;
        account.EmailAddress = input.EmailAddress;
        account.PhoneNumber = input.PhoneNumber;
        account.PersonalUrl = input.PersonalUrl;
        account.YearsOfAge = input.YearsOfAge;
        account.RelationshipStatus = input.RelationshipStatus;
        _unit.Account.Update(account);
        if (await _unit.SaveChangesAsync() > 0)
        {
            return Ok(account);
        }
        return BadRequest();
    }

    /// <summary>
    /// Remove account
    /// </summary>
    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [Produces("application/json")]
    public async Task<ActionResult> Remove(Guid id)
    {
        var account = await _unit.Account.SingleAsync(account => account.Id == id);
        if (account is null)
        {
            return NotFound();
        }
        _unit.Account.Remove(account);
        if (await _unit.SaveChangesAsync() > 0)
        {
            return Ok(account);
        }
        return BadRequest();
    }
}
