using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Coffeeland.Database.Records
{
    public class OrderRecord
    {
        public int orderId;
        public int clientId;
        public int workerId;
        public int addressId;
        public int status;
        public string openDate;
        public string closeDate;
    }
}