using AutoMapper;
using Ecommerce.Application.DTOs.Cart;
using Ecommerce.Application.DTOs.Response;
using Ecommerce.Application.Services.Interfaces.Cart;
using Ecommerce.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.Application.Services.Implementations.Cart
{
    public class PaymentMethodService(IPaymentMethod _paymentMethod, IMapper _map) : IPaymentMethodService
    {
        public async Task<IEnumerable<GetPaymentMethod>> GetPaymentMethodsAsync()
        {
            var methods = await _paymentMethod.GetPaymentMethodAsync();
            if (!methods.Any()) return [];
            return _map.Map<IEnumerable<GetPaymentMethod>>(methods);
        }
    }
}
