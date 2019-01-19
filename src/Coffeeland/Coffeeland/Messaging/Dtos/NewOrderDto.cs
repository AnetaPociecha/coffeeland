using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Coffeeland.Messaging.Shared;

namespace Coffeeland.Messaging.Dtos
{
    public class NewOrderDto : IResult
    {
        public int key;
        public OrderEntryDto[] orderEntries;
        public int totalPrice;
        public AddressDto address;
    }
}