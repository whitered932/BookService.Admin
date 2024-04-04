using BookService.Admin.Startup.Features.Restaurant;
using BookService.Admin.Startup.Features.Tables;
using BookService.Admin.Startup.Features.Tables.Models;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace BookService.Admin.Startup.Controllers;

public class TablesController(IMediator mediator) : BaseController(mediator)
{
    [HttpGet]
    public async Task<ActionResult<IReadOnlyList<TableDto>>> GetMany(GetTablesQuery query) =>
        await Execute(query);

    [HttpGet("{id:long}")]
    public async Task<ActionResult<TableDto>> Get(GetTableQuery query) => await Execute(query);

    [HttpPost]
    public async Task<IActionResult> Create(CreateTableCommand command) => await Execute(command);

    [HttpDelete("{id:long}")]
    public async Task<IActionResult> Delete(DeleteTableCommand command) => await Execute(command);

    [HttpPut("{id:long}")]
    public async Task<IActionResult> Update(UpdateTableCommand command) => await Execute(command);
}