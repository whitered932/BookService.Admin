using Ftsoft.Common.Result;

namespace BookService.Admin.Startup.Features.Employees.Errors;

public class EmployeeNotFoundError : Error
{
    public override string Type => nameof(EmployeeNotFoundError);
    public static EmployeeNotFoundError Instance => new();
}