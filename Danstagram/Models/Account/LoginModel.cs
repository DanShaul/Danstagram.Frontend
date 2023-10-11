using System;
using System.Collections.Generic;
using System.Text;

namespace Danstagram.Models.Account
{
    public class LoginModel : BindableErrorBase
    {
        #region Properties
        public string UserName { get; set; }
        public string Password { get; set; }

        private TextEditOptions<string> userNameOptions = new TextEditOptions<string>();
        public TextEditOptions<string> UserNameOptions { get { return userNameOptions; } set { SetProperty(ref userNameOptions, value); } }

        private TextEditOptions<string> passwordOptions = new TextEditOptions<string>();
        public TextEditOptions<string> PasswordOptions { get { return passwordOptions; } set { SetProperty(ref passwordOptions, value); } }

        #endregion
    }
}
