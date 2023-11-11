using Danstagram.Models;
using Danstagram.Models.Interactions;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Runtime.InteropServices;
using System.Text;
using System.Text.Json.Nodes;
using System.Threading.Tasks;
using System.Web;

namespace Danstagram.Services.Feed
{
    public class LikeApi
    {
        #region Constructors
        public LikeApi()
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

            client = new HttpClient(handler)
            {
                BaseAddress = new Uri(url)
            };
            client.DefaultRequestHeaders.Accept.Add(
            new MediaTypeWithQualityHeaderValue("application/json"));

        }
        #endregion

        #region Properties
        private readonly string url = "https://10.0.2.2:5005/interactions/likes";
        private readonly HttpClient client;
        #endregion

        #region Methods
        private class CreateLikeDto
        {
            public Guid UserId { get; set; }
            public Guid FeedItemId { get; set; }
        }
        public async Task<IReadOnlyCollection<LikeModel>> GetItemLikesAsync(Guid feedItemId)
        {
            Console.WriteLine("----Getting Item Likes Async----");

            var query = HttpUtility.ParseQueryString(string.Empty);
            query["feedItemId"] = feedItemId.ToString();
            string itemIdQueryString = query.ToString();

            var response = await client.GetAsync($"/feeditems/:id?{itemIdQueryString}").ConfigureAwait(false);

            try
            {
                response.EnsureSuccessStatusCode();
                var content = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
                var interactionsList = JsonConvert.DeserializeObject<IReadOnlyCollection<LikeModel>>(content);
                return interactionsList;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }

            return null;
        }

        public async Task CreateLikeAsync(LikeModel like)
        {
            CreateLikeDto createLike = new CreateLikeDto
            {
                UserId = like.UserId,
                FeedItemId = like.FeedItemId
            };
            var jsonBody = JsonConvert.SerializeObject(createLike);
            var content = new StringContent(jsonBody, Encoding.UTF8, "application/json");

            var response = await client.PostAsync($"", content).ConfigureAwait(false);
            Console.WriteLine($"----Path is: ----");
            Console.WriteLine($"----JsonBody is: {jsonBody}");
            try
            {
                response.EnsureSuccessStatusCode();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }

        public async Task DeleteInteractionAsync(Guid id)
        {
            var query = HttpUtility.ParseQueryString(string.Empty);
            query["id"] = id.ToString();
            string idQueryString = query.ToString();

            var response = await client.DeleteAsync($"/:id?{idQueryString}").ConfigureAwait(false);
            try
            {
                response.EnsureSuccessStatusCode();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }
        #endregion
    }
}
