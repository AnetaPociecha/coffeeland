using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using NUnit.Framework;
using Coffeeland.Database;
using Coffeeland.Messaging.Commands.Handlers;
using Coffeeland.Messaging.Commands.Commands;
using Coffeeland.Messaging.Dtos;
using Coffeeland.Messaging.Shared;
using Coffeeland.Session;



namespace Coffeeland.Tests.Messaging.Commands.Handlers
{
    [TestFixture]
    public class InactivateAddressCommandTest
    {
        [Test]
        public void InactivateAddress_ProperAttributes_Success()
        {
            DatabaseQueryProcessor.Erase();
            int clientId = 0;
            DatabaseQueryProcessor.CreateNewAddress(clientId, "Poland", "Cracow", "Urzednicza", 34040, 100, "1");
            String testSessionToken = SessionRepository.StartNewSession(clientId);

            InactivateAddressCommand inactivateAddressCommand = new InactivateAddressCommand
            {
                sessionToken = testSessionToken,
                addressKey = 0
            };

            InactivateAddressCommandHandler handler = new InactivateAddressCommandHandler();
            SuccessDto result = (SuccessDto) handler.Handle(inactivateAddressCommand);

            DatabaseQueryProcessor.Erase();
            SessionRepository.RemoveSession(testSessionToken);
            Assert.IsTrue(result.isSuccess);
           
        }

        [Test]
        public void InactivateAddress_NoAddressInDatabase_Fail()
        {
            DatabaseQueryProcessor.Erase();
            int clientId = 0;
            int testAddressKey = 0;
            String testSessionToken = SessionRepository.StartNewSession(clientId);

            InactivateAddressCommand inactivateAddressCommand = new InactivateAddressCommand
            {
                sessionToken = testSessionToken,
                addressKey = testAddressKey
            };

            InactivateAddressCommandHandler handler = new InactivateAddressCommandHandler();
            SuccessDto result = (SuccessDto)handler.Handle(inactivateAddressCommand);

            DatabaseQueryProcessor.Erase();
            SessionRepository.RemoveSession(testSessionToken);
            Assert.IsFalse(result.isSuccess);

        }

    }
}