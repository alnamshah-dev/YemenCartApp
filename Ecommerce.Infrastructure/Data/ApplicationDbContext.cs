using Ecommerce.Domain.Entities;
using Ecommerce.Domain.Entities.Cart;
using Ecommerce.Domain.Entities.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.Infrastructure.Data
{
    public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : IdentityDbContext<AppUser>(options)
    {
        public DbSet<Category> Categories =>Set<Category>();
        public DbSet<Product> Products =>Set<Product>();
        public DbSet<Achieve> Achieves =>Set<Achieve>();
        public DbSet<PaymentMethod> PaymentMethods =>Set<PaymentMethod>();
        public DbSet<RefreshToken> RefreshTokens => Set<RefreshToken>();
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.Entity<PaymentMethod>().HasData(new PaymentMethod
            {
                Id = Guid.NewGuid(),
                Name = "Credit Card"
            });
            builder.Entity<IdentityRole>().HasData(
                new IdentityRole
                {
                    Id = Guid.NewGuid().ToString(),
                    Name = "Admin",
                    NormalizedName = "ADMIN"
                },
                new IdentityRole
                {
                    Id = Guid.NewGuid().ToString(),
                    Name = "User",
                    NormalizedName = "USER"
                });
        }
    }
}
