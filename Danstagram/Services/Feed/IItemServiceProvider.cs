using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Threading.Tasks;

namespace Danstagram.Services.Feed
{
    interface IItemServiceProvider<T> where T : IEntity
    {
        Task<IReadOnlyCollection<T>> GetAllItemsAsync();
        Task<T> GetItemAsync(Guid id);
        Task CreateItemAsync(T item);
    }
}
