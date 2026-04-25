using EmployeeManagement.Core.Helpers;
using EmployeeManagement.Infra.DatabaseContext;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using AutoMapper;
using EmployeeManagement.Core.Exceptions;
using Serilog;
using Microsoft.AspNetCore.Hosting;
using EmployeeManagement.Core.Domain.RepositoryInterface;
using EmployeeManagement.Infra.RepositoriesImpl;
using EmployeeManagement.Core.ServiceInterface;
using EmployeeManagement.Core.Services;
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddDbContext<EmployeeManagementContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("sqlConnections"));
});
builder.Services.AddTransient<IEmployeeRepository, EmployeeRepository>();
builder.Services.AddTransient<IEmployeeService, EmployeeService>();
builder.Services.AddTransient<EmployeeManagementException>();
builder.Services.AddAutoMapper(typeof(EmployeeAutoMapper));
builder.Services.AddMemoryCache();
builder.Host.UseSerilog((HostBuilderContext web,IServiceProvider service,LoggerConfiguration log) =>
{
    log.ReadFrom.Configuration(web.Configuration).ReadFrom.Services(service);
});
var app = builder.Build();

// Configure the HTTP request pipeline.
app.UseEmployeeException();
app.UseAuthorization();

app.MapControllers();

app.Run();
