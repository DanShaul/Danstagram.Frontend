using System;
using System.ComponentModel;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using Danstagram.Services;
using Xamarin.Forms;

namespace Danstagram.Models.Feed
{
    public class BasePictureItem : BindableBase,IEntity
    {
        #region Properties

        private Guid id;
        public Guid Id { get { return id; } set { SetProperty(ref id, value); } }

        private string userName;
        public string UserName { get { return userName; } set { SetProperty(ref userName, value); } }

        private Guid userId;
        public Guid UserId { get { return userId; } set { SetProperty(ref userId, value); } }

        private byte[] image;
        public byte[] Image { get { return image; } set { SetProperty(ref image, value); } }

        private string caption;
        public string Caption { get { return caption; } set { SetProperty(ref caption, value); } }

        private int likeCount;
        public int LikeCount { get { return likeCount; } set { SetProperty(ref likeCount, value); } }

        private DateTimeOffset createdDate;
        public DateTimeOffset CreatedDate { get { return createdDate; } set { SetProperty(ref createdDate, value); } }
        #endregion

    }
}