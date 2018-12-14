using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Coffeeland.Session
{
    public class ClientSession
    {
        public int clientId;
        public string sessionToken;
        public DateTime expirationDate;
    }
}