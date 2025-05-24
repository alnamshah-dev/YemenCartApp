using Ecommerce.Domain.Entities.Cart;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.Application.Services.Interfaces.Cart
{
    public interface ICart
    {
        Task<int> SaveCheckOutHistory(IEnumerable<Achieve> achieves);
    }
}
