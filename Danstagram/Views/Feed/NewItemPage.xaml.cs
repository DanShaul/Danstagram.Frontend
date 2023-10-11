using Danstagram.Models;
using Danstagram.ViewModels;
using Danstagram.ViewModels.Feed;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Danstagram.Views.Feed
{
    public partial class NewItemPage : ContentPage
    {
        public PictureItem Item { get; set; }

        public NewItemPage()
        {
            InitializeComponent();
            BindingContext = new NewItemViewModel();
        }
    }
}