using Coffeeland.Database;
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
    public class GetOrdersQueryHandler : IQueryHandler<GetOrdersQuery>
    {
        public IResult Handle(GetOrdersQuery query)
        {
            int clientId = SessionRepository.GetClientIdOfSession(query.sessionToken);
            if (clientId == -1)
                throw new Exception();

            var clients = DatabaseQueryProcessor.GetClients();
            var foundClients = clients.FindAll(c => c.clientId == clientId);
            if (foundClients.Count != 1)
                throw new Exception();

            var orderRecords = DatabaseQueryProcessor.GetOrders(foundClients[0].clientId);
            var orderDtos = new OrderDto[orderRecords.Count];

            for (var i = 0; i < orderRecords.Count; i++)
            {
                orderDtos[i] = new OrderDto();
                orderDtos[i].key = orderRecords[i].orderId;
                var addressRecord = DatabaseQueryProcessor.GetAddress(orderRecords[i].addressId);
                orderDtos[i].address = new AddressDto()
                {
                    country = addressRecord.country,
                    city = addressRecord.city,
                    ZIPCode = addressRecord.ZIPCode,
                    apartmentNumber = addressRecord.apartmentNumber,
                    buildingNumber = addressRecord.buildingNumber,
                    street = addressRecord.street,
                    key = addressRecord.addressId
                };
                orderDtos[i].openDate = orderRecords[i].openDate;
                orderDtos[i].closeDate = orderRecords[i].closeDate;
                orderDtos[i].status = orderRecords[i].status;

                var orderEntriesRecords = DatabaseQueryProcessor.GetOrderEntries(orderRecords[i].orderId);
                var orderEntriesDtos = new OrderEntryDto[orderEntriesRecords.Count];
                for(int j = 0; j < orderEntriesRecords.Count; j++)
                {
                    orderEntriesDtos[j] = new OrderEntryDto();
                    var product = DatabaseQueryProcessor.GetProduct(orderEntriesRecords[j].productId);
                    orderEntriesDtos[j].key = orderEntriesRecords[j].orderEntryId;   
                    orderEntriesDtos[j].name = product.name;
                    orderEntriesDtos[j].price = orderEntriesRecords[j].amount * product.price;
                    orderEntriesDtos[j].quantity = orderEntriesRecords[j].amount;
                }
                orderDtos[i].orderEntries = orderEntriesDtos;
                orderDtos[i].totalPrice = orderEntriesDtos.Sum(orderEntry => orderEntry.price * orderEntry.quantity);
            }

            return new OrdersDto()
            {
                isSuccess = true,
                orders = orderDtos
            };
        }
    }
}