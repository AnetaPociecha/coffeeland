using System;
using Coffeeland.Database;
using Coffeeland.Messaging.Commands.Commands;
using Coffeeland.Messaging.Dtos;
using Coffeeland.Messaging.Shared;
using Coffeeland.Session;
using System.Threading;
using Coffeeland.MailService;
using Coffeeland.Payments;

namespace Coffeeland.Messaging.Commands.Handlers
{
    public class AddOrderCommandHandler : ICommandHandler<AddOrderCommand>
    {
        public IResult Handle(AddOrderCommand command)
        {
            int clientId = SessionRepository.GetClientIdOfSession(command.sessionToken);
            if (clientId == -1)
                throw new Exception();

            var address = DatabaseQueryProcessor.GetAddress(
                clientId,
                command.order.address.country,
                command.order.address.city,
                command.order.address.street,
                command.order.address.ZIPCode,
                command.order.address.buildingNumber,
                command.order.address.apartmentNumber
                );

            if (address == null)
                throw new Exception();

            var products = DatabaseQueryProcessor.GetProducts();

            var totalPrice = 0;
            foreach (var orderEntry in command.order.orderEntries)
            {
                var foundProducts = products.FindAll(p => p.name == orderEntry.name);
                if (foundProducts.Count != 1)
                    throw new Exception();
                
                totalPrice += foundProducts[0].price * orderEntry.quantity;
            }
        
            if (totalPrice != command.order.totalPrice)
                throw new Exception();

            var orderId = DatabaseQueryProcessor.CreateNewOrder(
              clientId,
              0,  // TO DO - get rid of workers in database
              address.addressId,
              command.order.status,
              DateTime.Now.ToString("dd-MM-yyyy")
              );

            foreach (var orderEntry in command.order.orderEntries)
            {
                var foundProducts = products.FindAll(p => p.name == orderEntry.name);

                DatabaseQueryProcessor.CreateNewOrderEntry(
                    orderId,
                    foundProducts[0].productId,
                    orderEntry.quantity
                    );
                totalPrice += foundProducts[0].price * orderEntry.quantity;
            }

            var client = DatabaseQueryProcessor.GetClient(clientId);
            DatabaseQueryProcessor.CreateNewPayment(
                    command.paymentId,
                    orderId,
                    totalPrice,
                    DateTime.Now.ToString("dd-MM-yyyy")
                    );

            ThreadPool.QueueUserWorkItem(o => (new OrderPlacementEmail()).Send(clientId));
            ThreadPool.QueueUserWorkItem(o => {
                var isSuccess = PaymentMethod.Check(command.paymentId, totalPrice);
                if (isSuccess)
                    DatabaseQueryProcessor.UpdateOrder(orderId, DateTime.Now.ToString("dd-MM-yyyy"));
                    DatabaseQueryProcessor.UpdateOrder(orderId, 1);
                //TO DO send email about payment
                });

            return new SuccessInfoDto()
            {
                isSuccess = true
            };
        }
    }
}