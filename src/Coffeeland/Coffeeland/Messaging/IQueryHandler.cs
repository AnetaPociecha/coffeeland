using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Coffeeland.Messaging
{
    public interface IQueryHandler
    {
        void Handle();
    }
}