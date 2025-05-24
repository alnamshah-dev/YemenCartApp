using Ecommerce.Application.Services.Interfaces.Logging;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.Infrastructure.Services
{
    public class SerilogLoggerAdapter<TValue>(ILogger<TValue> _logger) : IAppLogger<TValue>
    {
        public void LogError(Exception ex, string message)
           => _logger.LogError(ex, message);

        public void LogInformation(string message)
            =>_logger.LogInformation(message);

        public void LogWarning(string message)
            =>_logger.LogWarning(message);
    }
}
