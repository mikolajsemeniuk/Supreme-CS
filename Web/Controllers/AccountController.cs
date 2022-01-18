using System.Net.Mime;
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
    [Consumes(MediaTypeNames.Application.Json)]
    [Produces(MediaTypeNames.Application.Json)]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ActionResult<IEnumerable<Account>>> Get()
    {
        return Ok(await _unit.Account.AllAsync());
    }

    /// <summary>
    /// Get Account by id
    /// </summary>
    [HttpGet("{id}")]
    [Consumes(MediaTypeNames.Application.Json)]
    [Produces(MediaTypeNames.Application.Json)]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<Account>> GetById(Guid id)
    {
        var customer = await _unit.Account.SingleAsync(account => account.Id == id, track: Track.NoTracking);
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
    [Consumes(MediaTypeNames.Application.Json)]
    [Produces(MediaTypeNames.Application.Json)]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<Account>> Add([FromBody] AddAccountInput input)
    {
        var account = new Account(input.FullName,
            input.EmailAddress, input.PhoneNumber,
            input.PersonalUrl, input.YearsOfAge,
            input.IsExternalContractor, input.RelationshipStatus,
            input.Note);
        _unit.Account.Add(account);
        if (await _unit.SaveChangesAsync() > 0)
        {
            return CreatedAtAction(nameof(GetById), new { id = account.Id }, account);
        }
        return BadRequest();
    }

    /// <summary>
    /// Update account
    /// </summary>
    [HttpPatch("{id}")]
    [Consumes(MediaTypeNames.Application.Json)]
    [Produces(MediaTypeNames.Application.Json)]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<Account>> Update(Guid id, [FromBody] AddAccountInput input)
    {
        var account = await _unit.Account.SingleAsync(account => account.Id == id);
        if (account is null)
        {
            return NotFound();
        }
        account.FullName = input.FullName;
        account.EmailAddress = input.EmailAddress;
        account.PhoneNumber = input.PhoneNumber;
        account.PersonalUrl = input.PersonalUrl;
        account.YearsOfAge = input.YearsOfAge;
        account.Note = input.Note;
        account.IsExternalContractor = input.IsExternalContractor;
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
    [Consumes(MediaTypeNames.Application.Json)]
    [Produces(MediaTypeNames.Application.Json)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<object>> Remove(Guid id)
    {
        var account = await _unit.Account.SingleAsync(account => account.Id == id);
        if (account is null)
        {
            return NotFound();
        }
        _unit.Account.Remove(account);
        if (await _unit.SaveChangesAsync() > 0)
        {
            return NoContent();
        }
        return BadRequest();
    }
}
