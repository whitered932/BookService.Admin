using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace BookService.Admin.Startup.Controllers.Admin;

[Route(("api/admin/[controller]"))]
public class AdminBaseController(IMediator mediator) : BaseController(mediator)
{
    
}