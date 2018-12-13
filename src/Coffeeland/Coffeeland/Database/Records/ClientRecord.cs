using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace Coffeeland.Database.Records
{
    public class ClientRecord : IRecord
    {
        public int clientId;
        public string email;
        public string firstName;
        public string lastName;
        public string password;
        public bool newsletter;

        public void Fill(DataRow dr)
        {
            clientId = Convert.ToInt32(dr["clientId"]);
            email = dr["email"].ToString();
            firstName = dr["firstName"].ToString();
            lastName = dr["lastName"].ToString();
            password = dr["password"].ToString();
            newsletter = Convert.ToInt32(dr["newsletter"]) == 1 ? true : false;
        }
    }
}