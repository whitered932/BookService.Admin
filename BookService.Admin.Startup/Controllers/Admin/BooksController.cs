using BookService.Admin.Startup.Features.Reservations;
using BookService.Admin.Startup.Features.Reservations.Models;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace BookService.Admin.Startup.Controllers.Admin;

public class BooksController(IMediator mediator) : AdminBaseController(mediator)
{
    [HttpGet]
    public Task<ActionResult<IReadOnlyList<ReservationDto>>> Books(GetReservationsQuery query) => Execute(query);
    
    [HttpGet("{id:long}")]
    public Task<ActionResult<ReservationDto>> Book(GetReservationQuery query) => Execute(query);
}