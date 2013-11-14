using System;
using LFM.LandRegistry.Commands;
using NServiceBus;

namespace LFM.MessageService
{
    public class Lrap1Processor : IHandleMessages<SubmitLrap1Command>, IHandleMessages<SubmitLrap1AttachmentCommand>
    {
        //public IProcessMessages<SubmitLrap1Command> MessageProcessor { get; set; }
        //public IProcessMessages<SubmitLrap1AttachmentCommand> AttachmentMessageProcessor { get; set; }

        public IMessageProcessor MessageProcessor { get; set; }

        public void Handle(SubmitLrap1Command message)
        {
            Console.WriteLine("Received Message " + message.GetType().Name);
            MessageProcessor.Process(message);
        }

        public void Handle(SubmitLrap1AttachmentCommand message)
        {
            Console.WriteLine("Received Message " + message.GetType().Name);
            MessageProcessor.Process(message);
        }
    }
}