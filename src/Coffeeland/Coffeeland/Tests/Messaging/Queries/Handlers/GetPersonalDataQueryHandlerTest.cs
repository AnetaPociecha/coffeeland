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
        [TestCase(5)]
        public void AddAddress_ClientDoesntExist_Exception(int _clientId)
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
        public void AddAddress_CorrectData_Success(int _clientId)
        {
            DatabaseQueryProcessor.Erase();
            Shared.FillTheDatabase();

            int clientId = _clientId;

            string email = "marek@gmail.com";
            string firstName = "Marek";
            string lastName = "Ochocki";
            bool receiveNewsletterEmail = true;
            string newsletterEmail = "marek@gmail.com";

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
            Assert.AreEqual(email, result.email);
            Assert.AreEqual(firstName, result.firstName);
            Assert.AreEqual(lastName, result.lastName);
            Assert.AreEqual(receiveNewsletterEmail, result.receiveNewsletterEmail);
            Assert.AreEqual(newsletterEmail, result.newsletterEmail);
        }

    }
}