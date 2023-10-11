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
            Console.WriteLine("Started Loading App");
            InitializeComponent();
            MainPage = new AppShell();
            Shell.Current.GoToAsync("//LoginPage");
            Task.Run(() =>
            {
                var webClient = new WebClient();
                var pictureId = Guid.NewGuid();
                var mockUserCollection = new Collection<User>();
                var tempUserId = Guid.NewGuid();
                var tempUserName = "randuser1";
                mockUserCollection.Add(new User
                {
                    Id = tempUserId,
                    Username = tempUserName,
                    Password = "randuser1"

                });
                mockUserCollection.Add(new User
                {
                    Id = Guid.NewGuid(),
                    Username = "dan",
                    Password = "dan"

                });
                var mockLikeCollection = new Collection<LikeModel>();
                mockLikeCollection.Add(new LikeModel
                {
                    Id = Guid.NewGuid(),
                    UserId = tempUserId,
                    ItemId = pictureId
                });
                var mockCommentCollection = new Collection<CommentModel>();
                mockCommentCollection.Add(new CommentModel
                {
                    Id = Guid.NewGuid(),
                    UserId = tempUserId,
                    UserName = tempUserName,
                    ItemId = pictureId,
                    Message = "My first CommentModel",
                    CreatedDate = DateTimeOffset.UtcNow

                });
                var mockCollection = new Collection<PictureItem>
                {
                    new PictureItem
                    {
                        Id = pictureId,
                        UserId = tempUserId,
                        UserName = tempUserName,
                        Image = webClient.DownloadData("https://imgv3.fotor.com/images/cover-photo-image/a-beautiful-girl-with-gray-hair-and-lucxy-neckless-generated-by-Fotor-AI.jpg"),
                        Caption = "my lemon1",
                        LikeCount = 0,
                        CreatedDate = DateTimeOffset.UtcNow
                    }
                };
                webClient.Dispose();
               
                DependencyService.RegisterSingleton<ICollection<PictureItem>>(mockCollection);
                DependencyService.RegisterSingleton<ICollection<LikeModel>>(mockLikeCollection);
                DependencyService.RegisterSingleton<ICollection<CommentModel>>(mockCommentCollection);
                DependencyService.RegisterSingleton<ICollection<User>>(mockUserCollection);
                DependencyService.RegisterSingleton<IDataStore<PictureItem>>(new CollectionDataStore<PictureItem>());
                DependencyService.RegisterSingleton<IDataStore<LikeModel>>(new CollectionDataStore<LikeModel>());
                DependencyService.RegisterSingleton<IDataStore<CommentModel>>(new CollectionDataStore<CommentModel>());
                DependencyService.RegisterSingleton<IDataStore<User>>(new CollectionDataStore<User>());
                DependencyService.RegisterSingleton<ILoginServiceProvider>(new LoginServiceProvider());
                DependencyService.RegisterSingleton<IItemServiceProvider<PictureItem>>(new ItemServiceProvider());
                DependencyService.RegisterSingleton<IInteractionServiceProvider<LikeModel>>(new InteractionServiceProvider<LikeModel>());
                DependencyService.RegisterSingleton<IInteractionServiceProvider<CommentModel>>(new InteractionServiceProvider<CommentModel>());
            });
            Console.WriteLine("Finished Loading App");
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
