using LFM.LandRegistry.Commands;

namespace LFM.LandRegistry.SubmissionsService
{
    public class Lrap1Processor : IProcessMessages<SubmitLrap1Command>
    {
        public ISendMessages MessageSender { get; set; }
        public ICommsService CommsService { get; set; }

        public void Process(SubmitLrap1Command message)
        {
            var response = CommsService.Send(message);

            if (response == ResponseType.None)
            {
                MessageSender.Send(message);
            }
        }
    }
}