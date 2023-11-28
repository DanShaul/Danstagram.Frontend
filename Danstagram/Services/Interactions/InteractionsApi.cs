using Danstagram.Models;
using Danstagram.Models.Interactions;
using Danstagram.Services.Common;
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
    public class InteractionsApi<T> : Api where T : IInteraction
    {
        #region Constructors
        public InteractionsApi()
        {
            Client.BaseAddress = new Uri(url);

            var runtimeType = typeof(T).ToString().Split('.').Last();
            interactionType = runtimeType.ToLower().Substring(0, runtimeType.Length - 5) + "s";
        }
        #endregion

        #region Properties
        private readonly string url = "https://10.0.2.2:5005";
        private readonly string interactionType;

        #endregion
        #region Classes
        private class CreateInteractionDto
        {
            public Guid UserId { get; set; }
            public Guid FeedItemId { get; set; }
        }
        private sealed class CreateLikeDto : CreateInteractionDto { public Guid Id { get; set; } }
        private sealed class CreateCommentDto : CreateInteractionDto { public string Message { get; set; } }
        #endregion
        #region Methods


        public async Task<bool> IsUp()
        {
            HttpResponseMessage response;
            try
            {
                response = await Client.GetAsync("/health").WrapTimeout();
                response.EnsureSuccessStatusCode();
                return true;
            }
            catch (TaskCanceledException)
            {
                return false;
            }
        }
        public async Task<IReadOnlyCollection<T>> GetItemInteractionsAsync(Guid itemId)
        {

            var query = HttpUtility.ParseQueryString(string.Empty);
            query["feedItemId"] = itemId.ToString();
            string itemIdQueryString = query.ToString();

            var response = await Client.GetAsync($"/interactions/{interactionType}/feeditems/:id?{itemIdQueryString}").ConfigureAwait(false);
            var content = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<IReadOnlyCollection<T>>(content);
        }

        public async Task CreateInteractionAsync(T interaction)
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

            await Client.PostAsync($"/interactions/{interactionType}",content);
        }

        public async Task DeleteInteractionAsync(Guid id)
        {
            await Client.DeleteAsync($"/interactions/{interactionType}/{id}");
        }
        #endregion
    }
}



