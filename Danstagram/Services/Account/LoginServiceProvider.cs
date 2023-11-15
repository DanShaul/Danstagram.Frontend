using Danstagram.Models.Account;
using Danstagram.Services.Feed;
using System;
using System.Threading.Tasks;
using Xamarin.Forms;
using System.Net.Http.Headers;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Danstagram.Models.Account;
using Danstagram.Models;
using Newtonsoft.Json;
using System.Runtime.InteropServices;

namespace Danstagram.Services.Account
{
    class LoginServiceProvider : ILoginServiceProvider
    {
        #region Properties
        private readonly IdentitiesApi identitiesApi;
        private readonly IDataStore<User> dataStore;
        #endregion
        #region Contructors
        public LoginServiceProvider()
        {
            dataStore = DependencyService.Get<IDataStore<User>>();
            identitiesApi = new IdentitiesApi();
        }
        #endregion
        #region Methods
        private class CreatedUserDto
        {
            public Guid Id { get; set; }
            public string Username { get; set; }
        }
        public async Task<Guid> CreateUser(string username, string password)
        {
            Console.WriteLine("----Creating User----");
            User user = new User
            {
                Id = Guid.NewGuid(),
                Username = username,
                Password = password
            };

            var response = await identitiesApi.CreateUserAsync(user);
            var content = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
            var createdUser = JsonConvert.DeserializeObject<CreatedUserDto>(content);
            Console.WriteLine(content);
            try
            {
                response.EnsureSuccessStatusCode();
                return createdUser.Id;
            }
            catch
            {
                throw new UnauthorizedAccessException(content);
            }
            

        }
        public async Task<Guid> AuthenticateUser(string username, string password)
        {
            Console.WriteLine("----Authenticating Users----");
            var response = await identitiesApi.AuthenticateUserAsync(username, password);
            var content = await response.Content.ReadAsStringAsync();

            try
            {
                response.EnsureSuccessStatusCode();
            }catch
            {
                throw new UnauthorizedAccessException(content);
            }
            
            var user = JsonConvert.DeserializeObject<User>(content);
            return user.Id;
        }
        #endregion
    }
}
