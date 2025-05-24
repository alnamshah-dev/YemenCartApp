using AutoMapper;
using Ecommerce.Application.DTOs.Cart;
using Ecommerce.Application.DTOs.Response;
using Ecommerce.Application.Services.Interfaces.Cart;
using Ecommerce.Domain.Entities;
using Ecommerce.Domain.Entities.Cart;
using Ecommerce.Domain.Interface;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ecommerce.Application.Services.Implementations.Cart
{
    public class CartService(ICart _cart, IMapper _map, IGenericInterface<Product> _productInterface, IPaymentMethodService _paymethodService, IPaymentService _paymentService) : ICartService
    {
        public async Task<ServiceResponse> CheckOut(Checkout checkout)
        {
            var (product, totalAmounts) = await GetCartTotalAmount(checkout.Carts);
            var paymentMethod = await _paymethodService.GetPaymentMethodsAsync();
            if (checkout.PaymentMethodId == paymentMethod.FirstOrDefault()!.Id)
                return await _paymentService.Pay(totalAmounts, product, checkout.Carts);
            else
                return new ServiceResponse(false, "Invalid Payment method");
        }

        public async Task<ServiceResponse> SaveCheckoutHistory(IEnumerable<CreateAchieve> achieves)
        {
            var Data = _map.Map<IEnumerable<Achieve>>(achieves);
            var result = await _cart.SaveCheckOutHistory(Data);
            return result > 0 ? new ServiceResponse(true, "Checkout achieves") : new ServiceResponse(false, "error occurred in Saving!");
        }

       


        private async Task<(IEnumerable<Product>, decimal)> GetCartTotalAmount(IEnumerable<ProcessCart> carts)
        {
            if (!carts.Any()) return (Enumerable.Empty<Product>(), 0);
            var products = await _productInterface.GetAll();
            if (products.Any()) return (Enumerable.Empty<Product>(), 0);
            var cartProducts = carts.Select(cartItem => products.FirstOrDefault(p => p.Id == cartItem.ProductId))
            .Where(product => product != null).ToList();
            var totalAmounts = carts.Where(cartItem => cartProducts.Any(p => p.Id == cartItem.ProductId))
                .Sum(cartItem => cartItem.Quantity * cartProducts.First(p => p.Id == cartItem.ProductId)!.Price);
            return (cartProducts!, totalAmounts);
        }
    }
}
