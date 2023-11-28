using Danstagram.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http.Headers;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Danstagram.Models.Account;
using System.Net;
using Xamarin.Forms;
using System.Threading;
using Danstagram.Services.Common;

namespace Danstagram.Services.Feed
{
    public class IdentitiesApi : Api
    {
        #region Constructors
        public IdentitiesApi()
        {
            Client.BaseAddress = new Uri(url);
            Client.DefaultRequestHeaders.Accept.Add(
            new MediaTypeWithQualityHeaderValue("application/json"));
        }
        #endregion

        #region Properties
        private readonly string url = "https://10.0.2.2:5003";

        #endregion

        #region Methods
        public async Task<HttpResponseMessage> CreateUserAsync(User user)
        {
            var userDto = new
            {
                username = user.Username,
                password = user.Password
            };

            var jsonBody = JsonConvert.SerializeObject(userDto);
            var content = new StringContent(jsonBody, Encoding.UTF8, "application/json");
            try
            {
                return await Client.PostAsync("/identities", content).WrapTimeout();
            }
            catch (TaskCanceledException)
            {
                throw new HttpRequestException("Internal server error (Maybe the identities service is not up)");
            }
        }

        public async Task<HttpResponseMessage> AuthenticateUserAsync(string username,string password)
        {
            var body = new
            {
                userName = username,
                password = password
            };
            var jsonBody = JsonConvert.SerializeObject(body);
            var content = new StringContent(jsonBody,Encoding .UTF8, "application/json");
            try
            {
                return await Client.PostAsync("/identities/auth", content).WrapTimeout();
            }
            catch (TaskCanceledException)
            {
                throw new HttpRequestException("Internal server error (Maybe the identities service is not up)");
            }
        }
        #endregion
    }
}
