using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Danstagram.Services
{
    public interface IDataStore<T> where T : IEntity
    {
        Task CreateAsync(T entity);
        Task<T> GetAsync(Guid id);
        Task<T> GetAsync(Expression<Func<T,bool>> filter);
        Task<IReadOnlyCollection<T>> GetAllAsync();
        Task<IReadOnlyCollection<T>> GetAllAsync(Expression<Func<T, bool>> filter);
        Task UpdateAsync(T entity);
        Task DeleteAsync(Guid id);
        Task DeleteAllAsync(Expression<Func<T, bool>> filter);
        Task LoadDataFromBackend(IReadOnlyCollection<T> list);
        
    }
}
