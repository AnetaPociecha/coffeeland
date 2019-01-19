using Coffeeland.Messaging.Dtos;
using Coffeeland.Messaging.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Coffeeland.Messaging.Commands.Commands
{
    public class AddOrderCommand : ICommand
    {
        public string sessionToken;
        public string paymentId;
        public NewOrderDto order;
    }
}