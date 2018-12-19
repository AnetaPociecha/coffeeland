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

namespace Coffeeland.Tests.Messaging.Commands.Handlers
{
    [TestFixture]
    public class AddAddressCommandTest
    {

        [TestCase("Poland","Cracow","Urzednicza",34040,100,"")]
        public void AddAddress_ProperAttributes_Success(string _country, string _city, string _street, int _ZIPCode, int _buildingNumber, string _apatrmentNumber )
        {
            DatabaseQueryProcessor.Erase();
            int clientId = 0;
            var testSessionToken = SessionRepository.StartNewSession(clientId);

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
            var addressBook = (AddressBookDto) handler.Handle(addAddressCommand);

            var record = DatabaseQueryProcessor.GetAddress(clientId, _country, _city, _street, _ZIPCode, _buildingNumber, _apatrmentNumber);

            DatabaseQueryProcessor.Erase();
            SessionRepository.RemoveSession(testSessionToken);

            Assert.IsNotNull(record);
        }

    }
}