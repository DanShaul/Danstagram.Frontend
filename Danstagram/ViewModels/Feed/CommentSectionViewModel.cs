using Danstagram.Models;
using Danstagram.Models.Feed;
using Danstagram.Models.Interactions;
using Danstagram.Services;
using Danstagram.Services.Feed;
using Danstagram.Services.Interactions;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Dynamic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;
using System.Collections.ObjectModel;

namespace Danstagram.ViewModels.Feed
{
    [QueryProperty(nameof(StringItemId), "itemId")]
    class CommentSectionViewModel : ViewModelBase<CommentSectionModel>
    {
        #region Constructors
        public CommentSectionViewModel()
        {
            Title = "Comments";

            Model.MessageOptions.Caption = $"Add a comment for {((App)App.Current).UserName}";
            Model.MessageOptions.IsRequired = true;

            AddCommentCommand = new Command(async () => await OnSendCommentClicked());
            LoadCommentsCommand = new Command(async () => await OnRefresh());
        }

        #endregion

        #region Properties
        public Guid ItemId { get; set; }
        public string StringItemId { get { return ItemId.ToString(); } set { ItemId = Guid.Parse(value); } }
        public ICommand AddCommentCommand { get; }
        public ICommand LoadCommentsCommand { get; }
        #endregion

        #region Methods
        private async Task<bool> ValidateCommentService()
        {
            var isUpTask = Model.CommentServiceProvider.IsUp();
            Model.ResetErrorMessage();
            if (!(await isUpTask))
            {
                Model.SetErrorMessage("Problem connecting to interaction service");
            }
            return await isUpTask;
        }
        public async Task OnRefresh()
        {
            await LoadCommentsCollectionAsync();
        }
        public void OnAppearing()
        {
            IsBusy = true;
        }
        private async Task LoadCommentsCollectionAsync()
        {
            var validateCommentServiceTask = Task.Run(() => ValidateCommentService());
            if(await validateCommentServiceTask)
            {
                await Task.Run(async () =>
                {
                    ObservableCollection<CommentModel> newCollection = new ObservableCollection<CommentModel>();
                    var existingComments = await Model.CommentServiceProvider.GetItemInteractionsAsync(ItemId);
                    foreach (var comment in existingComments)
                    {
                        newCollection.Add(new CommentModel(comment));
                    }
                    Model.CommentList = new ObservableCollection<CommentModel>(newCollection);
                });
            }
            IsBusy = false;
        }
        private async Task OnSendCommentClicked()
        {
            var validateCommentServiceTask = Task.Run(() => ValidateCommentService());
            if (Model.MessageOptions.Validate())
            {
                Exception exception;
                if (await validateCommentServiceTask)
                {
                    await Task.Run(async () =>
                    {
                        try
                        {
                            var new_comment = new CommentModel()
                            {
                                Id = Guid.NewGuid(),
                                UserId = ((App)App.Current).UserId,
                                UserName = ((App)App.Current).UserName,
                                FeedItemId = ItemId,
                                Message = Model.Message,
                                CreatedDate = DateTimeOffset.UtcNow
                            };
                            Model.CommentList.Add(new_comment);
                            await Model.CommentServiceProvider.CreateInteractionAsync(new_comment);
                        }
                        catch (Exception except)
                        {
                            exception = except;
                            throw exception;
                        }

                    });
                }
                

            }
        }
        #endregion


    }
}
