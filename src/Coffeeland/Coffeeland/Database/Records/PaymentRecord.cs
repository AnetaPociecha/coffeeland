using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Coffeeland.Database.Records
{
    public class PaymentRecord
    {
        public int paymentId;
        public int orderId;
        public int amount;
        public string date;
    }
}