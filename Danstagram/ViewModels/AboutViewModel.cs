using Danstagram.Views;
using System;
using System.Windows.Input;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace Danstagram.ViewModels
{
    public class AboutViewModel : BaseViewModel
    {
        public AboutViewModel()
        {
            Title = "About";
            SignOutCommand = new Command(async () => await Shell.Current.GoToAsync("//LoginPage"));
        }

        public ICommand SignOutCommand { get; }
    }
}