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

        private readonly IDataStore<PictureItem> dataStore;

        #endregion


        #region Constructors

        public ItemServiceProvider()
        {
            dataStore = DependencyService.Get<IDataStore<PictureItem>>();
        }

        #endregion


        #region Methods

        public async Task CreateItemAsync(PictureItem item)
        {
            await dataStore.CreateAsync(item);

        }
        public async Task<IReadOnlyCollection<PictureItem>> GetAllItemsAsync()
        {
            return await dataStore.GetAllAsync();
        }
        public async Task<PictureItem> GetItemAsync(Guid id)
        {
            return await dataStore.GetAsync(id);
        }

        #endregion
    }
}
