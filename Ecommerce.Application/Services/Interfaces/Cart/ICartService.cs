using Ecommerce.Application.DTOs.Cart;
using Ecommerce.Application.DTOs.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.Application.Services.Interfaces.Cart
{
    public interface ICartService
    {
        Task<ServiceResponse> SaveCheckoutHistory(IEnumerable<CreateAchieve> achieves);
        Task<ServiceResponse> CheckOut(Checkout checkout);
    }
}
