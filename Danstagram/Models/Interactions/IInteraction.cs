using System;
using System.Collections.Generic;
using System.Text;

namespace Danstagram.Models.Interactions
{
    public interface IInteraction : IEntity
    {
        Guid ItemId { get; set; }

        Guid UserId { get; set; }
    }
}
