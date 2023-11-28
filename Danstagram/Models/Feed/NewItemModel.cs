using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace Danstagram.Models.Feed
{
    public class NewItemModel : BindableBase
    {
        #region Properties
        public string Caption { get; set; }

        private byte[] image;
        public byte[] Image { get { return image; } set { SetProperty(ref image, value); } }

        private TextEditOptions<string> captionOptions = new TextEditOptions<string>();
        public TextEditOptions<string> CaptionOptions { get {  return captionOptions; } set { SetProperty(ref captionOptions, value); } }

        private EditBaseOptions<byte[]> imageOptions = new EditBaseOptions<byte[]>();
        public EditBaseOptions<byte[]> ImageOptions { get { return imageOptions; } set { SetProperty(ref imageOptions, value); } }

        #endregion
    }
}
