using Danstagram.ViewModels.Feed;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Danstagram.Views.Feed
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class FeedPage : ContentPage
    {
        readonly FeedViewModel _viewModel;

        public FeedPage()
        {
            InitializeComponent();

            BindingContext = _viewModel = new FeedViewModel();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            _viewModel.OnAppearing();
        }
    }
}