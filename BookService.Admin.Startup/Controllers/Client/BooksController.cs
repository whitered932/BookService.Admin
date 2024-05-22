using BookService.Admin.Startup.Features.Client;
using BookService.Admin.Startup.Features.Reservations.Models;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BookService.Admin.Startup.Controllers.Client;

[Authorize(Policy = "ClientOnly")]
public class BooksController(IMediator mediator) : ClientBaseController(mediator)
{
    [HttpGet]
    public async Task<ActionResult<IReadOnlyList<ReservationDto>>> GetBooks(GetClientReservationsQuery query) =>
        await Execute(query);

    [HttpGet("{id:long}")]
    public async Task<ActionResult<ReservationDto>> GetBook(GetClientReservationQuery query) => await Execute(query);
}