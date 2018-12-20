using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Coffeeland.Database;
using Coffeeland.Messaging.Queries.Queries;
using Coffeeland.Messaging.Queries.Handlers;
using Coffeeland.Messaging.Dtos;
using Coffeeland.Session;
using Coffeeland.Tests.TestsShared;
using NUnit.Framework;

namespace Coffeeland.Tests.Messaging.Queries.Handlers
{
    [TestFixture]
    public class GetAddressBookQueryHandlerTest
    {
        [TestCase(0)]
        public void GetAddress_CorrectAttributes_Success(int _clientId)
        {
            DatabaseQueryProcessor.Erase();
            Shared.FillTheDatabase();

            var testAddresses = new AddressDto[2];
            var firstAddress = new AddressDto
            {
                key = 0,
                country = "Poland",
                city = "Gdynia",
                street = "Rzemieslnicza",
                ZIPCode = 30445,
                buildingNumber = 23,
                apartmentNumber = "1a"
            };
            testAddresses[0] = firstAddress;

            var secondAddress = new AddressDto
            {
                key = 0,
                country = "Poland",
                city = "Warsaw",
                street = "Grodzka",
                ZIPCode = 25487,
                buildingNumber = 12,
                apartmentNumber = "1"
            };
            testAddresses[1] = secondAddress;

            var expectedAddressBook = new AddressBookDto
            {
                isSuccess = true,
                addresses = testAddresses
            };

            var testSessionToken = SessionRepository.StartNewSession(_clientId);

            var getAddressBookQuery = new GetAddressBookQuery
            {
                sessionToken = testSessionToken,
            };

            var handler = new GetAddressBookQueryHandler();
            var addressBook = (AddressBookDto)handler.Handle(getAddressBookQuery);
            
            DatabaseQueryProcessor.Erase();
            SessionRepository.RemoveSession(testSessionToken);
            Assert.AreEqual(expectedAddressBook.addresses.Length, addressBook.addresses.Length);
            Assert.AreEqual(expectedAddressBook.addresses[0].country, addressBook.addresses[0].country);
            Assert.AreEqual(expectedAddressBook.addresses[1].city, addressBook.addresses[1].city);
            Assert.AreEqual(expectedAddressBook.addresses[1].apartmentNumber, addressBook.addresses[1].apartmentNumber);

        }

        [TestCase(5)]
        public void AddAddress_ClientDoesntExist_Exception(int _clientId)
        {
            DatabaseQueryProcessor.Erase();
            Shared.FillTheDatabase();

            var testSessionToken = SessionRepository.StartNewSession(_clientId);

            var getAddressBookQuery = new GetAddressBookQuery
            {
                sessionToken = testSessionToken,
            };

            var handler = new GetAddressBookQueryHandler();
            TestDelegate result = () => handler.Handle(getAddressBookQuery);

            DatabaseQueryProcessor.Erase();
            SessionRepository.RemoveSession(testSessionToken);

            Assert.Throws<Exception>(result);
        }

    }
}