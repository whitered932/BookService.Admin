using BookService.Admin.Startup.Features.Auth;
using BookService.Admin.Startup.Features.Client;
using BookService.Admin.Startup.Features.Client.Auth;
using BookService.Admin.Startup.Features.Client.Auth.Models;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BookService.Admin.Startup.Controllers.Client;

public class AuthController(IMediator mediator) : ClientBaseController(mediator)
{
    [HttpPost("sendEmail")]
    public async Task<IActionResult> SendEmail([FromBody] SendEmailCommand command) =>
        await Execute(command);

    [HttpGet("login")]
    public async Task<IActionResult> Login([FromQuery] LoginCommand command) => await Execute(command);
}