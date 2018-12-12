using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Coffeeland.Database.Records
{
    public class ComplaintRecord
    {
        public int orderId;
        public int workerId;
        public string description;
        public string date;
        public bool isClosed;
    }
}