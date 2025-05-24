using AutoMapper;
using Ecommerce.Application.DTOs.Product;
using Ecommerce.Application.DTOs.Response;
using Ecommerce.Application.Services.Interfaces;
using Ecommerce.Domain.Entities;
using Ecommerce.Domain.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Ecommerce.Application.Services.Implementations
{
    public class ProductService(IGenericInterface<Product> _product,IMapper _map) : IProductService
    {
        public async Task<ServiceResponse> CreateAsync(CreateProduct product)
        {
            var data=_map.Map<Product>(product);
            var result = await _product.CreateAsync(data);
            return Response(result, $"{data.Name} added successfully", $"{data.Name} failed to create");
        }

        public async Task<ServiceResponse> DeleteAsync(Guid id)
        {
            var result=await _product.DeleteAsync(id);
            return Response(result, $"Category with {id} deleted Successfully", $"{id} failed to delete");
        }

        public async Task<IEnumerable<GetProduct>> GetAll()
        {
            var result=await _product.GetAll();
            return _map.Map<IEnumerable<GetProduct>>(result);
        }

        public async Task<GetProduct> Get(Guid id)
        {
            var result=await _product.Get(id);
            return _map.Map<GetProduct>(result);
        }

        public async Task<ServiceResponse> UpdateAsync(UpdateProduct product)
        {
            var data =_map.Map<Product>(product);
            var result= await _product.UpdateAsync(data);
            return Response(result, "Product Updated Successfully", "Product failed to Update");
        }
        private ServiceResponse Response(int result,string successfulMessage, string failureMessage)
        {
            return result>0 ? 
                new ServiceResponse(true,successfulMessage)
                :new ServiceResponse(false,failureMessage);
        }
    }
}
