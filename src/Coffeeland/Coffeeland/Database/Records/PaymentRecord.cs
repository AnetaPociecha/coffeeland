using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace Coffeeland.Database.Records
{
    public class PaymentRecord : IRecord
    {
        public int paymentId;
        public int orderId;
        public int amount;
        public string date;

       
        public void Fill(DataRow dr)
        {
            paymentId = Convert.ToInt32(dr["paymentId"]);
            orderId = Convert.ToInt32(dr["orderId"]);
            amount = Convert.ToInt32(dr["amount"]);
            date = dr["date"].ToString();
        }
    }
}