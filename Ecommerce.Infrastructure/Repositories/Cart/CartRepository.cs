using Ecommerce.Application.Services.Interfaces.Cart;
using Ecommerce.Domain.Entities.Cart;
using Ecommerce.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.Infrastructure.Repositories.Cart
{
    public class CartRepository(ApplicationDbContext _context) : ICart
    {
        public async Task<int> SaveCheckOutHistory(IEnumerable<Achieve> achieves)
        {
             _context.Achieves.AddRange(achieves);
            return await _context.SaveChangesAsync();

        }
    }
}
