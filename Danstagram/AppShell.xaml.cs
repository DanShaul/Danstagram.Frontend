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
            Console.WriteLine("Started Loading AppShell");
            InitializeComponent();
            Routing.RegisterRoute(nameof(CommentSectionPage), typeof(CommentSectionPage));
            Routing.RegisterRoute(nameof(NewItemPage), typeof(NewItemPage));
            Console.WriteLine("Finished Loading AppShell");
        }

    }
}
