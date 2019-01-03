using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Coffeeland.Messaging.Dtos;
using Coffeeland.Messaging.Queries.Handlers;
using Coffeeland.Messaging.Queries.Queries;
using Coffeeland.Session;
using NUnit.Framework;

namespace Coffeeland.Tests.Messaging.Queries.Handlers
{
    [TestFixture]
    public class SignOutQueryHandlerTest
    {
        [TestCase(0)]
        public void SignOut_CorrectData_Success(int _clientId)
        {
            var testSessionToken = SessionRepository.StartNewSession(_clientId);

            var signOutQuery = new SignOutQuery
            {
            };

            var handler = new SignOutQueryHandler();
            var result = (SuccessInfoDto)handler.Handle(signOutQuery);
            
            Assert.IsTrue(result.isSuccess);
        }
    }
}