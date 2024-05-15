using BookService.Admin.Startup.Features.Employees.Errors;
using BookService.Admin.Startup.Features.Employees.Models;
using BookService.Domain.Repositories;
using Ftsoft.Application.Cqs.Mediatr;
using Ftsoft.Common.Result;
using Microsoft.AspNetCore.Mvc;

namespace BookService.Admin.Startup.Features.Employees;

public class GetEmployeesQuery : Query<IReadOnlyList<EmployeeDto>>
{
    [FromQuery] public long RestaurantId { get; set; }
}

public sealed class GetEmployeesQueryHandler(IEmployeeRepository employeeRepository) : QueryHandler<GetEmployeesQuery, IReadOnlyList<EmployeeDto>>
{
    public override async Task<Result<IReadOnlyList<EmployeeDto>>> Handle(GetEmployeesQuery request, CancellationToken cancellationToken)
    {
        var employees = await employeeRepository.ListAsync(x => x.RestaurantId == request.RestaurantId, cancellationToken);
        var result = employees.Select(x => new EmployeeDto()
        {
            Name = x.Name,
            Email = x.Email,
            Id = x.Id,
            Position = x.Position,
            PhoneNumber = x.PhoneNumber,
            RestaurantId = x.RestaurantId
        }).ToList();
        return Successful(result);
    }
}
