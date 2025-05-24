using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.Infrastructure.Middleware.Interface
{
    public interface IExceptionHandler
    {
        bool CanHandle(Exception exception);
        Task HandleAsync(HttpContext context,Exception exception);
    }
}
