using Danstagram.Models;
using Danstagram.Services.Common;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json.Nodes;
using System.Threading.Tasks;

namespace Danstagram.Services.Feed
{
    public class FeedApi
    {
        #region Constructors
        public FeedApi() {
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
        private readonly string url = "https://10.0.2.2:5001";
        private readonly HttpClient client;

        #endregion

        #region Methods
        public async Task<IReadOnlyCollection<PictureItem>> GetAllItemsAsync()
        {
            HttpResponseMessage response;
            try {
                response = await client.GetAsync("/items").WrapTimeout();
                response.EnsureSuccessStatusCode();
            }
            catch (TaskCanceledException)
            {
                throw new HttpRequestException("Internal server error(Maybe the feed service is not up)");
            }

            var content = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<IReadOnlyCollection<PictureItem>>(content);
        }

        public async Task CreateItemAsync(PictureItem item)
        {
            var body = new
            {
                userId = item.UserId,
                image = item.Image,
                caption = item.Caption
            };

            var jsonBody = JsonConvert.SerializeObject(body);
            var content = new StringContent(jsonBody, Encoding.UTF8, "application/json");

            HttpResponseMessage response;
            try
            {
                response = await client.PostAsync("/items", content);
                response.EnsureSuccessStatusCode();
            }
            catch (TaskCanceledException)
            {
                throw new HttpRequestException("Internal server error(Maybe the feed service is not up)");
            }
        }

        public async Task<PictureItem> GetItemAsync(Guid id)
        {
            HttpResponseMessage response;
            try
            {
                response = await client.GetAsync($"/items/{id}");
                response.EnsureSuccessStatusCode();

            }
            catch (TaskCanceledException)
            {
                throw new HttpRequestException("Internal server error(Maybe the feed service is not up)");
            }

            var content = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<PictureItem>(content);
        }
        #endregion
    }
}
