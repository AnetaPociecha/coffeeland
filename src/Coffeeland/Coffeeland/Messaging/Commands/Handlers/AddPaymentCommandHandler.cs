using System;
using Coffeeland.Database;
using Coffeeland.Messaging.Commands.Commands;
using Coffeeland.Messaging.Dtos;
using Coffeeland.Messaging.Shared;
using Coffeeland.Session;
using System.Threading;
using Coffeeland.MailService;
using Coffeeland.Payments;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Coffeeland.Messaging.Commands.Handlers
{
    public class AddPaymentCommandHandler : ICommandHandler<AddPaymentCommand>
    {
        public IResult Handle(AddPaymentCommand command)
        {
            int clientId = SessionRepository.GetClientIdOfSession(command.sessionToken);
            if (clientId == -1)
                throw new Exception();

            var client = DatabaseQueryProcessor.GetClient(clientId);
            var order = DatabaseQueryProcessor.GetTheMostRecentOrder(client.clientId);

            var totalPrice = 15; // TO DO
            DatabaseQueryProcessor.CreateNewPayment(
                    command.paymentId,
                    command.orderId,
                    totalPrice,
                    DateTime.Now.ToString("yyyy-MM-dd")
                    );

            ThreadPool.QueueUserWorkItem(o => (new OrderPlacementEmail()).Send(clientId));
            ThreadPool.QueueUserWorkItem(o =>
            {
                var isSuccess = PaymentMethod.Check(command.paymentId, totalPrice);
                if (isSuccess)
                {
                    DatabaseQueryProcessor.UpdateOrder(order.orderId, DateTime.Now.ToString("dd-MM-yyyy"));
                    DatabaseQueryProcessor.UpdateOrder(order.orderId, 1);
                    //TO DO send email about payment
                }
            });

            throw new NotImplementedException();
        }
    }
}