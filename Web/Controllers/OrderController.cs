using System.Net.Mime;
using Data.Entities;
using Microsoft.AspNetCore.Mvc;
using Service.Enums;
using Service.Interfaces;
using Web.Inputs;

namespace Web.Controllers;

public class OrderController : BaseController
{
    private readonly IUnitOfWork _unit;

    public OrderController(IUnitOfWork unit)
    {
        _unit = unit;
    }

    /// <summary>
    /// Get All Orders
    /// </summary>
    [HttpGet]
    [Consumes(MediaTypeNames.Application.Json)]
    [Produces(MediaTypeNames.Application.Json)]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ActionResult<IEnumerable<Account>>> Get()
    {
        return Ok(await _unit.Order.AllAsync());
    }

    /// <summary>
    /// Get Order by id
    /// </summary>
    [HttpGet("{id}")]
    [Consumes(MediaTypeNames.Application.Json)]
    [Produces(MediaTypeNames.Application.Json)]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<Account>> GetById(Guid id)
    {
        var order = await _unit.Order.SingleAsync(order => order.Id == id, track: Track.NoTracking);
        if (order is null)
        {
            return NotFound();
        }
        return Ok(order);
    }

    /// <summary>
    /// Add Order
    /// </summary>
    [HttpPost("{accountId}")]
    [Consumes(MediaTypeNames.Application.Json)]
    [Produces(MediaTypeNames.Application.Json)]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<Order>> Add(Guid accountId, [FromBody] OrderInput input)
    {
        var order = new Order(input.Name, accountId);
        _unit.Order.Add(order);
        if (await _unit.SaveChangesAsync() > 0)
        {
            return CreatedAtAction(nameof(GetById), new { id = order.Id }, order);
        }
        return BadRequest();
    }

    /// <summary>
    /// Update order
    /// </summary>
    [HttpPatch("{id}")]
    [Consumes(MediaTypeNames.Application.Json)]
    [Produces(MediaTypeNames.Application.Json)]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<Account>> Update(Guid id, [FromBody] OrderInput input)
    {
        var order = await _unit.Order.SingleAsync(order => order.Id == id);
        if (order is null)
        {
            return NotFound();
        }
        order.Name = input.Name;
        _unit.Order.Update(order);
        if (await _unit.SaveChangesAsync() > 0)
        {
            return Ok(order);
        }
        return BadRequest();
    }

    /// <summary>
    /// Remove order
    /// </summary>
    [HttpDelete("{id}")]
    [Consumes(MediaTypeNames.Application.Json)]
    [Produces(MediaTypeNames.Application.Json)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<object>> Remove(Guid id)
    {
        var order = await _unit.Order.SingleAsync(order => order.Id == id);
        if (order is null)
        {
            return NotFound();
        }
        _unit.Order.Remove(order);
        if (await _unit.SaveChangesAsync() > 0)
        {
            return NoContent();
        }
        return BadRequest();
    }
}