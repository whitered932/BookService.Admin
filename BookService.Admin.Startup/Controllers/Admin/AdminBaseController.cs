using BookService.Admin.Startup.Features.Auth;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BookService.Admin.Startup.Controllers.Admin;

[Authorize(Policy = "AdminOnly")]
[Route(("api/admin/[controller]"))]
public class AdminBaseController(IMediator mediator) : BaseController(mediator)
{
   
}