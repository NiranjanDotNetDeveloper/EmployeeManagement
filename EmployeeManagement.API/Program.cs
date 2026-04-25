using AutoMapper;
using EmployeeManagement.Core.Domain.RepositoryInterface;
using EmployeeManagement.Core.Exceptions;
using EmployeeManagement.Core.Helpers;
using EmployeeManagement.Core.ServiceInterface;
using EmployeeManagement.Core.Services;
using EmployeeManagement.Infra.DatabaseContext;
using EmployeeManagement.Infra.RepositoriesImpl;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Serilog;
using System.IO;
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
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, "app.xml"));
});
var app = builder.Build();

// Configure the HTTP request pipeline.
app.UseEmployeeException();
if (app.Environment.IsDevelopment())
{
    app.UseHttpsRedirection();
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseAuthorization();

app.MapControllers();

app.Run();
