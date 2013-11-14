using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Autofac;

namespace LFM.LandRegistry.SubmissionsService
{
    public class MessageProcessor : IMessageProcessor
    {
        private readonly ILifetimeScope _lifetimeScope;

        public MessageProcessor(ILifetimeScope lifetimeScope)
        {
            _lifetimeScope = lifetimeScope;
        }

        public void Process<TMessage>(TMessage message)
        {
            var processor = _lifetimeScope.Resolve<IProcessMessages<TMessage>>();
            processor.Process(message);
        }
    }
}
