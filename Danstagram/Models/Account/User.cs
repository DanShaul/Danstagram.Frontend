using System;
using System.Collections.Generic;
using System.Text;

namespace Danstagram.Models.Account
{
    public class User : IEntity
    {
        public Guid Id { get; set; }
        public string Password { get; set; }
        public string Username { get; set; }

    }
}
