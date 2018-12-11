using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Coffeeland.Messaging.Shared;
using Coffeeland.Messaging.Dtos;
using Newtonsoft.Json;

namespace Coffeeland.Messaging.Queries.Queries
{
    public class GetSomeNumberQuery : IQuery
    {
        public int variable;
    }
}