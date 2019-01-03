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
using Coffeeland.Messaging.Shared;

namespace Coffeeland.Tests.Messaging.Commands.Handlers
{
    [TestFixture]
    public class RegisterNewClientCommandTest
    {
        [TestCase("magdtrag@gmail.com","Marek","Ochocki","admin123",true,"majek@gmail.com")]
        public void RegisterNewClient_CorrectData_Success(string _email, string _firstName, string _lastName, string _password, bool _receiveNewsletterEmail, string _newsletterEmail){

            DatabaseQueryProcessor.Erase();
            Shared.FillTheDatabase();
            
            var registerNewClient = new RegisterNewClientCommand
            {
                email = _email,
                firstName = _firstName,
                lastName = _lastName,
                password = _password,
                receiveNewsletterEmail = _receiveNewsletterEmail,
                newsletterEmail = _newsletterEmail
            };

            var handler = new RegisterNewClientCommandHandler();
            var result = (SuccessInfoDto)handler.Handle(registerNewClient);

            var newClient = DatabaseQueryProcessor.GetClient(_email, PasswordEncryptor.encryptSha256(_password));

            DatabaseQueryProcessor.Erase();

            Assert.IsNotNull(newClient);
            Assert.IsTrue(result.isSuccess);
            Assert.AreEqual(_firstName, newClient.firstName);
            Assert.AreEqual(_lastName, newClient.lastName);
            Assert.AreEqual(_newsletterEmail, newClient.newsletterEmail);
        }

        [TestCase("wrong_email", "Marek", "Ochocki", "admin123", true, "majek@gmail.com")]
        [TestCase("marek@gmail.com", "12345", "Ochocki", "admin123", true, "majek@gmail.com")]
        [TestCase("marek@gmail.com", "Marek", "12345", "admin123", true, "majek@gmail.com")]
        [TestCase("marek@gmail.com", "Marek", "Ochocki", "admin123", true, "wrong_email")]
        public void RegisterNewClient_WrongData_Exception(string _email, string _firstName, string _lastName, string _password, bool _receiveNewsletterEmail, string _newsletterEmail)
        {

            DatabaseQueryProcessor.Erase();
            Shared.FillTheDatabase();
            
            var registerNewClient = new RegisterNewClientCommand
            {
                email = _email,
                firstName = _firstName,
                lastName = _lastName,
                password = _password,
                receiveNewsletterEmail = _receiveNewsletterEmail,
                newsletterEmail = _newsletterEmail
            };

            var handler = new RegisterNewClientCommandHandler();
            TestDelegate result = () => handler.Handle(registerNewClient);

            DatabaseQueryProcessor.Erase();
            Assert.Throws<Exception>(result);
        }

    }
}