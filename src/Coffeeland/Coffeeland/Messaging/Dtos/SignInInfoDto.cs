using Coffeeland.Messaging.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Coffeeland.Messaging.Dtos
{
    public class SignInInfoDto : IResult
    {
        public bool isSuccess;
        public string sessionToken;
    }
}