using BookService.Admin.Startup.Features.Restaurant;
using BookService.Admin.Startup.Features.Restaurant.Models;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace BookService.Admin.Startup.Controllers.Client;

public class RestaurantsController(IMediator mediator) : ClientBaseController(mediator)
{
    [HttpGet]
    public async Task<ActionResult<IReadOnlyList<RestaurantDto>>> GetMany([FromQuery] GetRestaurantsQuery query) =>
        await Execute(query);
    
    [HttpGet("{id:long}")]
    public async Task<ActionResult<RestaurantDto>> Get([FromQuery] GetRestaurantQuery query) => await Execute(query);
    
    
}