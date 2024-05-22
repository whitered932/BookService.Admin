using BookService.Admin.Startup.Features.Employees.Errors;
using BookService.Admin.Startup.Services;
using BookService.Domain.Repositories;
using Ftsoft.Application.Cqs.Mediatr;
using Ftsoft.Common.Result;

namespace BookService.Admin.Startup.Features.Auth;

public class LoginAdminCommand : Command
{
    public string Email { get; set; }
    public string Password { get; set; }
}

public sealed class LoginAdminCommandHandler(IAuthService authService, ICryptService cryptService, IAdminRepository adminRepository, IUserProvider userProvider) : CommandHandler<LoginAdminCommand>
{
    public override async Task<Result> Handle(LoginAdminCommand request, CancellationToken cancellationToken)
    {
        var admin = await adminRepository.SingleOrDefaultAsync(x => x.Email == request.Email, cancellationToken);
        if (admin is null)
        {
            return Error(EmployeeNotFoundError.Instance);
        }

        var isPasswordValid = cryptService.Verify(request.Password, admin.Password);
        if (!isPasswordValid)
        {
            return Error(EmployeeNotFoundError.Instance);
        }

        await authService.Authorize(admin.Email, admin.Email, "Admin", admin.Id.ToString(), DateTime.UtcNow.AddDays(1));
        return Successful();
    }
}