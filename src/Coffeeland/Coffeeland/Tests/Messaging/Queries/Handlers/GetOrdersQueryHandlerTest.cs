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

        [TestCase(5)]
        public void GetOrders_WrongClientId_Exception(int _clientId)
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

        [TestCase(0)]
        public void GetOrders_CorrectData_Success(int _clientId)
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
            var result = (OrdersDto) handler.Handle(getOrdersQuery);

            DatabaseQueryProcessor.Erase();
            SessionRepository.RemoveSession(testSessionToken);

            Assert.IsTrue(result.isSuccess);
        }
    }
}