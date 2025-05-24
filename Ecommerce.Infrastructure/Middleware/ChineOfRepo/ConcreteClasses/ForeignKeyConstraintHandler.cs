using Ecommerce.Infrastructure.Middleware.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.Infrastructure.Middleware.ChineOfRepo.ConcreteClasses
{
    public class ForeignKeyConstraintHandler : IExceptionHandler
    {
        public bool CanHandle(Exception exception)
            => exception is DbUpdateException dbUpdateEx && dbUpdateEx.InnerException is SqlException sqlEx && sqlEx.Number == 547;

        public async Task HandleAsync(HttpContext context, Exception exception)
        {
            context.Response.StatusCode = StatusCodes.Status409Conflict;
            await context.Response.WriteAsync("Foreign key constraint violation.");
        }
    }
}
