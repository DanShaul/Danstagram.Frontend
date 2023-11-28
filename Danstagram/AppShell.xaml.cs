using Danstagram.ViewModels;
using Danstagram.Views;
using Danstagram.Views.Feed;
using System;
using System.Collections.Generic;
using Xamarin.Forms;

namespace Danstagram
{
    public partial class AppShell : Xamarin.Forms.Shell
    {
        public AppShell()
        {
            InitializeComponent();
            Routing.RegisterRoute(nameof(CommentSectionPage), typeof(CommentSectionPage));
            Routing.RegisterRoute(nameof(NewItemPage), typeof(NewItemPage));
        }

    }
}
