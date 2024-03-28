using BookService.Admin.Startup.Features.Restaurant;
using BookService.Admin.Startup.Features.Restaurant.Models;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace BookService.Admin.Startup.Controllers;

public class RestaurantsController(IMediator mediator) : BaseController(mediator)
{
    [HttpGet]
    public async Task<ActionResult<IReadOnlyList<RestaurantDto>>> GetMany([FromQuery] GetRestaurantsQuery query) =>
        await Execute(query);

    [HttpGet("{id:long}")]
    public async Task<ActionResult<RestaurantDto>> Get([FromQuery] GetRestaurantQuery query) => await Execute(query);

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateRestaurantCommand command) => await Execute(command);

    [HttpDelete("{id:long}")]
    public async Task<IActionResult> Delete(DeleteRestaurantCommand command) => await Execute(command);

    [HttpPut("{id:long}")]
    public async Task<IActionResult> Update(long id, [FromBody] CreateRestaurantDto dto)
    {
        var command = new UpdateRestaurantCommand()
        {
            Title = dto.Title,
            Description = dto.Description,
            KitchenType = dto.KitchenType,
            Contact = dto.Contact,
            ReservationThreshold = dto.ReservationThreshold,
            Cost = dto.Cost,
            EndWorkTime = dto.EndWorkTime,
            StartWorkTime = dto.StartWorkTime,
            Pictures = new List<RestaurantPictureDto>(),
            Id = id,
            Menu = dto.Menu,
        };
        var result = await mediator.Send(command);
        if (!result.IsSuccessfull)
        {
            return BadRequest(result.GetErrors().First());
        }

        return Ok();
    }
}