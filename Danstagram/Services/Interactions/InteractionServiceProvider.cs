using Danstagram.Models.Interactions;
using Danstagram.Services.Feed;
using System;
using System.Collections.Generic;
using System.Linq;
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
        public async Task<bool> IsUp()
        {
            return await interactionsApi.IsUp();
        }

        public async Task<IReadOnlyCollection<T>> GetItemInteractionsAsync(Guid itemId)
        {
            var databaseInteractions = await interactionsApi.GetItemInteractionsAsync(itemId);
            await Task.Run(async () =>
            {
                await dataStore.DeleteAllAsync((entity) => entity.FeedItemId == itemId);
                foreach (var entity in databaseInteractions)
                {
                    await dataStore.CreateAsync(entity);
                }
            });
            return databaseInteractions;
        }

        public async Task CreateInteractionAsync(T interaction)
        {
            var CreateApiTask = interactionsApi.CreateInteractionAsync(interaction);
            var CreateLocalTask = dataStore.CreateAsync(interaction);
            await Task.WhenAll(CreateApiTask,CreateLocalTask);
        }

        public async Task DeleteInteractionAsync(T interaction)
        {
            if(interaction == null)
            {
                return;
            }
            var DeleteApiTask = interactionsApi.DeleteInteractionAsync(interaction.Id);
            var DeleteLocalTask = dataStore.DeleteAsync(interaction.Id);
            await Task.WhenAll(DeleteApiTask, DeleteLocalTask);
        }

        public async Task<IEnumerable<T>> GetItemUserInteractionsAsync(Guid itemId,Guid userId)
        {
            var databaseInteractions = await interactionsApi.GetItemInteractionsAsync(itemId);
            await Task.Run(async () =>
            {
                await dataStore.DeleteAllAsync((entity) => entity.FeedItemId == itemId);
                foreach (var entity in databaseInteractions)
                {
                    await dataStore.CreateAsync(entity);
                }
            });
            return databaseInteractions.Where((entity) => entity.FeedItemId == itemId && entity.UserId == userId);
        }
        #endregion
    }
}
