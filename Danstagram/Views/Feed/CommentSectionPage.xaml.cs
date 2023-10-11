using Danstagram.ViewModels;
using Danstagram.ViewModels.Feed;
using System;
using System.ComponentModel;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Danstagram.Views.Feed
{
    
    public partial class CommentSectionPage : ContentPage
    {
        readonly CommentSectionViewModel _viewModel;

        public CommentSectionPage()
        {
            
            InitializeComponent();
            BindingContext = _viewModel = new CommentSectionViewModel();
        }
        protected override void OnAppearing()
        {
            base.OnAppearing();
            
            _viewModel.OnAppearing();
        }
    }
}