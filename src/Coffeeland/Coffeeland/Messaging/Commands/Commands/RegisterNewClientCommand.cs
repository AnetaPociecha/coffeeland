using Coffeeland.Messaging.Shared;

namespace Coffeeland.Messaging.Commands.Commands
{
    public class RegisterNewClientCommand : ICommand
    {
        public string email;
        public string firstName;
        public string lastName;
        public string password;
        public bool receiveNewsletterEmail;
        public string newsletterEmail;
    }
}