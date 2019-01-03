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

            var addresses = DatabaseQueryProcessor.GetAddresses(foundClients[0].clientId);

            var foundActiveAddresses = addresses.FindAll(a => a.isActive == true);

            var addressesDto = new AddressDto[foundActiveAddresses.Count];
            for (var i = 0; i < foundActiveAddresses.Count; i++)
            {
                addressesDto[i] = new AddressDto();
                addressesDto[i].key = foundActiveAddresses[i].addressId;
                addressesDto[i].country = foundActiveAddresses[i].country;
                addressesDto[i].city = foundActiveAddresses[i].city;
                addressesDto[i].street = foundActiveAddresses[i].street;
                addressesDto[i].ZIPCode = foundActiveAddresses[i].ZIPCode;
                addressesDto[i].buildingNumber = foundActiveAddresses[i].buildingNumber;
                addressesDto[i].apartmentNumber = foundActiveAddresses[i].apartmentNumber;
            }

            return new AddressBookDto()
            {
                isSuccess = true,
                addresses = addressesDto
            };
        }
    }
}