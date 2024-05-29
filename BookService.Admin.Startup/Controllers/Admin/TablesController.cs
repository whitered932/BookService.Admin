using BookService.Admin.Startup.Features.Tables;
using BookService.Admin.Startup.Features.Tables.Models;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BookService.Admin.Startup.Controllers.Admin;

public class TablesController(IMediator mediator) : AdminBaseController(mediator)
{
    [Authorize(Roles = "Admin,Employee")]
    [HttpGet]
    public async Task<ActionResult<IReadOnlyList<TableDto>>> GetMany(GetTablesQuery query) =>
        await Execute(query);

    [Authorize(Roles = "Admin,Employee")]
    [HttpGet("{id:long}")]
    public async Task<ActionResult<TableDto>> Get(GetTableQuery query) => await Execute(query);

    [Authorize(Roles = "Admin")]
    [HttpPost]
    public async Task<IActionResult> Create(CreateTableCommand command) => await Execute(command);

    [Authorize(Roles = "Admin")]
    [HttpDelete("{id:long}")]
    public async Task<IActionResult> Delete(DeleteTableCommand command) => await Execute(command);

    [Authorize(Roles = "Admin")]
    [HttpPut("{id:long}")]
    public async Task<IActionResult> Update(UpdateTableCommand command) => await Execute(command);
}