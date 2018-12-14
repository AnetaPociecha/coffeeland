using Coffeeland.Database;
using Coffeeland.Messaging.Dtos;
using Coffeeland.Messaging.Queries.Queries;
using Coffeeland.Messaging.Shared;
using Coffeeland.Session;
using System;

namespace Coffeeland.Messaging.Queries.Handlers
{

    public class GetPersonalDataQueryHandler : IQueryHandler<GetPersonalDataQuery>
    {
        public IResult Handle(GetPersonalDataQuery query)
        {
            int clientId = SessionRepository.GetClientIdOfSession(query.sessionToken);
            if (clientId == -1)
                throw new Exception();

            var clients = DatabaseQueryProcessor.GetClients();
            var foundClients = clients.FindAll(c => c.clientId == clientId);
            if (foundClients.Count != 1)
                throw new Exception();

            return new PersonalDataDto()
            {
                isSuccess = true,
                email = foundClients[0].email,
                lastName = foundClients[0].lastName,
                firstName = foundClients[0].firstName,
                receiveNewsletterEmail = foundClients[0].isSignedUpForNewsletter,
                newsletterEmail = foundClients[0].newsletterEmail
            };
        }
    }
}