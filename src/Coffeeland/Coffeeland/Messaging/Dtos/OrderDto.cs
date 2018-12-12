using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Coffeeland.Messaging.Shared;

namespace Coffeeland.Messaging.Dtos
{
    public class OrderDto : IResult
    {
        public int key;
        public List<OrderEntryDto> orderEntries;
        public int totalPrice;
        public AddressDto address;
        public int status;
        public string openDate;
        public string closeDate;
    }
}