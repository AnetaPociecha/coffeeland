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

        [TestCase(0, "Poland", "Warsaw", "Szugadzka", 34040, 100, "2a")]
        [TestCase(0,"Poland", "Cracow", "Urzednicza", 34040, 100, "")]
        public void AddAddress_CorrectAttributes_Success(int _clientId, string _country, string _city, string _street, int _ZIPCode, int _buildingNumber, string _apartmentNumber)
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

        [TestCase(5, "Poland", "Cracow", "Urzednicza", 34040, 100, "")]
        public void AddAddress_ClientDoesntExist_Exception(int _clientId, string _country, string _city, string _street, int _ZIPCode, int _buildingNumber, string _apatrmentNumber)
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
                apartmentNumber = _apatrmentNumber
            };

            var handler = new AddAddressCommandHandler();
            TestDelegate result = () => handler.Handle(addAddressCommand);
            
            DatabaseQueryProcessor.Erase();
            SessionRepository.RemoveSession(testSessionToken);

            Assert.Throws<Exception>(result);
        }
    }
}