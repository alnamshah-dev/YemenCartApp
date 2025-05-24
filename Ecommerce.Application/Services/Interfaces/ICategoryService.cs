using Ecommerce.Application.DTOs.Category;
using Ecommerce.Application.DTOs.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.Application.Services.Interfaces
{
    public interface ICategoryService
    {
        Task<ServiceResponse> CreateAsync(CreateCategory category);
        Task<ServiceResponse> UpdateAsync(UpdateCategory category);
        Task<ServiceResponse> DeleteAsync(Guid Id);
        Task<GetCategory> Get(Guid Id);
        Task<IEnumerable<GetCategory>> GetAll();
    }
}
