using BookService.Admin.Startup.Features.Employees.Errors;
using BookService.Admin.Startup.Features.Employees.Models;
using BookService.Domain.Models;
using BookService.Domain.Repositories;
using Ftsoft.Application.Cqs.Mediatr;
using Ftsoft.Common.Result;
using Microsoft.AspNetCore.Mvc;

namespace BookService.Admin.Startup.Features.Employees;

public class GetEmployeeQuery : Query<EmployeeDto>
{
    [FromRoute] public long Id { get; set; }
}

public sealed class GetEmployeeQueryHandler(IEmployeeRepository employeeRepository) : QueryHandler<GetEmployeeQuery, EmployeeDto>
{
    public override async Task<Result<EmployeeDto>> Handle(GetEmployeeQuery request, CancellationToken cancellationToken)
    {
        var employee = await employeeRepository.SingleOrDefaultAsync(x => x.Id == request.Id, cancellationToken);
        if (employee is null)
        {
            return Error(EmployeeNotFoundError.Instance);
        }

        var result = new EmployeeDto()
        {
            Name = employee.Name,
            Email = employee.Email,
            Id = employee.Id,
            Position = employee.Position,
            PhoneNumber = employee.PhoneNumber,
            RestaurantId = employee.RestaurantId
        };
        return Successful(result);
    }
}