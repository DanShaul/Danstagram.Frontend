using Danstagram.Models.Account;
using System;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Danstagram.Services.Account
{
    class LoginServiceProvider : ILoginServiceProvider
    {
        #region Properties
        private readonly IDataStore<User> dataStore;
        #endregion
        #region Contructors
        public LoginServiceProvider()
        {
            dataStore = DependencyService.Get<IDataStore<User>>();
        }
        #endregion
        #region Methods
        public async Task<Guid> CreateUser(string username, string password)
        {
            var isUsernameExists = (await dataStore.GetAsync((existingUser) => existingUser.Username == username))!=(null);
            if (isUsernameExists)
            {
                return Guid.Empty;
            }
            var user = new User
            {
                Id = Guid.NewGuid(),
                Username = username,
                Password = password
            };
            await dataStore.CreateAsync(user);
            return user.Id;

        }
        public async Task<Guid> AuthenticateUser(string username, string password)
        {
            var user = await dataStore.GetAsync((existingUser) => existingUser.Username == username && existingUser.Password == password);
            if (user != null)
            {
                return user.Id;
            }
            return Guid.Empty;
        }
        #endregion
    }
}
