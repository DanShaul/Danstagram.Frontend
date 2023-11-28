using Danstagram.Models.Common;
using Danstagram.Models.Interactions;
using Danstagram.Services.Feed;
using Danstagram.Services.Interactions;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Text;
using Xamarin.Forms;

namespace Danstagram.Models.Feed
{
    public class FeedModel : BindableErrorBase
    {
        #region Classes

        public class FeedItem : BasePictureItem
        {
            #region Properties
            public string CommentIcon { get; } = "icon_comment";

            private readonly BaseTextOptions likeCountText = new BaseTextOptions();
            public BaseTextOptions LikeCountText { get { likeCountText.Text = $"{LikeCount}"; return likeCountText; } }

            private string likeIcon;
            public string LikeIcon { get { return likeIcon; } set { SetProperty(ref likeIcon, value); } }

            private bool isLiked;
            public bool IsLiked
            {
                get { return isLiked; }
                set
                {
                    SetProperty(ref isLiked, value);
                    LikeIcon = IsLiked ? "icon_like_red" : "icon_like";
                }
            }
            
            #endregion

            #region Constructors

            public FeedItem(BasePictureItem item)
            {
                Id = item.Id;
                UserId = item.UserId;
                UserName = item.UserName;
                Image = item.Image;
                Caption = item.Caption;
                LikeCount = item.LikeCount;
                CreatedDate = item.CreatedDate;
                IsLiked = false;
            }



            #endregion
        }

        #endregion

        #region Properties
        public IInteractionServiceProvider<LikeModel> LikeServiceProvider = DependencyService.Get<IInteractionServiceProvider<LikeModel>>();
        public IItemServiceProvider<PictureItem> ItemServiceProvider = DependencyService.Get<IItemServiceProvider<PictureItem>>();
        private ObservableCollection<FeedItem> itemList = new ObservableCollection<FeedItem>();
        public ObservableCollection<FeedItem> ItemList { get{ return itemList; } set { SetProperty(ref itemList, value); } }


        #endregion
    }
}
