using Ecommerce.Application.Services.Interfaces.Authentication;
using Ecommerce.Application.Services.Interfaces.Cart;
using Ecommerce.Application.Services.Interfaces.Logging;
using Ecommerce.Domain.Entities;
using Ecommerce.Domain.Entities.Cart;
using Ecommerce.Domain.Entities.Identity;
using Ecommerce.Domain.Interface;
using Ecommerce.Infrastructure.Data;
using Ecommerce.Infrastructure.Middleware.Handler;
using Ecommerce.Infrastructure.Repositories;
using Ecommerce.Infrastructure.Repositories.Authentication;
using Ecommerce.Infrastructure.Repositories.Cart;
using Ecommerce.Infrastructure.Service;
using Ecommerce.Infrastructure.Services;
using EntityFramework.Exceptions.SqlServer;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Migrations.Operations;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.Infrastructure.DependencyInjection
{
    public static class ServiceContainer
    {
        public static IServiceCollection AddInfrastructureService(this IServiceCollection services,IConfiguration config)
        {
            services.AddDbContext<ApplicationDbContext>(option => option.UseSqlServer(config.GetConnectionString("Default"),
                sqlOptions =>
                {
                    sqlOptions.MigrationsAssembly(typeof(ApplicationDbContext).Assembly.FullName);
                    sqlOptions.EnableRetryOnFailure();
                }).UseExceptionProcessor(),
                ServiceLifetime.Scoped);
            services.AddScoped<IGenericInterface<Category>,GenericRepository<Category>>();
            services.AddScoped<IGenericInterface<Product>,GenericRepository<Product>>();
            services.AddScoped(typeof(IAppLogger<>), typeof(SerilogLoggerAdapter<>));
            services.AddDefaultIdentity<AppUser>(options =>
            {
                options.SignIn.RequireConfirmedEmail=true;
                options.Tokens.EmailConfirmationTokenProvider = TokenOptions.DefaultEmailProvider;
                options.Password.RequireNonAlphanumeric=true;
                options.Password.RequireLowercase=true;
                options.Password.RequireUppercase=true;
                options.Password.RequireDigit=true;
                options.Password.RequiredUniqueChars = 1;
                options.Password.RequiredLength = 8;

            }).AddRoles<IdentityRole>().AddEntityFrameworkStores<ApplicationDbContext>();
            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(options =>
            {
                options.SaveToken=true;
                options.TokenValidationParameters = new TokenValidationParameters()
                {
                    ValidateAudience = true,
                    ValidateIssuer = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    RequireExpirationTime = true,
                    ValidIssuer = config["JWT:Issuer"],
                    ValidAudience = config["JWT:Audience"],
                    ClockSkew = TimeSpan.Zero,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(config["JWT:Key"]!))
                };
            });
            services.AddScoped<ICart,CartRepository>();
            services.AddScoped<IRoleManager, RoleManagement>();
            services.AddScoped<ITokenManager,TokenManagement>();
            services.AddScoped<IUserManager, UserManagement>();
            services.AddScoped<IPaymentMethod, PaymentMethodRepository>();
            services.AddScoped<IPaymentService,StripePaymentService>();
            Stripe.StripeConfiguration.ApiKey = config["Stripe:SecretKey"];
            return services;
            
        }
        public static IApplicationBuilder UseInfrastructureService(this IApplicationBuilder app)
        {
            app.UseMiddleware<ExceptionHandlingMiddleware>();
            return app;
        }
    }
}
