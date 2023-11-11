using System;
using System.Collections.Generic;
using System.Text;

namespace Danstagram.Models.Interactions
{
    public class CommentModel : BindableBase,IInteraction
    {
        #region Constructor
        public CommentModel() { }
        public CommentModel(CommentModel comment)
        {
            this.Id = comment.Id;
            this.Message = comment.Message;
            this.UserId = comment.UserId;
            this.FeedItemId = comment.FeedItemId;
            this.UserName = comment.UserName;
            this.CreatedDate = comment.CreatedDate;
        }
        #endregion
        #region Properties

        private Guid id;
        public Guid Id { get { return id; } set { SetProperty(ref id, value); } }

        private Guid feedItemId;
        public Guid FeedItemId { get { return feedItemId; } set { SetProperty(ref feedItemId, value); } }

        private Guid userId;
        public Guid UserId { get { return userId; } set { SetProperty(ref userId, value); } }

        private string message;
        public string Message { get { return message; } set { SetProperty(ref message, value); } }

        private DateTimeOffset createdDate;
        public DateTimeOffset CreatedDate { get { return createdDate; } set { SetProperty(ref createdDate, value); } }

        private string userName;
        public string UserName { get { return userName; } set { SetProperty(ref userName, value); } }
        #endregion

    }
}
