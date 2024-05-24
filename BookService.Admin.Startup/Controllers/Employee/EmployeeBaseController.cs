using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BookService.Admin.Startup.Controllers.Employee;

[Authorize(Roles = "Employee")]
[Route(("api/employee/[controller]"))]
public class EmployeeBaseController(IMediator mediator) : BaseController(mediator)
{
   
}