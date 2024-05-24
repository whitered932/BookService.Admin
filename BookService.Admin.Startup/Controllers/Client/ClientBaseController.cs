using System.Security.Claims;
using BookService.Admin.Startup.Features.Restaurant;
using BookService.Admin.Startup.Features.Restaurant.Models;
using MediatR;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BookService.Admin.Startup.Controllers.Client;

[Route(("api/[controller]"))]
[AllowAnonymous]
public class ClientBaseController(IMediator mediator) : BaseController(mediator)
{
 
}