using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Coffeeland.Messaging.Shared;


namespace Coffeeland.Messaging.Dtos

{
    public class PersonalDataDto : IResult
    {
        public bool isSuccess;
        public string email;
        public string firstName;
        public string lastName;
        public bool receiveNewsletterEmail;
        public string newsletterEmail;
    }
}