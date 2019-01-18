using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using NUnit.Framework;
using Coffeeland.Session;
using Coffeeland.Messaging.Commands.Commands;
using Coffeeland.Messaging.Commands.Handlers;
using Coffeeland.Database;
using Coffeeland.Tests.TestsShared;
using Coffeeland.Messaging.Dtos;

namespace Coffeeland.Tests.Messaging.Commands.Handlers
{
    [TestFixture]
    public class AddComplaintCommandHandlerTest
    {
        [TestCase(-1)]
        public void AddComplaint_IncorrectClientId_Exception(int _clientId)
        {
            var testSessionToken = SessionRepository.StartNewSession(_clientId);

            var addComplaintCommand = new AddComplaintCommand
            {
                sessionToken = testSessionToken,
                orderId = 0,
                description = "I am dissatisfied"
            };

            var handler = new AddComplaintCommandHandler();
            TestDelegate result = () => handler.Handle(addComplaintCommand);

            SessionRepository.RemoveSession(testSessionToken);

            Assert.Throws<Exception>(result);
        }

        [TestCase(100)]
        public void AddComplaint_NoOrderInDatabase_Exception(int _orderId)
        {
            DatabaseQueryProcessor.Erase();
            Shared.FillTheDatabase();

            var testSessionToken = SessionRepository.StartNewSession(0);

            var addComplaintCommand = new AddComplaintCommand
            {
                sessionToken = testSessionToken,
                orderId = _orderId,
                description = "I am dissatisfied"
            };

            var handler = new AddComplaintCommandHandler();
            TestDelegate result = () => handler.Handle(addComplaintCommand);

            SessionRepository.RemoveSession(testSessionToken);

            Assert.Throws<Exception>(result);
        }

        [TestCase(2)]
        public void AddComplaint_DifferentClientId_Exception(int _orderId)
        {
            DatabaseQueryProcessor.Erase();
            Shared.FillTheDatabase();

            var testSessionToken = SessionRepository.StartNewSession(0);

            var addComplaintCommand = new AddComplaintCommand
            {
                sessionToken = testSessionToken,
                orderId = _orderId,
                description = "I am dissatisfied"
            };

            var handler = new AddComplaintCommandHandler();
            TestDelegate result = () => handler.Handle(addComplaintCommand);

            SessionRepository.RemoveSession(testSessionToken);

            Assert.Throws<Exception>(result);
        }

        [TestCase(1, 2, "I am dissatisfied")]
        public void AddComplaint_CorrectArgs_Success(int _clientId, int _orderId, string _description)
        {
            DatabaseQueryProcessor.Erase();
            Shared.FillTheDatabase();

            var testSessionToken = SessionRepository.StartNewSession(_clientId);

            var addComplaintCommand = new AddComplaintCommand
            {
                sessionToken = testSessionToken,
                orderId = _orderId,
                description = _description
            };

            var handler = new AddComplaintCommandHandler();
            var result = (SuccessInfoDto) handler.Handle(addComplaintCommand);
            var receivedComplaint = DatabaseQueryProcessor.GetComplaint(_orderId);
            SessionRepository.RemoveSession(testSessionToken);

            DatabaseQueryProcessor.Erase();

            Assert.AreEqual(receivedComplaint.description, _description);
            Assert.AreEqual(receivedComplaint.openDate, DateTime.Now.ToString("yyyy-MM-dd"));
            Assert.IsTrue(result.isSuccess);
        }

        [TestCase(1, 1, "I am dissatisfied")]
        public void AddComplaint_ComplaintAlreadyExist_Exception(int _clientId, int _orderId, string _description)
        {
            DatabaseQueryProcessor.Erase();
            Shared.FillTheDatabase();

            var testSessionToken = SessionRepository.StartNewSession(_clientId);

            var addComplaintCommand = new AddComplaintCommand
            {
                sessionToken = testSessionToken,
                orderId = _orderId,
                description = _description
            };

            var handler = new AddComplaintCommandHandler();
            TestDelegate result = () => handler.Handle(addComplaintCommand);

            SessionRepository.RemoveSession(testSessionToken);

            Assert.Throws<Exception>(result);
        }
    }
}