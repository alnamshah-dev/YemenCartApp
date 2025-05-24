using Ecommerce.Application.Exceptions;
using Ecommerce.Domain.Interface;
using Ecommerce.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.Infrastructure.Repositories
{
    public class GenericRepository<T>(ApplicationDbContext _context) : IGenericInterface<T> where T : class
    {
        public async Task<int> CreateAsync(T entity)
        {
           await _context.Set<T>().AddAsync(entity);
            return await _context.SaveChangesAsync();

        }

        public async Task<int> DeleteAsync(Guid id)
        {
            var result=await Get(id);
            if (result is null) return 0;
             _context.Set<T>().Remove(result);
            return await _context.SaveChangesAsync();
        }

        public async Task<T> Get(Guid id)
        {
            var result= await _context.Set<T>().FindAsync(id)??
                 throw new ItemNotFoundException($"Item with {id} not found");
            return result!;
        }

        public async Task<ICollection<T>> GetAll()
            => await _context.Set<T>().AsNoTracking().ToListAsync();

        public async Task<int> UpdateAsync(T entity)
        {
            _context.Set<T>().Update(entity!);
            return await _context.SaveChangesAsync();
        }
    }
}
