using Coffeeland.Database;
using Coffeeland.Messaging.Commands.Commands;
using Coffeeland.Messaging.Dtos;
using Coffeeland.Messaging.Shared;
using Coffeeland.Session;
using System;
namespace Coffeeland.Messaging.Commands.Handlers
{
    public class InactivateAddressCommandHandler : ICommandHandler<InactivateAddressCommand>
    {
        public IResult Handle(InactivateAddressCommand command)
        {
            int clientId = SessionRepository.GetClientIdOfSession(command.sessionToken);
            if (clientId == -1)
                throw new Exception();

            var address = DatabaseQueryProcessor.GetAddress(command.addressKey);

            if (address != null && address.clientId == clientId)
            {
                DatabaseQueryProcessor.UpdateAddress(address.addressId, false);
                return new SuccessInfoDto()
                {
                    isSuccess = true
                };
            }

            return new SuccessInfoDto()
            {
                isSuccess = false
            };
    }
    }
}