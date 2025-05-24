using Ecommerce.Application.DTOs.Cart;
using Ecommerce.Application.DTOs.Response;
using Ecommerce.Application.Services.Interfaces.Cart;
using Ecommerce.Domain.Entities;
using Stripe.Checkout;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.WebRequestMethods;

namespace Ecommerce.Infrastructure.Service
{
    public class StripePaymentService : IPaymentService
    {
        public async Task<ServiceResponse> Pay(decimal TotalAmount, IEnumerable<Product> cartProducts, IEnumerable<ProcessCart> carts)
        {
            try
            {
                var lineItems = new List<SessionLineItemOptions>();
                foreach (var product in cartProducts)
                {
                    var pQuantity = carts.FirstOrDefault(x => x.ProductId == product.Id);
                    lineItems.Add(new SessionLineItemOptions
                    {
                        PriceData = new SessionLineItemPriceDataOptions
                        {
                            Currency = "usd",
                            ProductData = new SessionLineItemPriceDataProductDataOptions
                            {
                                Name = product.Name,
                                Description = product.Description,
                            },
                            UnitAmount = (long)(product.Price * 100),
                        },
                        Quantity = pQuantity!.Quantity!,

                    });
                }
                var options = new SessionCreateOptions
                {
                    PaymentMethodTypes = ["usd"],
                    LineItems = lineItems,
                    Mode = "payment",
                    SuccessUrl = "https:localhost: 7000 / payment - success",
                    CancelUrl = "https:localhost:7000/payment-cancel",
                };
                var service = new SessionService();
                Session session = await service.CreateAsync(options);
                return new ServiceResponse(false, session.Url);
            }
            catch (Exception ex) 
            {
                return new ServiceResponse(false, ex.Message);
            }

        }
    }
}
