using BookService.Admin.Startup.Features.Reservations;
using BookService.Admin.Startup.Features.Reservations.Models;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace BookService.Admin.Startup.Controllers.Employee;

public class BooksController(IMediator mediator) : EmployeeBaseController(mediator)
{
    [HttpGet]
    public Task<ActionResult<IReadOnlyList<ReservationDto>>> Books(GetReservationsQuery query) => Execute(query);
    
    [HttpGet("{id:long}")]
    public Task<ActionResult<ReservationDto>> Book(GetReservationQuery query) => Execute(query);
    
    [HttpDelete("{id:long}")]
    public Task<IActionResult> Delete(DeleteReservationCommand command) => Execute(command);
    
    [HttpPut("{id:long}")]
    public Task<IActionResult> Update(UpdateReservationCommand query) => Execute(query);
    
    [HttpPost("submit")]
    public Task<IActionResult> Submit([FromBody] SubmitReservationCommand command) => Execute(command);
    
    [HttpPost("cancel")]
    public Task<IActionResult> Cancel([FromBody] CancelReservationCommand command) => Execute(command);
    
    [HttpPost]
    public Task<IActionResult> Create(CreateReservationCommand query) => Execute(query);
}