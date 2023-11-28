using Danstagram.Models;
using Danstagram.Models.Account;
using Danstagram.Models.Feed;
using Danstagram.Models.Interactions;
using Danstagram.Services.Feed;
using Danstagram.Services.Interactions;
using Danstagram.Views;
using Danstagram.Views.Account;
using Danstagram.Views.Feed;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Essentials;
using Xamarin.Forms;
using System.Collections.ObjectModel;

namespace Danstagram.ViewModels.Feed
{
    class FeedViewModel : ViewModelBase<FeedModel>
    {
        #region Constructors

        public FeedViewModel()
        {
            Title = "Feed";

            AddItemCommand = new Command(async () => await OnAddItemClicked());
            LoadItemsCommand = new Command(() => OnRefresh());
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
        private async Task<bool> ValidateFeedService()
        {
            var isUpTask = Model.ItemServiceProvider.IsUp();
            Model.ResetErrorMessage();
            if(!(await isUpTask))
            {
                Model.SetErrorMessage("Problem connecting to feed service");
            }
            return await isUpTask;
        }
        private async Task<bool> ValidateLikeService()
        {
            var isUpTask = Model.LikeServiceProvider.IsUp();
            return await isUpTask;
        }
        public async Task OnRefresh()
        {
            await LoadItemCollectionAsync();
        }
        public void OnAppearing()
        {
            IsBusy = true;
        }
        private async Task OnLikeClicked(Guid id){
            var isLikeServiceUpTask = Task.Run(() => ValidateLikeService());
            var item = Model.ItemList.FirstOrDefault((existingItem)=> existingItem.Id == id) ?? throw new ArgumentException("Bad Argument");
            item.LikeCount += item.IsLiked?-1:1;
            item.IsLiked = !item.IsLiked;

            if (await isLikeServiceUpTask)
            {
                if (item.IsLiked)
                {
                    await Task.Run(
                        async () =>
                        {
                            await Model.LikeServiceProvider.CreateInteractionAsync(new LikeModel
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
                        var existingLikes = await Model.LikeServiceProvider.GetItemUserInteractionsAsync(id, ((App)App.Current).UserId);
                        await Model.LikeServiceProvider.DeleteInteractionAsync(existingLikes.FirstOrDefault());
                    });
                }
            }
            else
            {
                await App.Current.MainPage.DisplayAlert("Alert", "Couldnt send like, problem reaching interaction service", "OK");
            }

        }
        private async Task OnCommentClicked(Guid id){
            await Shell.Current.GoToAsync($"{nameof(CommentSectionPage)}?itemId={id}");
        }
        private async Task OnAddItemClicked()
        {
            await Shell.Current.GoToAsync(nameof(NewItemPage));
        }
        private async Task<IReadOnlyCollection<PictureItem>> GetAllItemsFromDatabase()
        {
            if (await ValidateFeedService())
            {
                var itemServiceProvider = DependencyService.Get<IItemServiceProvider<PictureItem>>();
                return await itemServiceProvider.GetAllItemsAsync();
            }
            return null;
        }


        private async Task LoadItemCollectionAsync()
        {
            var validateLikeServiceTask = Task.Run(() => ValidateLikeService());
            var existingItems = await Task.Run(() => GetAllItemsFromDatabase());
            if (existingItems != null)
            {
                await Task.Run(async () =>
                {
                    
                    ObservableCollection<FeedModel.FeedItem> newItemList = new ObservableCollection<FeedModel.FeedItem>();
                    var likeServiceProvider = DependencyService.Get<IInteractionServiceProvider<LikeModel>>();
                    var isLikeServiceUp = await validateLikeServiceTask;
                    foreach (var item in existingItems)
                    {
                        var likes = Enumerable.Empty<LikeModel>();
                        if (isLikeServiceUp)
                        {
                            likes = await likeServiceProvider.GetItemInteractionsAsync(item.Id);
                        }

                        var newItem = new FeedModel.FeedItem(item)
                        {
                            IsLiked = likes.SingleOrDefault((like) => like.FeedItemId == item.Id && like.UserId == ((App)App.Current).UserId) != null,
                            LikeCount = likes.Count()
                        };
                        newItemList.Add(newItem);
                    }
                    Model.ItemList = new ObservableCollection<FeedModel.FeedItem>(newItemList);
                });
            }

            IsBusy = false;
        }
        #endregion
    }
}
