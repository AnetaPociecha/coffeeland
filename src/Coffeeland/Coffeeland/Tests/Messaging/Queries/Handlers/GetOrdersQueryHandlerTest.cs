using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Coffeeland.Database;
using Coffeeland.Messaging.Commands.Commands;
using Coffeeland.Messaging.Commands.Handlers;
using Coffeeland.Messaging.Dtos;
using Coffeeland.Messaging.Queries.Handlers;
using Coffeeland.Messaging.Queries.Queries;
using Coffeeland.Session;
using Coffeeland.Tests.TestsShared;
using NUnit.Framework;

namespace Coffeeland.Tests.Messaging.Queries.Handlers
{
    [TestFixture]
    public class GetOrdersQueryHandlerTest
    {

        [TestCase(-1)]
        public void GetOrders_IncorrectClientId_Exception(int _clientId)
        {
            var testSessionToken = SessionRepository.StartNewSession(_clientId);

            var getOrdersQuery = new GetOrdersQuery
            {
                sessionToken = testSessionToken,
            };

            var handler = new GetOrdersQueryHandler();
            TestDelegate result = () => handler.Handle(getOrdersQuery);

            SessionRepository.RemoveSession(testSessionToken);

            Assert.Throws<Exception>(result);
        }


        [TestCase(5)]
        public void GetOrders_ClientDoesntExist_Exception(int _clientId)
        {
            DatabaseQueryProcessor.Erase();
            Shared.FillTheDatabase();

            int clientId = _clientId;
            var testSessionToken = SessionRepository.StartNewSession(clientId);

            var getOrdersQuery = new GetOrdersQuery
            {
                sessionToken = testSessionToken,
            };

            var handler = new GetOrdersQueryHandler();
            TestDelegate result = () => handler.Handle(getOrdersQuery);
            
            DatabaseQueryProcessor.Erase();
            SessionRepository.RemoveSession(testSessionToken);

            Assert.Throws<Exception>(result);
        }

        [TestCase(1)]
        public void GetOrders_ClientExists_Success(int _clientId)
        {
            var expectedOrderEntry = new OrderEntryDto
            {
                key = 2,
                name = "Vergnano",
                quantity = 1,
                price = 25
            };
            var expectedOrderEntries = new OrderEntryDto[1];
            expectedOrderEntries[0] = expectedOrderEntry;

            var expectedAddress = new AddressDto
            {
                key = 2,
                country = "Poland",
                city = "Cracow",
                street = "Krakowska",
                ZIPCode = 30000,
                buildingNumber = 1,
                apartmentNumber= ""
            };

            var expectedOrder = new OrderDto
            {
                key = 2,
                orderEntries = expectedOrderEntries,
                totalPrice = 25,
                address = expectedAddress,
                status = 0,
                openDate = "2018-06-12",
                closeDate = ""
            };

            var expectedOrders = new OrderDto[1];
            expectedOrders[0] = expectedOrder;

            var expectedOrderDto = new OrdersDto
            {
                isSuccess = true,
                orders = expectedOrders
            };

            DatabaseQueryProcessor.Erase();
            Shared.FillTheDatabase();

            int clientId = _clientId;
            var testSessionToken = SessionRepository.StartNewSession(clientId);

            var getOrdersQuery = new GetOrdersQuery
            {
                sessionToken = testSessionToken,
            };

            var handler = new GetOrdersQueryHandler();
            var result = (OrdersDto) handler.Handle(getOrdersQuery);

            DatabaseQueryProcessor.Erase();
            SessionRepository.RemoveSession(testSessionToken);

            Assert.IsTrue(result.isSuccess);
            Assert.AreEqual(result.orders.Length, expectedOrderDto.orders.Length);
            Assert.AreEqual(result.orders[0].orderEntries.Length, expectedOrderDto.orders[0].orderEntries.Length);
            Assert.AreEqual(result.orders[0].status, expectedOrderDto.orders[0].status);
            Assert.AreEqual(result.orders[0].address.apartmentNumber, expectedOrderDto.orders[0].address.apartmentNumber);
        }
    }
}