using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Coffeeland.Messaging.Shared;
using Coffeeland.Messaging.Queries.Queries;
using Coffeeland.Messaging.Dtos;

namespace Coffeeland.Messaging.Queries.Handlers
{
    public class GetSomeNumberQueryHandler : IQueryHandler<GetSomeNumberQuery>
    {
        public IResult Handle(GetSomeNumberQuery query)
        {
            return new NumberDto
            {
                number = 2
            };
        }
    }
}