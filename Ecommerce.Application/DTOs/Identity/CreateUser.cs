using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.Application.DTOs.Identity
{
    public class CreateUser:UserBase
    {
        public string Name {  get; set; }
        public string ConfirmPassword {  get; set; }
    }
}
