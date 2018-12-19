using Coffeeland.Messaging.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Coffeeland.Messaging.Commands.Commands
{
    public class AddAddressCommand : ICommand
    {
        public string sessionToken;
        public string country;
        public string city;
        public string street;
        public int ZIPCode;
        public int buildingNumber;
        public string apartmentNumber;
    }
}