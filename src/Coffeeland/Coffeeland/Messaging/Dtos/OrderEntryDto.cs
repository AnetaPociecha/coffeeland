using Coffeeland.Messaging.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Coffeeland.Messaging.Dtos
{
    public class OrderEntryDto : IResult
    {
        public int key;
        public string name;
        public int quantity;
        public int price;
    }
}