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
using Coffeeland.Payments;
using Coffeeland.Messaging.Dtos;
using System.Globalization;

namespace Coffeeland.Tests.Messaging.Commands.Handlers
{
    [TestFixture]
    public class AddPaymentCommandTest
    {
        
        [TestCase(-1)]
        public void AddPayment_IncorrectClientId_Exception(int _clientId)
        {
            var testSessionToken = SessionRepository.StartNewSession(_clientId);

            var addComplaintCommand = new AddPaymentCommand
            {
                sessionToken = testSessionToken,
                paymentId = "PAY-2RR93057JR3600055LQ5FWMA"
            };

            var handler = new AddPaymentCommandHandler();
            TestDelegate result = () => handler.Handle(addComplaintCommand);

            SessionRepository.RemoveSession(testSessionToken);

            Assert.Throws<Exception>(result);
        }

        [TestCase(1, "PAY-8XR9554307815842ALRBQN2Q")]
        public void AddPayment_CorrectAttributes_Success(int _clientId, string _paymentId)
        {
            var testSessionToken = SessionRepository.StartNewSession(_clientId);
            DatabaseQueryProcessor.Erase();
            Shared.FillTheDatabase();

            var addPaymentCommand = new AddPaymentCommand
            {
                sessionToken = testSessionToken,
                paymentId = _paymentId
            };

            var lastOrder = DatabaseQueryProcessor.GetTheMostRecentOrder(_clientId);
            var total = DatabaseQueryProcessor.GetTotal(lastOrder.orderId);

            var isSuccessfulPayment = PaymentMethod.Check(_paymentId,total);
            var handler = new AddPaymentCommandHandler();
            var result = (SuccessInfoDto)handler.Handle(addPaymentCommand);

            DatabaseQueryProcessor.Erase();

            SessionRepository.RemoveSession(testSessionToken);
            Assert.IsTrue(result.isSuccess);
            Assert.IsTrue(isSuccessfulPayment);
        }


    }


}

