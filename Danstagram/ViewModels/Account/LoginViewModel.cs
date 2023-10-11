using Danstagram.Models.Account;
using Danstagram.Services.Account;
using Danstagram.Views;
using Danstagram.Views.Account;
using Danstagram.Views.Feed;
using System;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace Danstagram.ViewModels.Account
{
    class LoginViewModel : ViewModelBase<LoginModel>
    {
        #region Consturctors
        public LoginViewModel()
        {
            Model.UserNameOptions.Caption = "Username";
            Model.UserNameOptions.IsRequired = true;

            Model.PasswordOptions.Caption = "Password";
            Model.PasswordOptions.IsRequired = true;

            LoginCommand = new Command(async () => await OnLoginClicked());
            SignUpCommand = new Command(async () => await OnSignUpClicked());
        }
        #endregion

        #region Properties
        public ICommand LoginCommand { get; }
        public ICommand SignUpCommand { get; }

        #endregion

        #region Methods
        public bool ValidateModelProperties()
        {
            bool usernameValid = Model.UserNameOptions.Validate(Model.UserName);
            bool passwordValid = Model.PasswordOptions.Validate(Model.Password);

            return usernameValid && passwordValid;
        }
        private async Task ProcceedToFrontPage()
        {
            await Shell.Current.GoToAsync($"//{nameof(ProfilePage)}");
        }
        private async Task OnLoginClicked()
        {
            Model.ResetErrorMessage();
            if (ValidateModelProperties())
            {
                IsBusy = true;
                await Task.Run(async () =>
                {
                    Exception loginException = null;
                    try
                    {

                        var loginServiceProvider = DependencyService.Get<ILoginServiceProvider>();
                        var userId = await loginServiceProvider.AuthenticateUser(Model.UserName, Model.Password);
                        if (userId == Guid.Empty)
                        {
                            throw new UnauthorizedAccessException("Invalid User Name or Password");
                        }

                        ((App)Application.Current).UserId = userId;
                        ((App)Application.Current).UserName = Model.UserName;

                    }
                    catch (Exception exception)
                    {
                        //Implement Backend Response codes handling
                        loginException = exception;
                    }
                    if (loginException == null)
                    {
                        await ProcceedToFrontPage();
                    }
                    else
                    {
                        Model.SetErrorMessage(loginException.Message);
                    }
                    IsBusy = false;

                });
            }
;
        }
        private async Task OnSignUpClicked()
        {
            Model.ResetErrorMessage();
            if (ValidateModelProperties())
            {
                IsBusy = true;
                await Task.Run(async () =>
                {
                    Exception loginException = null;
                    try
                    {

                        var loginServiceProvider = DependencyService.Get<ILoginServiceProvider>();
                        var userId = await loginServiceProvider.CreateUser(Model.UserName, Model.Password);
                        if (userId == Guid.Empty)
                        {
                            throw new UnauthorizedAccessException("A User with that User Name already exists");
                        }

                        ((App)Application.Current).UserId = userId;
                        ((App)Application.Current).UserName = Model.UserName;

                    }
                    catch (Exception exception)
                    {
                        //Implement Backend Response codes handling
                        loginException = exception;
                    }
                    if (loginException == null)
                    {
                        await ProcceedToFrontPage();
                    }
                    else
                    {
                        Model.SetErrorMessage(loginException.Message);
                    }
                    IsBusy = false;

                });
            }
;
        }
        #endregion

    }
}
