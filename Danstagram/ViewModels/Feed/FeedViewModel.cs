using Danstagram.Models;
using Danstagram.Models.Feed;
using Danstagram.Models.Interactions;
using Danstagram.Services.Feed;
using Danstagram.Services.Interactions;
using Danstagram.Views;
using Danstagram.Views.Feed;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace Danstagram.ViewModels.Feed
{
    class FeedViewModel : ViewModelBase<FeedModel>
    {
        #region Constructors

        public FeedViewModel()
        {
            Title = "Feed";
            Model = new FeedModel();

            AddItemCommand = new Command(async () => await OnAddItemClicked());
            LoadItemsCommand = new Command(async () => await LoadItemCollectionAsync());
            LikeCommand = new Command(async (id) => await OnLikeClicked((Guid)id));
            CommentCommand = new Command(async (id) => await OnCommentClicked((Guid)id));
            SignOutCommand = new Command(async () => await Shell.Current.GoToAsync("//LoginPage"));
        }

        #endregion
        #region Properties

        public ICommand AddItemCommand { get;}
        public ICommand LoadItemsCommand { get; }
        public ICommand LikeCommand { get; }
        public ICommand CommentCommand { get; }
        public ICommand SignOutCommand { get; }

        #endregion
        #region Methods
        public void OnAppearing()
        {
            IsBusy = true;
        }
        private async Task OnLikeClicked(Guid id){
            var item = Model.ItemList.FirstOrDefault((existingItem)=> existingItem.Id == id) ?? throw new ArgumentException("Bad Argument");
            item.LikeCount += item.IsLiked?-1:1;
            item.IsLiked = !item.IsLiked;
            if (item.IsLiked)
            {
            await Task.Run(
                async () =>
                {
                    var likeServiceProvider = DependencyService.Get<IInteractionServiceProvider<LikeModel>>();

                    await likeServiceProvider.CreateInteractionAsync(new LikeModel
                    {
                        Id = Guid.NewGuid(),
                        UserId = ((App)App.Current).UserId,
                        FeedItemId = item.Id,
                    });
                });
            }
            else
            {
                await Task.Run(
                async () =>
                {
                    var likeServiceProvider = DependencyService.Get<IInteractionServiceProvider<LikeModel>>();
                    var existingLikes = await likeServiceProvider.GetItemUserInteractionsAsync(id, ((App)App.Current).UserId);
                    await likeServiceProvider.DeleteInteractionAsync(existingLikes.First());
                });
            }

        }
        private async Task OnCommentClicked(Guid id){
            await Shell.Current.GoToAsync($"{nameof(CommentSectionPage)}?itemId={id}");
        }
        private async Task OnAddItemClicked()
        {
            await Shell.Current.GoToAsync(nameof(NewItemPage));
        }
        private async Task LoadItemCollectionAsync()
        {
            Model.ItemList.Clear();
            var itemServiceProvider = DependencyService.Get<IItemServiceProvider<PictureItem>>();
            var likeServiceProvider = DependencyService.Get<IInteractionServiceProvider<LikeModel>>();
            
            var userId = ((App)App.Current).UserId;
            var existingItems = await itemServiceProvider.GetAllItemsAsync();
            foreach (var item in existingItems)
            {
                var likes = await likeServiceProvider.GetItemInteractionsAsync(item.Id);
                var newItem = new FeedModel.FeedItem(item);
                newItem.IsLiked = (await likeServiceProvider.GetItemUserInteractionsAsync(item.Id, ((App)App.Current).UserId)).Count != 0;
                newItem.LikeCount = likes.Count;
                Model.ItemList.Add(newItem);
            }
            IsBusy = false;
        }
        private async Task LoadItemAsync(FeedModel.FeedItem item)
        {
            var itemServiceProvider = DependencyService.Get<ItemServiceProvider>();
            var existingItem = await itemServiceProvider.GetItemAsync(item.Id);
            var indexItem = Model.ItemList.IndexOf(item);
            Model.ItemList.Insert(indexItem,new FeedModel.FeedItem(existingItem));
        }

        #endregion
    }
}
