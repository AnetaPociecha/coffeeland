using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using NUnit.Framework;
using Coffeeland.Database;
using Coffeeland.Session;
using Coffeeland.Messaging.Commands.Handlers;
using Coffeeland.Messaging.Commands.Commands;
using Coffeeland.Database.Records;
using Coffeeland.Messaging.Dtos;
using Coffeeland.Tests.TestsShared;

namespace Coffeeland.Tests.Messaging.Commands.Handlers
{
    [TestFixture]
    public class AddAddressCommandTest
    {

        [TestCase(-1)]
        public void AddAddress_IncorrectClientId_Exception(int _clientId)
        {
            var testSessionToken = SessionRepository.StartNewSession(_clientId);

            var addAddressCommand = new AddAddressCommand
            {
                sessionToken = testSessionToken,
                country = "Poland",
                city = "Wroclove",
                street = "Mickiewicza",
                ZIPCode = 30000,
                buildingNumber = 12,
                apartmentNumber = ""
            };

            var handler = new AddAddressCommandHandler();
            TestDelegate result = () => handler.Handle(addAddressCommand);

            SessionRepository.RemoveSession(testSessionToken);

            Assert.Throws<Exception>(result);
        }

        [TestCase("123")]
        [TestCase("invalid_city_name")]
        [TestCase("invalid city&^%$$@#")]
        [TestCase("invalidcity123")]
        [TestCase("123invalidcity")]
        public void AddAddress_InvalidCity_Exception(string _city)
        {
            var testSessionToken = SessionRepository.StartNewSession(0);

            var addAddressCommand = new AddAddressCommand
            {
                sessionToken = testSessionToken,
                country = "Poland",
                city = _city,
                street = "Mickiewicza",
                ZIPCode = 30000,
                buildingNumber = 12,
                apartmentNumber = ""
            };

            var handler = new AddAddressCommandHandler();
            TestDelegate result = () => handler.Handle(addAddressCommand);

            SessionRepository.RemoveSession(testSessionToken);

            Assert.Throws<Exception>(result);
        }

        [TestCase("123")]
        [TestCase("invalid_country_name")]
        [TestCase("invalid country&^%$$@#")]
        [TestCase("invalidcountry123")]
        [TestCase("123invalidcountry")]
        public void AddAddress_InvalidCountry_Exception(string _country)
        {
            var testSessionToken = SessionRepository.StartNewSession(0);

            var addAddressCommand = new AddAddressCommand
            {
                sessionToken = testSessionToken,
                country = _country,
                city = "Wroclove",
                street = "Mickiewicza",
                ZIPCode = 30000,
                buildingNumber = 12,
                apartmentNumber = ""
            };

            var handler = new AddAddressCommandHandler();
            TestDelegate result = () => handler.Handle(addAddressCommand);

            SessionRepository.RemoveSession(testSessionToken);

            Assert.Throws<Exception>(result);
        }

        [TestCase("123")]
        [TestCase("invalid_street_name")]
        [TestCase("invalid street&^%$$@#")]
        [TestCase("invalidstreet123")]
        [TestCase("123invalidstreet")]
        public void AddAddress_InvalidStreet_Exception(string _street)
        {
            var testSessionToken = SessionRepository.StartNewSession(0);

            var addAddressCommand = new AddAddressCommand
            {
                sessionToken = testSessionToken,
                country = "Poland",
                city = "Wroclove",
                street = _street,
                ZIPCode = 30000,
                buildingNumber = 12,
                apartmentNumber = ""
            };

            var handler = new AddAddressCommandHandler();
            TestDelegate result = () => handler.Handle(addAddressCommand);

            SessionRepository.RemoveSession(testSessionToken);

            Assert.Throws<Exception>(result);
        }

        [TestCase("123aaa")]
        [TestCase("a1")]
        [TestCase("1ABC")]
        [TestCase("abc")]
        [TestCase("123 a")]
        [TestCase("123-a")]
        public void AddAddress_InvalidApartmentNumber_Exception(string _apartmentNumber)
        {
            var testSessionToken = SessionRepository.StartNewSession(0);

            var addAddressCommand = new AddAddressCommand
            {
                sessionToken = testSessionToken,
                country = "Poland",
                city = "Wroclove",
                street = "Mickiewicza",
                ZIPCode = 30000,
                buildingNumber = 12,
                apartmentNumber = _apartmentNumber
            };

            var handler = new AddAddressCommandHandler();
            TestDelegate result = () => handler.Handle(addAddressCommand);

            SessionRepository.RemoveSession(testSessionToken);

            Assert.Throws<Exception>(result);
        }

        [TestCase(0,"Poland", "Cracow", "Urzednicza", 34040, 100, "")]
        public void AddAddress_AddNewAddress_Success(int _clientId, string _country, string _city, string _street, int _ZIPCode, int _buildingNumber, string _apartmentNumber)
        {
            DatabaseQueryProcessor.Erase();
            Shared.FillTheDatabase();

            var testSessionToken = SessionRepository.StartNewSession(_clientId);

            var addAddressCommand = new AddAddressCommand
            {
                sessionToken = testSessionToken,
                country = _country,
                city = _city,
                street = _street,
                ZIPCode = _ZIPCode,
                buildingNumber = _buildingNumber,
                apartmentNumber = _apartmentNumber
            };

            var handler = new AddAddressCommandHandler();
            var addressBook = (AddressBookDto)handler.Handle(addAddressCommand);

            var record = DatabaseQueryProcessor.GetAddress(_clientId, _country, _city, _street, _ZIPCode, _buildingNumber, _apartmentNumber);

            DatabaseQueryProcessor.Erase();
            SessionRepository.RemoveSession(testSessionToken);

            Assert.IsNotNull(record);
            Assert.AreEqual(_clientId, record.clientId);
            Assert.AreEqual(_country, record.country);
            Assert.AreEqual(_city, record.city);
            Assert.AreEqual(_street, record.street);
            Assert.AreEqual(_ZIPCode, record.ZIPCode);
            Assert.AreEqual(_buildingNumber, record.buildingNumber);
            Assert.AreEqual(_apartmentNumber, record.apartmentNumber);
        }

        [TestCase(0, "Poland", "Gdynia", "Rzemieslnicza", 30445, 12, "1a")]
        public void AddAddress_DuplicateAddress_Success(int _clientId, string _country, string _city, string _street, int _ZIPCode, int _buildingNumber, string _apartmentNumber)
        {
            DatabaseQueryProcessor.Erase();
            Shared.FillTheDatabase();
            
            var testSessionToken = SessionRepository.StartNewSession(_clientId);
            var addressExists = DatabaseQueryProcessor.GetAddress(_clientId,_country,_city,_street,_ZIPCode,_buildingNumber,_apartmentNumber);

            if (addressExists != null)
            {
                DatabaseQueryProcessor.UpdateAddress(addressExists.addressId, false);
            }

            var addAddressCommand = new AddAddressCommand
            {
                sessionToken = testSessionToken,
                country = _country,
                city = _city,
                street = _street,
                ZIPCode = _ZIPCode,
                buildingNumber = _buildingNumber,
                apartmentNumber = _apartmentNumber
            };

            var handler = new AddAddressCommandHandler();
            var addressBook = (AddressBookDto)handler.Handle(addAddressCommand);

            var record = DatabaseQueryProcessor.GetAddress(_clientId, _country, _city, _street, _ZIPCode, _buildingNumber, _apartmentNumber);

            DatabaseQueryProcessor.Erase();
            SessionRepository.RemoveSession(testSessionToken);

            Assert.IsTrue(record.isActive);
        }

        [TestCase(5)]
        public void AddAddress_ClientDoesntExist_Exception(int _clientId)
        {
            DatabaseQueryProcessor.Erase();
            Shared.FillTheDatabase();

            var testSessionToken = SessionRepository.StartNewSession(_clientId);

            var addAddressCommand = new AddAddressCommand
            {
                sessionToken = testSessionToken,
                country = "Poland",
                city = "Cracow",
                street = "Urzednicza",
                ZIPCode = 34040,
                buildingNumber = 100,
                apartmentNumber = ""
            };

            var handler = new AddAddressCommandHandler();
            TestDelegate result = () => handler.Handle(addAddressCommand);
            
            DatabaseQueryProcessor.Erase();
            SessionRepository.RemoveSession(testSessionToken);

            Assert.Throws<Exception>(result);
        }
    }
}