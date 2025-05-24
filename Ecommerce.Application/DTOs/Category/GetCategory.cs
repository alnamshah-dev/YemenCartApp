using Ecommerce.Application.DTOs.Product;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.Application.DTOs.Category
{
    public class GetCategory : CategoryBase
    {
        public Guid Id { get; set; }
        public ICollection<GetProduct>? Products { get; set; }
    }
}
