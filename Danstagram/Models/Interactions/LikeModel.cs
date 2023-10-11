using System;
using System.Collections.Generic;
using System.Text;

namespace Danstagram.Models.Interactions
{
    class LikeModel: BindableBase,IInteraction
    {
        #region Properties

        private Guid id;
        public Guid Id { get { return id; } set { SetProperty(ref id, value); } }

        private Guid itemId;
        public Guid ItemId { get { return itemId; } set { SetProperty(ref itemId, value); } }

        private Guid userId;
        public Guid UserId { get { return userId; } set { SetProperty(ref userId, value); } }

        private DateTimeOffset createdDate;
        public DateTimeOffset CreatedDate { get { return createdDate; } set { SetProperty(ref createdDate, value); } }
        #endregion

    }
}
