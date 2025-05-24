using Ecommerce.Application.DTOs.Product;
using Ecommerce.Application.DTOs.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.Application.Services.Interfaces
{
    public interface IProductService
    {
        Task<ServiceResponse> CreateAsync(CreateProduct product);
        Task<ServiceResponse> UpdateAsync(UpdateProduct product);
        Task<ServiceResponse> DeleteAsync(Guid id);
        Task<GetProduct> Get(Guid id);
        Task<IEnumerable<GetProduct>> GetAll();

    }
}
