using Coffeeland.Messaging.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Coffeeland.Messaging.Dtos
{
    public class ShopItemDto : IResult
    {
        public int key;
        public string name;
        public int price;
        public string img;
        public string description;
        public string type;
    }
}