using Ftsoft.Common.Result;

namespace BookService.Admin.Startup.Features.Employees.Errors;

public class EmployeeAlreadyExistsError : Error
{
    public override string Type => nameof(EmployeeAlreadyExistsError);
    public static EmployeeAlreadyExistsError Instance => new();
}