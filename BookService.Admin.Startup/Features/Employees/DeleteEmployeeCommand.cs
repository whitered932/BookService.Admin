using BookService.Admin.Startup.Features.Employees.Errors;
using BookService.Domain.Repositories;
using Ftsoft.Application.Cqs.Mediatr;
using Ftsoft.Common.Result;
using Microsoft.AspNetCore.Mvc;

namespace BookService.Admin.Startup.Features.Employees;

public class DeleteEmployeeCommand : Command
{
    [FromRoute] public long Id { get; set; }
}

public sealed class DeleteEmployeeCommandHandler(IEmployeeRepository employeeRepository, IRestaurantRepository restaurantRepository) : CommandHandler<DeleteEmployeeCommand>
{
    public override async Task<Result> Handle(DeleteEmployeeCommand request, CancellationToken cancellationToken)
    {
        var employee = await employeeRepository.SingleOrDefaultAsync(x => x.Id == request.Id, cancellationToken);
        if (employee is null)
        {
            return Error(EmployeeNotFoundError.Instance);
        }

        await employeeRepository.RemoveAsync(employee);
        await employeeRepository.UnitOfWork.SaveChangesAsync(cancellationToken);
        return Successful();
    }
}