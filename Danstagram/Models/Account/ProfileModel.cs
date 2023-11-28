using System;
using System.Collections.Generic;
using System.Text;

namespace Danstagram.Models.Account
{
    class ProfileModel : BindableBase
    {
        private string userName;
        public string UserName { get { return userName; } set { SetProperty(ref userName, value); } }
    }
}
