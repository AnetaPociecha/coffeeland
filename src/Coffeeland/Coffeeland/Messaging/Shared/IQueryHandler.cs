﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Coffeeland.Messaging.Shared
{
    public interface IQueryHandler<Q> where Q : IQuery
    {
        IResult Handle(Q query);
    }
}