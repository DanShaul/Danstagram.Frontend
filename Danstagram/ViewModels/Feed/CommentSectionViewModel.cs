using Danstagram.Models;
using Danstagram.Models.Feed;
using Danstagram.Models.Interactions;
using Danstagram.Services;
using Danstagram.Services.Feed;
using Danstagram.Services.Interactions;
using System;
using System.Diagnostics;
using System.Dynamic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace Danstagram.ViewModels.Feed
{
    [QueryProperty(nameof(StringItemId), "itemId")]
    class CommentSectionViewModel : ViewModelBase<CommentSectionModel>
    {
        #region Constructors
        public CommentSectionViewModel()
        {
            Title = "Comments";
            Model = new CommentSectionModel();
            Model.MessageOptions.Caption = $"Add a comment for {((App)App.Current).UserName}";
            Model.MessageOptions.IsRequired = true;

            AddCommentCommand = new Command(async () => await OnSendCommentClicked());
            LoadCommentsCommand = new Command(async () => await LoadCommentsCollectionAsync());
        }

        #endregion

        #region Properties
        public Guid ItemId { get; set; }
        public string StringItemId { get { return ItemId.ToString(); } set { ItemId = Guid.Parse(value); } }
        public ICommand AddCommentCommand { get; }
        public ICommand LoadCommentsCommand { get; }
        #endregion

        #region Methods
        public async void OnAppearing()
        {
            await Task.Yield();
            await LoadCommentsCollectionAsync();
        }
        private async Task LoadCommentsCollectionAsync()
        {
            Model.CommentList.Clear();
            var interactionServiceProvider = DependencyService.Get<IInteractionServiceProvider<CommentModel>>();
            var existingComments = await interactionServiceProvider.GetItemInteractionsAsync(ItemId);
            foreach (var comment in existingComments)
            {
                Model.CommentList.Add(new CommentModel(comment));
            }
            IsBusy = false;
        }
        private async Task OnSendCommentClicked()
        {
            if (Model.MessageOptions.Validate())
            {
                Exception exception;
                await Task.Run(async () =>
                {
                    try
                    {
                        var interactionServiceProvider = DependencyService.Get<IInteractionServiceProvider<CommentModel>>();
                        var new_comment = new CommentModel()
                        {
                            Id = Guid.NewGuid(),
                            UserId = ((App)App.Current).UserId,
                            UserName = ((App)App.Current).UserName,
                            ItemId = ItemId,
                            Message = Model.Message,
                            CreatedDate = DateTimeOffset.UtcNow
                        };
                        Model.CommentList.Add(new_comment);
                        await interactionServiceProvider.CreateInteractionAsync(new_comment);
                    }
                    catch (Exception except)
                    {
                        exception = except;
                        throw exception;
                    }

                });
                

            }
        }
        #endregion


    }
}
