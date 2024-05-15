using BookService.Admin.Startup.Features.Employees.Errors;
using BookService.Admin.Startup.Features.Employees.Models;
using BookService.Admin.Startup.Services;
using BookService.Domain.Repositories;
using Ftsoft.Application.Cqs.Mediatr;
using Ftsoft.Common.Result;
using Microsoft.AspNetCore.Mvc;

namespace BookService.Admin.Startup.Features.Employees;

public class UpdateEmployeeCommand : Command
{
    [FromRoute] public long Id { get; set; }

    [FromBody] public UpdateEmployeeDto Data { get; set; }
}

public sealed class UpdateEmployeeCommandHandler(
    ICryptService cryptService,
    IEmployeeRepository employeeRepository,
    IRestaurantRepository restaurantRepository) : CommandHandler<UpdateEmployeeCommand>
{
    public override async Task<Result> Handle(UpdateEmployeeCommand request, CancellationToken cancellationToken)
    {
        var employee = await employeeRepository.SingleOrDefaultAsync(x => x.Id == request.Id, cancellationToken);
        if (employee is null)
        {
            return Error(EmployeeNotFoundError.Instance);
        }

        var data = request.Data;
        var hashedPassword = cryptService.Hash(data.Password);
        employee.Update(data.Name, data.PhoneNumber, data.Email, data.Position, hashedPassword);
        await employeeRepository.UnitOfWork.SaveChangesAsync(cancellationToken);
        return Successful();
    }
}