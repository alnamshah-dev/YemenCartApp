using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.Domain.Interface
{
    public interface IGenericInterface<T> where T : class
    {
        Task<ICollection<T>> GetAll();
        Task<T> Get(Guid id);
        Task<int> CreateAsync(T entity);
        Task<int> DeleteAsync(Guid id);
        Task<int> UpdateAsync(T entity);

    }
}
