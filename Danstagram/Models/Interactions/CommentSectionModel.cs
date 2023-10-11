using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Runtime.CompilerServices;
using System.Text;
using static Danstagram.Models.Feed.FeedModel;

namespace Danstagram.Models.Interactions
{
    public class CommentSectionModel : BindableBase
    {

        #region Properties

        public string Message { get; set; }

        private TextEditOptions<string> messageOptions = new TextEditOptions<string>();
        public TextEditOptions<string> MessageOptions { get { return messageOptions; } set { SetProperty(ref messageOptions, value); } }


        private ObservableCollection<CommentModel> commentList = new ObservableCollection<CommentModel>();
        public ObservableCollection<CommentModel> CommentList { get { return commentList; } set { SetProperty(ref commentList, value); } }

        #endregion
    }
}
