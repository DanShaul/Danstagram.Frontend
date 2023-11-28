using Danstagram.Models.Interactions;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Danstagram.Services.Interactions
{
    public interface IInteractionServiceProvider<T> where T : IInteraction
    {
        #region Methods
        Task CreateInteractionAsync(T interaction);
        Task DeleteInteractionAsync(T interaction);
        Task<IReadOnlyCollection<T>> GetItemInteractionsAsync(Guid itemId);
        Task<IEnumerable<T>> GetItemUserInteractionsAsync(Guid itemId,Guid userId);
        Task<bool> IsUp();
        #endregion
    }
}
