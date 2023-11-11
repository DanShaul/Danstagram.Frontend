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

namespace Danstagram.Services.Feed
{
    public class IdentitiesApi
    {
        #region Constructors
        public IdentitiesApi()
        {
            var handler = new HttpClientHandler();
            handler.ClientCertificateOptions = ClientCertificateOption.Manual;
            handler.ServerCertificateCustomValidationCallback =
                (httpRequestMessage, cert, cetChain, policyErrors) =>
                {
                    return true;
                };
            client = new HttpClient(handler);
            client.BaseAddress = new Uri(url);
            client.DefaultRequestHeaders.Accept.Add(
            new MediaTypeWithQualityHeaderValue("application/json"));
        }
        #endregion

        #region Properties
        private readonly string url = "https://10.0.2.2:5003";
        private readonly HttpClient client;

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
            Console.WriteLine(jsonBody);
            var content = new StringContent(jsonBody, Encoding.UTF8, "application/json");
            var response = await client.PostAsync("/identities", content).ConfigureAwait(false);
            if((int) response.StatusCode == 404)
            {
                throw new HttpRequestException("Internal server error (Maybe the identities service is not up)");
            }
            return response;
        }

        public async Task<HttpResponseMessage> AuthenticateUserAsync(string username,string password)
        {
            var body = new
            {
                userName = username,
                password = password
            };

            var jsonBody = JsonConvert.SerializeObject(body);
            var content = new StringContent(jsonBody,Encoding.UTF8, "application/json");
            var response = await client.PostAsync("/identities/auth",content).ConfigureAwait(false);

            return response;
        }
        #endregion
    }
}
