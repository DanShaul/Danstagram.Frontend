using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Threading.Tasks;

namespace Danstagram.Services.Feed
{
    public interface IItemServiceProvider<T> where T : IEntity
    {
        #region Methods
        Task<IReadOnlyCollection<T>> GetAllItemsAsync();
        Task<T> GetItemAsync(Guid id);
        Task CreateItemAsync(T item);
        Task<bool> IsUp();
        #endregion
    }
}
