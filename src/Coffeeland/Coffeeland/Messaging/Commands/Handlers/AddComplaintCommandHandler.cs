using System;
using Coffeeland.Messaging.Shared;
using Coffeeland.Messaging.Commands.Commands;
using Coffeeland.Session;
using Coffeeland.Database;
using Coffeeland.Messaging.Dtos;

namespace Coffeeland.Messaging.Commands.Handlers
{
    public class AddComplaintCommandHandler : ICommandHandler<AddComplaintCommand>
    {
        public IResult Handle(AddComplaintCommand command)
        {
            int clientId = SessionRepository.GetClientIdOfSession(command.sessionToken);
            if (clientId == -1)
                throw new Exception();

            var order = DatabaseQueryProcessor.GetOrder(command.orderId);

            if (order == null)
                throw new Exception();

            if (order.clientId != clientId)
                throw new Exception();

            var foundComplaint = DatabaseQueryProcessor.GetComplaint(order.orderId);
            if (foundComplaint != null)
                throw new Exception();

            DatabaseQueryProcessor.CreateNewComplaint(
                command.orderId,
                0,
                command.description,
                DateTime.Now.ToString("yyyy-MM-dd"),
                false
                );

            return new SuccessInfoDto()
            {
                isSuccess = true
            };
        }
    }
}