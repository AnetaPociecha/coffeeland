using System;
using Coffeeland.Database;
using Coffeeland.Messaging.Commands.Commands;
using Coffeeland.Messaging.Dtos;
using Coffeeland.Messaging.Shared;
using Coffeeland.Session;
using System.Threading;
using Coffeeland.MailService;
using Coffeeland.Payments;
using System.Globalization;

namespace Coffeeland.Messaging.Commands.Handlers
{
    public class AddOrderCommandHandler : ICommandHandler<AddOrder>
    {
        public IResult Handle(AddOrder command)
        {
            int clientId = SessionRepository.GetClientIdOfSession(command.sessionToken);
            if (clientId == -1)
                throw new Exception();

            var address = DatabaseQueryProcessor.GetAddress(
                clientId,
                command.address.country,
                command.address.city,
                command.address.street,
                command.address.ZIPCode,
                command.address.buildingNumber,
                command.address.apartmentNumber
                );

            if (address == null)
                throw new Exception();

            var products = DatabaseQueryProcessor.GetProducts();
            var totalPrice = 0;
            foreach (var orderEntry in command.orderEntries)
            {
                var foundProducts = products.FindAll(p => p.name == orderEntry.name);
                if (foundProducts.Count != 1)
                    throw new Exception();
                
                totalPrice += foundProducts[0].price/100 * orderEntry.quantity;
            }
        
            if (totalPrice.ToString("F", CultureInfo.InvariantCulture) != command.totalPrice)
                throw new Exception();

            var orderId = DatabaseQueryProcessor.CreateNewOrder(
              clientId,
              52,
              address.addressId,
              0,
              DateTime.Now.ToString("yyyy-MM-dd")
              );

            foreach (var orderEntry in command.orderEntries)
            {
                var foundProducts = products.FindAll(p => p.name == orderEntry.name);

                DatabaseQueryProcessor.CreateNewOrderEntry(
                    orderId,
                    foundProducts[0].productId,
                    orderEntry.quantity
                    );
            }

            return new SuccessInfoDto()
            {
                isSuccess = true
            };
        }
    }
}