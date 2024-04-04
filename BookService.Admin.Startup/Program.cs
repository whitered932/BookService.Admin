using BookService.Admin.Startup.Services;
using BookService.Domain.Repositories;
using BookService.Infrastructure.Storage;
using BookService.Infrastructure.Storage.Repositories;
using Ftsoft.Storage.EntityFramework;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddControllers();
builder.Services.AddMediatR(x => x.RegisterServicesFromAssembly(typeof(Program).Assembly));
builder.Services.AddHttpContextAccessor();

builder.Services.RegisterRepository<IRestaurantRepository, RestaurantRepository>();
builder.Services.RegisterRepository<ITableRepository, TableRepository>();
builder.Services.RegisterRepository<IClientRepository, ClientRepository>();
builder.Services.RegisterRepository<IAuthorizationTokenRepository, AuthorizationTokenRepository>();

builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options =>
    {
        options.ExpireTimeSpan = TimeSpan.FromDays(1);
        options.SlidingExpiration = true;
    });

builder.Services.AddScoped<IEmailSenderService, EmailSenderService>();


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