using Coffeeland.Messaging.Dtos;
using Coffeeland.Messaging.Queries.Queries;
using Coffeeland.Messaging.Shared;
using Coffeeland.Session;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Coffeeland.Messaging.Queries.Handlers
{
    public class SignOutQueryHandler : IQueryHandler<SignOutQuery>
    {
        public IResult Handle(SignOutQuery query)
        {
            SessionRepository.RemoveSession(query.sessionToken);
            return new SuccessInfoDto()
            {
                isSuccess = true
            };
        }
    }
}