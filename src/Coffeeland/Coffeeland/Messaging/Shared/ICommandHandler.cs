using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Coffeeland.Messaging.Shared
{
    public interface ICommandHandler<C> where C : ICommand
    {
        IResult Handle(C command);
    }
}