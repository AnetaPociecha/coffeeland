using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace Coffeeland.Database.Records
{
    public class ComplaintRecord : IRecord
    {
        public int orderId;
        public int workerId;
        public string description;
        public string openDate;
        public bool isClosed;

        public void Fill(DataRow dr)
        {
            orderId = Convert.ToInt32(dr["orderId"]);
            workerId = Convert.ToInt32(dr["workerId"]);
            description = dr["description"].ToString();
            openDate = dr["openDate"].ToString();
            isClosed = Convert.ToInt32(dr["isClosed"]) == 1 ? true : false;
        }
    }
}