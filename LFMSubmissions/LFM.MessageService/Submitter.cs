using System;
using LFM.LandRegistry;
using LFM.LandRegistry.Commands;
using NServiceBus;

namespace LFM.MessageService
{
    public class Submitter : ISubmitter
    {
        public IBus Bus { get; set; }

        public SubmitLrap1Result Send(SubmitLrap1Command command)
        {
            Bus.SendLocal(command);
            return  new SubmitLrap1Result()
            {
                Command = command
            };
        }

        public void Send(SubmitLrap1AttachmentCommand command)
        {
            throw new NotImplementedException();
        }
    }
}
