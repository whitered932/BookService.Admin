using BookService.Admin.Startup.Features.Reservations;
using BookService.Admin.Startup.Features.Restaurant.Models;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace BookService.Admin.Startup.Controllers.Client;

public class ReservationsController(IMediator mediator) : ClientBaseController(mediator)
{
    [HttpGet("timeslots")]
    public async Task<ActionResult<IReadOnlyList<TimeSlotDto>>> GetTimeSlots([FromQuery] GetTimeSlotsQuery query) => await Execute(query);
    
    [HttpPost("reserve")]
    public async Task<IActionResult> Reserve(CreateReservationCommand command) => await Execute(command);
}