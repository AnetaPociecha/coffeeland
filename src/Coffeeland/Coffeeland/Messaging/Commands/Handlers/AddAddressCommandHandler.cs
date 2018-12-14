using Coffeeland.Database;
using Coffeeland.Messaging.Commands.Commands;
using Coffeeland.Messaging.Queries.Handlers;
using Coffeeland.Messaging.Queries.Queries;
using Coffeeland.Messaging.Shared;
using Coffeeland.Session;
using System;

namespace Coffeeland.Messaging.Commands.Handlers
{
    public class AddAddressCommandHandler : ICommandHandler<AddAddressCommand>
    {
        public IResult Handle(AddAddressCommand command)
        {
            int clientId = SessionRepository.GetClientIdOfSession(command.sessionToken);
            if (clientId == -1)
                throw new Exception();

            if (false)  
            {
                // TODO Input checking
                throw new Exception();
            }

            var duplicate = DatabaseQueryProcessor.GetAddress(clientId, command.country, command.city, command.street, command.ZIPCode, command.buildingNumber, command.apartmentNumber);

            if(duplicate != null)
            {
                DatabaseQueryProcessor.UpdateAddress(duplicate.addressId, true);
            }
            else
            {
                DatabaseQueryProcessor.CreateNewAddress(
                clientId,
                command.country,
                command.city,
                command.street,
                command.ZIPCode,
                command.buildingNumber,
                command.apartmentNumber
                );
            }

            return new GetAddressBookQueryHandler().Handle(new GetAddressBookQuery() { sessionToken = command.sessionToken });
        }
    }
}