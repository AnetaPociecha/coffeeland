using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Coffeeland.Messaging.Shared;

namespace Coffeeland.Messaging.Commands.Commands
{
    public class AddComplaintCommand : ICommand
    {
        public string sessionToken;
        public int orderId;
        public string description;
    }
}