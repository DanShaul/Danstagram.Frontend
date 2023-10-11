using Danstagram.Models;
using Danstagram.Models.Interactions;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Danstagram.Services.Interactions
{
    public class InteractionServiceProvider<T> : IInteractionServiceProvider<T> where T : IInteraction
    {
        #region Properties
        private readonly IDataStore<T> dataStore;
        #endregion
        #region Contructors
        public InteractionServiceProvider()
        {
            dataStore = DependencyService.Get<IDataStore<T>>();
        }
        #endregion
        #region Methods
        public async Task<IReadOnlyCollection<T>> GetItemInteractionsAsync(Guid itemId)
        {
            return await dataStore.GetAllAsync((itemT) => itemT.ItemId == itemId);
        }

        public async Task CreateInteractionAsync(T interaction)
        {
            await dataStore.CreateAsync(interaction);
        }

        public async Task DeleteInteractionAsync(T interaction)
        {
            await dataStore.DeleteAsync(interaction.Id);
        }

        public async Task<IReadOnlyCollection<T>> GetItemUserInteractionsAsync(Guid itemId,Guid userId)
        {
            return await dataStore.GetAllAsync((entity) => entity.ItemId == itemId && entity.UserId == userId);
        }
        #endregion
    }
}
