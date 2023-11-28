using Danstagram.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Danstagram.Services.Feed
{
    class ItemServiceProvider : IItemServiceProvider<PictureItem>
    {
        #region Properties
        private readonly FeedApi feedApi;
        private readonly IDataStore<PictureItem> dataStore;

        #endregion


        #region Constructors

        public ItemServiceProvider()
        {
            feedApi = new FeedApi();
            dataStore = DependencyService.Get<IDataStore<PictureItem>>();
        }

        #endregion


        #region Methods
        public async Task<bool> IsUp()
        {
            return await feedApi.IsUp();
        }

        public async Task CreateItemAsync(PictureItem item)
        {
            Console.WriteLine("-----Creating Item-----");
            var createItemInDatabaseTask = feedApi.CreateItemAsync(item);
            var createItemLocallyTask = dataStore.CreateAsync(item);
            await Task.WhenAll(createItemInDatabaseTask,createItemLocallyTask);

        }
        public async Task<IReadOnlyCollection<PictureItem>> GetAllItemsAsync()
        {
            Console.WriteLine("-----Getting Items-----");
            var databasePictures = await feedApi.GetAllItemsAsync();
            await dataStore.LoadDataFromBackend(databasePictures);
            return await dataStore.GetAllAsync();
        }
        public async Task<PictureItem> GetItemAsync(Guid id)
        {
            Console.WriteLine("-----Getting Single Item-----");
            return await dataStore.GetAsync(id);
        }

        #endregion
    }
}
