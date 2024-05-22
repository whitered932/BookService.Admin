using BookService.Admin.Startup.Features.Client.Auth.Errors;
using BookService.Admin.Startup.Features.Client.Auth.Models;
using BookService.Admin.Startup.Features.Employees.Errors;
using BookService.Admin.Startup.Features.Reservations.Errors;
using BookService.Admin.Startup.Services;
using BookService.Domain.Repositories;
using Ftsoft.Application.Cqs.Mediatr;
using Ftsoft.Common.Result;

namespace BookService.Admin.Startup.Features.Client;

public class GetProfileQuery : Query<ClientProfileDto>
{
}

public sealed class GetProfileQueryHandler(IAdminRepository adminRepository, IEmployeeRepository employeeRepository, IUserProvider userProvider, IClientRepository clientRepository)  : QueryHandler<GetProfileQuery, ClientProfileDto>
{
    public override async Task<Result<ClientProfileDto>> Handle(GetProfileQuery request, CancellationToken cancellationToken)
    {
        var email = userProvider.Email;
        var role = userProvider.Role;
        var name = string.Empty;
        var phone = string.Empty;
        
        switch (role)
        {
            case "Employee":
                var employee = await employeeRepository.SingleOrDefaultAsync(x => x.Email == email, cancellationToken);
                if (employee is null)
                {
                    return Error(EmployeeNotFoundError.Instance);
                }
                name = employee.Name;
                email = employee.Email;
                break;
            case "Admin":
                var admin = await adminRepository.SingleOrDefaultAsync(x => x.Email == email, cancellationToken);
                if (admin is null)
                {
                    return Error(ClientNotFoundError.Instance);
                }
                name = admin.Email;
                email = admin.Email;
                break;
            case "Client":
                var client = await clientRepository.SingleOrDefaultAsync(x => x.Email == email, cancellationToken);
                if (client is null)
                {
                    return Error(TokenExpiredError.Instance);
                }
                break;
            default:
                return Error(ClientNotFoundError.Instance);
        }
        
        var result = new ClientProfileDto()
        {
            Email = email!,
            Name = name,
            PhoneNumber = phone
        };
        return Successful(result);
    }
}

