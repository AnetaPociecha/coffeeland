using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Coffeeland.Messaging.Shared;


namespace Coffeeland.Messaging.Dtos

{
    public class ClientDto : IResult
    {
        public int key;
        public string email;
        public string firstName;
        public string lastName;
        public List<OrderDto> orders;
        public string receiveNewsletterEmail;
    }
}