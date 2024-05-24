using BookService.Admin.Startup.Features.Employees.Errors;
using BookService.Admin.Startup.Services;
using BookService.Domain.Repositories;
using Ftsoft.Application.Cqs.Mediatr;
using Ftsoft.Common.Result;

namespace BookService.Admin.Startup.Features.Auth;

public class LoginEmployeeCommand : Command
{
    public string Email { get; set; }
    public string Password { get; set; }
}

public sealed class LoginEmployeeCommandHandler(IAuthService authService, ICryptService cryptService, IUserProvider userProvider, IEmployeeRepository employeeRepository) : CommandHandler<LoginEmployeeCommand>
{
    public override async Task<Result> Handle(LoginEmployeeCommand request, CancellationToken cancellationToken)
    {
        var employee = await employeeRepository.SingleOrDefaultAsync(x => x.Email == request.Email, cancellationToken);
        if (employee is null)
        {
            return Error(EmployeeNotFoundError.Instance);
        }

        var isPasswordValid = cryptService.Verify(request.Password, employee.Password);
        if (!isPasswordValid)
        {
            return Error(EmployeeNotFoundError.Instance);
        }

        await authService.Authorize(employee.Email, employee.Name, "Employee", employee.Id.ToString(), DateTime.UtcNow.AddDays(1));
        return Successful();
    }
}