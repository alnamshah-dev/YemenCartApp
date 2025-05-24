using Ecommerce.Application.Services.Interfaces.Logging;
using Ecommerce.Infrastructure.Middleware.ChineOfRepo.ConcreteClasses;
using Ecommerce.Infrastructure.Middleware.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;


namespace Ecommerce.Infrastructure.Middleware.Handler
{
    public class ExceptionHandlingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly List<IExceptionHandler> _handlers;
        public ExceptionHandlingMiddleware(RequestDelegate next)
        {
            _next=next;
            _handlers = new List<IExceptionHandler>
            {
                new CannotInsertNullHandler(),
                new ForeignKeyConstraintHandler(),
                new UniqueConstraintHandler(),
                new GeneralExceptionHandler()
            };
        }
        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                var logger = context.RequestServices.GetRequiredService<IAppLogger<ExceptionHandlingMiddleware>>();
                context.Response.ContentType= "application/json";
                var handler = _handlers.FirstOrDefault(h=>h.CanHandle(ex));
                if (handler != null)
                {
                    logger.LogError(ex, "Sql exception");
                    await handler.HandleAsync(context,ex);
                }
                else 
                {
                    logger.LogError(ex, "Related EFCore Exception");
                    context.Response.StatusCode=StatusCodes.Status500InternalServerError;
                    await context.Response.WriteAsync("An Un Expected error occurred");
                }
            }
        }

    }
}
