using Ecommerce.Application.Services.Interfaces.Cart;
using Ecommerce.Domain.Entities.Cart;
using Ecommerce.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.Infrastructure.Repositories.Cart
{
    public class PaymentMethodRepository(ApplicationDbContext _context) : IPaymentMethod
    {
        public async Task<IEnumerable<PaymentMethod>> GetPaymentMethodAsync()=>await _context.PaymentMethods.AsNoTracking().ToListAsync();
    }
}
