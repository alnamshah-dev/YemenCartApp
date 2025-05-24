using AutoMapper;
using Ecommerce.Application.DTOs.Category;
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
    public class CategoryService(IGenericInterface<Category> _category, IMapper _map) : ICategoryService
    {
        public async Task<ServiceResponse> CreateAsync(CreateCategory category)
        {
            var data=_map.Map<Category>(category);
            var result=await _category.CreateAsync(data);
            return Response(result, $"{data.Name} added successfully", $"{data.Name} failed to create");
        }

        public async Task<ServiceResponse> DeleteAsync(Guid Id)
        {
            var result = await _category.DeleteAsync(Id);
            return Response(result, $"Category with {Id} deleted successfully", $"{Id} failed to delete");
        }

        public async Task<IEnumerable<GetCategory>> GetAll()
        {
            var result=await _category.GetAll();
            return _map.Map<IEnumerable<GetCategory>>(result);
        }

        public async Task<GetCategory> Get(Guid Id)
        {
            var result=await _category.Get(Id);
            return _map.Map<GetCategory>(result);
        }

        public async Task<ServiceResponse> UpdateAsync(UpdateCategory category)
        {
            var data=_map.Map<Category>(category);
            var result=await _category.UpdateAsync(data);
            return Response(result, "Category Updated Successfully", "Category failed to Update");
        }
        private ServiceResponse Response(int result, string successfulMessage, string failureMessage)
        {
            return result > 0 ? new ServiceResponse(true, successfulMessage) : new ServiceResponse(false, failureMessage);
        }
    }
}
