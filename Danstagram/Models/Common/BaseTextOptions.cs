using System;
using System.Collections.Generic;
using System.Text;

namespace Danstagram.Models.Common
{
    public class BaseTextOptions : BindableBase
    {
        private string text = "";
        public string Text { get { return text; } set { SetProperty(ref text, value); } }

    }
}
