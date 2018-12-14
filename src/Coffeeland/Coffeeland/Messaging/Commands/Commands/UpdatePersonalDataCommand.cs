using Coffeeland.Messaging.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Coffeeland.Messaging.Commands.Commands
{
    public class UpdatePersonalDataCommand : ICommand
    {
        public string sessionToken;
        public string email;
        public string firstName;
        public string lastName;
        public bool changePassword;
        public string newPassword;
        public bool receiveNewsletterEmail;
        public string newsletterEmail;
    }
}