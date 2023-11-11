/*using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;

namespace Danstagram.Services
{
    public class Api<T> : IApi<T> where T : IEntity
    {
        #region Constructors

        public Api(string apiUrl)
        {
            this.client = new HttpClient();
            this.client.BaseAddress = new Uri(apiUrl);
            client.DefaultRequestHeaders.Accept.Add(
                   new MediaTypeWithQualityHeaderValue("application/json"));
        }

        #endregion

        #region Properties

        private HttpClient client;



        #endregion

        #region Methods
        public async Task CreateAsync(string endpoint, T entity)
        {
            await this.client.PostAsync(endpoint, new StringContent(JsonConvert.SerializeObject(entity)));
        }

        public async Task<T> GetAsync(Guid id)
        {
            var response = await this.client.GetAsync($"/endpoint/{id}");
            if (response.IsSuccessStatusCode)
            {
                string content = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<T>(content);
            }
            return default;
        }

        public Task<T> GetAsync(Expression<Func<T, bool>> filter)
        {
            throw new NotImplementedException();
        }

        public Task<IReadOnlyCollection<T>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public Task<IReadOnlyCollection<T>> GetAllAsync(Expression<Func<T, bool>> filter)
        {
            throw new NotImplementedException();
        }

        public Task UpdateAsync(T entity)
        {
            throw new NotImplementedException();
        }

        public Task DeleteAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}
*/