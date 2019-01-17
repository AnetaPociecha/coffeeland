using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace Coffeeland.Database.Records
{
    public class OrderEntryRecord : IRecord
    {
        public int orderEntryId;
        public int orderId;
        public int productId;
        public int quantity;

        public void Fill(DataRow dr)
        {
            orderEntryId = Convert.ToInt32(dr["orderEntryId"]);
            orderId = Convert.ToInt32(dr["orderId"]);
            productId = Convert.ToInt32(dr["productId"]);
            quantity = Convert.ToInt32(dr["quantity"]);
        }
    }


}