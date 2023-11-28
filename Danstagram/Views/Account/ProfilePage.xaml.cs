using Danstagram.ViewModels.Account;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Danstagram.Views.Account
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class ProfilePage : ContentPage
	{
        #region Properties
        readonly ProfileViewModel _viewModel;
        #endregion
        #region Constructors
        public ProfilePage ()
		{
			InitializeComponent ();
			this.BindingContext = _viewModel =  new ProfileViewModel();
		}
        #endregion
        #region Methods
        protected override void OnAppearing()
        {
            base.OnAppearing();
            _viewModel.Model.UserName = ((App)App.Current).UserName;
        }
		#endregion
	}
}