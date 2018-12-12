using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Coffeeland.Database.Records
{
    public class AddressRecord
    {
        public int addressId;
        public int clientId;
        public string country;
        public string city;
        public string street;
        public int ZIPCode;
        public int buildingNumber;
        public string apartmentNumber;
    }
}