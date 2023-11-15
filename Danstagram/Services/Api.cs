using Danstagram.Models.Account;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using Xamarin.Forms;

namespace Danstagram.Services
{
    public class Api
    {
        #region Constructors
        public Api()
        {
            Client = DependencyService.Get<HttpClient>();
        }
        #endregion
        #region Properties
        public HttpClient Client { get; set; }
        #endregion
    }
}
