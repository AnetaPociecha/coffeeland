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
using Coffeeland.Payments;

namespace Coffeeland.Tests.Messaging.Commands.Handlers
{
    [TestFixture]
    public class AddOrderCommandHandlerTest
    {
        [TestCase(-1)]
        public void AddOrder_IncorrectClientId_Exception(int _clientId)
        {
            var testSessionToken = SessionRepository.StartNewSession(_clientId);

            var testOrderEntry = new OrderEntryDto
            {
                key = 0,
                name = "Lavazza",
                quantity = 1,
                price = 15
            };
            var testOrderEntries = new OrderEntryDto[1];
            testOrderEntries[0] = testOrderEntry;

            var testAddress = new AddressDto
            {
                key = 0,
                country = "Poland",
                city = "Gdynia",
                street = "Rzemieslnicza",
                ZIPCode = 30445,
                buildingNumber = 12,
                apartmentNumber = "1a"
            };

            
            var addOrderCommand = new AddOrderCommand
            {
                sessionToken = testSessionToken,
                orderEntries = testOrderEntries,
                totalPrice = 15,
                address = testAddress
            };

            var handler = new AddOrderCommandHandler();
            TestDelegate result = () => handler.Handle(addOrderCommand);

            SessionRepository.RemoveSession(testSessionToken);

            Assert.Throws<Exception>(result);
        }

        [TestCase(0)]
        public void AddOrder_AddressDoesntExist_Exception(int _clientId)
        {
            var testSessionToken = SessionRepository.StartNewSession(_clientId);
            DatabaseQueryProcessor.Erase();
            Shared.FillTheDatabase();

            var testOrderEntry = new OrderEntryDto
            {
                key = 0,
                name = "Lavazza",
                quantity = 1,
                price = 15
            };
            var testOrderEntries = new OrderEntryDto[1];
            testOrderEntries[0] = testOrderEntry;

            var testAddress = new AddressDto
            {
                key = 0,
                country = "Poland",
                city = "Gdansk",
                street = "Rzemieslnicza",
                ZIPCode = 30445,
                buildingNumber = 12,
                apartmentNumber = "1a"
            };

            var addOrderCommand = new AddOrderCommand
            {
                sessionToken = testSessionToken,
                orderEntries = testOrderEntries,
                totalPrice = 15,
                address = testAddress
            };

            var handler = new AddOrderCommandHandler();
            TestDelegate result = () => handler.Handle(addOrderCommand);

            DatabaseQueryProcessor.Erase();

            SessionRepository.RemoveSession(testSessionToken);

            Assert.Throws<Exception>(result);
        }

        [TestCase(0)]
        public void AddOrder_ProductDoesntExist_Exception(int _clientId)
        {
            var testSessionToken = SessionRepository.StartNewSession(_clientId);
            DatabaseQueryProcessor.Erase();
            Shared.FillTheDatabase();

            var testOrderEntry = new OrderEntryDto
            {
                key = 0,
                name = "SomeCoffee",
                quantity = 1,
                price = 15
            };
            var testOrderEntries = new OrderEntryDto[1];
            testOrderEntries[0] = testOrderEntry;

            var testAddress = new AddressDto
            {
                key = 0,
                country = "Poland",
                city = "Gdynia",
                street = "Rzemieslnicza",
                ZIPCode = 30445,
                buildingNumber = 12,
                apartmentNumber = "1a"
            };


            var addOrderCommand = new AddOrderCommand
            {
                sessionToken = testSessionToken,
                orderEntries = testOrderEntries,
                totalPrice = 15,
                address = testAddress
            };

            var handler = new AddOrderCommandHandler();
            TestDelegate result = () => handler.Handle(addOrderCommand);
            DatabaseQueryProcessor.Erase();
            SessionRepository.RemoveSession(testSessionToken);

            Assert.Throws<Exception>(result);
        }

        [TestCase(0)]
        public void AddOrder_IncorrectPrice_Exception(int _clientId)
        {
            var testSessionToken = SessionRepository.StartNewSession(_clientId);
            DatabaseQueryProcessor.Erase();
            Shared.FillTheDatabase();

            var testOrderEntry = new OrderEntryDto
            {
                key = 0,
                name = "Lavazza",
                quantity = 1,
                price = 15
            };
            var testOrderEntries = new OrderEntryDto[1];
            testOrderEntries[0] = testOrderEntry;

            var testAddress = new AddressDto
            {
                key = 0,
                country = "Poland",
                city = "Gdynia",
                street = "Rzemieslnicza",
                ZIPCode = 30445,
                buildingNumber = 12,
                apartmentNumber = "1a"
            };


            var addOrderCommand = new AddOrderCommand
            {
                sessionToken = testSessionToken,
                orderEntries = testOrderEntries,
                totalPrice = 25,
                address = testAddress
            };

            var handler = new AddOrderCommandHandler();
            TestDelegate result = () => handler.Handle(addOrderCommand);

            SessionRepository.RemoveSession(testSessionToken);
            DatabaseQueryProcessor.Erase();

            Assert.Throws<Exception>(result);
        }

        [TestCase(0)]
        public void AddOrder_CorrectAttributes_Success(int _clientId)
        {
            var testSessionToken = SessionRepository.StartNewSession(_clientId);
            DatabaseQueryProcessor.Erase();
            Shared.FillTheDatabase();

            var testOrderEntry = new OrderEntryDto
            {
                key = 0,
                name = "Lavazza",
                quantity = 1,
                price = 15
            };
            var testOrderEntries = new OrderEntryDto[1];
            testOrderEntries[0] = testOrderEntry;

            var testAddress = new AddressDto
            {
                key = 0,
                country = "Poland",
                city = "Gdynia",
                street = "Rzemieslnicza",
                ZIPCode = 30445,
                buildingNumber = 12,
                apartmentNumber = "1a"
            };


            var addOrderCommand = new AddOrderCommand
            {
                sessionToken = testSessionToken,
                orderEntries = testOrderEntries,
                totalPrice = 15,
                address = testAddress
            };

            var handler = new AddOrderCommandHandler();
            var result = (SuccessInfoDto) handler.Handle(addOrderCommand);
            var isSuccess = PaymentMethod.Check("PAY-5WW79794RN043793ALRBFH2A", 15);


            SessionRepository.RemoveSession(testSessionToken);
            DatabaseQueryProcessor.Erase();
            
            Assert.IsTrue(result.isSuccess);
        }
    }
}