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

            
            var addOrderCommand = new AddOrder
            {
                sessionToken = testSessionToken,
                orderEntries = testOrderEntries,
                totalPrice = "15.00",
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

            var addOrderCommand = new AddOrder
            {
                sessionToken = testSessionToken,
                orderEntries = testOrderEntries,
                totalPrice = "15.00",
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


            var addOrderCommand = new AddOrder
            {
                sessionToken = testSessionToken,
                orderEntries = testOrderEntries,
                totalPrice = "15.00",
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


            var addOrderCommand = new AddOrder
            {
                sessionToken = testSessionToken,
                orderEntries = testOrderEntries,
                totalPrice = "25.00",
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
                int _key = 0;
                string _country = "Poland";
                string _city = "Gdynia";
                string _street = "Rzemieslnicza";
                int _ZIPCode = 30445;
                int _buildingNumber = 12;
                string _apartmentNumber = "1a";

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
                key = _key,
                country = _country,
                city = _city,
                street = _street,
                ZIPCode = _ZIPCode,
                buildingNumber = _buildingNumber,
                apartmentNumber = _apartmentNumber
            };


            var addOrderCommand = new AddOrder
            {
                sessionToken = testSessionToken,
                orderEntries = testOrderEntries,
                totalPrice = "15.00",
                address = testAddress
            };

            var addressId = DatabaseQueryProcessor.GetAddress(_clientId,
                                                              _country,
                                                              _city,
                                                              _street,
                                                              _ZIPCode,
                                                              _buildingNumber,
                                                              _apartmentNumber);

            var handler = new AddOrderCommandHandler();
            var result = (SuccessInfoDto) handler.Handle(addOrderCommand);

            var expectedOrder = DatabaseQueryProcessor.GetTheMostRecentOrder(_clientId);
            
            SessionRepository.RemoveSession(testSessionToken);
            DatabaseQueryProcessor.Erase();
            
           
            Assert.IsTrue(result.isSuccess);
        }
    }
}