using Coffeeland.Messaging.Shared;

namespace Coffeeland.Messaging.Commands.Commands
{
    public class InactivateAddressCommand : ICommand
    {
        public string sessionToken;
        public int addressKey;
    }
}