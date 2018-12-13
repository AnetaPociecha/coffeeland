using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace Coffeeland.Database.Records
{
    public class WorkerRecord : IRecord
    {
        public int workerId;
        public WorkerRole role;
        public string email;
        public string password;

        public void Fill(DataRow dr)
        {
            workerId = Convert.ToInt32(dr["workerId"]);
            role = (WorkerRole) Enum.Parse(typeof(WorkerRole), dr["role"].ToString());
            email = dr["email"].ToString();
            password = dr["password"].ToString();
        }
    }
}