using Danstagram.Models;
using Danstagram.Models.Feed;
using Danstagram.Services;
using Danstagram.Services.Feed;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace Danstagram.ViewModels.Feed
{
    class NewItemViewModel : ViewModelBase<NewItemModel>
    {
        #region Constructors

        public NewItemViewModel() {
            Title = "Post Your Picture";
            Model = new NewItemModel();
            Model.CaptionOptions.IsRequired = true;
            Model.CaptionOptions.Caption = "Caption";
            Model.ImageOptions.IsRequired = true;
            Model.ImageOptions.Caption = "Image";
            ChoosePictureCommand = new Command(async () => await OnChoosePictureClicked());
            PostCommand = new Command(async () => await OnPostClicked());
            CancelCommand = new Command(async () => await OnCancelClicked());
        }

        #endregion

        #region Properties
        public ICommand PostCommand { get; }
        public ICommand ChoosePictureCommand { get; }
        public ICommand CancelCommand { get; }

        #endregion

        #region Methods
        private bool Validate()
        {
            bool isValid = true;
            isValid = Model.ImageOptions.Validate(Model.Image) && isValid;
            isValid = Model.CaptionOptions.Validate(Model.Caption) && isValid;
            return isValid;

        }
        private async Task OnPostClicked()
        {
            if (Validate())
            {
                await Task.Run(async () =>
                {
                    var itemServiceProvider = DependencyService.Get<IItemServiceProvider<PictureItem>>();

                    await itemServiceProvider.CreateItemAsync(new PictureItem()
                    {
                        Id = Guid.NewGuid(),
                        UserId = ((App)App.Current).UserId,
                        UserName = ((App)App.Current).UserName,
                        Image = Model.Image,
                        Caption = Model.Caption,
                        LikeCount = 0,
                        CreatedDate = DateTimeOffset.UtcNow
                    });

                    await Shell.Current.GoToAsync("..");
                });
                
            }
        }
        private async Task OnChoosePictureClicked()
        {
            Stream stream = await DependencyService.Get<IPhotoPickerService>().GetImageStreamAsync();

            if (stream != null)
            {
                MemoryStream ms = new MemoryStream();
                stream.CopyTo(ms);
                Model.Image = ms.ToArray();
                ms.Dispose();
            }
        }
        private async Task OnCancelClicked()
        {
            await Shell.Current.GoToAsync("..");
        }

        #endregion


    }
}
