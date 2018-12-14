using Coffeeland.Messaging.Shared;

namespace Coffeeland.Messaging.Dtos
{
    public class AddressBookDto : IResult
    {
        public bool isSuccess;
        public AddressDto[] addresses;
    }
}