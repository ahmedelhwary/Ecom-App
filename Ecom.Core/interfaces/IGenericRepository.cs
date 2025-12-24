using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace Ecom.Core.interfaces
{
    public interface IGenericRepository <T> where T : class
    {
        Task<IReadOnlyList<T>> GetAllAsync();
        Task<IReadOnlyList<T>> GetAllAsync(params Expression<Func<T, Object>>[] includes);
        Task<T> GetByIdAsync(int id);
        Task<T> GetByIdAsync(int id,params Expression<Func<T, Object>>[] includes);
        Task AddAsync(T entity);
        Task UpdateAsync(T entity);
        Task DeleteAsync(int id);

    }
}
