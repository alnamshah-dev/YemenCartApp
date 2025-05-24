using Ecommerce.Application.DTOs.Cart;
using Ecommerce.Application.DTOs.Response;
using Ecommerce.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.Application.Services.Interfaces.Cart
{
    public interface IPaymentService
    {
        Task<ServiceResponse> Pay(decimal TotalAmount,IEnumerable<Product> cartProducts,IEnumerable<ProcessCart> carts);
    }
}
