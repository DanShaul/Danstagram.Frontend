using Danstagram.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Danstagram.Services.Account
{
    interface ILoginServiceProvider
    {
        Task<Guid> CreateUser(string username, string password);
        Task<Guid> AuthenticateUser(string username, string password);
    }
}
