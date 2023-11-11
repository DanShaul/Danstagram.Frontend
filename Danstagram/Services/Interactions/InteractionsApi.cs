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
    public class InteractionsApi<T> where T : IInteraction
    {
        #region Constructors
        public InteractionsApi()
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

            var runtimeType = typeof(T).ToString().Split('.').Last();
            interactionType = runtimeType.ToLower().Substring(0, runtimeType.Length - 5) + "s";
        }
        #endregion

        #region Properties
        private readonly string url = "https://10.0.2.2:5005";
        private readonly HttpClient client;
        private readonly string interactionType;

        #endregion

        #region Methods
        private class CreateInteractionDto
        {
            public Guid UserId { get; set; }
            public Guid FeedItemId { get; set; }
        }
        private class CreateLikeDto : CreateInteractionDto { public Guid Id { get; set; } }
        private class CreateCommentDto : CreateInteractionDto{ public string Message { get; set; }}
        public async Task<IReadOnlyCollection<T>> GetItemInteractionsAsync(Guid itemId)
        {
            Console.WriteLine("----Getting Item Interactions Async----");

            var query = HttpUtility.ParseQueryString(string.Empty);
            query["feedItemId"] = itemId.ToString();
            string itemIdQueryString = query.ToString();

            var response = await client.GetAsync($"/interactions/{interactionType}/feeditems/:id?{itemIdQueryString}").ConfigureAwait(false);

            try
            {
                response.EnsureSuccessStatusCode();
                var content = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
                var interactionsList = JsonConvert.DeserializeObject<IReadOnlyCollection<T>>(content);
                return interactionsList;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }

            return null;
        }

        public async Task<Guid> CreateInteractionAsync(T interaction)
        {
            CreateInteractionDto createInteraction = null;
            if (interactionType.Equals("likes"))
            {
                createInteraction = new CreateLikeDto
                {
                    Id = (interaction as LikeModel).Id,
                    UserId = interaction.UserId,
                    FeedItemId = interaction.FeedItemId
                };
            }
            else 
            {
                createInteraction = new CreateCommentDto
                {
                    UserId = interaction.UserId,
                    FeedItemId = interaction.FeedItemId,
                    Message = (interaction as CommentModel).Message
                };
            }
            var jsonBody = JsonConvert.SerializeObject(interaction);
            var content = new StringContent(jsonBody,Encoding.UTF8,"application/json");

            var response = await client.PostAsync($"/interactions/{interactionType}",content).ConfigureAwait(false);
            Console.WriteLine($"----Path is: /interactions/{interactionType}----");
            Console.WriteLine($"----JsonBody is: {jsonBody}");
            try
            {
                response.EnsureSuccessStatusCode();
            }
            catch(Exception e)
            {
                Console.WriteLine(e.Message);
            }
            return Guid.Empty;
        }

        public async Task DeleteInteractionAsync(Guid id)
        {
            Console.WriteLine($"----Deleting interaction ----");

            var response = await client.DeleteAsync($"/interactions/{interactionType}/{id}").ConfigureAwait(false);
            
            Console.WriteLine($"----Path is: /interactions/{interactionType}/{id}----");
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



