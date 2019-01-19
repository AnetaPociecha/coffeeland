﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Coffeeland.Messaging.Shared;

namespace Coffeeland.Messaging.Commands.Commands
{
    public class AddPayment : ICommand
    {
        public string sessionToken;
        public string paymentId;
    }
}