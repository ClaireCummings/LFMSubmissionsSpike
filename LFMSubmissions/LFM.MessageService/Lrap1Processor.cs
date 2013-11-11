using LFM.LandRegistry.Commands;
using LFM.LandRegistry.CommsService;
using NServiceBus;

namespace LFM.MessageService
{
    public class Lrap1Processor : IHandleMessages<SubmitLrap1Command>
    {
        public CommsService CommsService { get; set; }
        public IBus Bus { get; set; }

        public void Handle(SubmitLrap1Command message)
        {
            var response = CommsService.Send(message);

            if (response == ResponseType.None)
            {
                Bus.SendLocal(message);
            }
        }
    }
}