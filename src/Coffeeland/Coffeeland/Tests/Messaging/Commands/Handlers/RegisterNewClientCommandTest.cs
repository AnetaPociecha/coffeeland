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
        [TestCase("john_doe@gmail.com", "John", "Doe", "admin123", true, "john_doe@gmail.com")]
        public void RegisterNewClient_ClentExists_Exception(string _email, string _firstName, string _lastName, string _password, bool _receiveNewsletterEmail, string _newsletterEmail)
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
            
            Assert.Throws<Exception>(result);
            DatabaseQueryProcessor.Erase();
        }

        [TestCase("johny_doe@gmail.com","Johny","Doe","admin123",true,"johny_doe@gmail.com")]
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

        //[TestCase("wrong_email")]
        //[TestCase("123@gmail.com")]
        [TestCase("wrong.gmail.com")]
        //[TestCase("wrongemail@gmail")]
        //[TestCase("wrongemail@122.pl")]
        //[TestCase("wrongemail%%%%@gmail.com")]
        public void RegisterNewClient_IncorrectEmail_Exception(string _email)
        { 
            DatabaseQueryProcessor.Erase();
            Shared.FillTheDatabase();
            
            var registerNewClient = new RegisterNewClientCommand
            {
                email = _email,
                firstName = "Jane",
                lastName = "Doe",
                password = "admin123",
                receiveNewsletterEmail = false,
                newsletterEmail = ""
            };

            var handler = new RegisterNewClientCommandHandler();
            TestDelegate result = () => handler.Handle(registerNewClient);

            DatabaseQueryProcessor.Erase();
            Assert.Throws<Exception>(result);
        }


        //[TestCase("wrong_email")]
        //[TestCase("123@gmail.com")]
        [TestCase("wrong.gmail.com")]
        //[TestCase("wrongemail@gmail")]
        //[TestCase("wrongemail@122.pl")]
        //[TestCase("wrongemail%%%%@gmail.com")]
        public void RegisterNewClient_IncorrectNewsletterEmail_Exception(string _newsletterEmail)
        {

            DatabaseQueryProcessor.Erase();
            Shared.FillTheDatabase();

            var registerNewClient = new RegisterNewClientCommand
            {
                email = "janedoe@gmail.com",
                firstName = "Jane",
                lastName = "Doe",
                password = "admin123",
                receiveNewsletterEmail = true,
                newsletterEmail = _newsletterEmail
            };

            var handler = new RegisterNewClientCommandHandler();
            TestDelegate result = () => handler.Handle(registerNewClient);

            DatabaseQueryProcessor.Erase();
            Assert.Throws<Exception>(result);
        }

        //[TestCase("Jane_wrong")]
        [TestCase("Jane123")]
        [TestCase("Jane&&&")]
        public void RegisterNewClient_IncorrectFirstName_Exception(string _firstName)
        {

            DatabaseQueryProcessor.Erase();
            Shared.FillTheDatabase();

            var registerNewClient = new RegisterNewClientCommand
            {
                email = "janedoe@gmail.com",
                firstName = _firstName,
                lastName = "Doe",
                password = "admin123",
                receiveNewsletterEmail = false,
                newsletterEmail = ""
            };

            var handler = new RegisterNewClientCommandHandler();
            TestDelegate result = () => handler.Handle(registerNewClient);

            DatabaseQueryProcessor.Erase();
            Assert.Throws<Exception>(result);
        }

        //[TestCase("Doe_wrong")]
        [TestCase("Doe123")]
        [TestCase("Doe&&&")]
        public void RegisterNewClient_IncorrectLastName_Exception(string _lastName)
        {

            DatabaseQueryProcessor.Erase();
            Shared.FillTheDatabase();

            var registerNewClient = new RegisterNewClientCommand
            {
                email = "janedoe@gmail.com",
                firstName = "Jane",
                lastName = _lastName,
                password = "admin123",
                receiveNewsletterEmail = false,
                newsletterEmail = ""
            };

            var handler = new RegisterNewClientCommandHandler();
            TestDelegate result = () => handler.Handle(registerNewClient);

            DatabaseQueryProcessor.Erase();
            Assert.Throws<Exception>(result);
        }
    }
}