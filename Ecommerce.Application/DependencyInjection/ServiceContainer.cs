using Ecommerce.Application.Mapper;
using Ecommerce.Application.Services.Implementations;
using Ecommerce.Application.Services.Implementations.Authentication;
using Ecommerce.Application.Services.Implementations.Cart;
using Ecommerce.Application.Services.Interfaces;
using Ecommerce.Application.Services.Interfaces.Authentication;
using Ecommerce.Application.Services.Interfaces.Cart;
using Ecommerce.Application.Validation;
using Ecommerce.Application.Validation.Authentication;
using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.Extensions.DependencyInjection;
namespace Ecommerce.Application.DependencyInjection
{
    public static class ServiceContainer
    {
        public static IServiceCollection AddApplicationService(this IServiceCollection services)
        {
            services.AddAutoMapper(typeof(MappingFile));
            services.AddScoped<ICategoryService,CategoryService>();
            services.AddScoped<IProductService,ProductService>();
            services.AddFluentValidationAutoValidation();
            services.AddValidatorsFromAssemblyContaining<CreateUserValidator>();
            services.AddScoped<IPaymentMethodService, PaymentMethodService>();
            services.AddScoped<ICartService, CartService>();
            services.AddScoped<IValidationService, ValidationService>();
            services.AddScoped<IAuthenticationService,AuthenticationService>();
            return services;
        }
    }
}
