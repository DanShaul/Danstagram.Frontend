using Danstagram.Models.Interactions;
using Danstagram.Services.Feed;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Danstagram.Services.Interactions
{
    public class InteractionServiceProvider<T> : IInteractionServiceProvider<T> where T : IInteraction
    {
        #region Properties
        private readonly InteractionsApi<T> interactionsApi;
        private readonly IDataStore<T> dataStore;
        #endregion
        #region Contructors
        public InteractionServiceProvider()
        {
            dataStore = DependencyService.Get<IDataStore<T>>();
            interactionsApi = new InteractionsApi<T>();
        }
        #endregion
        #region Methods

        public async Task<IReadOnlyCollection<T>> GetItemInteractionsAsync(Guid itemId)
        {
            var databaseInteractions = await interactionsApi.GetItemInteractionsAsync(itemId);
            await dataStore.LoadDataFromBackend(databaseInteractions);
            return await dataStore.GetAllAsync((itemT) => itemT.FeedItemId == itemId);
        }

        public async Task CreateInteractionAsync(T interaction)
        {
            await interactionsApi.CreateInteractionAsync(interaction);
            await dataStore.CreateAsync(interaction);
        }

        public async Task DeleteInteractionAsync(T interaction)
        {
            await interactionsApi.DeleteInteractionAsync(interaction.Id);
            await dataStore.DeleteAsync(interaction.Id);
        }

        public async Task<IReadOnlyCollection<T>> GetItemUserInteractionsAsync(Guid itemId,Guid userId)
        {
            return await dataStore.GetAllAsync((entity) => entity.FeedItemId == itemId && entity.UserId == userId);
        }
        #endregion
    }
}
