using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Coffeeland.Messaging.Shared;

namespace Coffeeland.Messaging.Dtos
{
    public class AddressDto : IResult
    {
        public string country;
        public string city;
        public string street;
        public int ZIPCode;
        public int buildingNumber;
        public string apartmentNumber;
    }
}