using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Coffeeland.Database;
using Coffeeland.Messaging.Dtos;
using Coffeeland.Messaging.Queries.Handlers;
using Coffeeland.Messaging.Queries.Queries;
using Coffeeland.Session;
using Coffeeland.Tests.TestsShared;
using NUnit.Framework;

namespace Coffeeland.Tests.Messaging.Queries.Handlers
{
    [TestFixture]
    public class GetPersonalDataQueryHandlerTest
    {
        [TestCase(-1)]
        public void GetPersonalData_IncorrectClientId_Exception(int _clientId)
        {
            var testSessionToken = SessionRepository.StartNewSession(_clientId);

            var getPersonalDataQuery = new GetPersonalDataQuery
            {
                sessionToken = testSessionToken,
            };

            var handler = new GetPersonalDataQueryHandler();
            TestDelegate result = () => handler.Handle(getPersonalDataQuery);

            SessionRepository.RemoveSession(testSessionToken);

            Assert.Throws<Exception>(result);
        }

        [TestCase(5)]
        public void GetPersonalData_ClientDoesntExist_Exception(int _clientId)
        {
            DatabaseQueryProcessor.Erase();
            Shared.FillTheDatabase();

            int clientId = _clientId;

            var testSessionToken = SessionRepository.StartNewSession(clientId);

            var getPersonalDataQuery = new GetPersonalDataQuery
            {
                sessionToken = testSessionToken,
            };

            var handler =  new GetPersonalDataQueryHandler();
            TestDelegate result = () => handler.Handle(getPersonalDataQuery);

            DatabaseQueryProcessor.Erase();
            SessionRepository.RemoveSession(testSessionToken);

            Assert.Throws<Exception>(result);
        }

        [TestCase(0)]
        public void GetPersonalData_CorrectData_Success(int _clientId)
        {
            DatabaseQueryProcessor.Erase();
            Shared.FillTheDatabase();

            int clientId = _clientId;

            var expectedPersonalData = new PersonalDataDto
            {
                isSuccess = true,
                email = "jane_doe@gmail.com",
                firstName = "Jane",
                lastName = "Doe",
                receiveNewsletterEmail = true,
                newsletterEmail = "jane_doe@gmail.com"
            };

            var testSessionToken = SessionRepository.StartNewSession(clientId);

            var getPersonalDataQuery = new GetPersonalDataQuery
            {
                sessionToken = testSessionToken,
            };

            var handler = new GetPersonalDataQueryHandler();
            var result = (PersonalDataDto) handler.Handle(getPersonalDataQuery);

            DatabaseQueryProcessor.Erase();
            SessionRepository.RemoveSession(testSessionToken);

            Assert.IsTrue(result.isSuccess);
            Assert.AreEqual(expectedPersonalData.email, result.email);
            Assert.AreEqual(expectedPersonalData.firstName, result.firstName);
            Assert.AreEqual(expectedPersonalData.lastName, result.lastName);
            Assert.AreEqual(expectedPersonalData.receiveNewsletterEmail, result.receiveNewsletterEmail);
            Assert.AreEqual(expectedPersonalData.newsletterEmail, result.newsletterEmail);
        }

    }
}