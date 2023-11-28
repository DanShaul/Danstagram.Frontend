using Danstagram.Services.Interactions;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Runtime.CompilerServices;
using System.Text;
using Xamarin.Forms;

namespace Danstagram.Models.Interactions
{
    public class CommentSectionModel : BindableErrorBase
    {

        #region Properties

        public IInteractionServiceProvider<CommentModel> CommentServiceProvider = DependencyService.Get<IInteractionServiceProvider<CommentModel>>();
        public string Message { get; set; }
        private TextEditOptions<string> messageOptions = new TextEditOptions<string>();
        public TextEditOptions<string> MessageOptions { get { return messageOptions; } set { SetProperty(ref messageOptions, value); } }


        private ObservableCollection<CommentModel> commentList = new ObservableCollection<CommentModel>();
        public ObservableCollection<CommentModel> CommentList { get { return commentList; } set { SetProperty(ref commentList, value); } }

        #endregion
    }
}
