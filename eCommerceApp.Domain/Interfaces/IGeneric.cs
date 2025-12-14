using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace eCommerceApp.Domain.Interfaces
{
    public interface IGeneric<TEntity> where TEntity : class
    {
        Task<IEnumerable<TEntity>> GetAllAsync();
        Task<IReadOnlyList<TEntity>> GetAllAsync(params Expression<Func<TEntity, object>>[] Includes);
        Task<TEntity?> GetByIdAsync(Guid id, params Expression<Func<TEntity, object>>[] includes);
        Task<TEntity?> GetByIdAsync(Guid id);
        Task<int> AddAsync(TEntity entity);
        Task<int> UpdateAsync(TEntity entity);
        Task<int> DeleteAsync(Guid id);
        
        

    }
}
