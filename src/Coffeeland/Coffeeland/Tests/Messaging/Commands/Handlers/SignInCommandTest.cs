using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Coffeeland.Database;
using Coffeeland.Messaging.Commands.Commands;
using Coffeeland.Messaging.Commands.Handlers;
using Coffeeland.Messaging.Dtos;
using Coffeeland.Session;
using Coffeeland.Tests.TestsShared;
using NUnit.Framework;

namespace Coffeeland.Tests.Messaging.Commands.Handlers
{
    [TestFixture]
    public class SignInCommandTest
    {
        [TestCase("marek@gmail.com", "admin123")]
        public void SignIn_ProperAttributes_Success(string _email,string _password)
        {
            DatabaseQueryProcessor.Erase();
            Shared.FillTheDatabase();

            var signInCommand = new SignInCommand
            {
                email = _email,
                password = _password
            };
            
            var handler = new SignInCommandHandler();
            var result = (SignInInfoDto) handler.Handle(signInCommand);

            DatabaseQueryProcessor.Erase();
            Assert.IsTrue(result.isSuccess);
        }

        [TestCase("wrong_email","admin123")]
        [TestCase("marek@gmail.com", "wrong_passwd")]
        public void SignIn_WrongData_Fail(string _email, string _password)
        {
            DatabaseQueryProcessor.Erase();
            Shared.FillTheDatabase();

            var signInCommand = new SignInCommand
            {
                email = _email,
                password = _password
            };

            var handler = new SignInCommandHandler();
            var result = (SignInInfoDto)handler.Handle(signInCommand);

            DatabaseQueryProcessor.Erase();
            Assert.IsFalse(result.isSuccess);
        }
    }


}