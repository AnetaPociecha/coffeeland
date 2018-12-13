using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace Coffeeland.Database.Records
{
    public class ProductRecord : IRecord
    {
        public int productId;
        public string name;
        public int price;
        public string imagePath;
        public string productType;
        public string description;

        public void Fill(DataRow dr)
        {
            productId = Convert.ToInt32(dr["productId"]);
            name = dr["name"].ToString();
            price = Convert.ToInt32(dr["price"]);
            imagePath = dr["imagePath"].ToString();
            productType = dr["productType"].ToString();
            description = dr["description"].ToString();
        }
    }
}