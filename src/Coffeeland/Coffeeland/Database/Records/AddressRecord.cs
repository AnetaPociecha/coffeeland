using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace Coffeeland.Database.Records
{
    public class AddressRecord : IRecord
    {
        public int addressId;
        public int clientId;
        public string country;
        public string city;
        public string street;
        public int ZIPCode;
        public int buildingNumber;
        public string apartmentNumber;
        public bool isActive;

        public void Fill(DataRow dr)
        {
            addressId = Convert.ToInt32(dr["addressId"]);
            clientId = Convert.ToInt32(dr["clientId"]);
            country = dr["country"].ToString();
            city = dr["city"].ToString();
            street = dr["street"].ToString();
            ZIPCode = Convert.ToInt32(dr["ZIPCode"]);
            buildingNumber = Convert.ToInt32(dr["buildingNumber"]);
            apartmentNumber = dr["apartmentNumber"].ToString();
            isActive = Convert.ToInt32(dr["isActive"]) == 1 ? true : false;
        }
    }
}