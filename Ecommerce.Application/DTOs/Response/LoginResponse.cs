using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace Ecommerce.Application.DTOs.Response
{
    public record LoginResponse(bool Flag=false, string Messge = null!,string Token = null!,string RefreshToken=null!);
}