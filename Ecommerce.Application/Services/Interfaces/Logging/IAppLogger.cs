using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.Application.Services.Interfaces.Logging
{
    public interface IAppLogger<TValue>
    {
        void LogInformation(string message);
        void LogError(Exception ex,string message);
        void LogWarning(string message);
    }
}
