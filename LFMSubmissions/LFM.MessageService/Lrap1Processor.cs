using LFM.LandRegistry;
using LFM.LandRegistry.Commands;
using LFM.LandRegistry.CommsService;
using LFM.LandRegistry.SubmissionsService;
using NServiceBus;

namespace LFM.MessageService
{
    public class Lrap1Processor : IHandleMessages<SubmitLrap1Command>
    {
        public IBus Bus { get; set; }
        public IProcessMessages<SubmitLrap1Command> MessageProcessor { get; set; }

        public void Handle(SubmitLrap1Command message)
        {
            MessageProcessor.Process(message);
        }
    }
}