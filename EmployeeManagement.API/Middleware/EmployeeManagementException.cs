using EmployeeManagement.Core.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeManagement.Core.Exceptions
{
    public class EmployeeManagementException : IMiddleware
    {
        private readonly ILogger<EmployeeManagementException> _empExecption;
        public EmployeeManagementException(ILogger<EmployeeManagementException> empExecption)
        {
            _empExecption = empExecption;
        }
        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            try
            {
                await next(context);
            }
            catch(Exception ex)
            {
                _empExecption.LogError("Some Error Occured: " + ex.Message);

                context.Response.StatusCode = 500;
                context.Response.ContentType = "application/json";
                await context.Response.WriteAsJsonAsync(new
                {
                    Message = "Internal Server Error",
                    StatusCode = 500
                });
            }
        }
    }
    public static class EmployeeManagementExecptionExtension
    {
        public static IApplicationBuilder UseEmployeeException(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<EmployeeManagementException>();
        }
    }
}
