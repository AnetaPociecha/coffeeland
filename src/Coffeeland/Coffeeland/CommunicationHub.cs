using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNet.SignalR;
using Newtonsoft.Json;

namespace Coffeeland
{
    public class CommunicationHub : Hub
    {
        public dynamic Send(string json)
        {
            Dictionary<string, string> query = JsonConvert.DeserializeObject<Dictionary<string, string>>(json);
            string queryName = query.First().Value;

            var someObject = new Dictionary<string, string>();
            someObject.Add("property1", "value1");
            someObject.Add("property2", "value2");
            someObject.Add("property3", "value... surprisingly... 3");

            

            if(queryName.Equals("getSomeNumberQuery"))
                return new Random().Next(1, 11);
            if (queryName.Equals("getStringQuery"))
                return new String("wysokozmineralizowany".ToCharArray());
            if (queryName.Equals("getSameResponseQuery"))
                return new String(query.ElementAt(1).Value.ToCharArray());
            else
                return JsonConvert.SerializeObject(someObject);
        }
    }
}