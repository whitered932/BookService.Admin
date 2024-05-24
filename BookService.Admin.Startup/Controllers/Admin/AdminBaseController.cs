using BookService.Admin.Startup.Features.Auth;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BookService.Admin.Startup.Controllers.Admin;

[Authorize(Roles = "Admin")]
[Route(("api/admin/[controller]"))]
public class AdminBaseController(IMediator mediator) : BaseController(mediator)
{
   
}