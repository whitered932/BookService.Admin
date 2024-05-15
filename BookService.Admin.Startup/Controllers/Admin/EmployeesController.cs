using BookService.Admin.Startup.Features.Employees;
using BookService.Admin.Startup.Features.Employees.Models;
using BookService.Admin.Startup.Features.Reservations;
using BookService.Admin.Startup.Features.Reservations.Models;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace BookService.Admin.Startup.Controllers.Admin;

public class EmployeesController(IMediator mediator) : AdminBaseController(mediator)
{
    [HttpGet]
    public Task<ActionResult<IReadOnlyList<EmployeeDto>>> Employees(GetEmployeesQuery query) => Execute(query);
    
    [HttpGet("{id:long}")]
    public Task<ActionResult<EmployeeDto>> Employee(GetEmployeeQuery query) => Execute(query);
    
    [HttpDelete("{id:long}")]
    public Task<IActionResult> Delete(DeleteEmployeeCommand command) => Execute(command);

    [HttpPut("{id:long}")]
    public Task<IActionResult> Update(UpdateEmployeeCommand query) => Execute(query);
    
    [HttpPost]
    public async Task<IActionResult> Create(CreateEmployeeCommand command) => await Execute(command);

}