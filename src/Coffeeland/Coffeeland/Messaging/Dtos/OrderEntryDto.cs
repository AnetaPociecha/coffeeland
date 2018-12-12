using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Coffeeland.Messaging.Shared;

namespace Coffeeland.Messaging.Dtos
{
    public class OrderEntryDto : IResult
    {
        public int orderId;
        public string name;
        public int quantity;
        public int price;
    }
}