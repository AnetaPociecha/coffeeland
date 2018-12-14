using Coffeeland.Database;
using Coffeeland.Messaging.Dtos;
using Coffeeland.Messaging.Queries.Queries;
using Coffeeland.Messaging.Shared;
using Coffeeland.Session;
using System;

namespace Coffeeland.Messaging.Queries.Handlers
{
    public class GetAddressBookQueryHandler : IQueryHandler<GetAddressBookQuery>
    {
        public IResult Handle(GetAddressBookQuery query)
        {
            int clientId = SessionRepository.GetClientIdOfSession(query.sessionToken);
            if (clientId == -1)
                throw new Exception();

            var clients = DatabaseQueryProcessor.GetClients();
            var foundClients = clients.FindAll(c => c.clientId == clientId);
            if (foundClients.Count != 1)
                throw new Exception();

            var addresses = DatabaseQueryProcessor.GetAddresses(clients[0].clientId);

            var addressesDto = new AddressDto[addresses.Count];
            for (var i = 0; i < addresses.Count; i++)
            {
                addressesDto[i].key = addresses[i].addressId;
                addressesDto[i].country = addresses[i].country;
                addressesDto[i].city = addresses[i].city;
                addressesDto[i].street = addresses[i].street;
                addressesDto[i].ZIPCode = addresses[i].ZIPCode;
                addressesDto[i].buildingNumber = addresses[i].buildingNumber;
                addressesDto[i].apartmentNumber = addresses[i].apartmentNumber;
            }

            return new AddressBookDto()
            {
                isSuccess = true,
                addresses = addressesDto
            };
        }
    }
}