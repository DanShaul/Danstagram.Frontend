using Danstagram.Models.Account;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using Xamarin.Forms;
using System.Net.Http.Headers;
namespace Danstagram.Services
{
    public class Api
    {
        #region Constructors
        public Api()
        {
            var handler = new HttpClientHandler
            {
                ClientCertificateOptions = ClientCertificateOption.Manual,
                ServerCertificateCustomValidationCallback =
            (httpRequestMessage, cert, cetChain, policyErrors) =>
            {
                return true;
            }
            };
            Client = new HttpClient(handler, false);
            Client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }
        #endregion
        #region Properties
        public HttpClient Client { get; set; }
        #endregion
    }
}
