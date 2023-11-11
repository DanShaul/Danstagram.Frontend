using System;
using System.Collections.Generic;
using System.Text;

namespace Danstagram.Models.Interactions
{
    public class LikeModel: BindableBase,IInteraction
    {
        #region Properties

        private Guid id;
        public Guid Id { get { return id; } set { SetProperty(ref id, value); } }

        private Guid feedItemId;
        public Guid FeedItemId { get { return feedItemId; } set { SetProperty(ref feedItemId, value); } }

        private Guid userId;
        public Guid UserId { get { return userId; } set { SetProperty(ref userId, value); } }

        #endregion

    }
}
