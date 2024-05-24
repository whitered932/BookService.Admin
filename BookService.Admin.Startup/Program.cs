using System.Net;
using BookService.Admin.Startup.Services;
using BookService.Admin.Startup.Services.Implementations;
using BookService.Domain.Repositories;
using BookService.Infrastructure.Storage;
using BookService.Infrastructure.Storage.Repositories;
using Ftsoft.Storage.EntityFramework;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddControllers();
builder.Services.AddMediatR(x => x.RegisterServicesFromAssembly(typeof(Program).Assembly));
builder.Services.AddHttpContextAccessor();

builder.Services.RegisterRepository<IRestaurantRepository, RestaurantRepository>();
builder.Services.RegisterRepository<ITableRepository, TableRepository>();
builder.Services.RegisterRepository<IReservationRepository, ReservationRepository>();
builder.Services.RegisterRepository<IClientRepository, ClientRepository>();
builder.Services.RegisterRepository<IAuthorizationTokenRepository, AuthorizationTokenRepository>();
builder.Services.RegisterRepository<IEmployeeRepository, EmployeeRepository>();
builder.Services.RegisterRepository<IAdminRepository, AdminRepository>();



builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options =>
    {
        options.ExpireTimeSpan = TimeSpan.FromDays(1);
        options.SlidingExpiration = true;
        options.Events = new CookieAuthenticationEvents
        {
            OnRedirectToAccessDenied = context => 
            {
                if (context.Request.Path.StartsWithSegments("/api"))
                {
                    context.Response.StatusCode = (int)(HttpStatusCode.Unauthorized);
                }
                return Task.CompletedTask;
            },
        };
    });


builder.Services.AddScoped<IEmailSenderService, EmailSenderService>();
builder.Services.AddScoped<IUserProvider, UserProvider>();
builder.Services.AddScoped<ICryptService, CryptService>();
builder.Services.AddScoped<IAuthService, AuthService>();


var db = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<BookServiceDbContext>(opt => { opt.UseNpgsql(db); });

var MyAllowSpecificOrigins = "_myAllowSpecificOrigins";
builder.Services.AddCors(options =>
{
    options.AddPolicy(name: MyAllowSpecificOrigins,
        policy =>
        {
            policy.WithOrigins("http://localhost:5173").AllowAnyHeader().AllowAnyMethod().AllowCredentials();
        });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}



app.UseCors(MyAllowSpecificOrigins);
app.UseHttpsRedirection();
app.MapControllers();

app.UseAuthentication();
app.UseAuthorization();

app.Run();