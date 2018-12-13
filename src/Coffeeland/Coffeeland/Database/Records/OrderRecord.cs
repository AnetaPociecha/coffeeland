using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace Coffeeland.Database.Records
{
    public class OrderRecord :IRecord
    {
        public int orderId;
        public int clientId;
        public int workerId;
        public int addressId;
        public int status;
        public string openDate;
        public string closeDate;

        public void Fill(DataRow dr)
        {
            orderId = Convert.ToInt32(dr["orderId"]);
            clientId = Convert.ToInt32(dr["clientId"]);
            workerId = Convert.ToInt32(dr["workerId"]);
            addressId = Convert.ToInt32(dr["addressId"]);
            status = Convert.ToInt32(dr["status"]);
            openDate = dr["openDate"].ToString();
            closeDate = dr["closeDate"].ToString();
        }
    }
}