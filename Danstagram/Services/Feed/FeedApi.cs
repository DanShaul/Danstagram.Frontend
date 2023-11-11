using Danstagram.Models;
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
            var response = await client.GetAsync("/items").ConfigureAwait(false);

            try
            {
                response.EnsureSuccessStatusCode();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return null;
            }

            var content = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
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
            Console.WriteLine(jsonBody);
            var content = new StringContent(jsonBody, Encoding.UTF8, "application/json");
            var response = await client.PostAsync("/items",content).ConfigureAwait(false);

            try
            {
                response.EnsureSuccessStatusCode();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }  
        }

        public async Task<PictureItem> GetItemAsync(Guid id)
        {
            var response = await client.GetAsync($"/items/{id}").ConfigureAwait(false);

            try
            {
                response.EnsureSuccessStatusCode();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return null;
            }

            var content = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
            return JsonConvert.DeserializeObject<PictureItem>(content);
        }
        #endregion
    }
}
