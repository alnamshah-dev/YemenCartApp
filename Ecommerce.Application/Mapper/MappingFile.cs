using AutoMapper;
using Ecommerce.Application.DTOs.Cart;
using Ecommerce.Application.DTOs.Category;
using Ecommerce.Application.DTOs.Identity;
using Ecommerce.Application.DTOs.Product;
using Ecommerce.Domain.Entities;
using Ecommerce.Domain.Entities.Cart;
using Ecommerce.Domain.Entities.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.Application.Mapper
{
    public class MappingFile:Profile
    {
        public MappingFile()
        {
            CreateMap<CreateProduct, Product>();
            CreateMap<UpdateProduct, Product>();
            CreateMap<Product,GetProduct >();
            CreateMap<Category, GetCategory>();
            CreateMap<CreateCategory,Category>();
            CreateMap<UpdateCategory, Category>();
            CreateMap<CreateUser,AppUser>();
            CreateMap<LoginUser, AppUser>();
            CreateMap<CreateAchieve,Achieve>();
            CreateMap<PaymentMethod, GetPaymentMethod>();
        }
    }
}
