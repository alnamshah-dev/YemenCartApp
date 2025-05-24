using Ecommerce.Infrastructure.Middleware.Interface;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.Infrastructure.Middleware.ChineOfRepo.ConcreteClasses
{
    public class GeneralExceptionHandler : IExceptionHandler
    {
        public bool CanHandle(Exception exception) => true;

        public async Task HandleAsync(HttpContext context, Exception exception)
        {
            context.Response.StatusCode=StatusCodes.Status500InternalServerError;
            await context.Response.WriteAsync($"An error occurred : {exception.Message}");
        }
    }
}
