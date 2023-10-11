using Danstagram.ViewModels.Account;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Danstagram.Views.Account
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class LoginPage : ContentPage
    {
        public LoginPage()
        {
            InitializeComponent();
            this.BindingContext = new LoginViewModel();
        }
    }
}