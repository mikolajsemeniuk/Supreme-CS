using System.Net.Mime;
using Data.Entities;
using Data.Enums;
using Microsoft.AspNetCore.Mvc;
using Service.Enums;
using Service.Interfaces;
using Web.Inputs;

namespace Web.Controllers;

public class OrderController : BaseController
{
    private readonly IUnitOfWork _unit;
    private Random Random = new();

    public OrderController(IUnitOfWork unit)
    {
        _unit = unit;
    }

    /// <summary>
    /// Get all orders
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
    /// Get order by id
    /// </summary>
    [HttpGet("{orderId}")]
    [Consumes(MediaTypeNames.Application.Json)]
    [Produces(MediaTypeNames.Application.Json)]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<Account>> GetById(Guid orderId)
    {
        var order = await _unit.Order.SingleAsync(order => order.OrderId == orderId, track: Track.NoTracking);
        if (order is null)
        {
            return NotFound();
        }
        return Ok(order);
    }

    /// <summary>
    /// Add order
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
            return CreatedAtAction(nameof(GetById), new { orderId = order.OrderId }, order);
        }
        return BadRequest();
    }

    /// <summary>
    /// Process orders
    /// </summary>
    [HttpPost("process")]
    [Consumes(MediaTypeNames.Application.Json)]
    [Produces(MediaTypeNames.Application.Json)]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<object>> Proccess()
    {
        var orders = await _unit.Order.AllAsync(
            filter: order => order.OrderStatus == OrderStatus.Awaiting || order.OrderStatus == OrderStatus.Open,
            track: Track.Tracking);

        var x = Random.NextDouble() > 0.5;

        orders.ToList().ForEach(order => 
        {
            if (Random.NextDouble() > 0.6)
            {
                order.OrderStatus = OrderStatus.Resolved;
            }
            else
            {
                if (order.FailureCounter < 3)
                {
                    order.OrderStatus = OrderStatus.Open;
                    order.FailureCounter += 1;
                }
                else
                {
                    order.OrderStatus = OrderStatus.Rejected;
                }
            }
            _unit.Order.Update(order);
        });
        if (await _unit.SaveChangesAsync() > 0)
        {
            return Ok();
        }

        return BadRequest();
    }

    /// <summary>
    /// Update order
    /// </summary>
    [HttpPatch("{orderId}")]
    [Consumes(MediaTypeNames.Application.Json)]
    [Produces(MediaTypeNames.Application.Json)]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<Account>> Update(Guid orderId, [FromBody] OrderInput input)
    {
        var order = await _unit.Order.SingleAsync(order => order.OrderId == orderId);
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
    [HttpDelete("{orderId}")]
    [Consumes(MediaTypeNames.Application.Json)]
    [Produces(MediaTypeNames.Application.Json)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<object>> Remove(Guid orderId)
    {
        var order = await _unit.Order.SingleAsync(order => order.OrderId == orderId);
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