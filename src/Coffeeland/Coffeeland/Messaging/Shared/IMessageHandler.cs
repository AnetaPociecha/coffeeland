using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Coffeeland.Messaging.Shared
{
    public interface IMessageHandler<M> where M : IMessage
    {
        IResult Handle(M message);
    }
}