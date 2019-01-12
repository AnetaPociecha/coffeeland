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

        [TestCase(-1)]
        public void InactivateAddress_IncorrectClientId_Exception(int _clientId)
        {
            var testSessionToken = SessionRepository.StartNewSession(_clientId);

            var inactivateAddressCommand = new InactivateAddressCommand
            {
                sessionToken = testSessionToken,
                addressKey = 0
            };

            var handler = new InactivateAddressCommandHandler();
            TestDelegate result = () => handler.Handle(inactivateAddressCommand);

            SessionRepository.RemoveSession(testSessionToken);

            Assert.Throws<Exception>(result);
        }

        [Test]
        public void InactivateAddress_CorrectAttributes_Success()
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
            var result = (AddressBookDto) handler.Handle(inactivateAddressCommand);

            DatabaseQueryProcessor.Erase();
            SessionRepository.RemoveSession(testSessionToken);

            Assert.AreEqual(result.addresses.Length,1);
           
        }

        [Test]
        public void InactivateAddress_AddressImcompatibleWithClient_Fail()
        {
            DatabaseQueryProcessor.Erase();
            Shared.FillTheDatabase();
            int clientId = 0;

            var testSessionToken = SessionRepository.StartNewSession(clientId);

            var inactivateAddressCommand = new InactivateAddressCommand
            {
                sessionToken = testSessionToken,
                addressKey = 2
            };

            var handler = new InactivateAddressCommandHandler();
            var result = (SuccessInfoDto)handler.Handle(inactivateAddressCommand);

            DatabaseQueryProcessor.Erase();
            SessionRepository.RemoveSession(testSessionToken);

            Assert.IsFalse(result.isSuccess);

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