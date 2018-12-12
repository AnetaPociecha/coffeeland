using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Coffeeland.Messaging.Shared;

namespace Coffeeland.Messaging.Dtos
{
    public class ProductDto : IResult
    {
        public int key;
        public string name;
        public string img;
        public string type;
        public string description;
    }
}