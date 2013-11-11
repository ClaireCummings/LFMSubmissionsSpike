using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NServiceBus;

namespace LFM.MessageService
{
    public static class Conventions
    {
        public static Configure MessageConventions(this Configure configure)
        {
            configure.DefiningCommandsAs(t => t.Namespace != null && t.Namespace.EndsWith(".Commands"));
            return configure;
        }
    }
}
