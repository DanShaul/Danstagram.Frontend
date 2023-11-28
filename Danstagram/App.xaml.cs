using Danstagram.Models;
using Danstagram.Models.Account;
using Danstagram.Models.Interactions;
using Danstagram.Services;
using Danstagram.Services.Account;
using Danstagram.Services.Feed;
using Danstagram.Services.Interactions;
using Danstagram.Views;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Net;
using System.Net.Http;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using static System.Net.WebRequestMethods;

namespace Danstagram
{
    public partial class App : Application
    {
        public Guid UserId { get; set; }
        public string UserName { get; set; }
        public App()
        {
            UserId = Guid.Empty;
            UserName = "";

            InitializeComponent();
            MainPage = new AppShell();

            Shell.Current.GoToAsync("//LoginPage");

            Task.Run(() =>
            {
                DependencyService.RegisterSingleton<IDataStore<PictureItem>>(new CollectionDataStore<PictureItem>());
                DependencyService.RegisterSingleton<IDataStore<LikeModel>>(new CollectionDataStore<LikeModel>());
                DependencyService.RegisterSingleton<IDataStore<CommentModel>>(new CollectionDataStore<CommentModel>());

                DependencyService.RegisterSingleton<ILoginServiceProvider>(new LoginServiceProvider());
                DependencyService.RegisterSingleton<IItemServiceProvider<PictureItem>>(new ItemServiceProvider());
                DependencyService.RegisterSingleton<IInteractionServiceProvider<LikeModel>>(new InteractionServiceProvider<LikeModel>());
                DependencyService.RegisterSingleton<IInteractionServiceProvider<CommentModel>>(new InteractionServiceProvider<CommentModel>());
            });
        }
    }
}
