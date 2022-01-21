using System.Net.Mime;
using Data.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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
    /// Get all accounts
    /// </summary>
    [HttpGet]
    [Consumes(MediaTypeNames.Application.Json)]
    [Produces(MediaTypeNames.Application.Json)]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ActionResult<IEnumerable<Account>>> Get()
    {
        return Ok(await _unit.Account.AllAsync(include: account => account.Include(account => account.Orders)));
    }

    /// <summary>
    /// Get account by id
    /// </summary>
    [HttpGet("{accountId}")]
    [Consumes(MediaTypeNames.Application.Json)]
    [Produces(MediaTypeNames.Application.Json)]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<Account>> GetById(Guid accountId)
    {
        var customer = await _unit.Account.SingleAsync(account => account.AccountId == accountId, track: Track.NoTracking);
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
    public async Task<ActionResult<Account>> Add([FromBody] AccountInput input)
    {
        var account = new Account(input.FullName,
            input.EmailAddress, input.PhoneNumber,
            input.PersonalUrl, input.YearsOfAge,
            input.IsExternalContractor, input.RelationshipStatus,
            input.Note);
        _unit.Account.Add(account);
        if (await _unit.SaveChangesAsync() > 0)
        {
            return CreatedAtAction(nameof(GetById), new { accountId = account.AccountId }, account);
        }
        return BadRequest();
    }

    /// <summary>
    /// Update account
    /// </summary>
    [HttpPatch("{accountId}")]
    [Consumes(MediaTypeNames.Application.Json)]
    [Produces(MediaTypeNames.Application.Json)]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<Account>> Update(Guid accountId, [FromBody] AccountInput input)
    {
        var account = await _unit.Account.SingleAsync(account => account.AccountId == accountId);
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
        account.UpdatedAt = DateTime.Now;
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
    [HttpDelete("{accountId}")]
    [Consumes(MediaTypeNames.Application.Json)]
    [Produces(MediaTypeNames.Application.Json)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<object>> Remove(Guid accountId)
    {
        var account = await _unit.Account.SingleAsync(account => account.AccountId == accountId);
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
