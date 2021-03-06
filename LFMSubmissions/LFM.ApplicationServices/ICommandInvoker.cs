﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LFM.ApplicationServices.LandRegistry;

namespace LFM.ApplicationServices
{
    public interface ICommandInvoker
    {
        void Execute<TCommand>(TCommand command);
        TResponse Execute<TCommand, TResponse>(TCommand command);
    }
}
