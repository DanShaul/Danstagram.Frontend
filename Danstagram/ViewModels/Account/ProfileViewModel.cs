using Danstagram.Models.Account;
using Danstagram.Views.Account;
using Danstagram.Views.Feed;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace Danstagram.ViewModels.Account
{
    class ProfileViewModel : ViewModelBase<ProfileModel>
    {
        #region Constructors
        public ProfileViewModel() {
            Title = "Welcome";
            Model = new ProfileModel
            {
                UserName = ((App)App.Current).UserName
            };

            SignOutCommand = new Command(async () => await OnSignOutClicked());
            AddPictureCommand = new Command(async () => await OnAddPictureClicked());
        }
        #endregion

        #region Properties

        public ICommand SignOutCommand { get; }
        public ICommand AddPictureCommand { get; }

        #endregion

        #region Methods

        private async Task OnSignOutClicked()
        {
            await Shell.Current.GoToAsync($"//{nameof(LoginPage)}");
        }
        private async Task OnAddPictureClicked()
        {
            await Shell.Current.GoToAsync(nameof(NewItemPage));
        }
        #endregion



    }
}
