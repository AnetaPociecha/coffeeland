using Coffeeland.Messaging.Shared;

namespace Coffeeland.Messaging.Commands.Commands
{
    public class SignInCommand : ICommand
    {
        public string email;
        public string password;
    }
}