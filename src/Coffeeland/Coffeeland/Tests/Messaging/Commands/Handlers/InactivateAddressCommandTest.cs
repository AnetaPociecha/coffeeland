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
using Coffeeland.Tests.TestsShared;



namespace Coffeeland.Tests.Messaging.Commands.Handlers
{
    [TestFixture]
    public class InactivateAddressCommandTest
    {
        [Test]
        public void InactivateAddress_ProperAttributes_Success()
        {
            DatabaseQueryProcessor.Erase();
            Shared.FillTheDatabase();
            int clientId = 0;
            
            var testSessionToken = SessionRepository.StartNewSession(clientId);

            var inactivateAddressCommand = new InactivateAddressCommand
            {
                sessionToken = testSessionToken,
                addressKey = 0
            };

            var handler = new InactivateAddressCommandHandler();
            var result = (SuccessInfoDto) handler.Handle(inactivateAddressCommand);

            DatabaseQueryProcessor.Erase();
            SessionRepository.RemoveSession(testSessionToken);
            Assert.IsTrue(result.isSuccess);
           
        }

        [Test]
        public void InactivateAddress_NoAddressInDatabase_Fail()
        {
            DatabaseQueryProcessor.Erase();
            Shared.FillTheDatabase();

            int testClientId = 0;
            int testAddressKey = 50;
            var testSessionToken = SessionRepository.StartNewSession(testClientId);

            InactivateAddressCommand inactivateAddressCommand = new InactivateAddressCommand
            {
                sessionToken = testSessionToken,
                addressKey = testAddressKey
            };

            var handler = new InactivateAddressCommandHandler();
            var result = (SuccessInfoDto)handler.Handle(inactivateAddressCommand);

            DatabaseQueryProcessor.Erase();
            SessionRepository.RemoveSession(testSessionToken);
            Assert.IsFalse(result.isSuccess);

        }

    }
}