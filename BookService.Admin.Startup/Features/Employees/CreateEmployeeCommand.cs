using BookService.Admin.Startup.Features.Employees.Errors;
using BookService.Admin.Startup.Features.Employees.Models;
using BookService.Admin.Startup.Features.Restaurant.Errors;
using BookService.Admin.Startup.Services;
using BookService.Domain.Models;
using BookService.Domain.Repositories;
using Ftsoft.Application.Cqs.Mediatr;
using Ftsoft.Common.Result;
using Microsoft.AspNetCore.Mvc;
using IResult = Ftsoft.Common.Result.IResult;

namespace BookService.Admin.Startup.Features.Employees;

public class CreateEmployeeCommand : Command
{
    [FromBody] public CreateEmployeeDto Data { get; set; }
}

public sealed class CreateEmployeeCommandHandler(ICryptService cryptService, IEmployeeRepository employeeRepository,
    IRestaurantRepository restaurantRepository) : CommandHandler<CreateEmployeeCommand>
{
    protected override async Task<IResult> CanHandle(CreateEmployeeCommand request, CancellationToken cancellationToken)
    {
        var employee = await employeeRepository.SingleOrDefaultAsync(
            x => x.Email == request.Data.Email && x.RestaurantId == request.Data.RestaurantId, cancellationToken);
        if (employee is not null)
        {
            return Error(EmployeeAlreadyExistsError.Instance);
        }

        var restaurant =
            await restaurantRepository.SingleOrDefaultAsync(x => x.Id == request.Data.RestaurantId, cancellationToken);
        if (restaurant is null)
        {
            return Error(RestaurantNotFoundError.Instance);
        }

        return Successful();
    }

    public override async Task<Result> Handle(CreateEmployeeCommand request, CancellationToken cancellationToken)
    {
        var hashedPassword = cryptService.Hash(request.Data.Password);
        var employee = new Employee(
            request.Data.Name,
            request.Data.PhoneNumber,
            request.Data.Email,
            request.Data.Position,
            request.Data.RestaurantId,
            hashedPassword);
        await employeeRepository.AddAsync(employee, cancellationToken);
        await employeeRepository.UnitOfWork.SaveChangesAsync(cancellationToken);
        return Successful();
    }
}