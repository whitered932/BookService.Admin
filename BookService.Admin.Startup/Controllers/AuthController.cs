using BookService.Admin.Startup.Features.Auth;
using BookService.Admin.Startup.Features.Client;
using BookService.Admin.Startup.Features.Client.Auth.Models;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BookService.Admin.Startup.Controllers;

[Route(("api/[controller]"))]
public class AuthController(IMediator mediator) : BaseController(mediator)
{
    [Authorize]
    [HttpGet("profile")]
    public async Task<ActionResult<ClientProfileDto>> Profile([FromQuery] GetProfileQuery query) => await Execute(query);
    
    [AllowAnonymous]
    [HttpPost("admin/login")]
    public async Task<IActionResult> Login([FromBody] LoginAdminCommand command) => await Execute(command);
    
    [AllowAnonymous]
    [HttpPost("employee/login")]
    public async Task<IActionResult> Login([FromBody] LoginEmployeeCommand command) => await Execute(command);
}