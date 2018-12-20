using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Coffeeland.Database;
using Coffeeland.Messaging.Commands.Commands;
using Coffeeland.Messaging.Commands.Handlers;
using Coffeeland.Messaging.Dtos;
using Coffeeland.Messaging.Shared;
using Coffeeland.Session;
using Coffeeland.Tests.TestsShared;
using NUnit.Framework;


namespace Coffeeland.Tests.Messaging.Commands.Handlers
{
    [TestFixture]
    public class UpdatePersonalDataCommandTest
    {

        [TestCase("marek@gmail.com", "Marek", "Ochocki", true, "admin1234", false, "")] 
        public void UpdatePersonalData_ChangePassword_Success(string _email, string _firstName, string _lastName,bool _changePassword, string _newPassword, bool _receiveNewsletterEmail, string _newsletterEmail)
        { 
            DatabaseQueryProcessor.Erase();
            Shared.FillTheDatabase();

            int clientId = 0;
            var testSessionToken = SessionRepository.StartNewSession(clientId);

            var updatePersonalDataCommand = new UpdatePersonalDataCommand
            {
                sessionToken = testSessionToken,
                email = _email,
                firstName = _firstName,
                lastName = _lastName,
                changePassword = _changePassword,
                newPassword = _newPassword,
                receiveNewsletterEmail = _receiveNewsletterEmail,
                newsletterEmail = _newsletterEmail
            };
            
            var handler = new UpdatePersonalDataCommandHandler();
            var result = (PersonalDataDto) handler.Handle(updatePersonalDataCommand);

            var foundClient = DatabaseQueryProcessor.GetClient(_email, PasswordEncryptor.encryptSha256(_newPassword));

            DatabaseQueryProcessor.Erase();
            SessionRepository.RemoveSession(testSessionToken);

            Assert.IsNotNull(result);
            Assert.AreEqual(_email, result.email);
            Assert.AreEqual(_firstName, result.firstName);
            Assert.AreEqual(_lastName, result.lastName);
            Assert.IsNotNull(foundClient);
        }

        [TestCase("marek@gmail.com", "Marek", "Ochocki", true, "admin1234", true, "bad email")]
        [TestCase("marek@gmail.com", "bad name 123", "Ochocki", true, "admin1234", false, "")]
        [TestCase("merek@gmail.com", "Marek", "bad surname 123", true, "admin1234", false, "")]
        [TestCase("bad email", "Marek", "Ochocki", true, "admin1234", false, "")]
        public void UpdatePersonalData_WrongData_Exception(string _email, string _firstName, string _lastName, bool _changePassword, string _newPassword, bool _receiveNewsletterEmail, string _newsletterEmail)
        {
            DatabaseQueryProcessor.Erase();
            Shared.FillTheDatabase();

            int clientId = 0;
            var testSessionToken = SessionRepository.StartNewSession(clientId);

            var updatePersonalDataCommand = new UpdatePersonalDataCommand
            {
                sessionToken = testSessionToken,
                email = _email,
                firstName = _firstName,
                lastName = _lastName,
                changePassword = _changePassword,
                newPassword = _newPassword,
                receiveNewsletterEmail = _receiveNewsletterEmail,
                newsletterEmail = _newsletterEmail
            };

            var handler = new UpdatePersonalDataCommandHandler();
            TestDelegate result = () => handler.Handle(updatePersonalDataCommand);

            DatabaseQueryProcessor.Erase();
            SessionRepository.RemoveSession(testSessionToken);

            Assert.Throws<Exception>(result);
        }
    }
}