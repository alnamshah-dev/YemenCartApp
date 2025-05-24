using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.Application.DTOs.Response
{
    public record ServiceResponse(bool Flag=false,string message=null!);
}
